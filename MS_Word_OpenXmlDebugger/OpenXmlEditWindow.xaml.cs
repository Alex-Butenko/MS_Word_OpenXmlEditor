namespace OfficeTools {
    public partial class XmlWindow {
        public XmlWindow() {
            InitializeComponent();
            DataContext = new OpenXmlEditModel();
        }
    }
}