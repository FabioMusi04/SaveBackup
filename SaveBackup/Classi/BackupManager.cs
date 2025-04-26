using System.Text.Json;

namespace SaveBackup.Classi
{
    internal class BackupManager
    {
        public string BackupRootPath { get; }

        public BackupManager(string rootPath)
        {
            BackupRootPath = rootPath;
            Directory.CreateDirectory(BackupRootPath);
        }

        public void CreateBackup(string sourcePath)
        {
            if (sourcePath.EndsWith(@"\Backup"))
                throw new InvalidOperationException("La cartella di backup non può essere quella dei salvataggi.");

            string gameFolderName = Path.GetFileName(sourcePath);
            string backupFolder = Path.Combine(BackupRootPath, gameFolderName);

            CopyDirectory(sourcePath, backupFolder);

            var info = new BackupInfo { OriginalPath = sourcePath };
            string infoPath = Path.Combine(backupFolder, "info.json");
            File.WriteAllText(infoPath, JsonSerializer.Serialize(info));
        }

        public void RestoreBackup(string backupFolderName, string targetPath)
        {
            string sourcePath = Path.Combine(BackupRootPath, backupFolderName);
            CopyDirectory(sourcePath, targetPath);
        }

        public BackupInfo? GetBackupInfo(string backupFolderName)
        {
            string infoPath = Path.Combine(BackupRootPath, backupFolderName, "info.json");

            if (!File.Exists(infoPath)) return null;

            string json = File.ReadAllText(infoPath);
            return JsonSerializer.Deserialize<BackupInfo>(json);
        }

        public List<string> GetAvailableBackups()
        {
            return [.. Directory.GetDirectories(BackupRootPath)
                            .Select(Path.GetFileName)
                            .Where(name => name != null)
                            .Cast<string>()];
        }

        private static void CopyDirectory(string sourceDir, string destinationDir)
        {
            foreach (string dirPath in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourceDir, destinationDir));
            }

            foreach (string filePath in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
            {
                string destFilePath = filePath.Replace(sourceDir, destinationDir);
                Directory.CreateDirectory(Path.GetDirectoryName(destFilePath)!);

                using FileStream source = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using FileStream dest = new(destFilePath, FileMode.Create, FileAccess.Write);
                source.CopyTo(dest);
            }
        }
    }
}
