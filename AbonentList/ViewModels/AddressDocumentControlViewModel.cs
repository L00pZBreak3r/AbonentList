using System.Windows;

namespace AbonentList.ViewModels
{
    internal class AddressDocumentControlViewModel : DocumentControlBaseViewModel
    {
        public AddressDocumentControlViewModel(Window aWindow)
            : base(aWindow, "Адреса")
        {
            GridModel = new AddressGridControlViewModel();
        }

        public AddressGridControlViewModel GridModel { get; }
    }
}
