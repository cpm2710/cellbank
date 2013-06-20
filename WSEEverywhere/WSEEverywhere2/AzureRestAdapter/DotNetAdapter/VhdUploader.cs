using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureRestAdapter.DotNetAdapter
{
    public class VhdUploader
    {
        public void UploadVhd(string storageAccount, string storageContainer, string storageKey, string blobName, string filePath)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=" + storageAccount + ";AccountKey=" + storageKey);
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(storageContainer);

            // Retrieve reference to a blob named "myblob".
            CloudPageBlob blockBlob = container.GetPageBlobReference(blobName);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
    }
}
