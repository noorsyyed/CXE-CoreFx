using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("systemuser")]
	public partial class SystemUser : EntityBase
	{
		public SystemUser() : base("systemuser") { }
		public SystemUser(Guid id) : base("systemuser", id) { }

		[DataverseColumn("systemuserid")]
		public Guid SystemUserId
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

		[DataverseColumn("internalemailaddress")]
		public string EmailAddress
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("mobilephone")]
		public string MobilePhone
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("businessunitid")]
		public EntityReference BusinessUnit
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("clmbus_securityroleprofile_lookup")]
		public EntityReference SecurityRoleProfile
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("clmbus_refreshsecurityroles_boolean")]
		public bool? RefreshRoles
		{
			get => GetValue<bool?>();
			set => SetValue(value);
		}
	}
}