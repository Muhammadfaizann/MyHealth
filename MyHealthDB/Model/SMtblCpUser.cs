using System;

namespace MyHealthDB.Model
{
	public class SMtblCpUser
	{
		public int Id { get; set; }
		public string CharityName { get; set; }
		public string CharityAddress { get; set; }
		public string Website { get; set; }
		public string Email { get; set; }
		public string Number { get; set; }
		public string Fax { get; set; }
		public string Helpline { get; set; }
		public string LinkToDonate { get; set; }
		public byte[] CharityLogo { get; set; }
	}
}

