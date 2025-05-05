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
        public void TestFlowOne()
        {
            TestContext.Progress.WriteLine("This is test1");
            Assert.Pass();
        }

        [Test]
        public void TestFlowTwo()
        {
            TestContext.Progress.WriteLine("This is test2");
            Assert.Pass();
        }
    }
}