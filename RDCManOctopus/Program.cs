using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RemoteDesktopConnectionManagerOctopus.Entities;

namespace RDCManOctopus
{
	internal static class Program
	{
		private static void Main()
		{
			var environmentsJson = File.ReadAllText("./Data/environments.json", Encoding.UTF8);		// From http://octopus.waypoint-toolbox/api/environments/all
			var machinesJson = File.ReadAllText("./Data/machines.json", Encoding.UTF8);				// From http://octopus.waypoint-toolbox/api/machines/all

			var environments = JsonConvert.DeserializeObject<IList<Environment>>(environmentsJson);
			var machines = JsonConvert.DeserializeObject<IList<Machine>>(machinesJson);

			const string name = "Octopus";
			var xml = BuildXml(name, environments, machines);

			File.WriteAllText($"{name}.rdg", xml, Encoding.UTF8);
		}

		private static string BuildXml(string name, IList<Environment> environments, IList<Machine> machines)
		{
			var content = new StringBuilder();
			content.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
			content.AppendLine("<RDCMan programVersion=\"2.7\" schemaVersion=\"3\">");
			content.AppendLine("\t<file>");
			content.AppendLine("\t\t<credentialsProfiles />");
			content.AppendLine("\t\t<properties>");
			content.AppendLine("\t\t\t<expanded>True</expanded>");
			content.AppendLine($"\t\t\t<name>{name}</name>");
			content.AppendLine("\t\t</properties>");

			foreach (var environment in environments)
			{
				content.AppendLine("\t\t<group>");
				content.AppendLine("\t\t\t<properties>");
				content.AppendLine("\t\t\t\t<expanded>False</expanded>");
				content.AppendLine($"\t\t\t\t<name>{environment.Name}</name>");
				content.AppendLine("\t\t\t</properties>");

				foreach (var machine in machines.Where(m => m.EnvironmentIds.Contains(environment.Id)))
				{
					content.AppendLine("\t\t\t<server>");
					content.AppendLine("\t\t\t\t<properties>");
					content.AppendLine($"\t\t\t\t\t<displayName>{machine.Name} ({string.Join(", ", machine.Roles)})</displayName>");
					content.AppendLine($"\t\t\t\t\t<name>{machine.Uri.DnsSafeHost}</name>");
					content.AppendLine("\t\t\t\t</properties>");
					content.AppendLine("\t\t\t</server>");
				}

				content.AppendLine("\t\t</group>");
			}

			content.AppendLine("\t</file>");
			content.AppendLine("\t<connected />");
			content.AppendLine("\t<favorites />");
			content.AppendLine("\t<recentlyUsed />");
			content.AppendLine("</RDCMan>");

			return content.ToString();
		}
	}
}