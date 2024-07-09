using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001A3 RID: 419
	public class CertBag : Asn1Encodable
	{
		// Token: 0x06000DB8 RID: 3512 RVA: 0x00054ECC File Offset: 0x00054ECC
		public static CertBag GetInstance(object obj)
		{
			if (obj is CertBag)
			{
				return (CertBag)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new CertBag(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00054EF4 File Offset: 0x00054EF4
		[Obsolete("Use 'GetInstance' instead")]
		public CertBag(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.certID = DerObjectIdentifier.GetInstance(seq[0]);
			this.certValue = Asn1TaggedObject.GetInstance(seq[1]).GetObject();
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00054F50 File Offset: 0x00054F50
		public CertBag(DerObjectIdentifier certID, Asn1Object certValue)
		{
			this.certID = certID;
			this.certValue = certValue;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00054F68 File Offset: 0x00054F68
		public virtual DerObjectIdentifier CertID
		{
			get
			{
				return this.certID;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00054F70 File Offset: 0x00054F70
		public virtual Asn1Object CertValue
		{
			get
			{
				return this.certValue;
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00054F78 File Offset: 0x00054F78
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.certID,
				new DerTaggedObject(0, this.certValue)
			});
		}

		// Token: 0x040009CE RID: 2510
		private readonly DerObjectIdentifier certID;

		// Token: 0x040009CF RID: 2511
		private readonly Asn1Object certValue;
	}
}
