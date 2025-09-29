using System;
using System.Collections.Generic;
using System.Text;

namespace CXE.CoreFx.Enums
{
	public enum Lifetime
	{
		/// <summary>
		/// Type is created at most once per request, and then reused.
		/// </summary>
		Scoped = 1,
		/// <summary>
		/// Type is created at most once per container, and then reused.
		/// </summary>
		Singleton = 0,
		/// <summary>
		/// A new instance of the Type is created every time.
		/// </summary>
		Transient = 2,
	}
}
