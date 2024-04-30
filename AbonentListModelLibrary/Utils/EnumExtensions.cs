using System;
using System.ComponentModel;

using AbonentListModelLibrary.Enums;

namespace AbonentListModelLibrary.Utils;

public static class EnumExtensions
{
    private static T? GetAttribute<T>(this Enum aValue) where T : Attribute
    {
        var type = aValue.GetType();
        var memberInfo = type.GetMember(aValue.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
        return attributes.Length > 0 
          ? (T)attributes[0]
          : null;
    }

    public static string? GetDescription(this EPhoneNumberType aValue)
    {
        var aAttribute = aValue.GetAttribute<DescriptionAttribute>();
        return aAttribute?.Description;
    }
}
