using ETLAPIFramework.ConsoleApp.Data.Common;
using ETLAPIFramework.ConsoleApp.Data.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace ETLAPIFramework.ConsoleApp.Data.APIRequests
{
    public static class APIRequest
    {
        public static void GetAPIRequest(string url)
        {
            List<CommonModel> list = new List<CommonModel>();

            Console.WriteLine("Making API Call...");

            if (url.Contains("/1"))
            {
                Console.WriteLine("URL Contain Some Numbers...Hence Please check...API Call...");
                Environment.Exit(0);
            }

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                //1st//https://jsonplaceholder.typicode.com/todos
                //2nd//https://fakestoreapi.com/products/1

                client.BaseAddress = new Uri(url);

                HttpResponseMessage response = client.GetAsync(url).Result;

                response.EnsureSuccessStatusCode();

                string result = response.Content.ReadAsStringAsync().Result;

                //list = JsonConvert.DeserializeObject<List<CommonModel>>(result);

                dynamic MyDynamic = new ExpandoObject();

                DataTable dt = new DataTable();

                MyDynamic = JsonConvert.DeserializeObject<dynamic>(result);

                LoadDataTableToSQL loadDataTableToSQL = new LoadDataTableToSQL();

                ConvertListObjectToDataTable listtodt = new ConvertListObjectToDataTable();

                var users = MyDynamic[0];

                var jTokens = users.Children();

                //DataTable dtrefined = new DataTable();

                List<string> stringlists = new List<string>();

                List<Dictionary<string, string>> EmployeeList = new List<Dictionary<string, string>>();

                foreach (var str in jTokens)
                {
                    foreach (var keyValuePairs in EmployeeList)
                    {
                        //DataColumn dc = new DataColumn(str.Name, typeof(string));
                        //DataRow dr = dt.NewRow();
                        //dt.Columns.Add(dc);
                        //dr[str.Name] = str.Value;

                        //dt.Rows.Add(dr);//this will add the row at the end of the datatable

                        string strr = str.Name;
                        string strvalue = str.Value;

                        keyValuePairs.Add(strr, strvalue);
                    }

                }

                //DataTable dt = listtodt.ToDataTable(list);

                dt = ToDictionary(EmployeeList);

                loadDataTableToSQL.DeleteRecords("[dbo].[test]");

                loadDataTableToSQL.BulkInsert(dt, "[dbo].[test]");

            }
        }


        public static DataTable ToDictionary(List<Dictionary<string, string>> list)
        {
            DataTable result = new DataTable();
            if (list.Count == 0)
                return result;

            result.Columns.AddRange(
                list.First().Select(r => new DataColumn(r.Key)).ToArray()
            );

            list.ForEach(r => result.Rows.Add(r.Select(c => c.Value).Cast<object>().ToArray()));

            return result;
        }

    }
}