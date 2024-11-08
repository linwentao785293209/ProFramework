using Excel;
using System;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ProFramework
{
    public static class ProExcelTool
    {
        /// <summary>
        /// 配置在流文件夹的路径
        /// </summary>
        public static string StreamingAssetsPath => $"{Application.streamingAssetsPath}/{ProConst.Config}";

        /// <summary>
        /// 真正内容开始的行号
        /// </summary>
        public static int BEGIN_INDEX = 4;

        public static void GenerateExcelClassAndContainer(string excelPath, string excelDataClassPath,
            string excelDataContainerPath)
        {
            //加载指定路径中的所有Excel文件 用于生成对应的3个文件
            DirectoryInfo directoryInfo = Directory.CreateDirectory(excelPath);

            //得到指定路径中的所有文件信息 相当于就是得到所有的Excel表
            FileInfo[] fileInfoArray = directoryInfo.GetFiles();

            //数据表容器集合
            DataTableCollection tempDataTableCollection;

            for (int i = 0; i < fileInfoArray.Length; i++)
            {
                //如果不是excel文件就不要处理了
                if (fileInfoArray[i].Extension != ".xlsx" &&
                    fileInfoArray[i].Extension != ".xls")
                    continue;

                //打开一个Excel文件得到其中的所有表的数据
                using (FileStream fileStream = fileInfoArray[i].Open(FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                    tempDataTableCollection = excelDataReader.AsDataSet().Tables;
                    fileStream.Close();
                }

                //遍历文件中的所有表的信息
                foreach (DataTable dataTable in tempDataTableCollection)
                {
                    //生成数据结构类
                    GenerateExcelDataClass(dataTable, excelDataClassPath);
                    //生成容器类
                    GenerateExcelContainer(dataTable, excelDataContainerPath);
                    //生成2进制数据
                    GenerateExcelBinary(dataTable);
                }
            }
        }

        /// <summary>
        /// 生成Excel表对应的数据结构类
        /// </summary>
        /// <param name="dataTable"></param>
        private static void GenerateExcelDataClass(DataTable dataTable, string excelDataClassPath)
        {
            //字段名行
            DataRow rowName = GetVariableNameRow(dataTable);

            //字段类型行
            DataRow rowType = GetVariableTypeRow(dataTable);

            //判断路径是否存在 没有的话 就创建文件夹
            if (!Directory.Exists(excelDataClassPath))
                Directory.CreateDirectory(excelDataClassPath);

            //如果我们要生成对应的数据结构类脚本 其实就是通过代码进行字符串拼接 然后存进文件就行了
            string str = "public class " + dataTable.TableName + "\n{\n";

            //变量进行字符串拼接
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                str += "    public " + rowType[i].ToString() + " " + rowName[i].ToString() + ";\n";
            }

            str += "}";

            //把拼接好的字符串存到指定文件中去
            File.WriteAllText(excelDataClassPath + "/" + dataTable.TableName + ".cs", str);

            //刷新Project窗口
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 生成Excel表对应的数据容器类
        /// </summary>
        /// <param name="dataTable"></param>
        private static void GenerateExcelContainer(DataTable dataTable, string excelDataContainerPath)
        {
            //得到主键索引
            int keyIndex = GetKeyIndex(dataTable);

            //得到字段类型行
            DataRow rowType = GetVariableTypeRow(dataTable);

            //没有路径创建路径
            if (!Directory.Exists(excelDataContainerPath))
                Directory.CreateDirectory(excelDataContainerPath);

            string str = "using System.Collections.Generic;\n";

            str += "public class " + dataTable.TableName + "Container" + "\n{\n";

            str += "    ";
            str += "public Dictionary<" + rowType[keyIndex].ToString() + ", " + dataTable.TableName + ">";
            str += " dataClassDictionary = new " + "Dictionary<" + rowType[keyIndex].ToString() + ", " +
                   dataTable.TableName +
                   ">();\n";

            str += "}";

            File.WriteAllText(excelDataContainerPath + "/" + dataTable.TableName + "Container.cs", str);

            //刷新Project窗口
            AssetDatabase.Refresh();
        }


        /// <summary>
        /// 生成excel2进制数据
        /// </summary>
        /// <param name="dataTable"></param>
        private static void GenerateExcelBinary(DataTable dataTable)
        {
            //没有路径创建路径
            if (!Directory.Exists(StreamingAssetsPath))
                Directory.CreateDirectory(StreamingAssetsPath);

            //创建一个2进制文件进行写入
            using (FileStream fileStream = new FileStream(StreamingAssetsPath + dataTable.TableName + ".tao",
                       FileMode.OpenOrCreate, FileAccess.Write))
            {
                //存储具体的excel对应的2进制信息

                //1.先要存储我们需要写多少行的数据 方便我们读取
                //-4的原因是因为 前面4行是配置规则 并不是我们需要记录的数据内容
                fileStream.Write(BitConverter.GetBytes(dataTable.Rows.Count - 4), 0, 4);

                //2.存储主键的变量名
                string keyName = GetVariableNameRow(dataTable)[GetKeyIndex(dataTable)].ToString();
                byte[] bytes = Encoding.UTF8.GetBytes(keyName);
                //存储字符串字节数组的长度
                fileStream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
                //存储字符串字节数组
                fileStream.Write(bytes, 0, bytes.Length);

                //遍历所有内容的行 进行2进制的写入
                DataRow row;
                //得到类型行 根据类型来决定应该如何写入数据
                DataRow rowType = GetVariableTypeRow(dataTable);
                for (int i = BEGIN_INDEX; i < dataTable.Rows.Count; i++)
                {
                    //得到一行的数据
                    row = dataTable.Rows[i];
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        switch (rowType[j].ToString())
                        {
                            case "int":
                                fileStream.Write(BitConverter.GetBytes(int.Parse(row[j].ToString())), 0, 4);
                                break;
                            case "float":
                                fileStream.Write(BitConverter.GetBytes(float.Parse(row[j].ToString())), 0, 4);
                                break;
                            case "bool":
                                fileStream.Write(BitConverter.GetBytes(bool.Parse(row[j].ToString())), 0, 1);
                                break;
                            case "string":
                                bytes = Encoding.UTF8.GetBytes(row[j].ToString());
                                //写入字符串字节数组的长度
                                fileStream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
                                //写入字符串字节数组
                                fileStream.Write(bytes, 0, bytes.Length);
                                break;
                        }
                    }
                }

                fileStream.Close();
            }

            AssetDatabase.Refresh();
        }


        /// <summary>
        /// 获取变量名所在行
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataRow GetVariableNameRow(DataTable table)
        {
            return table.Rows[0];
        }

        /// <summary>
        /// 获取变量类型所在行
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataRow GetVariableTypeRow(DataTable table)
        {
            return table.Rows[1];
        }


        /// <summary>
        /// 获取主键索引
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int GetKeyIndex(DataTable table)
        {
            DataRow row = table.Rows[2];
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (row[i].ToString() == "key")
                    return i;
            }

            return 0;
        }
    }
}