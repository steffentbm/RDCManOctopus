using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RDCManOctopus.Rdg;
using RDCManOctopus.Repositories;
using RemoteDesktopConnectionManagerOctopus.Entities;
using Environment = RemoteDesktopConnectionManagerOctopus.Entities.Environment;

namespace RDCManOctopus
{
	internal static class Program
	{
		private static IConfigurationRoot Configuration { get; set; }

		private static void Main()
		{
			BuildConfiguration();

			var repositories = new RepositoryCollection();
			repositories.AddRepository(new WebRepository(Configuration["WebRepository:HostName"]));
			repositories.AddRepository(new FileRepository(Configuration["FileRepository:DataPath"]));

			var environmentsJson = repositories.GetEnvironments();
			var machinesJson = repositories.GetMachines();

			var success = false;
			try
			{
				var environments = JsonConvert.DeserializeObject<IList<Environment>>(environmentsJson);
				var machines = JsonConvert.DeserializeObject<IList<Machine>>(machinesJson);

				var xml = RdgBuilder.BuildXml(Configuration["Name"], environments, machines);

				File.WriteAllText($"{Configuration["Name"]}.rdg", xml, Encoding.UTF8);
				success = true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine("File creation {0}", success ? "successful!" : "failed.");

			Console.WriteLine("Press any key to exit...");
			Console.ReadLine();
		}

		private static void BuildConfiguration()
		{
			var configurationBuilder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			Configuration = configurationBuilder.Build();
		}
	}
}