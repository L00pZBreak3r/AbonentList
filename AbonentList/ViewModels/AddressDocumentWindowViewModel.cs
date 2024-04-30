using System.Windows;

using AbonentList.Helpers;

namespace AbonentList.ViewModels
{
  internal class AddressDocumentWindowViewModel : WindowViewModelBase
  {
    public AddressDocumentWindowViewModel(Window win)
      : base(win)
    {
      DocumentControlModel = new AddressDocumentControlViewModel(mWindow);
      WindowText = DocumentControlModel.LabelText;
    }

    #region DocumentControlModel

    public AddressDocumentControlViewModel DocumentControlModel { get; }

    #endregion

  }
}
