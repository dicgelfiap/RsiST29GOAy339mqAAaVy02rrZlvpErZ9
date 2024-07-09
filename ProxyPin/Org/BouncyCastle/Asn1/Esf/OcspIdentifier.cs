using System;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000154 RID: 340
	public class OcspIdentifier : Asn1Encodable
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x0004CF50 File Offset: 0x0004CF50
		public static OcspIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is OcspIdentifier)
			{
				return (OcspIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OcspIdentifier' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0004CFAC File Offset: 0x0004CFAC
		private OcspIdentifier(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.ocspResponderID = ResponderID.GetInstance(seq[0].ToAsn1Object());
			this.producedAt = (DerGeneralizedTime)seq[1].ToAsn1Object();
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0004D030 File Offset: 0x0004D030
		public OcspIdentifier(ResponderID ocspResponderID, DateTime producedAt)
		{
			if (ocspResponderID == null)
			{
				throw new ArgumentNullException();
			}
			this.ocspResponderID = ocspResponderID;
			this.producedAt = new DerGeneralizedTime(producedAt);
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0004D058 File Offset: 0x0004D058
		public ResponderID OcspResponderID
		{
			get
			{
				return this.ocspResponderID;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0004D060 File Offset: 0x0004D060
		public DateTime ProducedAt
		{
			get
			{
				return this.producedAt.ToDateTime();
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0004D070 File Offset: 0x0004D070
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.ocspResponderID,
				this.producedAt
			});
		}

		// Token: 0x0400080D RID: 2061
		private readonly ResponderID ocspResponderID;

		// Token: 0x0400080E RID: 2062
		private readonly DerGeneralizedTime producedAt;
	}
}
