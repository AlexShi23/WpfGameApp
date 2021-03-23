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
        [XmlAttribute()]
        public int Storage { get; set; }

        [XmlAttribute()]
        public int Size { get; set; }

        public int Count
        {
            get { return (Size == 0) ? 0 : 6 / Size; }
        }

        public Server()
        {
            ImageUri = new Uri("Resources/server.png", UriKind.Relative);
        }
    }
}
