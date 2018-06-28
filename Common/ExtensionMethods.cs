using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Sobhan.Common
{
    public static class ExtensionMethods
    {
        public static string GetDescription(this Enum value)
        {
            var attributes =
                (DescriptionAttribute[])value.GetType().GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

    }
}