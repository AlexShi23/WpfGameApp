using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Карточка Сервер
    /// </summary>
    public class Server : Entity
    {
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute(AttributeName ="CPU")]
        public int CPUs { get; set; }
        [XmlAttribute()]
        public int Weight { get; set; }
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
        [XmlAttribute()]
        public int Size { get; set; }

        public int Count
        {
            get { return (Size == 0) ? 0 : 6 / Size; }
        }

        [XmlAttribute()]
        public string Image { get; set; }
    }
}
