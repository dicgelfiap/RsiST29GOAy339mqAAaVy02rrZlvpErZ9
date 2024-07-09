using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000CD RID: 205
	public class CAKeyUpdAnnContent : Asn1Encodable
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x00040A6C File Offset: 0x00040A6C
		private CAKeyUpdAnnContent(Asn1Sequence seq)
		{
			this.oldWithNew = CmpCertificate.GetInstance(seq[0]);
			this.newWithOld = CmpCertificate.GetInstance(seq[1]);
			this.newWithNew = CmpCertificate.GetInstance(seq[2]);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00040ABC File Offset: 0x00040ABC
		public static CAKeyUpdAnnContent GetInstance(object obj)
		{
			if (obj is CAKeyUpdAnnContent)
			{
				return (CAKeyUpdAnnContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CAKeyUpdAnnContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00040B10 File Offset: 0x00040B10
		public virtual CmpCertificate OldWithNew
		{
			get
			{
				return this.oldWithNew;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00040B18 File Offset: 0x00040B18
		public virtual CmpCertificate NewWithOld
		{
			get
			{
				return this.newWithOld;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00040B20 File Offset: 0x00040B20
		public virtual CmpCertificate NewWithNew
		{
			get
			{
				return this.newWithNew;
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00040B28 File Offset: 0x00040B28
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.oldWithNew,
				this.newWithOld,
				this.newWithNew
			});
		}

		// Token: 0x040005D8 RID: 1496
		private readonly CmpCertificate oldWithNew;

		// Token: 0x040005D9 RID: 1497
		private readonly CmpCertificate newWithOld;

		// Token: 0x040005DA RID: 1498
		private readonly CmpCertificate newWithNew;
	}
}
