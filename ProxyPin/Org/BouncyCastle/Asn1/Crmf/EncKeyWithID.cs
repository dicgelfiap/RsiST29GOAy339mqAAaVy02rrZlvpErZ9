using System;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000132 RID: 306
	public class EncKeyWithID : Asn1Encodable
	{
		// Token: 0x06000AD1 RID: 2769 RVA: 0x00049870 File Offset: 0x00049870
		public static EncKeyWithID GetInstance(object obj)
		{
			if (obj is EncKeyWithID)
			{
				return (EncKeyWithID)obj;
			}
			if (obj != null)
			{
				return new EncKeyWithID(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00049898 File Offset: 0x00049898
		private EncKeyWithID(Asn1Sequence seq)
		{
			this.privKeyInfo = PrivateKeyInfo.GetInstance(seq[0]);
			if (seq.Count <= 1)
			{
				this.identifier = null;
				return;
			}
			if (!(seq[1] is DerUtf8String))
			{
				this.identifier = GeneralName.GetInstance(seq[1]);
				return;
			}
			this.identifier = seq[1];
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00049908 File Offset: 0x00049908
		public EncKeyWithID(PrivateKeyInfo privKeyInfo)
		{
			this.privKeyInfo = privKeyInfo;
			this.identifier = null;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00049920 File Offset: 0x00049920
		public EncKeyWithID(PrivateKeyInfo privKeyInfo, DerUtf8String str)
		{
			this.privKeyInfo = privKeyInfo;
			this.identifier = str;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00049938 File Offset: 0x00049938
		public EncKeyWithID(PrivateKeyInfo privKeyInfo, GeneralName generalName)
		{
			this.privKeyInfo = privKeyInfo;
			this.identifier = generalName;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00049950 File Offset: 0x00049950
		public virtual PrivateKeyInfo PrivateKey
		{
			get
			{
				return this.privKeyInfo;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00049958 File Offset: 0x00049958
		public virtual bool HasIdentifier
		{
			get
			{
				return this.identifier != null;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00049968 File Offset: 0x00049968
		public virtual bool IsIdentifierUtf8String
		{
			get
			{
				return this.identifier is DerUtf8String;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00049978 File Offset: 0x00049978
		public virtual Asn1Encodable Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00049980 File Offset: 0x00049980
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.privKeyInfo
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.identifier
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000770 RID: 1904
		private readonly PrivateKeyInfo privKeyInfo;

		// Token: 0x04000771 RID: 1905
		private readonly Asn1Encodable identifier;
	}
}
