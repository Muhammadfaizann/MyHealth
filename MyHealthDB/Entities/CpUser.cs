using System;

namespace MyHealthDB
{
	public class CpUser : DBEntityBase
	{
		public CpUser ()
		{
		}

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

