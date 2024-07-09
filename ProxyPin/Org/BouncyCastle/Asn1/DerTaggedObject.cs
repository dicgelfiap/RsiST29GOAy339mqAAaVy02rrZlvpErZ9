using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200025F RID: 607
	public class DerTaggedObject : Asn1TaggedObject
	{
		// Token: 0x06001354 RID: 4948 RVA: 0x0006A55C File Offset: 0x0006A55C
		public DerTaggedObject(int tagNo, Asn1Encodable obj) : base(tagNo, obj)
		{
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0006A568 File Offset: 0x0006A568
		public DerTaggedObject(bool explicitly, int tagNo, Asn1Encodable obj) : base(explicitly, tagNo, obj)
		{
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0006A574 File Offset: 0x0006A574
		public DerTaggedObject(int tagNo) : base(false, tagNo, DerSequence.Empty)
		{
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0006A584 File Offset: 0x0006A584
		internal override void Encode(DerOutputStream derOut)
		{
			if (base.IsEmpty())
			{
				derOut.WriteEncoded(160, this.tagNo, new byte[0]);
				return;
			}
			byte[] derEncoded = this.obj.GetDerEncoded();
			if (this.explicitly)
			{
				derOut.WriteEncoded(160, this.tagNo, derEncoded);
				return;
			}
			int flags = (int)((derEncoded[0] & 32) | 128);
			derOut.WriteTag(flags, this.tagNo);
			derOut.Write(derEncoded, 1, derEncoded.Length - 1);
		}
	}
}
