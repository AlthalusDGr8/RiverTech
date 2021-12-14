using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TechDemo.MoviesDb.Core.Extentions
{
	public static class Strings
	{
		public static bool In(this string source, params string[] list)
		{
			return list.Any(x => x.EqualsIgnoreCaseAndWhitespace(source));
		}

		public static bool EqualsIgnoreCaseAndWhitespace(this string source, string compare)
		{
			if (source == null && compare == null)
				return true;

			if (source == null || compare == null)
				return false;

			return source.SafeTrim().Equals(compare.SafeTrim(), StringComparison.OrdinalIgnoreCase);
		}

		public static string SafeTrim(this string source)
		{
			return string.IsNullOrEmpty(source) ? source : source.Trim();
		}

		/// <summary>
		/// Removes any special charatacters from a string
		/// </summary>
		/// <param name="source"></param>
		/// <param name="replacementCharacter"></param>
		/// <returns></returns>
		public static string RemoveSpecialCharacters(this string source, string replacementCharacter)
		{
			return Regex.Replace(source, @"[^0-9a-zA-Z\._]", replacementCharacter);
		}
	}
}