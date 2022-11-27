using ETLAPIFramework.ConsoleApp.Data.Common;
using ETLAPIFramework.ConsoleApp.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Net;
using System.Net.Http;

namespace ETLAPIFramework.ConsoleApp.Data.APIRequests
{
    public static class APIRequest
    {
        public static void GetAPIRequest()
        {
            List<CommonModel> list = new List<CommonModel>();

            Console.WriteLine("Making API Call...");
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                //https://jsonplaceholder.typicode.com/todos

                client.BaseAddress = new Uri("https://fakestoreapi.com/products/1");
                HttpResponseMessage response = client.GetAsync("https://fakestoreapi.com/products/1").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;

                //list = JsonConvert.DeserializeObject<List<CommonModel>>(result);

                dynamic MyDynamic = new ExpandoObject();

                MyDynamic = JsonConvert.DeserializeObject<dynamic>(result);

                LoadDataTableToSQL loadDataTableToSQL = new LoadDataTableToSQL();

                ConvertListObjectToDataTable listtodt = new ConvertListObjectToDataTable();
                //DataTable dt = listtodt.ToDataTable(list);

                DataTable dt = listtodt.ToDataTable(MyDynamic);
                loadDataTableToSQL.DeleteRecords("[dbo].[test]");
                loadDataTableToSQL.BulkInsert(dt, "[dbo].[test]");

            }
        }


    }
}