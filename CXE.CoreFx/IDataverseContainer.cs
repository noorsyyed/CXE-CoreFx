using System;
using System.Collections.Generic;
using System.Text;

namespace CXE.CoreFx
{
	public interface IDataverseContainer
	{
		IDataverseContainer AddScoped<TService>();
		IDataverseContainer AddTransient<TService>();
		IDataverseContainer AddSingleton<TService>();
	}
}
