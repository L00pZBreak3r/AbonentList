using System.Windows;

using AbonentList.Helpers;

namespace AbonentList.ViewModels
{
    internal abstract class DocumentControlBaseViewModel : WindowControlViewModelBase
    {
        protected DocumentControlBaseViewModel(Window aWindow, string aLabelText)
            : base(aWindow)
        {
            LabelText = aLabelText;
        }

        public string LabelText { get; }

        public void SaveAndClose()
        {
            CloseWindow(true, true);
        }
    }
}
