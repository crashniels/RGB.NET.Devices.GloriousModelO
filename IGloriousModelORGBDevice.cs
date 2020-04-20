using RGB.NET.Core;

namespace RGB.NET.Devices.GloriousModelO
{
    /// <summary>
    /// Represents a Corsair Link RGB-device.
    /// </summary>
    internal interface IGloriousModelORGBDevice : IRGBDevice
    {
        void Initialize();
    }
}