using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace DasBotAzureTableStorage
{
    public class Measure
    {
        private readonly string azureTableStorageName;
        private readonly string deviceId;

        private readonly CloudStorageAccount storageAccount;

        public Measure(string deviceId, string azureTableStorageName)
        {
            this.deviceId = deviceId;
            this.azureTableStorageName = azureTableStorageName;
            storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);
        }

        public void WriteData(int temperature, double humidity)
        {
            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "funky" table.
            var table = tableClient.GetTableReference(azureTableStorageName);
            table.CreateIfNotExists();

            // Create a new customer entity.
            var messureData = new MessureDataEntity(deviceId)
            {
                Temperature = temperature,
                Humidity = humidity
            };

            // Create the TableOperation object that inserts the customer entity.
            var insertOperation = TableOperation.Insert(messureData);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }

        public void ReadData()
        {
            //AZURE
            // Create the table client.
            var tableClient2 = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "funky" table.
            var table2 = tableClient2.GetTableReference(azureTableStorageName);

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            var query =
                new TableQuery<MessureDataEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",
                    QueryComparisons.Equal, deviceId));

            Console.WriteLine("Getting data from table");

            // Print the fields for each customer.
            foreach (var entity in table2.ExecuteQuery(query))
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey,
                    entity.Humidity, entity.Temperature);
            }

        }




    }

    public class MessureDataEntity : TableEntity
    {
        public MessureDataEntity(string deviceId)
        {
            this.PartitionKey = deviceId;
            this.RowKey = Guid.NewGuid().ToString();
        }

        public MessureDataEntity()
        {
        }

        public int Temperature { get; set; }

        public double Humidity { get; set; }
    }





}

