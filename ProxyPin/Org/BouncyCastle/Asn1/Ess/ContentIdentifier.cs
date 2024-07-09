using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000164 RID: 356
	public class ContentIdentifier : Asn1Encodable
	{
		// Token: 0x06000C27 RID: 3111 RVA: 0x0004EC1C File Offset: 0x0004EC1C
		public static ContentIdentifier GetInstance(object o)
		{
			if (o == null || o is ContentIdentifier)
			{
				return (ContentIdentifier)o;
			}
			if (o is Asn1OctetString)
			{
				return new ContentIdentifier((Asn1OctetString)o);
			}
			throw new ArgumentException("unknown object in 'ContentIdentifier' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0004EC78 File Offset: 0x0004EC78
		public ContentIdentifier(Asn1OctetString value)
		{
			this.value = value;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0004EC88 File Offset: 0x0004EC88
		public ContentIdentifier(byte[] value) : this(new DerOctetString(value))
		{
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0004EC98 File Offset: 0x0004EC98
		public Asn1OctetString Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0004ECA0 File Offset: 0x0004ECA0
		public override Asn1Object ToAsn1Object()
		{
			return this.value;
		}

		// Token: 0x0400082E RID: 2094
		private Asn1OctetString value;
	}
}
