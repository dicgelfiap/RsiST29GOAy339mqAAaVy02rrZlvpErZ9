using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000D5 RID: 213
	public class Challenge : Asn1Encodable
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00041414 File Offset: 0x00041414
		private Challenge(Asn1Sequence seq)
		{
			int index = 0;
			if (seq.Count == 3)
			{
				this.owf = AlgorithmIdentifier.GetInstance(seq[index++]);
			}
			this.witness = Asn1OctetString.GetInstance(seq[index++]);
			this.challenge = Asn1OctetString.GetInstance(seq[index]);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00041478 File Offset: 0x00041478
		public static Challenge GetInstance(object obj)
		{
			if (obj is Challenge)
			{
				return (Challenge)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Challenge((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x000414CC File Offset: 0x000414CC
		public virtual AlgorithmIdentifier Owf
		{
			get
			{
				return this.owf;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000414D4 File Offset: 0x000414D4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.owf
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.witness,
				this.challenge
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040005EA RID: 1514
		private readonly AlgorithmIdentifier owf;

		// Token: 0x040005EB RID: 1515
		private readonly Asn1OctetString witness;

		// Token: 0x040005EC RID: 1516
		private readonly Asn1OctetString challenge;
	}
}
