using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFrameWork.PageObjects
{
    public class ProductsPage
    {
        private IWebDriver driver;

        By cardTitle = By.CssSelector(".card-title a");

        By pButton = By.CssSelector(".card-footer button");

        public ProductsPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        //IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));
        //driver.FindElement(By.PartialLinkText("Checkout")).Click();


        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> cards;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkOut;

        public void waitForPage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button.btn")));
        }
        public IList<IWebElement> getCards()
        {
            return cards;
        }

        public By getCardTitle()
        {
            return cardTitle;
        }

        public By getfooterButton()
        {
            return pButton;
        }

        public CheckoutPagecs getCheckOut()
        {
            {
                checkOut.Click();
                return new CheckoutPagecs(driver);
            }
        }
    }
}
