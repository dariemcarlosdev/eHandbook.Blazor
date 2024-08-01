using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

public class ManualDto
{
    public required string Description { get; set; }
    /// <summary>
    /// This prop is just for testing purpose. It will be used to store the path of the file in the Azure Blob Storage.
    /// </summary>
    public required string Path { get; set; }



    #region Attributes and methods Not Used yed
   
    //create property for upload file
    public IFormFile File { get; set; }

    //method for upload local file to Azure Blob Storage here below
    public async Task UploadFileToBlobAsync(ManualDto manualDto)
    {
        var connectionString = "DefaultEndpointsProtocol=https;AccountName=youraccountname;AccountKey=youraccountkey;EndpointSuffix=core.windows.net";
        var containerName = "yourcontainername";
        var blobName = manualDto.Path;
        var file = manualDto.File;

        var blobServiceClient = new BlobServiceClient(connectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }
    }

    //generate BlobServiceClient class
    public class BlobServiceClient
    {
        private string connectionString;

        public BlobServiceClient(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public BlobContainerClient GetBlobContainerClient(string containerName)
        {
            return new BlobContainerClient(connectionString, containerName);
        }
    }
    #endregion
}
