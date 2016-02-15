using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace DasBotIoT
{
    public static class MessageHandler
    {
        private const string primaryKey = "Z/z+NcxiFgqD11WNp6AFlVCgK6EyUiSYPWdj4LvGPbE=";

        public static async void Send(string str)
        {
            string iotHubUri = "CloudHelloHub.azure-devices.net"; // ! put in value !
            string deviceId = "DasBotDevice"; // ! put in value !
            string deviceKey = primaryKey; // ! put in value !

            var deviceClient = DeviceClient.Create(iotHubUri,
                    AuthenticationMethodFactory.
                        CreateAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey),
                    TransportType.Http1);

            var message = new Message(Encoding.ASCII.GetBytes(str));
            try
            {
                await deviceClient.SendEventAsync(message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
        }
    }
}
