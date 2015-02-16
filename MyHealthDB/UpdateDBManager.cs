using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyHealthDB.Model;

namespace MyHealthDB
{
	public static class UpdateDBManager
	{
		//-- Create a string which contains information regarding inserted and updated records.
		public async static Task<Boolean> UpdateDiseases (List<SMtblCPCondition> AllConditions)
		{
			foreach (SMtblCPCondition condition in AllConditions) 
			{
				await MyHealthDB.DatabaseManager.SaveDisease (new Disease {
					ID = condition.Id,
					Name = condition.Condition,
					Url = condition.Url,
					Description = condition.Description,
					MisSpelling = condition.MisSpelling,
					PreventiveMeasures = condition.PreventiveMeasures,
					SignAndSymptoms = condition.SignAndSymptoms,
					CPUserId = condition.CPUserId,
				});
			}
			return true;
		}

		public async static Task<Boolean> UpdateCategories (List<SMtblCPCategory> AllCategories)
		{
			foreach (SMtblCPCategory category in AllCategories) 
			{
				await MyHealthDB.DatabaseManager.SaveDiseaseCategory (new DiseaseCategory {
					ID = category.Id,
					CategoryName = category.Category
				});
			}
			return true;
		}

		public async static Task<Boolean> UpdateDiseasesCategories (List<SMConditionCategories> AllCC)
		{
			foreach (SMConditionCategories cc in AllCC) 
			{
				await MyHealthDB.DatabaseManager.SaveDiseasesForCategory (new DiseasesForCategory {
					ID = cc.CategoryId,
					CategoryId = cc.CategoryId,
					ConditionId = string.Join(",", cc.ConditionId)
				});
			}
			return true;
		}

		public async static Task<Boolean> UpdateCounty (List<SMtblCounty> AllCounties)
		{
			foreach (SMtblCounty county in AllCounties) 
			{
				await MyHealthDB.DatabaseManager.SaveCounty (new County {
					ID = county.Id,
					Name = county.Name,
					Description = county.Description
				});
			}
			return true;
		}

		public async static Task<Boolean> UpdateHospitals (List<SMtblHealthHospital> AllHospitals)
		{
			foreach (SMtblHealthHospital hospital in AllHospitals) 
			{
				await MyHealthDB.DatabaseManager.SaveHospital (new Hospital {
					ID = hospital.Id,
					Name = hospital.Name,
					PhoneNumber = hospital.Number,
					URL = hospital.Website,
					CountyID = hospital.countyId
				});
			}
			return true;
		}

		public async static Task<Boolean> UpdateEmergencyNumber (List<SMtblHealthEmergencyNumber> AllNumbers)
		{
			foreach (SMtblHealthEmergencyNumber number in AllNumbers) 
			{
				await MyHealthDB.DatabaseManager.SaveEmergencyContacts (new EmergencyContacts {
					ID = number.Id,
					Name = number.Name,
					PhoneNumber = number.Number,
					Description = number.Description
				});
			}
			return true;
		}

		public async static Task<Boolean> UpdateOrganizations (List<SMtblHealthOrganizationsInfo> AllOrganizations)
		{
			foreach (SMtblHealthOrganizationsInfo organisation in AllOrganizations) 
			{
				await MyHealthDB.DatabaseManager.SaveOrganisation (new Organisation {
					ID = organisation.Id,
					Name = organisation.Name,
					PhoneNumber = organisation.Number,
					URL = organisation.Website
				});
			}
			return true;
		}

		public async static Task<Boolean> UpdateCpUsers (List<SMtblCpUser> AllCPUsers)
		{
			foreach (SMtblCpUser user in AllCPUsers) 
			{
				await MyHealthDB.DatabaseManager.SaveCpUser (new CpUser {
					ID = user.Id,
					CharityName = user.CharityName,
					CharityLogo = user.CharityLogo,
					CharityAddress = user.CharityAddress,
					Email = user.Email,
					Fax = user.Fax,
					Helpline = user.Helpline,
					LinkToDonate = user.LinkToDonate,
					Number = user.Number,
					Website = user.Website,
				});
			}
			return true;
		}
	}
}

