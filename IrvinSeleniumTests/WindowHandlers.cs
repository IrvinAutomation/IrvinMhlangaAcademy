using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace IrvinSeleniumTests
{
    public class WindowHandlers
    {
        IWebDriver driver;
        [SetUp]
        public void StartUpBrowser()
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
        public void WindowHandle()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String parentwinID = driver.CurrentWindowHandle;

            driver.FindElement(By.CssSelector(".blinkingText")).Click();

            String ChildWindowName = driver.WindowHandles[1];
       
            driver.SwitchTo().Window(ChildWindowName);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);

        }

        [Test]
        public void SwitchBackToParentWindow()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            String email = "mentor@rahulshettyacademy.com";
            String parentwinID = driver.CurrentWindowHandle;

            driver.FindElement(By.CssSelector(".blinkingText")).Click();

            String parentWindow = driver.CurrentWindowHandle.ToString();

            Assert.That(driver.WindowHandles.Count, Is.EqualTo(2));

            String childWindow = driver.WindowHandles[1];

            driver.SwitchTo().Window(childWindow);

           String text =  driver.FindElement(By.CssSelector(".red")).Text;

            String[] splitText = text.Split("at");

            String[] trimmedText = splitText[1].Trim().Split(" ");

            TestContext.Progress.WriteLine(trimmedText[0]);

            Assert.That(trimmedText[0], Is.EqualTo(email));

            driver.SwitchTo().Window(parentWindow);

            driver.FindElement(By.Id("username")).SendKeys(trimmedText[0]);



            

        }

    }

}

