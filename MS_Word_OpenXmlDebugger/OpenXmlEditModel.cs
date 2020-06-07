using System;
using System.ComponentModel;
using System.Windows.Input;

namespace OfficeTools {
    class OpenXmlEditModel : INotifyPropertyChanged {
        public OpenXmlEditModel() {
            Read = new Command(_ => Xml = _documentRepository.Read(ShowBodyOnly, RemoveRsids));
            Write = new Command(_ => _documentRepository.Write(Xml));
            ReadWholeDocument = new Command(_ => Xml = _documentRepository.ReadWholeDocument(ShowBodyOnly, RemoveRsids));
            WriteWholeDocument = new Command(_ => _documentRepository.WriteWholeDocument(Xml));

            Xml = _documentRepository.Read(ShowBodyOnly, RemoveRsids);
        }

        public ICommand Read { get; }
        public ICommand ReadWholeDocument { get; }
        public ICommand Write { get; }
        public ICommand WriteWholeDocument { get; }

        readonly DocumentRepository _documentRepository = new DocumentRepository();

        string _xml;
        public string Xml {
            get => _xml;
            set {
                _xml = value;
                NotifyPropertyChanged(nameof(Xml));
            }
        }

        bool _removeRsids;
        public bool RemoveRsids {
            get => _removeRsids;
            set {
                _removeRsids = value;
                NotifyPropertyChanged(nameof(RemoveRsids));
            }
        }

        bool _showBodyOnly;
        public bool ShowBodyOnly {
            get => _showBodyOnly;
            set {
                _showBodyOnly = value;
                NotifyPropertyChanged(nameof(ShowBodyOnly));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        class Command : ICommand {
            public Command(Action<object> method) => _method = method;

            readonly Action<object> _method;

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object _) => true;

            public void Execute(object arg) => _method(arg);
        }
    }
}