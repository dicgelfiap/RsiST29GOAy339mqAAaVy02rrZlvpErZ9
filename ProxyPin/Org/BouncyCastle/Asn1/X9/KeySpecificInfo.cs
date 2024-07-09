using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200022B RID: 555
	public class KeySpecificInfo : Asn1Encodable
	{
		// Token: 0x060011F7 RID: 4599 RVA: 0x00066140 File Offset: 0x00066140
		public KeySpecificInfo(DerObjectIdentifier algorithm, Asn1OctetString counter)
		{
			this.algorithm = algorithm;
			this.counter = counter;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00066158 File Offset: 0x00066158
		public KeySpecificInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.algorithm = (DerObjectIdentifier)enumerator.Current;
			enumerator.MoveNext();
			this.counter = (Asn1OctetString)enumerator.Current;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x000661A8 File Offset: 0x000661A8
		public DerObjectIdentifier Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x000661B0 File Offset: 0x000661B0
		public Asn1OctetString Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000661B8 File Offset: 0x000661B8
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algorithm,
				this.counter
			});
		}

		// Token: 0x04000CFC RID: 3324
		private DerObjectIdentifier algorithm;

		// Token: 0x04000CFD RID: 3325
		private Asn1OctetString counter;
	}
}
