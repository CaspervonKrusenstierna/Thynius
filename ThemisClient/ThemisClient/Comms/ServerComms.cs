using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ThyniusClient.Comms
{
    public class ServerComms
    {
        private string cookieDataPath;
        private Uri baseUri;
        private CookieContainer cookieContainer;
        private HttpClientHandler clientHandler;
        private HttpClient client;
        public ServerComms(string baseAddress, string CookieDataPath)
        {
            cookieDataPath = CookieDataPath;
            baseUri = new Uri(baseAddress);

            cookieContainer = new CookieContainer();
            LoadCookieData(baseAddress);
            clientHandler = new HttpClientHandler { UseCookies = true, CookieContainer = cookieContainer };

            client = new HttpClient(clientHandler);
            client.BaseAddress = baseUri;
        }

        public async Task<bool> LoginAsync(string _email, string _password)
        {
            var body = JsonContent.Create(new { email = _email, password = _password });
            var response = await client.PostAsync("/account/login?useCookies=true", body);
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                return false;
            }
            File.WriteAllText(cookieDataPath, JsonSerializer.Serialize(cookieContainer.GetCookies(baseUri).Select(i => i.ToString())));
            return true;
        }

        public async Task<bool> isLoggedIn()
        {
            var response = await client.GetAsync("/account/getsessioninfo");
            if ((int)response.StatusCode == 401)
            {
                return false;
            }
            return true;
        }

        private bool LoadCookieData(string baseAddress)
        {
            List<string> cookies;
            try
            {
                cookies = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(cookieDataPath));
            }
            catch {
                File.Create(cookieDataPath).Close();
                return false;
            }

            try
            {
                string CombinedCookieString = "";
                int CookieCount = cookies.Count();
                for(int i = 0; CookieCount > i; i++)
                {
                    CombinedCookieString += cookies[i];
                    if(i != CookieCount-1)
                    {
                        CombinedCookieString += ", ";
                    }
                }
                cookieContainer.SetCookies(baseUri, CombinedCookieString);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
