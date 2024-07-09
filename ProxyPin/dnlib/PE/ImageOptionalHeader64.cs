using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x02000751 RID: 1873
	[ComVisible(true)]
	public sealed class ImageOptionalHeader64 : FileSection, IImageOptionalHeader, IFileSection
	{
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06004181 RID: 16769 RVA: 0x001632CC File Offset: 0x001632CC
		public ushort Magic
		{
			get
			{
				return this.magic;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x001632D4 File Offset: 0x001632D4
		public byte MajorLinkerVersion
		{
			get
			{
				return this.majorLinkerVersion;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x001632DC File Offset: 0x001632DC
		public byte MinorLinkerVersion
		{
			get
			{
				return this.minorLinkerVersion;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x001632E4 File Offset: 0x001632E4
		public uint SizeOfCode
		{
			get
			{
				return this.sizeOfCode;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x001632EC File Offset: 0x001632EC
		public uint SizeOfInitializedData
		{
			get
			{
				return this.sizeOfInitializedData;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x001632F4 File Offset: 0x001632F4
		public uint SizeOfUninitializedData
		{
			get
			{
				return this.sizeOfUninitializedData;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x001632FC File Offset: 0x001632FC
		public RVA AddressOfEntryPoint
		{
			get
			{
				return this.addressOfEntryPoint;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06004188 RID: 16776 RVA: 0x00163304 File Offset: 0x00163304
		public RVA BaseOfCode
		{
			get
			{
				return this.baseOfCode;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x0016330C File Offset: 0x0016330C
		public RVA BaseOfData
		{
			get
			{
				return (RVA)0U;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600418A RID: 16778 RVA: 0x00163310 File Offset: 0x00163310
		public ulong ImageBase
		{
			get
			{
				return this.imageBase;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x00163318 File Offset: 0x00163318
		public uint SectionAlignment
		{
			get
			{
				return this.sectionAlignment;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600418C RID: 16780 RVA: 0x00163320 File Offset: 0x00163320
		public uint FileAlignment
		{
			get
			{
				return this.fileAlignment;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x00163328 File Offset: 0x00163328
		public ushort MajorOperatingSystemVersion
		{
			get
			{
				return this.majorOperatingSystemVersion;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600418E RID: 16782 RVA: 0x00163330 File Offset: 0x00163330
		public ushort MinorOperatingSystemVersion
		{
			get
			{
				return this.minorOperatingSystemVersion;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x00163338 File Offset: 0x00163338
		public ushort MajorImageVersion
		{
			get
			{
				return this.majorImageVersion;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x00163340 File Offset: 0x00163340
		public ushort MinorImageVersion
		{
			get
			{
				return this.minorImageVersion;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x00163348 File Offset: 0x00163348
		public ushort MajorSubsystemVersion
		{
			get
			{
				return this.majorSubsystemVersion;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x00163350 File Offset: 0x00163350
		public ushort MinorSubsystemVersion
		{
			get
			{
				return this.minorSubsystemVersion;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x00163358 File Offset: 0x00163358
		public uint Win32VersionValue
		{
			get
			{
				return this.win32VersionValue;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x00163360 File Offset: 0x00163360
		public uint SizeOfImage
		{
			get
			{
				return this.sizeOfImage;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x00163368 File Offset: 0x00163368
		public uint SizeOfHeaders
		{
			get
			{
				return this.sizeOfHeaders;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x00163370 File Offset: 0x00163370
		public uint CheckSum
		{
			get
			{
				return this.checkSum;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x00163378 File Offset: 0x00163378
		public Subsystem Subsystem
		{
			get
			{
				return this.subsystem;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06004198 RID: 16792 RVA: 0x00163380 File Offset: 0x00163380
		public DllCharacteristics DllCharacteristics
		{
			get
			{
				return this.dllCharacteristics;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06004199 RID: 16793 RVA: 0x00163388 File Offset: 0x00163388
		public ulong SizeOfStackReserve
		{
			get
			{
				return this.sizeOfStackReserve;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600419A RID: 16794 RVA: 0x00163390 File Offset: 0x00163390
		public ulong SizeOfStackCommit
		{
			get
			{
				return this.sizeOfStackCommit;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600419B RID: 16795 RVA: 0x00163398 File Offset: 0x00163398
		public ulong SizeOfHeapReserve
		{
			get
			{
				return this.sizeOfHeapReserve;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x001633A0 File Offset: 0x001633A0
		public ulong SizeOfHeapCommit
		{
			get
			{
				return this.sizeOfHeapCommit;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x001633A8 File Offset: 0x001633A8
		public uint LoaderFlags
		{
			get
			{
				return this.loaderFlags;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x001633B0 File Offset: 0x001633B0
		public uint NumberOfRvaAndSizes
		{
			get
			{
				return this.numberOfRvaAndSizes;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x001633B8 File Offset: 0x001633B8
		public ImageDataDirectory[] DataDirectories
		{
			get
			{
				return this.dataDirectories;
			}
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x001633C0 File Offset: 0x001633C0
		public ImageOptionalHeader64(ref DataReader reader, uint totalSize, bool verify)
		{
			if (totalSize < 112U)
			{
				throw new BadImageFormatException("Invalid optional header size");
			}
			if (verify && (ulong)reader.Position + (ulong)totalSize > (ulong)reader.Length)
			{
				throw new BadImageFormatException("Invalid optional header size");
			}
			base.SetStartOffset(ref reader);
			this.magic = reader.ReadUInt16();
			this.majorLinkerVersion = reader.ReadByte();
			this.minorLinkerVersion = reader.ReadByte();
			this.sizeOfCode = reader.ReadUInt32();
			this.sizeOfInitializedData = reader.ReadUInt32();
			this.sizeOfUninitializedData = reader.ReadUInt32();
			this.addressOfEntryPoint = (RVA)reader.ReadUInt32();
			this.baseOfCode = (RVA)reader.ReadUInt32();
			this.imageBase = reader.ReadUInt64();
			this.sectionAlignment = reader.ReadUInt32();
			this.fileAlignment = reader.ReadUInt32();
			this.majorOperatingSystemVersion = reader.ReadUInt16();
			this.minorOperatingSystemVersion = reader.ReadUInt16();
			this.majorImageVersion = reader.ReadUInt16();
			this.minorImageVersion = reader.ReadUInt16();
			this.majorSubsystemVersion = reader.ReadUInt16();
			this.minorSubsystemVersion = reader.ReadUInt16();
			this.win32VersionValue = reader.ReadUInt32();
			this.sizeOfImage = reader.ReadUInt32();
			this.sizeOfHeaders = reader.ReadUInt32();
			this.checkSum = reader.ReadUInt32();
			this.subsystem = (Subsystem)reader.ReadUInt16();
			this.dllCharacteristics = (DllCharacteristics)reader.ReadUInt16();
			this.sizeOfStackReserve = reader.ReadUInt64();
			this.sizeOfStackCommit = reader.ReadUInt64();
			this.sizeOfHeapReserve = reader.ReadUInt64();
			this.sizeOfHeapCommit = reader.ReadUInt64();
			this.loaderFlags = reader.ReadUInt32();
			this.numberOfRvaAndSizes = reader.ReadUInt32();
			for (int i = 0; i < this.dataDirectories.Length; i++)
			{
				if (reader.Position - (uint)this.startOffset + 8U <= totalSize)
				{
					this.dataDirectories[i] = new ImageDataDirectory(ref reader, verify);
				}
				else
				{
					this.dataDirectories[i] = new ImageDataDirectory();
				}
			}
			reader.Position = (uint)(this.startOffset + totalSize);
			base.SetEndoffset(ref reader);
		}

		// Token: 0x04002306 RID: 8966
		private readonly ushort magic;

		// Token: 0x04002307 RID: 8967
		private readonly byte majorLinkerVersion;

		// Token: 0x04002308 RID: 8968
		private readonly byte minorLinkerVersion;

		// Token: 0x04002309 RID: 8969
		private readonly uint sizeOfCode;

		// Token: 0x0400230A RID: 8970
		private readonly uint sizeOfInitializedData;

		// Token: 0x0400230B RID: 8971
		private readonly uint sizeOfUninitializedData;

		// Token: 0x0400230C RID: 8972
		private readonly RVA addressOfEntryPoint;

		// Token: 0x0400230D RID: 8973
		private readonly RVA baseOfCode;

		// Token: 0x0400230E RID: 8974
		private readonly ulong imageBase;

		// Token: 0x0400230F RID: 8975
		private readonly uint sectionAlignment;

		// Token: 0x04002310 RID: 8976
		private readonly uint fileAlignment;

		// Token: 0x04002311 RID: 8977
		private readonly ushort majorOperatingSystemVersion;

		// Token: 0x04002312 RID: 8978
		private readonly ushort minorOperatingSystemVersion;

		// Token: 0x04002313 RID: 8979
		private readonly ushort majorImageVersion;

		// Token: 0x04002314 RID: 8980
		private readonly ushort minorImageVersion;

		// Token: 0x04002315 RID: 8981
		private readonly ushort majorSubsystemVersion;

		// Token: 0x04002316 RID: 8982
		private readonly ushort minorSubsystemVersion;

		// Token: 0x04002317 RID: 8983
		private readonly uint win32VersionValue;

		// Token: 0x04002318 RID: 8984
		private readonly uint sizeOfImage;

		// Token: 0x04002319 RID: 8985
		private readonly uint sizeOfHeaders;

		// Token: 0x0400231A RID: 8986
		private readonly uint checkSum;

		// Token: 0x0400231B RID: 8987
		private readonly Subsystem subsystem;

		// Token: 0x0400231C RID: 8988
		private readonly DllCharacteristics dllCharacteristics;

		// Token: 0x0400231D RID: 8989
		private readonly ulong sizeOfStackReserve;

		// Token: 0x0400231E RID: 8990
		private readonly ulong sizeOfStackCommit;

		// Token: 0x0400231F RID: 8991
		private readonly ulong sizeOfHeapReserve;

		// Token: 0x04002320 RID: 8992
		private readonly ulong sizeOfHeapCommit;

		// Token: 0x04002321 RID: 8993
		private readonly uint loaderFlags;

		// Token: 0x04002322 RID: 8994
		private readonly uint numberOfRvaAndSizes;

		// Token: 0x04002323 RID: 8995
		private readonly ImageDataDirectory[] dataDirectories = new ImageDataDirectory[16];
	}
}
