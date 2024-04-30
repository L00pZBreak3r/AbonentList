using System.Windows;

namespace AbonentList.ViewModels
{
    internal class PhoneNumberDocumentControlViewModel : DocumentControlBaseViewModel
    {
        public PhoneNumberDocumentControlViewModel(Window aWindow)
            : base(aWindow, "Телефоны")
        {
            GridModel = new PhoneNumberGridControlViewModel();
        }

        public PhoneNumberGridControlViewModel GridModel { get; }

    }
}
