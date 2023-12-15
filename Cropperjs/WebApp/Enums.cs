using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WebApp;

public static class EnumExtentions
{
    public static string DisplayName(this Enum value)
    {
        if (!Enum.IsDefined(value.GetType(), value))
            throw new InvalidEnumArgumentException();


        var fieldInfo = value.GetType().GetField(value.ToString())!;

        if (fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)?.FirstOrDefault() is DisplayAttribute attribute)
        {
            //Display属性が定義してある場合（Name=,ResourceType=必須）
            var nameProperty = attribute.ResourceType!.GetProperty(attribute.Name!, BindingFlags.Static | BindingFlags.Public);
            var name = nameProperty!.GetValue(nameProperty.DeclaringType, null) as string;
            return name!;
        }
        else
        {
            //Display属性が定義していなかった場合、Enumの名称を返す
            return value.ToString();
        }

    }
}