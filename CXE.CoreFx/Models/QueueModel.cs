

using System;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("queue")]
	public partial class QueueModel : EntityBase
	{
		public QueueModel() : base("queue") { }
		public QueueModel(Guid id) : base("queue", id) { }

		[DataverseColumn("queueid")]
		public Guid QueueId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}
	}
}