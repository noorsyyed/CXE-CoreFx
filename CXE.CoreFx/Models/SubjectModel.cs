using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("subject")]
	public partial class SubjectModel : EntityBase
	{
		public SubjectModel() : base("subject") { }
		public SubjectModel(Guid id) : base("subject", id) { }

		[DataverseColumn("subjectid")]
		public Guid SubjectId
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

		[DataverseColumn("title")]
		public string Title
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("parentsubject")]
		public EntityReference ParentSubject
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}
	}
}
