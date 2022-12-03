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
                string authority = "https://login.microsoftonline.com/{tenant}";
                string resrouce = "https://management.core.windows.net";
                string clientId = "";
                string secret = "";
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
                string authority = "https://login.microsoftonline.com/{tenant}";
                string resrouce = "https://graph.microsoft.com";
                string clientId = "";
                string secret = "";
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

            //        //https://login.microsoftonline.com/{tenant}/oauth2/authorize?client_id={}&response_type=code&redirect_url=http%3a%2f%2flocalhost%3a4200&sso_reload=true&sso_nonce=AwABAAEAAAACAOz_BAD0_zSRcWqaC1uUXcZK9c1Msq09eIRkC_01pUwKsI7_D9I_ckZo4DVmJiLMIj2iZNDjdiS7GwMQkvNJs4YtI6Lvm_YgAA&client-request-id=e80171ec-76e4-4f40-aef4-3a2b92cf8879&mscrid=e80171ec-76e4-4f40-aef4-3a2b92cf8879

            string data = "";
            data += FormData("grant_type", "Authorization_Code");
            data += FormData("client_secret", "");
            data += FormData("Client_id", "");
            data += FormData("Code", "");

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