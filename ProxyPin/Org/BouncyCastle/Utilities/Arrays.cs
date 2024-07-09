using System;
using System.Text;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x020006FB RID: 1787
	public abstract class Arrays
	{
		// Token: 0x06003E68 RID: 15976 RVA: 0x00158AC4 File Offset: 0x00158AC4
		public static bool AreAllZeroes(byte[] buf, int off, int len)
		{
			uint num = 0U;
			for (int i = 0; i < len; i++)
			{
				num |= (uint)buf[off + i];
			}
			return num == 0U;
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00158AF4 File Offset: 0x00158AF4
		public static bool AreEqual(bool[] a, bool[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x00158B14 File Offset: 0x00158B14
		public static bool AreEqual(char[] a, char[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00158B34 File Offset: 0x00158B34
		public static bool AreEqual(byte[] a, byte[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x00158B54 File Offset: 0x00158B54
		public static bool AreEqual(byte[] a, int aFromIndex, int aToIndex, byte[] b, int bFromIndex, int bToIndex)
		{
			int num = aToIndex - aFromIndex;
			int num2 = bToIndex - bFromIndex;
			if (num != num2)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (a[aFromIndex + i] != b[bFromIndex + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x00158B9C File Offset: 0x00158B9C
		[Obsolete("Use 'AreEqual' method instead")]
		public static bool AreSame(byte[] a, byte[] b)
		{
			return Arrays.AreEqual(a, b);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x00158BA8 File Offset: 0x00158BA8
		public static bool ConstantTimeAreEqual(byte[] a, byte[] b)
		{
			if (a == null || b == null)
			{
				return false;
			}
			if (a == b)
			{
				return true;
			}
			int num = Math.Min(a.Length, b.Length);
			int num2 = a.Length ^ b.Length;
			for (int i = 0; i < num; i++)
			{
				num2 |= (int)(a[i] ^ b[i]);
			}
			for (int j = num; j < b.Length; j++)
			{
				num2 |= (int)(b[j] ^ ~(int)b[j]);
			}
			return 0 == num2;
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x00158C20 File Offset: 0x00158C20
		public static bool ConstantTimeAreEqual(int len, byte[] a, int aOff, byte[] b, int bOff)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			if (len < 0)
			{
				throw new ArgumentException("cannot be negative", "len");
			}
			if (aOff > a.Length - len)
			{
				throw new IndexOutOfRangeException("'aOff' value invalid for specified length");
			}
			if (bOff > b.Length - len)
			{
				throw new IndexOutOfRangeException("'bOff' value invalid for specified length");
			}
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				num |= (int)(a[aOff + i] ^ b[bOff + i]);
			}
			return 0 == num;
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x00158CBC File Offset: 0x00158CBC
		public static bool AreEqual(int[] a, int[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00158CDC File Offset: 0x00158CDC
		[CLSCompliant(false)]
		public static bool AreEqual(uint[] a, uint[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00158CFC File Offset: 0x00158CFC
		private static bool HaveSameContents(bool[] a, bool[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x00158D34 File Offset: 0x00158D34
		private static bool HaveSameContents(char[] a, char[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x00158D6C File Offset: 0x00158D6C
		private static bool HaveSameContents(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x00158DA4 File Offset: 0x00158DA4
		private static bool HaveSameContents(int[] a, int[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x00158DDC File Offset: 0x00158DDC
		private static bool HaveSameContents(uint[] a, uint[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x00158E14 File Offset: 0x00158E14
		public static string ToString(object[] a)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			if (a.Length > 0)
			{
				stringBuilder.Append(a[0]);
				for (int i = 1; i < a.Length; i++)
				{
					stringBuilder.Append(", ").Append(a[i]);
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x00158E78 File Offset: 0x00158E78
		public static int GetHashCode(byte[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[num];
			}
			return num2;
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x00158EB8 File Offset: 0x00158EB8
		public static int GetHashCode(byte[] data, int off, int len)
		{
			if (data == null)
			{
				return 0;
			}
			int num = len;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[off + num];
			}
			return num2;
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x00158EF8 File Offset: 0x00158EF8
		public static int GetHashCode(int[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= data[num];
			}
			return num2;
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00158F38 File Offset: 0x00158F38
		public static int GetHashCode(int[] data, int off, int len)
		{
			if (data == null)
			{
				return 0;
			}
			int num = len;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= data[off + num];
			}
			return num2;
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x00158F78 File Offset: 0x00158F78
		[CLSCompliant(false)]
		public static int GetHashCode(uint[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[num];
			}
			return num2;
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x00158FB8 File Offset: 0x00158FB8
		[CLSCompliant(false)]
		public static int GetHashCode(uint[] data, int off, int len)
		{
			if (data == null)
			{
				return 0;
			}
			int num = len;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[off + num];
			}
			return num2;
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x00158FF8 File Offset: 0x00158FF8
		[CLSCompliant(false)]
		public static int GetHashCode(ulong[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				ulong num3 = data[num];
				num2 *= 257;
				num2 ^= (int)num3;
				num2 *= 257;
				num2 ^= (int)(num3 >> 32);
			}
			return num2;
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x00159048 File Offset: 0x00159048
		[CLSCompliant(false)]
		public static int GetHashCode(ulong[] data, int off, int len)
		{
			if (data == null)
			{
				return 0;
			}
			int num = len;
			int num2 = num + 1;
			while (--num >= 0)
			{
				ulong num3 = data[off + num];
				num2 *= 257;
				num2 ^= (int)num3;
				num2 *= 257;
				num2 ^= (int)(num3 >> 32);
			}
			return num2;
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x00159098 File Offset: 0x00159098
		public static bool[] Clone(bool[] data)
		{
			if (data != null)
			{
				return (bool[])data.Clone();
			}
			return null;
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x001590B0 File Offset: 0x001590B0
		public static byte[] Clone(byte[] data)
		{
			if (data != null)
			{
				return (byte[])data.Clone();
			}
			return null;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x001590C8 File Offset: 0x001590C8
		public static int[] Clone(int[] data)
		{
			if (data != null)
			{
				return (int[])data.Clone();
			}
			return null;
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x001590E0 File Offset: 0x001590E0
		[CLSCompliant(false)]
		public static uint[] Clone(uint[] data)
		{
			if (data != null)
			{
				return (uint[])data.Clone();
			}
			return null;
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x001590F8 File Offset: 0x001590F8
		public static long[] Clone(long[] data)
		{
			if (data != null)
			{
				return (long[])data.Clone();
			}
			return null;
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x00159110 File Offset: 0x00159110
		[CLSCompliant(false)]
		public static ulong[] Clone(ulong[] data)
		{
			if (data != null)
			{
				return (ulong[])data.Clone();
			}
			return null;
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00159128 File Offset: 0x00159128
		public static byte[] Clone(byte[] data, byte[] existing)
		{
			if (data == null)
			{
				return null;
			}
			if (existing == null || existing.Length != data.Length)
			{
				return Arrays.Clone(data);
			}
			Array.Copy(data, 0, existing, 0, existing.Length);
			return existing;
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00159158 File Offset: 0x00159158
		[CLSCompliant(false)]
		public static ulong[] Clone(ulong[] data, ulong[] existing)
		{
			if (data == null)
			{
				return null;
			}
			if (existing == null || existing.Length != data.Length)
			{
				return Arrays.Clone(data);
			}
			Array.Copy(data, 0, existing, 0, existing.Length);
			return existing;
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x00159188 File Offset: 0x00159188
		public static bool Contains(byte[] a, byte n)
		{
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] == n)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x001591B8 File Offset: 0x001591B8
		public static bool Contains(short[] a, short n)
		{
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] == n)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x001591E8 File Offset: 0x001591E8
		public static bool Contains(int[] a, int n)
		{
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] == n)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x00159218 File Offset: 0x00159218
		public static void Fill(byte[] buf, byte b)
		{
			int i = buf.Length;
			while (i > 0)
			{
				buf[--i] = b;
			}
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x00159240 File Offset: 0x00159240
		public static void Fill(byte[] buf, int from, int to, byte b)
		{
			for (int i = from; i < to; i++)
			{
				buf[i] = b;
			}
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x00159264 File Offset: 0x00159264
		public static byte[] CopyOf(byte[] data, int newLength)
		{
			byte[] array = new byte[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x00159290 File Offset: 0x00159290
		public static char[] CopyOf(char[] data, int newLength)
		{
			char[] array = new char[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x001592BC File Offset: 0x001592BC
		public static int[] CopyOf(int[] data, int newLength)
		{
			int[] array = new int[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x001592E8 File Offset: 0x001592E8
		public static long[] CopyOf(long[] data, int newLength)
		{
			long[] array = new long[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x00159314 File Offset: 0x00159314
		public static BigInteger[] CopyOf(BigInteger[] data, int newLength)
		{
			BigInteger[] array = new BigInteger[newLength];
			Array.Copy(data, 0, array, 0, Math.Min(newLength, data.Length));
			return array;
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x00159340 File Offset: 0x00159340
		public static byte[] CopyOfRange(byte[] data, int from, int to)
		{
			int length = Arrays.GetLength(from, to);
			byte[] array = new byte[length];
			Array.Copy(data, from, array, 0, Math.Min(length, data.Length - from));
			return array;
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x00159378 File Offset: 0x00159378
		public static int[] CopyOfRange(int[] data, int from, int to)
		{
			int length = Arrays.GetLength(from, to);
			int[] array = new int[length];
			Array.Copy(data, from, array, 0, Math.Min(length, data.Length - from));
			return array;
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x001593B0 File Offset: 0x001593B0
		public static long[] CopyOfRange(long[] data, int from, int to)
		{
			int length = Arrays.GetLength(from, to);
			long[] array = new long[length];
			Array.Copy(data, from, array, 0, Math.Min(length, data.Length - from));
			return array;
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x001593E8 File Offset: 0x001593E8
		public static BigInteger[] CopyOfRange(BigInteger[] data, int from, int to)
		{
			int length = Arrays.GetLength(from, to);
			BigInteger[] array = new BigInteger[length];
			Array.Copy(data, from, array, 0, Math.Min(length, data.Length - from));
			return array;
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x00159420 File Offset: 0x00159420
		private static int GetLength(int from, int to)
		{
			int num = to - from;
			if (num < 0)
			{
				throw new ArgumentException(from + " > " + to);
			}
			return num;
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x0015945C File Offset: 0x0015945C
		public static byte[] Append(byte[] a, byte b)
		{
			if (a == null)
			{
				return new byte[]
				{
					b
				};
			}
			int num = a.Length;
			byte[] array = new byte[num + 1];
			Array.Copy(a, 0, array, 0, num);
			array[num] = b;
			return array;
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x0015949C File Offset: 0x0015949C
		public static short[] Append(short[] a, short b)
		{
			if (a == null)
			{
				return new short[]
				{
					b
				};
			}
			int num = a.Length;
			short[] array = new short[num + 1];
			Array.Copy(a, 0, array, 0, num);
			array[num] = b;
			return array;
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x001594DC File Offset: 0x001594DC
		public static int[] Append(int[] a, int b)
		{
			if (a == null)
			{
				return new int[]
				{
					b
				};
			}
			int num = a.Length;
			int[] array = new int[num + 1];
			Array.Copy(a, 0, array, 0, num);
			array[num] = b;
			return array;
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x0015951C File Offset: 0x0015951C
		public static byte[] Concatenate(byte[] a, byte[] b)
		{
			if (a == null)
			{
				return Arrays.Clone(b);
			}
			if (b == null)
			{
				return Arrays.Clone(a);
			}
			byte[] array = new byte[a.Length + b.Length];
			Array.Copy(a, 0, array, 0, a.Length);
			Array.Copy(b, 0, array, a.Length, b.Length);
			return array;
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x00159570 File Offset: 0x00159570
		public static byte[] ConcatenateAll(params byte[][] vs)
		{
			byte[][] array = new byte[vs.Length][];
			int num = 0;
			int num2 = 0;
			foreach (byte[] array2 in vs)
			{
				if (array2 != null)
				{
					array[num++] = array2;
					num2 += array2.Length;
				}
			}
			byte[] array3 = new byte[num2];
			int num3 = 0;
			for (int j = 0; j < num; j++)
			{
				byte[] array4 = array[j];
				Array.Copy(array4, 0, array3, num3, array4.Length);
				num3 += array4.Length;
			}
			return array3;
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x00159604 File Offset: 0x00159604
		public static int[] Concatenate(int[] a, int[] b)
		{
			if (a == null)
			{
				return Arrays.Clone(b);
			}
			if (b == null)
			{
				return Arrays.Clone(a);
			}
			int[] array = new int[a.Length + b.Length];
			Array.Copy(a, 0, array, 0, a.Length);
			Array.Copy(b, 0, array, a.Length, b.Length);
			return array;
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x00159658 File Offset: 0x00159658
		public static byte[] Prepend(byte[] a, byte b)
		{
			if (a == null)
			{
				return new byte[]
				{
					b
				};
			}
			int num = a.Length;
			byte[] array = new byte[num + 1];
			Array.Copy(a, 0, array, 1, num);
			array[0] = b;
			return array;
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x00159698 File Offset: 0x00159698
		public static short[] Prepend(short[] a, short b)
		{
			if (a == null)
			{
				return new short[]
				{
					b
				};
			}
			int num = a.Length;
			short[] array = new short[num + 1];
			Array.Copy(a, 0, array, 1, num);
			array[0] = b;
			return array;
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x001596D8 File Offset: 0x001596D8
		public static int[] Prepend(int[] a, int b)
		{
			if (a == null)
			{
				return new int[]
				{
					b
				};
			}
			int num = a.Length;
			int[] array = new int[num + 1];
			Array.Copy(a, 0, array, 1, num);
			array[0] = b;
			return array;
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x00159718 File Offset: 0x00159718
		public static byte[] Reverse(byte[] a)
		{
			if (a == null)
			{
				return null;
			}
			int num = 0;
			int num2 = a.Length;
			byte[] array = new byte[num2];
			while (--num2 >= 0)
			{
				array[num2] = a[num++];
			}
			return array;
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x00159758 File Offset: 0x00159758
		public static int[] Reverse(int[] a)
		{
			if (a == null)
			{
				return null;
			}
			int num = 0;
			int num2 = a.Length;
			int[] array = new int[num2];
			while (--num2 >= 0)
			{
				array[num2] = a[num++];
			}
			return array;
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x00159798 File Offset: 0x00159798
		public static void Clear(byte[] data)
		{
			if (data != null)
			{
				Array.Clear(data, 0, data.Length);
			}
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x001597AC File Offset: 0x001597AC
		public static void Clear(int[] data)
		{
			if (data != null)
			{
				Array.Clear(data, 0, data.Length);
			}
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x001597C0 File Offset: 0x001597C0
		public static bool IsNullOrContainsNull(object[] array)
		{
			if (array == null)
			{
				return true;
			}
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (array[i] == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002078 RID: 8312
		public static readonly byte[] EmptyBytes = new byte[0];

		// Token: 0x04002079 RID: 8313
		public static readonly int[] EmptyInts = new int[0];
	}
}
