using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CXE.CoreFx.Base
{
	public abstract class LoggerBase
	{
		private LogLevel _logLevel;
		private bool _enabled = true;
		private bool _isDelayedLogging = false;
		private List<string> _logLines;
		private int _indentSize;
		private string _indentString;
		private int _indentLevel;
		private bool _displayTimestamp;
		private string _timestampFormat;
		public const string SeparationLine = ":::::::::::::::::::::::::::::::::::::::";

		public bool Enabled
		{
			get => _enabled;
			set => _enabled = value;
		}

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

		public LogLevel LogLevel
		{
			get => _logLevel;
			set => _logLevel = value;
		}

		public bool DisplayTimestamp
		{
			get => _displayTimestamp;
			set => _displayTimestamp = value;
		}

		public string TimestampFormat
		{
			get => _timestampFormat;
			set => _timestampFormat = value;
		}

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

		/// <summary>
		/// Initiates a Logger that is logging to the Console.
		/// </summary>
		/// <param name="logLevel">The logging level</param>
		public LoggerBase(
			LogLevel logLevel = LogLevel.INFO)
		{
			_logLevel = logLevel;
			_enabled = true;
			_displayTimestamp = false;
			_timestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
			_indentSize = 4;
			_indentLevel = 0;
			_indentString = string.Empty;
			_logLines = new List<string>();
			GenerateIndent();
		}

		private bool FormatMessage(
			string message,
			LogLevel logLevel,
			out string formatedMessage)
		{
			if (!_enabled ||
				_logLevel > logLevel)
			{
				formatedMessage = string.Empty;
				return false;
			}

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
				formatedMessage = newLogLine;
				return false;
			}

			formatedMessage = newLogLine;
			return true;
		}

		/// <summary>
		/// implement this function to write the line to be logged how you want it.
		/// </summary>
		/// <param name="line"></param>
		protected abstract void WriteLogLine(string line);

		public void Log(
			string message,
			LogLevel logLevel = LogLevel.INFO)
		{
			if (!FormatMessage(message, logLevel, out string formattedMessage))
			{
				return;
			}

			WriteLogLine(formattedMessage);
		}

		/// <summary>
		/// Logs an empty line
		/// </summary>
		public void Log()
		{
			Log("");
		}

		public void Log(
			string message_1,
			string message_2,
			LogLevel logLevel = LogLevel.INFO)
		{
			Log(message_1, logLevel);
			Log(message_2, logLevel);
		}

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

		public void PushDelayedLogs()
		{
			if (Enabled)
			{
				foreach (var line in _logLines)
				{
					WriteLogLine(line);
				}

				_logLines.Clear();
			}
		}

		public void ClearDelayedLogs()
		{
			_logLines.Clear();
		}

		public void ResetIndent()
		{
			_indentLevel = 0;
		}

		public void IncreaseIndent()
		{
			_indentLevel++;
		}

		public void DecreaseIndent()
		{
			_indentLevel--;

			if (_indentLevel < 0)
			{
				_indentLevel = 0;
			}
		}

		private void GenerateIndent()
		{
			_indentString = "|";

			for (int i = 0; i < (_indentSize - 1); i++)
			{
				_indentString += " ";
			}
		}

		public void EnterFunction(
			[CallerMemberName] string functionName = "",
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

	public enum LogLevel
	{
		DEBUG = 0,
		INFO = 1,
		WARN = 2,
		ERROR = 3
	}
}
