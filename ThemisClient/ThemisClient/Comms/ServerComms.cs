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

namespace ThemisClient.Comms
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
            var response = await client.PostAsync("/login?useCookies=true", body);

            response.EnsureSuccessStatusCode();

            File.WriteAllText(cookieDataPath, JsonSerializer.Serialize(cookieContainer.GetCookies(baseUri).Select(i => i.ToString())));
            return true;
        }

        public async Task<bool> isLoggedIn()
        {
            var response = await client.GetAsync("/getsessioninfo");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> downloadService(string directory)
        {
            var response = await client.GetAsync("/service");
            if(!(response.StatusCode == HttpStatusCode.OK))
            {
                return false;
            }
            string ServiceUrl = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("ServiceUrl: " + ServiceUrl);
            return true;
            /*
            var stream = await client.GetStreamAsync(ServiceUrl);
            var fileStream = new FileStream(directory+ "\\ThemisService.dll", FileMode.OpenOrCreate);
            await stream.CopyToAsync(fileStream);
            stream.Dispose();
            fileStream.Dispose();
            return true;*/
        }

        public async Task<bool> downloadDll(string directory)
        {
            var response = await client.GetAsync("/dll");
            if (!(response.StatusCode == HttpStatusCode.OK))
            {
                return false;
            }
            string DllUrl = await response.Content.ReadAsStringAsync();
            return true;
            Debug.WriteLine("DllUrl: " + DllUrl);
            /*var stream = await client.GetStreamAsync(DllUrl);
            var fileStream = new FileStream(directory + "\\ThemisDll.dll", FileMode.OpenOrCreate);
            await stream.CopyToAsync(fileStream);
            stream.Dispose();
            fileStream.Dispose();
            return true;*/
        }

        private bool LoadCookieData(string baseAddress)
        {
            List<string> cookies;
            try
            {
                cookies = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(cookieDataPath));
            }
            catch {
                File.Create(cookieDataPath);
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
