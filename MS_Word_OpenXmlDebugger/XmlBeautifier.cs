using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace OfficeTools {
    class XmlBeautifier {
        public XmlBeautifier(string xml, bool removeRsids) {
            _document = XDocument.Parse(xml);
            if (removeRsids) {
                RemoveRsids(_document.Root);
            }
        }

        readonly XDocument _document;

        public string ToFormattedXml() {
            StringBuilder stringBuilder = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings {
                OmitXmlDeclaration = true,
                Indent = true,
                NewLineOnAttributes = true,
                IndentChars = "    "
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings)) {
                _document.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }

        static void RemoveRsids(XElement xElement) {
            string[] attributesToDelete = { "rsidR", "rsidP", "rsidRDefault" };
            foreach (string attribute in attributesToDelete) {
                xElement.Attribute(Namespaces.W + attribute)?.Remove();
            }
            foreach (XElement elt in xElement.Elements()) {
                RemoveRsids(elt);
            }
        }
    }
}
