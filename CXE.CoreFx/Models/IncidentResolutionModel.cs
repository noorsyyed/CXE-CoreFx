using System;
using CXE.CoreFx.Base;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("incidentresolution")]
	public partial class IncidentResolutionModel : EntityBase
	{
		public IncidentResolutionModel() : base("incidentresolution") { }
		public IncidentResolutionModel(Guid id) : base("incidentresolution", id) { }

		[DataverseColumn("activityid")]
		public Guid ActivityId
		{
			get => this.GetValue<Guid>();
			set => this.SetValue(value);
		}

		[DataverseColumn("incidentid")]
		public EntityReference IncidentId
		{
			get => this.GetValue<EntityReference>();
			set => this.SetValue(value);
		}

		[DataverseColumn("subject")]
		public string Subject
		{
			get => this.GetValue<string>();
			set => this.SetValue(value);
		}
	}
}