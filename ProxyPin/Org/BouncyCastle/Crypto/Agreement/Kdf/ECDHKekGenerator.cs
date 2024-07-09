using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x02000339 RID: 825
	public class ECDHKekGenerator : IDerivationFunction
	{
		// Token: 0x060018B0 RID: 6320 RVA: 0x0007F120 File Offset: 0x0007F120
		public ECDHKekGenerator(IDigest digest)
		{
			this.kdf = new Kdf2BytesGenerator(digest);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0007F134 File Offset: 0x0007F134
		public virtual void Init(IDerivationParameters param)
		{
			DHKdfParameters dhkdfParameters = (DHKdfParameters)param;
			this.algorithm = dhkdfParameters.Algorithm;
			this.keySize = dhkdfParameters.KeySize;
			this.z = dhkdfParameters.GetZ();
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0007F170 File Offset: 0x0007F170
		public virtual IDigest Digest
		{
			get
			{
				return this.kdf.Digest;
			}
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0007F180 File Offset: 0x0007F180
		public virtual int GenerateBytes(byte[] outBytes, int outOff, int len)
		{
			DerSequence derSequence = new DerSequence(new Asn1Encodable[]
			{
				new AlgorithmIdentifier(this.algorithm, DerNull.Instance),
				new DerTaggedObject(true, 2, new DerOctetString(Pack.UInt32_To_BE((uint)this.keySize)))
			});
			this.kdf.Init(new KdfParameters(this.z, derSequence.GetDerEncoded()));
			return this.kdf.GenerateBytes(outBytes, outOff, len);
		}

		// Token: 0x04001059 RID: 4185
		private readonly IDerivationFunction kdf;

		// Token: 0x0400105A RID: 4186
		private DerObjectIdentifier algorithm;

		// Token: 0x0400105B RID: 4187
		private int keySize;

		// Token: 0x0400105C RID: 4188
		private byte[] z;
	}
}
