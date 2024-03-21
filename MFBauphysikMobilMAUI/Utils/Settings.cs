// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MFBauphysikMobilMAUI.Utils
{
    /// <summary>
    /// Speichern die letzten Eingaben
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        private const string LastLoginSetting = "last_login_key";
        private static readonly string SettingsDefault = string.Empty;
        #endregion
        public static string LastLogin
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastLoginSetting, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastLoginSetting, value);
            }
        }
        private const string LastPassword = "last_login_pass";
        public static string LastLoginPass
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastPassword, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastPassword, value);
            }
        }
        private const string LastConnectSetting = "last_connect_key";
        public static string LastConnect
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastConnectSetting, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastConnectSetting, value);
            }
        }
        private const string LastPName = "last_projekt_name";
        public static string LastProjekt
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastPName, SettingsDefault);

            }
            set
            {
                AppSettings.AddOrUpdateValue(LastPName, value);

            }
        }
        private const string LastBVErsatz = "last_BV_Ersatz";
        public static string LastBV
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastBVErsatz, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastBVErsatz, value);
            }
        } 
    }
}
