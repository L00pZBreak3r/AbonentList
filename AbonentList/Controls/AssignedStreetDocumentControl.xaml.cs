using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using AbonentList.ViewModels;

namespace AbonentList.Controls
{
    /// <summary>
    /// Interaction logic for AssignedStreetDocumentControl.xaml
    /// </summary>
    public partial class AssignedStreetDocumentControl : UserControl
    {
        public AssignedStreetDocumentControl()
        {
            InitializeComponent();
        }

        private void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            AssignedStreetDocumentControlViewModel aViewModel = (DataContext as AssignedStreetDocumentControlViewModel)!;
            aViewModel.SaveAndClose();
        }
    }
}
