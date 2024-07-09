using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000258 RID: 600
	public class BerSequence : DerSequence
	{
		// Token: 0x06001332 RID: 4914 RVA: 0x0006A194 File Offset: 0x0006A194
		public new static BerSequence FromVector(Asn1EncodableVector elementVector)
		{
			if (elementVector.Count >= 1)
			{
				return new BerSequence(elementVector);
			}
			return BerSequence.Empty;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0006A1B0 File Offset: 0x0006A1B0
		public BerSequence()
		{
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0006A1B8 File Offset: 0x0006A1B8
		public BerSequence(Asn1Encodable element) : base(element)
		{
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0006A1C4 File Offset: 0x0006A1C4
		public BerSequence(params Asn1Encodable[] elements) : base(elements)
		{
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0006A1D0 File Offset: 0x0006A1D0
		public BerSequence(Asn1EncodableVector elementVector) : base(elementVector)
		{
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0006A1DC File Offset: 0x0006A1DC
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(48);
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

		// Token: 0x04000D95 RID: 3477
		public new static readonly BerSequence Empty = new BerSequence();
	}
}
