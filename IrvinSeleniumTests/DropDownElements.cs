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
 
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();

        }

        [Test]
        public void DropDownSelect()
        {
            IWebElement drpDown = driver.FindElement(By.XPath("//select[@class='form-control']"));
            SelectElement s = new SelectElement(drpDown);
            //s.SelectByIndex(0);
            s.SelectByText("Consultant");
            s.SelectByIndex(1);
            //s.SelectByValue("stud");
        }
    }
}
