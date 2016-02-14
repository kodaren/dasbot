using System;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;
using System.Globalization;

namespace DasBotIoT
{
    public class TemperatureSensor : SenseHatUtility
    {
        public static CultureInfo CultureInfoEn = new CultureInfo("en-GB");
        public TemperatureSensor(ISenseHat senseHat)
            : base(senseHat)
        {
        }

        //private int? lastTemperature = null;
        public override void Run()
        {
            
            ISenseHatDisplay display = SenseHat.Display;

            while (true)
            {
                SenseHat.Sensors.HumiditySensor.Update();

                if (SenseHat.Sensors.Temperature.HasValue)
                {
                    var temperature = SenseHat.Sensors.Temperature.Value.ToString("#.##", CultureInfoEn);
                    var json = "{ temperature: " + temperature + " }";
                    MessageHandler.Send(json);
                    //if (lastTemperature != temperature)
                    //{
                    //    lastTemperature = temperature;
                    //    MessageHandler.Send(temperature.ToString());
                    //}
                    // Sleep quite some time; the temperature usually change quite slowly...
                    Sleep(TimeSpan.FromSeconds(10));
                    
                }
                else
                {
                    // Rapid update until there is a temperature reading.
                    Sleep(TimeSpan.FromSeconds(0.5));
                }
            }
        }


    }
}