using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x020006DF RID: 1759
	public class UrlBase64
	{
		// Token: 0x06003D84 RID: 15748 RVA: 0x00150E88 File Offset: 0x00150E88
		public static byte[] Encode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				UrlBase64.encoder.Encode(data, 0, data.Length, memoryStream);
			}
			catch (IOException ex)
			{
				throw new Exception("exception encoding URL safe base64 string: " + ex.Message, ex);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x00150EE0 File Offset: 0x00150EE0
		public static int Encode(byte[] data, Stream outStr)
		{
			return UrlBase64.encoder.Encode(data, 0, data.Length, outStr);
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x00150EF4 File Offset: 0x00150EF4
		public static byte[] Decode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				UrlBase64.encoder.Decode(data, 0, data.Length, memoryStream);
			}
			catch (IOException ex)
			{
				throw new Exception("exception decoding URL safe base64 string: " + ex.Message, ex);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x00150F4C File Offset: 0x00150F4C
		public static int Decode(byte[] data, Stream outStr)
		{
			return UrlBase64.encoder.Decode(data, 0, data.Length, outStr);
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x00150F60 File Offset: 0x00150F60
		public static byte[] Decode(string data)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				UrlBase64.encoder.DecodeString(data, memoryStream);
			}
			catch (IOException ex)
			{
				throw new Exception("exception decoding URL safe base64 string: " + ex.Message, ex);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x00150FB4 File Offset: 0x00150FB4
		public static int Decode(string data, Stream outStr)
		{
			return UrlBase64.encoder.DecodeString(data, outStr);
		}

		// Token: 0x04001EF9 RID: 7929
		private static readonly IEncoder encoder = new UrlBase64Encoder();
	}
}
