using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x0200074D RID: 1869
	[ComVisible(true)]
	public sealed class ImageDosHeader : FileSection
	{
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x00162D44 File Offset: 0x00162D44
		public uint NTHeadersOffset
		{
			get
			{
				return this.ntHeadersOffset;
			}
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x00162D4C File Offset: 0x00162D4C
		public ImageDosHeader(ref DataReader reader, bool verify)
		{
			base.SetStartOffset(ref reader);
			ushort num = reader.ReadUInt16();
			if (verify && num != 23117)
			{
				throw new BadImageFormatException("Invalid DOS signature");
			}
			reader.Position = (uint)(this.startOffset + 60U);
			this.ntHeadersOffset = reader.ReadUInt32();
			base.SetEndoffset(ref reader);
		}

		// Token: 0x040022DC RID: 8924
		private readonly uint ntHeadersOffset;
	}
}
