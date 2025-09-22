using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("teammembership")]
	public partial class TeamMembershipModel : EntityBase
	{
		public TeamMembershipModel() : base("teammembership") { }
		public TeamMembershipModel(Guid id) : base("teammembership", id) { }

		[DataverseColumn("teammembershipid")]
		public Guid TeamMembershipId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}
	}
}}