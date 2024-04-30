using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using AbonentList.ViewModels;

namespace AbonentList.Controls
{
    /// <summary>
    /// Interaction logic for AddressDocumentControl.xaml
    /// </summary>
    public partial class AddressDocumentControl : UserControl
    {
        public AddressDocumentControl()
        {
            InitializeComponent();
        }

        private void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            AddressDocumentControlViewModel aViewModel = (DataContext as AddressDocumentControlViewModel)!;
            aViewModel.SaveAndClose();

        }
    }
}
