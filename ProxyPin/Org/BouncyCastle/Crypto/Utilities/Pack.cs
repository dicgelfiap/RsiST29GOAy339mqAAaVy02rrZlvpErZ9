using System;

namespace Org.BouncyCastle.Crypto.Utilities
{
	// Token: 0x0200055A RID: 1370
	internal sealed class Pack
	{
		// Token: 0x06002A72 RID: 10866 RVA: 0x000E3FBC File Offset: 0x000E3FBC
		private Pack()
		{
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000E3FC4 File Offset: 0x000E3FC4
		internal static void UInt16_To_BE(ushort n, byte[] bs)
		{
			bs[0] = (byte)(n >> 8);
			bs[1] = (byte)n;
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000E3FD4 File Offset: 0x000E3FD4
		internal static void UInt16_To_BE(ushort n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 8);
			bs[off + 1] = (byte)n;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000E3FE4 File Offset: 0x000E3FE4
		internal static ushort BE_To_UInt16(byte[] bs)
		{
			uint num = (uint)((int)bs[0] << 8 | (int)bs[1]);
			return (ushort)num;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000E4004 File Offset: 0x000E4004
		internal static ushort BE_To_UInt16(byte[] bs, int off)
		{
			uint num = (uint)((int)bs[off] << 8 | (int)bs[off + 1]);
			return (ushort)num;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000E4024 File Offset: 0x000E4024
		internal static byte[] UInt32_To_BE(uint n)
		{
			byte[] array = new byte[4];
			Pack.UInt32_To_BE(n, array, 0);
			return array;
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000E4048 File Offset: 0x000E4048
		internal static void UInt32_To_BE(uint n, byte[] bs)
		{
			bs[0] = (byte)(n >> 24);
			bs[1] = (byte)(n >> 16);
			bs[2] = (byte)(n >> 8);
			bs[3] = (byte)n;
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000E4068 File Offset: 0x000E4068
		internal static void UInt32_To_BE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 24);
			bs[off + 1] = (byte)(n >> 16);
			bs[off + 2] = (byte)(n >> 8);
			bs[off + 3] = (byte)n;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000E408C File Offset: 0x000E408C
		internal static byte[] UInt32_To_BE(uint[] ns)
		{
			byte[] array = new byte[4 * ns.Length];
			Pack.UInt32_To_BE(ns, array, 0);
			return array;
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000E40B4 File Offset: 0x000E40B4
		internal static void UInt32_To_BE(uint[] ns, byte[] bs, int off)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				Pack.UInt32_To_BE(ns[i], bs, off);
				off += 4;
			}
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000E40E8 File Offset: 0x000E40E8
		internal static uint BE_To_UInt32(byte[] bs)
		{
			return (uint)((int)bs[0] << 24 | (int)bs[1] << 16 | (int)bs[2] << 8 | (int)bs[3]);
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000E4104 File Offset: 0x000E4104
		internal static uint BE_To_UInt32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] << 24 | (int)bs[off + 1] << 16 | (int)bs[off + 2] << 8 | (int)bs[off + 3]);
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000E4124 File Offset: 0x000E4124
		internal static void BE_To_UInt32(byte[] bs, int off, uint[] ns)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = Pack.BE_To_UInt32(bs, off);
				off += 4;
			}
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000E4158 File Offset: 0x000E4158
		internal static byte[] UInt64_To_BE(ulong n)
		{
			byte[] array = new byte[8];
			Pack.UInt64_To_BE(n, array, 0);
			return array;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000E417C File Offset: 0x000E417C
		internal static void UInt64_To_BE(ulong n, byte[] bs)
		{
			Pack.UInt32_To_BE((uint)(n >> 32), bs);
			Pack.UInt32_To_BE((uint)n, bs, 4);
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000E4194 File Offset: 0x000E4194
		internal static void UInt64_To_BE(ulong n, byte[] bs, int off)
		{
			Pack.UInt32_To_BE((uint)(n >> 32), bs, off);
			Pack.UInt32_To_BE((uint)n, bs, off + 4);
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000E41B0 File Offset: 0x000E41B0
		internal static byte[] UInt64_To_BE(ulong[] ns)
		{
			byte[] array = new byte[8 * ns.Length];
			Pack.UInt64_To_BE(ns, array, 0);
			return array;
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000E41D8 File Offset: 0x000E41D8
		internal static void UInt64_To_BE(ulong[] ns, byte[] bs, int off)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				Pack.UInt64_To_BE(ns[i], bs, off);
				off += 8;
			}
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000E420C File Offset: 0x000E420C
		internal static ulong BE_To_UInt64(byte[] bs)
		{
			uint num = Pack.BE_To_UInt32(bs);
			uint num2 = Pack.BE_To_UInt32(bs, 4);
			return (ulong)num << 32 | (ulong)num2;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000E4234 File Offset: 0x000E4234
		internal static ulong BE_To_UInt64(byte[] bs, int off)
		{
			uint num = Pack.BE_To_UInt32(bs, off);
			uint num2 = Pack.BE_To_UInt32(bs, off + 4);
			return (ulong)num << 32 | (ulong)num2;
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000E4260 File Offset: 0x000E4260
		internal static void BE_To_UInt64(byte[] bs, int off, ulong[] ns)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = Pack.BE_To_UInt64(bs, off);
				off += 8;
			}
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000E4294 File Offset: 0x000E4294
		internal static void UInt16_To_LE(ushort n, byte[] bs)
		{
			bs[0] = (byte)n;
			bs[1] = (byte)(n >> 8);
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000E42A4 File Offset: 0x000E42A4
		internal static void UInt16_To_LE(ushort n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[off + 1] = (byte)(n >> 8);
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000E42B4 File Offset: 0x000E42B4
		internal static ushort LE_To_UInt16(byte[] bs)
		{
			uint num = (uint)((int)bs[0] | (int)bs[1] << 8);
			return (ushort)num;
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000E42D4 File Offset: 0x000E42D4
		internal static ushort LE_To_UInt16(byte[] bs, int off)
		{
			uint num = (uint)((int)bs[off] | (int)bs[off + 1] << 8);
			return (ushort)num;
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000E42F4 File Offset: 0x000E42F4
		internal static byte[] UInt32_To_LE(uint n)
		{
			byte[] array = new byte[4];
			Pack.UInt32_To_LE(n, array, 0);
			return array;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000E4318 File Offset: 0x000E4318
		internal static void UInt32_To_LE(uint n, byte[] bs)
		{
			bs[0] = (byte)n;
			bs[1] = (byte)(n >> 8);
			bs[2] = (byte)(n >> 16);
			bs[3] = (byte)(n >> 24);
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000E4338 File Offset: 0x000E4338
		internal static void UInt32_To_LE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[off + 1] = (byte)(n >> 8);
			bs[off + 2] = (byte)(n >> 16);
			bs[off + 3] = (byte)(n >> 24);
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000E435C File Offset: 0x000E435C
		internal static byte[] UInt32_To_LE(uint[] ns)
		{
			byte[] array = new byte[4 * ns.Length];
			Pack.UInt32_To_LE(ns, array, 0);
			return array;
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000E4384 File Offset: 0x000E4384
		internal static void UInt32_To_LE(uint[] ns, byte[] bs, int off)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				Pack.UInt32_To_LE(ns[i], bs, off);
				off += 4;
			}
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000E43B8 File Offset: 0x000E43B8
		internal static uint LE_To_UInt32(byte[] bs)
		{
			return (uint)((int)bs[0] | (int)bs[1] << 8 | (int)bs[2] << 16 | (int)bs[3] << 24);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000E43D4 File Offset: 0x000E43D4
		internal static uint LE_To_UInt32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] | (int)bs[off + 1] << 8 | (int)bs[off + 2] << 16 | (int)bs[off + 3] << 24);
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000E43F4 File Offset: 0x000E43F4
		internal static void LE_To_UInt32(byte[] bs, int off, uint[] ns)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = Pack.LE_To_UInt32(bs, off);
				off += 4;
			}
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000E4428 File Offset: 0x000E4428
		internal static void LE_To_UInt32(byte[] bs, int bOff, uint[] ns, int nOff, int count)
		{
			for (int i = 0; i < count; i++)
			{
				ns[nOff + i] = Pack.LE_To_UInt32(bs, bOff);
				bOff += 4;
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000E445C File Offset: 0x000E445C
		internal static uint[] LE_To_UInt32(byte[] bs, int off, int count)
		{
			uint[] array = new uint[count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Pack.LE_To_UInt32(bs, off);
				off += 4;
			}
			return array;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000E4498 File Offset: 0x000E4498
		internal static byte[] UInt64_To_LE(ulong n)
		{
			byte[] array = new byte[8];
			Pack.UInt64_To_LE(n, array, 0);
			return array;
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000E44BC File Offset: 0x000E44BC
		internal static void UInt64_To_LE(ulong n, byte[] bs)
		{
			Pack.UInt32_To_LE((uint)n, bs);
			Pack.UInt32_To_LE((uint)(n >> 32), bs, 4);
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000E44D4 File Offset: 0x000E44D4
		internal static void UInt64_To_LE(ulong n, byte[] bs, int off)
		{
			Pack.UInt32_To_LE((uint)n, bs, off);
			Pack.UInt32_To_LE((uint)(n >> 32), bs, off + 4);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000E44F0 File Offset: 0x000E44F0
		internal static byte[] UInt64_To_LE(ulong[] ns)
		{
			byte[] array = new byte[8 * ns.Length];
			Pack.UInt64_To_LE(ns, array, 0);
			return array;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000E4518 File Offset: 0x000E4518
		internal static void UInt64_To_LE(ulong[] ns, byte[] bs, int off)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				Pack.UInt64_To_LE(ns[i], bs, off);
				off += 8;
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000E454C File Offset: 0x000E454C
		internal static void UInt64_To_LE(ulong[] ns, int nsOff, int nsLen, byte[] bs, int bsOff)
		{
			for (int i = 0; i < nsLen; i++)
			{
				Pack.UInt64_To_LE(ns[nsOff + i], bs, bsOff);
				bsOff += 8;
			}
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000E4580 File Offset: 0x000E4580
		internal static ulong LE_To_UInt64(byte[] bs)
		{
			uint num = Pack.LE_To_UInt32(bs);
			uint num2 = Pack.LE_To_UInt32(bs, 4);
			return (ulong)num2 << 32 | (ulong)num;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000E45A8 File Offset: 0x000E45A8
		internal static ulong LE_To_UInt64(byte[] bs, int off)
		{
			uint num = Pack.LE_To_UInt32(bs, off);
			uint num2 = Pack.LE_To_UInt32(bs, off + 4);
			return (ulong)num2 << 32 | (ulong)num;
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000E45D4 File Offset: 0x000E45D4
		internal static void LE_To_UInt64(byte[] bs, int off, ulong[] ns)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = Pack.LE_To_UInt64(bs, off);
				off += 8;
			}
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000E4608 File Offset: 0x000E4608
		internal static void LE_To_UInt64(byte[] bs, int bsOff, ulong[] ns, int nsOff, int nsLen)
		{
			for (int i = 0; i < nsLen; i++)
			{
				ns[nsOff + i] = Pack.LE_To_UInt64(bs, bsOff);
				bsOff += 8;
			}
		}
	}
}
