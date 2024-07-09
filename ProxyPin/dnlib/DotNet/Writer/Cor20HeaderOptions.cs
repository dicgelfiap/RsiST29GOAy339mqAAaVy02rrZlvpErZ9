using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008A2 RID: 2210
	[ComVisible(true)]
	public sealed class Cor20HeaderOptions
	{
		// Token: 0x06005484 RID: 21636 RVA: 0x0019BD40 File Offset: 0x0019BD40
		public Cor20HeaderOptions()
		{
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0019BD48 File Offset: 0x0019BD48
		public Cor20HeaderOptions(ComImageFlags flags)
		{
			this.Flags = new ComImageFlags?(flags);
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0019BD5C File Offset: 0x0019BD5C
		public Cor20HeaderOptions(ushort major, ushort minor, ComImageFlags flags)
		{
			this.MajorRuntimeVersion = new ushort?(major);
			this.MinorRuntimeVersion = new ushort?(minor);
			this.Flags = new ComImageFlags?(flags);
		}

		// Token: 0x04002882 RID: 10370
		public const ushort DEFAULT_MAJOR_RT_VER = 2;

		// Token: 0x04002883 RID: 10371
		public const ushort DEFAULT_MINOR_RT_VER = 5;

		// Token: 0x04002884 RID: 10372
		public ushort? MajorRuntimeVersion;

		// Token: 0x04002885 RID: 10373
		public ushort? MinorRuntimeVersion;

		// Token: 0x04002886 RID: 10374
		public ComImageFlags? Flags;

		// Token: 0x04002887 RID: 10375
		public uint? EntryPoint;
	}
}
