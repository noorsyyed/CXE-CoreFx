using System;

namespace CXE.CoreFx.Base.Models
{
	[DataverseTable("environmentvariabledefinition")]
	public partial class EnvironmentVariableDefinitionModel : EntityBase
	{
		public EnvironmentVariableDefinitionModel() : base("environmentvariabledefinition") { }
		public EnvironmentVariableDefinitionModel(Guid id) : base("environmentvariabledefinition", id) { }

		[DataverseColumn("environmentvariabledefinitionid")]
		public Guid EnvironmentVariableDefinitionId
		{
			get => GetValue<Guid>();
			set => SetValue(value);
		}

		[DataverseColumn("defaultvalue")]
		public string DefaultValue
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		[DataverseColumn("schemaname")]
		public string SchemaName
		{
			get => GetValue<string>();
			set => SetValue(value);
		}
	}
}