using System.ComponentModel;
using System;
using System.Reflection;
using System.Linq;

namespace MatchInfo.API.Utilities
{
    /// <summary>
    /// A class which manage enums
    /// </summary>
    public static class EnumUtils
    {   
        /// <summary>
        /// Gets enum value from category.
        /// </summary>
        /// <typeparam name="T">The T type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T EnumFromCategory<T>(string value, T defaultValue) where T : System.Enum
        {
            System.Type type = typeof(T);

            foreach (Enum en in Enum.GetValues(type))
            {
                var attrs = GetEnumAttributes<CategoryAttribute>(en);
                if (attrs != null && attrs.Length > 0 && attrs.Any(x => x.Category == value))
                    return (T)en;
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets the display name attribute from the enum.
        /// </summary>
        /// <param name="en">The en enum.</param>
        /// <returns></returns>
        public static string Category(this Enum en)
        {
            return EnumToCategory(en);
        }


        /// <summary>
        /// Gets the category attribute from the enum.
        /// </summary>
        /// <param name="en">The en enum.</param>
        /// <returns></returns>
        public static string EnumToCategory(Enum en)
        {
            if (en != null)
            {
                var attr = GetEnumAttributes<CategoryAttribute>(en);
                if (attr != null && attr.Length > 0)
                    return attr.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Category))?.Category;
                else
                    return en.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets all attributes of enum.
        /// </summary>
        /// <typeparam name="T">The T type.</typeparam>
        /// <param name="en">The en enum.</param>
        /// <returns></returns>
        private static T[] GetEnumAttributes<T>(this Enum en) where T : Attribute
        {
            if (en is null) return null;
            FieldInfo fi = en.GetType().GetField(en.ToString());
            T[] attr = fi.GetCustomAttributes(typeof(T), false) as T[];
            return attr;
        }
    }
}
