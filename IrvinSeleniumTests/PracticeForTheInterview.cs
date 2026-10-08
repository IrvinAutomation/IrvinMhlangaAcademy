using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace IrvinSeleniumTests
{
    public class PracticeForTheInterview
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            driver = new FirefoxDriver();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void FirstTest()
        {
            TestContext.Progress.WriteLine($"The url I am hitting is {driver.Url}");
        }

        [Test]

        public void TestLocators()
        {
            String Exptect = "https://rahulshettyacademy.com/documents-request";

            IWebElement username = driver.FindElement(By.Id("username"));
            username.SendKeys("IrvinMhlanga");
            username.Clear();
            username.SendKeys("IrvinMhlanga");

            IWebElement password = driver.FindElement(By.CssSelector("input[type='password']"));
            password.SendKeys("12345");

            //click checkbox using xpath
            //driver.FindElement(By.XPath("//div[@class = 'form-group']/label/span/input")).Click();

            //use css 
            driver.FindElement(By.CssSelector(".form-group label span input")).Click();


            driver.FindElement(By.XPath("//input[@id='signInBtn']")).Click();

            Thread.Sleep(3000);

            String error = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine($"The error name is : {error}");

            //check if the url is what you are expecting
            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAtt = link.GetAttribute("href");

            Assert.That(Exptect, Is.EqualTo(hrefAtt));

        }

        [Test]

        public void LoginToTheSite()
        {
            String username = driver.FindElement(By.XPath("//div[@class='form-group']/p/b[1]")).Text;
            String password = driver.FindElement(By.XPath("//div[@class='form-group']/p/b[2]")).Text;

            driver.FindElement(By.Name("username")).SendKeys(username);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("signin")).Click();

        }

        [TearDown]
        public void CloseBrowser()
        {
            //driver.Close();
        }
    }
}
