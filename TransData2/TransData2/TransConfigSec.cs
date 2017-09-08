using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TransData2
{
    public class JXCConfig : ConfigurationSection
    {
        [ConfigurationProperty("JXCList", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(JXCSection), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap, RemoveItemName = "remove")]
        public JXCList JXC
        {
            get { return (JXCList)base["JXCList"]; }
            set { base["JXCList"] = value; }
        }

    }
    public class JXCList : ConfigurationElementCollection
    {
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JXCSection)element).JXCName;
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new JXCSection();
        }
        public JXCSection this[int i]
        {
            get
            {
                return (JXCSection)base.BaseGet(i);
            }
        }

        public JXCSection this[string key]
        {
            get
            {
                return (JXCSection)base.BaseGet(key);
            }
        }
    }
    public class JXCSection : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string JXCName
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("con", IsRequired = true, IsKey = true)]
        public string Connection
        {
            get { return (string)base["con"]; }
            set { base["con"] = value; }
        }
        [ConfigurationProperty("shdm", IsRequired = true, IsKey = true)]
        public string SHDM
        {
            get { return (string)base["shdm"]; }
            set { base["shdm"] = value; }
        }
    }
}
