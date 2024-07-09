using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x0200074B RID: 1867
	[DebuggerDisplay("{type}: TS:{timeDateStamp,h} V:{majorVersion,d}.{minorVersion,d} SZ:{sizeOfData} RVA:{addressOfRawData,h} FO:{pointerToRawData,h}")]
	[ComVisible(true)]
	public sealed class ImageDebugDirectory : FileSection
	{
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x00162C7C File Offset: 0x00162C7C
		public uint Characteristics
		{
			get
			{
				return this.characteristics;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600414A RID: 16714 RVA: 0x00162C84 File Offset: 0x00162C84
		public uint TimeDateStamp
		{
			get
			{
				return this.timeDateStamp;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600414B RID: 16715 RVA: 0x00162C8C File Offset: 0x00162C8C
		public ushort MajorVersion
		{
			get
			{
				return this.majorVersion;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600414C RID: 16716 RVA: 0x00162C94 File Offset: 0x00162C94
		public ushort MinorVersion
		{
			get
			{
				return this.minorVersion;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x00162C9C File Offset: 0x00162C9C
		public ImageDebugType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x00162CA4 File Offset: 0x00162CA4
		public uint SizeOfData
		{
			get
			{
				return this.sizeOfData;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x00162CAC File Offset: 0x00162CAC
		public RVA AddressOfRawData
		{
			get
			{
				return (RVA)this.addressOfRawData;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004150 RID: 16720 RVA: 0x00162CB4 File Offset: 0x00162CB4
		public FileOffset PointerToRawData
		{
			get
			{
				return (FileOffset)this.pointerToRawData;
			}
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x00162CBC File Offset: 0x00162CBC
		public ImageDebugDirectory(ref DataReader reader, bool verify)
		{
			base.SetStartOffset(ref reader);
			this.characteristics = reader.ReadUInt32();
			this.timeDateStamp = reader.ReadUInt32();
			this.majorVersion = reader.ReadUInt16();
			this.minorVersion = reader.ReadUInt16();
			this.type = (ImageDebugType)reader.ReadUInt32();
			this.sizeOfData = reader.ReadUInt32();
			this.addressOfRawData = reader.ReadUInt32();
			this.pointerToRawData = reader.ReadUInt32();
			base.SetEndoffset(ref reader);
		}

		// Token: 0x040022C0 RID: 8896
		private readonly uint characteristics;

		// Token: 0x040022C1 RID: 8897
		private readonly uint timeDateStamp;

		// Token: 0x040022C2 RID: 8898
		private readonly ushort majorVersion;

		// Token: 0x040022C3 RID: 8899
		private readonly ushort minorVersion;

		// Token: 0x040022C4 RID: 8900
		private readonly ImageDebugType type;

		// Token: 0x040022C5 RID: 8901
		private readonly uint sizeOfData;

		// Token: 0x040022C6 RID: 8902
		private readonly uint addressOfRawData;

		// Token: 0x040022C7 RID: 8903
		private readonly uint pointerToRawData;
	}
}
