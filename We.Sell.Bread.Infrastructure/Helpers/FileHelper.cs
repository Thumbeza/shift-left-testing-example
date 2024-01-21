namespace We.Sell.Bread.Infrastructure.Helpers
{
    public static class FileHelper
    {
        private static readonly string WorkingDirectory = Environment.CurrentDirectory;
        private static readonly DirectoryInfo DirectoryInfo = TryGetSolutionDirectoryInfo();

        public static string? GetBasePath()
        {
            return DirectoryInfo?.FullName;
        }

        private static DirectoryInfo? TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory;
        }

    }
}
