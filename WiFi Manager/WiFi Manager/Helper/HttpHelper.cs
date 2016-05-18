using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Tsing.Helper
{
    class HttpHelper
    {
        private HttpClient httpclient;
        private Uri host;

        public HttpHelper(Uri Host)
        {
            httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Add(
                "user-agent",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            httpclient.DefaultRequestHeaders.Add(
                "enctype",
                "multipart/form-data");
            host = Host;
        }

        public async Task<string> DoGet(string Uri)
        {
            string responseText = string.Empty;
            HttpResponseMessage response = await httpclient.GetAsync(new Uri(host.ToString() + Uri));
            responseText = await response.Content.ReadAsStringAsync();
            return responseText;
        }

        public async Task<string> DoPost(string Uri, List<KeyValuePair<string, string>> PostContent)
        {
            string responseText = string.Empty;
            HttpResponseMessage response = await httpclient.PostAsync(new Uri(host.ToString() + Uri), new HttpFormUrlEncodedContent(PostContent));
            responseText = await response.Content.ReadAsStringAsync();
            return responseText;
        }
        public async Task<string> DoPost(string Uri, string PostContent)
        {
            string responseText = string.Empty;
            HttpResponseMessage response = await httpclient.PostAsync(new Uri(host.ToString() + Uri), new HttpStringContent(PostContent));
            responseText = await response.Content.ReadAsStringAsync();
            return responseText;
        }
    }
}
