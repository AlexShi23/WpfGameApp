using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    class KvmConsole : Entity
    {
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public int Count { get; set; }
        [XmlAttribute()]
        public int Weight { get; set; }

        public KvmConsole()
        {
            ImageUri = new Uri("Resources/kvm.png", UriKind.Relative);
        }
    }
}
