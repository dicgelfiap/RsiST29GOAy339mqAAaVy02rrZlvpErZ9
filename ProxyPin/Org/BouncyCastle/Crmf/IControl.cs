using System;
using Org.BouncyCastle.Asn1;

namespace Org.BouncyCastle.Crmf
{
	// Token: 0x02000319 RID: 793
	public interface IControl
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060017F7 RID: 6135
		DerObjectIdentifier Type { get; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060017F8 RID: 6136
		Asn1Encodable Value { get; }
	}
}
