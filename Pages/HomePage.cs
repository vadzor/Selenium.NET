using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium.NET.Pages
{
    class HomePage : BasePage 
    {

        [FindsBy(How = How.XPath, Using = "//input[@id='input_search']")]
        private IWebElement searchField;

        [FindsBy(How = How.XPath, Using = "//button[@class='button-reset search-btn']")]
        private IWebElement searchButton;

        [FindsBy(How = How.XPath, Using = "//div[@class='prod-cart__descr']")]
        private IList<IWebElement> searchElements;

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement SearchField() { return searchField; }
        public IWebElement SearchButton() { return searchButton; }

        public IList<IWebElement> SearchElements() { return searchElements; }

        public void SearchByKeyword(string keyword)
        {
            SearchField().SendKeys(keyword);
        }




    }
}
