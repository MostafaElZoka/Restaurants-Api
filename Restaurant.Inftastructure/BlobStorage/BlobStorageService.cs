using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurant.Domain.Interfaces;
using Restaurant.Inftastructure.Configurations;

namespace Restaurant.Inftastructure.BlobStorage;

internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStrorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value; //getting the value of the json object in appsettings
    public async Task<string> UploadToBlobAsync(Stream stream, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString); //Creates a connection to Azure Storage Account using the connection string
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName); //refrences and points to the container in the storage account 

        var blobClient = blobContainerClient.GetBlobClient(fileName);//creates a blob(file) in the container(folder) on azure storage account

        await blobClient.UploadAsync(stream); //uploads the data

        var url = blobClient.Uri.ToString();

        return url;
    }

    public string? GetBlobSas(string? blobUrl)
    {
        if(blobUrl == null) return null;

        ///creating the sas token 
        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _blobStorageSettings.LogosContainerName,
            Resource = "b", // a token for accessing a single Blob (file) not the entire container or account (the letter matters)
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            BlobName = GetBlobNameFromUrl(blobUrl)
        };

        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString); //Creates a connection to Azure Storage Account using the connection string


        sasBuilder.SetPermissions(BlobSasPermissions.Read);//the permission to download the file (read)

        //this part takes the blueprint of the sas builder and uses the storage account key to sign it and create the actual token
        var sasToken = sasBuilder.
            ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
            .ToString();

        return $"{blobUrl}?{sasToken}"; //the Blob container is private so we need both the url and the token to access the file
    }

    private string GetBlobNameFromUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        return uri.Segments.Last(); //the blob (file) name is the last segment of the url example: https://restaurantblobstorage.blob.core.windows.net/logos/filename.png => filename.png
    }
}
