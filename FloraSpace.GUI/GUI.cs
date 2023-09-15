using FloraSpace.Core;
using FloraSpace.Logging;
using FloraSpace.Updater;

namespace FloraSpace.GUI
{
    public partial class GUI : Form
    {
        readonly IUpdater Updater = new ApplicationUpdater("FloraSpace");
        readonly IFileDeleter FileDeleter = new FileDeleter();
        private readonly string CurrentVersion = Application.ProductVersion;

        public GUI()
        {
            InitializeComponent();

            #region Initialize Updater
            VersionLabel.Text = $"v{CurrentVersion}";

            if (!Updater.IsConnectedToInternet())
            {
                return;
            }

            string onlineVersion = Updater.CheckForNewReleaseAsync("HalfDragonLucy", "FloraSpace").Result;

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
                        Log.LogMessage($"Deleting file: {tempFile}, Progress: {percent}%");
                    });

                    CancellationTokenSource cancellationTokenSource = new();

                    Exception? deleteError = await FileDeleter.DeleteFileAsync(tempFile, progress, cancellationTokenSource.Token);

                    if (deleteError == null)
                    {
                        Log.LogMessage($"Deleted file: {tempFile}");
                    }
                    else
                    {
                        Log.LogMessage($"Error deleting file: {tempFile}, Error: {deleteError.Message}", LogLevel.ERROR);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error deleting temp files: {ex.Message}", LogLevel.ERROR);
            }
        }
    }
}