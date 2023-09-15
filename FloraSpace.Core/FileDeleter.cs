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
                        return;
                    }

                    File.Delete(filePath);
                    progress.Report(100);
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
