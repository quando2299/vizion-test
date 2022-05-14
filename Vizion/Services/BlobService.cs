using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Vizion.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobService;

        public BlobService(BlobServiceClient blobService)
        {
            _blobService = blobService;
        }
        
        public async Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName)
        {
            var containerClient = GetContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
            return blobClient.Uri;
        }

        public async Task<bool> DeleteBlobFile(string blobContainerName, string fileName)
        {
            var containerClient = _blobService.GetBlobContainerClient(blobContainerName);
            return await containerClient.GetBlobClient(fileName).DeleteIfExistsAsync();
        }
        
        private BlobContainerClient GetContainerClient(string blobContainerName)
        {
            var containerClient = _blobService.GetBlobContainerClient(blobContainerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            return containerClient;
        }
    }
}