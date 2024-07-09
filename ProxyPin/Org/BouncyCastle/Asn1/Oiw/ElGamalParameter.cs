using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Oiw
{
	// Token: 0x0200019F RID: 415
	public class ElGamalParameter : Asn1Encodable
	{
		// Token: 0x06000DA5 RID: 3493 RVA: 0x00054B30 File Offset: 0x00054B30
		public ElGamalParameter(BigInteger p, BigInteger g)
		{
			this.p = new DerInteger(p);
			this.g = new DerInteger(g);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00054B50 File Offset: 0x00054B50
		public ElGamalParameter(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.p = DerInteger.GetInstance(seq[0]);
			this.g = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00054BA8 File Offset: 0x00054BA8
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00054BB8 File Offset: 0x00054BB8
		public BigInteger G
		{
			get
			{
				return this.g.PositiveValue;
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00054BC8 File Offset: 0x00054BC8
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.p,
				this.g
			});
		}

		// Token: 0x040009BC RID: 2492
		internal DerInteger p;

		// Token: 0x040009BD RID: 2493
		internal DerInteger g;
	}
}
