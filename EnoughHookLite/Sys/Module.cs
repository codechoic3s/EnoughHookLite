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
        public Process Process { get; private set; }

        public int BaseAdr { get; private set; }
        public int Size { get; private set; }
        public uint SizeAdr { get; private set; }

        private System.Diagnostics.ProcessModule pModule;

        public Module(System.Diagnostics.ProcessModule pm, Process proc)
        {
            pModule = pm;
            Process = proc;
            BaseAdr = (int)pm.BaseAddress;
            Size = pm.ModuleMemorySize;
            SizeAdr = (uint)(BaseAdr + Size);
        }
        #region LONG
        public sbyte ReadSByte(long adr)
        {
            var data = Process.ReadData(adr, 1);
            return (sbyte)data[0];
        }
        public byte ReadByte(long adr)
        {
            var data = Process.ReadData(adr, 1);
            return data[0];
        }
        public short ReadShort(long adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToInt16(data, 0);
        }
        public ushort ReadUShort(long adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToUInt16(data, 0);
        }
        public int ReadInt(long adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToInt32(data, 0);
        }
        public uint ReadUInt(long adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToUInt32(data, 0);
        }
        public float ReadFloat(long adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToSingle(data, 0);
        }
        public double ReadDouble(long adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToDouble(data, 0);
        }
        public long ReadLong(long adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToInt64(data, 0);
        }
        public ulong ReadULong(long adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToUInt64(data, 0);
        }
        public string ReadString(long adr, int size, Encoding enc)
        {
            var data = Process.ReadData(adr, size);
            var text = enc.GetString(data);
            if (text.Contains('\0'))
            {
                text = text.Substring(0, text.IndexOf('\0'));
            }
            return text;
        }
        public TStruct ReadStruct<TStruct>(long adr)
        {
            var len = Marshal.SizeOf<TStruct>();
            var data = Process.ReadData(adr, len);
            return ToStruct<TStruct>(data);
        }
        public IntPtr ReadIntPtr(long adr)
        {
            var data = Process.ReadData(adr, 4);
            return (IntPtr)BitConverter.ToInt32(data, 0);
        }
        #endregion
        #region ULONG
        public sbyte ReadSByte(ulong adr)
        {
            var data = Process.ReadData(adr, 1);
            return (sbyte)data[0];
        }
        public byte ReadByte(ulong adr)
        {
            var data = Process.ReadData(adr, 1);
            return data[0];
        }
        public short ReadShort(ulong adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToInt16(data, 0);
        }
        public ushort ReadUShort(ulong adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToUInt16(data, 0);
        }
        public int ReadInt(ulong adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToInt32(data, 0);
        }
        public uint ReadUInt(ulong adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToUInt32(data, 0);
        }
        public float ReadFloat(ulong adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToSingle(data, 0);
        }
        public double ReadDouble(ulong adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToDouble(data, 0);
        }
        public long ReadLong(ulong adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToInt64(data, 0);
        }
        public ulong ReadULong(ulong adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToUInt64(data, 0);
        }
        public string ReadString(ulong adr, int size, Encoding enc)
        {
            var data = Process.ReadData(adr, size);
            var text = enc.GetString(data);
            if (text.Contains('\0'))
            {
                text = text.Substring(0, text.IndexOf('\0'));
            }
            return text;
        }
        public TStruct ReadStruct<TStruct>(ulong adr)
        {
            var len = Marshal.SizeOf<TStruct>();
            var data = Process.ReadData(adr, len);
            return ToStruct<TStruct>(data);
        }
        public IntPtr ReadIntPtr(ulong adr)
        {
            var data = Process.ReadData(adr, 4);
            return (IntPtr)BitConverter.ToInt32(data, 0);
        }
        #endregion
        #region INT
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
        public string ReadString(int adr, int size, Encoding enc)
        {
            var data = Process.ReadData(adr, size);
            var text = enc.GetString(data);
            if (text.Contains('\0'))
            {
                text = text.Substring(0, text.IndexOf('\0'));
            }
            return text;
        }
        public TStruct ReadStruct<TStruct>(int adr)
        {
            var len = Marshal.SizeOf<TStruct>();
            var data = Process.ReadData(adr, len);
            return ToStruct<TStruct>(data);
        }
        public IntPtr ReadIntPtr(int adr)
        {
            var data = Process.ReadData(adr, 4);
            return (IntPtr)BitConverter.ToInt32(data, 0);
        }
        #endregion
        #region UINT
        public sbyte ReadSByte(uint adr)
        {
            var data = Process.ReadData(adr, 1);
            return (sbyte)data[0];
        }
        public byte ReadByte(uint adr)
        {
            var data = Process.ReadData(adr, 1);
            return data[0];
        }
        public short ReadShort(uint adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToInt16(data, 0);
        }
        public ushort ReadUShort(uint adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToUInt16(data, 0);
        }
        public int ReadInt(uint adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToInt32(data, 0);
        }
        public uint ReadUInt(uint adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToUInt32(data, 0);
        }
        public float ReadFloat(uint adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToSingle(data, 0);
        }
        public double ReadDouble(uint adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToDouble(data, 0);
        }
        public long ReadLong(uint adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToInt64(data, 0);
        }
        public ulong ReadULong(uint adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToUInt64(data, 0);
        }
        public string ReadString(uint adr, int size, Encoding enc)
        {
            var data = Process.ReadData(adr, size);
            var text = enc.GetString(data);
            if (text.Contains('\0'))
            {
                text = text.Substring(0, text.IndexOf('\0'));
            }
            return text;
        }
        public TStruct ReadStruct<TStruct>(uint adr)
        {
            var len = Marshal.SizeOf<TStruct>();
            var data = Process.ReadData(adr, len);
            return ToStruct<TStruct>(data);
        }
        public IntPtr ReadIntPtr(uint adr)
        {
            var data = Process.ReadData(adr, 4);
            return (IntPtr)BitConverter.ToInt32(data, 0);
        }
        #endregion
        #region INTPTR
        public sbyte ReadSByte(IntPtr adr)
        {
            var data = Process.ReadData(adr, 1);
            return (sbyte)data[0];
        }
        public byte ReadByte(IntPtr adr)
        {
            var data = Process.ReadData(adr, 1);
            return data[0];
        }
        public short ReadShort(IntPtr adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToInt16(data, 0);
        }
        public ushort ReadUShort(IntPtr adr)
        {
            var data = Process.ReadData(adr, 2);
            return BitConverter.ToUInt16(data, 0);
        }
        public int ReadInt(IntPtr adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToInt32(data, 0);
        }
        public uint ReadUInt(IntPtr adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToUInt32(data, 0);
        }
        public float ReadFloat(IntPtr adr)
        {
            var data = Process.ReadData(adr, 4);
            return BitConverter.ToSingle(data, 0);
        }
        public double ReadDouble(IntPtr adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToDouble(data, 0);
        }
        public long ReadLong(IntPtr adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToInt64(data, 0);
        }
        public ulong ReadULong(IntPtr adr)
        {
            var data = Process.ReadData(adr, 8);
            return BitConverter.ToUInt64(data, 0);
        }
        public string ReadString(IntPtr adr, int size, Encoding enc)
        {
            var data = Process.ReadData(adr, size);
            var text = enc.GetString(data);
            if (text.Contains('\0'))
            {
                text = text.Substring(0, text.IndexOf('\0'));
            }
            return text;
        }
        public TStruct ReadStruct<TStruct>(IntPtr adr)
        {
            var len = Marshal.SizeOf<TStruct>();
            var data = Process.ReadData(adr, len);
            return ToStruct<TStruct>(data);
        }
        public IntPtr ReadIntPtr(IntPtr adr)
        {
            var data = Process.ReadData(adr, 4);
            return (IntPtr)BitConverter.ToInt32(data, 0);
        }
        #endregion

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
