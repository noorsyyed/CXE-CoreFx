using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("activity")]
	public partial class ActivityModel : EntityBase
	{
		public ActivityModel() : base("activity") { }
		public ActivityModel(Guid id) : base("activity", id) { }

		[DataverseColumn("activityid")]
		public Guid ActivityId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("description")]
		public string Description
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("from")]
		public string From
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("regardingobjectid")]
		public EntityReference RegardingObject
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("subject")]
		public string Subject
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("to")]
		public string To
		{
			get => GetValue<string>();
			set => SetValue(value);
		}
	}
}