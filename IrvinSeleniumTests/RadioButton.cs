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
    public class RadioButton
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
        public void RadioButtonTests()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IList<IWebElement> radio = driver.FindElements(By.XPath("//input[@type = 'radio']"));

            foreach(IWebElement radioButton in  radio)
            {
                if(radioButton.GetAttribute("value").Equals("user"))
                {
                    radioButton.Click();
                }
            }

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn"))).Click();

            IWebElement dropDown = driver.FindElement(By.XPath("//select[@class='form-control']"));

            SelectElement s = new SelectElement(dropDown);
            s.SelectByIndex(1);

            Boolean isSelected = driver.FindElement(By.CssSelector("input[value='user']")).Selected;

            TestContext.Progress.WriteLine(isSelected);

            Assert.That(isSelected, Is.True);
            

        }
    }
}
