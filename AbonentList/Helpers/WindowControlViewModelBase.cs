using System.Windows;

namespace AbonentList.Helpers
{
  internal abstract class WindowControlViewModelBase : ViewModelBase
  {
    protected readonly Window mWindow;

    protected WindowControlViewModelBase(Window win)
    {
      mWindow = win;
    }

    #region CloseWindow

    public virtual void CloseWindow(bool setDialogResult = false, bool dialogResult = false)
    {
      if (mWindow != null)
      {
        if (setDialogResult)
          mWindow.DialogResult = dialogResult;
        else
          mWindow.Close();
      }
    }

    #endregion
  }
}
