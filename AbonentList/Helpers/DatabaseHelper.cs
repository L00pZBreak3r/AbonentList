using System.Collections.ObjectModel;

using AbonentListModelLibrary.Models;
using System.Threading.Tasks;
using AbonentListGrpcClient;

namespace AbonentList.Helpers
{
    internal static class DatabaseHelper
    {
        public static ObservableCollection<Abonent> Abonents { get; private set; }
        public static ObservableCollection<Address> Addresses { get; private set; }
        public static ObservableCollection<AssignedStreet> AssignedStreets { get; private set; }
        public static ObservableCollection<PhoneNumber> PhoneNumbers { get; private set; }

        static DatabaseHelper()
        {
            Abonents = new ObservableCollection<Abonent>();
            Addresses = new ObservableCollection<Address>();
            AssignedStreets = new ObservableCollection<AssignedStreet>();
            PhoneNumbers = new ObservableCollection<PhoneNumber>();
        }

        public static async Task<bool> RetrieveAbonentsAsync(string aServiceUrl)
        {
            using var aAbonentListClient = new AbonentListClient(aServiceUrl);
            var aDatabaseView = await aAbonentListClient.GetAbonentFullSetAsync();

            Abonents = aDatabaseView.Abonents;
            Addresses = aDatabaseView.Addresses;
            AssignedStreets = aDatabaseView.AssignedStreets;
            PhoneNumbers = aDatabaseView.PhoneNumbers;

            return Abonents.Count > 0;
        }
    }
}
