using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Crmf;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x0200032C RID: 812
	public class RegTokenControl : IControl
	{
		// Token: 0x0600185B RID: 6235 RVA: 0x0007DCD4 File Offset: 0x0007DCD4
		public RegTokenControl(DerUtf8String token)
		{
			this.token = token;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0007DCE4 File Offset: 0x0007DCE4
		public RegTokenControl(string token)
		{
			this.token = new DerUtf8String(token);
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0007DCF8 File Offset: 0x0007DCF8
		public DerObjectIdentifier Type
		{
			get
			{
				return RegTokenControl.type;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x0007DD00 File Offset: 0x0007DD00
		public Asn1Encodable Value
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x04001020 RID: 4128
		private static readonly DerObjectIdentifier type = CrmfObjectIdentifiers.id_regCtrl_regToken;

		// Token: 0x04001021 RID: 4129
		private readonly DerUtf8String token;
	}
}
