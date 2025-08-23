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
	internal static class EntityReferenceExtention
	{

		#region EXTENTIONS


		// ============================================================================
		/// <summary>
		/// Retrieves the logical name of the primary field from the entity reference
		/// </summary>
		/// <param name="recordRef">The entity reference</param>
		/// <param name="serviceClient">The IOrganisationService</param>
		/// <returns></returns>
		public static string GetPrimaryFieldName(
			this EntityReference recordRef,
			IOrganizationService serviceClient)
		{
			RetrieveEntityRequest retrievesEntityRequest = new RetrieveEntityRequest
			{
				EntityFilters = EntityFilters.Entity,
				LogicalName = recordRef.LogicalName
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
		/// Retrieves the value of the entities primary field.
		/// </summary>
		/// <param name="recordRef">The entity reference</param>
		/// <param name="serviceClient">The IOrganisationService</param>
		/// <returns></returns>
		public static string GetPrimaryFieldValue(
			this EntityReference recordRef,
			IOrganizationService serviceClient)
		{
			if (recordRef == null)
			{
				return string.Empty;
			}

			var primaryFieldName =
				recordRef.GetPrimaryFieldName(serviceClient);

			var record =
				serviceClient.Retrieve(
					recordRef.LogicalName,
					recordRef.Id,
					new ColumnSet(
						primaryFieldName));

			return record.GetAttributeString(primaryFieldName);
		}


		#endregion

		#region PRIVATE


		#endregion
	}
}
