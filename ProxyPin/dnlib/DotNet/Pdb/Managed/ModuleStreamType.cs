using System;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x0200094E RID: 2382
	internal enum ModuleStreamType : uint
	{
		// Token: 0x04002C4E RID: 11342
		Symbols = 241U,
		// Token: 0x04002C4F RID: 11343
		Lines,
		// Token: 0x04002C50 RID: 11344
		StringTable,
		// Token: 0x04002C51 RID: 11345
		FileInfo,
		// Token: 0x04002C52 RID: 11346
		FrameData,
		// Token: 0x04002C53 RID: 11347
		InlineeLines,
		// Token: 0x04002C54 RID: 11348
		CrossScopeImports,
		// Token: 0x04002C55 RID: 11349
		CrossScopeExports,
		// Token: 0x04002C56 RID: 11350
		ILLines,
		// Token: 0x04002C57 RID: 11351
		FuncMDTokenMap,
		// Token: 0x04002C58 RID: 11352
		TypeMDTokenMap,
		// Token: 0x04002C59 RID: 11353
		MergedAssemblyInput
	}
}
