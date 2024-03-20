using System;
using System.Runtime.InteropServices;

namespace WitnessTest
{
    internal class AdjustMouseSpeed
    {
        public const UInt32 SPI_SETMOUSESPEED = 0x0071;

        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            UInt32 pvParam,
            UInt32 fWinIni);

        public static void SetMouseSpeed(uint args)
        {
            SystemParametersInfo(
                SPI_SETMOUSESPEED,
                0,
                args,
                0);
        }
    }
}
