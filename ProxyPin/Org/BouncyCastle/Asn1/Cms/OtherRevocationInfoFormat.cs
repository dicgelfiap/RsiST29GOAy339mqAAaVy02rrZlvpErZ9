using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000119 RID: 281
	public class OtherRevocationInfoFormat : Asn1Encodable
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x000472A8 File Offset: 0x000472A8
		public OtherRevocationInfoFormat(DerObjectIdentifier otherRevInfoFormat, Asn1Encodable otherRevInfo)
		{
			this.otherRevInfoFormat = otherRevInfoFormat;
			this.otherRevInfo = otherRevInfo;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000472C0 File Offset: 0x000472C0
		private OtherRevocationInfoFormat(Asn1Sequence seq)
		{
			this.otherRevInfoFormat = DerObjectIdentifier.GetInstance(seq[0]);
			this.otherRevInfo = seq[1];
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000472E8 File Offset: 0x000472E8
		public static OtherRevocationInfoFormat GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return OtherRevocationInfoFormat.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000472F8 File Offset: 0x000472F8
		public static OtherRevocationInfoFormat GetInstance(object obj)
		{
			if (obj is OtherRevocationInfoFormat)
			{
				return (OtherRevocationInfoFormat)obj;
			}
			if (obj != null)
			{
				return new OtherRevocationInfoFormat(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00047320 File Offset: 0x00047320
		public virtual DerObjectIdentifier InfoFormat
		{
			get
			{
				return this.otherRevInfoFormat;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00047328 File Offset: 0x00047328
		public virtual Asn1Encodable Info
		{
			get
			{
				return this.otherRevInfo;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00047330 File Offset: 0x00047330
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.otherRevInfoFormat,
				this.otherRevInfo
			});
		}

		// Token: 0x04000710 RID: 1808
		private readonly DerObjectIdentifier otherRevInfoFormat;

		// Token: 0x04000711 RID: 1809
		private readonly Asn1Encodable otherRevInfo;
	}
}
