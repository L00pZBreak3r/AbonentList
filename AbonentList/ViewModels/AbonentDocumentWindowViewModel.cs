using System.Windows;

using AbonentList.Helpers;

namespace AbonentList.ViewModels
{
  internal class AbonentDocumentWindowViewModel : WindowViewModelBase
  {
    public AbonentDocumentWindowViewModel(Window win)
      : base(win)
    {
      DocumentControlModel = new AbonentDocumentControlViewModel(mWindow);
      WindowText = DocumentControlModel.LabelText;
    }

    #region DocumentControlModel

    public AbonentDocumentControlViewModel DocumentControlModel { get; }

    #endregion

  }
}
