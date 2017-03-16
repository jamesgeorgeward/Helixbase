namespace Helixbase.Extensions.HideFields
{
    public class Settings
    {
        public static bool HidingEmptySections
        {
            get { return Sitecore.Configuration.Settings.GetBoolSetting("Extensions.HideFields.HideEmptySections", true); }
        }
    }
}
