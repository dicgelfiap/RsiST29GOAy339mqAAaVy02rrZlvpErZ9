using System;
using System.Collections;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004EA RID: 1258
	public class DefaultTlsDHVerifier : TlsDHVerifier
	{
		// Token: 0x0600269E RID: 9886 RVA: 0x000D1410 File Offset: 0x000D1410
		private static void AddDefaultGroup(DHParameters dhParameters)
		{
			DefaultTlsDHVerifier.DefaultGroups.Add(dhParameters);
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000D1420 File Offset: 0x000D1420
		static DefaultTlsDHVerifier()
		{
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe2048);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe3072);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe4096);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe6144);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc7919_ffdhe8192);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_1536);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_2048);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_3072);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_4096);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_6144);
			DefaultTlsDHVerifier.AddDefaultGroup(DHStandardGroups.rfc3526_8192);
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000D14B4 File Offset: 0x000D14B4
		public DefaultTlsDHVerifier() : this(DefaultTlsDHVerifier.DefaultMinimumPrimeBits)
		{
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000D14C4 File Offset: 0x000D14C4
		public DefaultTlsDHVerifier(int minimumPrimeBits) : this(DefaultTlsDHVerifier.DefaultGroups, minimumPrimeBits)
		{
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000D14D4 File Offset: 0x000D14D4
		public DefaultTlsDHVerifier(IList groups, int minimumPrimeBits)
		{
			this.mGroups = groups;
			this.mMinimumPrimeBits = minimumPrimeBits;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000D14EC File Offset: 0x000D14EC
		public virtual bool Accept(DHParameters dhParameters)
		{
			return this.CheckMinimumPrimeBits(dhParameters) && this.CheckGroup(dhParameters);
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x000D1504 File Offset: 0x000D1504
		public virtual int MinimumPrimeBits
		{
			get
			{
				return this.mMinimumPrimeBits;
			}
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000D150C File Offset: 0x000D150C
		protected virtual bool AreGroupsEqual(DHParameters a, DHParameters b)
		{
			return a == b || (this.AreParametersEqual(a.P, b.P) && this.AreParametersEqual(a.G, b.G));
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000D1554 File Offset: 0x000D1554
		protected virtual bool AreParametersEqual(BigInteger a, BigInteger b)
		{
			return a == b || a.Equals(b);
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000D1568 File Offset: 0x000D1568
		protected virtual bool CheckGroup(DHParameters dhParameters)
		{
			foreach (object obj in this.mGroups)
			{
				DHParameters b = (DHParameters)obj;
				if (this.AreGroupsEqual(dhParameters, b))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000D15DC File Offset: 0x000D15DC
		protected virtual bool CheckMinimumPrimeBits(DHParameters dhParameters)
		{
			return dhParameters.P.BitLength >= this.MinimumPrimeBits;
		}

		// Token: 0x04001913 RID: 6419
		public static readonly int DefaultMinimumPrimeBits = 2048;

		// Token: 0x04001914 RID: 6420
		protected static readonly IList DefaultGroups = Platform.CreateArrayList();

		// Token: 0x04001915 RID: 6421
		protected readonly IList mGroups;

		// Token: 0x04001916 RID: 6422
		protected readonly int mMinimumPrimeBits;
	}
}
