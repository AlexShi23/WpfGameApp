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
        public int price;
        [XmlAttribute()]
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Цена не может быть отрицательной");
                }
                price = value;
            }
        }
        [XmlIgnore()]
        public Uri ImageUri { get; set; }
    }
}
