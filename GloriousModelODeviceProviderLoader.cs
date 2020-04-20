using RGB.NET.Core;

namespace RGB.NET.Devices.GloriousModelO
{
    class GloriousModelODeviceProviderLoader : IRGBDeviceProviderLoader
    {
        #region Properties & Fields

        /// <inheritdoc />
        public bool RequiresInitialization => false;

        #endregion

        #region Methods

        /// <inheritdoc />
        public IRGBDeviceProvider GetDeviceProvider() => GloriousModelODeviceProvider.Instance;

        #endregion
    }
}
