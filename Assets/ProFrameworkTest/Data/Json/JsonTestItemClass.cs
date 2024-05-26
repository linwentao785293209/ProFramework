namespace ProFrameworkTest
{
    // JsonUtility需要加特性
    [System.Serializable]
    public class JsonTestItemClass
    {
        public int id = 1;
        public int num = 10;

        public JsonTestItemClass()
        {
        }

        public JsonTestItemClass(int id, int num)
        {
            this.id = id;
            this.num = num;
        }
    }
}