using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200084B RID: 2123
	[Flags]
	[ComVisible(true)]
	public enum SigComparerOptions : uint
	{
		// Token: 0x0400271E RID: 10014
		DontCompareTypeScope = 1U,
		// Token: 0x0400271F RID: 10015
		CompareMethodFieldDeclaringType = 2U,
		// Token: 0x04002720 RID: 10016
		ComparePropertyDeclaringType = 4U,
		// Token: 0x04002721 RID: 10017
		CompareEventDeclaringType = 8U,
		// Token: 0x04002722 RID: 10018
		CompareDeclaringTypes = 14U,
		// Token: 0x04002723 RID: 10019
		CompareSentinelParams = 16U,
		// Token: 0x04002724 RID: 10020
		CompareAssemblyPublicKeyToken = 32U,
		// Token: 0x04002725 RID: 10021
		CompareAssemblyVersion = 64U,
		// Token: 0x04002726 RID: 10022
		CompareAssemblyLocale = 128U,
		// Token: 0x04002727 RID: 10023
		TypeRefCanReferenceGlobalType = 256U,
		// Token: 0x04002728 RID: 10024
		DontCompareReturnType = 512U,
		// Token: 0x04002729 RID: 10025
		CaseInsensitiveTypeNamespaces = 2048U,
		// Token: 0x0400272A RID: 10026
		CaseInsensitiveTypeNames = 4096U,
		// Token: 0x0400272B RID: 10027
		CaseInsensitiveTypes = 6144U,
		// Token: 0x0400272C RID: 10028
		CaseInsensitiveMethodFieldNames = 8192U,
		// Token: 0x0400272D RID: 10029
		CaseInsensitivePropertyNames = 16384U,
		// Token: 0x0400272E RID: 10030
		CaseInsensitiveEventNames = 32768U,
		// Token: 0x0400272F RID: 10031
		CaseInsensitiveAll = 63488U,
		// Token: 0x04002730 RID: 10032
		PrivateScopeFieldIsComparable = 65536U,
		// Token: 0x04002731 RID: 10033
		PrivateScopeMethodIsComparable = 131072U,
		// Token: 0x04002732 RID: 10034
		PrivateScopeIsComparable = 196608U,
		// Token: 0x04002733 RID: 10035
		RawSignatureCompare = 262144U,
		// Token: 0x04002734 RID: 10036
		IgnoreModifiers = 524288U,
		// Token: 0x04002735 RID: 10037
		MscorlibIsNotSpecial = 1048576U,
		// Token: 0x04002736 RID: 10038
		DontProjectWinMDRefs = 2097152U,
		// Token: 0x04002737 RID: 10039
		DontCheckTypeEquivalence = 4194304U,
		// Token: 0x04002738 RID: 10040
		IgnoreMultiDimensionalArrayLowerBoundsAndSizes = 8388608U
	}
}
