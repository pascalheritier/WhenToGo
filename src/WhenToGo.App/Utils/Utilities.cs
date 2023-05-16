namespace WhenToGo.App.Utils
{
    internal static class Utilities
    {
        public const string SvgFileExtension = ".svg";
        public const string PngFileExtension = ".png";
        private const string BindingPropertySeparator = ".";


        public static object GetPropertyValue(object source, string propertyName)
        {
            try
            {
                if (propertyName.Contains(BindingPropertySeparator))
                {
                    int splitIndex = propertyName.IndexOf(BindingPropertySeparator);
                    string parentName = propertyName.Substring(0, splitIndex);
                    string childName = propertyName.Substring(splitIndex + 1);
                    object parentInstance = source?.GetType().GetProperty(parentName)?.GetValue(source);
                    return GetPropertyValue(parentInstance, childName);
                }
                return source?.GetType().GetProperty(propertyName)?.GetValue(source);
            }
            catch(Exception e)
            {
                // silent on purpose, binding retrieval was not successful but do not crash the app
                return null;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
    }
}
