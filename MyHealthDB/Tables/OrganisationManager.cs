using System;
using System.Collections.Generic;

namespace MyHealthDB
{
	public class OrganisationManager
	{
		static OrganisationManager ()
		{
		}

		public static Organisation GetOrganisation (int id)
		{
			return DatabaseRepository.GetOrganisation (id);
		}

		public static IList<Organisation> GetAllOrganisations ()
		{
			return new List<Organisation> (DatabaseRepository.GetAllOrganisations ());
		}

		public static int SaveOrganisation( Organisation item ) 
		{
			return DatabaseRepository.SaveOrganisation (item);
		}

		public static int DeleteOrganisation (int id)
		{
			return DatabaseRepository.DeleteNewsChannels (id);
		}
	}
}

