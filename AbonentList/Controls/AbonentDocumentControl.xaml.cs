using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using AbonentList.ViewModels;

namespace AbonentList.Controls
{
    /// <summary>
    /// Interaction logic for AbonentDocumentControl.xaml
    /// </summary>
    public partial class AbonentDocumentControl : UserControl
    {
        public AbonentDocumentControl()
        {
            InitializeComponent();
        }

        private void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            AbonentDocumentControlViewModel aViewModel = (DataContext as AbonentDocumentControlViewModel)!;
            aViewModel.SaveAndClose();

        }
    }
}
