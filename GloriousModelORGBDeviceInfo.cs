using RGB.NET.Core;
using System;

namespace RGB.NET.Devices.GloriousModelO
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a generic information for a Corsair Link-<see cref="T:RGB.NET.Core.IRGBDevice" />.
    /// </summary>
    public class GloriousModelORGBDeviceInfo : IRGBDeviceInfo
    {


        public RGBDeviceType DeviceType  => RGBDeviceType.Mouse;

        public string DeviceName => "Glorious Model O";

        public string Manufacturer => "Glorious";

        public string Model => "Model O";

        RGBDeviceLighting IRGBDeviceInfo.Lighting => RGBDeviceLighting.Device;

        bool IRGBDeviceInfo.SupportsSyncBack => false;

        Uri IRGBDeviceInfo.Image { get; set; }



    }
}