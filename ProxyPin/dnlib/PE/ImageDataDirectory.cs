using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x0200074A RID: 1866
	[DebuggerDisplay("{virtualAddress} {dataSize}")]
	[ComVisible(true)]
	public sealed class ImageDataDirectory : FileSection
	{
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06004145 RID: 16709 RVA: 0x00162C24 File Offset: 0x00162C24
		public RVA VirtualAddress
		{
			get
			{
				return this.virtualAddress;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x00162C2C File Offset: 0x00162C2C
		public uint Size
		{
			get
			{
				return this.dataSize;
			}
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x00162C34 File Offset: 0x00162C34
		public ImageDataDirectory()
		{
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x00162C3C File Offset: 0x00162C3C
		public ImageDataDirectory(ref DataReader reader, bool verify)
		{
			base.SetStartOffset(ref reader);
			this.virtualAddress = (RVA)reader.ReadUInt32();
			this.dataSize = reader.ReadUInt32();
			base.SetEndoffset(ref reader);
		}

		// Token: 0x040022BE RID: 8894
		private readonly RVA virtualAddress;

		// Token: 0x040022BF RID: 8895
		private readonly uint dataSize;
	}
}
