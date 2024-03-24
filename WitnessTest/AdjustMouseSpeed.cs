using System;
using System.Runtime.InteropServices;

namespace WitnessTest
{
    internal class AdjustMouseSpeed
    {
        //dll imput that affects mouse sensitivity
        public const UInt32 SPI_SETMOUSESPEED = 0x0071;

        [DllImport("User32.dll")] //imports dll
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            UInt32 pvParam,
            UInt32 fWinIni);

        //adjusts mouse speed based on a uint input, ranging from 0 to 20
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
