

using System;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("quote")]
	public partial class QuoteModel : EntityBase
	{
		public QuoteModel() : base("quote") { }
		public QuoteModel(Guid id) : base("quote", id) { }

		[DataverseColumn("quoteid")]
		public Guid QuoteId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}
	}
}