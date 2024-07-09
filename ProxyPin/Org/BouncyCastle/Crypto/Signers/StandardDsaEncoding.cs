using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x020004B1 RID: 1201
	public class StandardDsaEncoding : IDsaEncoding
	{
		// Token: 0x06002509 RID: 9481 RVA: 0x000CE4DC File Offset: 0x000CE4DC
		public virtual BigInteger[] Decode(BigInteger n, byte[] encoding)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)Asn1Object.FromByteArray(encoding);
			if (asn1Sequence.Count == 2)
			{
				BigInteger bigInteger = this.DecodeValue(n, asn1Sequence, 0);
				BigInteger bigInteger2 = this.DecodeValue(n, asn1Sequence, 1);
				byte[] a = this.Encode(n, bigInteger, bigInteger2);
				if (Arrays.AreEqual(a, encoding))
				{
					return new BigInteger[]
					{
						bigInteger,
						bigInteger2
					};
				}
			}
			throw new ArgumentException("Malformed signature", "encoding");
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000CE55C File Offset: 0x000CE55C
		public virtual byte[] Encode(BigInteger n, BigInteger r, BigInteger s)
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.EncodeValue(n, r),
				this.EncodeValue(n, s)
			}).GetEncoded("DER");
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000CE5A4 File Offset: 0x000CE5A4
		protected virtual BigInteger CheckValue(BigInteger n, BigInteger x)
		{
			if (x.SignValue < 0 || (n != null && x.CompareTo(n) >= 0))
			{
				throw new ArgumentException("Value out of range", "x");
			}
			return x;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000CE5D8 File Offset: 0x000CE5D8
		protected virtual BigInteger DecodeValue(BigInteger n, Asn1Sequence s, int pos)
		{
			return this.CheckValue(n, ((DerInteger)s[pos]).Value);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000CE5F4 File Offset: 0x000CE5F4
		protected virtual DerInteger EncodeValue(BigInteger n, BigInteger x)
		{
			return new DerInteger(this.CheckValue(n, x));
		}

		// Token: 0x04001770 RID: 6000
		public static readonly StandardDsaEncoding Instance = new StandardDsaEncoding();
	}
}
