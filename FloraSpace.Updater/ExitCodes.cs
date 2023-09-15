namespace FloraSpace.Updater
{
    /// <summary>
    /// Provides default exit codes for different handling scenarios.
    /// </summary>
    public static class ExitCodes
    {
        /// <summary>
        /// The default exit code for a successful update.
        /// </summary>
        public const int UpdateSuccess = 0;

        /// <summary>
        /// The default exit code when an error occurs during the update process.
        /// </summary>
        public const int UpdateError = 1;

        /// <summary>
        /// The default exit code when an error occurs during the initialization process.
        /// </summary>
        public const int InitializationError = 2;

        /// <summary>
        /// The default exit code when the application is already up-to-date.
        /// </summary>
        public const int AlreadyUpToDate = 3;

        /// <summary>
        /// The default exit code when the application cannot connect to the internet.
        /// </summary>
        public const int NoInternetConnection = 4;

        /// <summary>
        /// The default exit code for other unspecified errors.
        /// </summary>
        public const int UnknownError = 5;
    }
}
