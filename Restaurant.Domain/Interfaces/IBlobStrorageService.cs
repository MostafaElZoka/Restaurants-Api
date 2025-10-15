namespace Restaurant.Domain.Interfaces;

public interface IBlobStrorageService
{
    string? GetBlobSas(string? blobUrl);
    public Task<string> UploadToBlobAsync(Stream stream, string fileName);
}
