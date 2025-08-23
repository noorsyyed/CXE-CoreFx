

using System;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("contact")]
	public partial class ContactModel : EntityBase
	{
		public ContactModel() : base("contact") { }
		public ContactModel(Guid id) : base("contact", id) { }

		[DataverseColumn("contactid")]
		public Guid ContactId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("firstname")]
		public string FirstName
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("middlename")]
		public string MiddleName
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("lastname")]
		public string LastName
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("fullname")]
		public string FullName
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("emailaddress1")]
		public string EmailAddress
		{
			get => GetValue<string>();
			set => SetValue(value);
		}
	}
}