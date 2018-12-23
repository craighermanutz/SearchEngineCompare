using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace SearchCompare
{
    class Program : Driver
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What searches would you like to compare?");
            string userresponse = Console.ReadLine();
            userresponse = Convert.ToString(userresponse);

            Thread thread1 = new Thread(Search1);
            Thread thread2 = new Thread(Search2);
            Thread thread3 = new Thread(Search3);

            thread1.Start(userresponse);
            thread2.Start(userresponse);
            thread3.Start(userresponse);

            thread1.Join();
            thread2.Join();
            thread3.Join();
        }

        public static void Search1(object input)

        {
            Driver driver1 = new Driver();
            driver1.Initialize();
            driver1.Instance.Navigate().GoToUrl("http://www.google.com");

            driver1.Instance.FindElement(By.Name("q")).SendKeys(input + Keys.Enter);

            var link = driver1.Instance.FindElements(By.LinkText("ping"));
            Console.Write(link);
        }

        public static void Search2(object input)

        {
            Driver driver2 = new Driver();
            driver2.Initialize();
            driver2.Instance.Navigate().GoToUrl("http://www.bing.com");

            driver2.Instance.FindElement(By.Id("sb_form_q")).SendKeys(input + Keys.Enter);
        }

        public static void Search3(object input)
        {
            Driver driver3 = new Driver();
            driver3.Initialize();
            driver3.Instance.Navigate().GoToUrl("http://www.duckduckgo.com");

            driver3.Instance.FindElement(By.Id("search_form_input_homepage")).SendKeys(input + Keys.Enter);
        }
    }
}
