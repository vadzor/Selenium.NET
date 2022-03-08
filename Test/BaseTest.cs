using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxDB.Collector;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.NET.InfluxDB;
using Selenium.NET.Pages;

namespace Selenium.NET.Test
{
   class BaseTest
    {
        IWebDriver driver;
        private string AVIC_URL = "https://avic.ua/";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = AVIC_URL;

            Metrics.Collector = new CollectorConfiguration()
                .Tag.With("project", InfluxConfig.Database)
                .Tag.With("testclass", TestContext.CurrentContext.Test.ClassName)
                .Tag.With("test", TestContext.CurrentContext.Test.Name)
                .Batch.AtInterval(TimeSpan.FromSeconds(2))
                .WriteTo.InfluxDB(InfluxConfig.Host, InfluxConfig.Database)
                .CreateCollector(); 

        }

        

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
            
        }

        public HomePage GetHomePage() { return new HomePage(driver); }
    }
}
