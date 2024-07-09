using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001BB RID: 443
	public class SignedData : Asn1Encodable
	{
		// Token: 0x06000E6D RID: 3693 RVA: 0x00057998 File Offset: 0x00057998
		public static SignedData GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			SignedData signedData = obj as SignedData;
			if (signedData != null)
			{
				return signedData;
			}
			return new SignedData(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x000579CC File Offset: 0x000579CC
		public SignedData(DerInteger _version, Asn1Set _digestAlgorithms, ContentInfo _contentInfo, Asn1Set _certificates, Asn1Set _crls, Asn1Set _signerInfos)
		{
			this.version = _version;
			this.digestAlgorithms = _digestAlgorithms;
			this.contentInfo = _contentInfo;
			this.certificates = _certificates;
			this.crls = _crls;
			this.signerInfos = _signerInfos;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00057A04 File Offset: 0x00057A04
		private SignedData(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.digestAlgorithms = (Asn1Set)enumerator.Current;
			enumerator.MoveNext();
			this.contentInfo = ContentInfo.GetInstance(enumerator.Current);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1Object asn1Object = (Asn1Object)obj;
				if (asn1Object is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Object;
					switch (asn1TaggedObject.TagNo)
					{
					case 0:
						this.certificates = Asn1Set.GetInstance(asn1TaggedObject, false);
						break;
					case 1:
						this.crls = Asn1Set.GetInstance(asn1TaggedObject, false);
						break;
					default:
						throw new ArgumentException("unknown tag value " + asn1TaggedObject.TagNo);
					}
				}
				else
				{
					this.signerInfos = (Asn1Set)asn1Object;
				}
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00057B00 File Offset: 0x00057B00
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00057B08 File Offset: 0x00057B08
		public Asn1Set DigestAlgorithms
		{
			get
			{
				return this.digestAlgorithms;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00057B10 File Offset: 0x00057B10
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00057B18 File Offset: 0x00057B18
		public Asn1Set Certificates
		{
			get
			{
				return this.certificates;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00057B20 File Offset: 0x00057B20
		public Asn1Set Crls
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00057B28 File Offset: 0x00057B28
		public Asn1Set SignerInfos
		{
			get
			{
				return this.signerInfos;
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00057B30 File Offset: 0x00057B30
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.digestAlgorithms,
				this.contentInfo
			});
			asn1EncodableVector.AddOptionalTagged(false, 0, this.certificates);
			asn1EncodableVector.AddOptionalTagged(false, 1, this.crls);
			asn1EncodableVector.Add(this.signerInfos);
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x04000AA5 RID: 2725
		private readonly DerInteger version;

		// Token: 0x04000AA6 RID: 2726
		private readonly Asn1Set digestAlgorithms;

		// Token: 0x04000AA7 RID: 2727
		private readonly ContentInfo contentInfo;

		// Token: 0x04000AA8 RID: 2728
		private readonly Asn1Set certificates;

		// Token: 0x04000AA9 RID: 2729
		private readonly Asn1Set crls;

		// Token: 0x04000AAA RID: 2730
		private readonly Asn1Set signerInfos;
	}
}
