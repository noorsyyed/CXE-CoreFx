using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace CXE.CoreFx.Base.Extensions
{
	internal static class StringExtention
	{
		#region EXTENTIONS
		public static string ReplaceDateTemplateInFileName(
			this string fileName)
		{
			var onlyFileName =
				Path.GetFileName(fileName);

			var currentDate = DateTime.Now;

			onlyFileName = onlyFileName.Replace("YYYY", currentDate.Year.ToString("D4"));
			onlyFileName = onlyFileName.Replace("MM", currentDate.Month.ToString("D2"));
			onlyFileName = onlyFileName.Replace("DD", currentDate.Day.ToString("D2"));

			return
				Path.GetDirectoryName(fileName) + @"\" +
				onlyFileName;
		}

		public static bool HasValue(
			this string input)
		{
			return !string.IsNullOrWhiteSpace(input);
		}

		public static bool IsEmpty(
			this string input)
		{
			return string.IsNullOrWhiteSpace(input);
		}

		public static string Shorten(
			this string input,
			int newMaxLength)
		{
			if (input.Length < newMaxLength)
			{
				return input;
			}

			input = input.Substring(0, newMaxLength - 3);
			return input + "...";
		}

		public static string ExtendLength(
			this string input,
			int newLength,
			char fillerChar = ' ')
		{
			if (input.Length > newLength)
			{
				return input;
			}

			return input.PadRight(newLength, fillerChar);
		}

		public static bool HasNonLatinCharacters(
			this string input)
		{
			return Regex.IsMatch(input, "[^a-zA-Z0-9^\\-\\/ &.(),]");
		}

		public static string RemoveHtml(
			this string input)
		{
			var result = input;

			result = RemoveComments(result);
			result = result.Replace("<br>", "\n");
			result = RemoveHtmlTags(result);
			result = WebUtility.HtmlDecode(result);
			result = result.Replace("\n\n", "\n");

			return result;
		}

		/// <summary>
		/// Removes extra spaces in the given string, collapsing multiple spaces into one
		/// and trimming leading/trailing whitespace.
		/// </summary>
		/// <param name="input"></param>
		/// <returns>The cleaned up string</returns>
		public static string RemoveExtraSpaces(
			this string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return string.Empty;
			}

			return Regex.Replace(input.Trim(), "\\s+", " ");
		}

		/// <summary>
		/// Removes all \n and \r characters from the string.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string RemoveLineBreaks(
			this string input)
		{
			input = input.Replace("\r", "");
			input = input.Replace("\n", "");

			return input;
		}
		#endregion

		#region Interal Helper Code
		private static string RemoveComments(
			string source)
		{
			return Regex.Replace(
				source,
				"<!--.*?-->",
				string.Empty,
				RegexOptions.Singleline);
		}

		private static string RemoveHtmlTags(
			string source)
		{
			char[] array = new char[source.Length];
			int arrayIndex = 0;
			bool inside = false;

			for (int i = 0; i < source.Length; i++)
			{
				char let = source[i];
				if (let == '<')
				{
					inside = true;
					continue;
				}
				if (let == '>')
				{
					inside = false;
					continue;
				}
				if (!inside)
				{
					array[arrayIndex] = let;
					arrayIndex++;
				}
			}

			return new string(array, 0, arrayIndex);
		}
		#endregion
	}
}