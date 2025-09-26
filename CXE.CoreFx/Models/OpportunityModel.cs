

using System;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("opportunity")]
	public partial class OpportunityModel : EntityBase
	{
		public OpportunityModel() : base("opportunity") { }
		public OpportunityModel(Guid id) : base("opportunity", id) { }

		[DataverseColumn("opportunityid")]
		public Guid OpportunityId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}
	}
}