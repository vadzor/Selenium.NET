using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using InfluxDB.Collector;
using Selenium.NET.InfluxDB;

namespace Selenium.NET.Pages
{
    class BasePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public DefaultWait<IWebDriver> fluentWait
        {
            get
            {
                DefaultWait<IWebDriver> _fluentWait = new DefaultWait<IWebDriver>(driver);
                _fluentWait.Timeout = TimeSpan.FromSeconds(10);
                _fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
                return _fluentWait;
            }
        }
        public IJavaScriptExecutor js => (IJavaScriptExecutor)driver; 
       

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        public void WaitForPageLoadComplete()
        {
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void WriteResponseTime()
        {
             var ResponseTime = Convert.ToInt32(((IJavaScriptExecutor)driver).ExecuteScript("return window.performance.timing.loadEventEnd - window.performance.timing.navigationStart;"));
             Console.WriteLine("Page {0} loading time is {1}", driver.Title, ResponseTime);
        }
        public Metric MeasurePageLoad(string scenarioName, string actionName, Action action)
        {
            var startTime = (long)js.ExecuteScript("return window.performance.timing.navigationStart");

            action.Invoke();

            fluentWait.Until(x => (long)js.ExecuteScript("return window.performance.timing.navigationStart") != startTime);

            Metric metric = new Metric
            {
                ScenarioName = scenarioName,
                ActionName = actionName,
                Elapsed = PageLoadTime()
            };

            Write(metric);
            return metric;
        }

        private void Write(Metric metric)
        {
            Metrics.Write(InfluxConfig.Measurement, new Dictionary<string, object>
            {
                ["elapsed"] = metric.Elapsed.TotalMilliseconds
            }, new Dictionary<string, string>
            {
                ["action"] = metric.ActionName,
                ["scenario"] = metric.ScenarioName,
            });
        }

        public TimeSpan PageLoadTime()
        {
            double pageLoadTime = 0.0;

            fluentWait.Until(x =>
            {
                if (!(bool)js.ExecuteScript("return document.readyState === 'complete'"))
                    return false;
                pageLoadTime = (double)(long)js.ExecuteScript("return performance.timing.loadEventEnd - performance.timing.navigationStart;");
                return pageLoadTime >= 0.0;
            });

            return TimeSpan.FromMilliseconds(Convert.ToDouble(pageLoadTime));
        }


    }
}
