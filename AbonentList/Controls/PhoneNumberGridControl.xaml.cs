using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using AbonentList.ViewModels;

namespace AbonentList.Controls
{
    /// <summary>
    /// Interaction logic for PhoneNumberGridControl.xaml
    /// </summary>
    public partial class PhoneNumberGridControl : UserControl
    {
        public static readonly DependencyProperty IsDataGridReadOnlyProperty = DependencyProperty.Register(
            nameof(IsDataGridReadOnly), typeof(bool), typeof(PhoneNumberGridControl), new PropertyMetadata(false));

        public bool IsDataGridReadOnly
        {
            get { return (bool)GetValue(IsDataGridReadOnlyProperty); }
            set { SetValue(IsDataGridReadOnlyProperty, value); }
        }

        private CollectionViewSource mPhoneNumbersViewSource;

        public PhoneNumberGridControl()
        {
            InitializeComponent();
            mPhoneNumbersViewSource = (CollectionViewSource)FindResource(nameof(mPhoneNumbersViewSource));
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (DataContext is GridControlBaseViewModel aViewModel)
            {
                var aCurrentColumn = mPhoneNumbersDataGrid.CurrentColumn;
                aViewModel.SetCurrentColumn(aCurrentColumn?.DisplayIndex ?? -1, aCurrentColumn?.Header);
            }
        }

        private void PhoneNumberGridControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is PhoneNumberGridControlViewModel aViewModel)
            {
                aViewModel.SetCollectionViewSource(mPhoneNumbersViewSource, mPhoneNumbersDataGrid);
            }
        }

    }
}
