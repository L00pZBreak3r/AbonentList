using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using AbonentList.ViewModels;

namespace AbonentList.Controls
{
    /// <summary>
    /// Interaction logic for AddressGridControl.xaml
    /// </summary>
    public partial class AddressGridControl : UserControl
    {
        public static readonly DependencyProperty IsDataGridReadOnlyProperty = DependencyProperty.Register(
            nameof(IsDataGridReadOnly), typeof(bool), typeof(AddressGridControl), new PropertyMetadata(false));

        public bool IsDataGridReadOnly
        {
            get { return (bool)GetValue(IsDataGridReadOnlyProperty); }
            set { SetValue(IsDataGridReadOnlyProperty, value); }
        }

        private CollectionViewSource mAddressesViewSource;

        public AddressGridControl()
        {
            InitializeComponent();
            mAddressesViewSource = (CollectionViewSource)FindResource(nameof(mAddressesViewSource));
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (DataContext is GridControlBaseViewModel aViewModel)
            {
                var aCurrentColumn = mAddressesDataGrid.CurrentColumn;
                aViewModel.SetCurrentColumn(aCurrentColumn?.DisplayIndex ?? -1, aCurrentColumn?.Header);
            }
        }

        private void AddressGridControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is AddressGridControlViewModel aViewModel)
            {
                aViewModel.SetCollectionViewSource(mAddressesViewSource, mAddressesDataGrid);
            }
        }
    }
}
