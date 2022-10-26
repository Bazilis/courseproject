using DAL.Entities;

namespace BLL.ExtensionMethods
{
    public static class StringExtensions
    {
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            return Enum.TryParse(value, true, out T result) ? result : defaultValue;
        }

        public static CollectionTypes ToCollectionTypesEnum(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return CollectionTypes.NoType;

            return Enum.TryParse(value, true, out CollectionTypes result) ? result : CollectionTypes.NoType;
        }
    }
}
