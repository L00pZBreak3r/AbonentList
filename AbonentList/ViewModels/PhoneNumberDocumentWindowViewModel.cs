using System.Windows;

using AbonentList.Helpers;

namespace AbonentList.ViewModels
{
  internal class PhoneNumberDocumentWindowViewModel : WindowViewModelBase
  {
    public PhoneNumberDocumentWindowViewModel(Window win)
      : base(win)
    {
      DocumentControlModel = new PhoneNumberDocumentControlViewModel(mWindow);
      WindowText = DocumentControlModel.LabelText;
    }

    #region DocumentControlModel

    public PhoneNumberDocumentControlViewModel DocumentControlModel { get; }

    #endregion

  }
}
