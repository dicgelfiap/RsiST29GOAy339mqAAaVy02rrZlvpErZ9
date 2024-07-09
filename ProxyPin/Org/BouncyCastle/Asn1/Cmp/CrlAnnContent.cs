using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000D8 RID: 216
	public class CrlAnnContent : Asn1Encodable
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x000417D0 File Offset: 0x000417D0
		private CrlAnnContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000417E0 File Offset: 0x000417E0
		public static CrlAnnContent GetInstance(object obj)
		{
			if (obj is CrlAnnContent)
			{
				return (CrlAnnContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlAnnContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00041834 File Offset: 0x00041834
		public virtual CertificateList[] ToCertificateListArray()
		{
			CertificateList[] array = new CertificateList[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertificateList.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00041880 File Offset: 0x00041880
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x04000609 RID: 1545
		private readonly Asn1Sequence content;
	}
}
