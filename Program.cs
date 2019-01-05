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
public class NewOne : Driver

{
    public List<string> Gsearch { get; set; }
    public List<string> Bsearch { get; set; }
    public List<string> Dsearch { get; set; }
    public string Parameter { get; set; }
}

public class Program : Driver
{
    static void Main(string[] args)
    {
            Console.WriteLine("What searches would you like to compare?");
            string userresponse = Console.ReadLine();
            userresponse = Convert.ToString(userresponse);

            NewOne Results = new NewOne
            {
                Gsearch = null,
                Bsearch = null,
                Dsearch = null,
                Parameter = userresponse
            };

            Thread thread1 = new Thread(Search1);
            Thread thread2 = new Thread(Search2);
            Thread thread3 = new Thread(Search3);

            thread1.Start(Results);
            thread2.Start(Results);
            thread3.Start(Results);

            thread1.Join();
            thread2.Join();
            thread3.Join();

            Console.WriteLine(Results.Dsearch); // currently used to check if string lists are populated
            Console.WriteLine(Results.Gsearch);
            Console.WriteLine(Results.Bsearch);

            string[] Test = new string[300];    // makes sure that chosen string list contains desired information 
            List<string> Test2 = Results.Dsearch;
            int i = 1;
            foreach(string element in Test2)
            {
                Test[i] = element;
                Console.WriteLine(element);
                i++;
            }

        }

        public static void Search1(object input)
        {
            NewOne Results = (NewOne)input;

            Driver driver1 = new Driver();
            driver1.Initialize();
            driver1.Instance.Navigate().GoToUrl("http://www.google.com");

            driver1.Instance.FindElement(By.Name("q")).SendKeys(Results.Parameter + Keys.Enter);

            List<string> ElemList = new List<string>(driver1.Instance.FindElements(By.XPath("//a[@href]")).Select(iw => iw.Text).ToList());

            Results.Gsearch = ElemList;
        }

        public static void Search2(object input)

        {
            NewOne Results = (NewOne)input;

            Driver driver2 = new Driver();
            driver2.Initialize();
            driver2.Instance.Navigate().GoToUrl("http://www.bing.com");

            driver2.Instance.FindElement(By.Id("sb_form_q")).SendKeys(Results.Parameter + Keys.Enter);

            //List<string> ElemList = new List<string>(driver2.Instance.FindElements(By.XPath("//a[@href]")).Select(iw => iw.Text));

            //Results.Bsearch = ElemList;
        }

        public static void Search3(object input)
        {
            NewOne Results = (NewOne)input;

            Driver driver3 = new Driver();
            driver3.Initialize();
            driver3.Instance.Navigate().GoToUrl("http://www.duckduckgo.com");

            driver3.Instance.FindElement(By.Id("search_form_input_homepage")).SendKeys(Results.Parameter + Keys.Enter);

            List<string> ElemList = new List<string>(driver3.Instance.FindElements(By.XPath("//a[@href]")).Select(iw => iw.Text));

            Results.Dsearch = ElemList;
        }
    }
}
