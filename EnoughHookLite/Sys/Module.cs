using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Sys
{
    public class Module
    {
        public Process Process;

        public sbyte ReadSByte(int adr)
        {
            var data = Process.ReadData(adr, 1);
            return (sbyte)data[0];
        }
        public byte ReadByte(int adr)
        {
            var data = Process.ReadData(adr, 1);
            return data[0];
        }
        public short ReadShort(int adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToInt16(data, 0);
        }
        public ushort ReadUShort(int adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToUInt16(data, 0);
        }
        public int ReadInt(int adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToInt32(data, 0);
        }
        public uint ReadUInt(int adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToUInt32(data, 0);
        }
        public float ReadFloat(int adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToSingle(data, 0);
        }
        public double ReadDouble(int adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToDouble(data, 0);
        }
        public long ReadLong(int adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToInt64(data, 0);
        }
        public ulong ReadULong(int adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToUInt64(data, 0);
        }

        public TStruct ReadStruct<TStruct>(int adr)
        {
            var len = Marshal.SizeOf<TStruct>();
            var data = Process.ReadData(adr, len);
            return ToStruct<TStruct>(data);
        }

        public static TStruct ToStruct<TStruct>(byte[] data)
        {
            var len = data.Length;
            var ptr = Marshal.AllocHGlobal(len);
            var obj = Marshal.PtrToStructure<TStruct>(ptr);
            Marshal.FreeHGlobal(ptr);
            return obj;
        }
    }
}
