using Microsoft.Office.Interop.Word;

namespace OfficeTools {
    class InteropHelper {
        Range SelectedRange => Globals.ThisAddIn.Application.Selection.Range;

        Document ActiveDocument => Globals.ThisAddIn.Application.ActiveDocument;

        public string GetDocumentXml() => ActiveDocument.WordOpenXML;

        public string GetSelectedRangeXml() => SelectedRange.WordOpenXML;

        public void SetDocumentXml(string xml) => ActiveDocument.Range().InsertXML(xml);

        public void SetSelectedRangeXml(string xml) => SelectedRange.InsertXML(xml);
    }
}