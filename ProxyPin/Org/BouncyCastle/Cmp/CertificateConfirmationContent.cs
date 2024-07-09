using System;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Cms;

namespace Org.BouncyCastle.Cmp
{
	// Token: 0x020002C7 RID: 711
	public class CertificateConfirmationContent
	{
		// Token: 0x060015B3 RID: 5555 RVA: 0x00072784 File Offset: 0x00072784
		public CertificateConfirmationContent(CertConfirmContent content)
		{
			this.content = content;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00072794 File Offset: 0x00072794
		public CertificateConfirmationContent(CertConfirmContent content, DefaultDigestAlgorithmIdentifierFinder digestAlgFinder)
		{
			this.content = content;
			this.digestAlgFinder = digestAlgFinder;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000727AC File Offset: 0x000727AC
		public CertConfirmContent ToAsn1Structure()
		{
			return this.content;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000727B4 File Offset: 0x000727B4
		public CertificateStatus[] GetStatusMessages()
		{
			CertStatus[] array = this.content.ToCertStatusArray();
			CertificateStatus[] array2 = new CertificateStatus[array.Length];
			for (int num = 0; num != array2.Length; num++)
			{
				array2[num] = new CertificateStatus(this.digestAlgFinder, array[num]);
			}
			return array2;
		}

		// Token: 0x04000EDC RID: 3804
		private readonly DefaultDigestAlgorithmIdentifierFinder digestAlgFinder;

		// Token: 0x04000EDD RID: 3805
		private readonly CertConfirmContent content;
	}
}
