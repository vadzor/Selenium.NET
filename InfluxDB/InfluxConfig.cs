using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.NET.InfluxDB
{
    public static class InfluxConfig
    {
        public static string Host => "http://localhost:8086";
        public static string Database => "avic";
        public static string Measurement => "actions";
        public static bool Enabled => true;
    }
}
