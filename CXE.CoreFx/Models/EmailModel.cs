using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("email")]
	public partial class EmailModel : EntityBase
	{
		public EmailModel() : base("email") { }
		public EmailModel(Guid id) : base("email", id) { }

		[DataverseColumn("activityid")]
		public Guid ActivityId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("sender")]
		public string Sender
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("from")]
		public string SenderCollection
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("torecipients")]
		public string Recipient
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("to")]
		public string RecipientCollection
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("regardingobjectid")]
		public EntityReference Regarding
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

		[DataverseColumn("description")]
		public string EmailBody
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("directioncode")]
		public int Direction
		{
			get => GetValue<int>();
			set => SetValue(value);
		}
	}
}