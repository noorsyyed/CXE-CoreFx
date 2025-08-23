
using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("clmbus_securityroleprofile_entity")]
	public partial class SecurityRoleProfileModel : EntityBase
	{
		public SecurityRoleProfileModel() : base("clmbus_securityroleprofile_entity") { }
		public SecurityRoleProfileModel(Guid id) : base("clmbus_securityroleprofile_entity", id) { }

		[DataverseColumn("clmbus_securityroleprofile_entityid")]
		public Guid SecurityRoleProfileId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("clmbus_title_string")]
		public string Title
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("clmbus_securityroles_text")]
		public string SecurityRoles
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("clmbus_removeroles_boolean")]
		public bool RemoveRoles
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}
	}
}
