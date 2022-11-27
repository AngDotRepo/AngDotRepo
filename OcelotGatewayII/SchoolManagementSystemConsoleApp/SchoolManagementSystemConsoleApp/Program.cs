using RestSharp;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolManagementSystemConsoleApp
{
    class Program
    {
        private static string token = string.Empty;

        static void Main(string[] args)
        {
            //Get an authentication access token
            GetToken();
        }

        #region Get an authentication access token
        private static void GetToken()
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
            data += FormData("client_secret", "pBU8Q~q~iSRrqQO8~v3wt66SlcZ1DnTQViOIKcNl");
            data += FormData("Client_id", "fae54d7a-4774-4a7c-855f-2cecff201135");
            data += FormData("Code", "0.ARoAv4j5cvGGr0GRqy180BHbR3pN5fp0R3xKhV8s7P8gETU4AF8.AgABAAIAAAD--DLA3VO7QrddgJg7WevrAgDs_wQA9P_ejqZDiEO3Jq0YVdDVcm4WdxbuplDCqBbY3Z2F6RWcOgzeuvtPjSBR7vHKeAVxdl0Cbm2laY_GfjouT8WCty3iIsuGmVgM4bvh_pxu0CMDBkyd-5FAd2FVaui9n5MFKEIuNJyfXHiWy0YXIWz_Q7NPK7z6SuX7vRRp9GOXMAM4qbPcPCu4SAWLzQUp-ZoGQxSdP53khtuUoEDcxmPHrf1nVcpQVOkXFoXEDfnVRuxkoT-Ffk4ynYk2oMv7oDc7J4WZairDcposfquGfCArV8Fn_F5-J8HDoV_eEpt-0kirCFHCDduZMmxgpcs34Bf5YOCCqUpNNaYnP_3_EqpwBDMzmIzY9tna-WHZMrmnpN-aMC_ZntdvmgRir6MTq8YRo9DsC0QdTd9S3H7A0-DxTzh3DZvkrWbUqqaCqXETLGefvq-uFf1Kubux_E3uO4kK_Ehc_IuM0uz6F0_vJ5pellNhbUJ8s1fv8uboP0-lde-ghI2BYF8L0RTzFQEQ5WG7UlS_S5TbkBK5_oV4oYQzN9Bgx69TYCoE_CoLQ8_KdVMCkNNBNpwaBnv-hvNjQo1hDmHBKAxQyzcWD7hPLQwFfumge8HOghUChEzvKqrajnobA1b3wyOyMEVDQgKx2M8QXoeTjfX9pF0mzNzzmIgwlu7yyRxEYvZWPcglvm6glQKkDto3lRXOL9r5vptsPlfn8vxP0_V-o40yATLtZJr80HawOG0HaR_E51WBjWDqGP7TH0Z74K3vCbipwQ2WukyaC6MC2ZLs2Tbu5giP1Lhd8Rt1c6PIq7JGVmuRLOQCzulYQK5nIYQzwdR8Rl0Nbt3Z6T9PkndaO05HO1zwfe34shU9ba8iAuCgCsJzhhefQeFdc7Cvtg");

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
    }
    #endregion

}