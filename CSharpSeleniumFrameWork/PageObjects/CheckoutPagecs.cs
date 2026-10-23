using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFrameWork.PageObjects
{
    public class CheckoutPagecs
    {
        private IWebDriver driver;
        public CheckoutPagecs(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        // IWebElement checkOut = driver.FindElement(By.CssSelector("button[class= 'btn btn-success']"));

        [FindsBy(How = How.CssSelector, Using = "button[class= 'btn btn-success']")]
        private IWebElement checkOutBut;

        public IWebElement getCheckOutButon()
        {
            return checkOutBut;
        }



    }
}
