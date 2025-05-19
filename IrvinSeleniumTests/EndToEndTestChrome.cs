using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace IrvinSeleniumTests
{
    public class EndToEndTestChrome
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
            driver = new ChromeDriver();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void getListOfProducts()
        {
            String[] Expectedproducts = { "iphone X", "Nokia Edge", "Blackberry" };
            String phoneT = "₹. 215000";

            driver.FindElement(By.Name("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("learning");
            
            IList<IWebElement> radioButtons = driver.FindElements(By.CssSelector("input[name='radio']"));
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

            Thread.Sleep(10000);

            IWebElement total = driver.FindElement(By.CssSelector(".text-right h3 strong"));

            String PhoneTotals = total.Text;

            TestContext.Progress.WriteLine(PhoneTotals);

            Assert.That(PhoneTotals, Is.EqualTo(phoneT));
        }

    }
}
