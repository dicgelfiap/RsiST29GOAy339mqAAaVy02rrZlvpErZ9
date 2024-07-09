using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B8 RID: 440
	public class RsaPrivateKeyStructure : Asn1Encodable
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x00057314 File Offset: 0x00057314
		public static RsaPrivateKeyStructure GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return RsaPrivateKeyStructure.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00057324 File Offset: 0x00057324
		public static RsaPrivateKeyStructure GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is RsaPrivateKeyStructure)
			{
				return (RsaPrivateKeyStructure)obj;
			}
			return new RsaPrivateKeyStructure(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0005734C File Offset: 0x0005734C
		public RsaPrivateKeyStructure(BigInteger modulus, BigInteger publicExponent, BigInteger privateExponent, BigInteger prime1, BigInteger prime2, BigInteger exponent1, BigInteger exponent2, BigInteger coefficient)
		{
			this.modulus = modulus;
			this.publicExponent = publicExponent;
			this.privateExponent = privateExponent;
			this.prime1 = prime1;
			this.prime2 = prime2;
			this.exponent1 = exponent1;
			this.exponent2 = exponent2;
			this.coefficient = coefficient;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000573A0 File Offset: 0x000573A0
		[Obsolete("Use 'GetInstance' method(s) instead")]
		public RsaPrivateKeyStructure(Asn1Sequence seq)
		{
			BigInteger value = ((DerInteger)seq[0]).Value;
			if (value.IntValue != 0)
			{
				throw new ArgumentException("wrong version for RSA private key");
			}
			this.modulus = ((DerInteger)seq[1]).Value;
			this.publicExponent = ((DerInteger)seq[2]).Value;
			this.privateExponent = ((DerInteger)seq[3]).Value;
			this.prime1 = ((DerInteger)seq[4]).Value;
			this.prime2 = ((DerInteger)seq[5]).Value;
			this.exponent1 = ((DerInteger)seq[6]).Value;
			this.exponent2 = ((DerInteger)seq[7]).Value;
			this.coefficient = ((DerInteger)seq[8]).Value;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00057498 File Offset: 0x00057498
		public BigInteger Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x000574A0 File Offset: 0x000574A0
		public BigInteger PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x000574A8 File Offset: 0x000574A8
		public BigInteger PrivateExponent
		{
			get
			{
				return this.privateExponent;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000574B0 File Offset: 0x000574B0
		public BigInteger Prime1
		{
			get
			{
				return this.prime1;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000574B8 File Offset: 0x000574B8
		public BigInteger Prime2
		{
			get
			{
				return this.prime2;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x000574C0 File Offset: 0x000574C0
		public BigInteger Exponent1
		{
			get
			{
				return this.exponent1;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x000574C8 File Offset: 0x000574C8
		public BigInteger Exponent2
		{
			get
			{
				return this.exponent2;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x000574D0 File Offset: 0x000574D0
		public BigInteger Coefficient
		{
			get
			{
				return this.coefficient;
			}
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x000574D8 File Offset: 0x000574D8
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(0),
				new DerInteger(this.Modulus),
				new DerInteger(this.PublicExponent),
				new DerInteger(this.PrivateExponent),
				new DerInteger(this.Prime1),
				new DerInteger(this.Prime2),
				new DerInteger(this.Exponent1),
				new DerInteger(this.Exponent2),
				new DerInteger(this.Coefficient)
			});
		}

		// Token: 0x04000A92 RID: 2706
		private readonly BigInteger modulus;

		// Token: 0x04000A93 RID: 2707
		private readonly BigInteger publicExponent;

		// Token: 0x04000A94 RID: 2708
		private readonly BigInteger privateExponent;

		// Token: 0x04000A95 RID: 2709
		private readonly BigInteger prime1;

		// Token: 0x04000A96 RID: 2710
		private readonly BigInteger prime2;

		// Token: 0x04000A97 RID: 2711
		private readonly BigInteger exponent1;

		// Token: 0x04000A98 RID: 2712
		private readonly BigInteger exponent2;

		// Token: 0x04000A99 RID: 2713
		private readonly BigInteger coefficient;
	}
}
