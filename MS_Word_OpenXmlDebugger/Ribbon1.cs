using Microsoft.Office.Tools.Ribbon;

namespace OfficeTools {
    public partial class Ribbon1 {
        void ButtonOpenXmlDebugger_Click(object sender, RibbonControlEventArgs e) {
            new XmlWindow().Show();
        }
    }
}