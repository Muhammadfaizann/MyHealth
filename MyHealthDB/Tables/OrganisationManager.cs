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
			return null; //DatabaseRepository.GetOrganisation (id);
		}

		public static List<Organisation> GetAllOrganisations ()
		{
			return null; //new List<Organisation> (DatabaseRepository.GetAllOrganisations ());
		}

		public static int SaveOrganisation( Organisation item ) 
		{
			return 0; //DatabaseRepository.SaveOrganisation (item);
		}

		public static int DeleteOrganisation (int id)
		{
			return 0; //DatabaseRepository.DeleteNewsChannels (id);
		}
	}
}

