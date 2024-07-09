using System;
using System.Collections;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001A7 RID: 423
	public class DHParameter : Asn1Encodable
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x00055440 File Offset: 0x00055440
		public DHParameter(BigInteger p, BigInteger g, int l)
		{
			this.p = new DerInteger(p);
			this.g = new DerInteger(g);
			if (l != 0)
			{
				this.l = new DerInteger(l);
			}
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00055474 File Offset: 0x00055474
		public DHParameter(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.p = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.g = (DerInteger)enumerator.Current;
			if (enumerator.MoveNext())
			{
				this.l = (DerInteger)enumerator.Current;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x000554E0 File Offset: 0x000554E0
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x000554F0 File Offset: 0x000554F0
		public BigInteger G
		{
			get
			{
				return this.g.PositiveValue;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00055500 File Offset: 0x00055500
		public BigInteger L
		{
			get
			{
				if (this.l != null)
				{
					return this.l.PositiveValue;
				}
				return null;
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0005551C File Offset: 0x0005551C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.p,
				this.g
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.l
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009D9 RID: 2521
		internal DerInteger p;

		// Token: 0x040009DA RID: 2522
		internal DerInteger g;

		// Token: 0x040009DB RID: 2523
		internal DerInteger l;
	}
}
