using AbonentListGrpcService.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

using AbonentListGrpcService.Helpers;

namespace AbonentListGrpcService.Services;

public class AbonentListService : AbonentList.AbonentListBase
{
    private readonly ILogger<AbonentListService> _logger;
    public AbonentListService(ILogger<AbonentListService> logger)
    {
        _logger = logger;
    }

    public override Task<GetDescriptionReply> GetDescription(Empty request, ServerCallContext context)
    {
        return Task.FromResult(new GetDescriptionReply
        {
            Description = nameof(AbonentListService)
        });
    }

    public override Task<AbonentSetReply> GetAbonentFullSet(Empty request, ServerCallContext context)
    {
        var aAbonents = DatabaseHelper.Abonents;
        var aAddresses = DatabaseHelper.Addresses;
        var aAssignedStreets = DatabaseHelper.AssignedStreets;
        var aPhoneNumbers = DatabaseHelper.PhoneNumbers;

        var aReply = new AbonentSetReply();
        aReply.Abonents.AddRange(aAbonents.Select(RpcMessageHelper.CreateAbonentReply));
        aReply.Addresses.AddRange(aAddresses.Select(RpcMessageHelper.CreateAddressReply));
        aReply.AssignedStreets.AddRange(aAssignedStreets.Select(RpcMessageHelper.CreateAssignedStreetReply));
        aReply.PhoneNumbers.AddRange(aPhoneNumbers.Select(RpcMessageHelper.CreatePhoneNumberReply));

        return Task.FromResult(aReply);
    }
}
