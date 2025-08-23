/*
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Net;
using System.ServiceModel;
using System.Text.RegularExpressions;
*/

// ============================================================================
// ============================================================================
// ============================================================================
namespace CXE.CoreFx.Plugin
{
	/*
	// ============================================================================
	// ============================================================================
	// ============================================================================
	public abstract class CustomActionBase : CodeActivity
	{

		private DataverseHelper _helper;


		// ============================================================================
		protected override void Execute(
			CodeActivityContext codeActivityContext)
		{
			_helper =
				new DataverseHelper(codeActivityContext);

			bool _isError = false;

			// -----------------------------------------------------
			try
			{
				Execute();
			}
			catch (InvalidPluginExecutionException ex)
			{
				string message = string.Empty;

				message =
					(string.IsNullOrWhiteSpace(ex.Message) ? "" : ex.Message) +
					(ex.InnerException == null ? "" : " - " + ex.InnerException.Message);

				_helper.Logger.Log(message);
				_isError = true;

				throw new InvalidPluginExecutionException(
					message,
					ex);
			}

			catch (Exception ex)
			{
				string message = string.Empty;

				message =
					(string.IsNullOrWhiteSpace(ex.Message) ? "" : ex.Message) +
					(ex.InnerException == null ? "" : " - " + ex.InnerException.Message);


				_helper.Logger.Log(message);
				_isError = true;

				throw new Exception(
					message,
					ex);
			}

			// -----------------------------------------------------
			finally
			{
				Logger.ResetIndent();

				if (_isError)
				{
					Log("FINISHED WITH ERROR!");
				}
				else
				{
					Log("END & SUCCESS!");
				}

				_helper.Logger.PushDelayedLogs();
			}
		}


		// ============================================================================
		/// <summary>
		/// Override this function. There will be a private 'helper' object for accessing
		/// all tools from the datavserseHelper.
		/// </summary>
		public abstract void Execute();


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		protected DataverseHelper Helper
		{
			get
			{
				return _helper;
			}
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		protected IPluginExecutionContext Context
		{
			get
			{
				return _helper.Context;
			}
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		protected CodeActivityContext ActivityContext
		{
			get
			{
				return _helper.ActivityContext;
			}
		}


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// The service-object to connect with Dataverse.
		/// </summary>
		protected IOrganizationService ServiceClient
		{
			get
			{
				return _helper.ServiceClient;
			}
		}


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		protected Logger Logger
		{
			get
			{
				return _helper.Logger;
			}
		}

		// ============================================================================
		protected void Log(
			string comment,
			LogLevel logLevel = LogLevel.INFO)
		{
			_helper.Logger.Log(
				comment,
				logLevel);
		}


		// ============================================================================
		protected void EnterFunction(
			string functionName)
		{
			_helper.Logger.EnterFunction(functionName);
		}


		// ============================================================================
		protected void EnterFunction(
			string functionName,
			string attribute)
		{
			_helper.Logger.EnterFunction(functionName, attribute);
		}


		// ============================================================================
		protected void ExitFunction(
			string comment = "")
		{
			_helper.Logger.ExitFunction(comment);
		}

	}

	*/
}
