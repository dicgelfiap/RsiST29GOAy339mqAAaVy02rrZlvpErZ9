using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200011C RID: 284
	public class RecipientIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x0004763C File Offset: 0x0004763C
		public RecipientIdentifier(IssuerAndSerialNumber id)
		{
			this.id = id;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0004764C File Offset: 0x0004764C
		public RecipientIdentifier(Asn1OctetString id)
		{
			this.id = new DerTaggedObject(false, 0, id);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00047664 File Offset: 0x00047664
		public RecipientIdentifier(Asn1Object id)
		{
			this.id = id;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00047674 File Offset: 0x00047674
		public static RecipientIdentifier GetInstance(object o)
		{
			if (o == null || o is RecipientIdentifier)
			{
				return (RecipientIdentifier)o;
			}
			if (o is IssuerAndSerialNumber)
			{
				return new RecipientIdentifier((IssuerAndSerialNumber)o);
			}
			if (o is Asn1OctetString)
			{
				return new RecipientIdentifier((Asn1OctetString)o);
			}
			if (o is Asn1Object)
			{
				return new RecipientIdentifier((Asn1Object)o);
			}
			throw new ArgumentException("Illegal object in RecipientIdentifier: " + Platform.GetTypeName(o));
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x000476F8 File Offset: 0x000476F8
		public bool IsTagged
		{
			get
			{
				return this.id is Asn1TaggedObject;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00047708 File Offset: 0x00047708
		public Asn1Encodable ID
		{
			get
			{
				if (this.id is Asn1TaggedObject)
				{
					return Asn1OctetString.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return IssuerAndSerialNumber.GetInstance(this.id);
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00047738 File Offset: 0x00047738
		public override Asn1Object ToAsn1Object()
		{
			return this.id.ToAsn1Object();
		}

		// Token: 0x04000718 RID: 1816
		private Asn1Encodable id;
	}
}
