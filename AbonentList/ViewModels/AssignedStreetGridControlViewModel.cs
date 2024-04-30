using System;
using System.Collections.ObjectModel;

using AbonentList.Helpers;
using AbonentListModelLibrary.Models;

namespace AbonentList.ViewModels
{
    internal class AssignedStreetGridControlViewModel : GridControlBaseViewModel
    {
        #region AssignedStreets

        public ObservableCollection<AssignedStreet> AssignedStreets { get; }
        #endregion

        public AssignedStreetGridControlViewModel()
        {
            mDataGridSearchColumnIndexMax = 2;
            AssignedStreets = DatabaseHelper.AssignedStreets;
        }

        protected override string GetItemCsvString(object? aItem)
        {
            string aResult = "";
            if (aItem is AssignedStreet aRecord)
            {
                aResult = string.Join(',', $"\"{aRecord.Street}\"", $"\"{aRecord.AbonentCount}\"");
            }
            return aResult;
        }

        protected override bool ApplyFilter(object? aItem)
        {
            bool aAccepted = true;
            if (aItem is AssignedStreet aRecord)
            {
                string? aHaystackString = null;
                bool aNoColumn = false;
                switch (mDataGridCurrentColumnIndex)
                {
                    case 1:
                    aHaystackString = aRecord.Street;
                    break;
                    case 2:
                    aHaystackString = aRecord.AbonentCount.ToString();
                    break;
                    default:
                    aNoColumn = true;
                    break;
                }

                aAccepted = aNoColumn || !string.IsNullOrEmpty(aHaystackString) && aHaystackString.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase);
            }

            return aAccepted;
        }
    }
}
