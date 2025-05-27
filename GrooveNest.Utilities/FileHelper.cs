using Microsoft.AspNetCore.Http;

namespace GrooveNest.Utilities
{
    public static class FileHelper
    {
        public static string SaveFile(Stream stream, string fileName, string subDirectory)
        {
            var safeFileName = Path.GetFileName(fileName);
            var savePath = Path.Combine("wwwroot", subDirectory, safeFileName);
            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

            stream.Position = 0;
            using var fileStream = new FileStream(savePath, FileMode.Create);
            stream.CopyTo(fileStream);

            return $"/{subDirectory}/{safeFileName}";
        }

    }
}
