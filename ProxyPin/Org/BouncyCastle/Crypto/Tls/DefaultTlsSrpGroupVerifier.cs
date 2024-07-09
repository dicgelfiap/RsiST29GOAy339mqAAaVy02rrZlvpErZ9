using System;
using System.Collections;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004EF RID: 1263
	public class DefaultTlsSrpGroupVerifier : TlsSrpGroupVerifier
	{
		// Token: 0x060026C1 RID: 9921 RVA: 0x000D1A8C File Offset: 0x000D1A8C
		static DefaultTlsSrpGroupVerifier()
		{
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_1024);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_1536);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_2048);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_3072);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_4096);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_6144);
			DefaultTlsSrpGroupVerifier.DefaultGroups.Add(Srp6StandardGroups.rfc5054_8192);
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000D1B18 File Offset: 0x000D1B18
		public DefaultTlsSrpGroupVerifier() : this(DefaultTlsSrpGroupVerifier.DefaultGroups)
		{
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000D1B28 File Offset: 0x000D1B28
		public DefaultTlsSrpGroupVerifier(IList groups)
		{
			this.mGroups = groups;
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x000D1B38 File Offset: 0x000D1B38
		public virtual bool Accept(Srp6GroupParameters group)
		{
			foreach (object obj in this.mGroups)
			{
				Srp6GroupParameters b = (Srp6GroupParameters)obj;
				if (this.AreGroupsEqual(group, b))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000D1BAC File Offset: 0x000D1BAC
		protected virtual bool AreGroupsEqual(Srp6GroupParameters a, Srp6GroupParameters b)
		{
			return a == b || (this.AreParametersEqual(a.N, b.N) && this.AreParametersEqual(a.G, b.G));
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000D1BF4 File Offset: 0x000D1BF4
		protected virtual bool AreParametersEqual(BigInteger a, BigInteger b)
		{
			return a == b || a.Equals(b);
		}

		// Token: 0x0400191F RID: 6431
		protected static readonly IList DefaultGroups = Platform.CreateArrayList();

		// Token: 0x04001920 RID: 6432
		protected readonly IList mGroups;
	}
}
