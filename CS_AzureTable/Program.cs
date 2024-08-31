
// See https://aka.ms/new-console-template for more information
using Azure.Data.Tables;
using CS_AzureTable;

Console.WriteLine("Azure TAble Storage");

string StorageConnectionString = "CONNSTR";
string TableName = "Person";

//TableServiceClient: Create a Client to interact with the Table Service
var serviceClient = new TableServiceClient(StorageConnectionString);
//TableClient: Create a Client to interact with the Table
var tableClient = serviceClient.GetTableClient(TableName);
// Create a table if it is not exists
tableClient.CreateIfNotExists();
Console.WriteLine("Table Created");
 

TableStorageExample tableStorageExample = new TableStorageExample();

// Create a new entity
var person = new PersonEntity()
{
    Name = "Mahesh Sabnis",
    Email = "m.s@ms.com",
    PhoneNumber = "1234567890",
    City = "Pune",
    PartitionKey = "Pune",
    RowKey = "1"
};

// Insert the entity
tableStorageExample.DeleteEntity(tableClient, person);

Console.ReadLine();