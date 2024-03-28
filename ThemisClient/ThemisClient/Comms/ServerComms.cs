using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ThemisClient.Comms
{
    struct LoginResponse
    {
        public string refreshToken;
        public string accessToken;
        public string tokenType;
        public int expiresIn;
    }
    class ServerComms
    {
        private static HttpClient client;
        private string CookieDataPath;
        public ServerComms(string baseAddress, string CookieDataPath)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            this.CookieDataPath = CookieDataPath;
        }
        public async Task<LoginResponse> LoginAsync(string _email, string _password)
        {
            var body = JsonContent.Create(new { email = _email, password = _password });
            var response = await client.PostAsync("/login?useCookies=true", body);
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<LoginResponse>(contentStream);
        }

        public bool isLoggedIn()
        {
            return true;
        }
        public async void CreateText(string TextTitle, string DataPath)
        {
            var response = await client.PostAsync("/usertext?TextTitle=" + TextTitle, null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> CreateTextSession(string DataPath, int TextId)
        {
            var form = new MultipartFormDataContent();

            var fs = File.OpenRead(DataPath);

            var streamContent = new StreamContent(fs);

            var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            form.Add(fileContent, "file", Path.GetFileName(DataPath));

            HttpResponseMessage response = await client.PostAsync("/textSession?sessionId=" + TextId, form);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream);

            return Convert.ToInt32(reader.ReadToEnd());
        }
    }
}
