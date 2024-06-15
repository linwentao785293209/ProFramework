using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ProFramework
{
    /// <summary>
    /// PlayerPrefs数据管理器，用于将数据保存到PlayerPrefs中并从中加载数据
    /// </summary>
    public class ProPlayerPrefsDataManager : ProAbstractDataManager<ProPlayerPrefsDataManager>
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ProPlayerPrefsDataManager()
        {
        }

        public override void Save<T>(string key, T value)
        {
            Save(key, (object)value);
        }

        public override void Save(string key, object value)
        {
            Type valueType = value.GetType();
            FieldInfo[] fieldInfoArray = valueType.GetFields();

            string tempKey = "";
            FieldInfo tempFieldInfo;

            for (int i = 0; i < fieldInfoArray.Length; i++)
            {
                tempFieldInfo = fieldInfoArray[i];
                tempKey = key + "_" + valueType.Name +
                          "_" + tempFieldInfo.FieldType.Name + "_" + tempFieldInfo.Name;

                SaveField(tempKey, tempFieldInfo.GetValue(value));
            }

            PlayerPrefs.Save();
        }

        private void SaveField(string key, object value)
        {
            Type valueType = value.GetType();

            // 有符号类型
            if (valueType == typeof(sbyte))
            {
                PlayerPrefs.SetInt(key, (sbyte)value);
            }
            else if (valueType == typeof(int))
            {
                PlayerPrefs.SetInt(key, (int)value);
            }
            else if (valueType == typeof(short))
            {
                PlayerPrefs.SetInt(key, (short)value);
            }
            else if (valueType == typeof(long))
            {
                PlayerPrefs.SetString(key, value.ToString());
            }

            // 无符号类型
            else if (valueType == typeof(byte))
            {
                PlayerPrefs.SetInt(key, (byte)value);
            }
            else if (valueType == typeof(uint))
            {
                PlayerPrefs.SetString(key, value.ToString());
            }
            else if (valueType == typeof(ushort))
            {
                PlayerPrefs.SetInt(key, (ushort)value);
            }
            else if (valueType == typeof(ulong))
            {
                PlayerPrefs.SetString(key, value.ToString());
            }

            // 浮点数
            else if (valueType == typeof(float))
            {
                PlayerPrefs.SetFloat(key, (float)value);
            }
            else if (valueType == typeof(double))
            {
                PlayerPrefs.SetString(key, value.ToString());
            }
            else if (valueType == typeof(decimal))
            {
                PlayerPrefs.SetString(key, value.ToString());
            }

            // 特殊类型
            else if (valueType == typeof(bool))
            {
                PlayerPrefs.SetInt(key, (bool)value ? 1 : 0);
            }
            else if (valueType == typeof(char))
            {
                PlayerPrefs.SetString(key, value.ToString());
            }
            else if (valueType == typeof(string))
            {
                PlayerPrefs.SetString(key, value.ToString());
            }

            // 集合类型


            else if (valueType.IsArray)
            {
                Array array = value as Array;
                PlayerPrefs.SetInt(key + "_Length", array.Length); // 保存数组长度


                // 遍历数组，保存每个元素
                for (int i = 0; i < array.Length; i++)
                {
                    SaveField(key + "_" + i, array.GetValue(i));
                }
            }

            else if (typeof(IList).IsAssignableFrom(valueType))
            {
                IList list = value as IList;
                PlayerPrefs.SetInt(key, list.Count);

                int index = 0;
                foreach (object obj in list)
                {
                    SaveField(key + index, obj);
                    ++index;
                }
            }
            else if (typeof(IDictionary).IsAssignableFrom(valueType))
            {
                IDictionary dictionary = value as IDictionary;
                PlayerPrefs.SetInt(key, dictionary.Count);

                int index = 0;
                foreach (object dicKey in dictionary.Keys)
                {
                    SaveField(key + "_key_" + index, dicKey);
                    SaveField(key + "_value_" + index, dictionary[dicKey]);
                    ++index;
                }
            }

            // 自定义类型
            else
            {
                Save(key, value);
            }
        }

        public override T Load<T>(string key)
        {
            return (T)Load(key, typeof(T));
        }

        public override object Load(string key, Type type)
        {
            object value = Activator.CreateInstance(type);
            FieldInfo[] infos = type.GetFields();

            string tempKey = "";
            FieldInfo tempFieldInfo;

            for (int i = 0; i < infos.Length; i++)
            {
                tempFieldInfo = infos[i];
                tempKey = key + "_" + type.Name +
                          "_" + tempFieldInfo.FieldType.Name + "_" + tempFieldInfo.Name;
                tempFieldInfo.SetValue(value, LoadField(tempKey, tempFieldInfo.FieldType));
            }

            return value;
        }

        private object LoadField(string key, Type type)
        {
            // 有符号类型
            if (type == typeof(sbyte))
            {
                return (sbyte)PlayerPrefs.GetInt(key, 0);
            }
            else if (type == typeof(int))
            {
                return PlayerPrefs.GetInt(key, 0);
            }
            else if (type == typeof(short))
            {
                return (short)PlayerPrefs.GetInt(key, 0);
            }
            else if (type == typeof(long))
            {
                return long.Parse(PlayerPrefs.GetString(key, "0"));
            }

            // 无符号类型
            else if (type == typeof(byte))
            {
                return (byte)PlayerPrefs.GetInt(key, 0);
            }
            else if (type == typeof(uint))
            {
                return uint.Parse(PlayerPrefs.GetString(key, "0"));
            }
            else if (type == typeof(ushort))
            {
                return (ushort)PlayerPrefs.GetInt(key, 0);
            }
            else if (type == typeof(ulong))
            {
                return ulong.Parse(PlayerPrefs.GetString(key, "0"));
            }

            // 浮点数
            else if (type == typeof(float))
            {
                return PlayerPrefs.GetFloat(key, 0);
            }
            else if (type == typeof(double))
            {
                return double.Parse(PlayerPrefs.GetString(key, "0"));
            }
            else if (type == typeof(decimal))
            {
                return decimal.Parse(PlayerPrefs.GetString(key, "0"));
            }

            // 特殊类型
            else if (type == typeof(bool))
            {
                return PlayerPrefs.GetInt(key, 0) == 1;
            }
            else if (type == typeof(char))
            {
                return PlayerPrefs.GetString(key, "")[0];
            }
            else if (type == typeof(string))
            {
                return PlayerPrefs.GetString(key, "");
            }

            // 集合类型

            // 先判断是否是数组类型
            else if (type.IsArray)
            {
                int arrayLength = PlayerPrefs.GetInt(key + "_Length", 0); // 加载数组长度


                // 创建数组对象
                Array array = Array.CreateInstance(type.GetElementType(), arrayLength);

                // 遍历数组，加载每个元素
                for (int i = 0; i < arrayLength; i++)
                {
                    array.SetValue(LoadField(key + "_" + i, type.GetElementType()), i);
                }

                return array;
            }

            else if (typeof(IList).IsAssignableFrom(type))
            {
                int listCount = PlayerPrefs.GetInt(key, 0);

                IList list = Activator.CreateInstance(type) as IList;
                for (int i = 0; i < listCount; i++)
                {
                    list.Add(LoadField(key + i, type.GetGenericArguments()[0]));
                }

                return list;
            }
            else if (typeof(IDictionary).IsAssignableFrom(type))
            {
                int dictionaryCount = PlayerPrefs.GetInt(key, 0);

                IDictionary dictionary = Activator.CreateInstance(type) as IDictionary;
                Type[] kvType = type.GetGenericArguments();
                for (int i = 0; i < dictionaryCount; i++)
                {
                    dictionary.Add(LoadField(key + "_key_" + i, kvType[0]),
                        LoadField(key + "_value_" + i, kvType[1]));
                }

                return dictionary;
            }
            else
            {
                return Load(key, type);
            }
        }
    }
}