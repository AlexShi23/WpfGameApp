using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Windows.Forms;

namespace WpfGameApp.Entities
{
    [XmlRoot(ElementName = "State")]
    public class GameState
    {
        [XmlElement(ElementName = "Entity")]
        public List<Entity> Entities { get; set; }

        public GameState()
        {
            Entities = new List<Entity>();
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
                MessageBox.Show($"Сохранение не удалось загрузить!\n{ex.Message}");
                return new GameState();
            }
        }
    }
}
