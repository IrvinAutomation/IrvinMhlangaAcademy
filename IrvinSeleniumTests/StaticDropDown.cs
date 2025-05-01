using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace IrvinSeleniumTests
{
    public class StaticDropDown
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
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void StaticDropDownTests()
        {
            driver.FindElement(By.XPath("//input[@value='user']")).Click();
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("okayBtn"))));
            driver.FindElement(By.Id("okayBtn")).Click();
           Boolean result = driver.FindElement(By.XPath("//input[@value='user']")).Selected;
           IWebElement dropDown = driver.FindElement(By.XPath("//select[@class='form-control']"));
            SelectElement s = new SelectElement(dropDown);
            //s.SelectByIndex(0);
            s.SelectByText("Consultant");
            s.SelectByValue("stud");

            Assert.That(result, Is.True);   
        }
    }
}
