using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.WebSockets;
using DasBotModels;
using Microsoft.ServiceBus.Messaging;

namespace DasBotWeb.Controllers
{
    public class StatusController : ApiController
    {

        private readonly Activity[] data = {
                new Activity {Status = true, Name = ActivityType.Sailing, Description = "Segling"},
                new Activity {Status = false, Name = ActivityType.Fishing, Description = "Fiske"}
            };

        // GET api/<controller>
        //public IEnumerable<Activity> Get()
        //{
        //    return data;
        //}

        //[HttpGet]
        //[Route("status/t")]
        public async Task<IEnumerable<Activity>> Get()
        {
            var internalData = await MessageHandler.StartReceiveAsync();

            var activity = new Activity {Status = true, Name = ActivityType.Sailing, Description = "Segling"};

            activity.Status = double.Parse(internalData) > 35.5;

            var activites = new List<Activity> {activity};

            return activites;
        }
        //[Route("status/{statusName}")]
        //public Activity Get(string statusName)
        //{
        //    return data.Where(x => x.Name == (ActivityType)(statusName));
        //}




    }

    public static class MessageHandler
    {
        private const string connectionString = "HostName=CloudHelloHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Mnn1CUPk4cCVUxzWq3oeRgl1Y8XyrAjl4umWMgLr6ho=";
        private const string iotHubD2cEndpoint = "messages/events";

        public static async Task<string> StartReceiveAsync()
        {
            var eventHubClient = EventHubClient.
                CreateFromConnectionString(connectionString, iotHubD2cEndpoint);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            foreach (string partition in d2cPartitions)
            {
                var receiver = eventHubClient.GetDefaultConsumerGroup().
                    CreateReceiver(partition, DateTime.Now);

                return await ReceiveMessagesFromDeviceAsync(receiver);
                
            }
            return null;
        }

        public static async Task<string> ReceiveMessagesFromDeviceAsync(EventHubReceiver receiver)
        {
            while (true)
            {
                EventData eventData = await receiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());


                Console.WriteLine("Message received: '{0}'", data);

                return data;
            }
        }
    }
}