//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Collections.Generic;
using OpenTelemetry;
using OpenTelemetry.Resources;

namespace Grafana.OpenTelemetry.Tests
{
    /// <summary>
    /// The upstream `InMemoryExporter` doesn't capture resources.
    ///
    /// This implementation adds support for capturing the resource.
    /// </summary>
    public class InMemoryResourceExporter<T> : BaseExporter<T>
        where T : class
    {
        private readonly ICollection<(T, Resource)> exportedItems;
        private readonly ExportFunc onExport;
        private bool disposed;
        private string disposedStackTrace;

        public InMemoryResourceExporter(ICollection<(T, Resource)> exportedItems)
        {
            this.exportedItems = exportedItems;
            this.onExport = this.DefaultExport;
        }

        private InMemoryResourceExporter(ExportFunc exportFunc)
        {
            this.onExport = exportFunc;
        }

        internal delegate ExportResult ExportFunc(in Batch<T> batch);

        public override ExportResult Export(in Batch<T> batch)
        {
            if (this.disposed)
            {
                // Note: In-memory exporter is designed for testing purposes so this error is strategic to surface lifecycle management bugs during development.
                throw new ObjectDisposedException(
                    this.GetType().Name,
                    $"The in-memory exporter is still being invoked after it has been disposed. This could be the result of invalid lifecycle management of the OpenTelemetry .NET SDK. Dispose was called on the following stack trace:{Environment.NewLine}{this.disposedStackTrace}");
            }

            return this.onExport(batch);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposedStackTrace = Environment.StackTrace;
                this.disposed = true;
            }

            base.Dispose(disposing);
        }

        private ExportResult DefaultExport(in Batch<T> batch)
        {
            if (this.exportedItems == null)
            {
                return ExportResult.Failure;
            }

            foreach (var data in batch)
            {
                this.exportedItems.Add((data, this.ParentProvider.GetResource()));
            }

            return ExportResult.Success;
        }
    }
}
