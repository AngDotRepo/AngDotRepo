using ETLAPIFramework.ConsoleApp.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

                List<string> stringlists = new List<string>();

                Dictionary<string, string> EmployeeList = new Dictionary<string, string>();

                foreach (var str in jTokens)
                {
                    string strname = str.Name;
                    string strvalue = str.Value.ToString();

                    EmployeeList.Add(strname, strvalue);
                }

                //DataTable dt = listtodt.ToDataTable(list);

                dt = ToDictionary(EmployeeList);

                string tableName = CreateTABLE("[dbo].[testtable]", dt);

                CreateTABLEScript(tableName);

                loadDataTableToSQL.DeleteRecords("[dbo].[testtable]");

                loadDataTableToSQL.BulkInsert(dt, "[dbo].[testtable]");

            }
        }
        public static DataTable ToDictionary(Dictionary<string, string> list)
        {
            DataTable result = new DataTable();
            if (list.Count == 0)
                return result;

            result.Columns.AddRange(
                list.Select(r => new DataColumn(r.Key)).ToArray()
            );

            result.Rows.Add(list.Select(r => new DataColumn(r.Value)).ToArray());

            return result;
        }

        public static string CreateTABLE(string tableName, DataTable table)
        {
            string sqlsc;
            sqlsc = "CREATE TABLE " + tableName + "(";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlsc += "\n [" + table.Columns[i].ColumnName + "] ";
                string columnType = table.Columns[i].DataType.ToString();
                switch (columnType)
                {
                    case "System.Int32":
                        sqlsc += " int ";
                        break;
                    case "System.Int64":
                        sqlsc += " bigint ";
                        break;
                    case "System.Int16":
                        sqlsc += " smallint";
                        break;
                    case "System.Byte":
                        sqlsc += " tinyint";
                        break;
                    case "System.Decimal":
                        sqlsc += " decimal ";
                        break;
                    case "System.DateTime":
                        sqlsc += " datetime ";
                        break;
                    case "System.String":
                    default:
                        sqlsc += string.Format(" nvarchar({0}) ", table.Columns[i].MaxLength == -1 ? "max" : table.Columns[i].MaxLength.ToString());
                        break;
                }
                if (table.Columns[i].AutoIncrement)
                    sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (!table.Columns[i].AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += ",";
            }
            return sqlsc.Substring(0, sqlsc.Length - 1) + "\n)";
        }

        public static void CreateTABLEScript(string tableName)
        {

            string queryString = tableName;
            string connectionString = @"Data Source=SUBBUCHAND19\SQLEXPRESS;" + "Initial Catalog=AdventureWorks2019;" + "Integrated Security=SSPI;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        }

    }
}