using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000163 RID: 355
	public class ContentHints : Asn1Encodable
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x0004EADC File Offset: 0x0004EADC
		public static ContentHints GetInstance(object o)
		{
			if (o == null || o is ContentHints)
			{
				return (ContentHints)o;
			}
			if (o is Asn1Sequence)
			{
				return new ContentHints((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'ContentHints' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0004EB38 File Offset: 0x0004EB38
		private ContentHints(Asn1Sequence seq)
		{
			IAsn1Convertible asn1Convertible = seq[0];
			if (asn1Convertible.ToAsn1Object() is DerUtf8String)
			{
				this.contentDescription = DerUtf8String.GetInstance(asn1Convertible);
				this.contentType = DerObjectIdentifier.GetInstance(seq[1]);
				return;
			}
			this.contentType = DerObjectIdentifier.GetInstance(seq[0]);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0004EB98 File Offset: 0x0004EB98
		public ContentHints(DerObjectIdentifier contentType)
		{
			this.contentType = contentType;
			this.contentDescription = null;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0004EBB0 File Offset: 0x0004EBB0
		public ContentHints(DerObjectIdentifier contentType, DerUtf8String contentDescription)
		{
			this.contentType = contentType;
			this.contentDescription = contentDescription;
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0004EBC8 File Offset: 0x0004EBC8
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0004EBD0 File Offset: 0x0004EBD0
		public DerUtf8String ContentDescription
		{
			get
			{
				return this.contentDescription;
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0004EBD8 File Offset: 0x0004EBD8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.contentDescription
			});
			asn1EncodableVector.Add(this.contentType);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400082C RID: 2092
		private readonly DerUtf8String contentDescription;

		// Token: 0x0400082D RID: 2093
		private readonly DerObjectIdentifier contentType;
	}
}
