namespace WhenToGo.App.Utils
{
    internal static class AppConstants
    {
        public const string HolidayDetailParameter = "HolidayDetailParameter";


        #region Counties

        public static IEnumerable<string> PreferredCounties
        {
            get
            {
                yield return "CH-BE";
                yield return "CH-GE";
                yield return "CH-FR";
                yield return "CH-JU";
                yield return "CH-NE";
                yield return "CH-VD";
                yield return "CH-VS";
            }
        }

        #endregion
    }
}
