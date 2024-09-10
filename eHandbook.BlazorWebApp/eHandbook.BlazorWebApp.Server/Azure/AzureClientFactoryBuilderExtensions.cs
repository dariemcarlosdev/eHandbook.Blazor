using Azure.Core.Extensions;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;



internal static class AzureClientFactoryBuilderExtensions
{
    /// <summary>
    /// this method is called in Program.cs to add the BlobServiceClient to the DI container.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceUriOrConnectionString"></param>
    /// <param name="preferMsi"></param>
    /// <returns></returns>
    public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
    {
        if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri? serviceUri))
        {
            return builder.AddBlobServiceClient(serviceUri);
        }
        else
        {
            return builder.AddBlobServiceClient(serviceUriOrConnectionString);
        }
    }
    /// <summary>
    /// this method is called in Program.cs to add the QueueServiceClient to the DI container.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceUriOrConnectionString"></param>
    /// <param name="preferMsi"></param>
    /// <returns></returns>
    public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
    {
        if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri? serviceUri))
        {
            return builder.AddQueueServiceClient(serviceUri);
        }
        else
        {
            return builder.AddQueueServiceClient(serviceUriOrConnectionString);
        }
    }
}
