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

namespace ThyniusService
{
    struct sessionMetaData
    {
        public int WordCount;
        public int CharCount;
    }
    public class ServerComms
    {
        private string cookieDataPath;
        private string baseAddress;
        private Uri baseUri;
        private CookieContainer cookieContainer;
        ILogger<WindowsBackgroundService> _logger;
        public ServerComms(string baseAddress, string CookieDataPath, ILogger<WindowsBackgroundService> logger)
        {
            _logger = logger;
            this.baseAddress = baseAddress;
            cookieDataPath = CookieDataPath;
            baseUri = new Uri(baseAddress);
            cookieContainer = new CookieContainer();
            LoadCookieData(baseAddress);
        }
        public async Task<bool> isLoggedIn()
        {
            LoadCookieData(baseAddress);
            HttpClientHandler clientHandler = new HttpClientHandler { UseCookies = true, CookieContainer = cookieContainer };
            HttpClient client = new HttpClient(clientHandler);
            client.BaseAddress = baseUri;
            var response = await client.GetAsync("/account/getsessioninfo");
            client.Dispose();
            if ((int)response.StatusCode == 204)
            {
                return false;
            }
            return true;

        }

        public async Task<bool> SubmitSession(string GUID, string SessionPath)
        {
            LoadCookieData(baseAddress);
            HttpClientHandler clientHandler = new HttpClientHandler { UseCookies = true, CookieContainer = cookieContainer };
            HttpClient client = new HttpClient(clientHandler);
            client.BaseAddress = baseUri;

            _logger.LogInformation("path: " + SessionPath);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            List<unTreatedInput> untreatedInputs = Utils.ReadInputs(File.ReadAllText(SessionPath, Encoding.GetEncoding(1252)));
            InputsMetaData metaData = Utils.readMetaData(File.ReadAllText(SessionPath + "_metadata"));

            string[] split = SessionPath.Split('/');
            string wantedString = "";
            for(int i = 0; split.Length-1 > i; i++)
            {
                wantedString += split[i];
            }

            Treater treater = new Treater(untreatedInputs, metaData, _logger);
            List<FilteredInput> treatedInputs = treater.getTreatedInputs();
            Utils.WriteInputsCsvString(treatedInputs, SessionPath);

            MultipartFormDataContent form = new MultipartFormDataContent();
            var fs = File.OpenRead(SessionPath + "_result");
            var stream = new StreamContent(fs);
            form.Add(stream, "sessionData", Path.GetFileName(SessionPath));
            form.Add(new StringContent(GUID), "guid");
            HttpResponseMessage response = await client.PutAsync("/usertext?guid=" + GUID, form);
            fs.Dispose();
            stream.Dispose();
            client.Dispose();

            if (response.StatusCode == HttpStatusCode.OK)
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
            catch
            {
                File.Create(cookieDataPath).Close();
                return false;
            }

            try
            {
                string CombinedCookieString = "";
                int CookieCount = cookies.Count();
                for (int i = 0; CookieCount > i; i++)
                {
                    CombinedCookieString += cookies[i];
                    if (i != CookieCount - 1)
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
