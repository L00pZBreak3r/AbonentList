using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using AbonentList.ViewModels;

namespace AbonentList.Controls
{
    /// <summary>
    /// Interaction logic for AssignedStreetGridControl.xaml
    /// </summary>
    public partial class AssignedStreetGridControl : UserControl
    {
        public static readonly DependencyProperty IsDataGridReadOnlyProperty = DependencyProperty.Register(
            nameof(IsDataGridReadOnly), typeof(bool), typeof(AssignedStreetGridControl), new PropertyMetadata(false));

        public bool IsDataGridReadOnly
        {
            get { return (bool)GetValue(IsDataGridReadOnlyProperty); }
            set { SetValue(IsDataGridReadOnlyProperty, value); }
        }

        private CollectionViewSource mAssignedStreetsViewSource;

        public AssignedStreetGridControl()
        {
            InitializeComponent();
            mAssignedStreetsViewSource = (CollectionViewSource)FindResource(nameof(mAssignedStreetsViewSource));
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (DataContext is GridControlBaseViewModel aViewModel)
            {
                var aCurrentColumn = mAssignedStreetsDataGrid.CurrentColumn;
                aViewModel.SetCurrentColumn(aCurrentColumn?.DisplayIndex ?? -1, aCurrentColumn?.Header);
            }
        }

        private void AssignedStreetGridControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is AssignedStreetGridControlViewModel aViewModel)
            {
                aViewModel.SetCollectionViewSource(mAssignedStreetsViewSource, mAssignedStreetsDataGrid);
            }
        }
    }
}
