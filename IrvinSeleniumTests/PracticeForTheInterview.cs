using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using NUnit.Framework.Internal.Commands;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace IrvinSeleniumTests
{
    public class PracticeForTheInterview
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            driver = new FirefoxDriver();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void FirstTest()
        {
            TestContext.Progress.WriteLine($"The url I am hitting is {driver.Url}");
        }

        [Test]

        public void TestLocators()
        {
            String Exptect = "https://rahulshettyacademy.com/documents-request";

            IWebElement username = driver.FindElement(By.Id("username"));
            username.SendKeys("IrvinMhlanga");
            username.Clear();
            username.SendKeys("IrvinMhlanga");

            IWebElement password = driver.FindElement(By.XPath("//*[@type='password']"));
            password.SendKeys("12345");

            //click checkbox using xpath
            //driver.FindElement(By.XPath("//div[@class = 'form-group']/label/span/input")).Click();

            //use css 
            driver.FindElement(By.CssSelector(".form-group label span input")).Click();


            driver.FindElement(By.XPath("//input[@id='signInBtn']")).Click();

            Thread.Sleep(3000);

            String error = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine($"The error name is : {error}");

            //check if the url is what you are expecting
            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAtt = link.GetAttribute("href");

            Assert.That(Exptect, Is.EqualTo(hrefAtt));

        }

        [Test]

        public void LoginToTheSite()
        {
            String username = driver.FindElement(By.XPath("//div[@class='form-group']/p/b[1]")).Text;
            String password = driver.FindElement(By.XPath("//div[@class='form-group']/p/b[2]")).Text;

            driver.FindElement(By.Name("username")).SendKeys(username);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("signin")).Click();

        }

        [Test]

        public void TestAlerts()
        {
            String Alert = "Irvin";
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
            driver.FindElement(By.CssSelector("input[placeholder='Enter Your Name']")).SendKeys("Irvin");
            driver.FindElement(By.CssSelector("#confirmbtn")).Click();

            String alertText = driver.SwitchTo().Alert().Text;

            TestContext.Progress.WriteLine(alertText);

            Thread.Sleep(5000);
            driver.SwitchTo().Alert().Accept();

            StringAssert.Contains(Alert, alertText);
        }

        [Test]
        public void autoSuggestedText()
        {
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

            driver.FindElement(By.CssSelector("#autocomplete")).SendKeys("GA");
            Thread.Sleep(5000);

           IList<IWebElement> countryList = driver.FindElements(By.CssSelector(".ui-menu li div"));

            foreach (IWebElement country in countryList)
            {
                if(country.Text.Equals("Portugal"))
                {
                    country.Click();
                }
            }

            //To grab values that are dynamically passed on the webbrowser
            //.Text won't work in this case .GetAttribute("value") will suffice
            String Attvalue = driver.FindElement(By.CssSelector("#autocomplete")).GetAttribute("value");

            TestContext.Progress.WriteLine(Attvalue);
        }

        [Test]

        public void test_Actions()
        {
            driver.Url = "https://rahulshettyacademy.com/#/index";

            //driver.FindElement(By.XPath("//button[@id='radix-:r2:']")).Click();

      

            //driver.FindElement(By.CssSelector("div[dir='ltr'] a:nth-child(3)")).Click();

            Actions a = new Actions(driver);
            a.MoveToElement(driver.FindElement(By.XPath("//div/button[@id='radix-:r2:']"))).Perform();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div/button[@id='radix-:r2:']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("div[dir='ltr'] a:nth-child(3)")).Click();


           String parent = driver.WindowHandles[0];
            String child = driver.WindowHandles[1];

            driver.SwitchTo().Window(child);
            Thread.Sleep(5000);
            driver.Close();
            driver.SwitchTo().Window(parent);
            //a.MoveToElement(driver.FindElement(By.XPath("//a[normalize-space()='QA Jobs']//*[name()='svg']"))).Click().Perform();

        }

        [Test]
        public void Frames()
        {
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

            IWebElement scr= driver.FindElement(By.Id("courses-iframe"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript("arguments[0].scrollIntoView(true);", scr);

     
            driver.SwitchTo().Frame("courses-iframe");

            Thread.Sleep(3000);
            driver.FindElement(By.PartialLinkText("All Acces")).Click();

            driver.SwitchTo().DefaultContent();


            driver.FindElement(By.CssSelector("#mousehover")).Click();

        }

        [Test]

        public void WindowHandle()
        {
            driver.FindElement(By.XPath("//div[@class= 'float-right']/a[1]")).Click();

            String parentWindow = driver.WindowHandles[0];

            String childWindow = driver.WindowHandles[1];

            driver.SwitchTo().Window(childWindow);

            Thread.Sleep(5000);

           String pageTitle = driver.FindElement(By.CssSelector(".red")).Text;

            TestContext.Progress.WriteLine(pageTitle);

            //Please email us at mentor@rahulshettyacademy.com with below template to receive response

            String[] splitOne = pageTitle.Split("at");

            TestContext.Progress.WriteLine($"First Split is : {splitOne}");

           String[] splitTwo = splitOne[1].Trim().Split(" ");

            TestContext.Progress.WriteLine($"Second Split is: {splitTwo[0]}");

            driver.SwitchTo().Window(parentWindow);

        }

        public void CloseBrowser()
        {
            //driver.Close();
        }
    }
}
