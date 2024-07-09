using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000260 RID: 608
	public class BerTaggedObject : DerTaggedObject
	{
		// Token: 0x06001358 RID: 4952 RVA: 0x0006A60C File Offset: 0x0006A60C
		public BerTaggedObject(int tagNo, Asn1Encodable obj) : base(tagNo, obj)
		{
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0006A618 File Offset: 0x0006A618
		public BerTaggedObject(bool explicitly, int tagNo, Asn1Encodable obj) : base(explicitly, tagNo, obj)
		{
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0006A624 File Offset: 0x0006A624
		public BerTaggedObject(int tagNo) : base(false, tagNo, BerSequence.Empty)
		{
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0006A634 File Offset: 0x0006A634
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteTag(160, this.tagNo);
				derOut.WriteByte(128);
				if (!base.IsEmpty())
				{
					if (!this.explicitly)
					{
						IEnumerable enumerable;
						if (this.obj is Asn1OctetString)
						{
							if (this.obj is BerOctetString)
							{
								enumerable = (BerOctetString)this.obj;
							}
							else
							{
								Asn1OctetString asn1OctetString = (Asn1OctetString)this.obj;
								enumerable = new BerOctetString(asn1OctetString.GetOctets());
							}
						}
						else if (this.obj is Asn1Sequence)
						{
							enumerable = (Asn1Sequence)this.obj;
						}
						else
						{
							if (!(this.obj is Asn1Set))
							{
								throw Platform.CreateNotImplementedException(Platform.GetTypeName(this.obj));
							}
							enumerable = (Asn1Set)this.obj;
						}
						using (IEnumerator enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								Asn1Encodable obj2 = (Asn1Encodable)obj;
								derOut.WriteObject(obj2);
							}
							goto IL_138;
						}
					}
					derOut.WriteObject(this.obj);
				}
				IL_138:
				derOut.WriteByte(0);
				derOut.WriteByte(0);
				return;
			}
			base.Encode(derOut);
		}
	}
}
