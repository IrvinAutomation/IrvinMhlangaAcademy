namespace IrvinSeleniumTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TestContext.Progress.WriteLine("This is a Setup Method execution");
        }

        [Test]
        public void Test1()
        {
            TestContext.Progress.WriteLine("This is test1");
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            TestContext.Progress.WriteLine("This is test2");
            Assert.Pass();
        }
    }
}