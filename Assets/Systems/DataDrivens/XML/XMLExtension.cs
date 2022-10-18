using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace DataDriven.XML
{
    public static class XMLExtension
    {
        public static XmlNode GetFirstElementByTagName(this XmlDocument xml, string tagName)
        {
            return xml.GetElementsByTagName(tagName)[0];
        }

    }
}

