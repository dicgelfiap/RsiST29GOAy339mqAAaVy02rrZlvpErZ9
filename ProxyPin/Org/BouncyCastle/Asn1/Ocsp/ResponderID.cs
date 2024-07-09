using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000197 RID: 407
	public class ResponderID : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000D5D RID: 3421 RVA: 0x00053E1C File Offset: 0x00053E1C
		public static ResponderID GetInstance(object obj)
		{
			if (obj == null || obj is ResponderID)
			{
				return (ResponderID)obj;
			}
			if (obj is DerOctetString)
			{
				return new ResponderID((DerOctetString)obj);
			}
			if (!(obj is Asn1TaggedObject))
			{
				return new ResponderID(X509Name.GetInstance(obj));
			}
			Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
			if (asn1TaggedObject.TagNo == 1)
			{
				return new ResponderID(X509Name.GetInstance(asn1TaggedObject, true));
			}
			return new ResponderID(Asn1OctetString.GetInstance(asn1TaggedObject, true));
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00053EA0 File Offset: 0x00053EA0
		public ResponderID(Asn1OctetString id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00053EC0 File Offset: 0x00053EC0
		public ResponderID(X509Name id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00053EE0 File Offset: 0x00053EE0
		public static ResponderID GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return ResponderID.GetInstance(obj.GetObject());
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00053EF0 File Offset: 0x00053EF0
		public virtual byte[] GetKeyHash()
		{
			if (this.id is Asn1OctetString)
			{
				return ((Asn1OctetString)this.id).GetOctets();
			}
			return null;
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00053F14 File Offset: 0x00053F14
		public virtual X509Name Name
		{
			get
			{
				if (this.id is Asn1OctetString)
				{
					return null;
				}
				return X509Name.GetInstance(this.id);
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00053F34 File Offset: 0x00053F34
		public override Asn1Object ToAsn1Object()
		{
			if (this.id is Asn1OctetString)
			{
				return new DerTaggedObject(true, 2, this.id);
			}
			return new DerTaggedObject(true, 1, this.id);
		}

		// Token: 0x040009A0 RID: 2464
		private readonly Asn1Encodable id;
	}
}
