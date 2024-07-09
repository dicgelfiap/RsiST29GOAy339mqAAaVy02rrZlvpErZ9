using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200068B RID: 1675
	public abstract class PkixAttrCertChecker
	{
		// Token: 0x06003A64 RID: 14948
		public abstract ISet GetSupportedExtensions();

		// Token: 0x06003A65 RID: 14949
		public abstract void Check(IX509AttributeCertificate attrCert, PkixCertPath certPath, PkixCertPath holderCertPath, ICollection unresolvedCritExts);

		// Token: 0x06003A66 RID: 14950
		public abstract PkixAttrCertChecker Clone();
	}
}
