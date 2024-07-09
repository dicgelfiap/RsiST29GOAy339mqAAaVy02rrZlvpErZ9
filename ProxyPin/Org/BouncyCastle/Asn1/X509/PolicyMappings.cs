using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000206 RID: 518
	public class PolicyMappings : Asn1Encodable
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x00060EFC File Offset: 0x00060EFC
		public PolicyMappings(Asn1Sequence seq)
		{
			this.seq = seq;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00060F0C File Offset: 0x00060F0C
		public PolicyMappings(Hashtable mappings) : this(mappings)
		{
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00060F18 File Offset: 0x00060F18
		public PolicyMappings(IDictionary mappings)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			foreach (object obj in mappings.Keys)
			{
				string text = (string)obj;
				string identifier = (string)mappings[text];
				asn1EncodableVector.Add(new DerSequence(new Asn1Encodable[]
				{
					new DerObjectIdentifier(text),
					new DerObjectIdentifier(identifier)
				}));
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00060FCC File Offset: 0x00060FCC
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04000C24 RID: 3108
		private readonly Asn1Sequence seq;
	}
}
