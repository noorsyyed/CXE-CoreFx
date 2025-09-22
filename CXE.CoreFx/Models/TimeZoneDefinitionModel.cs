

using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("timezonedefinition")]
	public partial class TimeZoneDefinitionModel : EntityBase
	{
		public TimeZoneDefinitionModel() : base("timezonedefinition") { }
		public TimeZoneDefinitionModel(Guid id) : base("timezonedefinition", id) { }

		[DataverseColumn("timezonedefinitionid")]
		public Guid TimeZoneDefinitionId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("standardname")]
		public string StandardName
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("timezonecode")]
		public int TimeZoneCode
		{
			get => GetValue<int>();
			set => SetValue(value);
		}
	}
}