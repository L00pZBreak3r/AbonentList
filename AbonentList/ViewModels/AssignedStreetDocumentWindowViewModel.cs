using System.Windows;

using AbonentList.Helpers;

namespace AbonentList.ViewModels
{
  internal class AssignedStreetDocumentWindowViewModel : WindowViewModelBase
  {
    public AssignedStreetDocumentWindowViewModel(Window win)
      : base(win)
    {
      DocumentControlModel = new AssignedStreetDocumentControlViewModel(mWindow);
      WindowText = DocumentControlModel.LabelText;
    }

    #region DocumentControlModel

    public AssignedStreetDocumentControlViewModel DocumentControlModel { get; }

    #endregion

  }
}
