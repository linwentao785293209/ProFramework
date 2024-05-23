namespace ProFramework
{
    /// <summary>
    /// 系统对象接口 想要被复用的 数据结构类、逻辑类 都必须要继承该接口
    /// </summary>
    public interface IProSystemObject
    {
        /// <summary>
        /// 重置数据的方法
        /// </summary>
        void ResetInfo();
    }
}