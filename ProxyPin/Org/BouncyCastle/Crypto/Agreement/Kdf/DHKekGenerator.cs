using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x02000338 RID: 824
	public class DHKekGenerator : IDerivationFunction
	{
		// Token: 0x060018AC RID: 6316 RVA: 0x0007EF24 File Offset: 0x0007EF24
		public DHKekGenerator(IDigest digest)
		{
			this.digest = digest;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0007EF34 File Offset: 0x0007EF34
		public virtual void Init(IDerivationParameters param)
		{
			DHKdfParameters dhkdfParameters = (DHKdfParameters)param;
			this.algorithm = dhkdfParameters.Algorithm;
			this.keySize = dhkdfParameters.KeySize;
			this.z = dhkdfParameters.GetZ();
			this.partyAInfo = dhkdfParameters.GetExtraInfo();
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0007EF7C File Offset: 0x0007EF7C
		public virtual IDigest Digest
		{
			get
			{
				return this.digest;
			}
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0007EF84 File Offset: 0x0007EF84
		public virtual int GenerateBytes(byte[] outBytes, int outOff, int len)
		{
			if (outBytes.Length - len < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			long num = (long)len;
			int digestSize = this.digest.GetDigestSize();
			if (num > 8589934591L)
			{
				throw new ArgumentException("Output length too large");
			}
			int num2 = (int)((num + (long)digestSize - 1L) / (long)digestSize);
			byte[] array = new byte[this.digest.GetDigestSize()];
			uint num3 = 1U;
			for (int i = 0; i < num2; i++)
			{
				this.digest.BlockUpdate(this.z, 0, this.z.Length);
				DerSequence derSequence = new DerSequence(new Asn1Encodable[]
				{
					this.algorithm,
					new DerOctetString(Pack.UInt32_To_BE(num3))
				});
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
				{
					derSequence
				});
				if (this.partyAInfo != null)
				{
					asn1EncodableVector.Add(new DerTaggedObject(true, 0, new DerOctetString(this.partyAInfo)));
				}
				asn1EncodableVector.Add(new DerTaggedObject(true, 2, new DerOctetString(Pack.UInt32_To_BE((uint)this.keySize))));
				byte[] derEncoded = new DerSequence(asn1EncodableVector).GetDerEncoded();
				this.digest.BlockUpdate(derEncoded, 0, derEncoded.Length);
				this.digest.DoFinal(array, 0);
				if (len > digestSize)
				{
					Array.Copy(array, 0, outBytes, outOff, digestSize);
					outOff += digestSize;
					len -= digestSize;
				}
				else
				{
					Array.Copy(array, 0, outBytes, outOff, len);
				}
				num3 += 1U;
			}
			this.digest.Reset();
			return (int)num;
		}

		// Token: 0x04001054 RID: 4180
		private readonly IDigest digest;

		// Token: 0x04001055 RID: 4181
		private DerObjectIdentifier algorithm;

		// Token: 0x04001056 RID: 4182
		private int keySize;

		// Token: 0x04001057 RID: 4183
		private byte[] z;

		// Token: 0x04001058 RID: 4184
		private byte[] partyAInfo;
	}
}
