using System;

namespace CXE.CoreFx.Base
{
	internal class ConsoleLogger : LoggerBase
	{
		protected override void WriteLogLine(
			string line)
		{
			Console.WriteLine(line);
		}
	}
}
