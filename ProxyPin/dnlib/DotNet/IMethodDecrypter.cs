using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x020007E2 RID: 2018
	[ComVisible(true)]
	public interface IMethodDecrypter
	{
		// Token: 0x060048CC RID: 18636
		bool GetMethodBody(uint rid, RVA rva, IList<Parameter> parameters, GenericParamContext gpContext, out MethodBody methodBody);
	}
}
