using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("usersettings")]
	public partial class SystemUserSettingModel : EntityBase
	{
		public SystemUserSettingModel() : base("usersettings") { }
		public SystemUserSettingModel(Guid id) : base("usersettings", id) { }

		[DataverseColumn("usersettingsid")]
		public Guid UserSettingsId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("localeid")]
		public int LocaleId
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		[DataverseColumn("timezonecode")]
		public int TimeZoneCode
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		[DataverseColumn("systemuserid")]
		public EntityReference SystemUser
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}
	}
}