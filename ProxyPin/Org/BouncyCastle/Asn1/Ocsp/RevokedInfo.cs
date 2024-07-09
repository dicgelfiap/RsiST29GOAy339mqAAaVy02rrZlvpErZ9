using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200019A RID: 410
	public class RevokedInfo : Asn1Encodable
	{
		// Token: 0x06000D77 RID: 3447 RVA: 0x00054300 File Offset: 0x00054300
		public static RevokedInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return RevokedInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00054310 File Offset: 0x00054310
		public static RevokedInfo GetInstance(object obj)
		{
			if (obj == null || obj is RevokedInfo)
			{
				return (RevokedInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RevokedInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0005436C File Offset: 0x0005436C
		public RevokedInfo(DerGeneralizedTime revocationTime) : this(revocationTime, null)
		{
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00054378 File Offset: 0x00054378
		public RevokedInfo(DerGeneralizedTime revocationTime, CrlReason revocationReason)
		{
			if (revocationTime == null)
			{
				throw new ArgumentNullException("revocationTime");
			}
			this.revocationTime = revocationTime;
			this.revocationReason = revocationReason;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000543A0 File Offset: 0x000543A0
		private RevokedInfo(Asn1Sequence seq)
		{
			this.revocationTime = (DerGeneralizedTime)seq[0];
			if (seq.Count > 1)
			{
				this.revocationReason = new CrlReason(DerEnumerated.GetInstance((Asn1TaggedObject)seq[1], true));
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000543F4 File Offset: 0x000543F4
		public DerGeneralizedTime RevocationTime
		{
			get
			{
				return this.revocationTime;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x000543FC File Offset: 0x000543FC
		public CrlReason RevocationReason
		{
			get
			{
				return this.revocationReason;
			}
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00054404 File Offset: 0x00054404
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.revocationTime
			});
			asn1EncodableVector.AddOptionalTagged(true, 0, this.revocationReason);
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040009AA RID: 2474
		private readonly DerGeneralizedTime revocationTime;

		// Token: 0x040009AB RID: 2475
		private readonly CrlReason revocationReason;
	}
}
