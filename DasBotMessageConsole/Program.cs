﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace DasBotMessageConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "HostName=CloudHelloHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Mnn1CUPk4cCVUxzWq3oeRgl1Y8XyrAjl4umWMgLr6ho=";
            string iotHubD2cEndpoint = "messages/events";

            var eventHubClient = EventHubClient.
                CreateFromConnectionString(connectionString, iotHubD2cEndpoint);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            foreach (string partition in d2cPartitions)
            {
                var receiver = eventHubClient.GetDefaultConsumerGroup().
                    CreateReceiver(partition, DateTime.Now);
                ReceiveMessagesFromDeviceAsync(receiver);
            }
            Console.ReadLine();
        }

        async static Task ReceiveMessagesFromDeviceAsync(EventHubReceiver receiver)
        {
            while (true)
            {
                EventData eventData = await receiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine("Message received: '{0}'", data);
            }
        }
    }
}

