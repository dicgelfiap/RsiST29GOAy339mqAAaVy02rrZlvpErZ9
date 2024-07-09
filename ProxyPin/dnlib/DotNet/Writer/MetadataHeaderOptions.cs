using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008B7 RID: 2231
	[ComVisible(true)]
	public sealed class MetadataHeaderOptions
	{
		// Token: 0x06005627 RID: 22055 RVA: 0x001A586C File Offset: 0x001A586C
		public static MetadataHeaderOptions CreatePortablePdbV1_0()
		{
			return new MetadataHeaderOptions
			{
				Signature = new uint?(1112167234U),
				MajorVersion = new ushort?((ushort)1),
				MinorVersion = new ushort?((ushort)1),
				Reserved1 = new uint?(0U),
				VersionString = "PDB v1.0",
				StorageFlags = new StorageFlags?(dnlib.DotNet.MD.StorageFlags.Normal),
				Reserved2 = new byte?(0)
			};
		}

		// Token: 0x04002946 RID: 10566
		public const string DEFAULT_VERSION_STRING = "v2.0.50727";

		// Token: 0x04002947 RID: 10567
		public const uint DEFAULT_SIGNATURE = 1112167234U;

		// Token: 0x04002948 RID: 10568
		public uint? Signature;

		// Token: 0x04002949 RID: 10569
		public ushort? MajorVersion;

		// Token: 0x0400294A RID: 10570
		public ushort? MinorVersion;

		// Token: 0x0400294B RID: 10571
		public uint? Reserved1;

		// Token: 0x0400294C RID: 10572
		public string VersionString;

		// Token: 0x0400294D RID: 10573
		public StorageFlags? StorageFlags;

		// Token: 0x0400294E RID: 10574
		public byte? Reserved2;
	}
}
