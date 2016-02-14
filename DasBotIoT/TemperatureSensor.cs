using System;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;

namespace DasBotIoT
{
    public class TemperatureSensor : SenseHatUtility
    {
        public TemperatureSensor(ISenseHat senseHat)
            : base(senseHat)
        {
        }

        private int? lastTemperature = null;
        public override void Run()
        {
            int readings = 10;
            ISenseHatDisplay display = SenseHat.Display;

            while (readings > 0)
            {
                SenseHat.Sensors.HumiditySensor.Update();

                if (SenseHat.Sensors.Temperature.HasValue)
                {
                    int temperature = (int)Math.Round(SenseHat.Sensors.Temperature.Value);
                    if (lastTemperature != temperature)
                    {
                        lastTemperature = temperature;
                        MessageHandler.Send(temperature.ToString());
                        //TODO: SendMessage
                    }
                    // Sleep quite some time; the temperature usually change quite slowly...
                    Sleep(TimeSpan.FromSeconds(5));
                    readings--;
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