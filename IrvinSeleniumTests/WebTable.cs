using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace IrvinSeleniumTests
{
    public class WebTable
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
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void WebTablesTest()
        {
            ArrayList a = new ArrayList();
            ArrayList b = new ArrayList();
            SelectElement s = new SelectElement(driver.FindElement(By.Id("page-menu")));
            //s.SelectByText("20");
            s.SelectByIndex(2);

            IList<IWebElement> ListOfVeggies = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach (IWebElement veggie in ListOfVeggies)
            {
                a.Add(veggie.Text);
                TestContext.Progress.WriteLine(veggie.Text);
            }
           
            a.Sort();

            TestContext.Progress.WriteLine("AFTER SORTING");

            foreach (String element in a)
            {
                TestContext.Progress.WriteLine(element);
           
            }
          
        }
    }
}
