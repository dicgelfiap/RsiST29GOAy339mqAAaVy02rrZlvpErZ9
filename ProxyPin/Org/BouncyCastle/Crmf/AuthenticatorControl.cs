using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Crmf;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x0200031A RID: 794
	public class AuthenticatorControl : IControl
	{
		// Token: 0x060017F9 RID: 6137 RVA: 0x0007CA98 File Offset: 0x0007CA98
		public AuthenticatorControl(DerUtf8String token)
		{
			this.token = token;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0007CAA8 File Offset: 0x0007CAA8
		public AuthenticatorControl(string token)
		{
			this.token = new DerUtf8String(token);
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0007CABC File Offset: 0x0007CABC
		public DerObjectIdentifier Type
		{
			get
			{
				return AuthenticatorControl.type;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0007CAC4 File Offset: 0x0007CAC4
		public Asn1Encodable Value
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x04000FF2 RID: 4082
		private static readonly DerObjectIdentifier type = CrmfObjectIdentifiers.id_regCtrl_authenticator;

		// Token: 0x04000FF3 RID: 4083
		private readonly DerUtf8String token;
	}
}
