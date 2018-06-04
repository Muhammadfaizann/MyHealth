namespace MyHealthDB
{
    public class RegisteredDevice: DBEntityBase
	{
		public RegisteredDevice()
		{
		}

		//[PrimaryKey]
		//public int ID { get; set; }

		public string UserName { get; set; }
		public string DeviceId { get; set; }
		public string DeviceType { get; set; }
	}
}

