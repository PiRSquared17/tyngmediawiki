using System;
using System.Configuration;

namespace Tyng.MediaWiki.Configuration
{
    public sealed class Section : ConfigurationSection
    {
         // Fields
        private static readonly ConfigurationProperty _propSleep = new ConfigurationProperty("sleep", typeof(ApiSleepSettingsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
        private static readonly ConfigurationProperty _propSiteName = new ConfigurationProperty("siteName", typeof(string), "Wikipedia", null, new StringValidator(1), ConfigurationPropertyOptions.None);
        private static readonly ConfigurationProperty _propServer = new ConfigurationProperty("server", typeof(string), "http://en.wikipedia.org", null, new StringValidator(1), ConfigurationPropertyOptions.None);
        private static readonly ConfigurationProperty _propScriptName = new ConfigurationProperty("scriptName", typeof(string), "index.php", null, new StringValidator(1), ConfigurationPropertyOptions.None);
        private static readonly ConfigurationProperty _propScriptPath = new ConfigurationProperty("scriptPath", typeof(string), "/w", null, new StringValidator(1), ConfigurationPropertyOptions.None);
        private static readonly ConfigurationProperty _propApiName = new ConfigurationProperty("apiName", typeof(string), "api.php", null, new StringValidator(1), ConfigurationPropertyOptions.None);
        
        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        // Methods
        static Section()
        {
            _properties.Add(_propSleep);
            _properties.Add(_propSiteName);
            _properties.Add(_propServer);
        }

        protected override void InitializeDefault()
        {
            base.InitializeDefault();

            Sleep.Add(new ApiSleepSettings("query", 1000));
            Sleep.Add(new ApiSleepSettings("edit", 60000));
            Sleep.Add(new ApiSleepSettings("login", 60000));
        }

        protected override object GetRuntimeObject()
        {
            this.SetReadOnly();
            return this;
        }

        // Properties
        [ConfigurationProperty("sleep")]
        public ApiSleepSettingsCollection Sleep
        {
            get
            {
                return (ApiSleepSettingsCollection)base[_propSleep];
            }
        }

        [ConfigurationProperty("siteName", DefaultValue="Wikipedia")]
        public string SiteName
        {
            get
            {
                return (string)base[_propSiteName];
            }
        }

        [ConfigurationProperty("server", DefaultValue = "http://en.wikipedia.org")]
        public string Server
        {
            get
            {
                return (string)base[_propServer];
            }
        }

        [ConfigurationProperty("scriptName", DefaultValue = "index.php")]
        public string ScriptName
        {
            get
            {
                return (string)base[_propScriptName];
            }
        }

        [ConfigurationProperty("apiName", DefaultValue = "api.php")]
        public string ApiName
        {
            get
            {
                return (string)base[_propApiName];
            }
        }

        [ConfigurationProperty("scriptPath", DefaultValue = "/w")]
        public string ScriptPath
        {
            get
            {
                return (string)base[_propScriptPath];
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }
    }
}
