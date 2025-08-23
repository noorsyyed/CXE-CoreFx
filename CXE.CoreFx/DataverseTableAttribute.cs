using System;

namespace CXE.CoreFx.Base
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class DataverseTableAttribute : Attribute
	{
		public string Name
		{
			get;
			set;
		}

		public DataverseTableAttribute(string name)
		{
			Name = name;
		}
	}
}
