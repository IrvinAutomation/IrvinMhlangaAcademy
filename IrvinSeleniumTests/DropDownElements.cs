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
    public class DropDownElements
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
             driver = new ChromeDriver();
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();

        }

        [Test]
        public void DropDownSelect()
        {
            IWebElement dropdown = driver.FindElement(By.CssSelector(".form-group select"));

            SelectElement s = new SelectElement(dropdown);

            //s.SelectByIndex(2);
            //s.SelectByValue("teach");
            s.SelectByText("Consultant");
        }

    }
}
