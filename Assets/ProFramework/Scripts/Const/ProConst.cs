namespace ProFramework
{
    public static class ProConst
    {
        public const string ProFramework = "ProFramework";
        public const string Version = "0.0.1";

        // 对象池
        public const int GameObjectPoolDefaultMaxNum = 1000; // 游戏对象池默认最大数量

        // 常用AB包
        public const string Animations = "animations";
        public const string Animators = "animators";
        public const string Audios = "audios";
        public const string Configs = "configs";
        public const string Effects = "effects";
        public const string Fonts = "fonts";
        public const string Materials = "materials";
        public const string Models = "models";
        public const string Prefabs = "prefabs";
        public const string ScriptableObjects = "scriptableobjects";
        public const string Textures = "textures";
        public const string Ui = "ui";


        public static readonly string EditorPath = $"Assets/Editor";
        public static readonly string EditorAssetBundlePath = $"{EditorPath}/{AssetBundle}/";

        public const string AssetBundle = "AssetBundle";

        private static string AssetBundlePlatform
        {
            get
            {
            #if UNITY_IOS
                return "IOS";
            #elif UNITY_ANDROID
                return "Android";
            #else
                return "PC";
            #endif
            }
        }

        public static readonly string AssetBundlePath = $"{AssetBundle}/{AssetBundlePlatform}/";


        public const string Data = "Data";

        public const string Xml = "Xml";
        public static readonly string XmlDataPath = $"{Data}/{Xml}/";

        public const string Json = "Json";
        public static readonly string JsonDataPath = $"{Data}/{Json}/";

        public const string Binary = "Binary";
        public static readonly string BinaryDataPath = $"/{Data}/{Binary}/";


        public const string Excel = "Excel";
        public const string File = "File";
        public const string Container = "Container";
        public const string DataClass = "DataClass";

        public static readonly string ExcelPath = $"{ProFramework}/{Configs}/{Excel}/";
        public static readonly string ExcelFilePath = $"{ExcelPath}{File}/";
        public static readonly string ExcelContainerPath = $"{ExcelPath}{Container}/";
        public static readonly string ExcelDataClassPath = $"{ExcelPath}{DataClass}/";
    }
}