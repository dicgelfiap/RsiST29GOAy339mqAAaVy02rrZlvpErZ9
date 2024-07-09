using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007CD RID: 1997
	[ComVisible(true)]
	public interface IGenericParameterProvider : ICodedToken, IMDTokenProvider, IIsTypeOrMethod
	{
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x0600488F RID: 18575
		int NumberOfGenericParameters { get; }
	}
}
