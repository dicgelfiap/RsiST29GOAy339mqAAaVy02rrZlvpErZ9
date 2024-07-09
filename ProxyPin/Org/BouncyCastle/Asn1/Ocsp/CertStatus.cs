using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200018F RID: 399
	public class CertStatus : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000D29 RID: 3369 RVA: 0x00053438 File Offset: 0x00053438
		public CertStatus()
		{
			this.tagNo = 0;
			this.value = DerNull.Instance;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00053454 File Offset: 0x00053454
		public CertStatus(RevokedInfo info)
		{
			this.tagNo = 1;
			this.value = info;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0005346C File Offset: 0x0005346C
		public CertStatus(int tagNo, Asn1Encodable value)
		{
			this.tagNo = tagNo;
			this.value = value;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00053484 File Offset: 0x00053484
		public CertStatus(Asn1TaggedObject choice)
		{
			this.tagNo = choice.TagNo;
			switch (choice.TagNo)
			{
			case 0:
			case 2:
				this.value = DerNull.Instance;
				return;
			case 1:
				this.value = RevokedInfo.GetInstance(choice, false);
				return;
			default:
				throw new ArgumentException("Unknown tag encountered: " + choice.TagNo);
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000534FC File Offset: 0x000534FC
		public static CertStatus GetInstance(object obj)
		{
			if (obj == null || obj is CertStatus)
			{
				return (CertStatus)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new CertStatus((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00053558 File Offset: 0x00053558
		public int TagNo
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00053560 File Offset: 0x00053560
		public Asn1Encodable Status
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00053568 File Offset: 0x00053568
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.tagNo, this.value);
		}

		// Token: 0x04000983 RID: 2435
		private readonly int tagNo;

		// Token: 0x04000984 RID: 2436
		private readonly Asn1Encodable value;
	}
}
