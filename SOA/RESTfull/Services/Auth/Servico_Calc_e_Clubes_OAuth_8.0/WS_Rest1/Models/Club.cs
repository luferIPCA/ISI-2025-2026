using System.Xml.Serialization;

namespace WS_Rest1.Models
{
    [Serializable]
    public class Club
    {
        string name;

        public Club() { }

        public Club(string n)
        {
            name = n;
        }

        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
