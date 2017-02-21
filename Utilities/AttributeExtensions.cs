using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeckOfPlayingCards.Core.Utilities
{
    /// <summary>
    /// A set of extension methods relating to custom attributes.
    /// </summary>
    public static class AttributeExtensions
    {
        /// <summary>
        /// Get a custom attribute of a given type if it exists on this item.
        /// </summary>
        /// <typeparam name="T">Attribute type.</typeparam>
        /// <param name="provider">Attribute provider.</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this ICustomAttributeProvider provider)
            where T : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(T), true);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        /// <summary>
        /// Get custom attributes of a given type if it exists on this item.
        /// </summary>
        /// <typeparam name="T">Attribute type.</typeparam>
        /// <param name="provider">Attribute provider.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider provider)
            where T : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(T), true);
            return attributes.Cast<T>();
        }

        /// <summary>
        /// Determine if a custom attribute of a given type exists on this item.
        /// </summary>
        /// <typeparam name="T">Attribute type.</typeparam>
        /// <param name="provider">Attribute provider.</param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this ICustomAttributeProvider provider)
            where T : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(T), true);
            return attributes.Length > 0;
        }
    }
}
