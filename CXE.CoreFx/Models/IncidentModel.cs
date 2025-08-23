// ============================================================================
// ============================================================================
// ============================================================================
using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("incident")]
	public partial class IncidentModel : EntityBase
	{
		public IncidentModel() : base("incident") { }
		public IncidentModel(Guid id) : base("incident", id) { }

		[DataverseColumn("incidentid")]
		public Guid IncidentId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("caseorigincode")]
		public int CaseOrigin
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		[DataverseColumn("transactioncurrencyid")]
		public EntityReference Currency
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("customerid")]
		public EntityReference Customer
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("description")]
		public string Description
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("emailaddress")]
		public string EmailAddress
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("firstresponsesent")]
		public bool? IsFirstResponseSent
		{
			get => GetValue<bool?>();
			set => SetValue(value);
		}

		[DataverseColumn("parentcaseid")]
		public EntityReference ParentCase
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("primarycontactid")]
		public EntityReference PrimaryContact
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}

		[DataverseColumn("ticketnumber")]
		public string TicketNumber
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

		[DataverseColumn("subjectid")]
		public EntityReference Subject
		{
			get => GetValue<EntityReference>();
			set => SetValue(value);
		}
	}
}