using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Helpers
{
	public enum HttpMethod
	{
		GET = 1,
		POST = 2,
		PUT = 3,
		DELETE =4
	}

	public static class Util
	{
		public async static Task<HttpResponseMessage> DoRequest(HttpMethod httpMethod, string route, Object payload)
		{
			HttpResponseMessage response = new HttpResponseMessage();
			var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

			using(HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:58618/");
				client.Timeout = TimeSpan.FromMinutes(20);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				switch(httpMethod)
				{
					case HttpMethod.GET:
						response = await client.GetAsync(route);
						break;
					case HttpMethod.POST:
						response = await client.PostAsync(route, content);
						break;
					case HttpMethod.PUT:
						response = await client.PutAsync(route, content);
						break;
					case HttpMethod.DELETE:
						response = await client.DeleteAsync(route);
						break;

					default:
						break;
				}
			}

			return response;
		}
	}
}
