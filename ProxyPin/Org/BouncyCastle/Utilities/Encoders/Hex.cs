using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006DB RID: 1755
	public sealed class Hex
	{
		// Token: 0x06003D66 RID: 15718 RVA: 0x00150868 File Offset: 0x00150868
		private Hex()
		{
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x00150870 File Offset: 0x00150870
		public static string ToHexString(byte[] data)
		{
			return Hex.ToHexString(data, 0, data.Length);
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x0015087C File Offset: 0x0015087C
		public static string ToHexString(byte[] data, int off, int length)
		{
			byte[] bytes = Hex.Encode(data, off, length);
			return Strings.FromAsciiByteArray(bytes);
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x0015089C File Offset: 0x0015089C
		public static byte[] Encode(byte[] data)
		{
			return Hex.Encode(data, 0, data.Length);
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x001508A8 File Offset: 0x001508A8
		public static byte[] Encode(byte[] data, int off, int length)
		{
			MemoryStream memoryStream = new MemoryStream(length * 2);
			Hex.encoder.Encode(data, off, length, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x001508D8 File Offset: 0x001508D8
		public static int Encode(byte[] data, Stream outStream)
		{
			return Hex.encoder.Encode(data, 0, data.Length, outStream);
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x001508EC File Offset: 0x001508EC
		public static int Encode(byte[] data, int off, int length, Stream outStream)
		{
			return Hex.encoder.Encode(data, off, length, outStream);
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x001508FC File Offset: 0x001508FC
		public static byte[] Decode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream((data.Length + 1) / 2);
			Hex.encoder.Decode(data, 0, data.Length, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x00150934 File Offset: 0x00150934
		public static byte[] Decode(string data)
		{
			MemoryStream memoryStream = new MemoryStream((data.Length + 1) / 2);
			Hex.encoder.DecodeString(data, memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x00150968 File Offset: 0x00150968
		public static int Decode(string data, Stream outStream)
		{
			return Hex.encoder.DecodeString(data, outStream);
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x00150978 File Offset: 0x00150978
		public static byte[] DecodeStrict(string str)
		{
			return Hex.encoder.DecodeStrict(str, 0, str.Length);
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x0015098C File Offset: 0x0015098C
		public static byte[] DecodeStrict(string str, int off, int len)
		{
			return Hex.encoder.DecodeStrict(str, off, len);
		}

		// Token: 0x04001EF5 RID: 7925
		private static readonly HexEncoder encoder = new HexEncoder();
	}
}
