using System;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("account")]
	public partial class AccountModel : EntityBase
	{
		public AccountModel() : base("account") { }
		public AccountModel(Guid id) : base("account", id) { }

		[DataverseColumn("accountid")]
		public Guid AccountId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("name")]
		public string Name
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("accountnumber")]
		public string AccountNumber
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("accountcategorycode")]
		public int AccountCategoryCode
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		[DataverseColumn("emailaddress1")]
		public string EmailAddress
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_composite")]
		public string Address1Composite
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_addresstypecode")]
		public int Address1TypeCode
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_line1")]
		public string Address1Line1
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_line2")]
		public string Address1Line2
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_line3")]
		public string Address1Line3
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_city")]
		public string Address1City
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_postalcode")]
		public string Address1ZipCode
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_stateorprovince")]
		public string Address1StateOrProvince
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_country")]
		public string Address1Country
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_latitude")]
		public double? Address1Latitude
		{
			get => GetValue<double?>();
			set => SetValue(value);
		}

		[DataverseColumn("address1_longitude")]
		public double? Address1Longitude
		{
			get => GetValue<double?>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_composite")]
		public string Address2Composite
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_addresstypecode")]
		public int Address2TypeCode
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_line1")]
		public string Address2Line1
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_line2")]
		public string Address2Line2
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_line3")]
		public string Address2Line3
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_city")]
		public string Address2City
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_postalcode")]
		public string Address2ZipCode
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_stateorprovince")]
		public string Address2StateOrProvince
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_country")]
		public string Address2Country
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_latitude")]
		public double? Address2Latitude
		{
			get => GetValue<double?>();
			set => SetValue(value);
		}

		[DataverseColumn("address2_longitude")]
		public double? Address2Longitude
		{
			get => GetValue<double?>();
			set => SetValue(value);
		}
	}
}