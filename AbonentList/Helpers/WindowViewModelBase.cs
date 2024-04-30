using System.Windows;

namespace AbonentList.Helpers
{
  internal abstract class WindowViewModelBase : WindowControlViewModelBase
  {
    protected WindowViewModelBase(Window win)
        : base(win)
    {
    }

    #region WindowText

    private string mWindowText = string.Empty;

    public string WindowText
    {
      get => mWindowText;
      set
      {
        if (mWindowText != value)
        {
          mWindowText = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion
  }
}
