using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000825 RID: 2085
	[ComVisible(true)]
	public enum NativeType : uint
	{
		// Token: 0x04002640 RID: 9792
		End,
		// Token: 0x04002641 RID: 9793
		Void,
		// Token: 0x04002642 RID: 9794
		Boolean,
		// Token: 0x04002643 RID: 9795
		I1,
		// Token: 0x04002644 RID: 9796
		U1,
		// Token: 0x04002645 RID: 9797
		I2,
		// Token: 0x04002646 RID: 9798
		U2,
		// Token: 0x04002647 RID: 9799
		I4,
		// Token: 0x04002648 RID: 9800
		U4,
		// Token: 0x04002649 RID: 9801
		I8,
		// Token: 0x0400264A RID: 9802
		U8,
		// Token: 0x0400264B RID: 9803
		R4,
		// Token: 0x0400264C RID: 9804
		R8,
		// Token: 0x0400264D RID: 9805
		SysChar,
		// Token: 0x0400264E RID: 9806
		Variant,
		// Token: 0x0400264F RID: 9807
		Currency,
		// Token: 0x04002650 RID: 9808
		Ptr,
		// Token: 0x04002651 RID: 9809
		Decimal,
		// Token: 0x04002652 RID: 9810
		Date,
		// Token: 0x04002653 RID: 9811
		BStr,
		// Token: 0x04002654 RID: 9812
		LPStr,
		// Token: 0x04002655 RID: 9813
		LPWStr,
		// Token: 0x04002656 RID: 9814
		LPTStr,
		// Token: 0x04002657 RID: 9815
		FixedSysString,
		// Token: 0x04002658 RID: 9816
		ObjectRef,
		// Token: 0x04002659 RID: 9817
		IUnknown,
		// Token: 0x0400265A RID: 9818
		IDispatch,
		// Token: 0x0400265B RID: 9819
		Struct,
		// Token: 0x0400265C RID: 9820
		IntF,
		// Token: 0x0400265D RID: 9821
		SafeArray,
		// Token: 0x0400265E RID: 9822
		FixedArray,
		// Token: 0x0400265F RID: 9823
		Int,
		// Token: 0x04002660 RID: 9824
		UInt,
		// Token: 0x04002661 RID: 9825
		NestedStruct,
		// Token: 0x04002662 RID: 9826
		ByValStr,
		// Token: 0x04002663 RID: 9827
		ANSIBStr,
		// Token: 0x04002664 RID: 9828
		TBStr,
		// Token: 0x04002665 RID: 9829
		VariantBool,
		// Token: 0x04002666 RID: 9830
		Func,
		// Token: 0x04002667 RID: 9831
		ASAny = 40U,
		// Token: 0x04002668 RID: 9832
		Array = 42U,
		// Token: 0x04002669 RID: 9833
		LPStruct,
		// Token: 0x0400266A RID: 9834
		CustomMarshaler,
		// Token: 0x0400266B RID: 9835
		Error,
		// Token: 0x0400266C RID: 9836
		IInspectable,
		// Token: 0x0400266D RID: 9837
		HString,
		// Token: 0x0400266E RID: 9838
		LPUTF8Str,
		// Token: 0x0400266F RID: 9839
		Max = 80U,
		// Token: 0x04002670 RID: 9840
		NotInitialized = 4294967294U,
		// Token: 0x04002671 RID: 9841
		RawBlob
	}
}
