using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200011D RID: 285
	public class RecipientInfo : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x00047748 File Offset: 0x00047748
		public RecipientInfo(KeyTransRecipientInfo info)
		{
			this.info = info;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00047758 File Offset: 0x00047758
		public RecipientInfo(KeyAgreeRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 1, info);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00047770 File Offset: 0x00047770
		public RecipientInfo(KekRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 2, info);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00047788 File Offset: 0x00047788
		public RecipientInfo(PasswordRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 3, info);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000477A0 File Offset: 0x000477A0
		public RecipientInfo(OtherRecipientInfo info)
		{
			this.info = new DerTaggedObject(false, 4, info);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000477B8 File Offset: 0x000477B8
		public RecipientInfo(Asn1Object info)
		{
			this.info = info;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000477C8 File Offset: 0x000477C8
		public static RecipientInfo GetInstance(object o)
		{
			if (o == null || o is RecipientInfo)
			{
				return (RecipientInfo)o;
			}
			if (o is Asn1Sequence)
			{
				return new RecipientInfo((Asn1Sequence)o);
			}
			if (o is Asn1TaggedObject)
			{
				return new RecipientInfo((Asn1TaggedObject)o);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(o));
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00047834 File Offset: 0x00047834
		public DerInteger Version
		{
			get
			{
				if (!(this.info is Asn1TaggedObject))
				{
					return KeyTransRecipientInfo.GetInstance(this.info).Version;
				}
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)this.info;
				switch (asn1TaggedObject.TagNo)
				{
				case 1:
					return KeyAgreeRecipientInfo.GetInstance(asn1TaggedObject, false).Version;
				case 2:
					return this.GetKekInfo(asn1TaggedObject).Version;
				case 3:
					return PasswordRecipientInfo.GetInstance(asn1TaggedObject, false).Version;
				case 4:
					return new DerInteger(0);
				default:
					throw new InvalidOperationException("unknown tag");
				}
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000478D0 File Offset: 0x000478D0
		public bool IsTagged
		{
			get
			{
				return this.info is Asn1TaggedObject;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000478E0 File Offset: 0x000478E0
		public Asn1Encodable Info
		{
			get
			{
				if (!(this.info is Asn1TaggedObject))
				{
					return KeyTransRecipientInfo.GetInstance(this.info);
				}
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)this.info;
				switch (asn1TaggedObject.TagNo)
				{
				case 1:
					return KeyAgreeRecipientInfo.GetInstance(asn1TaggedObject, false);
				case 2:
					return this.GetKekInfo(asn1TaggedObject);
				case 3:
					return PasswordRecipientInfo.GetInstance(asn1TaggedObject, false);
				case 4:
					return OtherRecipientInfo.GetInstance(asn1TaggedObject, false);
				default:
					throw new InvalidOperationException("unknown tag");
				}
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00047968 File Offset: 0x00047968
		private KekRecipientInfo GetKekInfo(Asn1TaggedObject o)
		{
			return KekRecipientInfo.GetInstance(o, o.IsExplicit());
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00047978 File Offset: 0x00047978
		public override Asn1Object ToAsn1Object()
		{
			return this.info.ToAsn1Object();
		}

		// Token: 0x04000719 RID: 1817
		internal Asn1Encodable info;
	}
}
