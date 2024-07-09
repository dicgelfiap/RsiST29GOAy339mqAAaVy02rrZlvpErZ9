using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200098F RID: 2447
	[ComVisible(true)]
	public sealed class ImageCor20Header : FileSection
	{
		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06005E2E RID: 24110 RVA: 0x001C5544 File Offset: 0x001C5544
		public bool HasNativeHeader
		{
			get
			{
				return (this.flags & ComImageFlags.ILLibrary) > (ComImageFlags)0U;
			}
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x001C5554 File Offset: 0x001C5554
		public uint CB
		{
			get
			{
				return this.cb;
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06005E30 RID: 24112 RVA: 0x001C555C File Offset: 0x001C555C
		public ushort MajorRuntimeVersion
		{
			get
			{
				return this.majorRuntimeVersion;
			}
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06005E31 RID: 24113 RVA: 0x001C5564 File Offset: 0x001C5564
		public ushort MinorRuntimeVersion
		{
			get
			{
				return this.minorRuntimeVersion;
			}
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06005E32 RID: 24114 RVA: 0x001C556C File Offset: 0x001C556C
		public ImageDataDirectory Metadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x001C5574 File Offset: 0x001C5574
		public ComImageFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06005E34 RID: 24116 RVA: 0x001C557C File Offset: 0x001C557C
		public uint EntryPointToken_or_RVA
		{
			get
			{
				return this.entryPointToken_or_RVA;
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x001C5584 File Offset: 0x001C5584
		public ImageDataDirectory Resources
		{
			get
			{
				return this.resources;
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06005E36 RID: 24118 RVA: 0x001C558C File Offset: 0x001C558C
		public ImageDataDirectory StrongNameSignature
		{
			get
			{
				return this.strongNameSignature;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x001C5594 File Offset: 0x001C5594
		public ImageDataDirectory CodeManagerTable
		{
			get
			{
				return this.codeManagerTable;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06005E38 RID: 24120 RVA: 0x001C559C File Offset: 0x001C559C
		public ImageDataDirectory VTableFixups
		{
			get
			{
				return this.vtableFixups;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06005E39 RID: 24121 RVA: 0x001C55A4 File Offset: 0x001C55A4
		public ImageDataDirectory ExportAddressTableJumps
		{
			get
			{
				return this.exportAddressTableJumps;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06005E3A RID: 24122 RVA: 0x001C55AC File Offset: 0x001C55AC
		public ImageDataDirectory ManagedNativeHeader
		{
			get
			{
				return this.managedNativeHeader;
			}
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x001C55B4 File Offset: 0x001C55B4
		public ImageCor20Header(ref DataReader reader, bool verify)
		{
			base.SetStartOffset(ref reader);
			this.cb = reader.ReadUInt32();
			if (verify && this.cb < 72U)
			{
				throw new BadImageFormatException("Invalid IMAGE_COR20_HEADER.cb value");
			}
			this.majorRuntimeVersion = reader.ReadUInt16();
			this.minorRuntimeVersion = reader.ReadUInt16();
			this.metadata = new ImageDataDirectory(ref reader, verify);
			this.flags = (ComImageFlags)reader.ReadUInt32();
			this.entryPointToken_or_RVA = reader.ReadUInt32();
			this.resources = new ImageDataDirectory(ref reader, verify);
			this.strongNameSignature = new ImageDataDirectory(ref reader, verify);
			this.codeManagerTable = new ImageDataDirectory(ref reader, verify);
			this.vtableFixups = new ImageDataDirectory(ref reader, verify);
			this.exportAddressTableJumps = new ImageDataDirectory(ref reader, verify);
			this.managedNativeHeader = new ImageDataDirectory(ref reader, verify);
			base.SetEndoffset(ref reader);
		}

		// Token: 0x04002E00 RID: 11776
		private readonly uint cb;

		// Token: 0x04002E01 RID: 11777
		private readonly ushort majorRuntimeVersion;

		// Token: 0x04002E02 RID: 11778
		private readonly ushort minorRuntimeVersion;

		// Token: 0x04002E03 RID: 11779
		private readonly ImageDataDirectory metadata;

		// Token: 0x04002E04 RID: 11780
		private readonly ComImageFlags flags;

		// Token: 0x04002E05 RID: 11781
		private readonly uint entryPointToken_or_RVA;

		// Token: 0x04002E06 RID: 11782
		private readonly ImageDataDirectory resources;

		// Token: 0x04002E07 RID: 11783
		private readonly ImageDataDirectory strongNameSignature;

		// Token: 0x04002E08 RID: 11784
		private readonly ImageDataDirectory codeManagerTable;

		// Token: 0x04002E09 RID: 11785
		private readonly ImageDataDirectory vtableFixups;

		// Token: 0x04002E0A RID: 11786
		private readonly ImageDataDirectory exportAddressTableJumps;

		// Token: 0x04002E0B RID: 11787
		private readonly ImageDataDirectory managedNativeHeader;
	}
}
