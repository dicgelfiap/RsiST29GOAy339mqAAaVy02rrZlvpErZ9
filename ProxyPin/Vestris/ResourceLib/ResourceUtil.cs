using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D35 RID: 3381
	[ComVisible(true)]
	public static class ResourceUtil
	{
		// Token: 0x0600895E RID: 35166 RVA: 0x00294D9C File Offset: 0x00294D9C
		internal static IntPtr Align(long p)
		{
			return new IntPtr(p + 3L & -4L);
		}

		// Token: 0x0600895F RID: 35167 RVA: 0x00294DAC File Offset: 0x00294DAC
		internal static IntPtr Align(IntPtr p)
		{
			return ResourceUtil.Align(p.ToInt64());
		}

		// Token: 0x06008960 RID: 35168 RVA: 0x00294DBC File Offset: 0x00294DBC
		internal static long PadToWORD(BinaryWriter w)
		{
			long num = w.BaseStream.Position;
			if (num % 2L != 0L)
			{
				long num2 = 2L - num % 2L;
				ResourceUtil.Pad(w, (ushort)num2);
				num += num2;
			}
			return num;
		}

		// Token: 0x06008961 RID: 35169 RVA: 0x00294DF8 File Offset: 0x00294DF8
		internal static long PadToDWORD(BinaryWriter w)
		{
			long num = w.BaseStream.Position;
			if (num % 4L != 0L)
			{
				long num2 = 4L - num % 4L;
				ResourceUtil.Pad(w, (ushort)num2);
				num += num2;
			}
			return num;
		}

		// Token: 0x06008962 RID: 35170 RVA: 0x00294E34 File Offset: 0x00294E34
		internal static ushort HiWord(uint value)
		{
			return (ushort)((value & 4294901760U) >> 16);
		}

		// Token: 0x06008963 RID: 35171 RVA: 0x00294E44 File Offset: 0x00294E44
		internal static ushort LoWord(uint value)
		{
			return (ushort)(value & 65535U);
		}

		// Token: 0x06008964 RID: 35172 RVA: 0x00294E50 File Offset: 0x00294E50
		internal static void WriteAt(BinaryWriter w, long value, long address)
		{
			long position = w.BaseStream.Position;
			w.Seek((int)address, SeekOrigin.Begin);
			w.Write((ushort)value);
			w.Seek((int)position, SeekOrigin.Begin);
		}

		// Token: 0x06008965 RID: 35173 RVA: 0x00294E8C File Offset: 0x00294E8C
		internal static long Pad(BinaryWriter w, ushort len)
		{
			for (;;)
			{
				ushort num = len;
				len = num - 1;
				if (num <= 0)
				{
					break;
				}
				w.Write(0);
			}
			return w.BaseStream.Position;
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x06008966 RID: 35174 RVA: 0x00294EB0 File Offset: 0x00294EB0
		public static ushort NEUTRALLANGID
		{
			get
			{
				return ResourceUtil.MAKELANGID(0, 0);
			}
		}

		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x06008967 RID: 35175 RVA: 0x00294EBC File Offset: 0x00294EBC
		public static ushort USENGLISHLANGID
		{
			get
			{
				return ResourceUtil.MAKELANGID(9, 1);
			}
		}

		// Token: 0x06008968 RID: 35176 RVA: 0x00294EC8 File Offset: 0x00294EC8
		public static ushort MAKELANGID(int primary, int sub)
		{
			return (ushort)((int)((ushort)sub) << 10 | (int)((ushort)primary));
		}

		// Token: 0x06008969 RID: 35177 RVA: 0x00294ED4 File Offset: 0x00294ED4
		public static ushort PRIMARYLANGID(ushort lcid)
		{
			return lcid & 1023;
		}

		// Token: 0x0600896A RID: 35178 RVA: 0x00294EE0 File Offset: 0x00294EE0
		public static ushort SUBLANGID(ushort lcid)
		{
			return (ushort)(lcid >> 10);
		}

		// Token: 0x0600896B RID: 35179 RVA: 0x00294EE8 File Offset: 0x00294EE8
		internal static byte[] GetBytes<T>(T anything)
		{
			int num = Marshal.SizeOf(anything);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr(anything, intPtr, false);
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			return array;
		}

		// Token: 0x0600896C RID: 35180 RVA: 0x00294F30 File Offset: 0x00294F30
		internal static List<string> FlagsToList<T>(uint flagValue)
		{
			List<string> list = new List<string>();
			foreach (object obj in Enum.GetValues(typeof(T)))
			{
				T t = (T)((object)obj);
				uint num = Convert.ToUInt32(t);
				if ((flagValue & num) > 0U || flagValue == num)
				{
					list.Add(t.ToString());
				}
			}
			return list;
		}

		// Token: 0x0600896D RID: 35181 RVA: 0x00294FD0 File Offset: 0x00294FD0
		internal static string FlagsToString<T>(uint flagValue)
		{
			List<string> list = new List<string>();
			list.AddRange(ResourceUtil.FlagsToList<T>(flagValue));
			return string.Join(" | ", list.ToArray());
		}
	}
}
