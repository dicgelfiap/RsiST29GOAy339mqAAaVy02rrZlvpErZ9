using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000779 RID: 1913
	[ComVisible(true)]
	public enum AssemblyHashAlgorithm : uint
	{
		// Token: 0x040023CA RID: 9162
		None,
		// Token: 0x040023CB RID: 9163
		MD2 = 32769U,
		// Token: 0x040023CC RID: 9164
		MD4,
		// Token: 0x040023CD RID: 9165
		MD5,
		// Token: 0x040023CE RID: 9166
		SHA1,
		// Token: 0x040023CF RID: 9167
		MAC,
		// Token: 0x040023D0 RID: 9168
		SSL3_SHAMD5 = 32776U,
		// Token: 0x040023D1 RID: 9169
		HMAC,
		// Token: 0x040023D2 RID: 9170
		TLS1PRF,
		// Token: 0x040023D3 RID: 9171
		HASH_REPLACE_OWF,
		// Token: 0x040023D4 RID: 9172
		SHA_256,
		// Token: 0x040023D5 RID: 9173
		SHA_384,
		// Token: 0x040023D6 RID: 9174
		SHA_512
	}
}
