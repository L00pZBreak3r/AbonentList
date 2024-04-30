using System;
using System.Collections.ObjectModel;

using AbonentList.Helpers;
using AbonentListModelLibrary.Models;

namespace AbonentList.ViewModels
{
    internal class AbonentGridControlViewModel : GridControlBaseViewModel
    {
        #region Abonents

        public ObservableCollection<Abonent> Abonents { get; }
        #endregion

        public AbonentGridControlViewModel()
        {
            mDataGridSearchColumnIndexMax = 10;
            Abonents = DatabaseHelper.Abonents;
        }

        protected override string GetItemCsvString(object? aItem)
        {
            string aResult = "";
            if (aItem is Abonent aRecord)
            {
                aResult = string.Join(',', $"\"{aRecord.FullName}\"",
                                            $"\"{aRecord.LastName}\"",
                                            $"\"{aRecord.FirstName}\"",
                                            $"\"{aRecord.MiddleName}\"",
                                            $"\"{aRecord.ContractDate.ToString("d")}\"",
                                            $"\"{aRecord.Street}\"",
                                            $"\"{aRecord.Building}\"",
                                            $"\"{aRecord.FirstPhoneNumberPrivateString}\"",
                                            $"\"{aRecord.FirstPhoneNumberOrganizationString}\"",
                                            $"\"{aRecord.FirstPhoneNumberMobileString}\"");
            }
            return aResult;
        }

        protected override bool ApplyFilter(object? aItem)
        {
            bool aAccepted = true;
            if (aItem is Abonent aRecord)
            {
                string? aHaystackString = null;
                bool aNoColumn = false;
                switch (mDataGridCurrentColumnIndex)
                {
                    case 1:
                    aHaystackString = aRecord.FullName;
                    break;
                    case 2:
                    aHaystackString = aRecord.LastName;
                    break;
                    case 3:
                    aHaystackString = aRecord.FirstName;
                    break;
                    case 4:
                    aHaystackString = aRecord.MiddleName;
                    break;
                    case 5:
                    aHaystackString = aRecord.ContractDate.ToString("d");
                    break;
                    case 6:
                    aHaystackString = aRecord.Street;
                    break;
                    case 7:
                    aHaystackString = aRecord.Building;
                    break;
                    case 8:
                    aHaystackString = aRecord.FirstPhoneNumberPrivateString;
                    break;
                    case 9:
                    aHaystackString = aRecord.FirstPhoneNumberOrganizationString;
                    break;
                    case 10:
                    aHaystackString = aRecord.FirstPhoneNumberMobileString;
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
