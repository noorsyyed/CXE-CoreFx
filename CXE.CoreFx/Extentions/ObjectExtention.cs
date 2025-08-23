// ============================================================================
// ============================================================================
// ============================================================================
namespace CXE.CoreFx.Base.Extensions
{

	// ============================================================================
	// ============================================================================
	// ============================================================================
	internal static class ObjectExtention
	{

		#region EXTENTIONS

		// ============================================================================
		public static bool IsNullObject(
			this object obj)
		{
			if (obj == null)
			{
				return true;
			}

			string type = obj.GetType().Name;

			return type switch
			{
				"String" => string.IsNullOrWhiteSpace((string) obj),
				_ => false,
			};
		}

		#endregion


		#region PRIVATE




		#endregion

	}


}