using System;
using System.Configuration;

namespace NInsight.Core.Config
{
    public class NInsightSettings : ConfigurationSection
    {
        private static readonly NInsightSettings settings =
            ConfigurationManager.GetSection("nInsight") as NInsightSettings;

        public static NInsightSettings Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("record", DefaultValue = false, IsRequired = false)]
        public bool Record
        {
            get
            {
                return (bool)this["record"];
            }
            set
            {
                this["record"] = value;
            }
        }

        [ConfigurationProperty("neo4j", IsRequired = false)]
        public Neo4jSettings Neo4j
        {
            get
            {
                return (Neo4jSettings)this["neo4j"];
            }
            set
            {
                this["neo4j"] = value;
            }
        }
    }

    public class Neo4jSettings : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public String Url
        {
            get
            {
                return (String)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }

        [ConfigurationProperty("use", DefaultValue = false, IsRequired = true)]
        public bool Use
        {
            get
            {
                return (bool)this["use"];
            }
            set
            {
                this["use"] = value;
            }
        }
    }
}