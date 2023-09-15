using FloraSpace.Logging;

namespace FloraSpace.Core
{
    /// <summary>
    /// Provides methods to delete a single file.
    /// </summary>
    public interface IFileDeleter
    {
        /// <summary>
        /// Asynchronously deletes a single file.
        /// </summary>
        /// <param name="filePath">The path of the file to delete.</param>
        /// <param name="progress">An IProgress instance to report progress.</param>
        /// <param name="cancellationToken">A CancellationToken to support cancellation.</param>
        /// <returns>
        /// An Exception if an error occurs during the deletion process.
        /// </returns>
        Task<Exception?> DeleteFileAsync(string filePath, IProgress<int> progress, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Implements IFileDeleter for deleting a single file.
    /// </summary>
    public class FileDeleter : IFileDeleter
    {
        public async Task<Exception?> DeleteFileAsync(string filePath, IProgress<int> progress, CancellationToken cancellationToken)
        {
            Exception? error = null;

            await Task.Run(() =>
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        error = new OperationCanceledException("Deletion operation was canceled.");
                        Log.LogMessage($"Canceled file deletion: {filePath}");
                        return;
                    }

                    File.Delete(filePath);
                    progress.Report(100);
                    Log.LogMessage($"Deleted file: {filePath}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    error = ex;
                    Log.LogMessage($"Error deleting file due to unauthorized access: {filePath}", LogLevel.ERROR);
                }
                catch (IOException ex)
                {
                    error = ex;
                    Log.LogMessage($"Error deleting file due to I/O exception: {filePath}", LogLevel.ERROR);
                }
                catch (Exception ex)
                {
                    error = ex;
                    Log.LogMessage($"Error deleting file: {filePath}, Error: {ex.Message}", LogLevel.ERROR);
                }
            }, cancellationToken);

            return error;
        }
    }
}
