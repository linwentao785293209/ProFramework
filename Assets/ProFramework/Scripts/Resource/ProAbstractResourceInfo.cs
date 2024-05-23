namespace ProFramework
{
    /// <summary>
    /// 资源信息基类 主要用于里式替换原则 父类容器装子类对象
    /// </summary>
    public abstract class ProAbstractResourceInfo
    {
        //引用计数
        public int refCount;
    }
}