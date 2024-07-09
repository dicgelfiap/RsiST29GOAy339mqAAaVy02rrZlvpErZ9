using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200013D RID: 317
	public class SinglePubInfo : Asn1Encodable
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x0004A6B0 File Offset: 0x0004A6B0
		private SinglePubInfo(Asn1Sequence seq)
		{
			this.pubMethod = DerInteger.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.pubLocation = GeneralName.GetInstance(seq[1]);
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0004A6F8 File Offset: 0x0004A6F8
		public static SinglePubInfo GetInstance(object obj)
		{
			if (obj is SinglePubInfo)
			{
				return (SinglePubInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SinglePubInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0004A74C File Offset: 0x0004A74C
		public virtual GeneralName PubLocation
		{
			get
			{
				return this.pubLocation;
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0004A754 File Offset: 0x0004A754
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pubMethod
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.pubLocation
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000797 RID: 1943
		private readonly DerInteger pubMethod;

		// Token: 0x04000798 RID: 1944
		private readonly GeneralName pubLocation;
	}
}
