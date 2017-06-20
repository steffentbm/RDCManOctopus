using System;
using System.IO;
using System.Text;

namespace RDCManOctopus.Repositories
{
	public class FileRepository : IRepository
	{
		private readonly string _path;

		public FileRepository(string path)
		{
			_path = path;
		}

		public string GetMachines()
		{
			return TryReadFile($"{_path}/machines.json");
		}

		public string GetEnvironments()
		{
			return TryReadFile($"{_path}/environments.json");
		}

		private static string TryReadFile(string path)
		{
			try
			{
				var contents = File.ReadAllText(path, Encoding.UTF8);
				Console.WriteLine($"Successfully read data from {path}");
				return contents;
			}
			catch (Exception exception)
			{
				Console.WriteLine("[ERROR] FileRepository: Exception {0}", exception);
			}

			return null;
		}
	}
}