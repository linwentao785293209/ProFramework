using System.IO;

namespace ProFramework
{
    /// <summary>
    /// 提供文件信息操作的实用方法。
    /// </summary>
    public static class ProFileInfoUtil
    {
        /// <summary>
        /// 获取文件信息。
        /// </summary>
        public static FileInfo GetFileInfo(string path)
        {
            return new FileInfo(path);
        }

        /// <summary>
        /// 获取文件名。
        /// </summary>
        public static string GetFileName(string path)
        {
            return GetFileInfo(path).Name;
        }

        /// <summary>
        /// 获取文件全路径。
        /// </summary>
        public static string GetFullPath(string path)
        {
            return GetFileInfo(path).FullName;
        }

        /// <summary>
        /// 获取文件大小。
        /// </summary>
        public static long GetFileSize(string path)
        {
            return GetFileInfo(path).Length;
        }

        /// <summary>
        /// 获取文件扩展名。
        /// </summary>
        public static string GetFileExtension(string path)
        {
            return GetFileInfo(path).Extension;
        }
    }
}