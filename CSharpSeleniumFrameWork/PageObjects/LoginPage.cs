using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFrameWork.PageObjects
{
    public class LoginPage
    {

        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //Page Object Factory design
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.CssSelector, Using = "#signInBtn")]
        private IWebElement signButton;

     

        //By wrapping the login code in one method then calling it from the test object
        public ProductsPage validLogin(String User, String Pass)
        {
            username.SendKeys(User);
            password.SendKeys(Pass);
            //signButton.Click();
            return new ProductsPage(driver);
        }

        // Or create  methods of each of the locators in the login page
        public IWebElement getPassword()
        {
            return password;
        }

        public IWebElement getSigninButton()
        {
            return signButton;
        }
        public IWebElement getUserName()
        {
            return username;
        }

        public void selectDropDown()
        {
            IWebElement dropDown = driver.FindElement(By.CssSelector(".form-group select"));


            SelectElement s = new SelectElement(dropDown);
            s.SelectByText("Consultant");
        }

        public void selectOption()
        {
            IList<IWebElement> radioButtons = driver.FindElements(By.CssSelector("input[type='radio']"));
            foreach (IWebElement radioButtonElements in radioButtons)
            {
                if (radioButtonElements.GetAttribute("value").Equals("user"))
                {
                    radioButtonElements.Click();
                }
            }
        }

    }
}
