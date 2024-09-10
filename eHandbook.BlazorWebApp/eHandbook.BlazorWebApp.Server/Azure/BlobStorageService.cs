
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace eHandbook.BlazorWebApp.Server.Azure
{
    public class BlobStorageService : IBlobStorageService
    {
        //Create field for Iconfiguration
        private readonly IConfiguration _configuration;
        //create field for Ilogger one instance
        private readonly ILogger<BlobStorageService> _logger;
        private string blobStorageConnection;
        private string blobContainerName;
        private readonly BlobServiceClient _blobServiceClient;

        //Constructor
        public BlobStorageService(IConfiguration configuration, ILogger<BlobStorageService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            blobStorageConnection = _configuration["AzureStorageAccountSetting:ConnectionString"];
            blobContainerName = _configuration["AzureStorageAccountSetting:ContainerName"];
        }
        public async Task<bool> DeleteFileToBlobAsync(string strFileName)
        {
            try
            {
                var container = new BlobContainerClient(blobStorageConnection, blobContainerName);
                var createResponse = await container.CreateIfNotExistsAsync();
                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(PublicAccessType.Blob);
                var blob = container.GetBlobClient(strFileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        public Task<Stream> DownloadFileToBlobAsync(string strFileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileToBlobAsync(string strFileName, string contecntType, Stream fileStream)
        {
            try
            {
                //Create a blob client and get a reference to the container
                var container = new BlobContainerClient(blobStorageConnection, blobContainerName);
                var createContainerResponse = await container.CreateIfNotExistsAsync();
                if (createContainerResponse != null && createContainerResponse.GetRawResponse().Status == 201)
                    //setacesspolicyasync
                    await container.SetAccessPolicyAsync(PublicAccessType.Blob);

                var blob = container.GetBlobClient(strFileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contecntType });
                var blobUrlString = blob.Uri.ToString();
                return blobUrlString;

            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "Error uploading file to Azure Blob Storage. Status: {Status}, ErrorCode: {ErrorCode}", ex.Status, ex.ErrorCode);
                //         // Optionally, you can rethrow the exception or handle it as needed
                //         uploadError = "An error occurred while uploading the file.";
                //         uploadSuccess = false;
                throw;
            }


            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }


            // private async Task UploadFileToAzureBlobStorage()
            // {
            //     var connectionString = Configuration["AzureBlobStorage:ConnectionString"];
            //     var containerName = Configuration["AzureBlobStorage:ContainerName"];

            //     try
            //     {
            //         var blobServiceClient = new BlobServiceClient(connectionString);
            //         var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            //         var blobClient = blobContainerClient.GetBlobClient(selectedFileName);

            //         using (var stream = new MemoryStream(manualToCreateModel.FileContent))
            //         {
            //             await blobClient.UploadAsync(stream, true);
            //         }

            //         Logger.LogInformation("File uploaded to Azure Blob Storage successfully.");
            //         uploadSuccess = true;
            //         uploadError = null;
            //     }
            //     catch (RequestFailedException ex)
            //     {
            //         Logger.LogError(ex, "Error uploading file to Azure Blob Storage. Status: {Status}, ErrorCode: {ErrorCode}", ex.Status, ex.ErrorCode);
            //         // Optionally, you can rethrow the exception or handle it as needed
            //         uploadError = "An error occurred while uploading the file.";
            //         uploadSuccess = false;
            //         throw;
            //     }
            //     catch (Exception ex)
            //     {
            //         Logger.LogError(ex, "An unexpected error occurred while uploading the file to Azure Blob Storage.");
            //         // Optionally, you can rethrow the exception or handle it as needed
            //         uploadError = "An error occurred while uploading the file.";
            //         uploadSuccess = false;
            //         throw;
            //     }




            // }
        }
    }
}
