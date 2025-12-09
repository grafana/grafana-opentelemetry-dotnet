# Releasing

> [!IMPORTANT]
> Releases are [immutable][immutable-releases] and cannot be changed or their
> associated tag deleted once published.
>
> However, the description can still be edited to fix any mistakes or omissions
> after publishing.

1. Open the [publish-release workflow][publish-release]
1. Click on the **Run workflow** button
1. If required, enter a specific version number (e.g. `x.y.z`) in the version field.
   If left blank, the version will be auto-incremented to the next patch version
   since the [latest release][latest-release].
1. Wait for the workflow to complete successfully.
1. The publish-release workflow will have created a new tag for the relevant version,
   which starts the [CI][ci] workflow to build that tag.
1. Wait for the CI workflow to complete successfully. This will create a new draft
   release with the NuGet packages attached to it pointing to the tag that was created.
1. Click the link in the CI workflow run summary to the release draft that was created.
1. Click the edit button (pencil icon) at the top right of the release notes.
1. Verify that the release notes are correct. Make any manual adjustments if necessary.
1. Click on **Publish release**.
1. Wait for the [publish-packages workflow][publish-packages] to complete
   successfully. This will publish the NuGet packages to [NuGet.org][nuget].

<!-- editorconfig-checker-disable -->
<!-- markdownlint-disable MD013 -->

[ci]: https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/ci.yml
[immutable-releases]: https://docs.github.com/code-security/supply-chain-security/understanding-your-software-supply-chain/immutable-releases
[latest-release]: https://github.com/grafana/grafana-opentelemetry-dotnet/releases/tag/1.3.0
[nuget]: https://www.nuget.org/profiles/Grafana
[publish-packages]: https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/publish-packages.yml
[publish-release]: https://github.com/grafana/grafana-opentelemetry-dotnet/actions/workflows/publish-release.yml
