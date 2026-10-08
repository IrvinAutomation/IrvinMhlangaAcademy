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
    public class Waits
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowSer()
        {
            //implicit wait is applied globally
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            //Delcare the implicit wait that is applied Globally
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Url = driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();
        }

        [Test]

        public void WaitsMechanisms()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            IWebElement username = driver.FindElement(By.Id("username"));
            username.SendKeys("IrvinMhlanga");
            username.Clear();
            username.SendKeys("IrvinMhlanga");

            IWebElement password = driver.FindElement(By.CssSelector("input[type='password']"));
            password.SendKeys("12345");

            //use xpath to locate the same input
            //driver.FindElement(By.XPath("//div[@class = 'form-group']/label/span/input")).Click();

            //use css to locate the same input
            driver.FindElement(By.CssSelector(".form-group label span input")).Click();


            driver.FindElement(By.XPath("//input[@id='signInBtn']")).Click();

            //Thread.Sleep(3000);

    
            IWebElement Err = driver.FindElement(By.ClassName("alert-danger"));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(Err, "Incorrect username/password."));

            String error = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine($"The error name is : {error}");
        }
    }
}
