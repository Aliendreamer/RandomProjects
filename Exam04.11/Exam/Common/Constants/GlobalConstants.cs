namespace Panda.Infrastructure.Constants
{
    public static class GlobalConstants
    {
        public static class Constants
        {
            public const decimal MultiplierConst = 2.67m;

            public const string DatetimeFormat = "M/d/yyyy";
            public const string PendingStatusDate = "N/A";
        }

        public static class Error
        {
            public const string PackageResolverAcquiredError = "Acquired packages shouldn't be accessible";
            public const string PackageResolverADefaultError = "Something went wrong with admin options";
        }

        public static class ViewSetup
        {
            public const string IsAdmin = "IsAdmin";
            public const string IsLogged = "IsLogged";
            public const string NotLogged = "NotLogged";
            public const string NotAdmin = "NotAdmin";
        }

        public static class Display
        {
            public const string DisplayNone = "none";
            public const string DisplayBlock = "block";
        }
    }
}