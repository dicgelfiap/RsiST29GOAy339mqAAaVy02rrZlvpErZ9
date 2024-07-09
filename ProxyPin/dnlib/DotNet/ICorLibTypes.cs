using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007E0 RID: 2016
	[ComVisible(true)]
	public interface ICorLibTypes
	{
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060048B2 RID: 18610
		CorLibTypeSig Void { get; }

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060048B3 RID: 18611
		CorLibTypeSig Boolean { get; }

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060048B4 RID: 18612
		CorLibTypeSig Char { get; }

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060048B5 RID: 18613
		CorLibTypeSig SByte { get; }

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060048B6 RID: 18614
		CorLibTypeSig Byte { get; }

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060048B7 RID: 18615
		CorLibTypeSig Int16 { get; }

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060048B8 RID: 18616
		CorLibTypeSig UInt16 { get; }

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060048B9 RID: 18617
		CorLibTypeSig Int32 { get; }

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060048BA RID: 18618
		CorLibTypeSig UInt32 { get; }

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060048BB RID: 18619
		CorLibTypeSig Int64 { get; }

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060048BC RID: 18620
		CorLibTypeSig UInt64 { get; }

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060048BD RID: 18621
		CorLibTypeSig Single { get; }

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060048BE RID: 18622
		CorLibTypeSig Double { get; }

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060048BF RID: 18623
		CorLibTypeSig String { get; }

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060048C0 RID: 18624
		CorLibTypeSig TypedReference { get; }

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060048C1 RID: 18625
		CorLibTypeSig IntPtr { get; }

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060048C2 RID: 18626
		CorLibTypeSig UIntPtr { get; }

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060048C3 RID: 18627
		CorLibTypeSig Object { get; }

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060048C4 RID: 18628
		AssemblyRef AssemblyRef { get; }

		// Token: 0x060048C5 RID: 18629
		TypeRef GetTypeRef(string @namespace, string name);
	}
}
