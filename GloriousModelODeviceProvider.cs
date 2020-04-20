using RGB.NET.Core;
using RGB.NET.Devices.GloriousModelO.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RGB.NET.Devices.GloriousModelO
{
    public class GloriousModelODeviceProvider : IRGBDeviceProvider
    {

        #region Properties & Fields

        private static GloriousModelODeviceProvider _instance;
        /// <summary>
        /// Gets the singleton <see cref="CorsarLinkDeviceProvider"/> instance.
        /// </summary>
        public static GloriousModelODeviceProvider Instance => _instance ?? new GloriousModelODeviceProvider();

        /// <summary>
        /// Gets a modifiable list of paths used to find the native SDK-dlls for x86 applications.
        /// The first match will be used.
        /// </summary>
        public static List<string> PossibleX86NativePaths { get; } = new List<string> { "x86/GloriousModelO.dll" };

        /// <summary>
        /// Gets a modifiable list of paths used to find the native SDK-dlls for x64 applications.
        /// The first match will be used.
        /// </summary>
        public static List<string> PossibleX64NativePaths { get; } = new List<string> { "x64/GloriousModelO.dll" };

        public bool HasExclusiveAccess => false; // we don't really need this


        public bool IsInitialized { get; private set; }

        public IEnumerable<IRGBDevice> Devices { get; private set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GloriousModelODeviceProvider"/> class.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if this constructor is called even if there is already an instance of this class.</exception>
        public GloriousModelODeviceProvider()
        {
            if (_instance != null) throw new InvalidOperationException($"There can be only one instanc of type {nameof(GloriousModelODeviceProvider)}");
            _instance = this;
        }

        public bool Initialize(RGBDeviceType loadFilter = RGBDeviceType.All, bool exclusiveAccessIfPossible = false, bool throwExceptions = false)
        {
            IsInitialized = false;
            try
            {
                _GloriousModelO.Reload();
                IList<IRGBDevice> devices = new List<IRGBDevice>();
                IGloriousModelORGBDevice device = new GloriousModelORGBDevice(new GloriousModelORGBDeviceInfo());
                device.Initialize();
                devices.Add(device);
                Devices = new ReadOnlyCollection<IRGBDevice>(devices);
                if (_GloriousModelO.DetectDevice() == 1)
                {
                    IsInitialized = true;
                }
            }
            catch
            {
                if (throwExceptions)
                    throw;
                else
                    return false;
            }


            return true;
        }

        public void ResetDevices()
        {
            // we don't really need this
        }

        public void Dispose()
        {

        }
    }
}


