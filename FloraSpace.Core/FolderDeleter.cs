namespace FloraSpace.Core
{
    /// <summary>
    /// Provides methods to delete a folder and its contents.
    /// </summary>
    public interface IFolderDeleter
    {
        /// <summary>
        /// Asynchronously deletes a folder and its contents.
        /// </summary>
        /// <param name="folderPath">The path of the folder to delete.</param>
        /// <param name="progress">An IProgress instance to report progress.</param>
        /// <param name="cancellationToken">A CancellationToken to support cancellation.</param>
        /// <returns>
        /// An Exception if an error occurs during the deletion process.
        /// </returns>
        Task<Exception?> DeleteFolderAsync(string folderPath, IProgress<int> progress, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Implements IFolderDeleter for deleting a folder and its contents.
    /// </summary>
    public class FolderDeleter : IFolderDeleter
    {
        public async Task<Exception?> DeleteFolderAsync(string folderPath, IProgress<int> progress, CancellationToken cancellationToken)
        {
            Exception? error = null;

            await Task.Run(() =>
            {
                try
                {
                    string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
                    int totalFiles = files.Length;
                    int filesDeleted = 0;

                    foreach (string filePath in files)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            error = new OperationCanceledException("Deletion operation was canceled.");
                            return;
                        }

                        try
                        {
                            File.Delete(filePath);
                            filesDeleted++;

                            int percentComplete = (int)((double)filesDeleted / totalFiles * 100);
                            progress.Report(percentComplete);
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            error = ex;
                        }
                        catch (IOException ex)
                        {
                            error = ex;
                        }
                        catch (Exception ex)
                        {
                            error = ex;
                        }
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        Directory.Delete(folderPath, true);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    error = ex;
                }
                catch (IOException ex)
                {
                    error = ex;
                }
                catch (Exception ex)
                {
                    error = ex;
                }
            }, cancellationToken);

            return error;
        }
    }
}
