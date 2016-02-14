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


        public override void Run()
        {
            var tinyFont = new TinyFont();

            ISenseHatDisplay display = SenseHat.Display;

            while (true)
            {
                SenseHat.Sensors.HumiditySensor.Update();

                if (SenseHat.Sensors.Temperature.HasValue)
                {
                    int temperature = (int)Math.Round(SenseHat.Sensors.Temperature.Value);
                    string text = temperature.ToString();

                    if (text.Length > 2)
                    {
                        // Too long to fit the display!
                        text = "**";
                    }

                    display.Clear();
                    tinyFont.Write(display, text, Colors.White);
                    display.Update();

                    // Sleep quite some time; the temperature usually change quite slowly...
                    Sleep(TimeSpan.FromSeconds(5));
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