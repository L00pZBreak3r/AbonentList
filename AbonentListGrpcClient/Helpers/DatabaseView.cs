using System;
using System.Collections.ObjectModel;
using System.Linq;

using AbonentListGrpcClient.Protos;
using AbonentListModelLibrary.Enums;
using AbonentListModelLibrary.Models;


namespace AbonentListGrpcClient.Helpers;

public class DatabaseView
{
    public ObservableCollection<Abonent> Abonents { get; }
    public ObservableCollection<Address> Addresses { get; }
    public ObservableCollection<AssignedStreet> AssignedStreets { get; }
    public ObservableCollection<PhoneNumber> PhoneNumbers { get; }

    public DatabaseView(AbonentSetReply aAbonentSetReply)
    {
        Abonents = new ObservableCollection<Abonent>(aAbonentSetReply.Abonents.Select(CreateAbonent));
        Addresses = new ObservableCollection<Address>(aAbonentSetReply.Addresses.Select(CreateAddress));
        AssignedStreets = new ObservableCollection<AssignedStreet>(aAbonentSetReply.AssignedStreets.Select(CreateAssignedStreet));
        PhoneNumbers = new ObservableCollection<PhoneNumber>(aAbonentSetReply.PhoneNumbers.Select(CreatePhoneNumber));

        FillCollections();
    }

    private void FillCollections()
    {
        foreach (var aAddress in Addresses)
        {
            if (aAddress.AssignedStreetId.HasValue && (aAddress.AssignedStreetId.Value > 0))
                aAddress.AssignedStreet = AssignedStreets.FirstOrDefault(a => a.Id == aAddress.AssignedStreetId.Value);
            if (aAddress.AssignedStreet == null)
                aAddress.AssignedStreetId = null;

            aAddress.Abonents = new ObservableCollection<Abonent>(Abonents.Where(a => a.AddressId == aAddress.Id));
            aAddress.PhoneNumbers = new ObservableCollection<PhoneNumber>(PhoneNumbers.Where(a => a.AddressId == aAddress.Id));
        }

        foreach (var aAssignedStreet in AssignedStreets)
        {
            aAssignedStreet.Addresses = new ObservableCollection<Address>(Addresses.Where(a => a.AssignedStreetId == aAssignedStreet.Id));
        }

        foreach (var aAbonent in Abonents)
        {
            if (aAbonent.AddressId > 0)
                aAbonent.Address = Addresses.FirstOrDefault(a => a.Id == aAbonent.AddressId);
            if (aAbonent.Address == null)
                aAbonent.AddressId = 0;

            aAbonent.PhoneNumbers = new ObservableCollection<PhoneNumber>(PhoneNumbers.Where(a => a.AbonentId == aAbonent.Id));
        }

        foreach (var aPhoneNumber in PhoneNumbers)
        {
            if (aPhoneNumber.AddressId.HasValue && (aPhoneNumber.AddressId.Value > 0))
                aPhoneNumber.Address = Addresses.FirstOrDefault(a => a.Id == aPhoneNumber.AddressId.Value);
            if (aPhoneNumber.Address == null)
                aPhoneNumber.AddressId = null;

            if (aPhoneNumber.AbonentId.HasValue && (aPhoneNumber.AbonentId.Value > 0))
                aPhoneNumber.Abonent = Abonents.FirstOrDefault(a => a.Id == aPhoneNumber.AbonentId.Value);
            if (aPhoneNumber.Abonent == null)
                aPhoneNumber.AbonentId = null;
        }
    }

    private static Abonent CreateAbonent(AbonentReply aItem)
    {
        var aAbonent = new Abonent()
        {
            Id = aItem.Id,
            LastName = aItem.LastName,
            FirstName = aItem.FirstName,
            MiddleName = aItem.MiddleName,
            ContractDate = DateTime.FromBinary(aItem.ContractDate),
            AddressId = aItem.AddressId
        };
        return aAbonent;
    }

    private static Address CreateAddress(AddressReply aItem)
    {
        var aAddress = new Address()
        {
            Id = aItem.Id,
            Country = aItem.Country,
            State = aItem.State,
            City = aItem.City,
            Street = aItem.Street,
            Building = aItem.Building,
            Apartment = aItem.Apartment,
            AssignedStreetId = aItem.AssignedStreetId
        };
        return aAddress;
    }

    private static AssignedStreet CreateAssignedStreet(AssignedStreetReply aItem)
    {
        var aAssignedStreet = new AssignedStreet()
        {
            Id = aItem.Id,
            Street = aItem.Street,
        };
        return aAssignedStreet;
    }

    private static PhoneNumber CreatePhoneNumber(PhoneNumberReply aItem)
    {
        var aPhoneNumber = new PhoneNumber()
        {
            Id = aItem.Id,
            Number = aItem.Number,
            RegionCode = aItem.RegionCode,
            RegionNumber = aItem.RegionNumber,
            PhoneNumberType = (EPhoneNumberType)aItem.PhoneNumberType,
            IsPlusRegion = aItem.IsPlusRegion,
            IsMobile = aItem.IsMobile,
            AbonentId = aItem.AbonentId,
            AddressId = aItem.AddressId
        };
        return aPhoneNumber;
    }
}
