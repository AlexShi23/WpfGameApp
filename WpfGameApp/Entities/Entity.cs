using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    [XmlInclude(typeof(Server))]
    [XmlInclude(typeof(Rack))]
    [XmlInclude(typeof(KvmConsole))]
    [XmlInclude(typeof(NetworkSwitch))]
    [XmlInclude(typeof(Storage))]
    public abstract class Entity
    {
        [XmlIgnore()]
        public Uri ImageUri { get; set; }
    }
}
