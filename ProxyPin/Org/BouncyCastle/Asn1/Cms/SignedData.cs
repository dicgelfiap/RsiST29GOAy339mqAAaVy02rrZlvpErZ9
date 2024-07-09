using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000120 RID: 288
	public class SignedData : Asn1Encodable
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x00047C64 File Offset: 0x00047C64
		public static SignedData GetInstance(object obj)
		{
			if (obj is SignedData)
			{
				return (SignedData)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new SignedData(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00047C8C File Offset: 0x00047C8C
		public SignedData(Asn1Set digestAlgorithms, ContentInfo contentInfo, Asn1Set certificates, Asn1Set crls, Asn1Set signerInfos)
		{
			this.version = this.CalculateVersion(contentInfo.ContentType, certificates, crls, signerInfos);
			this.digestAlgorithms = digestAlgorithms;
			this.contentInfo = contentInfo;
			this.certificates = certificates;
			this.crls = crls;
			this.signerInfos = signerInfos;
			this.crlsBer = (crls is BerSet);
			this.certsBer = (certificates is BerSet);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00047D00 File Offset: 0x00047D00
		private DerInteger CalculateVersion(DerObjectIdentifier contentOid, Asn1Set certs, Asn1Set crls, Asn1Set signerInfs)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (certs != null)
			{
				foreach (object obj in certs)
				{
					if (obj is Asn1TaggedObject)
					{
						Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
						if (asn1TaggedObject.TagNo == 1)
						{
							flag3 = true;
						}
						else if (asn1TaggedObject.TagNo == 2)
						{
							flag4 = true;
						}
						else if (asn1TaggedObject.TagNo == 3)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (flag)
			{
				return SignedData.Version5;
			}
			if (crls != null)
			{
				foreach (object obj2 in crls)
				{
					if (obj2 is Asn1TaggedObject)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (flag2)
			{
				return SignedData.Version5;
			}
			if (flag4)
			{
				return SignedData.Version4;
			}
			if (flag3 || !CmsObjectIdentifiers.Data.Equals(contentOid) || this.CheckForVersion3(signerInfs))
			{
				return SignedData.Version3;
			}
			return SignedData.Version1;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00047E6C File Offset: 0x00047E6C
		private bool CheckForVersion3(Asn1Set signerInfs)
		{
			foreach (object obj in signerInfs)
			{
				SignerInfo instance = SignerInfo.GetInstance(obj);
				if (instance.Version.IntValueExact == 3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00047EE4 File Offset: 0x00047EE4
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
						this.certsBer = (asn1TaggedObject is BerTaggedObject);
						this.certificates = Asn1Set.GetInstance(asn1TaggedObject, false);
						break;
					case 1:
						this.crlsBer = (asn1TaggedObject is BerTaggedObject);
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

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00047FFC File Offset: 0x00047FFC
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00048004 File Offset: 0x00048004
		public Asn1Set DigestAlgorithms
		{
			get
			{
				return this.digestAlgorithms;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0004800C File Offset: 0x0004800C
		public ContentInfo EncapContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00048014 File Offset: 0x00048014
		public Asn1Set Certificates
		{
			get
			{
				return this.certificates;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0004801C File Offset: 0x0004801C
		public Asn1Set CRLs
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00048024 File Offset: 0x00048024
		public Asn1Set SignerInfos
		{
			get
			{
				return this.signerInfos;
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0004802C File Offset: 0x0004802C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.digestAlgorithms,
				this.contentInfo
			});
			if (this.certificates != null)
			{
				if (this.certsBer)
				{
					asn1EncodableVector.Add(new BerTaggedObject(false, 0, this.certificates));
				}
				else
				{
					asn1EncodableVector.Add(new DerTaggedObject(false, 0, this.certificates));
				}
			}
			if (this.crls != null)
			{
				if (this.crlsBer)
				{
					asn1EncodableVector.Add(new BerTaggedObject(false, 1, this.crls));
				}
				else
				{
					asn1EncodableVector.Add(new DerTaggedObject(false, 1, this.crls));
				}
			}
			asn1EncodableVector.Add(this.signerInfos);
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x0400071F RID: 1823
		private static readonly DerInteger Version1 = new DerInteger(1);

		// Token: 0x04000720 RID: 1824
		private static readonly DerInteger Version3 = new DerInteger(3);

		// Token: 0x04000721 RID: 1825
		private static readonly DerInteger Version4 = new DerInteger(4);

		// Token: 0x04000722 RID: 1826
		private static readonly DerInteger Version5 = new DerInteger(5);

		// Token: 0x04000723 RID: 1827
		private readonly DerInteger version;

		// Token: 0x04000724 RID: 1828
		private readonly Asn1Set digestAlgorithms;

		// Token: 0x04000725 RID: 1829
		private readonly ContentInfo contentInfo;

		// Token: 0x04000726 RID: 1830
		private readonly Asn1Set certificates;

		// Token: 0x04000727 RID: 1831
		private readonly Asn1Set crls;

		// Token: 0x04000728 RID: 1832
		private readonly Asn1Set signerInfos;

		// Token: 0x04000729 RID: 1833
		private readonly bool certsBer;

		// Token: 0x0400072A RID: 1834
		private readonly bool crlsBer;
	}
}
