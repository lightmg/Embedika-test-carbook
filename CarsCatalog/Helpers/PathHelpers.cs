using System.IO;
using System.Reflection;
using System.Text;

namespace CarsCatalog.Helpers
{
    public static class PathHelpers
    {
        public static string BaseDirectory { get; } =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string GetPath(params string[] parts)
        {
            var path = BaseDirectory;
            foreach (var part in parts)
            {
                EnsureDirectory(path);
                path = Path.Combine(path, part);
            }

            if (!parts[^1].Contains('.'))
                EnsureDirectory(path);
            return path;
        }

        public static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static StreamWriter OpenWrite(string path, bool append = true) =>
            new StreamWriter(path, append, Encoding.UTF8);
    }
}