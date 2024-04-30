using System.Windows;

using AbonentList.Helpers;

namespace AbonentList.ViewModels
{
  class MainWindowViewModel : WindowViewModelBase
  {
    public MainWindowViewModel(Window win)
      : base(win)
    {
      System.Threading.Thread.CurrentThread.CurrentCulture = CultureHelper.CurrentCultureDefault;
      DocumentControlModel = new MainDocumentControlViewModel(mWindow);
      WindowText = "Список абонентов телефонной компании";
    }

    #region DocumentControlModel

    public MainDocumentControlViewModel DocumentControlModel { get; }

    #endregion
  }
}
