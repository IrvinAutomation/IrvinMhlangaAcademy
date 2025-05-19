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
    public class EndToEndTests
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
        public void getListOfProducts()
        {
            String[] Expectedproducts = { "iphone X", "Samsung Note 8", "Blackberry" };

            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("learning");

            IList<IWebElement> radioButtons = driver.FindElements(By.CssSelector("input[type='radio']"));
            foreach (IWebElement radioButtonElements in radioButtons)
            {
                if (radioButtonElements.GetAttribute("value").Equals("user"))
                {
                    radioButtonElements.Click();
                }
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("okayBtn"))));
            driver.FindElement(By.Id("okayBtn")).Click();

            Boolean result = driver.FindElement(By.XPath("//input[@value='user']")).Selected;

            IWebElement dropDown = driver.FindElement(By.XPath("//select[@class='form-control']"));

            SelectElement s = new SelectElement(dropDown);
            s.SelectByText("Consultant");
            s.SelectByValue("stud");

            driver.FindElement(By.Id("signInBtn")).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button.btn")));

            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));

            foreach (IWebElement prodItem in products)
            {

                if (Expectedproducts.Contains(prodItem.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    //click on add cart
                    prodItem.FindElement(By.CssSelector(".card-footer button")).Click();
                }

             TestContext.Progress.WriteLine(prodItem.FindElement(By.CssSelector(".card-title a")).Text);
          
            }
            driver.FindElement(By.PartialLinkText("Checkout")).Click();
        }

    }
}
