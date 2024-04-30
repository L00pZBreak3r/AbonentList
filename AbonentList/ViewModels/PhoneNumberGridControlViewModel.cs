using System;
using System.Collections.ObjectModel;

using AbonentList.Helpers;
using AbonentListModelLibrary.Models;

namespace AbonentList.ViewModels
{
    internal class PhoneNumberGridControlViewModel : GridControlBaseViewModel
    {
        #region PhoneNumbers
        public ObservableCollection<PhoneNumber> PhoneNumbers { get; }
        #endregion

        public PhoneNumberGridControlViewModel()
        {
            mDataGridSearchColumnIndexMax = 4;
            PhoneNumbers = DatabaseHelper.PhoneNumbers;
        }

        protected override string GetItemCsvString(object? aItem)
        {
            string aResult = "";
            if (aItem is PhoneNumber aRecord)
            {
                aResult = string.Join(',', $"\"{aRecord.FullNumberString}\"",
                                            $"\"{aRecord.Number:D7}\"",
                                            $"\"{aRecord.RegionCode}\"",
                                            $"\"{aRecord.RegionNumber}\"",
                                            $"\"{aRecord.IsPlusRegion}\"",
                                            $"\"{aRecord.IsMobile}\"",
                                            $"\"{aRecord.PhoneNumberType}\"");
            }
            return aResult;
        }

        protected override bool ApplyFilter(object? aItem)
        {
            bool aAccepted = true;
            if (aItem is PhoneNumber aRecord)
            {
                string? aHaystackString = null;
                bool aNoColumn = false;
                switch (mDataGridCurrentColumnIndex)
                {
                    case 1:
                    aHaystackString = aRecord.FullNumberString;
                    break;
                    case 2:
                    aHaystackString = aRecord.Number.ToString();
                    break;
                    case 3:
                    aHaystackString = aRecord.RegionCode.ToString();
                    break;
                    case 4:
                    aHaystackString = aRecord.RegionNumber.ToString();
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
