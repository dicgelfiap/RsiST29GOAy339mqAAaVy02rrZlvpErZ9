using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000232 RID: 562
	public class X9FieldElement : Asn1Encodable
	{
		// Token: 0x06001235 RID: 4661 RVA: 0x00066E80 File Offset: 0x00066E80
		public X9FieldElement(ECFieldElement f)
		{
			this.f = f;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00066E90 File Offset: 0x00066E90
		[Obsolete("Will be removed")]
		public X9FieldElement(BigInteger p, Asn1OctetString s) : this(new FpFieldElement(p, new BigInteger(1, s.GetOctets())))
		{
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00066EAC File Offset: 0x00066EAC
		[Obsolete("Will be removed")]
		public X9FieldElement(int m, int k1, int k2, int k3, Asn1OctetString s) : this(new F2mFieldElement(m, k1, k2, k3, new BigInteger(1, s.GetOctets())))
		{
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00066EDC File Offset: 0x00066EDC
		public ECFieldElement Value
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00066EE4 File Offset: 0x00066EE4
		public override Asn1Object ToAsn1Object()
		{
			int byteLength = X9IntegerConverter.GetByteLength(this.f);
			byte[] str = X9IntegerConverter.IntegerToBytes(this.f.ToBigInteger(), byteLength);
			return new DerOctetString(str);
		}

		// Token: 0x04000D11 RID: 3345
		private ECFieldElement f;
	}
}
