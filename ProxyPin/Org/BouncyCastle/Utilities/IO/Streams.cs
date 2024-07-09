using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020006EA RID: 1770
	public sealed class Streams
	{
		// Token: 0x06003DAB RID: 15787 RVA: 0x00151220 File Offset: 0x00151220
		private Streams()
		{
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x00151228 File Offset: 0x00151228
		public static void Drain(Stream inStr)
		{
			byte[] array = new byte[512];
			while (inStr.Read(array, 0, array.Length) > 0)
			{
			}
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x00151254 File Offset: 0x00151254
		public static byte[] ReadAll(Stream inStr)
		{
			MemoryStream memoryStream = new MemoryStream();
			Streams.PipeAll(inStr, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x00151278 File Offset: 0x00151278
		public static byte[] ReadAllLimited(Stream inStr, int limit)
		{
			MemoryStream memoryStream = new MemoryStream();
			Streams.PipeAllLimited(inStr, (long)limit, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x001512A0 File Offset: 0x001512A0
		public static int ReadFully(Stream inStr, byte[] buf)
		{
			return Streams.ReadFully(inStr, buf, 0, buf.Length);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x001512B0 File Offset: 0x001512B0
		public static int ReadFully(Stream inStr, byte[] buf, int off, int len)
		{
			int i;
			int num;
			for (i = 0; i < len; i += num)
			{
				num = inStr.Read(buf, off + i, len - i);
				if (num < 1)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x001512E8 File Offset: 0x001512E8
		public static void PipeAll(Stream inStr, Stream outStr)
		{
			byte[] array = new byte[512];
			int count;
			while ((count = inStr.Read(array, 0, array.Length)) > 0)
			{
				outStr.Write(array, 0, count);
			}
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x00151324 File Offset: 0x00151324
		public static long PipeAllLimited(Stream inStr, long limit, Stream outStr)
		{
			byte[] array = new byte[512];
			long num = 0L;
			int num2;
			while ((num2 = inStr.Read(array, 0, array.Length)) > 0)
			{
				if (limit - num < (long)num2)
				{
					throw new StreamOverflowException("Data Overflow");
				}
				num += (long)num2;
				outStr.Write(array, 0, num2);
			}
			return num;
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x0015137C File Offset: 0x0015137C
		public static void WriteBufTo(MemoryStream buf, Stream output)
		{
			buf.WriteTo(output);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x00151388 File Offset: 0x00151388
		public static int WriteBufTo(MemoryStream buf, byte[] output, int offset)
		{
			int num = (int)buf.Length;
			buf.WriteTo(new MemoryStream(output, offset, num, true));
			return num;
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x001513B4 File Offset: 0x001513B4
		public static void WriteZeroes(Stream outStr, long count)
		{
			byte[] buffer = new byte[512];
			while (count > 512L)
			{
				outStr.Write(buffer, 0, 512);
				count -= 512L;
			}
			outStr.Write(buffer, 0, (int)count);
		}

		// Token: 0x04001F00 RID: 7936
		private const int BufferSize = 512;
	}
}
