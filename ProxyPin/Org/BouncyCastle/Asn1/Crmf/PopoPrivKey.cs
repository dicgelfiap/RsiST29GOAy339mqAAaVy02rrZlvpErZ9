using System;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000139 RID: 313
	public class PopoPrivKey : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x0004A164 File Offset: 0x0004A164
		private PopoPrivKey(Asn1TaggedObject obj)
		{
			this.tagNo = obj.TagNo;
			switch (this.tagNo)
			{
			case 0:
				this.obj = DerBitString.GetInstance(obj, false);
				return;
			case 1:
				this.obj = SubsequentMessage.ValueOf(DerInteger.GetInstance(obj, false).IntValueExact);
				return;
			case 2:
				this.obj = DerBitString.GetInstance(obj, false);
				return;
			case 3:
				this.obj = PKMacValue.GetInstance(obj, false);
				return;
			case 4:
				this.obj = EnvelopedData.GetInstance(obj, false);
				return;
			default:
				throw new ArgumentException("unknown tag in PopoPrivKey", "obj");
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0004A20C File Offset: 0x0004A20C
		public static PopoPrivKey GetInstance(Asn1TaggedObject tagged, bool isExplicit)
		{
			return new PopoPrivKey(Asn1TaggedObject.GetInstance(tagged.GetObject()));
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0004A220 File Offset: 0x0004A220
		public PopoPrivKey(SubsequentMessage msg)
		{
			this.tagNo = 1;
			this.obj = msg;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0004A238 File Offset: 0x0004A238
		public virtual int Type
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0004A240 File Offset: 0x0004A240
		public virtual Asn1Encodable Value
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0004A248 File Offset: 0x0004A248
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.tagNo, this.obj);
		}

		// Token: 0x04000784 RID: 1924
		public const int thisMessage = 0;

		// Token: 0x04000785 RID: 1925
		public const int subsequentMessage = 1;

		// Token: 0x04000786 RID: 1926
		public const int dhMAC = 2;

		// Token: 0x04000787 RID: 1927
		public const int agreeMAC = 3;

		// Token: 0x04000788 RID: 1928
		public const int encryptedKey = 4;

		// Token: 0x04000789 RID: 1929
		private readonly int tagNo;

		// Token: 0x0400078A RID: 1930
		private readonly Asn1Encodable obj;
	}
}
