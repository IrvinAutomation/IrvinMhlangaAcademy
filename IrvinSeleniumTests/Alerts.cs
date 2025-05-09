using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebDriverManager.DriverConfigs.Impl;

namespace IrvinSeleniumTests
{
    public class Alerts
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver = new ChromeDriver();
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void AlertsTests()
        {
            String name = "Irvin";

            driver.FindElement(By.Name("enter-name")).SendKeys(name);
            driver.FindElement(By.CssSelector("input[value='Confirm']")).Click();
            String AlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            StringAssert.Contains(name, AlertText);
        }

        [Test]
        public void AutoSuggestion()
        {
            IWebElement inputValue = driver.FindElement(By.Id("autocomplete"));
            inputValue.SendKeys("LO");
            Thread.Sleep(4000);
            IList<IWebElement> Options = driver.FindElements(By.CssSelector("li[class='ui-menu-item']"));

            foreach (IWebElement option in Options)
            {
                if (option.Text.Equals("Solomon Islands"))
                {
                    option.Click();
                    
                }
            }
            TestContext.Progress.WriteLine(inputValue.GetAttribute("value"));
        }

        [Test]
        public void ActionTest()
        {
            driver.Url = "https://rahulshettyacademy.com/";
            Actions a = new Actions(driver);
            a.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
            Thread.Sleep(5000);
            driver.FindElement(By.LinkText("Contact")).Click();

            ////ul[@class='dropdown-menu']/li[1]/a

        }

        [Test]

        public void iframe()
        {
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

            //scroll
            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);

            //id, name and index
            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.PartialLinkText("All Access")).Click();
            Thread.Sleep(5000);
            TestContext.Progress.WriteLine( driver.FindElement(By.CssSelector("h1")).Text);
        }

        [Test]
        public void SwitchOutOfIfram()
        {
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

            //scroll
            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);

            //id, name and index
            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.PartialLinkText("All Access")).Click();
            Thread.Sleep(5000);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
        }
    }
}
