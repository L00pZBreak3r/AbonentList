using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using AbonentList.ViewModels;

namespace AbonentList.Controls
{
    /// <summary>
    /// Interaction logic for PhoneNumberDocumentControl.xaml
    /// </summary>
    public partial class PhoneNumberDocumentControl : UserControl
    {
        public PhoneNumberDocumentControl()
        {
            InitializeComponent();
        }

        private void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumberDocumentControlViewModel aViewModel = (DataContext as PhoneNumberDocumentControlViewModel)!;
            aViewModel.SaveAndClose();
        }
    }
}
