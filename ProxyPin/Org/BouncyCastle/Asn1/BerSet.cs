using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200025C RID: 604
	public class BerSet : DerSet
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x0006A3F4 File Offset: 0x0006A3F4
		public new static BerSet FromVector(Asn1EncodableVector elementVector)
		{
			if (elementVector.Count >= 1)
			{
				return new BerSet(elementVector);
			}
			return BerSet.Empty;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0006A410 File Offset: 0x0006A410
		internal new static BerSet FromVector(Asn1EncodableVector elementVector, bool needsSorting)
		{
			if (elementVector.Count >= 1)
			{
				return new BerSet(elementVector, needsSorting);
			}
			return BerSet.Empty;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0006A42C File Offset: 0x0006A42C
		public BerSet()
		{
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0006A434 File Offset: 0x0006A434
		public BerSet(Asn1Encodable element) : base(element)
		{
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0006A440 File Offset: 0x0006A440
		public BerSet(Asn1EncodableVector elementVector) : base(elementVector, false)
		{
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0006A44C File Offset: 0x0006A44C
		internal BerSet(Asn1EncodableVector elementVector, bool needsSorting) : base(elementVector, needsSorting)
		{
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0006A458 File Offset: 0x0006A458
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(49);
				derOut.WriteByte(128);
				foreach (object obj in this)
				{
					Asn1Encodable obj2 = (Asn1Encodable)obj;
					derOut.WriteObject(obj2);
				}
				derOut.WriteByte(0);
				derOut.WriteByte(0);
				return;
			}
			base.Encode(derOut);
		}

		// Token: 0x04000D98 RID: 3480
		public new static readonly BerSet Empty = new BerSet();
	}
}
