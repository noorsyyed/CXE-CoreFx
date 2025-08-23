using System;
using System.Collections.Generic;
using System.Security.Policy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;


// ============================================================================
// ============================================================================
// ============================================================================
namespace CXE.CoreFx.Base.Extensions
{

	// ============================================================================
	// ============================================================================
	// ============================================================================
	internal static class EntityExtention
	{

		#region EXTENTIONS

		// ============================================================================
		/// <summary>
		/// Retrieves the attribute value as the given type even if it is an aliased value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="record"></param>
		/// <param name="attributeLogicName"></param>
		/// <param name="alias"></param>
		/// <returns></returns>
		public static T GetAttribute<T>(
			this Entity record,
			string attributeLogicName,
			string alias = "")
		{
			var theObject =
				record.GetAttributeObject(
					attributeLogicName,
					alias);

			if (theObject != null)
			{
				return (T) theObject;
			}

			// Possible null reference return.
#pragma warning disable CS8603
			return default(T);
#pragma warning restore CS8603
		}


		// ============================================================================
		public static string GetPrimaryFieldName(
			this Entity record,
			IOrganizationService serviceClient)
		{
			RetrieveEntityRequest retrievesEntityRequest = new RetrieveEntityRequest
			{
				EntityFilters = EntityFilters.Entity,
				LogicalName = record.LogicalName
			};

			//Execute Request
			var retrieveEntityResponse =
				(RetrieveEntityResponse) serviceClient.Execute(
					retrievesEntityRequest);

			return
				retrieveEntityResponse.EntityMetadata.PrimaryNameAttribute;
		}


		// ============================================================================
		/// <summary>
		/// 
		/// </summary>
		/// <param name="record"></param>
		/// <param name="serviceClient"></param>
		/// <returns></returns>
		public static string GetPrimaryFieldValue(
			this Entity record,
			IOrganizationService serviceClient)
		{
			if (record == null)
			{
				return string.Empty;
			}

			var primaryFieldName =
				record.GetPrimaryFieldName(
					serviceClient);

			record.LoadMissingAttribute(
				primaryFieldName,
				serviceClient);

			return record.GetAttributeString(primaryFieldName);
		}



		// ============================================================================
		public static string GetAttributeString(
			this Entity record,
			string attributeLogicName,
			string alias = "")
		{
			var theObject =
				record.GetAttributeObject(
					attributeLogicName,
					alias);

			if (!string.IsNullOrWhiteSpace(alias))
			{
				attributeLogicName = alias + "." + attributeLogicName;
			}

			return
				StringHelper.GetObjectValueAsString(
					theObject,
					attributeLogicName,
					record);
		}


		// ============================================================================
		/// <summary>
		/// Adds or updates the given parameter in the record.
		/// </summary>
		/// <param name="record">The record that needs to be updated</param>
		/// <param name="attributeLogicName">The key-value of the parameter to be added</param>
		/// <param name="value">The value of the parameter to be added</param>
		public static void AddAttribute(
			this Entity record,
			string attributeLogicName,
			object value)
		{
			if (record == null ||
				value.IsNullObject())
			{
				return;
			}

			if (record.Contains(attributeLogicName))
			{
				record[attributeLogicName] = value;
			}
			else
			{
				record.Attributes.Add(
					attributeLogicName,
					value);
			}
		}

		// ============================================================================
		/// <summary>
		/// Sets the existing attribute to null (emptying an existing value in Dataverse)
		/// </summary>
		/// <param name="record">The record that needs to be updated</param>
		/// <param name="key">The key-value of the parameter to be added</param>
		public static void EmptyAttribute(
			this Entity record,
			string key)
		{
			if (record == null)
			{
				return;
			}

			if (record.Contains(key))
			{
				record[key] = null;
			}
		}


		// ============================================================================
		/// <summary>
		/// Loads the missing attribute and adds it to the record where missing.
		/// Existing attributes will not be overwritten.
		/// </summary>
		/// <param name="record"></param>
		/// <param name="serviceClient"></param>
		/// <param name="attribute"></param>
		public static void LoadMissingAttribute(
			this Entity record,
			string attribute,
			IOrganizationService serviceClient)
		{
			record.LoadMissingAttributes(
				new List<string>()
				{
					attribute
				},
				serviceClient);
		}



		// ============================================================================
		/// <summary>
		/// Loads the missing attributes and adds them to the record where missing.
		/// Existing attributes will not be overwritten.
		/// </summary>
		/// <param name="record"></param>
		/// <param name="serviceClient"></param>
		/// <param name="attributes"></param>
		public static bool LoadMissingAttributes(
			this Entity record,
			List<string> attributes,
			IOrganizationService serviceClient)
		{
			Entity recordLoaded;

			try
			{
				recordLoaded =
					serviceClient.Retrieve(
						record.LogicalName,
						record.Id,
						new ColumnSet(attributes.ToArray()));
			}
			catch (Exception) { return false; }

			foreach (var attribute in attributes)
			{
				if (record.Contains(attribute) ||
					!recordLoaded.Contains(attribute))
				{
					continue;
				}

				record.AddAttribute(
					attribute,
					recordLoaded[attribute]);
			}

			return true;
		}


		// ============================================================================
		/// <summary>
		/// Checks if all of the parameters listed are contained in the record.
		/// </summary>
		/// <param name="record">The record that should be checked</param>
		/// <param name="parameterName">A parameter name that need to be contained</param>
		/// <returns>Return true if the parameter-name is contained in the record.</returns>
		public static bool CheckIfContains(
			this Entity record,
			string parameterName)
		{
			return record.CheckIfContains(
				new List<string> {
					parameterName
				});
		}



		// ============================================================================
		/// <summary>
		/// Checks if all of the parameters listed are contained in the record.
		/// </summary>
		/// <param name="record">The record that should be checked</param>
		/// <param name="parameterNames">The list of parameter names that need to be contained</param>
		/// <returns>Return true if all of the parameter-names are contained in the record.</returns>
		public static bool CheckIfContains(
			this Entity record,
			List<string> parameterNames)
		{
			if (record == null)
				return false;

			foreach (var parameter in parameterNames)
			{
				if (!record.Contains(parameter))
				{
					return false;
				}
			}

			return true;
		}


		// ============================================================================
		/// <summary>
		/// Synchronizes non existant parameters from the recordSource to
		/// the record being updated. Existing attributes will not be overwritten.
		/// </summary>
		/// <param name="recordBeingUpdated">The record that is updated with missing parameters</param>
		/// <param name="recordSource">The record that is synched into the function-parameter called 'record'</param>
		/// <param name="forceOverwrite">Optional parameter to force over-writing of all attributes</param>		/// 
		/// <param name="attributeList">Optional parameter to only sync selected attributes</param>
		public static void SynchronizeAttributesFrom(
			this Entity recordBeingUpdated,
			Entity recordSource,
			bool forceOverwrite = false,
			List<string> attributeList = null)
		{
			if (recordBeingUpdated == null)
				return;

			var counter = 0;

			foreach (var attributeSource in recordSource.Attributes)
			{
				var copyAttribute = true;

				if (attributeList != null)
				{
					copyAttribute = false;
					foreach (var attribute in attributeList)
					{
						if (attributeSource.Key == attribute)
						{
							copyAttribute = true;
							break;// Stop the loop if a match is found
						}
					}
				}

				if ((!recordBeingUpdated.Contains(attributeSource.Key) || forceOverwrite) &&
					copyAttribute)
				{
					recordBeingUpdated.Attributes[attributeSource.Key] = attributeSource.Value;

					counter++;
				}
			}
		}

		// ============================================================================
		/// <summary>
		/// Logs all the attributes of the given record.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="excludedFields"></param>
		public static void LogAllAttributes(
			this Entity entity,
			LoggerBase logger,
			string excludedFields = "")
		{
			if (entity == null)
				return;

			logger.Log("- - - - - - - - - - -");
			logger.Log(" A T T R I B U T E S");
			logger.IncreaseIndent();

			logger.Log(
				"Entity: " + entity.LogicalName + ".{" + entity.Id.ToString() + "}");

			if (entity != null)
			{
				List<string> attributeList = new List<string>();

				foreach (var attribute in entity.Attributes)
				{

					if (excludedFields.Contains(attribute.Key))
					{
						continue;
					}

					string attributeString;

					if (attribute.Value != null)
					{
						attributeString =
							attribute.Key +
							" [" +
							attribute.Value.GetType().Name +
							"] = " +
							entity.GetAttributeString(attribute.Key);
					}
					else
					{
						attributeString =
							attribute.Key +
							" [NULL]";
					}

					attributeList.Add(attributeString);
				}

				attributeList.Sort();
				foreach (var attributeString in attributeList)
				{
					logger.Log(attributeString);
				}
			}
			else
			{
				logger.Log(
					"Empty entity received!");
			}

			logger.DecreaseIndent();
			logger.Log("- - - - - - - - - - -");

		}


		// ============================================================================
		public static void LogAllAttributesWithFormattedValues(
			this Entity entity,
			LoggerBase logger,
			string excludedFields = "")
		{
			if (entity == null)
				return;

			logger.Log("- - - - - - - - - - -");
			logger.Log(" A T T R I B U T E S");
			logger.IncreaseIndent();

			logger.Log(
				"Entity: " + entity.LogicalName + ".{" + entity.Id.ToString() + "}");

			if (entity != null)
			{
				List<string> attributeList = new List<string>();

				foreach (var attribute in entity.Attributes)
				{

					if (excludedFields.Contains(attribute.Key))
					{
						continue;
					}

					string attributeString;

					if (attribute.Value != null)
					{
						attributeString =
							attribute.Key +
							" [" +
							attribute.Value.GetType().Name +
							"] = " +
							entity.GetAttributeString(attribute.Key);
					}
					else
					{
						attributeString =
							attribute.Key +
							" [NULL]";
					}

					attributeList.Add(attributeString);
				}

				attributeList.Sort();
				foreach (var attributeString in attributeList)
				{
					logger.Log(attributeString);
				}

				// Formatted Values
				logger.DecreaseIndent();
				logger.Log("");
				logger.Log("- - - - - - - - - - -");
				logger.Log("  F O R M A T T E D");
				logger.IncreaseIndent();

				attributeList = new List<string>();

				foreach (var formattedValue in entity.FormattedValues)
				{
					attributeList.Add(
						formattedValue.Key + " [" +
						formattedValue.Value + "]");
				}

				attributeList.Sort();
				foreach (var attributeString in attributeList)
				{
					logger.Log(attributeString);
				}

			}
			else
			{
				logger.Log(
					"Empty entity received!");
			}

			logger.DecreaseIndent();
			logger.Log("- - - - - - - - - - -");

		}


		#endregion


		#region PRIVATE

		// ============================================================================
		/// <summary>
		/// Retrieves the data object of the attribute even if it is an aliased value
		/// </summary>
		/// <param name="record"></param>
		/// <param name="attributeName"></param>
		/// <param name="aliasName"></param>
		/// <returns></returns>
		private static object GetAttributeObject(
			this Entity record,
			string attributeName,
			string aliasName = "")
		{
			if (!string.IsNullOrWhiteSpace(aliasName))
			{
				attributeName =
					aliasName + "." + attributeName;
			}

			if (!record.Contains(attributeName))
			{
				return null;
			}

			var type = record.Attributes[attributeName]?.GetType()?.Name;

			switch (type)
			{
				case "AliasedValue":

					var aliased =
						record.GetAttributeValue<AliasedValue>(attributeName);

					return
						aliased.Value;

				default:
					return
					   record.Attributes[attributeName];

			}
		}


		#endregion

	}


}