using System;

namespace Org.BouncyCastle.Asn1.X509.SigI
{
	// Token: 0x020001E1 RID: 481
	public sealed class SigIObjectIdentifiers
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x0005C774 File Offset: 0x0005C774
		private SigIObjectIdentifiers()
		{
		}

		// Token: 0x04000B91 RID: 2961
		public static readonly DerObjectIdentifier IdSigI = new DerObjectIdentifier("1.3.36.8");

		// Token: 0x04000B92 RID: 2962
		public static readonly DerObjectIdentifier IdSigIKP = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigI + ".2");

		// Token: 0x04000B93 RID: 2963
		public static readonly DerObjectIdentifier IdSigICP = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigI + ".1");

		// Token: 0x04000B94 RID: 2964
		public static readonly DerObjectIdentifier IdSigION = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigI + ".4");

		// Token: 0x04000B95 RID: 2965
		public static readonly DerObjectIdentifier IdSigIKPDirectoryService = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigIKP + ".1");

		// Token: 0x04000B96 RID: 2966
		public static readonly DerObjectIdentifier IdSigIONPersonalData = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigION + ".1");

		// Token: 0x04000B97 RID: 2967
		public static readonly DerObjectIdentifier IdSigICPSigConform = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigICP + ".1");
	}
}
