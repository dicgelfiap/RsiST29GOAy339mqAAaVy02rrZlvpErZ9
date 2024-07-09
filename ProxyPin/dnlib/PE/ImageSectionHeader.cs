using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x02000752 RID: 1874
	[DebuggerDisplay("RVA:{virtualAddress} VS:{virtualSize} FO:{pointerToRawData} FS:{sizeOfRawData} {displayName}")]
	[ComVisible(true)]
	public sealed class ImageSectionHeader : FileSection
	{
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x001635EC File Offset: 0x001635EC
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x001635F4 File Offset: 0x001635F4
		public byte[] Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x001635FC File Offset: 0x001635FC
		public uint VirtualSize
		{
			get
			{
				return this.virtualSize;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060041A4 RID: 16804 RVA: 0x00163604 File Offset: 0x00163604
		public RVA VirtualAddress
		{
			get
			{
				return this.virtualAddress;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x0016360C File Offset: 0x0016360C
		public uint SizeOfRawData
		{
			get
			{
				return this.sizeOfRawData;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x00163614 File Offset: 0x00163614
		public uint PointerToRawData
		{
			get
			{
				return this.pointerToRawData;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060041A7 RID: 16807 RVA: 0x0016361C File Offset: 0x0016361C
		public uint PointerToRelocations
		{
			get
			{
				return this.pointerToRelocations;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x00163624 File Offset: 0x00163624
		public uint PointerToLinenumbers
		{
			get
			{
				return this.pointerToLinenumbers;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x0016362C File Offset: 0x0016362C
		public ushort NumberOfRelocations
		{
			get
			{
				return this.numberOfRelocations;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x00163634 File Offset: 0x00163634
		public ushort NumberOfLinenumbers
		{
			get
			{
				return this.numberOfLinenumbers;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x0016363C File Offset: 0x0016363C
		public uint Characteristics
		{
			get
			{
				return this.characteristics;
			}
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x00163644 File Offset: 0x00163644
		public ImageSectionHeader(ref DataReader reader, bool verify)
		{
			base.SetStartOffset(ref reader);
			this.name = reader.ReadBytes(8);
			this.virtualSize = reader.ReadUInt32();
			this.virtualAddress = (RVA)reader.ReadUInt32();
			this.sizeOfRawData = reader.ReadUInt32();
			this.pointerToRawData = reader.ReadUInt32();
			this.pointerToRelocations = reader.ReadUInt32();
			this.pointerToLinenumbers = reader.ReadUInt32();
			this.numberOfRelocations = reader.ReadUInt16();
			this.numberOfLinenumbers = reader.ReadUInt16();
			this.characteristics = reader.ReadUInt32();
			base.SetEndoffset(ref reader);
			this.displayName = ImageSectionHeader.ToString(this.name);
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x001636F4 File Offset: 0x001636F4
		private static string ToString(byte[] name)
		{
			StringBuilder stringBuilder = new StringBuilder(name.Length);
			foreach (byte b in name)
			{
				if (b == 0)
				{
					break;
				}
				stringBuilder.Append((char)b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002324 RID: 8996
		private readonly string displayName;

		// Token: 0x04002325 RID: 8997
		private readonly byte[] name;

		// Token: 0x04002326 RID: 8998
		private readonly uint virtualSize;

		// Token: 0x04002327 RID: 8999
		private readonly RVA virtualAddress;

		// Token: 0x04002328 RID: 9000
		private readonly uint sizeOfRawData;

		// Token: 0x04002329 RID: 9001
		private readonly uint pointerToRawData;

		// Token: 0x0400232A RID: 9002
		private readonly uint pointerToRelocations;

		// Token: 0x0400232B RID: 9003
		private readonly uint pointerToLinenumbers;

		// Token: 0x0400232C RID: 9004
		private readonly ushort numberOfRelocations;

		// Token: 0x0400232D RID: 9005
		private readonly ushort numberOfLinenumbers;

		// Token: 0x0400232E RID: 9006
		private readonly uint characteristics;
	}
}
