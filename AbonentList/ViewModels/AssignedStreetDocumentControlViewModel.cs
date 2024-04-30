using System.Windows;

namespace AbonentList.ViewModels
{
    internal class AssignedStreetDocumentControlViewModel : DocumentControlBaseViewModel
    {
        public AssignedStreetDocumentControlViewModel(Window aWindow)
            : base(aWindow, "Обслуживаемые улицы")
        {
            GridModel = new AssignedStreetGridControlViewModel();
        }

        public AssignedStreetGridControlViewModel GridModel { get; }
    }
}
