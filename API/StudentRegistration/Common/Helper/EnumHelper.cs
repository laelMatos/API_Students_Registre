using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Common
{
    public static class EnumHelper
    {
        public static string GetDescriptionEnumField(this object field)
        {
            var description = field.ToString();
            var fieldInfo = field.GetType().GetField(field.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }
    }
}
