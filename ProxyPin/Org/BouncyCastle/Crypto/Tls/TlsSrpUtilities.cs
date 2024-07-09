using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200054F RID: 1359
	public abstract class TlsSrpUtilities
	{
		// Token: 0x060029B8 RID: 10680 RVA: 0x000DFFE4 File Offset: 0x000DFFE4
		public static void AddSrpExtension(IDictionary extensions, byte[] identity)
		{
			extensions[12] = TlsSrpUtilities.CreateSrpExtension(identity);
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000DFFFC File Offset: 0x000DFFFC
		public static byte[] GetSrpExtension(IDictionary extensions)
		{
			byte[] extensionData = TlsUtilities.GetExtensionData(extensions, 12);
			if (extensionData != null)
			{
				return TlsSrpUtilities.ReadSrpExtension(extensionData);
			}
			return null;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000E0024 File Offset: 0x000E0024
		public static byte[] CreateSrpExtension(byte[] identity)
		{
			if (identity == null)
			{
				throw new TlsFatalAlert(80);
			}
			return TlsUtilities.EncodeOpaque8(identity);
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000E003C File Offset: 0x000E003C
		public static byte[] ReadSrpExtension(byte[] extensionData)
		{
			if (extensionData == null)
			{
				throw new ArgumentNullException("extensionData");
			}
			MemoryStream memoryStream = new MemoryStream(extensionData, false);
			byte[] result = TlsUtilities.ReadOpaque8(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			return result;
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000E0074 File Offset: 0x000E0074
		public static BigInteger ReadSrpParameter(Stream input)
		{
			return new BigInteger(1, TlsUtilities.ReadOpaque16(input));
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000E0084 File Offset: 0x000E0084
		public static void WriteSrpParameter(BigInteger x, Stream output)
		{
			TlsUtilities.WriteOpaque16(BigIntegers.AsUnsignedByteArray(x), output);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000E0094 File Offset: 0x000E0094
		public static bool IsSrpCipherSuite(int cipherSuite)
		{
			switch (cipherSuite)
			{
			case 49178:
			case 49179:
			case 49180:
			case 49181:
			case 49182:
			case 49183:
			case 49184:
			case 49185:
			case 49186:
				return true;
			default:
				return false;
			}
		}
	}
}
