﻿using Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ProFramework
{
    public class ProConfigManager : ProCSharpSingleton<ProConfigManager>, IStreamingAssetsPath
    {
        public string StreamingAssetsPath => $"{Application.streamingAssetsPath}/{ProConst.Config}/";


        /// <summary>
        /// 用于存储所有Excel表数据的容器
        /// </summary>
        private Dictionary<string, object> _excelTableDictionary = new Dictionary<string, object>();

        private ProConfigManager()
        {
        }

        /// <summary>
        /// 加载Excel表的2进制数据到内存中 
        /// </summary>
        /// <typeparam name="T">容器类名</typeparam>
        /// <typeparam name="K">数据结构类类名</typeparam>
        public void LoadExcelTable<T, K>()
        {
            //读取 对应路径下 excel表对应的2进制文件 来进行解析
            using (FileStream fileStream = File.Open(StreamingAssetsPath + typeof(K).Name + ".tao", FileMode.Open,
                       FileAccess.Read))
            {
                //把所有的文件流数据存到直接数组综合
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();

                //标识 用于记录当前读取了多少字节了
                int index = 0;

                //读取一个要读多少行数据
                int count = BitConverter.ToInt32(bytes, index);
                //读了一个int 序号标识要加四个字节
                index += 4;

                //读取主键的名字
                //想读取主键的长度
                int keyNameLength = BitConverter.ToInt32(bytes, index);
                //读了一个int 序号标识要加四个字节
                index += 4;

                //根据主键的长度在读取主键名字
                string keyName = Encoding.UTF8.GetString(bytes, index, keyNameLength);
                //序号标识要加上主键的长度
                index += keyNameLength;

                //创建容器类对象
                //得到容器类的Type
                Type contaninerType = typeof(T);
                //用反射方法根据类创建容器对象
                object contaninerObj = Activator.CreateInstance(contaninerType);

                //得到数据结构类的Type
                Type classType = typeof(K);
                //通过反射 得到数据结构类 所有字段的信息
                FieldInfo[] infos = classType.GetFields();

                //读取每一行的信息
                for (int i = 0; i < count; i++)
                {
                    //实例化一个数据结构类 对象
                    object dataObj = Activator.CreateInstance(classType);

                    //遍历所有字段信息 
                    foreach (FieldInfo info in infos)
                    {
                        if (info.FieldType == typeof(int))
                        {
                            //相当于就是把2进制数据转为int 然后赋值给了实例化一个数据结构类的对象的对应的字段
                            info.SetValue(dataObj, BitConverter.ToInt32(bytes, index));
                            index += 4;
                        }
                        else if (info.FieldType == typeof(float))
                        {
                            info.SetValue(dataObj, BitConverter.ToSingle(bytes, index));
                            index += 4;
                        }
                        else if (info.FieldType == typeof(bool))
                        {
                            info.SetValue(dataObj, BitConverter.ToBoolean(bytes, index));
                            index += 1;
                        }
                        else if (info.FieldType == typeof(string))
                        {
                            //读取字符串字节数组的长度
                            int length = BitConverter.ToInt32(bytes, index);
                            index += 4;
                            //根据字符串长度在读字符串
                            info.SetValue(dataObj, Encoding.UTF8.GetString(bytes, index, length));
                            index += length;
                        }
                    }

                    //读取完一行的数据了 应该把这个数据添加到容器对象中

                    //得到容器对象中的 字典对象这个字段
                    object dicObject = contaninerType.GetField("dataClassDictionary").GetValue(contaninerObj);
                    //通过字典对象得到其中的 Add方法
                    MethodInfo mInfo = dicObject.GetType().GetMethod("Add");
                    //得到数据结构类对象中 指定主键字段的值 先得到数据结构类的 在得到数据结构类的主键 在得到主键的值
                    object keyValue = classType.GetField(keyName).GetValue(dataObj);
                    //调用Add方法 传入字典对象 和字典的key还有具体要存储的一条数据对象
                    mInfo.Invoke(dicObject, new object[] { keyValue, dataObj });
                }

                //把读取完的表记录下来 传入容器名作为Key 容器对象做为值
                _excelTableDictionary.Add(typeof(T).Name, contaninerObj);

                fileStream.Close();
            }
        }

        /// <summary>
        /// 得到一张表的信息
        /// </summary>
        /// <typeparam name="T">容器类名</typeparam>
        /// <returns>返回指定类型的表信息</returns>
        public T GetExcelTable<T>() where T : class
        {
            // 获取指定类型的表名
            string tableName = typeof(T).Name;

            // 检查表字典中是否包含指定表名的项
            if (_excelTableDictionary.ContainsKey(tableName))
                // 如果包含，则将该项转换为指定类型，并返回
                return _excelTableDictionary[tableName] as T;

            // 如果不包含，则返回null
            return null;
        }
    }
}