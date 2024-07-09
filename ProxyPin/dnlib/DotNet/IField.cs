using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007CE RID: 1998
	[ComVisible(true)]
	public interface IField : ICodedToken, IMDTokenProvider, ITokenOperand, IFullName, IMemberRef, IOwnerModule, IIsTypeOrMethod
	{
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06004890 RID: 18576
		// (set) Token: 0x06004891 RID: 18577
		FieldSig FieldSig { get; set; }
	}
}
