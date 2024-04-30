using System.Windows;

namespace AbonentList.ViewModels
{
    internal class AbonentDocumentControlViewModel : DocumentControlBaseViewModel
    {
        public AbonentDocumentControlViewModel(Window aWindow)
            : base(aWindow, "Абоненты")
        {
            GridModel = new AbonentGridControlViewModel();
        }

        public AbonentGridControlViewModel GridModel { get; }
    }
}
