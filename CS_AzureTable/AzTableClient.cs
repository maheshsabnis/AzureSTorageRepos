using System;
using Azure;
using Azure.Data.Tables;
using CS_AzureTable;

public class TableStorageExample
{
 

   

    //public static void Main(string[] args)
    //{
    //    var serviceClient = new TableServiceClient(StorageConnectionString);
    //    var tableClient = serviceClient.GetTableClient(TableName);

    //    // Read operation
    //    ReadEntity(tableClient, "PartitionKey", "RowKey");

    //    // Update operation
    //    UpdateEntity(tableClient, "PartitionKey", "RowKey");

    //    // Delete operation
    //    DeleteEntity(tableClient, "PartitionKey", "RowKey");
    //}
    /// <summary>
    /// Tablelient is used to perform CRUD operations on the table.
    /// </summary>
    /// <param name="tableClient"></param>
    /// <param name="partitionKey"></param>
    /// <param name="rowKey"></param>

    public void ReadEntity(TableClient tableClient, PersonEntity person)
    {
        try
        {
            var entity = tableClient.GetEntity<PersonEntity>(person.PartitionKey,person.RowKey);
            Console.WriteLine($"Entity found:");
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            Console.WriteLine("Entity not found.");
        }
    }

    public void CreateEntity(TableClient tableClient, PersonEntity person)
    {
        try
        {
            // Insert the entity
            var response = tableClient.AddEntity<PersonEntity>(person);
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Error creating entity: {ex.Message}");
        }
    }

    public void UpdateEntity(TableClient tableClient,PersonEntity person )
    {
        try
        {
            var entity = tableClient.GetEntity<PersonEntity>(person.PartitionKey, person.RowKey).Value;
            tableClient.UpdateEntity<PersonEntity>(person, ETag.All, TableUpdateMode.Replace);
            Console.WriteLine("Entity updated.");
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Error updating entity: {ex.Message}");
        }
    }

    public  void DeleteEntity(TableClient tableClient,PersonEntity person)
    {
        try
        {
            tableClient.DeleteEntity(person.PartitionKey, person.RowKey);
            Console.WriteLine("Entity deleted.");
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Error deleting entity: {ex.Message}");
        }
    }
}
