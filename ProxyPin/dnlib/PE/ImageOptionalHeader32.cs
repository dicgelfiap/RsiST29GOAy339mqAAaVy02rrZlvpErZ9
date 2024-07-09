using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x02000750 RID: 1872
	[ComVisible(true)]
	public sealed class ImageOptionalHeader32 : FileSection, IImageOptionalHeader, IFileSection
	{
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004161 RID: 16737 RVA: 0x00162F88 File Offset: 0x00162F88
		public ushort Magic
		{
			get
			{
				return this.magic;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x00162F90 File Offset: 0x00162F90
		public byte MajorLinkerVersion
		{
			get
			{
				return this.majorLinkerVersion;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x00162F98 File Offset: 0x00162F98
		public byte MinorLinkerVersion
		{
			get
			{
				return this.minorLinkerVersion;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x00162FA0 File Offset: 0x00162FA0
		public uint SizeOfCode
		{
			get
			{
				return this.sizeOfCode;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x00162FA8 File Offset: 0x00162FA8
		public uint SizeOfInitializedData
		{
			get
			{
				return this.sizeOfInitializedData;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06004166 RID: 16742 RVA: 0x00162FB0 File Offset: 0x00162FB0
		public uint SizeOfUninitializedData
		{
			get
			{
				return this.sizeOfUninitializedData;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06004167 RID: 16743 RVA: 0x00162FB8 File Offset: 0x00162FB8
		public RVA AddressOfEntryPoint
		{
			get
			{
				return this.addressOfEntryPoint;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x00162FC0 File Offset: 0x00162FC0
		public RVA BaseOfCode
		{
			get
			{
				return this.baseOfCode;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x00162FC8 File Offset: 0x00162FC8
		public RVA BaseOfData
		{
			get
			{
				return this.baseOfData;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x00162FD0 File Offset: 0x00162FD0
		public ulong ImageBase
		{
			get
			{
				return (ulong)this.imageBase;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x00162FDC File Offset: 0x00162FDC
		public uint SectionAlignment
		{
			get
			{
				return this.sectionAlignment;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600416C RID: 16748 RVA: 0x00162FE4 File Offset: 0x00162FE4
		public uint FileAlignment
		{
			get
			{
				return this.fileAlignment;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x00162FEC File Offset: 0x00162FEC
		public ushort MajorOperatingSystemVersion
		{
			get
			{
				return this.majorOperatingSystemVersion;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600416E RID: 16750 RVA: 0x00162FF4 File Offset: 0x00162FF4
		public ushort MinorOperatingSystemVersion
		{
			get
			{
				return this.minorOperatingSystemVersion;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x00162FFC File Offset: 0x00162FFC
		public ushort MajorImageVersion
		{
			get
			{
				return this.majorImageVersion;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06004170 RID: 16752 RVA: 0x00163004 File Offset: 0x00163004
		public ushort MinorImageVersion
		{
			get
			{
				return this.minorImageVersion;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x0016300C File Offset: 0x0016300C
		public ushort MajorSubsystemVersion
		{
			get
			{
				return this.majorSubsystemVersion;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06004172 RID: 16754 RVA: 0x00163014 File Offset: 0x00163014
		public ushort MinorSubsystemVersion
		{
			get
			{
				return this.minorSubsystemVersion;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x0016301C File Offset: 0x0016301C
		public uint Win32VersionValue
		{
			get
			{
				return this.win32VersionValue;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06004174 RID: 16756 RVA: 0x00163024 File Offset: 0x00163024
		public uint SizeOfImage
		{
			get
			{
				return this.sizeOfImage;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x0016302C File Offset: 0x0016302C
		public uint SizeOfHeaders
		{
			get
			{
				return this.sizeOfHeaders;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x00163034 File Offset: 0x00163034
		public uint CheckSum
		{
			get
			{
				return this.checkSum;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x0016303C File Offset: 0x0016303C
		public Subsystem Subsystem
		{
			get
			{
				return this.subsystem;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06004178 RID: 16760 RVA: 0x00163044 File Offset: 0x00163044
		public DllCharacteristics DllCharacteristics
		{
			get
			{
				return this.dllCharacteristics;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x0016304C File Offset: 0x0016304C
		public ulong SizeOfStackReserve
		{
			get
			{
				return (ulong)this.sizeOfStackReserve;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600417A RID: 16762 RVA: 0x00163058 File Offset: 0x00163058
		public ulong SizeOfStackCommit
		{
			get
			{
				return (ulong)this.sizeOfStackCommit;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x00163064 File Offset: 0x00163064
		public ulong SizeOfHeapReserve
		{
			get
			{
				return (ulong)this.sizeOfHeapReserve;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x00163070 File Offset: 0x00163070
		public ulong SizeOfHeapCommit
		{
			get
			{
				return (ulong)this.sizeOfHeapCommit;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x0016307C File Offset: 0x0016307C
		public uint LoaderFlags
		{
			get
			{
				return this.loaderFlags;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x00163084 File Offset: 0x00163084
		public uint NumberOfRvaAndSizes
		{
			get
			{
				return this.numberOfRvaAndSizes;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600417F RID: 16767 RVA: 0x0016308C File Offset: 0x0016308C
		public ImageDataDirectory[] DataDirectories
		{
			get
			{
				return this.dataDirectories;
			}
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x00163094 File Offset: 0x00163094
		public ImageOptionalHeader32(ref DataReader reader, uint totalSize, bool verify)
		{
			if (totalSize < 96U)
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
			this.baseOfData = (RVA)reader.ReadUInt32();
			this.imageBase = reader.ReadUInt32();
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
			this.sizeOfStackReserve = reader.ReadUInt32();
			this.sizeOfStackCommit = reader.ReadUInt32();
			this.sizeOfHeapReserve = reader.ReadUInt32();
			this.sizeOfHeapCommit = reader.ReadUInt32();
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

		// Token: 0x040022E7 RID: 8935
		private readonly ushort magic;

		// Token: 0x040022E8 RID: 8936
		private readonly byte majorLinkerVersion;

		// Token: 0x040022E9 RID: 8937
		private readonly byte minorLinkerVersion;

		// Token: 0x040022EA RID: 8938
		private readonly uint sizeOfCode;

		// Token: 0x040022EB RID: 8939
		private readonly uint sizeOfInitializedData;

		// Token: 0x040022EC RID: 8940
		private readonly uint sizeOfUninitializedData;

		// Token: 0x040022ED RID: 8941
		private readonly RVA addressOfEntryPoint;

		// Token: 0x040022EE RID: 8942
		private readonly RVA baseOfCode;

		// Token: 0x040022EF RID: 8943
		private readonly RVA baseOfData;

		// Token: 0x040022F0 RID: 8944
		private readonly uint imageBase;

		// Token: 0x040022F1 RID: 8945
		private readonly uint sectionAlignment;

		// Token: 0x040022F2 RID: 8946
		private readonly uint fileAlignment;

		// Token: 0x040022F3 RID: 8947
		private readonly ushort majorOperatingSystemVersion;

		// Token: 0x040022F4 RID: 8948
		private readonly ushort minorOperatingSystemVersion;

		// Token: 0x040022F5 RID: 8949
		private readonly ushort majorImageVersion;

		// Token: 0x040022F6 RID: 8950
		private readonly ushort minorImageVersion;

		// Token: 0x040022F7 RID: 8951
		private readonly ushort majorSubsystemVersion;

		// Token: 0x040022F8 RID: 8952
		private readonly ushort minorSubsystemVersion;

		// Token: 0x040022F9 RID: 8953
		private readonly uint win32VersionValue;

		// Token: 0x040022FA RID: 8954
		private readonly uint sizeOfImage;

		// Token: 0x040022FB RID: 8955
		private readonly uint sizeOfHeaders;

		// Token: 0x040022FC RID: 8956
		private readonly uint checkSum;

		// Token: 0x040022FD RID: 8957
		private readonly Subsystem subsystem;

		// Token: 0x040022FE RID: 8958
		private readonly DllCharacteristics dllCharacteristics;

		// Token: 0x040022FF RID: 8959
		private readonly uint sizeOfStackReserve;

		// Token: 0x04002300 RID: 8960
		private readonly uint sizeOfStackCommit;

		// Token: 0x04002301 RID: 8961
		private readonly uint sizeOfHeapReserve;

		// Token: 0x04002302 RID: 8962
		private readonly uint sizeOfHeapCommit;

		// Token: 0x04002303 RID: 8963
		private readonly uint loaderFlags;

		// Token: 0x04002304 RID: 8964
		private readonly uint numberOfRvaAndSizes;

		// Token: 0x04002305 RID: 8965
		private readonly ImageDataDirectory[] dataDirectories = new ImageDataDirectory[16];
	}
}
