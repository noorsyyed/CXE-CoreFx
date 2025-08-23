
using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("team")]
	public class TeamModel : EntityBase
	{
		public TeamModel() : base("team") { }
		public TeamModel(Guid id) : base("team", id) { }

		[DataverseColumn("teamid")]
		public Guid TeamId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}
	}
}