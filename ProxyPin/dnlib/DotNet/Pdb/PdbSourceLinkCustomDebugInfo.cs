using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x02000908 RID: 2312
	[ComVisible(true)]
	public sealed class PdbSourceLinkCustomDebugInfo : PdbCustomDebugInfo
	{
		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x0600597B RID: 22907 RVA: 0x001B5D88 File Offset: 0x001B5D88
		public override PdbCustomDebugInfoKind Kind
		{
			get
			{
				return PdbCustomDebugInfoKind.SourceLink;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x0600597C RID: 22908 RVA: 0x001B5D90 File Offset: 0x001B5D90
		public override Guid Guid
		{
			get
			{
				return CustomDebugInfoGuids.SourceLink;
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x0600597D RID: 22909 RVA: 0x001B5D98 File Offset: 0x001B5D98
		// (set) Token: 0x0600597E RID: 22910 RVA: 0x001B5DA0 File Offset: 0x001B5DA0
		public byte[] FileBlob { get; set; }

		// Token: 0x0600597F RID: 22911 RVA: 0x001B5DAC File Offset: 0x001B5DAC
		public PdbSourceLinkCustomDebugInfo()
		{
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x001B5DB4 File Offset: 0x001B5DB4
		public PdbSourceLinkCustomDebugInfo(byte[] fileBlob)
		{
			this.FileBlob = fileBlob;
		}
	}
}
