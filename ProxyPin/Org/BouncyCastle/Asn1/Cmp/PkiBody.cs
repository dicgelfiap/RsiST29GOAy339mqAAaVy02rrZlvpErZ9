using System;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000E0 RID: 224
	public class PkiBody : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x00042194 File Offset: 0x00042194
		public static PkiBody GetInstance(object obj)
		{
			if (obj is PkiBody)
			{
				return (PkiBody)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new PkiBody((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000421E8 File Offset: 0x000421E8
		private PkiBody(Asn1TaggedObject tagged)
		{
			this.tagNo = tagged.TagNo;
			this.body = PkiBody.GetBodyForType(this.tagNo, tagged.GetObject());
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00042224 File Offset: 0x00042224
		public PkiBody(int type, Asn1Encodable content)
		{
			this.tagNo = type;
			this.body = PkiBody.GetBodyForType(type, content);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00042240 File Offset: 0x00042240
		private static Asn1Encodable GetBodyForType(int type, Asn1Encodable o)
		{
			switch (type)
			{
			case 0:
				return CertReqMessages.GetInstance(o);
			case 1:
				return CertRepMessage.GetInstance(o);
			case 2:
				return CertReqMessages.GetInstance(o);
			case 3:
				return CertRepMessage.GetInstance(o);
			case 4:
				return CertificationRequest.GetInstance(o);
			case 5:
				return PopoDecKeyChallContent.GetInstance(o);
			case 6:
				return PopoDecKeyRespContent.GetInstance(o);
			case 7:
				return CertReqMessages.GetInstance(o);
			case 8:
				return CertRepMessage.GetInstance(o);
			case 9:
				return CertReqMessages.GetInstance(o);
			case 10:
				return KeyRecRepContent.GetInstance(o);
			case 11:
				return RevReqContent.GetInstance(o);
			case 12:
				return RevRepContent.GetInstance(o);
			case 13:
				return CertReqMessages.GetInstance(o);
			case 14:
				return CertRepMessage.GetInstance(o);
			case 15:
				return CAKeyUpdAnnContent.GetInstance(o);
			case 16:
				return CmpCertificate.GetInstance(o);
			case 17:
				return RevAnnContent.GetInstance(o);
			case 18:
				return CrlAnnContent.GetInstance(o);
			case 19:
				return PkiConfirmContent.GetInstance(o);
			case 20:
				return PkiMessages.GetInstance(o);
			case 21:
				return GenMsgContent.GetInstance(o);
			case 22:
				return GenRepContent.GetInstance(o);
			case 23:
				return ErrorMsgContent.GetInstance(o);
			case 24:
				return CertConfirmContent.GetInstance(o);
			case 25:
				return PollReqContent.GetInstance(o);
			case 26:
				return PollRepContent.GetInstance(o);
			default:
				throw new ArgumentException("unknown tag number: " + type, "type");
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000423A4 File Offset: 0x000423A4
		public virtual int Type
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000423AC File Offset: 0x000423AC
		public virtual Asn1Encodable Content
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000423B4 File Offset: 0x000423B4
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(true, this.tagNo, this.body);
		}

		// Token: 0x0400061C RID: 1564
		public const int TYPE_INIT_REQ = 0;

		// Token: 0x0400061D RID: 1565
		public const int TYPE_INIT_REP = 1;

		// Token: 0x0400061E RID: 1566
		public const int TYPE_CERT_REQ = 2;

		// Token: 0x0400061F RID: 1567
		public const int TYPE_CERT_REP = 3;

		// Token: 0x04000620 RID: 1568
		public const int TYPE_P10_CERT_REQ = 4;

		// Token: 0x04000621 RID: 1569
		public const int TYPE_POPO_CHALL = 5;

		// Token: 0x04000622 RID: 1570
		public const int TYPE_POPO_REP = 6;

		// Token: 0x04000623 RID: 1571
		public const int TYPE_KEY_UPDATE_REQ = 7;

		// Token: 0x04000624 RID: 1572
		public const int TYPE_KEY_UPDATE_REP = 8;

		// Token: 0x04000625 RID: 1573
		public const int TYPE_KEY_RECOVERY_REQ = 9;

		// Token: 0x04000626 RID: 1574
		public const int TYPE_KEY_RECOVERY_REP = 10;

		// Token: 0x04000627 RID: 1575
		public const int TYPE_REVOCATION_REQ = 11;

		// Token: 0x04000628 RID: 1576
		public const int TYPE_REVOCATION_REP = 12;

		// Token: 0x04000629 RID: 1577
		public const int TYPE_CROSS_CERT_REQ = 13;

		// Token: 0x0400062A RID: 1578
		public const int TYPE_CROSS_CERT_REP = 14;

		// Token: 0x0400062B RID: 1579
		public const int TYPE_CA_KEY_UPDATE_ANN = 15;

		// Token: 0x0400062C RID: 1580
		public const int TYPE_CERT_ANN = 16;

		// Token: 0x0400062D RID: 1581
		public const int TYPE_REVOCATION_ANN = 17;

		// Token: 0x0400062E RID: 1582
		public const int TYPE_CRL_ANN = 18;

		// Token: 0x0400062F RID: 1583
		public const int TYPE_CONFIRM = 19;

		// Token: 0x04000630 RID: 1584
		public const int TYPE_NESTED = 20;

		// Token: 0x04000631 RID: 1585
		public const int TYPE_GEN_MSG = 21;

		// Token: 0x04000632 RID: 1586
		public const int TYPE_GEN_REP = 22;

		// Token: 0x04000633 RID: 1587
		public const int TYPE_ERROR = 23;

		// Token: 0x04000634 RID: 1588
		public const int TYPE_CERT_CONFIRM = 24;

		// Token: 0x04000635 RID: 1589
		public const int TYPE_POLL_REQ = 25;

		// Token: 0x04000636 RID: 1590
		public const int TYPE_POLL_REP = 26;

		// Token: 0x04000637 RID: 1591
		private int tagNo;

		// Token: 0x04000638 RID: 1592
		private Asn1Encodable body;
	}
}
