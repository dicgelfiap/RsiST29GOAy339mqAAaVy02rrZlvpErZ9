using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using PeNet.Structures;

namespace PeNet.Utilities
{
	// Token: 0x02000B94 RID: 2964
	[ComVisible(true)]
	public static class ExtensionMethods
	{
		// Token: 0x06007760 RID: 30560 RVA: 0x00239C9C File Offset: 0x00239C9C
		private static ushort BytesToUInt16(byte b1, byte b2)
		{
			return BitConverter.ToUInt16(new byte[]
			{
				b1,
				b2
			}, 0);
		}

		// Token: 0x06007761 RID: 30561 RVA: 0x00239CB4 File Offset: 0x00239CB4
		public static ushort BytesToUInt16(this byte[] buff, ulong offset)
		{
			return checked(ExtensionMethods.BytesToUInt16(buff[(int)((IntPtr)offset)], buff[(int)((IntPtr)(unchecked(offset + 1UL)))]));
		}

		// Token: 0x06007762 RID: 30562 RVA: 0x00239CC8 File Offset: 0x00239CC8
		public static uint BytesToUInt16(this byte[] buff, uint offset, uint numOfBytes)
		{
			byte[] array = new byte[2];
			int num = 0;
			while ((long)num < (long)((ulong)numOfBytes))
			{
				array[num] = buff[(int)(checked((IntPtr)(unchecked((ulong)offset + (ulong)((long)num)))))];
				num++;
			}
			return (uint)BitConverter.ToUInt16(array, 0);
		}

		// Token: 0x06007763 RID: 30563 RVA: 0x00239D04 File Offset: 0x00239D04
		private static uint BytesToUInt32(byte b1, byte b2, byte b3, byte b4)
		{
			return BitConverter.ToUInt32(new byte[]
			{
				b1,
				b2,
				b3,
				b4
			}, 0);
		}

		// Token: 0x06007764 RID: 30564 RVA: 0x00239D24 File Offset: 0x00239D24
		public static uint BytesToUInt32(this byte[] buff, uint offset)
		{
			return ExtensionMethods.BytesToUInt32(buff[(int)offset], buff[(int)(offset + 1U)], buff[(int)(offset + 2U)], buff[(int)(offset + 3U)]);
		}

		// Token: 0x06007765 RID: 30565 RVA: 0x00239D40 File Offset: 0x00239D40
		public static uint BytesToUInt32(this byte[] buff, uint offset, uint numOfBytes)
		{
			byte[] array = new byte[4];
			int num = 0;
			while ((long)num < (long)((ulong)numOfBytes))
			{
				array[num] = buff[(int)(checked((IntPtr)(unchecked((ulong)offset + (ulong)((long)num)))))];
				num++;
			}
			return BitConverter.ToUInt32(array, 0);
		}

		// Token: 0x06007766 RID: 30566 RVA: 0x00239D7C File Offset: 0x00239D7C
		public static uint BytesToUInt32(this byte[] buff, uint offset, uint numOfBytes, ref uint count)
		{
			byte[] array = new byte[4];
			int num = 0;
			while ((long)num < (long)((ulong)numOfBytes))
			{
				array[num] = buff[(int)(checked((IntPtr)(unchecked((ulong)offset + (ulong)((long)num)))))];
				num++;
			}
			count += numOfBytes;
			return BitConverter.ToUInt32(array, 0);
		}

		// Token: 0x06007767 RID: 30567 RVA: 0x00239DC0 File Offset: 0x00239DC0
		private static int BytesToInt32(byte b1, byte b2, byte b3, byte b4)
		{
			return BitConverter.ToInt32(new byte[]
			{
				b1,
				b2,
				b3,
				b4
			}, 0);
		}

		// Token: 0x06007768 RID: 30568 RVA: 0x00239DE0 File Offset: 0x00239DE0
		public static int BytesToInt32(this byte[] buff, uint offset)
		{
			return ExtensionMethods.BytesToInt32(buff[(int)offset], buff[(int)(offset + 1U)], buff[(int)(offset + 2U)], buff[(int)(offset + 3U)]);
		}

		// Token: 0x06007769 RID: 30569 RVA: 0x00239DFC File Offset: 0x00239DFC
		public static int BytesToInt32(this byte[] buff, uint offset, uint numOfBytes)
		{
			byte[] array = new byte[4];
			int num = 0;
			while ((long)num < (long)((ulong)numOfBytes))
			{
				array[num] = buff[(int)(checked((IntPtr)(unchecked((ulong)offset + (ulong)((long)num)))))];
				num++;
			}
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x0600776A RID: 30570 RVA: 0x00239E38 File Offset: 0x00239E38
		public static int BytesToInt32(this byte[] buff, uint offset, uint numOfBytes, ref uint count)
		{
			byte[] array = new byte[4];
			int num = 0;
			while ((long)num < (long)((ulong)numOfBytes))
			{
				array[num] = buff[(int)(checked((IntPtr)(unchecked((ulong)offset + (ulong)((long)num)))))];
				num++;
			}
			count += numOfBytes;
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x0600776B RID: 30571 RVA: 0x00239E7C File Offset: 0x00239E7C
		private static ulong BytesToUInt64(byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7, byte b8)
		{
			return BitConverter.ToUInt64(new byte[]
			{
				b1,
				b2,
				b3,
				b4,
				b5,
				b6,
				b7,
				b8
			}, 0);
		}

		// Token: 0x0600776C RID: 30572 RVA: 0x00239EB0 File Offset: 0x00239EB0
		public static ulong BytesToUInt64(this byte[] buff, ulong offset)
		{
			return checked(ExtensionMethods.BytesToUInt64(buff[(int)((IntPtr)offset)], buff[(int)((IntPtr)(unchecked(offset + 1UL)))], buff[(int)((IntPtr)(unchecked(offset + 2UL)))], buff[(int)((IntPtr)(unchecked(offset + 3UL)))], buff[(int)((IntPtr)(unchecked(offset + 4UL)))], buff[(int)((IntPtr)(unchecked(offset + 5UL)))], buff[(int)((IntPtr)(unchecked(offset + 6UL)))], buff[(int)((IntPtr)(unchecked(offset + 7UL)))]));
		}

		// Token: 0x0600776D RID: 30573 RVA: 0x00239EFC File Offset: 0x00239EFC
		public static ulong BytesToUInt64(this byte[] buff, uint offset, uint numOfBytes)
		{
			byte[] array = new byte[8];
			int num = 0;
			while ((long)num < (long)((ulong)numOfBytes))
			{
				array[num] = buff[(int)(checked((IntPtr)(unchecked((ulong)offset + (ulong)((long)num)))))];
				num++;
			}
			return BitConverter.ToUInt64(array, 0);
		}

		// Token: 0x0600776E RID: 30574 RVA: 0x00239F38 File Offset: 0x00239F38
		private static byte[] UInt16ToBytes(ushort value)
		{
			return BitConverter.GetBytes(value);
		}

		// Token: 0x0600776F RID: 30575 RVA: 0x00239F40 File Offset: 0x00239F40
		public static void SetUInt16(this byte[] buff, ulong offset, ushort value)
		{
			byte[] array = ExtensionMethods.UInt16ToBytes(value);
			checked
			{
				buff[(int)((IntPtr)offset)] = array[0];
				buff[(int)((IntPtr)(unchecked(offset + 1UL)))] = array[1];
			}
		}

		// Token: 0x06007770 RID: 30576 RVA: 0x00239F6C File Offset: 0x00239F6C
		private static byte[] UInt32ToBytes(uint value)
		{
			return BitConverter.GetBytes(value);
		}

		// Token: 0x06007771 RID: 30577 RVA: 0x00239F74 File Offset: 0x00239F74
		private static byte[] UInt64ToBytes(ulong value)
		{
			return BitConverter.GetBytes(value);
		}

		// Token: 0x06007772 RID: 30578 RVA: 0x00239F7C File Offset: 0x00239F7C
		public static void SetUInt32(this byte[] buff, uint offset, uint value)
		{
			byte[] array = ExtensionMethods.UInt32ToBytes(value);
			buff[(int)offset] = array[0];
			buff[(int)(offset + 1U)] = array[1];
			buff[(int)(offset + 2U)] = array[2];
			buff[(int)(offset + 3U)] = array[3];
		}

		// Token: 0x06007773 RID: 30579 RVA: 0x00239FB4 File Offset: 0x00239FB4
		public static void SetUInt64(this byte[] buff, ulong offset, ulong value)
		{
			byte[] array = ExtensionMethods.UInt64ToBytes(value);
			checked
			{
				buff[(int)((IntPtr)offset)] = array[0];
				buff[(int)((IntPtr)(unchecked(offset + 1UL)))] = array[1];
				buff[(int)((IntPtr)(unchecked(offset + 2UL)))] = array[2];
				buff[(int)((IntPtr)(unchecked(offset + 3UL)))] = array[3];
				buff[(int)((IntPtr)(unchecked(offset + 4UL)))] = array[4];
				buff[(int)((IntPtr)(unchecked(offset + 5UL)))] = array[5];
				buff[(int)((IntPtr)(unchecked(offset + 6UL)))] = array[6];
				buff[(int)((IntPtr)(unchecked(offset + 7UL)))] = array[7];
			}
		}

		// Token: 0x06007774 RID: 30580 RVA: 0x0023A01C File Offset: 0x0023A01C
		public static ulong VAtoFileMapping(this ulong VA, ICollection<IMAGE_SECTION_HEADER> sh)
		{
			VA -= sh.FirstOrDefault<IMAGE_SECTION_HEADER>().ImageBaseAddress;
			List<IMAGE_SECTION_HEADER> list = (from x in sh
			orderby x.VirtualAddress
			select x).ToList<IMAGE_SECTION_HEADER>();
			uint num = 0U;
			uint num2 = 0U;
			bool flag = false;
			for (int i = 0; i < list.Count - 1; i++)
			{
				if ((ulong)list[i].VirtualAddress <= VA && (ulong)list[i + 1].VirtualAddress > VA)
				{
					num = list[i].VirtualAddress;
					num2 = list[i].PointerToRawData;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (VA < (ulong)list.Last<IMAGE_SECTION_HEADER>().VirtualAddress || VA > (ulong)(list.Last<IMAGE_SECTION_HEADER>().VirtualSize + list.Last<IMAGE_SECTION_HEADER>().VirtualAddress))
				{
					throw new Exception("Cannot find corresponding section.");
				}
				num = list.Last<IMAGE_SECTION_HEADER>().VirtualAddress;
				num2 = list.Last<IMAGE_SECTION_HEADER>().PointerToRawData;
			}
			return VA - (ulong)num + (ulong)num2;
		}

		// Token: 0x06007775 RID: 30581 RVA: 0x0023A13C File Offset: 0x0023A13C
		public static uint VAtoFileMapping(this uint VA, ICollection<IMAGE_SECTION_HEADER> sh)
		{
			return ((uint)((ulong)VA)).VAtoFileMapping(sh);
		}

		// Token: 0x06007776 RID: 30582 RVA: 0x0023A148 File Offset: 0x0023A148
		public static ulong RVAtoFileMapping(this ulong RVA, ICollection<IMAGE_SECTION_HEADER> sh)
		{
			List<IMAGE_SECTION_HEADER> list = (from x in sh
			orderby x.VirtualAddress
			select x).ToList<IMAGE_SECTION_HEADER>();
			uint num = 0U;
			uint num2 = 0U;
			bool flag = false;
			for (int i = 0; i < list.Count - 1; i++)
			{
				if ((ulong)list[i].VirtualAddress <= RVA && (ulong)list[i + 1].VirtualAddress > RVA)
				{
					num = list[i].VirtualAddress;
					num2 = list[i].PointerToRawData;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (RVA < (ulong)list.Last<IMAGE_SECTION_HEADER>().VirtualAddress || RVA > (ulong)(list.Last<IMAGE_SECTION_HEADER>().VirtualSize + list.Last<IMAGE_SECTION_HEADER>().VirtualAddress))
				{
					throw new Exception("Cannot find corresponding section.");
				}
				num = list.Last<IMAGE_SECTION_HEADER>().VirtualAddress;
				num2 = list.Last<IMAGE_SECTION_HEADER>().PointerToRawData;
			}
			return RVA - (ulong)num + (ulong)num2;
		}

		// Token: 0x06007777 RID: 30583 RVA: 0x0023A25C File Offset: 0x0023A25C
		public static uint RVAtoFileMapping(this uint RVA, ICollection<IMAGE_SECTION_HEADER> sh)
		{
			return (uint)((ulong)RVA).RVAtoFileMapping(sh);
		}

		// Token: 0x06007778 RID: 30584 RVA: 0x0023A268 File Offset: 0x0023A268
		public static uint? SafeRVAtoFileMapping(this uint rva, ICollection<IMAGE_SECTION_HEADER> sh)
		{
			uint? result;
			try
			{
				result = new uint?(rva.RVAtoFileMapping(sh));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06007779 RID: 30585 RVA: 0x0023A2A8 File Offset: 0x0023A2A8
		public static string ToHexString(this ICollection<byte> bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(bytes.Count * 2);
			foreach (byte b in bytes)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return string.Format("0x{0}", stringBuilder);
		}

		// Token: 0x0600777A RID: 30586 RVA: 0x0023A328 File Offset: 0x0023A328
		public static string ToHexString(this ICollection<ushort> values)
		{
			if (values == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(values.Count * 2);
			foreach (ushort num in values)
			{
				stringBuilder.AppendFormat("{0:X4}", num);
			}
			return string.Format("0x{0}", stringBuilder);
		}

		// Token: 0x0600777B RID: 30587 RVA: 0x0023A3A8 File Offset: 0x0023A3A8
		public static string ToHexString(this byte value)
		{
			return string.Format("0x{0:X2}", value);
		}

		// Token: 0x0600777C RID: 30588 RVA: 0x0023A3BC File Offset: 0x0023A3BC
		public static string ToHexString(this ushort value)
		{
			return string.Format("0x{0:X4}", value);
		}

		// Token: 0x0600777D RID: 30589 RVA: 0x0023A3D0 File Offset: 0x0023A3D0
		public static string ToHexString(this uint value)
		{
			return string.Format("0x{0:X8}", value);
		}

		// Token: 0x0600777E RID: 30590 RVA: 0x0023A3E4 File Offset: 0x0023A3E4
		public static string ToHexString(this ulong value)
		{
			return string.Format("0x{0:X16}", value);
		}

		// Token: 0x0600777F RID: 30591 RVA: 0x0023A3F8 File Offset: 0x0023A3F8
		public static List<string> ToHexString(this byte[] input, ulong from, ulong length)
		{
			if (input == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			for (ulong num = from; num < from + length; num += 1UL)
			{
				list.Add(input[(int)(checked((IntPtr)num))].ToString("X2"));
			}
			return list;
		}

		// Token: 0x06007780 RID: 30592 RVA: 0x0023A444 File Offset: 0x0023A444
		public static long ToIntFromHexString(this string hexString)
		{
			return (long)new Int64Converter().ConvertFromString(hexString);
		}

		// Token: 0x06007781 RID: 30593 RVA: 0x0023A458 File Offset: 0x0023A458
		internal static ushort GetOrdinal(this byte[] buff, uint ordinal)
		{
			return BitConverter.ToUInt16(new byte[]
			{
				buff[(int)ordinal],
				buff[(int)(ordinal + 1U)]
			}, 0);
		}

		// Token: 0x06007782 RID: 30594 RVA: 0x0023A474 File Offset: 0x0023A474
		public static string GetCString(this byte[] buff, ulong stringOffset)
		{
			ulong cstringLength = buff.GetCStringLength(stringOffset);
			char[] array = new char[cstringLength];
			for (ulong num = 0UL; num < cstringLength; num += 1UL)
			{
				checked
				{
					array[(int)((IntPtr)num)] = (char)buff[(int)((IntPtr)(unchecked(stringOffset + num)))];
				}
			}
			return new string(array);
		}

		// Token: 0x06007783 RID: 30595 RVA: 0x0023A4B8 File Offset: 0x0023A4B8
		public static ulong GetCStringLength(this byte[] buff, ulong stringOffset)
		{
			ulong num = stringOffset;
			ulong num2 = 0UL;
			while (buff[(int)(checked((IntPtr)num))] != 0)
			{
				num2 += 1UL;
				num += 1UL;
			}
			return num2;
		}

		// Token: 0x06007784 RID: 30596 RVA: 0x0023A4E4 File Offset: 0x0023A4E4
		public static string GetUnicodeString(this byte[] buff, ulong stringOffset, int length)
		{
			byte[] array = new byte[length];
			Array.Copy(buff, (int)stringOffset, array, 0, length);
			return Encoding.Unicode.GetString(array);
		}

		// Token: 0x06007785 RID: 30597 RVA: 0x0023A514 File Offset: 0x0023A514
		public static uint MetaDataTableIndexSize(this Type indexEnum)
		{
			return (uint)Math.Ceiling(Math.Log((double)Enum.GetNames(indexEnum).Length, 2.0));
		}
	}
}
