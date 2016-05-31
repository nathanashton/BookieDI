using System;

namespace Bookie.Format.Mobi.Metadata
{
    public static class Converter
    {
        public static short ToInt16(byte[] bytes)
        {
            return BitConverter.ToInt16(CheckBytes(bytes), 0);
        }

        public static int ToInt32(byte[] bytes)
        {
            return BitConverter.ToInt32(CheckBytes(bytes), 0);
        }

        public static long ToInt64(byte[] bytes)
        {
            return BitConverter.ToInt64(CheckBytes(bytes), 0);
        }

        public static ushort ToUInt16(byte[] bytes)
        {
            return BitConverter.ToUInt16(CheckBytes(bytes), 0);
        }

        public static uint ToUInt32(byte[] bytes)
        {
            return BitConverter.ToUInt32(CheckBytes(bytes), 0);
        }

        public static ulong ToUInt64(byte[] bytes)
        {
            return BitConverter.ToUInt64(CheckBytes(bytes), 0);
        }
        
        //Checks to see if system architecture is little-endian (e.g. little end first) and if so reverse the byte array
        private static byte[] CheckBytes(byte[] bytesToCheck)
        {
            //Make copy so we're not permanently reversing the order of the bytes in the actual field
            byte[] buffer = (byte[])bytesToCheck.Clone();

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            return buffer;
        }
    }
}
