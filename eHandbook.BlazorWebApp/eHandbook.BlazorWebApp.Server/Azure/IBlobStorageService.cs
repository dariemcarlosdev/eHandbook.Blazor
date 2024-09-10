namespace eHandbook.BlazorWebApp.Server.Azure
{
    public interface IBlobStorageService
    {
        // UploadFileToBlobAsync
        Task<string> UploadFileToBlobAsync(string strFileName, string contecntType, Stream fileStream);
        // DownloadFileToBlobAsync
        Task<Stream> DownloadFileToBlobAsync(string strFileName);
        // DeleteFileToBlobAsync
        Task<bool> DeleteFileToBlobAsync(string strFileName);
    }
}
