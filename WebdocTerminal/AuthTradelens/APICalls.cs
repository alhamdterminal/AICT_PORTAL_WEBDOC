using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebdocTerminal.Models;

namespace WebdocTerminal.AuthTradelens
{
    public class APICalls
    {
        public static string BASE_API_URL = "https://platform.tradelens.com";
        //public static string BASE_API_URL = "https://platform-sandbox.tradelens.com";

        public static async Task<EDI_Tradelens_APIResponse> ActualGateIn(string token, ActualGateIn req)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_API_URL);

                client.DefaultRequestHeaders.Add("Accept", "application/json");

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var srzBody = JsonConvert.SerializeObject(req);
                var content = new StringContent(srzBody, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("/api/v1/transportEvents/actualGateIn", content);
                string resultContent = await result.Content.ReadAsStringAsync();

                var desResp = JsonConvert.DeserializeObject<EDI_Tradelens_APIResponse>(resultContent);

                return desResp;
            }
        }

        public static async Task<EDI_Tradelens_APIResponse> ActualStripping(string token, ActualStripping req)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_API_URL);

                client.DefaultRequestHeaders.Add("Accept", "application/json");

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var srzBody = JsonConvert.SerializeObject(req);
                var content = new StringContent(srzBody, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("/api/v1/transportEvents/actualContainerStripped", content);
                string resultContent = await result.Content.ReadAsStringAsync();

                var desResp = JsonConvert.DeserializeObject<EDI_Tradelens_APIResponse>(resultContent);

                return desResp;
            }
        }

        public static async Task<EDI_Tradelens_APIResponse> ActualGateOut(string token, ActualGateOut req)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_API_URL);

                client.DefaultRequestHeaders.Add("Accept", "application/json");

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var srzBody = JsonConvert.SerializeObject(req);
                var content = new StringContent(srzBody, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("/api/v1/transportEvents/actualGateOut", content);
                string resultContent = await result.Content.ReadAsStringAsync();

                var desResp = JsonConvert.DeserializeObject<EDI_Tradelens_APIResponse>(resultContent);

                return desResp;
            }
        }
    }
}
