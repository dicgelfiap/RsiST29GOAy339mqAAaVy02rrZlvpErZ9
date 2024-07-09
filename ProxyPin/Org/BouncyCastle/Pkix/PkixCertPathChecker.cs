using System;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000696 RID: 1686
	public abstract class PkixCertPathChecker
	{
		// Token: 0x06003AC3 RID: 15043
		public abstract void Init(bool forward);

		// Token: 0x06003AC4 RID: 15044
		public abstract bool IsForwardCheckingSupported();

		// Token: 0x06003AC5 RID: 15045
		public abstract ISet GetSupportedExtensions();

		// Token: 0x06003AC6 RID: 15046
		public abstract void Check(X509Certificate cert, ISet unresolvedCritExts);

		// Token: 0x06003AC7 RID: 15047 RVA: 0x0013C74C File Offset: 0x0013C74C
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
