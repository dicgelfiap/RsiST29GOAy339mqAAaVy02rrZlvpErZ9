using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using dnlib.IO;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008E4 RID: 2276
	[ComVisible(true)]
	public interface IResourceData : IFileSection
	{
		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x060058A9 RID: 22697
		ResourceTypeCode Code { get; }

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x060058AA RID: 22698
		// (set) Token: 0x060058AB RID: 22699
		FileOffset StartOffset { get; set; }

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x060058AC RID: 22700
		// (set) Token: 0x060058AD RID: 22701
		FileOffset EndOffset { get; set; }

		// Token: 0x060058AE RID: 22702
		void WriteData(BinaryWriter writer, IFormatter formatter);
	}
}
