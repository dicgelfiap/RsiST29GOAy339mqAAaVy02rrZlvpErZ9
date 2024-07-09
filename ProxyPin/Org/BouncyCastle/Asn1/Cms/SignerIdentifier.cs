using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000122 RID: 290
	public class SignerIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x0004834C File Offset: 0x0004834C
		public SignerIdentifier(IssuerAndSerialNumber id)
		{
			this.id = id;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0004835C File Offset: 0x0004835C
		public SignerIdentifier(Asn1OctetString id)
		{
			this.id = new DerTaggedObject(false, 0, id);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00048374 File Offset: 0x00048374
		public SignerIdentifier(Asn1Object id)
		{
			this.id = id;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00048384 File Offset: 0x00048384
		public static SignerIdentifier GetInstance(object o)
		{
			if (o == null || o is SignerIdentifier)
			{
				return (SignerIdentifier)o;
			}
			if (o is IssuerAndSerialNumber)
			{
				return new SignerIdentifier((IssuerAndSerialNumber)o);
			}
			if (o is Asn1OctetString)
			{
				return new SignerIdentifier((Asn1OctetString)o);
			}
			if (o is Asn1Object)
			{
				return new SignerIdentifier((Asn1Object)o);
			}
			throw new ArgumentException("Illegal object in SignerIdentifier: " + Platform.GetTypeName(o));
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00048408 File Offset: 0x00048408
		public bool IsTagged
		{
			get
			{
				return this.id is Asn1TaggedObject;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00048418 File Offset: 0x00048418
		public Asn1Encodable ID
		{
			get
			{
				if (this.id is Asn1TaggedObject)
				{
					return Asn1OctetString.GetInstance((Asn1TaggedObject)this.id, false);
				}
				return this.id;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00048444 File Offset: 0x00048444
		public override Asn1Object ToAsn1Object()
		{
			return this.id.ToAsn1Object();
		}

		// Token: 0x04000730 RID: 1840
		private Asn1Encodable id;
	}
}
