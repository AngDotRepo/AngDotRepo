using ETLAPIFramework.ConsoleApp.Data.APIRequests;
using System;
using System.Collections.Generic;

namespace ETLAPIFramework.ConsoleApp.Data
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> urllist = new List<string>();

            Dictionary<string, string> urlTableList = new Dictionary<string, string>();

            urlTableList.Add("https://jsonplaceholder.typicode.com/todos/1","dbo.testtodos");
            urlTableList.Add("https://fakestoreapi.com/products/1","dbo.testproducts");

            foreach (string url in urllist)
            {
                APIRequest.GetAPIRequest(url);
            }

            
        }
    }
}
