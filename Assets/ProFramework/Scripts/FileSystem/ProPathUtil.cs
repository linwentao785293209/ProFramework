using System.IO;

namespace ProFramework
{
    /// <summary>
    /// 提供路径操作的实用方法。
    /// </summary>
    public static class ProPathUtil
    {
        /// <summary>
        /// 获取完整的文件路径。
        /// </summary>
        public static string GetFilePath(string directory, string fileName, string extension = "")
        {
            return Path.Combine(directory, $"{fileName}{(string.IsNullOrEmpty(extension) ? "" : "." + extension)}");
        }

        /// <summary>
        /// 获取文件的目录部分。
        /// </summary>
        public static string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// 获取文件名部分。
        /// </summary>
        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary>
        /// 获取文件名和扩展名。
        /// </summary>
        public static string GetFileNameWithExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>
        /// 获取文件的扩展名。
        /// </summary>
        public static string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }
    }
}