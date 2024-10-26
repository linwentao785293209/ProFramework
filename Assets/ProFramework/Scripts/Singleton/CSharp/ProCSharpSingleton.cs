using System;


namespace ProFramework
{
    /// <summary>
    /// C#����ģʽ����
    /// </summary>
    /// <typeparam name="T">��������</typeparam>
    public abstract class ProCSharpSingleton<T> where T : ProCSharpSingleton<T>
    {
        // ʹ�� Lazy<T> ʵ���̰߳�ȫ�ĵ������ӳټ���
        private static readonly Lazy<T> _instance = new Lazy<T>(() =>
        {
            var type = typeof(T);

            if (type.IsAbstract)
            {
                ProLog.LogError($"���� {type} �ǳ����࣬�޷�ʵ������");
                throw new InvalidOperationException($"���� {type} �ǳ����࣬�޷�����ʵ����");
            }

            // ͨ��Activator��CreateInstance���� ������ù��캯��
            // return Activator.CreateInstance(typeof(T), true) as T;
            
            // ͨ�����Ͷ����GetConstructor���� ��ȡָ�����͵Ĺ��캯����Ϣ
            // BindingFlags.Instance | BindingFlags.NonPublic��ָ������ʵ�����캯����Ϊ�ǹ����ģ�˽�С��������ڲ��ȣ�
            // null��ָ�������ư�������
            // Type.EmptyTypes����ʾ���캯��û�в�������
            // null��ָ�������Ƶ���Լ��
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
                ProLog.LogError($"���� {type} �������һ��˽�л��ܱ������޲ι��캯����");
                throw new InvalidOperationException($"δ�ܻ�ȡ {type} ���͵��޲ι��캯�������鹹�캯���ķ������η���");
            }

            return (T)constructorInfo.Invoke(null);
        });

        public static T Instance => _instance.Value;

        protected ProCSharpSingleton()
        {
            // ��ֹ�ⲿͨ�������ظ�������������
            if (_instance.IsValueCreated)
            {
                ProLog.LogError($"���������Ѵ����������ظ����� {typeof(T)} ��ʵ����");
                throw new InvalidOperationException("���������Ѵ��ڣ������ظ�����ʵ����");
            }
        }
    }
}