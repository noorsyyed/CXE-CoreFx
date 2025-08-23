using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Com.Columbus.D365.Dataverse.Toolbox.Enums;
using Microsoft.Xrm.Sdk;



// ============================================================================
// ============================================================================
// ============================================================================
namespace Com.Columbus.D365.Dataverse.Toolbox
{


	// ============================================================================
	// ============================================================================
	// ============================================================================
	public class PluginLogger : Logger
	{

		private bool _enabled = true;
		private bool _isDelayedLogging = false;
		private ITracingService _tracer = null;
		private LogLevel _logLevel;
		private int _indentSize;
		private string _indentString;
		private int _indentLevel;
		private bool _displayTimestamp;
		private string _timestampFormat;
		private string _logFile;
		private List<string> _logLines;
		private LogType _logType;


		public const string SeparationLine = ":::::::::::::::::::::::::::::::::::::::";



		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		public bool Enabled
		{
			get => _enabled;
			set => _enabled = value;
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		public LogType LogType
		{
			get => _logType;
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		public string LogFile
		{
			get => _logFile;
			set
			{
				_logFile = value;
				_logType = LogType.File;
			}
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		public bool IsDelayedLogging
		{
			get => _isDelayedLogging;
			set
			{
				_isDelayedLogging = value;

				if (!_isDelayedLogging)
				{
					PushDelayedLogs();
				}
			}
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		public LogLevel LogLevel
		{
			get => _logLevel;
			set => _logLevel = value;
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		public bool DisplayTimestamp
		{
			get => _displayTimestamp;
			set => _displayTimestamp = value;
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		public string TimestampFormat
		{
			get => _timestampFormat;
			set => _timestampFormat = value;
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		public int IndentSize
		{
			get => _indentSize;

			set
			{
				_indentSize = value;

				if (_indentSize < 1)
				{
					_indentSize = 1;
				}

				GenerateIndent();
			}
		}


		// ============================================================================
		/// <summary>
		/// Initiates a Logger that is logging to the Tracing-Service.
		/// </summary>
		/// <param name="tracingService">The tracing service.</param>
		/// <param name="logLevel">The logging level</param>
		public PluginLogger(
			ITracingService tracingService,
			LogLevel logLevel = LogLevel.INFO)
		{
			_tracer = tracingService;
			_logLevel = logLevel;

			_enabled = true;
			_logType = LogType.TraceLog;
			_displayTimestamp = false;
			_timestampFormat = "yyyy-MM-dd HH:mm:ss.fff";

			_indentSize = 4;
			_indentLevel = 0;

			_logLines = new List<string>();

			GenerateIndent();
		}



		// ============================================================================
		public string GetStageName(
			int stagecode)
		{
			switch (stagecode)
			{
				case Stages.PreValidation:
					return "Pre-Validation";

				case Stages.PreOperation:
					return "Pre-Operation";

				case Stages.CoreOperation:
					return "Platform Core Operation";

				case Stages.PostOperation:
					return "Post-Operation";

			}

			return string.Empty;
		}


		// ============================================================================
		/// <summary>
		/// Logs an empty line
		/// </summary>
		public void Log()
		{
			Log("");
		}

		// ============================================================================
		public void Log(
			string message,
			LogLevel logLevel = LogLevel.INFO)
		{
			if (_enabled == true &&
				_logLevel <= logLevel)
			{
				var timestamp = string.Empty;
				var indent = string.Empty;

				if (_displayTimestamp)
				{
					timestamp = DateTime.Now.ToString(_timestampFormat) + " > ";
				}

				for (int i = 0; i < _indentLevel; i++)
				{
					indent += _indentString;
				}

				var newLogLine = timestamp + indent + message;

				if (_isDelayedLogging)
				{
					_logLines.Add(newLogLine);
				}
				else
				{
					switch (_logType)
					{
						case LogType.TraceLog:
							_tracer.Trace(newLogLine);
							break;

						case LogType.Console:
							Console.WriteLine(newLogLine);
							break;

						case LogType.File:

							using (StreamWriter sw = File.AppendText(_logFile))
							{
								sw.WriteLine(newLogLine);
							}

							break;
					}

				}

			}
		}

		// ============================================================================
		public void Log(
			string message_1,
			string message_2,
			LogLevel logLevel = LogLevel.INFO)
		{
			Log(message_1, logLevel);
			Log(message_2, logLevel);
		}


		// ============================================================================
		public void Log(
			string message_1,
			string message_2,
			string message_3,
			LogLevel logLevel = LogLevel.INFO)
		{
			Log(message_1, logLevel);
			Log(message_2, logLevel);
			Log(message_3, logLevel);
		}


		// ============================================================================
		public void Log(
			string message_1,
			string message_2,
			string message_3,
			string message_4,
			LogLevel logLevel = LogLevel.INFO)
		{
			Log(message_1, logLevel);
			Log(message_2, logLevel);
			Log(message_3, logLevel);
			Log(message_4, logLevel);
		}


		// ============================================================================
		public void Log(
			string message_1,
			string message_2,
			string message_3,
			string message_4,
			string message_5,
			LogLevel logLevel = LogLevel.INFO)
		{
			Log(message_1, logLevel);
			Log(message_2, logLevel);
			Log(message_3, logLevel);
			Log(message_4, logLevel);
			Log(message_5, logLevel);
		}




		// ============================================================================
		public void PushDelayedLogs()
		{
			if (Enabled)
			{
				foreach (var line in _logLines)
				{
					_tracer.Trace(line);
				}

				_logLines.Clear();
			}
		}

		// ============================================================================
		public void ClearDelayedLogs()
		{
			_logLines.Clear();
		}


		// ============================================================================
		public void ResetIndent()
		{
			_indentLevel = 0;
		}

		// ============================================================================
		public void IncreaseIndent()
		{
			_indentLevel++;
		}

		// ============================================================================
		public void DecreaseIndent()
		{
			_indentLevel--;

			if (_indentLevel < 0)
			{
				_indentLevel = 0;
			}
		}

		// ============================================================================
		private void GenerateIndent()
		{
			_indentString = "|";

			for (int i = 0; i < (_indentSize - 1); i++)
			{
				_indentString += " ";
			}
		}


		// ============================================================================
		public void LogContextInformation(
			IPluginExecutionContext context)
		{


			Log("//////////////////////////////////////////");
			Log("Message:     " + context.MessageName);
			Log("Stage:       " + context.Stage + " - " + GetStageName(context.Stage));

			if (context.PrimaryEntityName != "none")
			{
				Log("Entity:      " + context.PrimaryEntityName + " [" + context.PrimaryEntityId + "]");
			}
			else
			{
				Log("Entity:      n/a");
			}


			if (context.InputParameters.Contains("Target"))
			{
				Log("Target Type: " + context.InputParameters["Target"].ToString().Split('.').Last<string>());
			}
			else
			{
				Log("Target Type: n/a");
			}


			Log("//////////////////////////////////////////");
		}


		// ============================================================================
		public void EnterFunction(
			string functionName,
			bool preventIndent = false)
		{
			if (preventIndent)
			{
				Log("Entering and Leaving function " + functionName + "()");
			}
			else
			{
				Log("Entering function " + functionName + "():");
				IncreaseIndent();
			}
		}


		// ============================================================================
		public void EnterFunction(
			string functionName,
			string parameter,
			bool preventIndent = false)
		{
			if (preventIndent)
			{
				Log(
					"Entering and Leaving function " + functionName +
					(string.IsNullOrWhiteSpace(parameter) ? "()" : "('" + parameter + "')"));
			}
			else
			{
				Log(
					"Entering function " + functionName +
					(string.IsNullOrWhiteSpace(parameter) ? "()" : "('" + parameter + "')"));
				IncreaseIndent();
			}
		}


		// ============================================================================
		public void ExitFunction(
			string message = "")
		{
			if (!string.IsNullOrWhiteSpace(message))
			{
				Log(message);
			}

			DecreaseIndent();
		}

	}


	// ============================================================================
	// ============================================================================
	// ============================================================================
	public enum LogLevel
	{
		DEBUG = 0,
		INFO = 1,
		WARN = 2,
		ERROR = 3

	}



	// ============================================================================
	// ============================================================================
	// ============================================================================
	public enum LogType
	{
		TraceLog = 0,
		Console = 1,
		File = 2
	}
}
