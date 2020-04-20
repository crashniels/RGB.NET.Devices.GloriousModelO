// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedMember.Global

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using RGB.NET.Core;


namespace RGB.NET.Devices.GloriousModelO.Native
{
    // ReSharper disable once InconsistentNaming
    internal static class _GloriousModelO
    {
        #region Libary Management

        private static IntPtr _dllHandle = IntPtr.Zero;

        /// <summary>
        /// Gets the loaded architecture (x64/x86).
        /// </summary>
        internal static string LoadedArchitecture { get; private set; }

        /// <summary>
        /// Reloads the SDK.
        /// </summary>
        internal static void Reload()
        {
            Unload_GMO_DLL();
            Load_GMO_DLL();
        }

        private static void Load_GMO_DLL()
        {
            if (_dllHandle != IntPtr.Zero) return;

            // HACK: Load library at runtime to support both, x86 and x64 with one managed dll
            List<string> possiblePathList = Environment.Is64BitProcess ? GloriousModelODeviceProvider.PossibleX64NativePaths : GloriousModelODeviceProvider.PossibleX86NativePaths;
            string dllPath = possiblePathList.FirstOrDefault(File.Exists);
            if (dllPath == null) throw new RGBDeviceException($"Can't find the ModelO at one of the expected locations:\r\n '{string.Join("\r\n", possiblePathList.Select(Path.GetFullPath))}'");

            _dllHandle = LoadLibrary(dllPath);

            _SetColor = (SetColorPointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "SetColor"), typeof(SetColorPointer));

            _DetectDevice = (DetectDevicePointer)Marshal.GetDelegateForFunctionPointer(GetProcAddress(_dllHandle, "DetectDevice"), typeof(DetectDevicePointer));
        }

        private static void Unload_GMO_DLL()
        {
            if (_dllHandle == IntPtr.Zero) return;

            // ReSharper disable once EmptyEmbeddedStatement - DarthAffe 20.02.2016: We might need to reduce the internal reference counter more than once to set the library free
            while (FreeLibrary(_dllHandle)) ;
            _dllHandle = IntPtr.Zero;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr dllHandle);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr dllHandle, string name);

        #endregion

        #region SDK-METHODS

        #region Pointers

        private static SetColorPointer _SetColor;

        private static DetectDevicePointer _DetectDevice;

        #endregion

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SetColorPointer(byte red, byte green, byte blue);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int DetectDevicePointer();

        #endregion

        internal static bool SetColor(byte red, byte green, byte blue) => _SetColor(red, green, blue);

        internal static int DetectDevice() => _DetectDevice();

        #endregion
    }
}