using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.IO;
using System.Text;

using AbonentList.Helpers;

namespace AbonentList.ViewModels
{
    internal abstract class GridControlBaseViewModel : ViewModelBase
    {

        protected CollectionViewSource? mCollectionViewSource;
        protected DataGrid? mDataGrid;
        protected int mDataGridCurrentColumnIndex = -1;
        protected int mDataGridSearchColumnIndexMax;
        protected object? mDataGridCurrentColumnHeader;

        protected GridControlBaseViewModel()
        {
          ExportToCsvCommand = new RelayCommand(OnExportToCsvCommand, ExportToCsvCommandCanExecute);
          IsDataGridReadOnly = true;
        }

        #region IsDataGridReadOnly

        public bool IsDataGridReadOnly { get; }

        #endregion

        #region ExportToCsvCommand

        public ICommand ExportToCsvCommand { get; }

        private bool ExportToCsvCommandCanExecute(object? p)
        {
            ICollectionView? aCollectionView = (mDataGrid != null) ? CollectionViewSource.GetDefaultView(mDataGrid!.ItemsSource) : null;
            return CollectionViewHasData(aCollectionView);
        }

        private void OnExportToCsvCommand(object? p)
        {
            ICollectionView? aCollectionView = (mDataGrid != null) ? CollectionViewSource.GetDefaultView(mDataGrid!.ItemsSource) : null;
            if (CollectionViewHasData(aCollectionView))
            {
                ExportToCsv(aCollectionView!);
            }
        }

        #endregion

        private static bool CollectionViewHasData(ICollectionView? aCollectionView)
        {
            bool aResult = (aCollectionView != null) && !aCollectionView!.IsEmpty;
            if (aResult)
            {
                object? aFirstObject = null;
                foreach (var aRecord in aCollectionView!)
                {
                    aFirstObject = aRecord;
                    break;
                }
                if (aFirstObject == CollectionView.NewItemPlaceholder)
                    aResult = false;
            }
            return aResult;
        }

        private void ExportToCsv(ICollectionView aCollectionView)
        {
            string aDateTimeString = DateTime.Now.ToString("s").Replace('T', '_').Replace(':', '-');
            var aSaveFileDialog = new Microsoft.Win32.SaveFileDialog();
            aSaveFileDialog.FileName = "report_" + aDateTimeString;
            aSaveFileDialog.DefaultExt = ".csv";
            aSaveFileDialog.Filter = "Файлы CSV (.csv)|*.csv";

            bool? result = aSaveFileDialog.ShowDialog();

            if (result == true)
            {
                string filename = aSaveFileDialog.FileName;
                StringBuilder aStringBuilder = new();
                bool aHasLines = false;
                foreach (var aItem in aCollectionView)
                {
                    string aCsvString = GetItemCsvString(aItem);
                    if (!string.IsNullOrEmpty(aCsvString))
                    {
                        aStringBuilder.AppendLine(aCsvString);
                        aHasLines = true;
                    }
                }
                if (aHasLines)
                    File.WriteAllText(filename, aStringBuilder.ToString());
            }
        }

        protected virtual string GetItemCsvString(object? aItem)
        {
            return "";
        }

        protected virtual bool ApplyFilter(object? aItem)
        {
            return true;
        }

        protected virtual string GetColumnHeaderText()
        {
            return ((mDataGridCurrentColumnIndex > 0) && (mDataGridCurrentColumnIndex <= mDataGridSearchColumnIndexMax)) ? (mDataGridCurrentColumnHeader as string) ?? "" : "";
        }

        protected virtual void OnCurrentColumnChanged()
        {
        }

        private void SetCurrentColumnReset(int aCurrentColumnDisplayIndex, object? aCurrentColumnHeader)
        {
            int aDataGridCurrentColumnIndexPrev = mDataGridCurrentColumnIndex;
            mDataGridCurrentColumnIndex = aCurrentColumnDisplayIndex;
            mDataGridCurrentColumnHeader = aCurrentColumnHeader;
            if (aDataGridCurrentColumnIndexPrev != mDataGridCurrentColumnIndex)
                SearchText = "";
            SearchColumnName = GetColumnHeaderText();
            OnCurrentColumnChanged();
        }

        public void SetCurrentColumn(int aCurrentColumnDisplayIndex, object? aCurrentColumnHeader)
        {
            if (aCurrentColumnDisplayIndex >= 0)
                SetCurrentColumnReset(aCurrentColumnDisplayIndex, aCurrentColumnHeader);
        }

        public void SetCollectionViewSource(CollectionViewSource? aCollectionViewSource, DataGrid aDataGrid)
        {
            if (mCollectionViewSource != null)
                mCollectionViewSource.Filter -= CollectionViewSource_Filter;
            mCollectionViewSource = aCollectionViewSource;
            mDataGrid = aDataGrid;
            var aCurrentColumn = mDataGrid.CurrentColumn;
            SetCurrentColumnReset(aCurrentColumn?.DisplayIndex ?? -1, aCurrentColumn?.Header);
            if (mCollectionViewSource != null)
                mCollectionViewSource.Filter += CollectionViewSource_Filter;
        }

        private void CollectionViewSource_Filter(object? sender, FilterEventArgs e)
        {
            e.Accepted = ApplyFilter(e.Item);
        }

        #region SearchText

        private string mSearchText = string.Empty;

        public string SearchText
        {
          get => mSearchText;
          set
          {
            if (mSearchText != value)
            {
              mSearchText = value;
              RaisePropertyChanged();
              
              if (mDataGrid != null)
              {
                ICollectionView aCollectionView = CollectionViewSource.GetDefaultView(mDataGrid!.ItemsSource);
                aCollectionView.Refresh();
                if (!CollectionViewHasData(aCollectionView))
                  MessageBox.Show("Нет записей, удовлетворяющих критерию поиска.", "Поиск", MessageBoxButton.OK, MessageBoxImage.Warning);
              }
            }
          }
        }

        #endregion

        #region SearchColumnName

        private string mSearchColumnName = string.Empty;

        public string SearchColumnName
        {
          get => mSearchColumnName;
          set
          {
            if (mSearchColumnName != value)
            {
              mSearchColumnName = value;
              RaisePropertyChanged();
            }
          }
        }

        #endregion
    }
}
