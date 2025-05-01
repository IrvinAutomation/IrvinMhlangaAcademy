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

            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            //driver = new FirefoxDriver();
            //IWebDriver driver = new EdgeDriver();
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void FirstTest()
        { 
            TestContext.Progress.WriteLine("The page Title is : "+driver.PageSource);
           TestContext.Progress.WriteLine("The URL of the page is :" +driver.Url);
        }

        [TearDown]
        public void CloseBrowser()
        {
            //driver.Close();
        }
    }
}
