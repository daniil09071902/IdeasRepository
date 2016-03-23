using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeasRepository.Utilities
{
    public class EnumExtensions
    {
        public static TEnum GetEnumValue<TEnum>(string str) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum),str);
        }
    }
}