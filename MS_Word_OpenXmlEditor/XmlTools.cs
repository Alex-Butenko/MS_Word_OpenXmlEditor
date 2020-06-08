using System.Linq;
using System.Xml.Linq;
using static OfficeTools.Namespaces;

namespace OfficeTools {
    static class XmlTools {
        public static XElement GetBody(string xml) =>
            FindDocumentElement(xml)?.Element(W + "body");

        public static string ReplaceBody(string fullXml, string body) {
            XElement bodyElement = GetBody(fullXml);
            XDocument document = bodyElement.Document;
            bodyElement.ReplaceWith(GetRoot(body));
            return document?.ToString();
        }

        public static bool IsBodyOnly(string xml) => GetRoot(xml).Name == W + "body";

        static XElement GetRoot(string xml) => XDocument.Parse(xml).Root;

        static XElement FindDocumentElement(string xml) =>
            GetRoot(xml)
                ?.Elements()
                .FirstOrDefault(p => (string)p.Attribute(Pkg + "name") == "/word/document.xml")
                ?.Element(Pkg + "xmlData")
                ?.Element(W + "document");
    }
}