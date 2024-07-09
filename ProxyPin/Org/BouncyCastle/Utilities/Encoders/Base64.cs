using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006D6 RID: 1750
	public sealed class Base64
	{
		// Token: 0x06003D4A RID: 15690 RVA: 0x0014FE1C File Offset: 0x0014FE1C
		private Base64()
		{
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x0014FE24 File Offset: 0x0014FE24
		public static string ToBase64String(byte[] data)
		{
			return Convert.ToBase64String(data, 0, data.Length);
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x0014FE30 File Offset: 0x0014FE30
		public static string ToBase64String(byte[] data, int off, int length)
		{
			return Convert.ToBase64String(data, off, length);
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x0014FE3C File Offset: 0x0014FE3C
		public static byte[] Encode(byte[] data)
		{
			return Base64.Encode(data, 0, data.Length);
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x0014FE48 File Offset: 0x0014FE48
		public static byte[] Encode(byte[] data, int off, int length)
		{
			string s = Convert.ToBase64String(data, off, length);
			return Strings.ToAsciiByteArray(s);
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x0014FE68 File Offset: 0x0014FE68
		public static int Encode(byte[] data, Stream outStream)
		{
			byte[] array = Base64.Encode(data);
			outStream.Write(array, 0, array.Length);
			return array.Length;
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x0014FE90 File Offset: 0x0014FE90
		public static int Encode(byte[] data, int off, int length, Stream outStream)
		{
			byte[] array = Base64.Encode(data, off, length);
			outStream.Write(array, 0, array.Length);
			return array.Length;
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x0014FEB8 File Offset: 0x0014FEB8
		public static byte[] Decode(byte[] data)
		{
			string s = Strings.FromAsciiByteArray(data);
			return Convert.FromBase64String(s);
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x0014FED8 File Offset: 0x0014FED8
		public static byte[] Decode(string data)
		{
			return Convert.FromBase64String(data);
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x0014FEE0 File Offset: 0x0014FEE0
		public static int Decode(string data, Stream outStream)
		{
			byte[] array = Base64.Decode(data);
			outStream.Write(array, 0, array.Length);
			return array.Length;
		}
	}
}
