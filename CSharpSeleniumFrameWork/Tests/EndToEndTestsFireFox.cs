using AngleSharp.Text;
using CSharpSeleniumFrameWork.PageObjects;
using CSharpSeleniumFrameWork.Utilities;
using FluentAssertions;
using FluentAssertions.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace CSharpSeleniumFrameWork.Tests
{
    public class EndToEndTestsFireFox : Base
    {

        //[Test,TestCaseSource("addTestDataFile")]
        //parameterize your tests using [TestCaseSource()]
        //[TestCaseSource("addTestDataFile")]
        //Parameterize your your test using [TestCase()]
        [TestCase("rahulshettyacademy", "Learning@830$3mK2")]
        [TestCase("rahulshettyac", "Learning@830$3mK2")]

        public void loginToCheckOutTest(String username, String password)
        {
            String[] Expectedproducts = { "iphone X", "Nokia Edge", "Blackberry" };
            String phoneT = "₹. 215000";

            LoginPage loginpage = new LoginPage(getDriver());

            ProductsPage productsPage = loginpage.validLogin(username, password);

            loginpage.selectOption();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("okayBtn"))));
            driver.FindElement(By.Id("okayBtn")).Click();


            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Id("okayBtn")));

            Boolean result = driver.FindElement(By.XPath("//input[@value='user']")).Selected;

            loginpage.getSigninButton().Click();


            /*
            IList<IWebElement> radioButtons = driver.FindElements(By.CssSelector("input[type='radio']"));
            foreach (IWebElement radioButtonElements in radioButtons)
            {
                if (radioButtonElements.GetAttribute("value").Equals("user"))
                {
                    radioButtonElements.Click();
                }
            }
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("okayBtn"))));
            driver.FindElement(By.Id("okayBtn")).Click();


            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Id("okayBtn")));

            

            loginpage.selectDropDown();
            //s.SelectByValue("stud");
            //s.SelectByIndex(1);

            //loginpage.getSigninButton().Click();
            */

            productsPage.waitForPage();

            IList<IWebElement> products = productsPage.getCards();

            foreach (IWebElement prodItem in products)
            {

                if (Expectedproducts.Contains(prodItem.FindElement(productsPage.getCardTitle()).Text))
                {
                    //click on add cart
                    prodItem.FindElement(productsPage.getfooterButton()).Click();
                }

                TestContext.Progress.WriteLine(prodItem.FindElement(productsPage.getCardTitle()).Text);

            }

            CheckoutPagecs checkOutPage = productsPage.getCheckOut();


            String PhoneTotals = getDriver().FindElement(By.CssSelector(".text-right h3 strong")).Text;

            DateTime currentTime = DateTime.Now;
            String timeNow = currentTime.ToString("yyy-MM-dd_HH-mm-ss");
            TestContext.Progress.WriteLine($"{PhoneTotals} -- and the currentTime is :{timeNow}");

            checkOutPage.getCheckOutButon();

            IJavaScriptExecutor js = (IJavaScriptExecutor)getDriver();

            //js.ExecuteScript("arguments[0].scrollIntoView(true);", checkOutPage);


   
            checkOutPage.getCheckOutButon().Click();

            // using (new AssertionScope())
            {
               // Assert.AreEqual(PhoneTotals, phoneT);
                //StringAssert.Contains(PhoneTotals, phoneT);

            }

            using (new AssertionScope())
            {
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(PhoneTotals, phoneT);
                    StringAssert.Contains(PhoneTotals, phoneT);
                    Assert.That(result, Is.EqualTo(true));
                });

            }
            
        }

        public static IEnumerable<TestCaseData> addTestDataFile()
        {
            yield return new TestCaseData("rahulshettyacademy", "Learning@830$3mK2");
            yield return new TestCaseData("rahulshettyacademy1", "Learning@830$3mK2");
            yield return new TestCaseData("", "");
            //yield return new TestCaseData("rahulshettyacademy3", "Learning@830$3mK2");
            //yield return new TestCaseData("rahulshettyacademy4", "Learning@830$3mK2");
        }

    }
}
