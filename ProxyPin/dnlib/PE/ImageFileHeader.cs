using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.PE
{
	// Token: 0x0200074E RID: 1870
	[ComVisible(true)]
	public sealed class ImageFileHeader : FileSection
	{
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06004154 RID: 16724 RVA: 0x00162DB0 File Offset: 0x00162DB0
		public Machine Machine
		{
			get
			{
				return this.machine;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x00162DB8 File Offset: 0x00162DB8
		public int NumberOfSections
		{
			get
			{
				return (int)this.numberOfSections;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004156 RID: 16726 RVA: 0x00162DC0 File Offset: 0x00162DC0
		public uint TimeDateStamp
		{
			get
			{
				return this.timeDateStamp;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x00162DC8 File Offset: 0x00162DC8
		public uint PointerToSymbolTable
		{
			get
			{
				return this.pointerToSymbolTable;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x00162DD0 File Offset: 0x00162DD0
		public uint NumberOfSymbols
		{
			get
			{
				return this.numberOfSymbols;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x00162DD8 File Offset: 0x00162DD8
		public uint SizeOfOptionalHeader
		{
			get
			{
				return (uint)this.sizeOfOptionalHeader;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x00162DE0 File Offset: 0x00162DE0
		public Characteristics Characteristics
		{
			get
			{
				return this.characteristics;
			}
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x00162DE8 File Offset: 0x00162DE8
		public ImageFileHeader(ref DataReader reader, bool verify)
		{
			base.SetStartOffset(ref reader);
			this.machine = (Machine)reader.ReadUInt16();
			this.numberOfSections = reader.ReadUInt16();
			this.timeDateStamp = reader.ReadUInt32();
			this.pointerToSymbolTable = reader.ReadUInt32();
			this.numberOfSymbols = reader.ReadUInt32();
			this.sizeOfOptionalHeader = reader.ReadUInt16();
			this.characteristics = (Characteristics)reader.ReadUInt16();
			base.SetEndoffset(ref reader);
			if (verify && this.sizeOfOptionalHeader == 0)
			{
				throw new BadImageFormatException("Invalid SizeOfOptionalHeader");
			}
		}

		// Token: 0x040022DD RID: 8925
		private readonly Machine machine;

		// Token: 0x040022DE RID: 8926
		private readonly ushort numberOfSections;

		// Token: 0x040022DF RID: 8927
		private readonly uint timeDateStamp;

		// Token: 0x040022E0 RID: 8928
		private readonly uint pointerToSymbolTable;

		// Token: 0x040022E1 RID: 8929
		private readonly uint numberOfSymbols;

		// Token: 0x040022E2 RID: 8930
		private readonly ushort sizeOfOptionalHeader;

		// Token: 0x040022E3 RID: 8931
		private readonly Characteristics characteristics;
	}
}
