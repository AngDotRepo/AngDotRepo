using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SchoolManagementSystemConsoleApp
{
    class Program
    {

        [Obsolete]
        static void Main(string[] args)
        {
            string token = GetAccessToken();
        }

        #region Get an authentication access token via client credentials - Management Core
        [Obsolete]
        static string GetAccessToken()
        {
            string tokenn = null;

            try
            {
                string authority = "https://login.microsoftonline.com/e836f25b-d75a-4cb5-865e-ff52bda62dff";
                string resrouce = "https://management.core.windows.net";
                string clientId = "4a60585e-3399-4bf8-8c2b-74a9817dc3be";
                string secret = "_SY8Q~1AzUvOI1n0OxfHOj-nfZOdTY6CgyBs6dwK";
                ClientCredential credential = new ClientCredential(clientId, secret);
                AuthenticationContext authContext = new AuthenticationContext(authority);

                var token = authContext.AcquireTokenAsync(resrouce, credential).Result.AccessToken;
                return token;
            }

            catch (Exception ex)
            {

            }

            return tokenn;
        }
        #endregion

        #region Get an authentication access token via client credentials - Graph
        [Obsolete]
        static string GetAccessTokenGraph()
        {
            string tokenn = null;

            try
            {
                string authority = "https://login.microsoftonline.com/e836f25b-d75a-4cb5-865e-ff52bda62dff";
                string resrouce = "https://graph.microsoft.com";
                string clientId = "4a60585e-3399-4bf8-8c2b-74a9817dc3be";
                string secret = "_SY8Q~1AzUvOI1n0OxfHOj-nfZOdTY6CgyBs6dwK";
                ClientCredential credential = new ClientCredential(clientId, secret);
                AuthenticationContext authContext = new AuthenticationContext(authority);

                var token = authContext.AcquireTokenAsync(resrouce, credential).Result.AccessToken;
                return token;
            }

            catch (Exception ex)
            {

            }

            return tokenn;
        }
        #endregion

        #region Get an authentication access token via code
        private static void GetTokenviaCode()
        {
            string id_token = null;
            var request = (HttpWebRequest)WebRequest.Create("https://login.windows.net/" + "microsoft.onmicrosoft.com" + "/oauth2/token");

            string FormData(string key, string val)
            {
                return key + "=" + HttpUtility.UrlEncode(val) + "&";
            }

            //        //https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47/oauth2/authorize?client_id=fae54d7a-4774-4a7c-855f-2cecff201135&response_type=code&redirect_url=http%3a%2f%2flocalhost%3a4200&sso_reload=true&sso_nonce=AwABAAEAAAACAOz_BAD0_zSRcWqaC1uUXcZK9c1Msq09eIRkC_01pUwKsI7_D9I_ckZo4DVmJiLMIj2iZNDjdiS7GwMQkvNJs4YtI6Lvm_YgAA&client-request-id=e80171ec-76e4-4f40-aef4-3a2b92cf8879&mscrid=e80171ec-76e4-4f40-aef4-3a2b92cf8879

            string data = "";
            data += FormData("grant_type", "Authorization_Code");
            data += FormData("client_secret", "");
            data += FormData("Client_id", "");
            data += FormData("Code", "0.ARoAW_I26FrXtUyGXv9SvaYt_15YYEqZM_hLjCt0qYF9w744AF8.AgABAAIAAAD--DLA3VO7QrddgJg7WevrAgDs_wQA9P8CRPo262HWW5bx4KLMJS8EC7BDquWbh6CjTXuziR-YGVc6nv6fOILCl8YRuVWgj0Cl3gfH94w02B-KXSOGP2UqFnmleT7aTwddoegiOgvLZuT89Gr9o-ZhsMAp6izah8xETA-RaFFRvUmwxwibcpkhVCxtP9qNFREFrIAvtV0nza54pqxujRPxgbK2QT0rzp6qDAfhjLYcHeAcmn5_yk98hsay-GxvC9oslZ0rrA-5-T4pez_bko8VMG5BHzuHjQFZ3UB8eY5M0yYFJZxaG1W4QPZXqxeM4FY3kW8MQDW0Bw8Eu6l3Qn7dQPyEKjOpvRGx5SqLCH-rnGauuQvdWmWbU6xBfp0LUqfTQTuyjlcbgdVrxF6H_2YZt2fkyofzOTd0I86WEPukFbXgHNB8JREiVNYGlwot_xvpVxfaIPAgS4SL22ht7PJWqlU_9fShra3nh8zi7ocRW0S58ZLkUWZnCoXbIW7SqOVPtiJoy7B-XGtTEeqNCYtMeI8Anuo4GWweP3rPVcjIdnetqRZKxsVyaEUKZ4y_bS1DFnOYgd-SslA4LH7tAIudhEUcz8Te3JjSkL0MXL8DbQ");

            data = data.Substring(0, data.Length - 1);

            var resp = Encoding.ASCII.GetBytes(data);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = resp.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(resp, 0, resp.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            dynamic valuePoco = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
            string access_token = Convert.ToString(valuePoco.access_token);
            id_token = Convert.ToString(valuePoco.id_token);
            string refresh_token = Convert.ToString(valuePoco.refresh_token);

        }
        #endregion
    }
}