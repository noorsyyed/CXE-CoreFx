using System;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("lead")]
	public partial class LeadModel : EntityBase
	{
		public LeadModel() : base("lead") { }
		public LeadModel(Guid id) : base("lead", id) { }

		[DataverseColumn("leadid")]
		public Guid LeadId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}
	}
}