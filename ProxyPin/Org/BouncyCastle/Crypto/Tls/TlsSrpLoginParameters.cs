using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200054E RID: 1358
	public class TlsSrpLoginParameters
	{
		// Token: 0x060029B4 RID: 10676 RVA: 0x000DFFAC File Offset: 0x000DFFAC
		public TlsSrpLoginParameters(Srp6GroupParameters group, BigInteger verifier, byte[] salt)
		{
			this.mGroup = group;
			this.mVerifier = verifier;
			this.mSalt = salt;
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x000DFFCC File Offset: 0x000DFFCC
		public virtual Srp6GroupParameters Group
		{
			get
			{
				return this.mGroup;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x000DFFD4 File Offset: 0x000DFFD4
		public virtual byte[] Salt
		{
			get
			{
				return this.mSalt;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000DFFDC File Offset: 0x000DFFDC
		public virtual BigInteger Verifier
		{
			get
			{
				return this.mVerifier;
			}
		}

		// Token: 0x04001B18 RID: 6936
		protected readonly Srp6GroupParameters mGroup;

		// Token: 0x04001B19 RID: 6937
		protected readonly BigInteger mVerifier;

		// Token: 0x04001B1A RID: 6938
		protected readonly byte[] mSalt;
	}
}
