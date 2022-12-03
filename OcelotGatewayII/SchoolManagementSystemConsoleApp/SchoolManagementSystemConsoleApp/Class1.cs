//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;


//namespace ConsoleApp1
//{
//    class Program
//    {
//        static async Task Main(string[] args)
//        {
//            await asyncTaskCallWebAPIAsync();
//        }

//        static async Task asyncTaskCallWebAPIAsync()
//        {
//            using (var client = new HttpClient())
//            {
//                client.BaseAddress = new Uri("https://portal.azure.com/#asset/Microsoft_Azure_Security_Insights/Incident/subscriptions/c3f6ae6f-d48a-4cd4-b8bf-c85970ad6906/resourceGroups/ftrg/providers/Microsoft.OperationalInsights/workspaces/ftmicrosoftsentinell/providers/Microsoft.SecurityInsights/Incidents/37e63d9b-12db-4c97-a105-42c4bbfc9a8e");
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//                // New code:
//                HttpResponseMessage response = await client.GetAsync("https://portal.azure.com/#asset/Microsoft_Azure_Security_Insights/Incident/subscriptions/c3f6ae6f-d48a-4cd4-b8bf-c85970ad6906/resourceGroups/ftrg/providers/Microsoft.OperationalInsights/workspaces/ftmicrosoftsentinell/providers/Microsoft.SecurityInsights/Incidents/37e63d9b-12db-4c97-a105-42c4bbfc9a8e");
//                if (response.IsSuccessStatusCode)
//                {
//                    Rootobject rootobject = await response.Content.ReadAsAsync<Rootobject>();
//                }
//            }
//        }

//        static async Task AsyncTaskGenerateTokenAsync()
//        {
//            string id_token = null;
//            var request = (HttpWebRequest)WebRequest.Create("https://login.windows.net/" + "microsoft.onmicrosoft.com" + "/oauth2/token");

//            string FormData(string key, string val)
//            {
//                return key + "=" + HttpUtility.UrlEncode(val) + "&";
//            }

//            string data = "";
//            data += FormData("grant_type", "client_credentials");
//            data += FormData("client_secret", "");
//            data += FormData("Client_id", "");

//            data = data.Substring(0, data.Length - 1);

//            var resp = Encoding.ASCII.GetBytes(data);

//            request.Method = "POST";
//            request.ContentType = "application/x-www-form-urlencoded";
//            request.ContentLength = resp.Length;

//            using (var stream = request.GetRequestStream())
//            {
//                stream.Write(resp, 0, resp.Length);
//            }

//            var response = (HttpWebResponse)request.GetResponse();

//            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

//            dynamic valuePoco = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
//            string access_token = Convert.ToString(valuePoco.access_token);
//            id_token = Convert.ToString(valuePoco.id_token);
//            string refresh_token = Convert.ToString(valuePoco.refresh_token);
//        }

//        public class Rootobject
//        {
//            public string id { get; set; }
//            public string name { get; set; }
//            public string type { get; set; }
//            public string etag { get; set; }
//            public Properties properties { get; set; }
//        }

//        public class Properties
//        {
//            public DateTime lastModifiedTimeUtc { get; set; }
//            public DateTime createdTimeUtc { get; set; }
//            public DateTime lastActivityTimeUtc { get; set; }
//            public DateTime firstActivityTimeUtc { get; set; }
//            public string description { get; set; }
//            public string title { get; set; }
//            public Owner owner { get; set; }
//            public string severity { get; set; }
//            public string classification { get; set; }
//            public string classificationComment { get; set; }
//            public string classificationReason { get; set; }
//            public string status { get; set; }
//            public string incidentUrl { get; set; }
//            public int incidentNumber { get; set; }
//            public object[] labels { get; set; }
//            public string[] relatedAnalyticRuleIds { get; set; }
//            public Additionaldata additionalData { get; set; }
//        }

//        public class Owner
//        {
//            public string objectId { get; set; }
//            public string email { get; set; }
//            public string userPrincipalName { get; set; }
//            public string assignedTo { get; set; }
//        }

//        public class Additionaldata
//        {
//            public int alertsCount { get; set; }
//            public int bookmarksCount { get; set; }
//            public int commentsCount { get; set; }
//            public object[] alertProductNames { get; set; }
//            public string[] tactics { get; set; }
//        }
//    }
//}