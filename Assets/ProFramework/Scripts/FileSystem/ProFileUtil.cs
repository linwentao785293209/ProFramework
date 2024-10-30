using System.IO;

namespace ProFramework
{
    /// <summary>
    /// 提供文件操作的实用方法。
    /// </summary>
    public static class ProFileUtil
    {
        /// <summary>
        /// 判断文件是否存在。
        /// </summary>
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 创建文件。
        /// </summary>
        public static void CreateFile(string path)
        {
            if (!FileExists(path))
            {
                using (File.Create(path))
                {
                }
            }
        }

        /// <summary>
        /// 写入字节数组到文件。
        /// </summary>
        public static void WriteAllBytes(string path, byte[] bytes)
        {
            CreateFile(path);
            File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// 写入字符串数组到文件。
        /// </summary>
        public static void WriteAllLines(string path, string[] lines)
        {
            CreateFile(path);
            File.WriteAllLines(path, lines);
        }

        /// <summary>
        /// 写入文本到文件。
        /// </summary>
        public static void WriteAllText(string path, string text)
        {
            CreateFile(path);
            File.WriteAllText(path, text);
        }

        /// <summary>
        /// 读取字节数组。
        /// </summary>
        public static byte[] ReadAllBytes(string path)
        {
            if (FileExists(path))
            {
                return File.ReadAllBytes(path);
            }

            return null;
        }

        /// <summary>
        /// 读取字符串数组。
        /// </summary>
        public static string[] ReadAllLines(string path)
        {
            if (FileExists(path))
            {
                return File.ReadAllLines(path);
            }

            return null;
        }

        /// <summary>
        /// 读取文本。
        /// </summary>
        public static string ReadAllText(string path)
        {
            if (FileExists(path))
            {
                return File.ReadAllText(path);
            }

            return null;
        }

        /// <summary>
        /// 删除文件。
        /// </summary>
        public static void DeleteFile(string path)
        {
            if (FileExists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// 复制文件。
        /// </summary>
        public static void CopyFile(string sourcePath, string destPath, bool overwrite = false)
        {
            File.Copy(sourcePath, destPath, overwrite);
        }

        /// <summary>
        /// 替换文件。
        /// </summary>
        public static void ReplaceFile(string sourcePath, string destPath, string backupPath)
        {
            File.Replace(sourcePath, destPath, backupPath);
        }

        /// <summary>
        /// 获取指定目录下的所有文件。
        /// </summary>
        public static string[] GetFiles(string directory, string searchPattern = "*.*")
        {
            return Directory.GetFiles(directory, searchPattern);
        }
    }
}