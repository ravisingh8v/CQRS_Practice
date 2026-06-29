public static class Test
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        // TestClass testClass =  TestClass;
        // testClass.TestMethod();
        // TestClass.TestMethod();
        TestClass testClass = new TestClass.NestedTestClass();
        // testClass.NestedTestMethod();
    }

    public class TestClass
    {
        public  void TestMethod()
        {
            Console.WriteLine("This is a test method.");
        }

        public static class NestedTestClass
        {
            public static void NestedTestMethod()
            {
                Console.WriteLine("This is a nested test method.");
            }
        }
    }
}