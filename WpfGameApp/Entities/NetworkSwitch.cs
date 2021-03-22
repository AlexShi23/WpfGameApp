using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    class NetworkSwitch : Entity
    {
        [XmlAttribute()]
        public string Name { get; set; }

        [XmlAttribute()]
        public int Count { get; set; }

        [XmlAttribute()]
        public int Weight { get; set; }

        private int price;
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

        public NetworkSwitch()
        {
            ImageUri = new Uri("Resources/networkSwitch.png", UriKind.Relative);
        }

    }
}
