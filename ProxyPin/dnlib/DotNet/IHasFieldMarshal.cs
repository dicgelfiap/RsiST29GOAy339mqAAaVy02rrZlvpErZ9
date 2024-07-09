using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007D5 RID: 2005
	[ComVisible(true)]
	public interface IHasFieldMarshal : ICodedToken, IMDTokenProvider, IHasCustomAttribute, IHasConstant, IFullName
	{
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x0600489B RID: 18587
		int HasFieldMarshalTag { get; }

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x0600489C RID: 18588
		// (set) Token: 0x0600489D RID: 18589
		MarshalType MarshalType { get; set; }

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600489E RID: 18590
		bool HasMarshalType { get; }
	}
}
