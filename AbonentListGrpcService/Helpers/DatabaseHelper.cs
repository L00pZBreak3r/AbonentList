using AbonentListModelLibrary.Models;
using AbonentListDataLibrary;
using AbonentListDataLibrary.EF;

namespace AbonentListGrpcService.Helpers;

internal static class DatabaseHelper
{
    private static readonly AbonentListDBConfiguration DatabaseConfiguration = new AbonentListDBConfiguration();
    public static AbonentListDBContext DatabaseContext => DatabaseConfiguration.AbonentListDB;

    public static Abonent[] Abonents { get; }
    public static Address[] Addresses { get; }
    public static AssignedStreet[] AssignedStreets { get; }
    public static PhoneNumber[] PhoneNumbers { get; }

    static DatabaseHelper()
    {
        DatabaseConfiguration.AddRandomAbonents();

        Abonents = DatabaseContext.Abonents.Local.ToArray();
        Addresses = DatabaseContext.Addresses.Local.ToArray();
        AssignedStreets = DatabaseContext.AssignedStreets.Local.ToArray();
        PhoneNumbers = DatabaseContext.PhoneNumbers.Local.ToArray();
    }
}
