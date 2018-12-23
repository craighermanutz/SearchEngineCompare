using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SearchCompare
{
    public class Driver
    { 
        public IWebDriver Instance { get; set; }

        public void Initialize()
        {
            Instance = new ChromeDriver();

        }
    } 
}
