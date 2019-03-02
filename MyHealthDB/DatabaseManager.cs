using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace MyHealthDB
{
	public class DatabaseManager //: SQLiteAsyncConnection
	{
		public static string DatabaseFilePath {
			get {
				var sqliteFilename = "MyHealthDB.db3";

				#if SILVERLIGHT
				var path = sqliteFilename;
				#else

				#if __ANDROID__
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				System.IO.Directory.CreateDirectory(libraryPath);

				#else

				string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				string libraryPath = Path.Combine (documentsPath, "..", "Library");

				#endif

				var path = Path.Combine(libraryPath, sqliteFilename);

				#endif

				return path;
			}
		}

		private static SQLiteAsyncConnection conn;
		public static SQLiteAsyncConnection dbConnection {
			get {
				if (conn == null)
					conn = new SQLiteAsyncConnection (DatabaseFilePath, storeDateTimeAsTicks: false);
				return conn;
			}
		}

		public static Task CreateAllTables()
		{
            //var db = new SQLiteAsyncConnection(DatabaseRepository.DatabaseFilePath);

            return Task.WhenAll(
                dbConnection.CreateTableAsync<RegisteredDevice>(),

                dbConnection.CreateTableAsync<Province>(),

                dbConnection.CreateTableAsync<County>(),

                dbConnection.CreateTableAsync<Disease>(),

                dbConnection.CreateTableAsync<DiseaseCategory>(),

                dbConnection.CreateTableAsync<DiseasesForCategory>(),

                dbConnection.CreateTableAsync<EmergencyContacts>(),

                dbConnection.CreateTableAsync<Hospital>(),

                dbConnection.CreateTableAsync<HelpData>(),

                dbConnection.CreateTableAsync<NewsChannels>(),

                dbConnection.CreateTableAsync<Organisation>(),

                dbConnection.CreateTableAsync<UsefullNumbers>(),

                dbConnection.CreateTablesAsync<LogContent, LogExternalLink, LogFeedback, LogUsage>(),

                dbConnection.CreateTablesAsync<CpUser, AboutUs>(),

                dbConnection.CreateTableAsync<ImportantNotice>(),

                dbConnection.CreateTableAsync<VideoLink>(),

                dbConnection.CreateTableAsync<MediaCategory>()
            );
		}

		#region[Register]
		public async static Task<List<RegisteredDevice>> SelectAllDevices()
		{
			var devices = await dbConnection.Table<RegisteredDevice>().ToListAsync ();
			return devices;
		}

		public async static Task<RegisteredDevice> SelectDevice()
		{
			return await dbConnection.Table<RegisteredDevice> ().FirstOrDefaultAsync ();
		}

		public async static Task SaveDevice(RegisteredDevice device)
		{
			var selected = await dbConnection.Table<RegisteredDevice> ().Where(x => x.ID == device.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (device).ContinueWith (t => {
					Console.WriteLine ("New County Name : {0}", device.UserName);
				});
			} else {
				await dbConnection.UpdateAsync (device).ContinueWith (t => {
					Console.WriteLine ("Updated County Name : {0}", device.UserName);
				});
			}
		}

		public async static Task DeleteDevice(RegisteredDevice device)
		{
			await dbConnection.DeleteAsync(device).ContinueWith(t => {
				Console.WriteLine ("New County Name : {0}", device.UserName);
			});
		}
		#endregion

		#region[AboutUs]
//		public async static Task<List<County>> SelectAllCounties()
//		{
//			var counties = await dbConnection.Table<County>().ToListAsync ();
//			return counties;
//		}

		public async static Task<AboutUs> SelectAboutUs(int id)
		{
			return await dbConnection.Table<AboutUs> ().FirstOrDefaultAsync ();//dbConnection.Table<AboutUs> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveAboutUs(AboutUs aboutus)
		{
			var selected = await dbConnection.Table<AboutUs> ().Where(x => x.ID == aboutus.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (aboutus).ContinueWith (t => {
					Console.WriteLine ("Saved New About Us  : {0}", aboutus.Title);
				});
			} else {
				await dbConnection.UpdateAsync (aboutus).ContinueWith (t => {
					Console.WriteLine ("Updated About Us  : {0}", aboutus.Title);
				});
			}
		}

		public async static Task DeleteAboutus(AboutUs aboutus)
		{
			await dbConnection.DeleteAsync(aboutus).ContinueWith(t => {
				Console.WriteLine ("Deleted About Us  : {0}", aboutus.Title);
			});
		}
		#endregion

		#region[Province]
		public async static Task<List<Province>> SelectAllProvinces()
		{
			return await dbConnection.Table<Province> ().OrderBy (t => t.Name).ToListAsync ();
		}

		public async static Task<Province> SelectProvince(int id)
		{
			return await dbConnection.Table<Province> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task<Province> SelectProvince(string name)
		{
			//name = name.Trim ().ToUpper ();
			return await dbConnection.Table<Province> ().Where (c => c.Name == name).FirstOrDefaultAsync ();
		}

		public async static Task SaveProvince(Province province)
		{
			var selected = await dbConnection.Table<Province> ().Where(x => x.ID == province.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (province).ContinueWith (t => {
					Console.WriteLine ("New province Name : {0}", province.Name);
				});
			} else {
				await dbConnection.UpdateAsync (province).ContinueWith (t => {
					Console.WriteLine ("Updated province Name : {0}", province.Name);
				});
			}
		}

		public async static Task DeleteProvince(Province province)
		{
			await dbConnection.DeleteAsync(province).ContinueWith(t => {
				Console.WriteLine ("deleted province Name : {0}", province.Name);
			});
		}
		#endregion

		#region[County]
		public async static Task<List<County>> SelectAllCounties()
		{
			var counties = await dbConnection.Table<County>().OrderBy(t=>t.Name).ToListAsync ();
			return counties;
		}

		public async static Task<County> SelectCounty(int id)
		{
			return await dbConnection.Table<County> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task<List<County>> SelectCountiesByProvince(int provinceId)
		{
			return await dbConnection.Table<County> ().Where (h => h.ProvinceId == provinceId).ToListAsync ();
		}

		public async static Task SaveCounty(County county)
		{
			var selected = await dbConnection.Table<County> ().Where(x => x.ID == county.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (county).ContinueWith (t => {
					Console.WriteLine ("New County Name : {0}", county.Name);
				});
			} else {
				await dbConnection.UpdateAsync (county).ContinueWith (t => {
					Console.WriteLine ("Updated County Name : {0}", county.Name);
				});
			}
		}

		public async static Task DeleteCounty(County county)
		{
			await dbConnection.DeleteAsync(county).ContinueWith(t => {
				Console.WriteLine ("deleted County Name : {0}", county.Name);
			});
		}
		#endregion

		#region[Disease]
		public async static Task<List<Disease>> SelectAllDiseases()
		{
			var diseases = await dbConnection.Table<Disease>().OrderBy(d => d.Name).ToListAsync ();
			return diseases;
		}

		public async static Task<Disease> SelectDisease(int id)
		{
			return await dbConnection.Table<Disease> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveDisease(Disease disease)
		{
			var selected = await dbConnection.Table<Disease> ().Where(x => x.ID == disease.ID).FirstOrDefaultAsync();
			if (selected == null) {
				if (disease.Status != 5) { //for the first time if there is deleted condition then do not insert it.
					await dbConnection.InsertAsync (disease).ContinueWith (t => {
						Console.WriteLine ("New Disease Name : {0}", disease.Name);
					});
				}
			} else {

				if (disease.Status == 5) { //if deleted category is synced then delete it from local
					await dbConnection.DeleteAsync (selected).ContinueWith (t => {
						Console.WriteLine ("Deleted Disease Name : {0}", disease.Name);
					});
				} else {
					await dbConnection.UpdateAsync (disease).ContinueWith (t => {
						Console.WriteLine ("Updated Disease Name : {0}", disease.Name);
					});
				}
			}
		}

		public async static Task DeleteDisease(Disease county)
		{
			await dbConnection.DeleteAsync(county).ContinueWith(t => {
				Console.WriteLine ("New Disease Name : {0}", county.Name);
			});
		}
		#endregion

		#region[DiseaseCategory]
		public async static Task<List<DiseaseCategory>> SelectAllDiseaseCategories()
		{
			var diseaseCategory = await dbConnection.Table<DiseaseCategory>().OrderBy(t=>t.CategoryName).ToListAsync ();
			return diseaseCategory;
		}

		public async static Task<DiseaseCategory> SelectDiseaseCategory(int id)
		{
			return await dbConnection.Table<DiseaseCategory> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveDiseaseCategory(DiseaseCategory category)
		{
			var selected = await dbConnection.Table<DiseaseCategory> ().Where(x => x.ID == category.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (category).ContinueWith (t => {
					Console.WriteLine ("New Disease Name : {0}", category.CategoryName);
				});
			} else {
				await dbConnection.UpdateAsync (category).ContinueWith (t => {
					Console.WriteLine ("Updated Disease Name : {0}", category.CategoryName);
				});
			}
		}

		public async static Task DeleteDiseaseCategory(DiseaseCategory category)
		{
			await dbConnection.DeleteAsync(category).ContinueWith(t => {
				Console.WriteLine ("New Disease Name : {0}", category.CategoryName);
			});
		}
		#endregion

		#region[DiseasesForCategory]
		public async static Task<List<DiseasesForCategory>> SelectAllDiseasesForCategory()
		{
			var diseasesForCategories = await dbConnection.Table<DiseasesForCategory>().ToListAsync ();
			return diseasesForCategories;
		}

		public async static Task<DiseasesForCategory> SelectDiseasesForCategory(int id)
		{
			return await dbConnection.Table<DiseasesForCategory> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveDiseasesForCategory(DiseasesForCategory diseaseForCategory)
		{
			var selected = await dbConnection.Table<DiseasesForCategory> ().Where(x => x.ID == diseaseForCategory.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (diseaseForCategory).ContinueWith (t => {
					Console.WriteLine ("New Disease Name : {0}", diseaseForCategory.CategoryId);
				});
			} else {
				await dbConnection.UpdateAsync (diseaseForCategory).ContinueWith (t => {
					Console.WriteLine ("Updated Disease Name : {0}", diseaseForCategory.CategoryId);
				});
			}
		}

		public async static Task DeleteDiseasesForCategory(DiseasesForCategory diseaseForCategory)
		{
			await dbConnection.DeleteAsync(diseaseForCategory).ContinueWith(t => {
				Console.WriteLine ("New Disease Name : {0}", diseaseForCategory.CategoryId);
			});
		}
		#endregion

		#region[EmergencyContacts]
		public async static Task<List<EmergencyContacts>> SelectAllEmergencyContacts()
		{
			var contacts = await dbConnection.Table<EmergencyContacts>().OrderBy(t=>t.Name).ToListAsync ();
			return contacts;
		}

		public async static Task<EmergencyContacts> SelectEmergencyContacts(int id)
		{
			return await dbConnection.Table<EmergencyContacts> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveEmergencyContacts(EmergencyContacts contacts)
		{
			var selected = await dbConnection.Table<EmergencyContacts> ().Where(x => x.ID == contacts.ID).FirstOrDefaultAsync();
			if (selected == null) {
				if (contacts.isArchived != true) {
					await dbConnection.InsertAsync (contacts).ContinueWith (t => {
						Console.WriteLine ("New Contact Name : {0}", contacts.Name);
					});
				}
			} else {
				if (contacts.isArchived == true) {
					await dbConnection.DeleteAsync (selected).ContinueWith (t => {
						Console.WriteLine ("Delete Contact Name : {0}", contacts.Name);
					});
				} else {
					await dbConnection.UpdateAsync (contacts).ContinueWith (t => {
						Console.WriteLine ("Updated Contact Name : {0}", contacts.Name);
					});
				}
			}
		}

		public async static Task DeleteEmergencyContacts(EmergencyContacts contacts)
		{
			await dbConnection.DeleteAsync(contacts).ContinueWith(t => {
				Console.WriteLine ("New Disease Name : {0}", contacts.Name);
			});
		}
		#endregion

		#region[HelpData]
		public async static Task<List<HelpData>> SelectAllHelpData()
		{
			var counties = await dbConnection.Table<HelpData>().OrderBy(t=>t.Name).ToListAsync ();
			return counties;
		}

		public async static Task<HelpData> SelectHelpData(int id)
		{
			return await dbConnection.Table<HelpData> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveHelpData(HelpData data)
		{
			var selected = await dbConnection.Table<HelpData> ().Where(x => x.ID == data.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (data).ContinueWith (t => {
					Console.WriteLine ("New Disease Name : {0}", data.Name);
				});
			} else {
				await dbConnection.UpdateAsync (data).ContinueWith (t => {
					Console.WriteLine ("Updated Disease Name : {0}", data.Name);
				});
			}
		}

		public async static Task DeleteHelpData(HelpData data)
		{
			await dbConnection.DeleteAsync(data).ContinueWith(t => {
				Console.WriteLine ("New Disease Name : {0}", data.Name);
			});
		}
		#endregion

		#region[Hospital]
		public async static Task<List<Hospital>> SelectAllHospitals()
		{
			var counties = await dbConnection.Table<Hospital>().OrderBy(t=>t.Name).ToListAsync ();
			return counties;
		}

		public async static Task<Hospital> SelectHospital(int id)
		{
			return await dbConnection.Table<Hospital> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveHospital(Hospital hospital)
		{
			var selected = await dbConnection.Table<Hospital> ().Where(x => x.ID == hospital.ID).FirstOrDefaultAsync();
			if (selected == null) {
				if (hospital.isArchived != true) {
					await dbConnection.InsertAsync (hospital).ContinueWith (t => {
						Console.WriteLine ("New Disease Name : {0}", hospital.Name);
					});
				}
			} else {
				if (hospital.isArchived == true) {
					await dbConnection.DeleteAsync (selected).ContinueWith (t => {
						Console.WriteLine ("Delete Disease Name : {0}", hospital.Name);
					});
				} else {
					await dbConnection.UpdateAsync (hospital).ContinueWith (t => {
						Console.WriteLine ("Updated Disease Name : {0}", hospital.Name);
					});
				}
			}
		}

		public async static Task DeleteHospital(Hospital hospital)
		{
			await dbConnection.DeleteAsync(hospital).ContinueWith(t => {
				Console.WriteLine ("New Disease Name : {0}", hospital.Name);
			});
		}

		public async static Task<List<Hospital>> SelectHospitalsByCounty(int countyId)
		{
			return await dbConnection.Table<Hospital> ().Where (c => c.CountyID == countyId).OrderBy(t=>t.Name).ToListAsync ();
		}

		/// <summary>
		/// Selects the hospitals by province.
		/// </summary>
		/// <returns>The hospitals by province.</returns>
		/// <param name="provinceId">Province identifier.</param>
		public async static Task<List<Hospital>> SelectHospitalsByProvince(int provinceId)
		{
			// get counties in the province
			var counties = await SelectCountiesByProvince (provinceId);
			// read there ids only
			var countyIds = counties.Select (c => c.ID).ToArray ();
			// get by hospitals by county ids
			return await dbConnection.Table<Hospital> ().Where (c => countyIds.Contains(c.CountyID)).OrderBy(t=>t.Name).ToListAsync ();
		}
		#endregion

		#region[NewsChannels]
		public async static Task<List<NewsChannels>> SelectAllNewsChannels()
		{
			var counties = await dbConnection.Table<NewsChannels>().OrderBy(t=>t.Name).ToListAsync ();
			return counties;
		}

		public async static Task<NewsChannels> SelectNewsChannel(int id)
		{
			return await dbConnection.Table<NewsChannels> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveNewsChannels(NewsChannels channel)
		{
			var selected = await dbConnection.Table<NewsChannels> ().Where(x => x.ID == channel.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (channel).ContinueWith (t => {
					Console.WriteLine ("New Disease Name : {0}", channel.Name);
				});
			} else {
				await dbConnection.UpdateAsync (channel).ContinueWith (t => {
					Console.WriteLine ("Updated Disease Name : {0}", channel.Name);
				});
			}
		}

		public async static Task DeleteNewsChannels(NewsChannels channel)
		{
			await dbConnection.DeleteAsync(channel).ContinueWith(t => {
				Console.WriteLine ("New Disease Name : {0}", channel.Name);
			});
		}
		#endregion

		#region[Organisation]
		public async static Task<List<Organisation>> SelectAllOrganisations()
		{
			var organisations = await dbConnection.Table<Organisation>().OrderBy(t=>t.Name).ToListAsync ();
			return organisations;
		}

		public async static Task<Organisation> SelectOrganisation(int id)
		{
			return await dbConnection.Table<Organisation> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveOrganisation(Organisation organisation)
		{
			var selected = await dbConnection.Table<Organisation> ().Where(x => x.ID == organisation.ID).FirstOrDefaultAsync();
			if (selected == null) {
				if (organisation.isArchived != true) {
					await dbConnection.InsertAsync (organisation).ContinueWith (t => {
						Console.WriteLine ("New organisation Name : {0}", organisation.Name);
					});
				}
			} else {
				if (organisation.isArchived == true) {
					await dbConnection.DeleteAsync (selected).ContinueWith (t => {
						Console.WriteLine ("Delete organisation Name : {0}", organisation.Name);
					});
				} else {
					await dbConnection.UpdateAsync (organisation).ContinueWith (t => {
							Console.WriteLine ("Updated organisation Name : {0}", organisation.Name);
					});
				}
			}
		}

		public async static Task DeleteOrganisation(Organisation organisation)
		{
			await dbConnection.DeleteAsync(organisation).ContinueWith(t => {
				Console.WriteLine ("New Disease Name : {0}", organisation.Name);
			});
		}
		#endregion

		#region[UsefullNumbers]
		public async static Task<List<UsefullNumbers>> SelectAllUsefullNumbers()
		{
			var counties = await dbConnection.Table<UsefullNumbers>().ToListAsync ();
			return counties;
		}

		public async static Task<UsefullNumbers> SelectUsefullNumber(int id)
		{
			return await dbConnection.Table<UsefullNumbers> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveUsefullNumber(UsefullNumbers number)
		{
			var selected = await dbConnection.Table<UsefullNumbers> ().Where(x => x.ID == number.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (number).ContinueWith (t => {
					Console.WriteLine ("New Disease Name : {0}", number.Name);
				});
			} else {
				await dbConnection.UpdateAsync (number).ContinueWith (t => {
					Console.WriteLine ("Updated Disease Name : {0}", number.Name);
				});
			}
		}

		public async static Task DeleteUsefullNumber(UsefullNumbers number)
		{
			await dbConnection.DeleteAsync(number).ContinueWith(t => {
				Console.WriteLine ("New Disease Name : {0}", number.Name);
			});
		}
		#endregion

		#region[ImportantNotice]
		public async static Task<List<ImportantNotice>> SelectAllImportantNotice()
		{
			var counties = await dbConnection.Table<ImportantNotice>().ToListAsync ();
			return counties;
		}

		public async static Task<ImportantNotice> SelectImportantNotice(int id)
		{
			return await dbConnection.Table<ImportantNotice> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task<ImportantNotice> SelectImportantNotice(DateTime date)
		{
			return await dbConnection.Table<ImportantNotice> ().Where (c => date >= c.StartDate && date <= c.EndDate).OrderByDescending(n=>n.StartDate).FirstOrDefaultAsync ();
		}

		public async static Task SaveImportantNotice(ImportantNotice number)
		{
			var selected = await dbConnection.Table<ImportantNotice> ().Where(x => x.ID == number.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (number).ContinueWith (t => {
					Console.WriteLine ("New ImportantNotice Name : {0}", number.Name);
				});
			} else {
				await dbConnection.UpdateAsync (number).ContinueWith (t => {
					Console.WriteLine ("Updated ImportantNotice Name : {0}", number.Name);
				});
			}
		}

        public async static Task DeleteImportantNotice(ImportantNotice number)
		{
			await dbConnection.DeleteAsync(number).ContinueWith(t => {
				Console.WriteLine ("New ImportantNotice Name : {0}", number.Name);
			});
		}
        #endregion

        #region Video Links

        public static Task<List<VideoLink>> GetAllVideoLinksAsync()
        {
            return dbConnection
                .Table<VideoLink>()
                .ToListAsync();
        }

        public static async Task<List<VideoLink>> GetVideoLinksAsync(string categoryId)
        {
            var videoLinks = await GetAllVideoLinksAsync();

            return videoLinks
                .Where(vl => vl.MediaCategoryIds.Split(',').Contains($"{categoryId}"))
                .ToList();
        }

        public async static Task SaveVideoLinkAsync(VideoLink videoLink)
        {
            var existing = await dbConnection
                .Table<VideoLink>()
                .Where(x => x.ID == videoLink.ID)
                .FirstOrDefaultAsync();

            if (existing == null)
            {
                await dbConnection.InsertAsync(videoLink).ContinueWith(t =>
                {
                    Console.WriteLine("Saved new Video link Title: {0}", videoLink.Title);
                });
            }
            else
            {
                await dbConnection.UpdateAsync(videoLink).ContinueWith(t =>
                {
                    Console.WriteLine("Update Video link Title: {0}", videoLink.Title);
                });
            }
        }

        public static Task DeleteVideoLinkAsync(int videoLinkId)
        {
            return dbConnection.FindAsync<VideoLink>(videoLinkId)
                .ContinueWith(top =>
                {
                    if (top.Result != null)
                    {
                        return dbConnection.DeleteAsync(top.Result).ContinueWith(t => {
                            Console.WriteLine("Deleted video link title : {0}", top.Result.Title);
                        });
                    }

                    return Task.FromResult(0);
                });
        }

        #endregion Video Links

        #region Media Categories

        public static Task<List<MediaCategory>> GetAllMediaCategoriesAsync()
        {
            return dbConnection
                .Table<MediaCategory>()
                .OrderBy(c => c.CategoryTitle)
                .ToListAsync();
        }

        public async static Task SaveMediaCategoryAsync(MediaCategory mediaCategory)
        {
            var existing = await dbConnection
                .Table<MediaCategory>()
                .Where(x => x.ID == mediaCategory.ID)
                .FirstOrDefaultAsync();

            if (existing == null)
            {
                await dbConnection.InsertAsync(mediaCategory).ContinueWith(t =>
                {
                    Console.WriteLine("Saved new Media Category title: {0}", mediaCategory.CategoryTitle);
                });

                return;
            }

            await dbConnection.UpdateAsync(mediaCategory).ContinueWith(t =>
            {
                Console.WriteLine("Updated media category title: {0}", mediaCategory.CategoryTitle);
            });
        }

        public static Task DeleteMediaCategoryAsync(int categoryId)
        {
            return dbConnection.FindAsync<MediaCategory>(categoryId)
                .ContinueWith(c =>
                {
                    if (c.Result != null)
                    {
                        return dbConnection.DeleteAsync(c.Result).ContinueWith(ca => Console.WriteLine("Deleted Media Category title: {0}", c.Result.CategoryTitle));
                    }

                    return Task.FromResult(0);
                });
        }

        #endregion Media Categories

        #region[LogContent]
        public async static Task<List<LogContent>> SelectAllLogContent()
		{
			var log = await dbConnection.Table<LogContent>().ToListAsync ();
			return log;
		}

		public async static Task<List<LogContent>> SelectLogContent (int numberOfRecords = 100)
		{
			var log = await dbConnection.Table<LogContent> ().Take (numberOfRecords).ToListAsync ();
			return log;
		}

		public async static Task<LogContent> SelectLogContentList(int id)
		{
			return await dbConnection.Table<LogContent> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveLogContent(LogContent log)
		{
//			var selected = await dbConnection.Table<LogContent> ().Where (x => x.ID == log.ID);	//.FirstOrDefaultAsync();
//			if (selected == null) {
				await dbConnection.InsertAsync (log).ContinueWith (t => {
					Console.WriteLine ("New LogContent Name : {0}", log.ID);
				});
//			} else {
//
//				await dbConnection.UpdateAsync (log).ContinueWith (t => {
//					Console.WriteLine ("Updated LogContent Name : {0}", log.ID);
//				});
//			}
		}

		public async static Task DeleteLogContent(LogContent number)
		{
			await dbConnection.DeleteAsync(number).ContinueWith(t => {
				Console.WriteLine ("LogContent with ID : {0} >> deleted", number.ID);
			});
		}

		public async static Task DeleteLogContentList(List<LogContent> logList)
		{
			try {
				foreach (var log in logList) 
				{
					await dbConnection.DeleteAsync(log).ContinueWith(t => {
						Console.WriteLine ("LogContent with ID : {0} >> deleted", log.ID);
					});
				}
			} catch (Exception ex) {
				Console.WriteLine("Deletion List List<LogContent> was stopped because of [{0}]", ex.ToString());
			}
		}
		#endregion

		#region[LogExternalLink]
		public async static Task<List<LogExternalLink>> SelectAllLogExternalLink()
		{
			var log = await dbConnection.Table<LogExternalLink>().ToListAsync ();
			return log;
		}

		public async static Task<List<LogExternalLink>> SelectLogExternalLinkList (int numberOfRecords = 100)
		{
			var log = await dbConnection.Table<LogExternalLink> ().Take (numberOfRecords).ToListAsync ();
			return log;
		}

		public async static Task<LogExternalLink> SelectLogExternalLink(int id)
		{
			return await dbConnection.Table<LogExternalLink> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveLogExternalLink(LogExternalLink log)
		{
//			var selected = await dbConnection.Table<LogExternalLink> ().Where(x => x.ID == log.ID).FirstOrDefaultAsync();
//			if (selected == null) {
				await dbConnection.InsertAsync (log).ContinueWith (t => {
					Console.WriteLine ("New LogExternalLink Name : {0}", log.ID);
				});
//			} else {
//				 
//				await dbConnection.UpdateAsync (log).ContinueWith (t => {
//					Console.WriteLine ("Updated LogExternalLink Name : {0}", log.ID);
//				});
//			}
		}

		public async static Task DeleteLogExternalLink(LogExternalLink number)
		{
			await dbConnection.DeleteAsync(number).ContinueWith(t => {
				Console.WriteLine ("LogExternalLink with ID : {0} >> deleted", number.ID);
			});
		}

		public async static Task DeleteLogExternalLinkList(List<LogExternalLink> logList)
		{
			try {
				foreach (var log in logList) 
				{
					await dbConnection.DeleteAsync(log).ContinueWith(t => {
						Console.WriteLine ("LogContent with ID : {0} >> deleted", log.ID);
					});
				}
			} catch (Exception ex) {
				Console.WriteLine("Deletion List List<LogExternalLink> was stopped because of [{0}]", ex.ToString());
			}
		}

		#endregion

		#region[LogFeedback]
		public static Task<List<LogFeedback>> SelectAllLogFeedback()
		{
            return dbConnection.Table<LogFeedback>().ToListAsync ();
		}

		public static Task<List<LogFeedback>> SelectLogFeedbackList (int numberOfRecords = 100)
		{
			return dbConnection.Table<LogFeedback> ().Take (numberOfRecords).ToListAsync ();
		}

		public static Task<LogFeedback> SelectLogFeedback(int id)
		{
			return dbConnection.Table<LogFeedback> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public static Task SaveLogFeedback(LogFeedback log)
		{
            //			var selected = await dbConnection.Table<LogFeedback> ().Where(x => x.ID == log.ID).FirstOrDefaultAsync();
            //			if (selected == null) {
            return dbConnection.InsertAsync (log).ContinueWith (t => {
				Console.WriteLine ("New LogFeedback Name : {0}", log.ID);
			});
//			} else {
//				await dbConnection.UpdateAsync (log).ContinueWith (t => {
//					Console.WriteLine ("Updated LogExternalLink Name : {0}", log.ID);
//				});
//			}
		}

		public static Task DeleteLogFeedback(LogFeedback number)
		{
            return dbConnection.DeleteAsync(number).ContinueWith(t => {
				Console.WriteLine ("LogExternalLink with ID : {0} >> deleted", number.ID);
			});
		}

		public async static Task DeleteLogFeedbackList(List<LogFeedback> logList)
		{
			try {
				foreach (var log in logList) 
				{
					await dbConnection.DeleteAsync(log).ContinueWith(t => {
						Console.WriteLine ("LogContent with ID : {0} >> deleted", log.ID);
					});
				}
			} catch (Exception ex) {
				Console.WriteLine("Deletion List List<LogFeedback> was stopped because of [{0}]", ex.ToString());
			}
		}
		#endregion

		#region[LogUsage]
		public static Task<List<LogUsage>> SelectAllLogUsage()
		{
            return dbConnection.Table<LogUsage>().ToListAsync ();
		}

		public static Task<List<LogUsage>> SelectLogUsageList (int numberOfRecords = 100)
		{
            return dbConnection.Table<LogUsage> ().Take (numberOfRecords).ToListAsync ();
		}

		public static Task<LogUsage> SelectLogUsage(int id)
		{
			return dbConnection.Table<LogUsage> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public static Task SaveLogUsage(LogUsage log)
		{
            //			var selected = await dbConnection.Table<LogUsage> ().Where(x => x.ID == log.ID).FirstOrDefaultAsync();
            //			if (selected == null) {
            return dbConnection.InsertAsync (log).ContinueWith (t => {
					Console.WriteLine ("New LogExternalLink Name : {0}", log.ID);
				});
//			} else {
//				 
//				await dbConnection.UpdateAsync (log).ContinueWith (t => {
//					Console.WriteLine ("Updated LogExternalLink Name : {0}", log.ID);
//				});
//			}
		}

		public static Task DeleteLogUsage(LogUsage number)
		{
            return dbConnection.DeleteAsync(number).ContinueWith(t => {
				Console.WriteLine ("LogExternalLink with ID : {0} >> deleted", number.ID);
			});
		}

		public async static Task DeleteLogUsageList(List<LogUsage> logList)
		{
			try {
				foreach (var log in logList) 
				{
					await dbConnection.DeleteAsync(log).ContinueWith(t => {
						Console.WriteLine ("LogContent with ID : {0} >> deleted", log.ID);
					});
				}
			} catch (Exception ex) {
				Console.WriteLine("Deletion List List<LogFeedback> was stopped because of [{0}]", ex.ToString());
			}
		}
		#endregion

		#region[CpUser]
		public static Task<List<CpUser>> SelectAllCpUser()
		{
            return dbConnection.Table<CpUser>().ToListAsync ();
		}

		public static Task<List<CpUser>> SelectCpUserList (int numberOfRecords = 100)
		{
            return dbConnection.Table<CpUser> ().Take (numberOfRecords).ToListAsync ();
		}

		public static Task<CpUser> SelectCpUser(int id)
		{
			return dbConnection.Table<CpUser> ().Where (c => c.ID == id).FirstOrDefaultAsync ();
		}

		public async static Task SaveCpUser(CpUser user)
		{
			var selected = await dbConnection.Table<CpUser> ().Where(x => x.ID == user.ID).FirstOrDefaultAsync();
			if (selected == null) {
				await dbConnection.InsertAsync (user).ContinueWith (t => {
					Console.WriteLine ("New CpUser Name : {0}", user.ID);
				});
			} else {
				await dbConnection.UpdateAsync (user).ContinueWith (t => {
					Console.WriteLine ("Updated CpUser Name : {0}", user.ID);
				});
			}
		}

		public static Task DeleteCpUser(CpUser user)
		{
            return dbConnection.DeleteAsync(user).ContinueWith(t => {
				Console.WriteLine ("CpUser with ID : {0} >> deleted", user.ID);
			});
		}

		public async static Task DeleteCpUserList(List<CpUser> userList)
		{
			try {
				foreach (var user in userList) 
				{
					await dbConnection.DeleteAsync(user).ContinueWith(t => {
						Console.WriteLine ("CP User with ID : {0} >> deleted", user.ID);
					});
				}
			} catch (Exception ex) {
				Console.WriteLine("Deletion List List<CpUser> was stopped because of [{0}]", ex.ToString());
			}
		}
		#endregion
//
//		public async Task<List<T>> GetItems<T> () where T : IDBEntity, new () 
//		{
			//lock (locker) {
//				return await Table<T> ().ToListAsync ();
//				return (from i in Table<T> ()
//				        select i).ToList ();
			//}
//		}
//
//		public async Task<T> GetItem<T> (int id) where T : IDBEntity, new() 
//		{
//			return await Table<T> ().Where (x => x.ID == id).FirstOrDefaultAsync ();
//			//return Table<T> ().FirstOrDefault (x => x.ID == id);
//		}
//
//		public async Task<int> SaveItem<T> (T item) where T : IDBEntity, new()
//		{
//			//lock (locker) {
//				if (item.ID >= 0 && Table<T>().Where(x => x.ID == item.ID) != null) {
//					 await UpdateAsync (item);
//					return item.ID.Value;
//				} else {
//					await InsertAsync (item);
//					return item.ID.Value;
//				}
//			//}
//		}
//
//		public async Task<int> DeleteItem<T> (int id) where T : IDBEntity, new() 
//		{
//			//lock (locker) {
//			return await DeleteAsdync<T> (new T () { ID = id });
//			//}
//		}
	}
}

