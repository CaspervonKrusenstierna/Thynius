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
using ThemisClient.InputTreater;

namespace ThemisClient.Comms
{
    struct sessionMetaData
    {
        public int WordCount;
        public int CharCount;
    }
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

        public async Task<bool> SubmitSession(string GUID, string SessionPath)
        {
            List<unTreatedInput> untreatedInputs = InputTreater.InputTreater.ReadInputs(File.ReadAllText(SessionPath));
            InputsMetaData metaData = InputTreater.InputTreater.readMetaData(File.ReadAllText(SessionPath + "_metadata"));
            foreach (unTreatedInput input in untreatedInputs)
            {
                //Debug.WriteLine("UntreatedActionContent: " + input.ActionContent);
            }
            Input[] treatedInputs = InputTreater.InputTreater.TreatInputs(untreatedInputs, metaData);
            foreach(Input input in treatedInputs)
            {
                //Debug.WriteLine("TreatedActionContent: " + input.ActionContent);
            }
            return false;
            MultipartFormDataContent form = new MultipartFormDataContent();
            var fs = File.OpenRead(SessionPath);
            var stream = new StreamContent(fs);
            form.Add(stream, "sessionData", Path.GetFileName(SessionPath));
            form.Add(new StringContent(GUID), "guid");
            HttpResponseMessage response = await client.PutAsync("/usertext?guid="+GUID, form);

            Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
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
