using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008C9 RID: 2249
	[ComVisible(true)]
	public sealed class NativeModuleWriterOptions : ModuleWriterOptionsBase
	{
		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06005732 RID: 22322 RVA: 0x001A9AE8 File Offset: 0x001A9AE8
		// (set) Token: 0x06005733 RID: 22323 RVA: 0x001A9AF0 File Offset: 0x001A9AF0
		public bool KeepExtraPEData { get; set; }

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x001A9AFC File Offset: 0x001A9AFC
		// (set) Token: 0x06005735 RID: 22325 RVA: 0x001A9B04 File Offset: 0x001A9B04
		public bool KeepWin32Resources { get; set; }

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x001A9B10 File Offset: 0x001A9B10
		internal bool OptimizeImageSize { get; }

		// Token: 0x06005737 RID: 22327 RVA: 0x001A9B18 File Offset: 0x001A9B18
		public NativeModuleWriterOptions(ModuleDefMD module, bool optimizeImageSize) : base(module)
		{
			base.MetadataOptions.Flags |= MetadataFlags.PreserveAllMethodRids;
			if (optimizeImageSize)
			{
				this.OptimizeImageSize = 1;
				base.MetadataOptions.Flags |= (MetadataFlags.PreserveTypeRefRids | MetadataFlags.PreserveTypeDefRids | MetadataFlags.PreserveTypeSpecRids);
			}
		}
	}
}
