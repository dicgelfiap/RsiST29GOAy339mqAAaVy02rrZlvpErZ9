using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200010D RID: 269
	public class IssuerAndSerialNumber : Asn1Encodable
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x0004629C File Offset: 0x0004629C
		public static IssuerAndSerialNumber GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			IssuerAndSerialNumber issuerAndSerialNumber = obj as IssuerAndSerialNumber;
			if (issuerAndSerialNumber != null)
			{
				return issuerAndSerialNumber;
			}
			return new IssuerAndSerialNumber(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000462D0 File Offset: 0x000462D0
		[Obsolete("Use GetInstance() instead")]
		public IssuerAndSerialNumber(Asn1Sequence seq)
		{
			this.name = X509Name.GetInstance(seq[0]);
			this.serialNumber = (DerInteger)seq[1];
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000462FC File Offset: 0x000462FC
		public IssuerAndSerialNumber(X509Name name, BigInteger serialNumber)
		{
			this.name = name;
			this.serialNumber = new DerInteger(serialNumber);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00046318 File Offset: 0x00046318
		public IssuerAndSerialNumber(X509Name name, DerInteger serialNumber)
		{
			this.name = name;
			this.serialNumber = serialNumber;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00046330 File Offset: 0x00046330
		public X509Name Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00046338 File Offset: 0x00046338
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00046340 File Offset: 0x00046340
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.name,
				this.serialNumber
			});
		}

		// Token: 0x040006EF RID: 1775
		private X509Name name;

		// Token: 0x040006F0 RID: 1776
		private DerInteger serialNumber;
	}
}
