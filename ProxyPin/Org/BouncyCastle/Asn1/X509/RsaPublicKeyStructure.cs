using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200020C RID: 524
	public class RsaPublicKeyStructure : Asn1Encodable
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x00061588 File Offset: 0x00061588
		public static RsaPublicKeyStructure GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return RsaPublicKeyStructure.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00061598 File Offset: 0x00061598
		public static RsaPublicKeyStructure GetInstance(object obj)
		{
			if (obj == null || obj is RsaPublicKeyStructure)
			{
				return (RsaPublicKeyStructure)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RsaPublicKeyStructure((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid RsaPublicKeyStructure: " + Platform.GetTypeName(obj));
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x000615F0 File Offset: 0x000615F0
		public RsaPublicKeyStructure(BigInteger modulus, BigInteger publicExponent)
		{
			if (modulus == null)
			{
				throw new ArgumentNullException("modulus");
			}
			if (publicExponent == null)
			{
				throw new ArgumentNullException("publicExponent");
			}
			if (modulus.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA modulus", "modulus");
			}
			if (publicExponent.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA public exponent", "publicExponent");
			}
			this.modulus = modulus;
			this.publicExponent = publicExponent;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00061670 File Offset: 0x00061670
		private RsaPublicKeyStructure(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.modulus = DerInteger.GetInstance(seq[0]).PositiveValue;
			this.publicExponent = DerInteger.GetInstance(seq[1]).PositiveValue;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x000616DC File Offset: 0x000616DC
		public BigInteger Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x000616E4 File Offset: 0x000616E4
		public BigInteger PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000616EC File Offset: 0x000616EC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(this.Modulus),
				new DerInteger(this.PublicExponent)
			});
		}

		// Token: 0x04000C37 RID: 3127
		private BigInteger modulus;

		// Token: 0x04000C38 RID: 3128
		private BigInteger publicExponent;
	}
}
