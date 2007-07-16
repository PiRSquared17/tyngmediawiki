using System;
using System.Configuration;
using Tyng.MediaWiki.Configuration;

namespace Tyng.MediaWiki
{
    internal static class Config
    {
        public static Configuration.ApiSleepSettingsCollection Sleep
        {
            get
            {
                if (Section == null) return null;
                return Section.Sleep;
            }
        }

        private static Section Section
        {
            get
            {
                return (Configuration.Section)ConfigurationManager.GetSection("mediaWiki");                
            }
        }

        public static string SiteName
        {
            get
            {
                if (Section == null) return null;
                return Section.SiteName;
            }
        }

        public static string Server
        {
            get
            {
                if (Section == null) return null;
                return Section.Server;
            }
        }

        public static string ScriptName
        {
            get
            {
                if (Section == null) return null;
                return Section.ScriptName;
            }
        }

        public static string ScriptPath
        {
            get
            {
                if (Section == null) return null;
                return Section.ScriptPath;
            }
        }

        public static string ApiName
        {
            get
            {
                if (Section == null) return null;
                return Section.ApiName;
            }
        }
    }
}