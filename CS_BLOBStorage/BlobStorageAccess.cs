using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

public class BlobStorageExample
{
    private const string StorageConnectionString = " ";
    private const string ContainerName = "myfiles";
    private const string BlobName = "BhagwadGeeta_In_Marathi_pdfa.pdf";
    private const string LocalFilePath = "D:\\BhagwadGeeta_In_Marathi_pdfa.pdf";

    public BlobStorageExample()
    {
        // Create a BlobServiceClient object which will be used to create a container client
        var blobServiceClient = new BlobServiceClient(StorageConnectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        // Ensure the container exists
         blobContainerClient.CreateIfNotExists();

        // Upload the document
         UploadDocument(blobContainerClient, BlobName, LocalFilePath);
    }

    private void UploadDocument(BlobContainerClient blobContainerClient, string blobName, string localFilePath)
    {
        try
        {
            // Get a reference to a blob
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            Console.WriteLine($"Uploading to Blob storage as blob:\n\t {blobClient.Uri}");
            // Open the file and upload its data
            using FileStream uploadFileStream = File.OpenRead(localFilePath);
            // Upload the document
            blobClient.Upload(uploadFileStream, true);
           
            // Close the file
            uploadFileStream.Close();

            Console.WriteLine("Upload completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading document: {ex.Message}");
        }
    }
}
