using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AmigoSecreto.Services
{
    public static class SomeExtensions
    {
        public static string GetFile(this DirectoryInfo directory, string fileName)
        {
            return Path.Combine(directory.FullName, fileName);
        }

        public static bool GetFile(this DirectoryInfo directory, string fileName, out string path)
        {
            path = directory.GetFile(fileName);
            return File.Exists(path);
        }

        public static bool IsNullOrWhite(this string str) => string.IsNullOrWhiteSpace(str);

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var rnd = new Random();
            return enumerable.OrderBy(_ => rnd.Next());
        }
    }
}
