

using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("queueitem")]
	public partial class QueueItemModel : EntityBase
	{
		public QueueItemModel() : base("queueitem") { }
		public QueueItemModel(Guid id) : base("queueitem", id) { }

		[DataverseColumn("queueitemid")]
		public Guid QueueItemId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("objectid")]
		public EntityReference ObjectId
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}
	}
}