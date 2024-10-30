using System.IO;

namespace ProFramework
{
    /// <summary>
    /// 提供目录操作的实用方法。
    /// </summary>
    public static class ProDirectoryUtil
    {
        /// <summary>
        /// 判断目录是否存在。
        /// </summary>
        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 创建目录。
        /// </summary>
        public static DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 删除目录。
        /// </summary>
        public static void DeleteDirectory(string path, bool recursive = false)
        {
            if (DirectoryExists(path))
            {
                Directory.Delete(path, recursive);
            }
        }

        /// <summary>
        /// 获取子目录。
        /// </summary>
        public static string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        /// <summary>
        /// 获取文件。
        /// </summary>
        public static string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        /// <summary>
        /// 移动目录。
        /// </summary>
        public static void MoveDirectory(string sourceDir, string destDir)
        {
            Directory.Move(sourceDir, destDir);
        }

        /// <summary>
        /// 获取上级目录。
        /// </summary>
        public static DirectoryInfo GetParentDirectory(string path)
        {
            return Directory.GetParent(path);
        }
    }
}