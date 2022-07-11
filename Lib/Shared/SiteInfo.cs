using System;

namespace Blazor_App.Shared
{
    public class SiteInfo
    {

        //coderbasket
        public const string SiteName = "Christian Nations";
        public const string SiteUrlShort = "christian-nations";
        public const string SiteUrl = "https://christian-nations.github.io/";
        public const string SiteFullUrl = SiteUrl;
        public const string Email = "amazingview4@gmail.com";

        //Social
        public static string Twitter = "https://twitter.com/" + SiteUrlShort.Replace(".com", "");
        public static string TwitterAt = "@" + SiteUrlShort;
        public static string TextColor = "#000000";
        public static bool IsDarkTheme = false;
        private const string DefaultAccent = "#EA3D53"; //"#21bb9d";
        public const string AccentDarkerColor = "#B12B3D";
        public static string AccentColor = DefaultAccent;
        public const string BackgroundColor = "white";
        public const string BarBacgroundColor = "#f5f5f5 ";

        public static LanguageType Language = LanguageType.English;
        public static string Title = SiteInfo.Language.ToString() + " Christian Songs";
        public static string ThemeColor = AccentColor;
        //Ads
        public const string UnitId = null;
        public static string GetHeader()
        {
            if (IsDarkTheme)
            {
                TextColor = "#FFFFFF";
            }
            else
            {
                TextColor = "#000000";
            }
            Title = SiteInfo.Language.ToString() + " Christian Songs";
            return SiteInfo.Language.ToString() + " Christian Songs";
        }
        public static event EventHandler InfoChanged;
        public static void NotifyChanged()
        {
            InfoChanged?.Invoke(null, EventArgs.Empty);
        }
#if DEBUG
        public static bool IsDebug = true;
#else
        public static bool IsDebug = false;
#endif
    }
    public enum LanguageType
    {
        English = 1,
        Filipino = 2,
        Cebuano = 3,
        French = 4,
    }
}


