using AbonentListModelLibrary.Models;
using AbonentListGrpcService.Protos;

namespace AbonentListGrpcService.Helpers;

internal static class RpcMessageHelper
{
    public static AbonentReply CreateAbonentReply(Abonent aItem)
    {
        var aAbonentReply = new AbonentReply()
        {
            Id = aItem.Id,
            FullName = aItem.FullName,
            LastName = aItem.LastName,
            FirstName = aItem.FirstName,
            MiddleName = aItem.MiddleName,
            ContractDate = aItem.ContractDate.ToBinary(),
            Street = aItem.Street,
            Building = aItem.Building,
            FirstPhoneNumberPrivateString = aItem.FirstPhoneNumberPrivateString ?? "",
            FirstPhoneNumberOrganizationString = aItem.FirstPhoneNumberOrganizationString ?? "",
            FirstPhoneNumberMobileString = aItem.FirstPhoneNumberMobileString ?? "",
            AddressId = aItem.AddressId
        };
        return aAbonentReply;
    }

    public static AddressReply CreateAddressReply(Address aItem)
    {
        var aAddressReply = new AddressReply()
        {
            Id = aItem.Id,
            Country = aItem.Country,
            State = aItem.State,
            City = aItem.City,
            Street = aItem.Street,
            Building = aItem.Building,
            Apartment = aItem.Apartment,
            AssignedStreetId = aItem.AssignedStreetId ?? 0
        };
        return aAddressReply;
    }

    public static AssignedStreetReply CreateAssignedStreetReply(AssignedStreet aItem)
    {
        var aAssignedStreetReply = new AssignedStreetReply()
        {
            Id = aItem.Id,
            Street = aItem.Street,
            AbonentCount = aItem.AbonentCount
        };
        return aAssignedStreetReply;
    }

    public static PhoneNumberReply CreatePhoneNumberReply(PhoneNumber aItem)
    {
        var aPhoneNumberReply = new PhoneNumberReply()
        {
            Id = aItem.Id,
            Number = aItem.Number,
            RegionCode = aItem.RegionCode,
            RegionNumber = aItem.RegionNumber,
            PhoneNumberType = (int)aItem.PhoneNumberType,
            IsPlusRegion = aItem.IsPlusRegion,
            IsMobile = aItem.IsMobile,
            FullNumberString = aItem.FullNumberString,
            AbonentId = aItem.AbonentId ?? 0,
            AddressId = aItem.AddressId ?? 0
        };
        return aPhoneNumberReply;
    }
}
