using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    class Storage : Entity
    {
        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public int Size { get; set; }

        [XmlAttribute()]
        public int Weight { get; set; }

        public Storage()
        {
            ImageUri = new Uri("Resources/storage.png", UriKind.Relative);
        }
    }
}
