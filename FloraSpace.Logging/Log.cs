namespace FloraSpace.Logging
{
    public static class Log
    {
        private static readonly object lockObject = new();
        private static bool isInitialized = false;

        private const string LogFolderName = "Logs";
        private const string LogFileName = "FloraSpace.log";

        public static void LogMessage(string message, LogLevel logLevel = LogLevel.INFO)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fff}] [{logLevel}] {message}";

            lock (lockObject)
            {
                if (!isInitialized)
                {
                    InitializeLogFile();
                    isInitialized = true;
                }

                string logFilePath = Path.Combine(LogFolderName, LogFileName);

                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
        }

        private static void InitializeLogFile()
        {
            Directory.CreateDirectory(LogFolderName);
            string logFilePath = Path.Combine(LogFolderName, LogFileName);
            File.Create(logFilePath).Close();
        }
    }

    public enum LogLevel
    {
        INFO,
        WARNING,
        ERROR
    }
}