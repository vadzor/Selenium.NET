using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Selenium.NET.Pages;
using Selenium.NET.InfluxDB;

namespace Selenium.NET.Test
{
    class HomePageTest : BaseTest
    {
        private readonly string KEYWORD = "iPhone 13";

        [Test]
        public void Test1()
        {
            GetHomePage().SearchByKeyword(KEYWORD);

            GetHomePage().MeasurePageLoad("Avic", "Search",()=> GetHomePage().SearchButton().Click());
            GetHomePage().WaitForPageLoadComplete();
            GetHomePage().WriteResponseTime();

            foreach (IWebElement element in GetHomePage().SearchElements())
            {
                Assert.True(element.Text.Contains(KEYWORD));
            }

            

        }

       
    }
}