using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000904 RID: 2308
	internal sealed class PdbAsyncMethodSteppingInformationCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06005963 RID: 22883 RVA: 0x001B5C94 File Offset: 0x001B5C94
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.Unknown;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06005964 RID: 22884 RVA: 0x001B5C9C File Offset: 0x001B5C9C
		public override Guid Guid
		{
			get
			{
				return CustomDebugInfoGuids.AsyncMethodSteppingInformationBlob;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06005965 RID: 22885 RVA: 0x001B5CA4 File Offset: 0x001B5CA4
		// (set) Token: 0x06005966 RID: 22886 RVA: 0x001B5CAC File Offset: 0x001B5CAC
		public Instruction CatchHandler { get; set; }

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06005967 RID: 22887 RVA: 0x001B5CB8 File Offset: 0x001B5CB8
		public IList<PdbAsyncStepInfo> AsyncStepInfos
		{
			get
			{
				return this.asyncStepInfos;
			}
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x001B5CC0 File Offset: 0x001B5CC0
		public PdbAsyncMethodSteppingInformationCustomDebugInfo()
		{
			this.asyncStepInfos = new List<PdbAsyncStepInfo>();
		}

		// Token: 0x04002B4F RID: 11087
		private readonly IList<PdbAsyncStepInfo> asyncStepInfos;
	}
}
