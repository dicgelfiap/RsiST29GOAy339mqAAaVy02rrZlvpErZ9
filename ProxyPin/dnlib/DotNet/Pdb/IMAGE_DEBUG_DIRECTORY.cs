using System;
using System.Runtime.InteropServices;
using dnlib.PE;

namespace dnlib.DotNet.Pdb
{
	// Token: 0x020008F2 RID: 2290
	[ComVisible(true)]
	public struct IMAGE_DEBUG_DIRECTORY
	{
		// Token: 0x04002B19 RID: 11033
		public uint Characteristics;

		// Token: 0x04002B1A RID: 11034
		public uint TimeDateStamp;

		// Token: 0x04002B1B RID: 11035
		public ushort MajorVersion;

		// Token: 0x04002B1C RID: 11036
		public ushort MinorVersion;

		// Token: 0x04002B1D RID: 11037
		public ImageDebugType Type;

		// Token: 0x04002B1E RID: 11038
		public uint SizeOfData;

		// Token: 0x04002B1F RID: 11039
		public uint AddressOfRawData;

		// Token: 0x04002B20 RID: 11040
		public uint PointerToRawData;
	}
}
