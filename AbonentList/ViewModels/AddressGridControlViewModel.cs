using System;
using System.Collections.ObjectModel;

using AbonentList.Helpers;
using AbonentListModelLibrary.Models;

namespace AbonentList.ViewModels
{
    internal class AddressGridControlViewModel : GridControlBaseViewModel
    {
        #region Addresses

        public ObservableCollection<Address> Addresses { get; }
        #endregion

        public AddressGridControlViewModel()
        {
            mDataGridSearchColumnIndexMax = 6;
            Addresses = DatabaseHelper.Addresses;
        }

        protected override string GetItemCsvString(object? aItem)
        {
            string aResult = "";
            if (aItem is Address aRecord)
            {
                aResult = string.Join(',', $"\"{aRecord.Country}\"",
                                            $"\"{aRecord.State}\"",
                                            $"\"{aRecord.City}\"",
                                            $"\"{aRecord.Street}\"",
                                            $"\"{aRecord.Building}\"",
                                            $"\"{aRecord.Apartment}\"");
            }
            return aResult;
        }

        protected override bool ApplyFilter(object? aItem)
        {
            bool aAccepted = true;
            if (aItem is Address aRecord)
            {
                string? aHaystackString = null;
                bool aNoColumn = false;
                switch (mDataGridCurrentColumnIndex)
                {
                    case 1:
                    aHaystackString = aRecord.Country;
                    break;
                    case 2:
                    aHaystackString = aRecord.State;
                    break;
                    case 3:
                    aHaystackString = aRecord.City;
                    break;
                    case 4:
                    aHaystackString = aRecord.Street;
                    break;
                    case 5:
                    aHaystackString = aRecord.Building;
                    break;
                    case 6:
                    aHaystackString = aRecord.Apartment;
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
