using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.NET.InfluxDB
{
    public class Metric
    {
        public string ScenarioName { get; set; }
        public string ActionName { get; set; }
        public TimeSpan Elapsed { get; set; }

    }
}
