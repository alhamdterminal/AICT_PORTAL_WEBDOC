using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace WebdocTerminal.AuthTradelens
{
    public static class Authenticator
    {
        public static string BASE_AUTH_TOKEN_URL = "https://iam.cloud.ibm.com";

        public static string BASE_AUTH_API_URL = "https://platform.tradelens.com";

        //public static string BASE_AUTH_API_URL = "https://platform-sandbox.tradelens.com";


        public static async Task<TokenAuthResponse> GetAuthToken()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_AUTH_TOKEN_URL);

                client.DefaultRequestHeaders.Add("Accept", "application/json");

                //string api_key = "UIT8wprpojpj_7lpyIX8cu6KMwfY_AFniZufiicPIcD9";
                string api_key_sandbox = "JeNyzvLJraWEI072-s0jCjFiZJCMT-ueLqacSHPBpr_9";

                string content = "grant_type=urn:ibm:params:oauth:grant-type:apikey&apikey=" + api_key_sandbox;
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);

                var result = await client.PostAsync("/identity/token", byteContent);

                string resultContent = await result.Content.ReadAsStringAsync();

                var authResponse = JsonConvert.DeserializeObject<TokenAuthResponse>(resultContent);

                return authResponse;
            }
        }

        public static async Task<AuthResponse> GetAPIAuthToken(TokenAuthResponse resp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_AUTH_API_URL);

                var srzBody = JsonConvert.SerializeObject(resp);
                var content = new StringContent(srzBody, Encoding.UTF8, "application/json");
                var orgId = "6cf1d3ee-6b0b-41a9-8d2d-bcab8d285196";
                //var orgId_sandbox = "4380e94c-9621-4fa5-8671-f0991131c74b";
                var result = await client.PostAsync("/sa/api/v1/auth/exchange_token/organizations/" + orgId, content);
                string resultContent = await result.Content.ReadAsStringAsync();

                var des = JsonConvert.DeserializeObject<AuthResponse>(resultContent);

                return des;
            }
        }
    }
}
