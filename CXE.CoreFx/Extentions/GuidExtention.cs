using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// ============================================================================
// ============================================================================
// ============================================================================
namespace CXE.CoreFx.Base.Extensions
{
    internal static class GuidExtention
    {

        #region EXTENTIONS

        // ============================================================================
        public static string ToCleanString(
            this Guid input)
        {
            return
                input
                    .ToString()
                    .Replace("{", "")
                    .Replace("}", "")
                    .ToLower()
                    .Trim();
        }

        #endregion


        #region PRIVATE




        #endregion
    }
}
