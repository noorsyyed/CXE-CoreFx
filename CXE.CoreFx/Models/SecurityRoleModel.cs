
using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("role")]
	public partial class SecurityRoleModel : EntityBase
	{
		public SecurityRoleModel() : base("role") { }
		public SecurityRoleModel(Guid id) : base("role", id) { }

		[DataverseColumn("roleid")]
		public Guid RoleId
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

		[DataverseColumn("businessunitid")]
		public EntityReference BusinessUnit
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("parentroleid")]
		public EntityReference ParentSecurityRole
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}
	}
}
