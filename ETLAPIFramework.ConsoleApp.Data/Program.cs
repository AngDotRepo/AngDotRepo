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

            urllist.Add("https://jsonplaceholder.typicode.com/todos");
            //urllist.Add("https://fakestoreapi.com/products");

            foreach (string url in urllist)
            {
                APIRequest.GetAPIRequest(url);
            }

            
        }
    }
}
