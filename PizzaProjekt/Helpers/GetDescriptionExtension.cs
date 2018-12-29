using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PizzaProjekt.Helpers
{
    public static class GetDescriptionExtension
    {
        public static string GetDescription<T>(this T value)
        {
            var type = value.GetType();
            var field = type.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }

        public static IEnumerable<string> GetAllDescriptions(this Type value)
        {
            return value.GetFields()
                .Select(f => (DescriptionAttribute)f.GetCustomAttribute(typeof(DescriptionAttribute)))
                .Where(a => a != null)
                .Select(a => a.Description);
        }

        public static T GetValueFromDescription<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }
    }
}