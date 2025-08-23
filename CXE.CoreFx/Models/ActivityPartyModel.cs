

using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("activityparty")]
	public partial class ActivityPartyModel : EntityBase
	{
		public ActivityPartyModel() : base("activityparty") { }
		public ActivityPartyModel(Guid id) : base("activityparty", id) { }

		[DataverseColumn("activitypartyid")]
		public Guid ActivityPartyId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("activitypointer")]
		public EntityReference ActivityPointer
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("instancetypecode")]
		public int InstanceTypeCode
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		[DataverseColumn("ispartydeleted")]
		public bool IsPartyDeleted
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		[DataverseColumn("participationtypemask")]
		public int ParticipationTypeMask
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		[DataverseColumn("partyid")]
		public EntityReference PartyId
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("addressused")]
		public string AddressUsed
		{
			get => GetValue<string>();
			set => SetValue(value);
		}
	}
}