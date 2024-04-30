using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AbonentListDataLibrary.EF;
using AbonentListModelLibrary.Enums;
using AbonentListModelLibrary.Models;
using AbonentListDataLibrary.Helpers;

namespace AbonentListDataLibrary
{

  public class AbonentListDBConfiguration : IDisposable
  {
    private const string DATABASE_NAME_DEFAULT = "AbonentListDB";

    private const long MINIMAL_DATE_TICKS = 627667488000000000L;
    private const int RANDOM_ABONENT_COUNT_DEFAULT = 512;
    private const int RANDOM_PHONENUMBER_COUNT_DEFAULT = RANDOM_ABONENT_COUNT_DEFAULT * 4;
    private const int RANDOM_ADDRESSES_COUNT_DEFAULT = RANDOM_ABONENT_COUNT_DEFAULT * 2;
    private const int RANDOM_STREETS_COUNT_DEFAULT = RANDOM_ADDRESSES_COUNT_DEFAULT * 2;
    private const int RANDOM_MAX_NAME_LENGTH = 16;

    private bool disposedValue;
    public readonly AbonentListDBContext AbonentListDB;

    public AbonentListDBConfiguration(string? aDatabaseName = null)
    {
      if (string.IsNullOrWhiteSpace(aDatabaseName))
        aDatabaseName = DATABASE_NAME_DEFAULT;

      AbonentListDB = new AbonentListDBContext(aDatabaseName!);
      AbonentListDB.Database.EnsureCreated();

    }

    /// <summary>
    /// Fills table Abonents with fake test data.
    /// </summary>
    /// <returns></returns>
    public void AddRandomAbonents()
    {
      if (AbonentListDB.Abonents.Local.Count > 0)
        return;

      const int aAbonentCount = RANDOM_ABONENT_COUNT_DEFAULT;
      const int aPhoneNumberCount = RANDOM_PHONENUMBER_COUNT_DEFAULT;
      const int aAddressesCount = RANDOM_ADDRESSES_COUNT_DEFAULT;
      const int aStreetsCount = RANDOM_STREETS_COUNT_DEFAULT;

      const long aMinDateTicks = MINIMAL_DATE_TICKS;
      long aMaxDateTicks = DateTime.Now.Ticks;

        Random aRandom = new Random();

        var aStreets = new AssignedStreet[aStreetsCount];
        List<AssignedStreet> aStreetsIds = new();
        for (int i = 0; i < aStreetsCount; i++)
        {
          var aStreet = new AssignedStreet
          {
            Street = StringHelper.GenerateRandomString(aRandom, RANDOM_MAX_NAME_LENGTH)
          };
          aStreets[i] = aStreet;
          aStreetsIds.Add(aStreet);
        }

        var aAddresses = new Address[aAddressesCount];
        List<Address> aAddressesIds = new();
        List<Address> aAddressesPhoneIds = new();
        for (int i = 0; i < aAddressesCount; i++)
        {
          int aStreetIndex = aRandom.Next(aStreetsIds.Count);

          var aAddress = new Address
          {
            Country = "",
            State = "",
            City = "",
            Street = aStreets[aStreetIndex].Street,
            Building = aRandom.Next(1000).ToString(),
            Apartment = aRandom.Next(1000).ToString(),
            AssignedStreet = aStreetsIds[aStreetIndex]
          };
          aAddresses[i] = aAddress;
          aAddressesIds.Add(aAddress);
          aAddressesPhoneIds.Add(aAddress);
        }

        var aPhoneNumbers = new PhoneNumber[aPhoneNumberCount];
        List<PhoneNumber> aMobilePhoneIds = new();
        Dictionary<Address, PhoneNumber> aAddressToPhone = new();
        for (int i = 0; i < aPhoneNumberCount; i++)
        {
          int aAddressIndex = aRandom.Next(aAddressesPhoneIds.Count);

          int aRegionCode = aRandom.Next(9) + 1;
          bool aIsMobile = (aRandom.Next(2) == 0) || (aAddressIndex >= aAddressesPhoneIds.Count);
          PhoneNumber aPhoneNumber = new PhoneNumber
          {
            Number = aRandom.Next(10000000),
            RegionCode = aRegionCode,
            RegionNumber = aRandom.Next(1000),
            IsPlusRegion = (aRegionCode > 4) && (aRegionCode != 8),
            IsMobile = aIsMobile,
            PhoneNumberType = (EPhoneNumberType)aRandom.Next(2)
          };
          aPhoneNumbers[i] = aPhoneNumber;
          if (aIsMobile)
            aMobilePhoneIds.Add(aPhoneNumber);
          else
          {
            Address aAddress = aAddressesPhoneIds[aAddressIndex];
            aPhoneNumber.Address = aAddress;
            aAddressesPhoneIds.RemoveAt(aAddressIndex);
            aAddressToPhone.Add(aAddress, aPhoneNumber);
          }
        }

        var aAbonents = new Abonent[aAbonentCount];
        for (int i = 0; i < aAbonentCount; i++)
        {
          int aAddressIndex = aRandom.Next(aAddressesIds.Count);
          Address aAddress = aAddressesIds[aAddressIndex];
          aAddressToPhone.TryGetValue(aAddress, out PhoneNumber? aAbonentPhoneNumber);
          PhoneNumber? aAbonentMobilePhoneNumber = null;

          if ((aMobilePhoneIds.Count > 0) && ((aAbonentPhoneNumber == null) || (aRandom.Next(2) == 0)))
          {
            int aMobilePhoneIndex = aRandom.Next(aMobilePhoneIds.Count);
            aAbonentMobilePhoneNumber = aMobilePhoneIds[aMobilePhoneIndex];
            aMobilePhoneIds.RemoveAt(aMobilePhoneIndex);
          }

          var aAbonent = new Abonent
          {
            LastName = StringHelper.GenerateRandomString(aRandom, RANDOM_MAX_NAME_LENGTH),
            FirstName = StringHelper.GenerateRandomString(aRandom, RANDOM_MAX_NAME_LENGTH),
            MiddleName = StringHelper.GenerateRandomString(aRandom, RANDOM_MAX_NAME_LENGTH),
            ContractDate = (new DateTime(DateTimeHelper.GenerateRandomLong(aRandom, aMinDateTicks, aMaxDateTicks))).Date,
            Address = aAddress
          };
          aAbonents[i] = aAbonent;
          aAddressesIds.RemoveAt(aAddressIndex);
          if (aAbonentPhoneNumber != null)
            aAbonentPhoneNumber.Abonent = aAbonent;
          if (aAbonentMobilePhoneNumber != null)
            aAbonentMobilePhoneNumber.Abonent = aAbonent;
        }

        AbonentListDB.AssignedStreets.AddRange(aStreets);
        AbonentListDB.Addresses.AddRange(aAddresses);
        AbonentListDB.Abonents.AddRange(aAbonents);
        AbonentListDB.PhoneNumbers.AddRange(aPhoneNumbers);

        AbonentListDB.SaveChanges();

    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          AbonentListDB.Dispose();
        }

        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        // TODO: set large fields to null
        disposedValue = true;
      }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~AbonentListDBConfiguration()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
