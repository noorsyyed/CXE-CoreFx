using System;
using System.Runtime.CompilerServices;
using CXE.CoreFx.Plugin.Enums;
using Microsoft.Xrm.Sdk;

namespace CXE.CoreFx.Plugin
{
	/// <summary>
	/// When inheriting from this class you need to override at least one of the following methods:
	/// - Execute()
	/// - OnCreatePreOperation()
	/// - OnCreatePostOperation()
	/// - OnUpdatePreOperation()
	/// - OnUpdatePostOperation()
	/// - OnDeletePreValidation()
	/// - OnDeletePreOperation()
	/// - OnDeletePostOperation()
	/// </summary>
	public abstract class PluginBase : IPlugin
	{
		private string _defaultErrorMessage = string.Empty;
		private string _customErrorMessage = string.Empty;

		public void Execute(
			IServiceProvider serviceProvider)
		{
			Helper =
				new DataverseHelper(serviceProvider);

			bool _isError = false;

			try
			{
				Execute();

				switch (Context.MessageName)
				{
					case MessageNames.Create:
						switch (Context.Stage)
						{
							case Stages.PreOperation:
								InitializeController();
								OnCreatePreOperation();
								break;
							case Stages.PostOperation:
								InitializeController();
								OnCreatePostOperation();
								break;
						}
						break;

					case MessageNames.Update:
						switch (Context.Stage)
						{
							case Stages.PreOperation:
								InitializeController();
								OnUpdatePreOperation();
								break;
							case Stages.PostOperation:
								InitializeController();
								OnUpdatePostOperation();
								break;
						}
						break;

					case MessageNames.Delete:
						switch (Context.Stage)
						{
							case Stages.PreValidation:
								InitializeController();
								OnDeletePreValidation();
								break;
							case Stages.PreOperation:
								InitializeController();
								OnDeletePreOperation();
								break;
							case Stages.PostOperation:
								InitializeController();
								OnDeletePostOperation();
								break;
						}
						break;

					case MessageNames.SetState:
						switch (Context.Stage)
						{
							case Stages.PreValidation:
								InitializeController();
								OnUpdatePreValidation();
								break;

							case Stages.PreOperation:
								InitializeController();
								OnSetStatePreOperation();
								break;
							case Stages.PostOperation:
								InitializeController();
								OnSetStatePostOperation();
								break;
						}
						break;

					case MessageNames.RemoveFromQueue:
						switch (Context.Stage)
						{
							case Stages.PreOperation:
								InitializeController();
								OnRemoveFromQueuePreOperation();
								break;
							case Stages.PostOperation:
								InitializeController();
								OnRemoveFromQueuePostOperation();
								break;
						}
						break;

				}
			}
			catch (InvalidPluginExecutionException ex)
			{
				string message = string.Empty;
				bool isCustom = false;

				if (!string.IsNullOrWhiteSpace(_customErrorMessage))
				{
					message =
						_customErrorMessage;

					isCustom = true;
				}
				else
				{
					message = !string.IsNullOrWhiteSpace(_defaultErrorMessage)
						? _defaultErrorMessage
						: (string.IsNullOrWhiteSpace(ex.Message) ? "" : ex.Message) +
						(ex.InnerException == null ? "" : " - " + ex.InnerException.Message);
				}

				Helper.Logger.Log(message);
				_isError = true;

				if (!IgnoreExceptions)
				{
					if (isCustom)
					{
						throw new InvalidPluginExecutionException(
							message);
					}

					throw new InvalidPluginExecutionException(
						message,
						ex);
				}
			}
			catch (Exception ex)
			{
				string message = string.Empty;

				message = !string.IsNullOrWhiteSpace(_customErrorMessage)
					? _customErrorMessage
					: !string.IsNullOrWhiteSpace(_defaultErrorMessage)
						? _defaultErrorMessage
						: (string.IsNullOrWhiteSpace(ex.Message) ? "" : ex.Message) +
						(ex.InnerException == null ? "" : " - " + ex.InnerException.Message);

				Helper.Logger.Log(message);
				_isError = true;

				if (!IgnoreExceptions)
				{
					throw new Exception(
						message,
						ex);
				}
			}
			finally
			{
				Helper.Logger.ResetIndent();

				if (_isError)
				{
					Log("FINISHED WITH ERROR!");
				}
				else
				{
					Log("END & SUCCESS!");
				}

				Helper.Logger.PushDelayedLogs();
			}
		}

		/// <summary>
		/// Override this function. All basic error handling and setup is already taken care of.
		/// You can access Context, ServiceClient, Logger and many more...
		/// </summary>
		public virtual void Execute()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnCreatePreOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnCreatePostOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnUpdatePreValidation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnUpdatePreOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnUpdatePostOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnDeletePreValidation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnDeletePreOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnDeletePostOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnSetStatePreOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnSetStatePostOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnRemoveFromQueuePreOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void OnRemoveFromQueuePostOperation()
		{
			// this function is supposed to be overwritten
		}
		public virtual void InitializeController()
		{
			// this function is supposed to be overwritten
		}
		public Entity PreImageEntity
		{
			get
			{
				return this.Context.PreEntityImages.TryGetValue("PreImage", out Entity preImage) ? preImage : null;
			}
		}
		public Entity PostImageEntity
		{
			get
			{
				return this.Context.PostEntityImages.TryGetValue("PostImage", out Entity postImage) ? postImage : null;
			}
		}

		protected DataverseHelper Helper
		{
			get; private set;
		}

		protected IPluginExecutionContext2 Context => Helper.Context;

		/// <summary>
		/// The service-object to connect with Dataverse.
		/// </summary>
		protected virtual IOrganizationService ServiceClient => Helper.ServiceClient;

		protected TraceLogger Logger => Helper.Logger;

		protected bool IgnoreExceptions { get; set; } = false;

		protected bool IsCreate => Helper.IsCreate;

		protected bool IsUpdate => Helper.IsUpdate;

		protected bool IsDelete => Helper.IsDelete;

		protected bool IsSetState => Helper.IsSetState;

		protected bool IsAssign => Helper.IsAssign;

		protected bool IsAssociate => Helper.IsAssociate;

		protected bool IsDisassociate => Helper.IsDisassociate;

		protected bool IsStagePreOperation => Helper.IsStagePreOperation;

		protected bool IsStagePostOperation => Helper.IsStagePostOperation;

		protected bool IsStageCoreOperation => Helper.IsStageCoreOperation;

		protected bool IsStagePreValidation => Helper.IsStagePreValidation;

		protected bool HasTargetEntity => Helper.HasTargetEntity;

		protected Entity TargetEntity => Helper.TargetEntity;

		protected bool HasTargetEntityReference => Helper.HasTargetEntityReference;

		protected EntityReference TargetEntityReference => Helper.TargetEntityReference;

		protected Entity FirstPreImage => Helper.FirstPreImage;

		protected Entity FirstPostImage => Helper.FirstPostImage;

		/// <summary>
		/// Sets the defaul error message.
		/// </summary>
		/// <param name="defaultErrorMessage"></param>
		protected void SetDefaultErrorMessage(
			string defaultErrorMessage)
		{
			_defaultErrorMessage = defaultErrorMessage;
		}

		/// <summary>
		/// Removes the defaul error message.
		/// </summary>
		protected void RemoveDefaultErrorMessage()
		{
			_defaultErrorMessage = string.Empty;
		}

		/// <summary>
		/// Throws a new exception with the given message. This overrides the possibly 
		/// set default error message.
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <exception cref="InvalidPluginExecutionException"></exception>
		protected void ThrowUserErrorMessage(string errorMessage)
		{
			_customErrorMessage = errorMessage;
			throw new InvalidPluginExecutionException(errorMessage);
		}

		protected void Log(
			string comment)
		{
			Helper.Logger.Log(
				comment);
		}

		protected void EnterFunction(
			[CallerMemberName] string functionName = "")
		{
			Helper.Logger.EnterFunction(functionName);
		}

		protected void EnterFunction(
			string functionName,
			string attribute)
		{
			Helper.Logger.EnterFunction(functionName, attribute);
		}

		protected void ExitFunction(
			string comment = "")
		{
			Helper.Logger.ExitFunction(comment);
		}
	}
}