using RGB.NET.Core;
using RGB.NET.Devices.GloriousModelO.Native;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RGB.NET.Devices.GloriousModelO
{
    public class GloriousModelORGBDevice : AbstractRGBDevice<GloriousModelORGBDeviceInfo>, IGloriousModelORGBDevice
    {
        public override GloriousModelORGBDeviceInfo DeviceInfo { get; }


        public GloriousModelORGBDevice(GloriousModelORGBDeviceInfo info)
        {
            this.DeviceInfo = info;
        }

        public void Initialize()
        {
            InitializeLayout();

            //1 time code
        }

        protected override object CreateLedCustomData(LedId ledId) => (int)ledId - (int)LedId.Mouse1;

        public void InitializeLayout()
        {
            InitializeLed(LedId.Custom1, new Rectangle(0, 0, 10, 10));
        }


        protected override void UpdateLeds(IEnumerable<Led> ledsToUpdate)
        {
            Led led = ledsToUpdate.FirstOrDefault(x => x.Color.A > 0);
            if (led == null) return;
            try
            {

                _GloriousModelO.SetColor
                    (
                    System.Convert.ToByte(led.Color.R / (led.Color.A / 255)),
                    System.Convert.ToByte(led.Color.G / (led.Color.A / 255)),
                    System.Convert.ToByte(led.Color.B / (led.Color.A / 255))
                    );

            }
            catch
            {

            }

        }

    }
}

