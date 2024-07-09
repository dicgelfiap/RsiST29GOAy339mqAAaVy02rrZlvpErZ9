using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200084D RID: 2125
	[ComVisible(true)]
	public interface ISignatureReaderHelper
	{
		// Token: 0x0600502C RID: 20524
		ITypeDefOrRef ResolveTypeDefOrRef(uint codedToken, GenericParamContext gpContext);

		// Token: 0x0600502D RID: 20525
		TypeSig ConvertRTInternalAddress(IntPtr address);
	}
}
