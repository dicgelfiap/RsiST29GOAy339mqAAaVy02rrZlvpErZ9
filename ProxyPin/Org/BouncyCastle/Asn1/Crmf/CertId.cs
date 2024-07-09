using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200012A RID: 298
	public class CertId : Asn1Encodable
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x00048F0C File Offset: 0x00048F0C
		private CertId(Asn1Sequence seq)
		{
			this.issuer = GeneralName.GetInstance(seq[0]);
			this.serialNumber = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00048F38 File Offset: 0x00048F38
		public static CertId GetInstance(object obj)
		{
			if (obj is CertId)
			{
				return (CertId)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertId((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00048F8C File Offset: 0x00048F8C
		public static CertId GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return CertId.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00048F9C File Offset: 0x00048F9C
		public virtual GeneralName Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00048FA4 File Offset: 0x00048FA4
		public virtual DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00048FAC File Offset: 0x00048FAC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.issuer,
				this.serialNumber
			});
		}

		// Token: 0x04000749 RID: 1865
		private readonly GeneralName issuer;

		// Token: 0x0400074A RID: 1866
		private readonly DerInteger serialNumber;
	}
}
