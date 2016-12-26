using System;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace CoreCRM.IntegrationTest
{
    public abstract class AbstractIntegrationTest<TStartup> : IClassFixture<TestFixture<TStartup>>, IDisposable where TStartup : class
    {
        protected readonly HttpClient _client;
        protected readonly TestServer _server;

        // here we get our testServerFixture, also see above IClassFixture.
        protected AbstractIntegrationTest(TestFixture<TStartup> fixture)
        {
            _client = fixture.Client;
            _server = fixture.Server;
        }
        public virtual void Dispose()
        {
            // Do cleanup
        }
        
        public HttpRequestMessage CreatePostRequest(String path, Dictionary<string, string> formPostBodyData)
        {
	        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, path)
	        {
		        Content = new FormUrlEncodedContent(ToFormPostData(formPostBodyData))
	        };
	        return httpRequestMessage;
	    }

	    public List<KeyValuePair<string, string>> ToFormPostData(Dictionary<string, string> formPostBodyData)
	    {
		    List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
		    formPostBodyData.Keys.ToList().ForEach(key =>
		    {
			    result.Add(new KeyValuePair<string, string>(key, formPostBodyData[key]));
		    });
		    return result;
	    }

	    public HttpRequestMessage CreateWithCookiesFromResponse(string path, Dictionary<string, string> formPostBodyData, HttpResponseMessage response)
	    {
	        var httpRequestMessage = CreatePostRequest(path, formPostBodyData);
		    return CookiesHelper.CopyCookiesFromResponse(httpRequestMessage, response);
	    }

		public string ExtractAntiForgeryToken(string htmlResponseText)
		{
			if (htmlResponseText == null) throw new ArgumentNullException("htmlResponseText");

			Match match = Regex.Match(htmlResponseText, @"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");
			return match.Success ? match.Groups[1].Captures[0].Value : null;
		}

		public async Task<string> ExtractAntiForgeryToken(HttpResponseMessage response)
		{
			string responseAsString = await response.Content.ReadAsStringAsync();
			return await Task.FromResult(ExtractAntiForgeryToken(responseAsString));
		}
    }
}
