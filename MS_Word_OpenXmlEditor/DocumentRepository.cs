using System;

namespace OfficeTools {
    class DocumentRepository {
        readonly InteropHelper _interopHelper = new InteropHelper();

        public string Read(bool bodyOnly, bool removeRsids) =>
            Read(_interopHelper.GetSelectedRangeXml(), bodyOnly, removeRsids);

        public string ReadWholeDocument(bool bodyOnly, bool removeRsids) =>
            Read(_interopHelper.GetDocumentXml(), bodyOnly, removeRsids);

        static string Read(string xml, bool bodyOnly, bool removeRsids) {
            string result = bodyOnly ? XmlTools.GetBody(xml).ToString() : xml;
            return new XmlBeautifier(result, bodyOnly && removeRsids).ToFormattedXml();
        }

        public void Write(string xml) =>
            _interopHelper.SetSelectedRangeXml(PrepareXml(xml, _interopHelper.GetSelectedRangeXml));

        public void WriteWholeDocument(string xml) =>
            _interopHelper.SetDocumentXml(PrepareXml(xml, _interopHelper.GetSelectedRangeXml));

        string PrepareXml(string xml, Func<string> getCurrentXml) {
            string fullXml = xml;
            if (XmlTools.IsBodyOnly(xml)) {
                fullXml = XmlTools.ReplaceBody(getCurrentXml(), xml);
            }
            return fullXml;
        }
    }
}