using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000693 RID: 1683
	[Serializable]
	public class PkixCertPathBuilderException : GeneralSecurityException
	{
		// Token: 0x06003AB6 RID: 15030 RVA: 0x0013C558 File Offset: 0x0013C558
		public PkixCertPathBuilderException()
		{
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x0013C560 File Offset: 0x0013C560
		public PkixCertPathBuilderException(string message) : base(message)
		{
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x0013C56C File Offset: 0x0013C56C
		public PkixCertPathBuilderException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
