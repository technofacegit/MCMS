using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Reflection;

public static class Extensions
{
    #region "Dictionary"

    public static void AddOrReplace(this IDictionary<string, object> DICT, string key, object value)
    {
        if (DICT.ContainsKey(key))
            DICT[key] = value;
        else
            DICT.Add(key, value);
    }

    public static dynamic GetObjectOrDefault(this IDictionary<string, object> DICT, string key)
    {
        if (DICT.ContainsKey(key))
            return DICT[key];
        else
            return null;
    }

    public static T GetObjectOrDefault<T>(this IDictionary<string, object> DICT, string key)
    {
        if (DICT.ContainsKey(key))
            return (T)Convert.ChangeType(DICT[key], typeof(T));
        else
            return default(T);
    }

    #endregion

    #region "String"

    public static string ToSelfURL(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        string outputStr = text.Trim().Replace(":", "").Replace("&", "").Replace(" ", "-").Replace("'", "").Replace(",", "").Replace("(", "").Replace(")", "").Replace("--", "").Replace(".", "");
        return Regex.Replace(outputStr.Trim().ToLower().Replace("--", ""), "[^a-zA-Z0-9_-]+", "", RegexOptions.Compiled);
    }

    public static string TrimLength(this string input, int length, bool Incomplete = true)
    {
        if (String.IsNullOrEmpty(input)) { return String.Empty; }
        return input.Length > length ? String.Concat(input.Substring(0, length), Incomplete ? "..." : "") : input;
    }

    public static string ToTitle(this string input)
    {
        return String.IsNullOrEmpty(input) ? String.Empty : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
    }

    public static bool ContainsAny(this string input, params string[] values)
    {
        return String.IsNullOrEmpty(input) ? false : values.Any(S => input.Contains(S));
    }

    #endregion

    #region "Collection"

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source != null && source.Count() >= 0)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }
    }

    public static bool IsNotNullAndNotEmpty<T>(this ICollection<T> source)
    {
        return source != null && source.Count() > 0;
    }

    #endregion

}
