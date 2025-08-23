
using System;
//using System.Activities;
using System.Linq;
using CXE.CoreFx.Base;
using CXE.CoreFx.Base.Extensions;
//using System.Runtime.Remoting.Contexts;
using CXE.CoreFx.Plugin.Enums;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;


// ============================================================================
// ============================================================================
// ============================================================================
namespace CXE.CoreFx.Plugin
{

	// ============================================================================
	// ============================================================================
	// ============================================================================
	public class DataverseHelper
	{



		// ============================================================================
		[Obsolete]
		public DataverseHelper(
			IServiceProvider serviceProvider)
		{
			TracingService =
				(ITracingService) serviceProvider.GetService(typeof(ITracingService));

			Context =
				(IPluginExecutionContext) serviceProvider.GetService(
					typeof(IPluginExecutionContext));

			IOrganizationServiceFactory serviceFactory =
				(IOrganizationServiceFactory) serviceProvider.GetService(
				typeof(IOrganizationServiceFactory));

			Service =
				serviceFactory.CreateOrganizationService(
					Context.UserId);

			Logger = new TraceLogger(TracingService)
			{
				DisplayTimestamp = true,
				IsDelayedLogging = true
			};
			Logger.LogContextInformation(Context);

		}


		#region Core-Dataverse-Objects

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// The tracing-service
		/// </summary>
		public ITracingService TracingService
		{
			get;
		}


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// The execution-context.
		/// </summary>
		public IPluginExecutionContext Context
		{
			get;
		}

		/*
		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// The execution-context.
		/// </summary>
		public CodeActivityContext ActivityContext
		{
			get
			{
				return _contextActivity;
			}
		}
		*/

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// The service-object to connect with Dataverse.
		/// </summary>
		[Obsolete]
		public virtual IOrganizationService ServiceClient => Service;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Service is deprecated, please use ServiceClient instead.
		/// </summary>
		[Obsolete("Service is deprecated, please use ServiceClient instead.")]
		public IOrganizationService Service
		{
			get;
		}

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		// A tool for logging to Dataverse trace-logs.
		public TraceLogger Logger
		{
			get;
		}

		#endregion


		#region Properties

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the message name is 'Create'.
		/// </summary>
		public bool IsCreate => Context.MessageName == MessageNames.Create;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the message name is 'Update'.
		/// </summary>
		public bool IsUpdate => Context.MessageName == MessageNames.Update;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the message name is 'Delete'.
		/// </summary>
		public bool IsDelete => Context.MessageName == MessageNames.Delete;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the message name is 'SetState'.
		/// </summary>
		public bool IsSetState => Context.MessageName == MessageNames.SetState;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the message name is 'SetState'.
		/// </summary>
		public bool IsAssign => Context.MessageName == MessageNames.Assign;


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the message name is 'Associate'.
		/// </summary>
		public bool IsAssociate => Context.MessageName == MessageNames.Associate;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the message name is 'Disassociate'.
		/// </summary>
		public bool IsDisassociate => Context.MessageName == MessageNames.Disassociate;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the stage is 'Pre-Validation'.
		/// </summary>
		public bool IsStagePreValidation => Context.Stage == Stages.PreValidation;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the stage is 'Pre-Operation'.
		/// </summary>
		public bool IsStagePreOperation => Context.Stage == Stages.PreOperation;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the stage is 'Core-Operation'.
		/// </summary>
		public bool IsStageCoreOperation => Context.Stage == Stages.CoreOperation;

		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the stage is 'Post-Operation'.
		/// </summary>
		public bool IsStagePostOperation => Context.Stage == Stages.PostOperation;


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the context contains a Target with type Entity
		/// </summary>
		public bool HasTargetEntity => Context.InputParameters.Contains("Target") &&
					Context.InputParameters["Target"] is Entity;


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the context contains a Target with type EntityReference
		/// </summary>
		public bool HasTargetEntityReference => Context.InputParameters.Contains("Target") &&
					Context.InputParameters["Target"] is EntityReference;


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// The Taget from the execution-context. If there is no Taget of type Entity
		/// this return null
		/// </summary>
		public Entity TargetEntity => HasTargetEntity ? (Entity) Context.InputParameters["Target"] : null;


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// The Taget from the execution-context. If there is no Taget of type EntityReference
		/// this return null
		/// </summary>
		public EntityReference TargetEntityReference => HasTargetEntityReference ? (EntityReference) Context.InputParameters["Target"] : null;


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the context contains a Pre-Image.
		/// </summary>
		public bool HasPreImage => Context.PreEntityImages.Count > 0;


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Returns the first Pre-Image that is foiund. If there is no Pre-Image,
		/// this return null
		/// </summary>
		public Entity FirstPreImage => HasPreImage ? Context.PreEntityImages.First().Value : null;


		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Checks if the context contains a Post-Image.
		/// </summary>
		public bool HasPostImage => Context.PostEntityImages.Count > 0;



		// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Returns the first Post-Image that is foiund. If there is no Post-Image,
		/// this return null
		/// </summary>
		public Entity FirstPostImage => HasPostImage ? Context.PostEntityImages.First().Value : null;




		#endregion


		#region Public-Functions

		// ============================================================================
		/// <summary>
		/// Handles the re-sorting of child records with an integer sorting-index 
		/// field. The lowest possible sorting index needs to be 1.
		/// When deleting a record, theRecord and the preImage should be set to the identical
		/// record (equal sorting indexes).
		/// </summary>
		/// <param name="theRecord"></param>
		/// <param name="preImage"></param>
		/// <param name="sortingIndexFieldName"></param>
		/// <param name="parentRecordLookupName"></param>
		/// <param name="automaticSortingFlagFieldName"></param>
		public void HandleReSorting(
			ref Entity theRecord,
			Entity preImage,
			string sortingIndexFieldName,
			string parentRecordLookupName,
			string automaticSortingFlagFieldName)
		{
			Logger.EnterFunction("HandleReSorting");

			if (!preImage.Contains(sortingIndexFieldName))
			{
				Logger.ExitFunction("The pre-image does not contain the needed sorting index.");
				return;
			}

			Entity recordLoaded =
				ServiceClient.Retrieve(
					theRecord.LogicalName,
					theRecord.Id,
					new ColumnSet(
						sortingIndexFieldName,
						parentRecordLookupName));

			theRecord.SynchronizeAttributesFrom(recordLoaded);

			int indexPre = preImage.GetAttribute<int>(sortingIndexFieldName);
			int indexPost = theRecord.GetAttribute<int>(sortingIndexFieldName);
			EntityCollection sortables =
				LoadSortableRecords(
					theRecord,
					parentRecordLookupName,
					sortingIndexFieldName);

			if (indexPost > sortables.Entities.Count)
			{
				indexPost = sortables.Entities.Count;

				theRecord.AddAttribute(
					sortingIndexFieldName,
					sortables.Entities.Count);
			}

			foreach (Entity item in sortables.Entities)
			{
				if (item.Id == theRecord.Id)
				{
					continue;
				}

				int updateVal = 0;

				int currentIndex =
					item.GetAttribute<int>(
						sortingIndexFieldName);

				// Moved up in the list
				if (currentIndex >= indexPost &&
					currentIndex < indexPre)
				{
					updateVal = +1;
				}

				// Moved down in the list
				if (currentIndex > indexPre &&
					currentIndex <= indexPost)
				{
					updateVal = -1;
				}

				// Removed from the list
				if (indexPost == indexPre &&
					currentIndex > indexPre)
				{
					updateVal = -1;
				}

				if (updateVal == 0)
				{
					continue;
				}

				Entity itemUpdated =
					new(
						item.LogicalName,
						item.Id);

				itemUpdated.AddAttribute(
					sortingIndexFieldName,
					item.GetAttribute<int>(sortingIndexFieldName) + updateVal);

				itemUpdated.AddAttribute(
					automaticSortingFlagFieldName,
					true);

				ServiceClient.Update(itemUpdated);
			}

			Logger.ExitFunction();
		}



		// ============================================================================
		/// <summary>
		/// Retrieves the logical name of the entity with the given logical name
		/// </summary>
		/// <param name="entityLogicalName">The logical name of the entity from which you want to get the primary fields name</param>
		/// <returns></returns>
		public string GetPrimaryFieldName(
			string entityLogicalName)
		{

			RetrieveEntityRequest retrievesEntityRequest = new()
			{
				EntityFilters = EntityFilters.Entity,
				LogicalName = entityLogicalName
			};

			//Execute Request
			RetrieveEntityResponse retrieveEntityResponse =
				(RetrieveEntityResponse) ServiceClient.Execute(
					retrievesEntityRequest);

			return
				retrieveEntityResponse.EntityMetadata.PrimaryNameAttribute;
		}


		// ============================================================================
		/// <summary>
		/// Retrieves the value of the entities primary field.
		/// </summary>
		/// <param name="recordRef">The reference to the record from which you want to get the primary fields value</param>
		/// <returns></returns>
		public string GetPrimaryFieldValue(
			EntityReference recordRef)
		{
			if (recordRef == null)
			{
				return string.Empty;
			}

			string primaryFieldName =
				GetPrimaryFieldName(
					recordRef.LogicalName);

			Entity record =
				ServiceClient.Retrieve(
					recordRef.LogicalName,
					recordRef.Id,
					new ColumnSet(
						primaryFieldName));

			return record.GetAttributeString(primaryFieldName);
		}



		// ============================================================================
		/// <summary>
		/// Retrieves the value of the entities primary field.
		/// </summary>
		/// <param name="record">The record from which you want to get the primary fields value</param>
		/// <returns></returns>
		public string GetPrimaryFieldValue(
			Entity record)
		{
			string primaryFieldName =
				GetPrimaryFieldName(
					record.LogicalName);

			Entity loadedRecord =
				ServiceClient.Retrieve(
					record.LogicalName,
					record.Id,
					new ColumnSet(
						primaryFieldName));

			return loadedRecord.GetAttributeString(primaryFieldName);
		}


		// ============================================================================
		//public DateTime ConvertDateTimeToUserLocalTime(
		//	DateTime inputTime,
		//	TimeZoneInfo timeZoneInfo = null)

		//{
		//	timeZoneInfo ??=
		//			GetCurrentUsersTimeZoneInfo();

		//	return TimeZoneInfo.ConvertTimeFromUtc(inputTime, timeZoneInfo);
		//}



		// ============================================================================
		//public TimeZoneInfo GetCurrentUsersTimeZoneInfo()

		//{
		//	// ----------------------------------
		//	QueryExpression query =
		//		new(
		//			SystemUserSetting.LogicalName)
		//		{
		//			ColumnSet =
		//			new ColumnSet(
		//				SystemUserSetting.LocaleId,
		//				SystemUserSetting.TimeZoneCode)
		//		};

		//	query.Criteria.AddCondition(
		//			new ConditionExpression(
		//				SystemUserSetting.SystemUser,
		//				ConditionOperator.EqualUserId));

		//	EntityCollection queryResponse =
		//		ServiceClient.RetrieveMultiple(query);

		//	if (queryResponse.Entities.Count < 1)
		//	{
		//		return null;
		//	}

		//	Entity firstRecord =
		//		queryResponse.Entities.First();

		//	int timeZoneCode =
		//		firstRecord.GetAttributeValue<int>(SystemUserSetting.TimeZoneCode);

		//	// ----------------------------------
		//	query =
		//		new QueryExpression(
		//			TimeZoneDefinition.LogicalName)
		//		{
		//			ColumnSet =
		//			new ColumnSet(
		//				TimeZoneDefinition.StandardName)
		//		};

		//	query.Criteria.AddCondition(
		//		TimeZoneDefinition.TimeZoneCode,
		//		ConditionOperator.Equal,
		//		timeZoneCode);

		//	queryResponse =
		//		ServiceClient.RetrieveMultiple(query);

		//	string timeZoneStandardName =
		//		queryResponse.Entities.First().GetAttributeValue<string>(
		//			TimeZoneDefinition.StandardName);

		//	return TimeZoneInfo.FindSystemTimeZoneById(
		//		timeZoneStandardName);
		//}


		// ============================================================================
		public void LogAllInputParameters()
		{
			Logger.Log("- - - - - - - - - - -");
			Logger.Log("I N P U T   P A R A M");
			Logger.IncreaseIndent();

			int maxLength = 0;

			foreach (System.Collections.Generic.KeyValuePair<string, object> parameter in Context.InputParameters)
			{
				if (parameter.Key.Length > maxLength)
				{
					maxLength = parameter.Key.Length;
				}
			}

			foreach (System.Collections.Generic.KeyValuePair<string, object> parameter in Context.InputParameters)
			{
				Logger.Log(
					string.Format("{0," + -maxLength + "}", parameter.Key) + " : " +
					StringHelper.GetObjectValueAsString(parameter.Value));
			}

			Logger.DecreaseIndent();
			Logger.Log("- - - - - - - - - - -");

		}


		// ============================================================================
		public void LogAllOutputParameters()
		{
			Logger.Log("- - - - - - - - - - - - -");
			Logger.Log(" O U T P U T   P A R A M");
			Logger.IncreaseIndent();

			int maxLength = 0;

			foreach (System.Collections.Generic.KeyValuePair<string, object> parameter in Context.OutputParameters)
			{
				if (parameter.Key.Length > maxLength)
				{
					maxLength = parameter.Key.Length;
				}
			}

			foreach (System.Collections.Generic.KeyValuePair<string, object> parameter in Context.OutputParameters)
			{
				Logger.Log(
					string.Format("{0," + -maxLength + "}", parameter.Key) + " : " +
					StringHelper.GetObjectValueAsString(parameter.Value));
			}

			Logger.DecreaseIndent();
			Logger.Log("- - - - - - - - - - - - -");

		}


		// ============================================================================
		/// <summary>
		/// Logs att the attributes of the Target Entity if there is one.
		/// </summary>
		public void LogAllAttributes()
		{
			if (HasTargetEntity)
			{
				TargetEntity.LogAllAttributes(Logger);
			}
		}


		// ============================================================================
		/// <summary>
		/// Retrieves the value of an environment variable of type string.
		/// </summary>
		/// <param name="variableSchemaName">The schema name of the environment-variabel</param>
		/// <param name="doLogging">Optional parameter to enable logging. Default is false</param>
		/// <returns></returns>
		//public string GetEnvironmentVariableValue(
		//	string variableSchemaName,
		//	bool doLogging = false)
		//{
		//	if (doLogging)
		//	{
		//		Logger.EnterFunction("GetEnvironmentVariableValue", variableSchemaName);
		//	}

		//	QueryExpression query =
		//		new(
		//			EnvironmentVariableDefinition.LogicalName)
		//		{
		//			ColumnSet =
		//			new ColumnSet(
		//				EnvironmentVariableDefinition.DefaultValue)
		//		};

		//	query.Criteria.AddCondition(
		//		new ConditionExpression(
		//			EnvironmentVariableDefinition.SchemaName,
		//			ConditionOperator.Equal,
		//			variableSchemaName));

		//	query.Criteria.AddCondition(
		//		new ConditionExpression(
		//			EnvironmentVariableDefinition.StateCode,
		//			ConditionOperator.Equal,
		//			0)); // active

		//	EntityCollection result =
		//		Service.RetrieveMultiple(query);

		//	if (result.Entities.Count > 0)
		//	{
		//		if (doLogging)
		//		{
		//			Logger.ExitFunction("An environment variable was found.");
		//		}

		//		Entity record = result.Entities[0];

		//		return
		//			record.GetAttributeValue<string>(
		//				EnvironmentVariableDefinition.DefaultValue);

		//	}

		//	if (doLogging)
		//	{
		//		Logger.EnterFunction(
		//		   "No environment variables with the schema name '" + variableSchemaName +
		//			"' were found!");
		//	}
		//	return null;
		//}


		// ============================================================================
		/*protected string GetConfigurationKeyValue(
			string keyName)
		{

			QueryExpression query =
				new(
					ConfigurationKey.LogicalName)
				{
					ColumnSet =
					new ColumnSet(
						ConfigurationKey.ValueString),

					Criteria =
					new FilterExpression(
						LogicalOperator.And)
				};

			query.Criteria.AddCondition(
				new ConditionExpression(
					ConfigurationKey.Key,
					ConditionOperator.Equal,
					keyName));

			EntityCollection resultCollection =
				Service.RetrieveMultiple(
					query);

			if (resultCollection.Entities.Count == 1)
			{
				Entity configKey =
					resultCollection.Entities[0];

				if (configKey.Contains(ConfigurationKey.ValueString))
				{
					return (string) configKey[ConfigurationKey.ValueString];
				}
			}

			return string.Empty;
		}*/



		#endregion


		#region Private-Functions


		// ============================================================================
		private EntityCollection LoadSortableRecords(
			Entity sortable,
			string parentRecordLookupFieldName,
			string sortingIndexFieldName)
		{
			EntityReference parentRecordRef =
				sortable.GetAttribute<EntityReference>(
					parentRecordLookupFieldName);

			if (parentRecordRef == null)
			{
				return null;
			}

			QueryExpression query = new(sortable.LogicalName)
			{
				ColumnSet =
					new ColumnSet(
						sortingIndexFieldName)
			};

			query.Criteria.AddCondition(
				new ConditionExpression(
					parentRecordLookupFieldName,
					ConditionOperator.Equal,
					parentRecordRef.Id));

			query.AddOrder(
				sortingIndexFieldName,
				OrderType.Ascending);

			return ServiceClient.RetrieveMultiple(query);
		}


		#endregion



	}
}
