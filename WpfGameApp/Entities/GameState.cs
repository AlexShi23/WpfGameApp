using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace WpfGameApp.Entities
{
    [XmlRoot(Namespace = "http://www.croc.ru", ElementName = "State")]
    public class GameState
    {
        [XmlElement(ElementName = "Server")]
        public Server[] Servers { get; set; }

        public GameState()
        {
            Servers = new Server[7];
        }
        public void Save(string name)
        {
            var ser = new XmlSerializer(GetType());
            var settings = new XmlWriterSettings()
            {
                Indent = true
            };
            using (XmlWriter wrt = XmlWriter.Create(name, settings))
            {
                ser.Serialize(wrt, this);
            }
        }

        public static GameState Load(string name)
        {
            try
            {
                var ser = new XmlSerializer(typeof(GameState));
                using (XmlReader rdr = XmlReader.Create(name))
                {
                    return (GameState)ser.Deserialize(rdr);
                }
            }
            catch (Exception ex)
            {
                return new GameState();
            }
        }
    }
}
