using System;

namespace CXE.CoreFx.Base
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class DataverseColumnAttribute : Attribute
	{
		private string? _logicalName;
		public string? LogicalName
		{
			get => IsReference ? $"_{_logicalName?.ToLower()}_value" : _logicalName;
			set => _logicalName = value;
		}
		public string? SerializedName => IsReference ? $"{_logicalName}@odata.bind" : _logicalName;
		public bool IsReference
		{
			get; set;
		}
		public string? ReferenceTable
		{
			get; set;
		}

		public DataverseColumnAttribute(string? logicalName)
		{
			LogicalName = logicalName;
		}
	}
}
