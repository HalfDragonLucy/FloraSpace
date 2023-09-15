namespace FloraSpace.Logging
{
    public static class Log
    {
        private static readonly object lockObject = new();

        public static void LogMessage(string message, LogLevel logLevel = LogLevel.INFO)
        {
            string logEntry = $"[{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fff}] [{logLevel}] {message}";

            lock (lockObject)
            {
                string logFolderPath = "Logs";
                Directory.CreateDirectory(logFolderPath);

                string logFileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".log";
                string logFilePath = Path.Combine(logFolderPath, logFileName);

                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
        }
    }

    public enum LogLevel
    {
        INFO,
        WARNING,
        ERROR
    }
}
