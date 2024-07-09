using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x0200091C RID: 2332
	[ComVisible(true)]
	public sealed class PdbLocal : IHasCustomDebugInformation
	{
		// Token: 0x06005A07 RID: 23047 RVA: 0x001B661C File Offset: 0x001B661C
		public PdbLocal()
		{
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x001B6630 File Offset: 0x001B6630
		public PdbLocal(Local local, string name, PdbLocalAttributes attributes)
		{
			this.Local = local;
			this.Name = name;
			this.Attributes = attributes;
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06005A09 RID: 23049 RVA: 0x001B6668 File Offset: 0x001B6668
		// (set) Token: 0x06005A0A RID: 23050 RVA: 0x001B6670 File Offset: 0x001B6670
		public Local Local { get; set; }

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06005A0B RID: 23051 RVA: 0x001B667C File Offset: 0x001B667C
		// (set) Token: 0x06005A0C RID: 23052 RVA: 0x001B6684 File Offset: 0x001B6684
		public string Name { get; set; }

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06005A0D RID: 23053 RVA: 0x001B6690 File Offset: 0x001B6690
		// (set) Token: 0x06005A0E RID: 23054 RVA: 0x001B6698 File Offset: 0x001B6698
		public PdbLocalAttributes Attributes { get; set; }

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06005A0F RID: 23055 RVA: 0x001B66A4 File Offset: 0x001B66A4
		public int Index
		{
			get
			{
				return this.Local.Index;
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06005A10 RID: 23056 RVA: 0x001B66B4 File Offset: 0x001B66B4
		// (set) Token: 0x06005A11 RID: 23057 RVA: 0x001B66C4 File Offset: 0x001B66C4
		public bool IsDebuggerHidden
		{
			get
			{
				return (this.Attributes & PdbLocalAttributes.DebuggerHidden) > PdbLocalAttributes.None;
			}
			set
			{
				if (value)
				{
					this.Attributes |= PdbLocalAttributes.DebuggerHidden;
					return;
				}
				this.Attributes &= ~PdbLocalAttributes.DebuggerHidden;
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06005A12 RID: 23058 RVA: 0x001B66EC File Offset: 0x001B66EC
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06005A13 RID: 23059 RVA: 0x001B66F0 File Offset: 0x001B66F0
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06005A14 RID: 23060 RVA: 0x001B6700 File Offset: 0x001B6700
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				return this.customDebugInfos;
			}
		}

		// Token: 0x04002B8F RID: 11151
		private readonly IList<PdbCustomDebugInfo> customDebugInfos = new List<PdbCustomDebugInfo>();
	}
}
