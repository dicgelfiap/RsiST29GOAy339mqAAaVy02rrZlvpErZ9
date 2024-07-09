using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001E4 RID: 484
	public class AttCertValidityPeriod : Asn1Encodable
	{
		// Token: 0x06000F86 RID: 3974 RVA: 0x0005CA7C File Offset: 0x0005CA7C
		public static AttCertValidityPeriod GetInstance(object obj)
		{
			if (obj is AttCertValidityPeriod || obj == null)
			{
				return (AttCertValidityPeriod)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AttCertValidityPeriod((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0005CAD8 File Offset: 0x0005CAD8
		public static AttCertValidityPeriod GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return AttCertValidityPeriod.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0005CAE8 File Offset: 0x0005CAE8
		private AttCertValidityPeriod(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.notBeforeTime = DerGeneralizedTime.GetInstance(seq[0]);
			this.notAfterTime = DerGeneralizedTime.GetInstance(seq[1]);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0005CB4C File Offset: 0x0005CB4C
		public AttCertValidityPeriod(DerGeneralizedTime notBeforeTime, DerGeneralizedTime notAfterTime)
		{
			this.notBeforeTime = notBeforeTime;
			this.notAfterTime = notAfterTime;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0005CB64 File Offset: 0x0005CB64
		public DerGeneralizedTime NotBeforeTime
		{
			get
			{
				return this.notBeforeTime;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0005CB6C File Offset: 0x0005CB6C
		public DerGeneralizedTime NotAfterTime
		{
			get
			{
				return this.notAfterTime;
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0005CB74 File Offset: 0x0005CB74
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.notBeforeTime,
				this.notAfterTime
			});
		}

		// Token: 0x04000B9E RID: 2974
		private readonly DerGeneralizedTime notBeforeTime;

		// Token: 0x04000B9F RID: 2975
		private readonly DerGeneralizedTime notAfterTime;
	}
}
