using System;


namespace ProFramework
{
    /// <summary>
    /// C#单例模式基类
    /// </summary>
    /// <typeparam name="T">单例类型</typeparam>
    public abstract class ProCSharpSingleton<T> where T : ProCSharpSingleton<T>
    {
        // 使用 Lazy<T> 实现线程安全的单例，延迟加载
        private static readonly Lazy<T> _instance = new Lazy<T>(() =>
        {
            var type = typeof(T);

            if (type.IsAbstract)
            {
                ProLog.LogError($"类型 {type} 是抽象类，无法实例化。");
                throw new InvalidOperationException($"类型 {type} 是抽象类，无法创建实例。");
            }

            // 通过Activator的CreateInstance方法 反射调用构造函数
            // return Activator.CreateInstance(typeof(T), true) as T;
            
            // 通过类型对象的GetConstructor方法 获取指定类型的构造函数信息
            // BindingFlags.Instance | BindingFlags.NonPublic：指定查找实例构造函数且为非公共的（私有、保护、内部等）
            // null：指定不限制绑定上下文
            // Type.EmptyTypes：表示构造函数没有参数类型
            // null：指定不限制调用约定
            var constructorInfo = type.GetConstructor(
                System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public,
                null,
                Type.EmptyTypes,
                null
            );

            if (constructorInfo == null || constructorInfo.IsPublic)
            {
                ProLog.LogError($"类型 {type} 必须包含一个私有或受保护的无参构造函数。");
                throw new InvalidOperationException($"未能获取 {type} 类型的无参构造函数。请检查构造函数的访问修饰符。");
            }

            return (T)constructorInfo.Invoke(null);
        });

        public static T Instance => _instance.Value;

        protected ProCSharpSingleton()
        {
            // 防止外部通过反射重复创建单例对象
            if (_instance.IsValueCreated)
            {
                ProLog.LogError($"单例对象已创建，不能重复创建 {typeof(T)} 的实例。");
                throw new InvalidOperationException("单例对象已存在，不能重复创建实例。");
            }
        }
    }
}