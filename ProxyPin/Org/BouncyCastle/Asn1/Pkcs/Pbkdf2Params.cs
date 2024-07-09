using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B1 RID: 433
	public class Pbkdf2Params : Asn1Encodable
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x00055F88 File Offset: 0x00055F88
		public static Pbkdf2Params GetInstance(object obj)
		{
			if (obj == null || obj is Pbkdf2Params)
			{
				return (Pbkdf2Params)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Pbkdf2Params((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00055FE4 File Offset: 0x00055FE4
		public Pbkdf2Params(Asn1Sequence seq)
		{
			if (seq.Count < 2 || seq.Count > 4)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.octStr = (Asn1OctetString)seq[0];
			this.iterationCount = (DerInteger)seq[1];
			Asn1Encodable asn1Encodable = null;
			Asn1Encodable asn1Encodable2 = null;
			if (seq.Count > 3)
			{
				asn1Encodable = seq[2];
				asn1Encodable2 = seq[3];
			}
			else if (seq.Count > 2)
			{
				if (seq[2] is DerInteger)
				{
					asn1Encodable = seq[2];
				}
				else
				{
					asn1Encodable2 = seq[2];
				}
			}
			if (asn1Encodable != null)
			{
				this.keyLength = (DerInteger)asn1Encodable;
			}
			if (asn1Encodable2 != null)
			{
				this.prf = AlgorithmIdentifier.GetInstance(asn1Encodable2);
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x000560C4 File Offset: 0x000560C4
		public Pbkdf2Params(byte[] salt, int iterationCount)
		{
			this.octStr = new DerOctetString(salt);
			this.iterationCount = new DerInteger(iterationCount);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x000560E4 File Offset: 0x000560E4
		public Pbkdf2Params(byte[] salt, int iterationCount, int keyLength) : this(salt, iterationCount)
		{
			this.keyLength = new DerInteger(keyLength);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000560FC File Offset: 0x000560FC
		public Pbkdf2Params(byte[] salt, int iterationCount, int keyLength, AlgorithmIdentifier prf) : this(salt, iterationCount, keyLength)
		{
			this.prf = prf;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00056110 File Offset: 0x00056110
		public Pbkdf2Params(byte[] salt, int iterationCount, AlgorithmIdentifier prf) : this(salt, iterationCount)
		{
			this.prf = prf;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00056124 File Offset: 0x00056124
		public byte[] GetSalt()
		{
			return this.octStr.GetOctets();
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00056134 File Offset: 0x00056134
		public BigInteger IterationCount
		{
			get
			{
				return this.iterationCount.Value;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00056144 File Offset: 0x00056144
		public BigInteger KeyLength
		{
			get
			{
				if (this.keyLength != null)
				{
					return this.keyLength.Value;
				}
				return null;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00056160 File Offset: 0x00056160
		public bool IsDefaultPrf
		{
			get
			{
				return this.prf == null || this.prf.Equals(Pbkdf2Params.algid_hmacWithSHA1);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00056180 File Offset: 0x00056180
		public AlgorithmIdentifier Prf
		{
			get
			{
				if (this.prf == null)
				{
					return Pbkdf2Params.algid_hmacWithSHA1;
				}
				return this.prf;
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0005619C File Offset: 0x0005619C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.octStr,
				this.iterationCount
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.keyLength
			});
			if (!this.IsDefaultPrf)
			{
				asn1EncodableVector.Add(this.prf);
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009EA RID: 2538
		private static AlgorithmIdentifier algid_hmacWithSHA1 = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdHmacWithSha1, DerNull.Instance);

		// Token: 0x040009EB RID: 2539
		private readonly Asn1OctetString octStr;

		// Token: 0x040009EC RID: 2540
		private readonly DerInteger iterationCount;

		// Token: 0x040009ED RID: 2541
		private readonly DerInteger keyLength;

		// Token: 0x040009EE RID: 2542
		private readonly AlgorithmIdentifier prf;
	}
}
