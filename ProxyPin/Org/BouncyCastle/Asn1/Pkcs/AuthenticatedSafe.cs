using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001A2 RID: 418
	public class AuthenticatedSafe : Asn1Encodable
	{
		// Token: 0x06000DB2 RID: 3506 RVA: 0x00054DDC File Offset: 0x00054DDC
		private static ContentInfo[] Copy(ContentInfo[] info)
		{
			return (ContentInfo[])info.Clone();
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00054DEC File Offset: 0x00054DEC
		public static AuthenticatedSafe GetInstance(object obj)
		{
			if (obj is AuthenticatedSafe)
			{
				return (AuthenticatedSafe)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new AuthenticatedSafe(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00054E14 File Offset: 0x00054E14
		[Obsolete("Use 'GetInstance' instead")]
		public AuthenticatedSafe(Asn1Sequence seq)
		{
			this.info = new ContentInfo[seq.Count];
			for (int num = 0; num != this.info.Length; num++)
			{
				this.info[num] = ContentInfo.GetInstance(seq[num]);
			}
			this.isBer = (seq is BerSequence);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00054E7C File Offset: 0x00054E7C
		public AuthenticatedSafe(ContentInfo[] info)
		{
			this.info = AuthenticatedSafe.Copy(info);
			this.isBer = true;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00054E98 File Offset: 0x00054E98
		public ContentInfo[] GetContentInfo()
		{
			return AuthenticatedSafe.Copy(this.info);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00054EA8 File Offset: 0x00054EA8
		public override Asn1Object ToAsn1Object()
		{
			if (this.isBer)
			{
				return new BerSequence(this.info);
			}
			return new DerSequence(this.info);
		}

		// Token: 0x040009CC RID: 2508
		private readonly ContentInfo[] info;

		// Token: 0x040009CD RID: 2509
		private readonly bool isBer;
	}
}
