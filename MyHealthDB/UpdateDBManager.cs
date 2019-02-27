using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyHealthDB.Model;
using System.Linq;

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
					Status = condition.ApproveStatus??1
				});
			}
			return true;
		}

		public async static Task<Boolean>UpdateAboutUs(AboutUs info) 
		{
			await MyHealthDB.DatabaseManager.SaveAboutUs (info);
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

		public async static Task<Boolean> UpdateProvince (List<SMtblProvince> AllProvinces)
		{
			foreach (SMtblProvince province in AllProvinces) 
			{
				await MyHealthDB.DatabaseManager.SaveProvince (new Province {
					ID = province.Id,
					Name = province.Name
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
					Description = county.Description,
					ProvinceId = county.ProvinceId
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
					PhoneNumber = hospital.Number.ToString(),
					URL = hospital.Website,
					CountyID = hospital.countyId,
					isArchived = hospital.isArchived
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
					PhoneNumber = number.Number.ToString(),
					Description = number.Description,
					isArchived = number.isArchived
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
					PhoneNumber = organisation.Number.ToString(),
					URL = organisation.Website,
					isArchived = organisation.isArchived
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

		public async static Task<Boolean> UpdateImportantNotices (List<SMtblHealthImportantNotice> allImportantNotices)
		{
			foreach (SMtblHealthImportantNotice importantNotice in allImportantNotices) 
			{
				await MyHealthDB.DatabaseManager.SaveImportantNotice (new ImportantNotice {
					ID = importantNotice.Id,
					Name = importantNotice.Name,
					StartDate = importantNotice.StartDate,
					EndDate = importantNotice.EndDate,
					LastUpdatedDate = importantNotice.LastUpdatedDate,
					isArchived = importantNotice.isArchived,
					NoticeColor = importantNotice.NoticeColor
				});
			}
			return true;
		}

        public static Task<bool> UpdateVideoLinks(List<SMtblVideoLink> videoLinks)
        {
            List<Task> tasks = new List<Task>();

            foreach (var item in videoLinks)
            {
                if (item.IsDeleted)
                {
                    tasks.Add(
                        DatabaseManager.DeleteVideoLinkAsync(item.Id)
                        );
                }
                else
                {
                    tasks.Add(
                        DatabaseManager.SaveVideoLinkAsync(new VideoLink
                        {
                            ID = item.Id,
                            Title = item.Title,
                            Description = item.Description,
                            UrlDisplayName = item.UrlDisplayName,
                            Url = item.Url,
                            IsDeleted = item.IsDeleted,

                            MediaCategoryIds = string.Join(",", item.MediaCategoryIds),
                        })
                    );
                }
            }

            return Task.WhenAll(tasks).ContinueWith(_ => _.IsCompleted);
        }
    }
}

