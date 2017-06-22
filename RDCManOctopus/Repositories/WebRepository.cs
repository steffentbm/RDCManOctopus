using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RDCManOctopus.Repositories
{
	public class WebRepository : IRepository
	{
		private readonly string _hostname;

		public WebRepository(string hostname)
		{
			_hostname = hostname;
		}

		public string GetMachines()
		{
			return Task.Run(() => CallUrl($"http://{_hostname}/api/machines/all")).Result;
		}

		public string GetEnvironments()
		{
			return Task.Run(() => CallUrl($"http://{_hostname}/api/environments/all")).Result;
		}

		private static async Task<string> CallUrl(string url)
		{
			using (var handler = new HttpClientHandler { Credentials = CredentialCache.DefaultNetworkCredentials })
			using (var client = new HttpClient(handler))
			using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = HttpMethod.Get, Headers = { { "X-Octopus-ApiKey", Program.Configuration["WebRepository:ApiKey"] } } })
			{
				var response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine($"Successfully read data from {url}");
					return await response.Content.ReadAsStringAsync();
				}

				Console.WriteLine($"[ERROR] WebRepository: Status code {response.StatusCode} when trying to access {url}");
			}
			return null;
		}
	}
}