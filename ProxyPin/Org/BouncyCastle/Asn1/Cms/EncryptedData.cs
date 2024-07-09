using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000109 RID: 265
	public class EncryptedData : Asn1Encodable
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x00045BA4 File Offset: 0x00045BA4
		public static EncryptedData GetInstance(object obj)
		{
			if (obj is EncryptedData)
			{
				return (EncryptedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new EncryptedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid EncryptedData: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00045BE4 File Offset: 0x00045BE4
		public EncryptedData(EncryptedContentInfo encInfo) : this(encInfo, null)
		{
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00045BF0 File Offset: 0x00045BF0
		public EncryptedData(EncryptedContentInfo encInfo, Asn1Set unprotectedAttrs)
		{
			if (encInfo == null)
			{
				throw new ArgumentNullException("encInfo");
			}
			this.version = new DerInteger((unprotectedAttrs == null) ? 0 : 2);
			this.encryptedContentInfo = encInfo;
			this.unprotectedAttrs = unprotectedAttrs;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00045C30 File Offset: 0x00045C30
		private EncryptedData(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.version = DerInteger.GetInstance(seq[0]);
			this.encryptedContentInfo = EncryptedContentInfo.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.unprotectedAttrs = Asn1Set.GetInstance((Asn1TaggedObject)seq[2], false);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00045CD8 File Offset: 0x00045CD8
		public virtual DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00045CE0 File Offset: 0x00045CE0
		public virtual EncryptedContentInfo EncryptedContentInfo
		{
			get
			{
				return this.encryptedContentInfo;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00045CE8 File Offset: 0x00045CE8
		public virtual Asn1Set UnprotectedAttrs
		{
			get
			{
				return this.unprotectedAttrs;
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00045CF0 File Offset: 0x00045CF0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.encryptedContentInfo
			});
			if (this.unprotectedAttrs != null)
			{
				asn1EncodableVector.Add(new BerTaggedObject(false, 1, this.unprotectedAttrs));
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x040006E1 RID: 1761
		private readonly DerInteger version;

		// Token: 0x040006E2 RID: 1762
		private readonly EncryptedContentInfo encryptedContentInfo;

		// Token: 0x040006E3 RID: 1763
		private readonly Asn1Set unprotectedAttrs;
	}
}
