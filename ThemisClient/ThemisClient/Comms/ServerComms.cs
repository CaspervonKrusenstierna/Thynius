using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
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
        public ServerComms(string baseAddress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
        }
        public async Task<LoginResponse> LoginAsync(string _email, string _password)
        {
            var body = JsonContent.Create(new {email = _email, password = _password });
            var response = await client.PostAsync("/login?useCookies=true", body);
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(contentStream);
        } 
        public async void CreateText(string TextTitle, string DataPath)
        {
            var response = await client.PostAsync("/usertext?TextTitle="+TextTitle, null);
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
        public async void UpdateTextSession(string DataPath, int SessionId)
        {
            var form = new MultipartFormDataContent();

            var fs = File.OpenRead(DataPath);

            var streamContent = new StreamContent(fs);

            var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            form.Add(fileContent, "file", Path.GetFileName(DataPath));

            HttpResponseMessage response = await client.PutAsync("/textSession?sessionId=" + SessionId, form);

            response.EnsureSuccessStatusCode();
        }
    }
}
