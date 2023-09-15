using FloraSpace.Logging;
using Octokit;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace FloraSpace.Updater
{
    /// <summary>
    /// Provides functionality to interact with GitHub repositories for updating applications.
    /// </summary>
    public interface IUpdater
    {
        /// <summary>
        /// Downloads and executes the latest release from a GitHub repository.
        /// </summary>
        /// <param name="repoOwner">The owner of the GitHub repository.</param>
        /// <param name="repoName">The name of the GitHub repository.</param>
        /// <param name="executableName">The name of the executable file in the release assets.</param>
        Task UpdateFromGitHubAsync(string repoOwner, string repoName, string executableName);

        /// <summary>
        /// Checks for a new release in a GitHub repository.
        /// </summary>
        /// <param name="repoOwner">The owner of the GitHub repository.</param>
        /// <param name="repoName">The name of the GitHub repository.</param>
        /// <returns>The tag name (version) of the latest release if found; otherwise, an empty string.</returns>
        Task<string> CheckForNewReleaseAsync(string repoOwner, string repoName);

        /// <summary>
        /// Checks if the user is connected to the internet.
        /// </summary>
        /// <returns>True if the user is connected to the internet; otherwise, false.</returns>
        bool IsConnectedToInternet();

        /// <summary>
        /// Checks if the new version is greater than the current version of the program.
        /// </summary>
        /// <param name="newVersion">The new version string in the format "yyyy.MM.dd".</param>
        /// <param name="currentVersion">The current version string in the format "yyyy.MM.dd".</param>
        /// <returns>True if the new version is greater than the current version; otherwise, false.</returns>
        bool IsVersionGreaterThanCurrent(string newVersion, string currentVersion);
    }

    /// <summary>
    /// Provides functionality to interact with GitHub repositories for updating applications.
    /// </summary>
    public class ApplicationUpdater : IUpdater
    {
        private readonly GitHubClient GitHubClient;

        public ApplicationUpdater(string productHeader)
        {
            GitHubClient = new GitHubClient(new ProductHeaderValue(productHeader));
        }

        public async Task UpdateFromGitHubAsync(string repoOwner, string repoName, string executableName)
        {
            try
            {
                Log.LogMessage($"Updating from GitHub: {repoOwner}/{repoName}, Executable: {executableName}");

                string latestRelease = await CheckForNewReleaseAsync(repoOwner, repoName);

                if (latestRelease != null)
                {
                    var releases = await GitHubClient.Repository.Release.GetAll(repoOwner, repoName);

                    foreach (var asset in releases[0].Assets)
                    {
                        if (asset.Name.Equals(executableName, StringComparison.OrdinalIgnoreCase))
                        {
                            Log.LogMessage($"Downloading {executableName}...");

                            string downloadFilePath = Path.Combine(Path.GetTempPath(), asset.Name);

                            using (var httpClient = new HttpClient())
                            {
                                var assetStream = await httpClient.GetStreamAsync(asset.BrowserDownloadUrl);
                                using var fileStream = File.Create(downloadFilePath);
                                await assetStream.CopyToAsync(fileStream);
                            }

                            Log.LogMessage($"Downloaded {executableName}.");

                            var process = new Process();
                            process.StartInfo.FileName = downloadFilePath;
                            process.StartInfo.UseShellExecute = true;
                            process.Start();

                            Environment.Exit(ExitCodes.UpdateSuccess);
                            process.WaitForExit();
                            File.Delete(downloadFilePath);
                        }
                    }
                }
                else
                {
                    Log.LogMessage($"No updates found for {executableName}.");
                    Environment.Exit(ExitCodes.AlreadyUpToDate);
                }
            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error during update: {ex.Message}", LogLevel.ERROR);
                Environment.Exit(ExitCodes.UpdateError);
            }
        }

        public async Task<string> CheckForNewReleaseAsync(string repoOwner, string repoName)
        {
            try
            {
                Log.LogMessage($"Checking for new release in {repoOwner}/{repoName}...");

                var releases = await GitHubClient.Repository.Release.GetAll(repoOwner, repoName);

                if (releases.Count > 0)
                {
                    var latestRelease = releases[0];
                    Log.LogMessage($"Latest release found: {latestRelease.TagName}");
                    return latestRelease.TagName;
                }
                else
                {
                    Log.LogMessage("No releases found.");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error checking for new release: {ex.Message}", LogLevel.ERROR);
                return string.Empty;
            }
        }

        public bool IsConnectedToInternet()
        {
            try
            {
                NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    if (networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        Log.LogMessage("Connected to the internet.");
                        return true;
                    }
                }

                Log.LogMessage("Not connected to the internet.");
                return false;
            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error checking internet connection: {ex.Message}", LogLevel.ERROR);
                return false;
            }
        }

        public bool IsVersionGreaterThanCurrent(string newVersion, string currentVersion)
        {
            try
            {
                var newVersionDate = DateTime.Parse(newVersion);
                var currentVersionDate = DateTime.Parse(currentVersion);

                bool isGreater = newVersionDate > currentVersionDate;

                Log.LogMessage($"Comparing versions: New version ({newVersion}) is greater than current version ({currentVersion}): {isGreater}");

                return isGreater;
            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error comparing versions: {ex.Message}", LogLevel.ERROR);
                return false;
            }
        }
    }
}
