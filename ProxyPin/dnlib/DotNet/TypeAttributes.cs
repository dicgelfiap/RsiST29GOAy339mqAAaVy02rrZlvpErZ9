using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000859 RID: 2137
	[Flags]
	[ComVisible(true)]
	public enum TypeAttributes : uint
	{
		// Token: 0x0400276D RID: 10093
		VisibilityMask = 7U,
		// Token: 0x0400276E RID: 10094
		NotPublic = 0U,
		// Token: 0x0400276F RID: 10095
		Public = 1U,
		// Token: 0x04002770 RID: 10096
		NestedPublic = 2U,
		// Token: 0x04002771 RID: 10097
		NestedPrivate = 3U,
		// Token: 0x04002772 RID: 10098
		NestedFamily = 4U,
		// Token: 0x04002773 RID: 10099
		NestedAssembly = 5U,
		// Token: 0x04002774 RID: 10100
		NestedFamANDAssem = 6U,
		// Token: 0x04002775 RID: 10101
		NestedFamORAssem = 7U,
		// Token: 0x04002776 RID: 10102
		LayoutMask = 24U,
		// Token: 0x04002777 RID: 10103
		AutoLayout = 0U,
		// Token: 0x04002778 RID: 10104
		SequentialLayout = 8U,
		// Token: 0x04002779 RID: 10105
		ExplicitLayout = 16U,
		// Token: 0x0400277A RID: 10106
		ClassSemanticsMask = 32U,
		// Token: 0x0400277B RID: 10107
		ClassSemanticMask = 32U,
		// Token: 0x0400277C RID: 10108
		Class = 0U,
		// Token: 0x0400277D RID: 10109
		Interface = 32U,
		// Token: 0x0400277E RID: 10110
		Abstract = 128U,
		// Token: 0x0400277F RID: 10111
		Sealed = 256U,
		// Token: 0x04002780 RID: 10112
		SpecialName = 1024U,
		// Token: 0x04002781 RID: 10113
		Import = 4096U,
		// Token: 0x04002782 RID: 10114
		Serializable = 8192U,
		// Token: 0x04002783 RID: 10115
		WindowsRuntime = 16384U,
		// Token: 0x04002784 RID: 10116
		StringFormatMask = 196608U,
		// Token: 0x04002785 RID: 10117
		AnsiClass = 0U,
		// Token: 0x04002786 RID: 10118
		UnicodeClass = 65536U,
		// Token: 0x04002787 RID: 10119
		AutoClass = 131072U,
		// Token: 0x04002788 RID: 10120
		CustomFormatClass = 196608U,
		// Token: 0x04002789 RID: 10121
		CustomFormatMask = 12582912U,
		// Token: 0x0400278A RID: 10122
		BeforeFieldInit = 1048576U,
		// Token: 0x0400278B RID: 10123
		Forwarder = 2097152U,
		// Token: 0x0400278C RID: 10124
		ReservedMask = 264192U,
		// Token: 0x0400278D RID: 10125
		RTSpecialName = 2048U,
		// Token: 0x0400278E RID: 10126
		HasSecurity = 262144U
	}
}
