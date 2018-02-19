using System;

namespace Smooth.IoC.UnitOfWork.Helpers
{
    public class EnumHelper
    {
        public static T ConvertIntToEnum<T>(int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ConvertEnumToEnum<T>(object value)
        {
            return (T)Enum.ToObject(typeof(T),(int) value);
        }
    }
}
