using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x0200081A RID: 2074
	[ComVisible(true)]
	public sealed class ModuleCreationOptions
	{
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06004BAF RID: 19375 RVA: 0x0017E948 File Offset: 0x0017E948
		// (set) Token: 0x06004BB0 RID: 19376 RVA: 0x0017E950 File Offset: 0x0017E950
		public ModuleContext Context { get; set; }

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06004BB1 RID: 19377 RVA: 0x0017E95C File Offset: 0x0017E95C
		// (set) Token: 0x06004BB2 RID: 19378 RVA: 0x0017E964 File Offset: 0x0017E964
		public PdbReaderOptions PdbOptions { get; set; }

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x0017E970 File Offset: 0x0017E970
		// (set) Token: 0x06004BB4 RID: 19380 RVA: 0x0017E978 File Offset: 0x0017E978
		public object PdbFileOrData { get; set; }

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06004BB5 RID: 19381 RVA: 0x0017E984 File Offset: 0x0017E984
		// (set) Token: 0x06004BB6 RID: 19382 RVA: 0x0017E98C File Offset: 0x0017E98C
		public bool TryToLoadPdbFromDisk { get; set; } = true;

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06004BB7 RID: 19383 RVA: 0x0017E998 File Offset: 0x0017E998
		// (set) Token: 0x06004BB8 RID: 19384 RVA: 0x0017E9A0 File Offset: 0x0017E9A0
		public AssemblyRef CorLibAssemblyRef { get; set; }

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06004BB9 RID: 19385 RVA: 0x0017E9AC File Offset: 0x0017E9AC
		// (set) Token: 0x06004BBA RID: 19386 RVA: 0x0017E9B4 File Offset: 0x0017E9B4
		public CLRRuntimeReaderKind Runtime { get; set; }

		// Token: 0x06004BBB RID: 19387 RVA: 0x0017E9C0 File Offset: 0x0017E9C0
		public ModuleCreationOptions()
		{
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x0017E9D0 File Offset: 0x0017E9D0
		public ModuleCreationOptions(ModuleContext context)
		{
			this.Context = context;
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x0017E9E8 File Offset: 0x0017E9E8
		public ModuleCreationOptions(CLRRuntimeReaderKind runtime)
		{
			this.Runtime = runtime;
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0017EA00 File Offset: 0x0017EA00
		public ModuleCreationOptions(ModuleContext context, CLRRuntimeReaderKind runtime)
		{
			this.Context = context;
			this.Runtime = runtime;
		}

		// Token: 0x040025D6 RID: 9686
		internal static readonly ModuleCreationOptions Default = new ModuleCreationOptions();

		// Token: 0x040025D8 RID: 9688
		internal const PdbReaderOptions DefaultPdbReaderOptions = PdbReaderOptions.None;
	}
}
