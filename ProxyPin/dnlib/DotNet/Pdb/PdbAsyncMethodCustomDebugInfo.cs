using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x0200090A RID: 2314
	[ComVisible(true)]
	public sealed class PdbAsyncMethodCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x001B5E00 File Offset: 0x001B5E00
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.AsyncMethod;
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06005988 RID: 22920 RVA: 0x001B5E08 File Offset: 0x001B5E08
		public override Guid Guid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06005989 RID: 22921 RVA: 0x001B5E10 File Offset: 0x001B5E10
		// (set) Token: 0x0600598A RID: 22922 RVA: 0x001B5E18 File Offset: 0x001B5E18
		public MethodDef KickoffMethod { get; set; }

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x0600598B RID: 22923 RVA: 0x001B5E24 File Offset: 0x001B5E24
		// (set) Token: 0x0600598C RID: 22924 RVA: 0x001B5E2C File Offset: 0x001B5E2C
		public Instruction CatchHandlerInstruction { get; set; }

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x0600598D RID: 22925 RVA: 0x001B5E38 File Offset: 0x001B5E38
		public IList<PdbAsyncStepInfo> StepInfos
		{
			get
			{
				return this.asyncStepInfos;
			}
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x001B5E40 File Offset: 0x001B5E40
		public PdbAsyncMethodCustomDebugInfo()
		{
			this.asyncStepInfos = new List<PdbAsyncStepInfo>();
		}

		// Token: 0x0600598F RID: 22927 RVA: 0x001B5E54 File Offset: 0x001B5E54
		public PdbAsyncMethodCustomDebugInfo(int stepInfosCapacity)
		{
			this.asyncStepInfos = new List<PdbAsyncStepInfo>(stepInfosCapacity);
		}

		// Token: 0x04002B56 RID: 11094
		private readonly IList<PdbAsyncStepInfo> asyncStepInfos;
	}
}
