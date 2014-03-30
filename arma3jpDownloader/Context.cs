using System.Xml.Serialization;

namespace arma3jpDownloader {
    public class Key {
        [XmlAttribute("name")]
        public string ID { get; set; }
        [XmlElement]
        public string[] feedKey { get; set; }
    }

    [XmlRoot("context")]
    public class Context {
        public bool preserve;
        public string username { get; set; }
        public string password { get; set; }
        public string feedBaseURI { get; set; }
        public Key[] keys { get; set; }
    }
}
