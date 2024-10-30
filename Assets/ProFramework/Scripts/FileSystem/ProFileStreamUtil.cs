using System;
using System.IO;

namespace ProFramework
{
    /// <summary>
    /// 提供文件流操作的实用方法。
    /// </summary>
    public static class ProFileStreamUtil
    {
        /// <summary>
        /// 打开文件流。
        /// </summary>
        public static FileStream OpenFile(string path, FileMode mode, FileAccess access)
        {
            return new FileStream(path, mode, access);
        }

        /// <summary>
        /// 写入字节到文件流。
        /// </summary>
        public static void WriteBytes(FileStream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        /// <summary>
        /// 写入字符串到文件流。
        /// </summary>
        public static void WriteString(FileStream stream, string text)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
            WriteBytes(stream, BitConverter.GetBytes(bytes.Length)); // 先写入长度
            WriteBytes(stream, bytes); // 再写入字符串内容
        }

        /// <summary>
        /// 读取字节。
        /// </summary>
        public static byte[] ReadBytes(FileStream stream, int count)
        {
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            return bytes;
        }

        /// <summary>
        /// 读取字符串。
        /// </summary>
        public static string ReadString(FileStream stream)
        {
            byte[] lengthBytes = ReadBytes(stream, 4);
            int length = BitConverter.ToInt32(lengthBytes, 0);
            byte[] stringBytes = ReadBytes(stream, length);
            return System.Text.Encoding.UTF8.GetString(stringBytes);
        }

        /// <summary>
        /// 关闭文件流。
        /// </summary>
        public static void CloseFile(FileStream stream)
        {
            stream.Close();
        }
    }
}