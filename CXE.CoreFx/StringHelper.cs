using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk;


// ============================================================================
// ============================================================================
// ============================================================================
namespace CXE.CoreFx.Base
{

	// ============================================================================
	// ============================================================================
	// ============================================================================
	internal class StringHelper
	{


		// ============================================================================
		public static string GenerateSeparatedString(
			params string[] inputList)
		{
			var resultString = string.Empty;

			foreach (var item in inputList)
			{
				resultString += item + ",";
			}

			resultString = resultString.Substring(0, resultString.Length - 1);

			return resultString;
		}

		// ============================================================================
		public static string GenerateSeparatedString(
			string separator = ",",
			params string[] inputList)
		{
			var resultString = string.Empty;

			foreach (var item in inputList)
			{
				resultString += item + separator;
			}

			resultString = resultString.Substring(0, resultString.Length - separator.Length);

			return resultString;
		}


		// ============================================================================
		public static string GetObjectValueAsString(
			object theObject,
			string key = null,
			Entity theRecord = null)
		{
			if (theObject == null)
			{
				return null;
			}

			var type = theObject.GetType().Name;

			switch (type)
			{
				case "String":
					return (string) theObject;

				case "Boolean":
					return ((bool) theObject).ToString();

				case "DateTime":
					return ((DateTime) theObject).ToString("yyyy-MM-dd HH:mm:ss");

				case "Decimal":
					return ((decimal) theObject).ToString("0.##");

				case "EntityCollection":
					var collection = (EntityCollection) theObject;
					return
						collection.Entities.Count + " " +
						collection.EntityName + "(s)";

				case "EntityReference":
					var objectRef = (EntityReference) theObject;
					return objectRef.LogicalName + "." + objectRef.Id.ToString();

				case "Guid":
					return ((Guid) theObject).ToString();

				case "Int":
					return ((int) theObject).ToString();

				case "Money":
					return ((Money) theObject).Value.ToString();

				case "OptionSetValue":

					var optionSetObject =
						(OptionSetValue) theObject;

					if (theRecord != null &&
						key != null &
						theRecord.FormattedValues.Contains(key))
					{
						return theRecord.FormattedValues[key].ToString();
					}

					return optionSetObject.Value.ToString();

				default:
					return (string) theObject;
			}
		}






	}
}