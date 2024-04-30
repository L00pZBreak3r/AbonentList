using System;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

using AbonentList.Helpers;
using AbonentList.Windows;

namespace AbonentList.ViewModels
{
  internal class MainDocumentControlViewModel : WindowControlViewModelBase
  {
    private AbonentDocumentWindow? mAbonentDocumentWindow;
    private PhoneNumberDocumentWindow? mPhoneNumberDocumentWindow;
    private AddressDocumentWindow? mAddressDocumentWindow;
    private AssignedStreetDocumentWindow? mAssignedStreetDocumentWindow;

    public MainDocumentControlViewModel(Window win)
      : base(win)
    {
        LoadAbonentListCommand = new RelayCommand(OnLoadAbonentListCommand, LoadAbonentListCommandCanExecute);
    }

    #region AbonentListServiceUrl

    private string mAbonentListServiceUrl = string.Empty;

    public string AbonentListServiceUrl
    {
      get => mAbonentListServiceUrl;
      set
      {
        if (mAbonentListServiceUrl != value)
        {
          mAbonentListServiceUrl = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion

    #region AbonentListIsAvailable

    private bool mAbonentListIsAvailable;

    public bool AbonentListIsAvailable
    {
      get => mAbonentListIsAvailable;
      set
      {
        if (mAbonentListIsAvailable != value)
        {
          mAbonentListIsAvailable = value;
          RaisePropertyChanged();
        }
      }
    }

    #endregion

    #region LoadAbonentListCommand

    public ICommand LoadAbonentListCommand { get; }

    private bool LoadAbonentListCommandCanExecute(object? p)
    {
        return !string.IsNullOrEmpty(AbonentListServiceUrl);
    }

    private void OnLoadAbonentListCommand(object? p)
    {
        Task.Run(LoadAbonentListAsync);
    }

    #endregion

    private async Task LoadAbonentListAsync()
    {
        AbonentListIsAvailable = await DatabaseHelper.RetrieveAbonentsAsync(AbonentListServiceUrl);
    }

    public void ShowAbonentDocumentWindow()
    {
      if (mAbonentDocumentWindow == null)
      {
        mAbonentDocumentWindow = new AbonentDocumentWindow();
        mAbonentDocumentWindow.Owner = mWindow;
        mAbonentDocumentWindow.DataContext = new AbonentDocumentWindowViewModel(mAbonentDocumentWindow);
        mAbonentDocumentWindow.Closed += MAbonentDocumentWindow_Closed;
        mAbonentDocumentWindow.ShowDialog();
      }
    }

    public void ShowPhoneNumberDocumentWindow()
    {
      if (mPhoneNumberDocumentWindow == null)
      {
        mPhoneNumberDocumentWindow = new PhoneNumberDocumentWindow();
        mPhoneNumberDocumentWindow.Owner = mWindow;
        mPhoneNumberDocumentWindow.DataContext = new PhoneNumberDocumentWindowViewModel(mPhoneNumberDocumentWindow);
        mPhoneNumberDocumentWindow.Closed += MPhoneNumberDocumentWindow_Closed;
        mPhoneNumberDocumentWindow.ShowDialog();
      }
    }

    public void ShowAddressDocumentWindow()
    {
      if (mAddressDocumentWindow == null)
      {
        mAddressDocumentWindow = new AddressDocumentWindow();
        mAddressDocumentWindow.Owner = mWindow;
        mAddressDocumentWindow.DataContext = new AddressDocumentWindowViewModel(mAddressDocumentWindow);
        mAddressDocumentWindow.Closed += MAddressDocumentWindow_Closed;
        mAddressDocumentWindow.ShowDialog();
      }
    }

    public void ShowAssignedStreetDocumentWindow()
    {
      if (mAssignedStreetDocumentWindow == null)
      {
        mAssignedStreetDocumentWindow = new AssignedStreetDocumentWindow();
        mAssignedStreetDocumentWindow.Owner = mWindow;
        mAssignedStreetDocumentWindow.DataContext = new AssignedStreetDocumentWindowViewModel(mAssignedStreetDocumentWindow);
        mAssignedStreetDocumentWindow.Closed += MAssignedStreetDocumentWindow_Closed;
        mAssignedStreetDocumentWindow.ShowDialog();
      }
    }


    private void MAbonentDocumentWindow_Closed(object? sender, EventArgs e)
    {
      mAbonentDocumentWindow = null;
    }

    private void MPhoneNumberDocumentWindow_Closed(object? sender, EventArgs e)
    {
      mPhoneNumberDocumentWindow = null;
    }

    private void MAddressDocumentWindow_Closed(object? sender, EventArgs e)
    {
      mAddressDocumentWindow = null;
    }

    private void MAssignedStreetDocumentWindow_Closed(object? sender, EventArgs e)
    {
      mAssignedStreetDocumentWindow = null;
    }
  }
}
