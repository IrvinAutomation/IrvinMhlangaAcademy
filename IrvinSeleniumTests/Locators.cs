using FluentAssertions.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace IrvinSeleniumTests
{
    public class Locators
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            //Implicit wait is applied globaly
            //explicit wait is applied to a specific element

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();
            driver = new ChromeDriver();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void LocatorsInUse()
        {
           IWebElement userName = driver.FindElement(By.Name("username"));
            userName.SendKeys("rahulshettyacademy");
            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys("12345");
            //ccs selector for the below locator .text-info span:nth-child(1) input
            //xpath of the below locator //div[@class='form-group'][5]/label/span/input
            driver.FindElement(By.CssSelector(".text-info span:nth-child(1) input")).Click();
            driver.FindElement(By.Id("signInBtn")).Click();

            //get the error text message
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.FindElement(By.Id("signInBtn")), "Sign In"));

           String alert = driver.FindElement(By.XPath("//div[@class='alert alert-danger col-md-12']")).Text;
           TestContext.Progress.WriteLine(alert);

            String UrlName = "https://rahulshettyacademy.com/documents-request";

            IWebElement LinkTex = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefLink = LinkTex.GetAttribute("href");

            Assert.That(hrefLink, Is.EqualTo(UrlName));
            

        }

    }
}
