using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    public class Rack : Entity
    {
        [XmlAttribute()]
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        [XmlAttribute()]
        /// <summary>
        /// Количество серверов
        /// </summary>
        public int Count { get; set; }

        [XmlAttribute()]
        /// <summary>
        /// Нагрузка, кг
        /// </summary>
        public int Capacity { get; set; }

        public Rack()
        {
            ImageUri = new Uri("Resources/rack.png", UriKind.Relative);
        }
    }
}
