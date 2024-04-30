using System.Windows;

using AbonentList.ViewModels;

namespace AbonentList
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContext = new MainWindowViewModel(this);
    }
  }
}
