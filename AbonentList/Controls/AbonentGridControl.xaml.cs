using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using AbonentList.ViewModels;

namespace AbonentList.Controls
{
    /// <summary>
    /// Interaction logic for AbonentGridControl.xaml
    /// </summary>
    public partial class AbonentGridControl : UserControl
    {
        public static readonly DependencyProperty IsDataGridReadOnlyProperty = DependencyProperty.Register(
            nameof(IsDataGridReadOnly), typeof(bool), typeof(AbonentGridControl), new PropertyMetadata(false));

        public bool IsDataGridReadOnly
        {
            get { return (bool)GetValue(IsDataGridReadOnlyProperty); }
            set { SetValue(IsDataGridReadOnlyProperty, value); }
        }

        private CollectionViewSource mAbonentsViewSource;

        public AbonentGridControl()
        {
            InitializeComponent();
            mAbonentsViewSource = (CollectionViewSource)FindResource(nameof(mAbonentsViewSource));
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (DataContext is GridControlBaseViewModel aViewModel)
            {
                var aCurrentColumn = mAbonentsDataGrid.CurrentColumn;
                aViewModel.SetCurrentColumn(aCurrentColumn?.DisplayIndex ?? -1, aCurrentColumn?.Header);
            }
        }

        private void AbonentGridControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is AbonentGridControlViewModel aViewModel)
            {
                aViewModel.SetCollectionViewSource(mAbonentsViewSource, mAbonentsDataGrid);
            }
        }
    }
}
