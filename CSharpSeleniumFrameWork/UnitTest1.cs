using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using CSharpSeleniumFrameWork.Utilities;
using System.Collections;
namespace CSharpSeleniumFrameWork
{
    public class Tests : Base
    {
 
        [Test]
        public void LocatorsInUse()
        {
            IWebElement userName = driver.FindElement(By.Name("username"));
            userName.SendKeys("rahulshettyacademy");
            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys("12345");
            driver.FindElement(By.XPath("//input[@id='terms']")).Click();
            driver.FindElement(By.Id("signInBtn")).Click();

            //get the error text message
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.FindElement(By.Id("signInBtn")), "Sign In"));

            String alert = driver.FindElement(By.XPath("//div[@class='alert alert-danger col-md-12']")).Text;
            TestContext.Progress.WriteLine(alert);

            IWebElement LinkTex = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefLink = LinkTex.GetAttribute("href");


            Assert.That(hrefLink, Is.EqualTo("https://rahulshettyacademy.com/documents-request"));

        }

        [Test]
        public void SwitchingToNewWindowOrANewTab()
        {

            driver.SwitchTo().NewWindow(WindowType.Tab); // opens on a new tab

            //driver.SwitchTo().NewWindow(WindowType.Window);  //opens a new window

            driver.Url = "https://rahulshettyacademy.com/documents-request";

            ArrayList list = new ArrayList(driver.WindowHandles);

            TestContext.Progress.WriteLine(list[0]);
            TestContext.Progress.WriteLine(list[1]);




        }
    }
}