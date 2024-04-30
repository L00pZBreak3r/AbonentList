using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using AbonentList.ViewModels;

namespace AbonentList.Controls
{
  /// <summary>
  /// Interaction logic for MainDocumentControl.xaml
  /// </summary>
  public partial class MainDocumentControl : UserControl
  {
    public MainDocumentControl()
    {
      InitializeComponent();
    }

    private void ShowAbonentDocumentWindowButton_Click(object sender, RoutedEventArgs e)
    {
      (DataContext as MainDocumentControlViewModel)!.ShowAbonentDocumentWindow();
    }

    private void ShowPhoneNumberDocumentWindowButton_Click(object sender, RoutedEventArgs e)
    {
      (DataContext as MainDocumentControlViewModel)!.ShowPhoneNumberDocumentWindow();
    }

    private void ShowAddressDocumentWindowButton_Click(object sender, RoutedEventArgs e)
    {
      (DataContext as MainDocumentControlViewModel)!.ShowAddressDocumentWindow();
    }

    private void ShowAssignedStreetDocumentWindowButton_Click(object sender, RoutedEventArgs e)
    {
      (DataContext as MainDocumentControlViewModel)!.ShowAssignedStreetDocumentWindow();
    }

  }
}
