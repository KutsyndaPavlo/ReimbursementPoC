using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using ReimbursementPoC.Content.API.Model;

namespace ReimbursementPoC.Content.API.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : Controller
    {
        private const string _cdb_connectionString = "**";
        readonly string _connectionString = "**";
        private readonly string _containerName = "attachments";

        [HttpGet("list")]
        public async Task<IEnumerable<string>> GetAsync()
        {
            BlobContainerClient client = GetClient();

            var files = new List<string>();

            await foreach (var file in client.GetBlobsAsync())
            {
                files.Add(file.Name);
            }

            return files;
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadAsync([FromQuery] string? name)
        {
            BlobContainerClient client = GetClient();
            BlobClient file = client.GetBlobClient(name);

            if (await file.ExistsAsync())
            {
                var stream = await file.OpenReadAsync();
                return File(stream, "application/octet-stream", file.Name);
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var storageAccount = CloudStorageAccount.Parse(_connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(_containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions()
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var blob = container.GetBlockBlobReference(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream);
            }

            //response.Status = $"File {blob.FileName} Uploaded Successfully";
            //response.Error = false;
            //response.Blob.Uri = client.Uri.AbsoluteUri;
            //response.Blob.Name = client.Name;

            var cosmosClient = new CosmosClient(_cdb_connectionString);
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync("RiDatabase");
            Container dbcontainer = await database.CreateContainerIfNotExistsAsync("RiConteiner", "/UserId");
            var attachment = new Attachment { Name = file.Name, UserId = Guid.NewGuid() };
            ItemResponse<Attachment> response = await dbcontainer.CreateItemAsync<Attachment>(attachment, new PartitionKey(attachment.UserId.ToString()));

            FeedIterator<Attachment> queryResultSetIterator = dbcontainer.GetItemQueryIterator<Attachment>(new QueryDefinition($"SELECT * FROM c WHERE c.UserId = '{attachment.UserId}'"));

            List<Attachment> list = new List<Attachment>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Attachment> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Attachment item in currentResultSet)
                {
                    list.Add(item);
                }
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] string? name)
        {
            BlobContainerClient client = GetClient();
            BlobClient file = client.GetBlobClient(name);
            await file.DeleteAsync();
            // Error = false, Status = $"File: {blobFilename} has been successfully deleted." };
            return Ok();
        }
        private BlobContainerClient GetClient()
        {
            return new BlobContainerClient(_connectionString, _containerName);
        }
    }
}
