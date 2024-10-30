using System.IO;

namespace ProFramework
{
    /// <summary>
    /// 提供目录信息操作的实用方法。
    /// </summary>
    public static class ProDirectoryInfoUtil
    {
        /// <summary>
        /// 获取目录信息。
        /// </summary>
        public static DirectoryInfo GetDirectoryInfo(string path)
        {
            return new DirectoryInfo(path);
        }

        /// <summary>
        /// 获取目录的全路径。
        /// </summary>
        public static string GetFullPath(string path)
        {
            return GetDirectoryInfo(path).FullName;
        }

        /// <summary>
        /// 获取目录的名称。
        /// </summary>
        public static string GetName(string path)
        {
            return GetDirectoryInfo(path).Name;
        }

        /// <summary>
        /// 获取子目录信息。
        /// </summary>
        public static DirectoryInfo[] GetDirectories(DirectoryInfo directoryInfo)
        {
            return directoryInfo.GetDirectories();
        }

        /// <summary>
        /// 获取文件信息。
        /// </summary>
        public static FileInfo[] GetFiles(DirectoryInfo directoryInfo)
        {
            return directoryInfo.GetFiles();
        }
    }
}