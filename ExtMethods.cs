using Mafi.Localization;

namespace COIDataExport
{
    public static class ExtMethods
    {
        public static LocStr GetEnUsStrFromLocStr(this LocStr str)
        {
            return LocalizationManager.LoadLocalizedString0(str.Id);
        }
    }
}