using System;
using System.IO;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000685 RID: 1669
	public class Pkcs8EncryptedPrivateKeyInfoBuilder
	{
		// Token: 0x06003A4C RID: 14924 RVA: 0x0013A240 File Offset: 0x0013A240
		public Pkcs8EncryptedPrivateKeyInfoBuilder(byte[] privateKeyInfo) : this(PrivateKeyInfo.GetInstance(privateKeyInfo))
		{
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x0013A250 File Offset: 0x0013A250
		public Pkcs8EncryptedPrivateKeyInfoBuilder(PrivateKeyInfo privateKeyInfo)
		{
			this.privateKeyInfo = privateKeyInfo;
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x0013A260 File Offset: 0x0013A260
		public Pkcs8EncryptedPrivateKeyInfo Build(ICipherBuilder encryptor)
		{
			Pkcs8EncryptedPrivateKeyInfo result;
			try
			{
				MemoryStream memoryStream = new MemoryOutputStream();
				ICipher cipher = encryptor.BuildCipher(memoryStream);
				byte[] encoded = this.privateKeyInfo.GetEncoded();
				Stream stream = cipher.Stream;
				stream.Write(encoded, 0, encoded.Length);
				Platform.Dispose(stream);
				result = new Pkcs8EncryptedPrivateKeyInfo(new EncryptedPrivateKeyInfo((AlgorithmIdentifier)encryptor.AlgorithmDetails, memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new InvalidOperationException("cannot encode privateKeyInfo");
			}
			return result;
		}

		// Token: 0x04001E46 RID: 7750
		private PrivateKeyInfo privateKeyInfo;
	}
}
