namespace ProFramework
{
    public static class ProConst
    {
        public const string ProFramework = "ProFramework";
        public const string Version = "0.0.1";

        public const int DefaultMaxNum = 1000; // 默认最大数量


        public const string Animations = "animations";
        public const string Audios = "audios";
        public const string Configs = "configs";
        public const string Effects = "effects";
        public const string Fonts = "fonts";
        public const string Materials = "materials";
        public const string Models = "models";
        public const string ScriptableObjects = "scriptableobjects";
        public const string Textures = "textures";
        public const string Ui = "ui";


        public static readonly string ResourcePath = $"{ProFramework}/";

        public static readonly string EditorPath = $"Assets/Editor";
        public static readonly string EditorResourcePath = $"{EditorPath}/{ProFramework}/";

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

        public static readonly string AssetBundlePath =
            $"{ProFramework}/{AssetBundle}/{AssetBundlePlatform}/";
        
        
        public const string Data = "Data";
        
        public const string Xml = "Xml";
        
        public static readonly string XmlDataPath =
            $"{ProFramework}/{Data}/{Xml}/";
        
        
        public const string Json = "Json";
        
        public static readonly string JsonDataPath =
            $"{ProFramework}/{Data}/{Json}/";
        
    }
}