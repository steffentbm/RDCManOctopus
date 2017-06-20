using System.Collections.Generic;
using System.Linq;

namespace RDCManOctopus.Repositories
{
	public class RepositoryCollection : IRepository
	{
		private readonly IList<IRepository> _repositories;

		public RepositoryCollection()
		{
			_repositories = new List<IRepository>();
		}

		public RepositoryCollection(IList<IRepository> repositories)
		{
			_repositories = repositories;
		}

		public string GetMachines()
		{
			return _repositories.Select(repository => repository.GetMachines()).FirstOrDefault(result => result != null);
		}

		public string GetEnvironments()
		{
			return _repositories.Select(repository => repository.GetEnvironments()).FirstOrDefault(result => result != null);
		}

		public void AddRepository(IRepository repository)
		{
			_repositories.Add(repository);
		}

		public bool RemoveRepository(IRepository repository)
		{
			return _repositories.Remove(repository);
		}
	}
}