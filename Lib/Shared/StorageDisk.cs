using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blazor_App.Shared
{
    public class StorageDisk
    {
        public const string DirName = "StorageDisk";
        public static string Dir { get; private set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        public static void SetDir(string dir)
        {
            Dir = dir;
        }
        public static string GetBibleDir()
        {
            var path = Path.Combine(Dir, DirName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}
