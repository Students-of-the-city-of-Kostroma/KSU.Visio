using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KSU.Visio.Lib.Xml
{
    public static class XmlConvert
    {

        public static XmlNode ToXmlNode(XmlDocument xml, Size size, string name = "Size", string p1 = "width", string p2 = "height")
        {
            return ToXmlNode(xml, new Point(size), name, p1, p2);
        }

        public static XmlNode ToXmlNode(XmlDocument xml, Point point, string name = "Location", string p1 = "x", string p2 ="y")
        {
            XmlAttribute xXml = xml.CreateAttribute(p1);
            xXml.Value = point.X.ToString();

            XmlAttribute yXml = xml.CreateAttribute(p2);
            yXml.Value = point.Y.ToString();
            

            XmlNode pointXML = xml.CreateNode(XmlNodeType.Element, name, "");
            pointXML.Attributes.Append(xXml);
            pointXML.Attributes.Append(yXml);

            return pointXML;
        }
        public static Point XmlNodeToPoint(XmlNode locationXML, string x = "x", string y = "y")
        {
            return (locationXML == null) ? new Point(100,100) : new Point(
                int.Parse(locationXML.Attributes[x].Value),
                int.Parse(locationXML.Attributes[y].Value));
        }
        public static Size XmlNodeToSize(XmlNode sizeXML, string width = "width", string height = "height")
        {
            return (sizeXML == null) ? new Size(100,100) : new Size(
                int.Parse(sizeXML.Attributes[width].Value), 
                int.Parse(sizeXML.Attributes[height].Value));
        }
    }
}
