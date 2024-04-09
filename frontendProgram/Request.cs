using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace frontendProgram.Requests
{
    public class Request
    {
        private static string client_id = "0a93c763bd40398b525f";
        private static string githubLoginURL = "https://github.com/login/device/code";
        private static string AccessTokenURL = "https://github.com/login/oauth/access_token";
        private static string userInfoURL = " https://api.github.com/user";
        private static string grantType = "urn:ietf:params:oauth:grant-type:device_code";
        private static string? bearerToken;
        private static string? user_name;
        private static string? jwt;
        private static string apiURL = "http://bookmark.phipson.co.za:5170";
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
        public static void setJWT(string JWT)
        {
            jwt = JWT;
        }
        public static string getJWT()
        {
            return jwt;
        }
        public static string getUsername()
        {
            return user_name;
        }
        public static void setUsername(string newUserName)
        {
            user_name = newUserName;
        }

        public static void setBearerToken(string new_bearerToken)
        {
            bearerToken = new_bearerToken;
        }

        public static string getBearerToken()
        {
            return bearerToken;
        }


        public static async Task newJWT()
        {
            using (HttpClient client = new HttpClient())
            {
                string queryURL = apiURL + "/api/login?username=" + user_name + "&githubToken=" + bearerToken;
                try
                {
                    HttpResponseMessage response = await client.PostAsync(queryURL + "/api/Login",null);
                    if (response.IsSuccessStatusCode)
                    {
                        var body = await response.Content.ReadAsStreamAsync();
                        using (JsonDocument jsonDocument = await JsonDocument.ParseAsync(body))
                        {
                            JsonElement root = jsonDocument.RootElement;
                            string JWT = root.GetProperty("token").GetString();
                            setJWT(JWT);

                        }
                    }
                    else
                    {
                        string body=await response.Content.ReadAsStringAsync();
                        Debug.WriteLine(body);
                    }
                }catch(Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
       public static async Task<string> getUserName()
        {
            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(userInfoURL),
                    Method = HttpMethod.Get,
                };
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
                request.Headers.UserAgent.ParseAdd("Bookmarkers/1.0.0 (Microsoft MAUI; .NET 6.0; Windows 10)");

                try
                {
                    HttpResponseMessage response = await client.SendAsync(request);
                    if(response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStreamAsync();

                        using (JsonDocument jsonDocument = await JsonDocument.ParseAsync(content))
                        {
                            JsonElement root = jsonDocument.RootElement;
                            string username = root.GetProperty("login").GetString();
                            setUsername(username);
                            return(username);

                        }
                    }
                    else
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return ("failed request: " + response.StatusCode.ToString());
                    }

                }
                catch (Exception ex)
                {
                    return ("Error "+ex);
                }
            }
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
