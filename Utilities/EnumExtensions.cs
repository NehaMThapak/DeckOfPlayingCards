using System;
using System.ComponentModel;

namespace DeckOfPlayingCards.Core.Utilities
{
    /// <summary>
    /// Extension for the Enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the attribute value from an enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="enumValue">Input value of the enum you want to get attribute.</param>
        /// <returns>Value of the attribute if exists. Otherwise returns null.</returns>
        public static T GetAttribute<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());
            return memberInfo[0].GetAttribute<T>();
        }

        /// <summary>
        /// Get the [Description] attribute for an enumValue.
        /// </summary>
        /// <param name="enumValue">Input value of the enum.</param>
        /// <returns>Text defined in [Description] attribute.</returns>
        public static string GetDescription(this Enum enumValue)
        {
            var descriptionAttribute = enumValue.GetAttribute<DescriptionAttribute>();
            return descriptionAttribute == null ? string.Empty : descriptionAttribute.Description;
        }

        /// <summary>
        /// Parsing a long value to an enum type. 
        /// It will throw exception if the input value is not 
        /// in the defined range of enum type.
        /// </summary>
        /// <typeparam name="T">Type of enum, it should be defined long type.</typeparam>
        /// <param name="value">Input value to be matched to enum value.</param>
        /// <returns>Enum value.</returns>
        public static T ParseEnum<T>(this long value) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException($"Invalid enum Type '{typeof(T)}'.");

            T result;
            if (!Enum.TryParse(value.ToString(), out result))
                throw new ArgumentException($"'{value}' is not a defined value for enum type '{typeof(T).FullName}'.");
            
            // Check value is defined in the enum or not.
            if (!Enum.IsDefined(typeof(T), result))
                throw new ArgumentException($"'{value}' is not an underlying value for enum type '{typeof(T).FullName}'.");

            return result;
        }

        /// <summary>
        /// Convert the enum value to long.
        /// If the underlying type of input enum is not long, it will throw exception.
        /// </summary>
        /// <param name="value">An enum value.</param>
        /// <returns>A long number matching a value of the enum.</returns>
        public static long ToLong(this Enum value)
        {
            var underlyingType = Enum.GetUnderlyingType(value.GetType());
            if (underlyingType != typeof(long))
                throw new ArgumentException(nameof(value), value.ToString(), null);

            return (long)(object)value;
        }
    }
}
