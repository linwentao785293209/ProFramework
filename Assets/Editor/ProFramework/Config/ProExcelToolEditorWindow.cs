using UnityEditor;
using UnityEngine;
using System.IO;

namespace ProFramework
{
    public class ProExcelToolEditorWindow : EditorWindow
    {
        private string _excelPath = "";
        private string _excelDataClassPath = "";
        private string _excelDataContainerPath = "";

        private const string ExcelPathKey = "ProExcelTool_ExcelPath";
        private const string ExcelDataClassPathKey = "ProExcelTool_ExcelDataClassPath";
        private const string ExcelDataContainerPathKey = "ProExcelTool_ExcelDataContainerPath";

        [MenuItem("ProFramework/Excel自动生成 数据结构类 容器类 二进制配置 工具")]
        public static void ShowWindow()
        {
            ProExcelToolEditorWindow win = EditorWindow.GetWindow<ProExcelToolEditorWindow>();
            win.titleContent = new GUIContent("Excel自动生成 数据结构类 容器类 二进制配置 工具");
            win.Show();
        }

        private void OnEnable()
        {
            // 加载保存的路径
            _excelPath = EditorPrefs.GetString(ExcelPathKey, "");
            _excelDataClassPath = EditorPrefs.GetString(ExcelDataClassPathKey, "");
            _excelDataContainerPath = EditorPrefs.GetString(ExcelDataContainerPathKey, "");
        }

        private void OnDisable()
        {
            // 保存路径
            EditorPrefs.SetString(ExcelPathKey, _excelPath);
            EditorPrefs.SetString(ExcelDataClassPathKey, _excelDataClassPath);
            EditorPrefs.SetString(ExcelDataContainerPathKey, _excelDataContainerPath);
        }

        private void OnGUI()
        {
            // 创建一个GUIStyle并设置其对齐方式为居中
            GUIStyle centeredStyle = new GUIStyle(GUI.skin.label);
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontSize = 16; // 可选：更改字体大小

            // 居中显示标题
            GUILayout.Label("Excel自动生成 数据结构类 容器类 二进制配置 工具", centeredStyle);
            EditorGUILayout.Space(30);

            _excelPath = EditorGUILayout.TextField("Excel文件路径", _excelPath);
            if (GUILayout.Button("选择Excel文件夹"))
            {
                string selectedPath = EditorUtility.OpenFolderPanel("选择Excel文件夹", _excelPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    _excelPath = selectedPath;
                }
            }

            EditorGUILayout.Space(15);

            _excelDataClassPath = EditorGUILayout.TextField("数据类路径", _excelDataClassPath);
            if (GUILayout.Button("选择数据类文件夹"))
            {
                string selectedPath = EditorUtility.OpenFolderPanel("选择数据类文件夹", _excelDataClassPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    _excelDataClassPath = selectedPath;
                }
            }

            EditorGUILayout.Space(15);

            _excelDataContainerPath = EditorGUILayout.TextField("容器类路径", _excelDataContainerPath);
            if (GUILayout.Button("选择容器类文件夹"))
            {
                string selectedPath = EditorUtility.OpenFolderPanel("选择容器类文件夹", _excelDataContainerPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    _excelDataContainerPath = selectedPath;
                }
            }

            EditorGUILayout.Space(15);

            if (GUILayout.Button("生成Excel数据结构类和容器类"))
            {
                if (string.IsNullOrEmpty(_excelPath) || string.IsNullOrEmpty(_excelDataClassPath) ||
                    string.IsNullOrEmpty(_excelDataContainerPath))
                {
                    EditorUtility.DisplayDialog("错误", "请先选择所有路径后再生成。", "确定");
                    return;
                }

                ProExcelTool.GenerateExcelClassAndContainer(_excelPath, _excelDataClassPath, _excelDataContainerPath);
            }
        }
    }
}