using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using static System.Net.Mime.MediaTypeNames;

namespace IrvinSeleniumTests
{
    public class SeleniumFirst
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            IWebDriver driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void TestOne()
        {
            TestContext.Progress.WriteLine("LOGIN PAGE TITLE IS :" +driver.Title);
            TestContext.Progress.WriteLine("THE LOGIN PAGE URL IS :" +driver.Url);
           TestContext.Progress.WriteLine("THE PAGE SOURCE IS :" +driver.PageSource);
        }
    }
}
