using AngleSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace CSharpSeleniumFrameWork.Utilities
{
    public class Base
    {
        public IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            //Implicit wait is applied globaly
            //explicit wait is applied to a specific element

            //Configuration


            String BrowserConfig = ConfigurationManager.AppSettings["browserName"];

            InitBrowser(BrowserConfig);

            //new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();
            //driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            ;

        }

        public void InitBrowser(String browserName)
        {
            switch(browserName)
            {
                case "FireFox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;

                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                     driver = new ChromeDriver();
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;

                 default:
                    TestContext.Progress.WriteLine("Invalid BrowserName");
                    break;
            }
        }

        public IWebDriver getDriver()
        {
            return driver;
        }

        [TearDown]
        public void quitBrowser()
        {
            driver.Quit();
        }

        public void captureScreenShot()
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();

            string path = Path.Combine(Directory.GetCurrentDirectory(),
            $"screenshot_{DateTime.Now:yyMMdd_HHmmss}.png");


            //string path = Path.Combine(Directory.GetCurrentDirectory(),
            //$"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            screenshot.SaveAsFile(path);
        }
      
    }
}
