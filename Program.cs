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
    public IEnumerable<string> Gsearch { get; set; } //google search results
    public IEnumerable<string> Bsearch { get; set; } // bing search results   
    public IEnumerable<string> Dsearch { get; set; } // duckduckgo search results
    public string SearchParameter { get; set; }      // what will be searhed for across the engines
    public int SearchCalcValue { get; set; }
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
                SearchParameter = userresponse,
                SearchCalcValue = 1
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

            //CompareCalc(Results.Gsearch, Results.Bsearch, Results.Dsearch);           // will soon return a number correlating to 
            //Console.WriteLine("The realatedness of your search is" + CompareCalc);    // comparedness once the method is done

            string[] Test = new string[300];              // currently used to check if string lists are populated
            IEnumerable<string> Test2 = Results.Gsearch;
            int i = 1;
            foreach(string element in Test2)
            {
                Test[i] = element;
                Console.WriteLine("heres duck" + element);
                i++;
            }

            string[] Test3 = new string[300];           // currently used to check if string lists are populated
            IEnumerable<string> Test4 = Results.Gsearch;
            int j = 1;
            foreach (string element in Test4)
            {
                Test[j] = element;
                Console.WriteLine("heres google" + element);
                j++;
            }

            string[] Test5 = new string[300];           // currently used to check if string lists are populated
            IEnumerable<string> Test6 = Results.Gsearch;
            int r = 1;
            foreach (string element in Test6)
            {
                Test[r] = element;
                Console.WriteLine("heres bing" + element);
                r++;
            }
        }

        public static int CompareCalc(IEnumerable<string> searchg, IEnumerable<string> searchb, IEnumerable<string> searchd)
        {

            return CompareValue; // this method will take in the search results of all 3 engines, with urls weeded out, and return a value
                                 // where 1 means all the string collections are identical and 0 is no relatedness.

        }

        public static void Search1(object input)
        {
            NewOne Results = (NewOne)input;

            Driver driver1 = new Driver();
            driver1.Initialize();
            driver1.Instance.Navigate().GoToUrl("http://www.google.com");

            driver1.Instance.FindElement(By.Name("q")).SendKeys(Results.SearchParameter + Keys.Enter);

            List<string> ElemList = new List<string>(driver1.Instance.FindElements(By.XPath("//a[@href]")).Select(iw => iw.Text).ToList());

            IEnumerable<string> RefinedElemlist = from e in ElemList
                                                  where e.Contains("https")
                                                  select e;     
            Results.Gsearch = RefinedElemlist;
        }
         
        public static void Search2(object input)

        {
            NewOne Results = (NewOne)input;

            Driver driver2 = new Driver();
            driver2.Initialize();
            driver2.Instance.Navigate().GoToUrl("http://www.bing.com");

            driver2.Instance.FindElement(By.Id("sb_form_q")).SendKeys(Results.SearchParameter + Keys.Enter);

            List<string> ElemList = new List<string>(driver2.Instance.FindElements(By.XPath("//a[@href]")).Select(iw => iw.Text));

            IEnumerable<string> RefinedElemlist = from e in ElemList
                                                  where e.Contains("http")
                                                  select e;
            Results.Bsearch = RefinedElemlist;
        }

        public static void Search3(object input)
        {
            NewOne Results = (NewOne)input;

            Driver driver3 = new Driver();
            driver3.Initialize();
            driver3.Instance.Navigate().GoToUrl("http://www.duckduckgo.com");

            driver3.Instance.FindElement(By.Id("search_form_input_homepage")).SendKeys(Results.SearchParameter + Keys.Enter);

            List<string> ElemList = new List<string>(driver3.Instance.FindElements(By.XPath("//a[@href]")).Select(iw => iw.Text));

            IEnumerable<string> RefinedElemlist = from e in ElemList
                                                  where e.Contains("http")
                                                  select e;
            Results.Dsearch = RefinedElemlist;
        }
    }
}
