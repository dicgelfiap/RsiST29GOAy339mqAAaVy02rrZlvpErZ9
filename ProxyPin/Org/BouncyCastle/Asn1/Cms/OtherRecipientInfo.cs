using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000118 RID: 280
	public class OtherRecipientInfo : Asn1Encodable
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x000471DC File Offset: 0x000471DC
		public OtherRecipientInfo(DerObjectIdentifier oriType, Asn1Encodable oriValue)
		{
			this.oriType = oriType;
			this.oriValue = oriValue;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000471F4 File Offset: 0x000471F4
		[Obsolete("Use GetInstance() instead")]
		public OtherRecipientInfo(Asn1Sequence seq)
		{
			this.oriType = DerObjectIdentifier.GetInstance(seq[0]);
			this.oriValue = seq[1];
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0004721C File Offset: 0x0004721C
		public static OtherRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OtherRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0004722C File Offset: 0x0004722C
		public static OtherRecipientInfo GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			OtherRecipientInfo otherRecipientInfo = obj as OtherRecipientInfo;
			if (otherRecipientInfo != null)
			{
				return otherRecipientInfo;
			}
			return new OtherRecipientInfo(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00047260 File Offset: 0x00047260
		public virtual DerObjectIdentifier OriType
		{
			get
			{
				return this.oriType;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00047268 File Offset: 0x00047268
		public virtual Asn1Encodable OriValue
		{
			get
			{
				return this.oriValue;
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00047270 File Offset: 0x00047270
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.oriType,
				this.oriValue
			});
		}

		// Token: 0x0400070E RID: 1806
		private readonly DerObjectIdentifier oriType;

		// Token: 0x0400070F RID: 1807
		private readonly Asn1Encodable oriValue;
	}
}
