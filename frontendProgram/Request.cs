using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
namespace frontendProgram.Requests
{
    public class Request
    {
        private static string client_id = "0a93c763bd40398b525f";
        private static string githubLoginURL = "https://github.com/login/device/code";
        private static string AccessTokenURL = "https://github.com/login/oauth/access_token";
        private static string grantType = "urn:ietf:params:oauth:grant-type:device_code";
        private static string? bearerToken;
        public static string getUserCode(string full_device_code)
        {
            string[] parts = full_device_code.Split('&');
            string device_code = parts[3].Split("=")[1];
            return (device_code);
        }
        public static string getDeviceCode(string full_device_code)
        {
            string[] parts = full_device_code.Split('&');
            string device_code = parts[0].Split("=")[1];
            return (device_code);
        }

        public static void setBearerToken(string new_bearerToken)
        {
            bearerToken = new_bearerToken;
        }

        public static string getBearerToken()
        {
            return bearerToken;
        }
       public static async Task<string> GetDeviceCode()
        {
        using (HttpClient client = new HttpClient())
            {
                string key = "client_id";
                string jsonData = $"{{\"{key}\": \"{client_id}\"}}";
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json"); 
                
                try
                {
                    HttpResponseMessage response = await client.PostAsync(githubLoginURL, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        return (responseBody);
                    }
                    else
                    {
                        return ("Error: "+response.StatusCode);
                    }
                }catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }


        public static async Task<string> AuthorizeLogin(string device_code)
        {
            using (HttpClient client = new HttpClient()) {
                string[] keys = { "client_id", "device_code", "grant_type" };
                string jsonObject = $"{{\"{keys[0]}\": \"{client_id}\", \"{keys[1]}\": \"{device_code}\", \"{keys[2]}\": \"{grantType}\"}}";

                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(AccessTokenURL, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                    else
                    {
                        return ("Login Error: " + response.StatusCode);
                    }
                }catch(Exception ex)
                {
                    return(ex.Message);
                }
            }
        }
    }
}
