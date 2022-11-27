using ETLAPIFramework.ConsoleApp.Data.APIRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETLAPIFramework.ConsoleApp.Data
{
    static class Program
    {
        static void Main(string[] args)
        {

            List<string> urllist = new List<string>();


            List<Dictionary<string, string>> keyValuePairs = new List<Dictionary<string, string>>();

            Dictionary<string, string> urlTableList1 = new Dictionary<string, string>();
            Dictionary<string, string> urlTableList2 = new Dictionary<string, string>();

            urlTableList1.Add("https://jsonplaceholder.typicode.com/todos/1", "dbo.testtodos");
            urlTableList2.Add("https://fakestoreapi.com/products/1", "dbo.testproducts");

            keyValuePairs.Add(urlTableList1);
            keyValuePairs.Add(urlTableList2);

            foreach (var keyValuePair in keyValuePairs)
            {
                foreach (var pair in keyValuePair)
                {
                    string url = pair.Key;
                    string table = pair.Value;

                    APIRequest.GetAPIRequest(url, table);
                }

                //APIRequest.GetAPIRequest(keyValuePair.Keys,keyValuePair.Values);
            }

        }
    }
}
