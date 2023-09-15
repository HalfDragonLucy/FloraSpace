using FloraSpace.Core;
using FloraSpace.Logging;
using FloraSpace.Updater;

namespace FloraSpace.GUI
{
    public partial class GUI : Form
    {
        readonly IUpdater Updater = new ApplicationUpdater("FloraSpace");
        readonly IFileDeleter FileDeleter = new FileDeleter();
        readonly IFolderDeleter FolderDeleter = new FolderDeleter();
        private readonly string CurrentVersion = Application.ProductVersion;

        public GUI()
        {
            InitializeComponent();
            Select();

            #region Initialize Updater
            Log.LogMessage($"Current Version: v{CurrentVersion}");
            VersionLabel.Text = $"v{CurrentVersion}";

            if (!Updater.IsConnectedToInternet())
            {
                return;
            }

            string onlineVersion = Task.Run(async () =>
            {
                return await Updater.CheckForNewReleaseAsync("HalfDragonLucy", "FloraSpace");
            }).Result;

            if (onlineVersion == string.Empty)
            {
                return;
            }

            Log.LogMessage($"Online Version: {onlineVersion}");

            if (Updater.IsVersionGreaterThanCurrent(onlineVersion, CurrentVersion))
            {
                BtnUpdateProgram.Visible = true;
            }
            #endregion
        }


        private async void BtnUpdateProgram_Click(object sender, EventArgs e)
        {
            if (!Updater.IsConnectedToInternet())
            {
                return;
            }

            await Updater.UpdateFromGitHubAsync("HalfDragonLucy", "FloraSpace", "setup.exe");
        }

        private async void BtnTempFiles_Click(object sender, EventArgs e)
        {
            string tempFolderPath = Path.GetTempPath();

            try
            {
                string[] tempFiles = Directory.GetFiles(tempFolderPath);

                foreach (string tempFile in tempFiles)
                {
                    IProgress<int> progress = new Progress<int>(percent =>
                    {
                        CoreProgress.ProgressBar.Value = percent;
                    });

                    CancellationTokenSource cancellationTokenSource = new();

                    Exception? deleteError = await FileDeleter.DeleteFileAsync(tempFile, progress, cancellationTokenSource.Token);

                    CoreProgress.ProgressBar.Value = 0;
                }

                string[] tempDirs = Directory.GetDirectories(tempFolderPath);

                foreach (string tempdir in tempDirs)
                {

                    IProgress<int> folderDeletionProgress = new Progress<int>(percent =>
                    {
                        CoreProgress.ProgressBar.Value = percent;
                    });

                    CancellationTokenSource folderDeletionCancellationTokenSource = new();

                    Exception? folderDeleteError = await FolderDeleter.DeleteFolderAsync(tempdir, folderDeletionProgress, folderDeletionCancellationTokenSource.Token);

                    CoreProgress.ProgressBar.Value = 0;
                }

                CoreProgress.ProgressBar.Value = 0;
            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error deleting temp files: {ex.Message}", LogLevel.ERROR);
            }
        }

    }
}