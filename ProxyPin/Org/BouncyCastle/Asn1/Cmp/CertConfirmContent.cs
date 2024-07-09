using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020000CE RID: 206
	public class CertConfirmContent : Asn1Encodable
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x00040B70 File Offset: 0x00040B70
		private CertConfirmContent(Asn1Sequence seq)
		{
			this.content = seq;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00040B80 File Offset: 0x00040B80
		public static CertConfirmContent GetInstance(object obj)
		{
			if (obj is CertConfirmContent)
			{
				return (CertConfirmContent)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CertConfirmContent((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00040BD4 File Offset: 0x00040BD4
		public virtual CertStatus[] ToCertStatusArray()
		{
			CertStatus[] array = new CertStatus[this.content.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = CertStatus.GetInstance(this.content[num]);
			}
			return array;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00040C20 File Offset: 0x00040C20
		public override Asn1Object ToAsn1Object()
		{
			return this.content;
		}

		// Token: 0x040005DB RID: 1499
		private readonly Asn1Sequence content;
	}
}
