using Mafi.Localization;

namespace COIWorldMapChange
{
    public static class ExtMethods
    {
        public static LocStr GetEnUsStrFromLocStr(this LocStr str)
        {
            return LocalizationManager.LoadLocalizedString0(str.Id);
        }
    }
}