using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x0200074F RID: 1871
	[ComVisible(true)]
	public sealed class ImageNTHeaders : FileSection
	{
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600415C RID: 16732 RVA: 0x00162E80 File Offset: 0x00162E80
		public uint Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600415D RID: 16733 RVA: 0x00162E88 File Offset: 0x00162E88
		public ImageFileHeader FileHeader
		{
			get
			{
				return this.imageFileHeader;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x00162E90 File Offset: 0x00162E90
		public IImageOptionalHeader OptionalHeader
		{
			get
			{
				return this.imageOptionalHeader;
			}
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x00162E98 File Offset: 0x00162E98
		public ImageNTHeaders(ref DataReader reader, bool verify)
		{
			base.SetStartOffset(ref reader);
			this.signature = reader.ReadUInt32();
			if (verify && (ushort)this.signature != 17744)
			{
				throw new BadImageFormatException("Invalid NT headers signature");
			}
			this.imageFileHeader = new ImageFileHeader(ref reader, verify);
			this.imageOptionalHeader = this.CreateImageOptionalHeader(ref reader, verify);
			base.SetEndoffset(ref reader);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x00162F08 File Offset: 0x00162F08
		private IImageOptionalHeader CreateImageOptionalHeader(ref DataReader reader, bool verify)
		{
			ushort num = reader.ReadUInt16();
			reader.Position -= 2U;
			IImageOptionalHeader result;
			if (num != 267)
			{
				if (num != 523)
				{
					throw new BadImageFormatException("Invalid optional header magic");
				}
				result = new ImageOptionalHeader64(ref reader, this.imageFileHeader.SizeOfOptionalHeader, verify);
			}
			else
			{
				result = new ImageOptionalHeader32(ref reader, this.imageFileHeader.SizeOfOptionalHeader, verify);
			}
			return result;
		}

		// Token: 0x040022E4 RID: 8932
		private readonly uint signature;

		// Token: 0x040022E5 RID: 8933
		private readonly ImageFileHeader imageFileHeader;

		// Token: 0x040022E6 RID: 8934
		private readonly IImageOptionalHeader imageOptionalHeader;
	}
}
