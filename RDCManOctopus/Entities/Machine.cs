using System;
using System.Collections.Generic;

namespace RemoteDesktopConnectionManagerOctopus.Entities
{
	public class Machine
	{
		public string Id;
		public string Name;
		public Uri Uri;
		public bool IsDisabled;
		public IList<string> EnvironmentIds;
		public IList<string> Roles;

		public string Status;
		public string HealthStatus;
	}
}