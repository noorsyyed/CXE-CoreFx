using System.Linq;
using CXE.CoreFx.Plugin.Enums;
using CXE.CoreFx.Base;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Plugin
{
	public class TraceLogger : LoggerBase
	{
		private readonly ITracingService _tracingService;

		public TraceLogger(
			ITracingService tracingService)
		{
			_tracingService = tracingService;
		}

		protected override void WriteLogLine(
			string line)
		{
			_tracingService.Trace(line);
		}

		public string GetStageName(
			int stagecode)
		{
			return stagecode switch
			{
				Stages.PreValidation => "Pre-Validation",
				Stages.PreOperation => "Pre-Operation",
				Stages.CoreOperation => "Platform Core Operation",
				Stages.PostOperation => "Post-Operation",
				_ => string.Empty,
			};
		}

		public void LogContextInformation(
			IPluginExecutionContext context)
		{
			WriteLogLine("//////////////////////////////////////////");
			WriteLogLine("Message:     " + context.MessageName);
			WriteLogLine("Stage:       " + context.Stage + " - " + GetStageName(context.Stage));

			if (context.PrimaryEntityName != "none")
			{
				WriteLogLine("Entity:      " + context.PrimaryEntityName + " [" + context.PrimaryEntityId + "]");
			}
			else
			{
				WriteLogLine("Entity:      n/a");
			}

			if (context.InputParameters.Contains("Target"))
			{
				WriteLogLine("Target Type: " + context.InputParameters["Target"].ToString().Split('.').Last<string>());
			}
			else
			{
				WriteLogLine("Target Type: n/a");
			}

			WriteLogLine("//////////////////////////////////////////");
		}
	}
}
