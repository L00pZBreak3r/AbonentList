using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using AbonentListGrpcClient.Protos;

using AbonentListGrpcClient.Helpers;

namespace AbonentListGrpcClient;

public class AbonentListClient : IDisposable
{
    protected readonly GrpcChannel mChannel;
    protected readonly AbonentList.AbonentListClient mAbonentListClient;

    public AbonentListClient(string aServiceUrl)
    {
        mChannel = GrpcChannel.ForAddress(aServiceUrl);
        mAbonentListClient = new AbonentList.AbonentListClient(mChannel);
    }

    public async Task<string> GetDescriptionAsync()
    {
        var reply = await mAbonentListClient.GetDescriptionAsync(new Empty());
        return reply.Description;
    }

    public async Task<DatabaseView> GetAbonentFullSetAsync()
    {
        var reply = await mAbonentListClient.GetAbonentFullSetAsync(new Empty());
        return new DatabaseView(reply);
    }

    #region IDisposable

    // To detect redundant calls
    private bool mDisposedValue;

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (!mDisposedValue)
        {
            if (disposing)
            {
                mChannel.Dispose();
                //mChannel = null;
            }

            mDisposedValue = true;
        }
    }

    #endregion
}
