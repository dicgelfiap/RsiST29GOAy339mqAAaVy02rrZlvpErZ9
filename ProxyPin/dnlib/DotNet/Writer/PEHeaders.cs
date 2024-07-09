using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008CF RID: 2255
	[ComVisible(true)]
	public sealed class PEHeaders : IChunk
	{
		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x001AC9B8 File Offset: 0x001AC9B8
		// (set) Token: 0x06005795 RID: 22421 RVA: 0x001AC9C0 File Offset: 0x001AC9C0
		public StartupStub StartupStub { get; set; }

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06005796 RID: 22422 RVA: 0x001AC9CC File Offset: 0x001AC9CC
		// (set) Token: 0x06005797 RID: 22423 RVA: 0x001AC9D4 File Offset: 0x001AC9D4
		public ImageCor20Header ImageCor20Header { get; set; }

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x001AC9E0 File Offset: 0x001AC9E0
		// (set) Token: 0x06005799 RID: 22425 RVA: 0x001AC9E8 File Offset: 0x001AC9E8
		public ImportAddressTable ImportAddressTable { get; set; }

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x0600579A RID: 22426 RVA: 0x001AC9F4 File Offset: 0x001AC9F4
		// (set) Token: 0x0600579B RID: 22427 RVA: 0x001AC9FC File Offset: 0x001AC9FC
		public ImportDirectory ImportDirectory { get; set; }

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x0600579C RID: 22428 RVA: 0x001ACA08 File Offset: 0x001ACA08
		// (set) Token: 0x0600579D RID: 22429 RVA: 0x001ACA10 File Offset: 0x001ACA10
		public Win32ResourcesChunk Win32Resources { get; set; }

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x0600579E RID: 22430 RVA: 0x001ACA1C File Offset: 0x001ACA1C
		// (set) Token: 0x0600579F RID: 22431 RVA: 0x001ACA24 File Offset: 0x001ACA24
		public RelocDirectory RelocDirectory { get; set; }

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x060057A0 RID: 22432 RVA: 0x001ACA30 File Offset: 0x001ACA30
		// (set) Token: 0x060057A1 RID: 22433 RVA: 0x001ACA38 File Offset: 0x001ACA38
		public DebugDirectory DebugDirectory { get; set; }

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x001ACA44 File Offset: 0x001ACA44
		// (set) Token: 0x060057A3 RID: 22435 RVA: 0x001ACA4C File Offset: 0x001ACA4C
		internal IChunk ExportDirectory { get; set; }

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x060057A4 RID: 22436 RVA: 0x001ACA58 File Offset: 0x001ACA58
		public ulong ImageBase
		{
			get
			{
				return this.imageBase;
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x060057A5 RID: 22437 RVA: 0x001ACA60 File Offset: 0x001ACA60
		// (set) Token: 0x060057A6 RID: 22438 RVA: 0x001ACA68 File Offset: 0x001ACA68
		public bool IsExeFile
		{
			get
			{
				return this.isExeFile;
			}
			set
			{
				this.isExeFile = value;
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x060057A7 RID: 22439 RVA: 0x001ACA74 File Offset: 0x001ACA74
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x060057A8 RID: 22440 RVA: 0x001ACA7C File Offset: 0x001ACA7C
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x060057A9 RID: 22441 RVA: 0x001ACA84 File Offset: 0x001ACA84
		public uint SectionAlignment
		{
			get
			{
				return this.sectionAlignment;
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x060057AA RID: 22442 RVA: 0x001ACA8C File Offset: 0x001ACA8C
		public uint FileAlignment
		{
			get
			{
				return this.fileAlignment;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x060057AB RID: 22443 RVA: 0x001ACA94 File Offset: 0x001ACA94
		// (set) Token: 0x060057AC RID: 22444 RVA: 0x001ACA9C File Offset: 0x001ACA9C
		public IList<PESection> PESections
		{
			get
			{
				return this.sections;
			}
			set
			{
				this.sections = value;
			}
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x001ACAA8 File Offset: 0x001ACAA8
		public PEHeaders() : this(new PEHeadersOptions())
		{
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x001ACAB8 File Offset: 0x001ACAB8
		public PEHeaders(PEHeadersOptions options)
		{
			this.options = (options ?? new PEHeadersOptions());
			this.sectionAlignment = (this.options.SectionAlignment ?? 8192U);
			this.fileAlignment = (this.options.FileAlignment ?? 512U);
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x001ACB40 File Offset: 0x001ACB40
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
			this.length = (uint)PEHeaders.dosHeader.Length;
			this.length += 24U;
			this.length += (this.Use32BitOptionalHeader() ? 224U : 240U);
			this.length += (uint)(this.sections.Count * 40);
			if (this.Use32BitOptionalHeader())
			{
				this.imageBase = (this.options.ImageBase ?? (this.IsExeFile ? 4194304UL : 268435456UL));
				return;
			}
			this.imageBase = (this.options.ImageBase ?? (this.IsExeFile ? 5368709120UL : 6442450944UL));
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x060057B0 RID: 22448 RVA: 0x001ACC5C File Offset: 0x001ACC5C
		private int SectionsCount
		{
			get
			{
				int num = 0;
				using (IEnumerator<PESection> enumerator = this.sections.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.GetVirtualSize() != 0U)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x001ACCBC File Offset: 0x001ACCBC
		public uint GetFileLength()
		{
			return this.length;
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x001ACCC4 File Offset: 0x001ACCC4
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x001ACCCC File Offset: 0x001ACCCC
		private IEnumerable<SectionSizeInfo> GetSectionSizeInfos()
		{
			foreach (PESection pesection in this.sections)
			{
				uint virtualSize = pesection.GetVirtualSize();
				if (virtualSize != 0U)
				{
					yield return new SectionSizeInfo(virtualSize, pesection.Characteristics);
				}
			}
			IEnumerator<PESection> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x001ACCDC File Offset: 0x001ACCDC
		public void WriteTo(DataWriter writer)
		{
			this.startOffset = writer.Position;
			writer.WriteBytes(PEHeaders.dosHeader);
			writer.WriteInt32(17744);
			writer.WriteUInt16((ushort)this.GetMachine());
			writer.WriteUInt16((ushort)this.SectionsCount);
			writer.WriteUInt32(this.options.TimeDateStamp ?? PEHeadersOptions.CreateNewTimeDateStamp());
			writer.WriteUInt32(this.options.PointerToSymbolTable.GetValueOrDefault());
			writer.WriteUInt32(this.options.NumberOfSymbols.GetValueOrDefault());
			writer.WriteUInt16(this.Use32BitOptionalHeader() ? 224 : 240);
			writer.WriteUInt16((ushort)this.GetCharacteristics());
			SectionSizes sectionSizes = new SectionSizes(this.fileAlignment, this.sectionAlignment, this.length, () => this.GetSectionSizeInfos());
			uint value = (uint)((this.StartupStub == null || !this.StartupStub.Enable) ? ((RVA)0U) : this.StartupStub.EntryPointRVA);
			if (this.Use32BitOptionalHeader())
			{
				writer.WriteUInt16(267);
				writer.WriteByte(this.options.MajorLinkerVersion ?? 11);
				writer.WriteByte(this.options.MinorLinkerVersion.GetValueOrDefault());
				writer.WriteUInt32(sectionSizes.SizeOfCode);
				writer.WriteUInt32(sectionSizes.SizeOfInitdData);
				writer.WriteUInt32(sectionSizes.SizeOfUninitdData);
				writer.WriteUInt32(value);
				writer.WriteUInt32(sectionSizes.BaseOfCode);
				writer.WriteUInt32(sectionSizes.BaseOfData);
				writer.WriteUInt32((uint)this.imageBase);
				writer.WriteUInt32(this.sectionAlignment);
				writer.WriteUInt32(this.fileAlignment);
				writer.WriteUInt16(this.options.MajorOperatingSystemVersion ?? 4);
				writer.WriteUInt16(this.options.MinorOperatingSystemVersion.GetValueOrDefault());
				writer.WriteUInt16(this.options.MajorImageVersion.GetValueOrDefault());
				writer.WriteUInt16(this.options.MinorImageVersion.GetValueOrDefault());
				writer.WriteUInt16(this.options.MajorSubsystemVersion ?? 4);
				writer.WriteUInt16(this.options.MinorSubsystemVersion.GetValueOrDefault());
				writer.WriteUInt32(this.options.Win32VersionValue.GetValueOrDefault());
				writer.WriteUInt32(sectionSizes.SizeOfImage);
				writer.WriteUInt32(sectionSizes.SizeOfHeaders);
				this.checkSumOffset = writer.Position;
				writer.WriteInt32(0);
				writer.WriteUInt16((ushort)(this.options.Subsystem ?? Subsystem.WindowsGui));
				writer.WriteUInt16((ushort)(this.options.DllCharacteristics ?? (DllCharacteristics.DynamicBase | DllCharacteristics.NxCompat | DllCharacteristics.NoSeh | DllCharacteristics.TerminalServerAware)));
				writer.WriteUInt32((uint)(this.options.SizeOfStackReserve ?? 1048576UL));
				writer.WriteUInt32((uint)(this.options.SizeOfStackCommit ?? 4096UL));
				writer.WriteUInt32((uint)(this.options.SizeOfHeapReserve ?? 1048576UL));
				writer.WriteUInt32((uint)(this.options.SizeOfHeapCommit ?? 4096UL));
				writer.WriteUInt32(this.options.LoaderFlags.GetValueOrDefault());
				writer.WriteUInt32(this.options.NumberOfRvaAndSizes ?? 16U);
			}
			else
			{
				writer.WriteUInt16(523);
				writer.WriteByte(this.options.MajorLinkerVersion ?? 11);
				writer.WriteByte(this.options.MinorLinkerVersion.GetValueOrDefault());
				writer.WriteUInt32(sectionSizes.SizeOfCode);
				writer.WriteUInt32(sectionSizes.SizeOfInitdData);
				writer.WriteUInt32(sectionSizes.SizeOfUninitdData);
				writer.WriteUInt32(value);
				writer.WriteUInt32(sectionSizes.BaseOfCode);
				writer.WriteUInt64(this.imageBase);
				writer.WriteUInt32(this.sectionAlignment);
				writer.WriteUInt32(this.fileAlignment);
				writer.WriteUInt16(this.options.MajorOperatingSystemVersion ?? 4);
				writer.WriteUInt16(this.options.MinorOperatingSystemVersion.GetValueOrDefault());
				writer.WriteUInt16(this.options.MajorImageVersion.GetValueOrDefault());
				writer.WriteUInt16(this.options.MinorImageVersion.GetValueOrDefault());
				writer.WriteUInt16(this.options.MajorSubsystemVersion ?? 4);
				writer.WriteUInt16(this.options.MinorSubsystemVersion.GetValueOrDefault());
				writer.WriteUInt32(this.options.Win32VersionValue.GetValueOrDefault());
				writer.WriteUInt32(sectionSizes.SizeOfImage);
				writer.WriteUInt32(sectionSizes.SizeOfHeaders);
				this.checkSumOffset = writer.Position;
				writer.WriteInt32(0);
				writer.WriteUInt16((ushort)(this.options.Subsystem ?? Subsystem.WindowsGui));
				writer.WriteUInt16((ushort)(this.options.DllCharacteristics ?? (DllCharacteristics.DynamicBase | DllCharacteristics.NxCompat | DllCharacteristics.NoSeh | DllCharacteristics.TerminalServerAware)));
				writer.WriteUInt64(this.options.SizeOfStackReserve ?? 4194304UL);
				writer.WriteUInt64(this.options.SizeOfStackCommit ?? 16384UL);
				writer.WriteUInt64(this.options.SizeOfHeapReserve ?? 1048576UL);
				writer.WriteUInt64(this.options.SizeOfHeapCommit ?? 8192UL);
				writer.WriteUInt32(this.options.LoaderFlags.GetValueOrDefault());
				writer.WriteUInt32(this.options.NumberOfRvaAndSizes ?? 16U);
			}
			writer.WriteDataDirectory(this.ExportDirectory);
			writer.WriteDataDirectory(this.ImportDirectory);
			writer.WriteDataDirectory(this.Win32Resources);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(this.RelocDirectory);
			writer.WriteDebugDirectory(this.DebugDirectory);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(this.ImportAddressTable);
			writer.WriteDataDirectory(null);
			writer.WriteDataDirectory(this.ImageCor20Header);
			writer.WriteDataDirectory(null);
			uint num = Utils.AlignUp(sectionSizes.SizeOfHeaders, this.sectionAlignment);
			int num2 = 0;
			foreach (PESection pesection in this.sections)
			{
				if (pesection.GetVirtualSize() != 0U)
				{
					num += pesection.WriteHeaderTo(writer, this.fileAlignment, this.sectionAlignment, num);
				}
				else
				{
					num2++;
				}
			}
			if (num2 != 0)
			{
				writer.Position += (long)(num2 * 40);
			}
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x001AD554 File Offset: 0x001AD554
		public void WriteCheckSum(DataWriter writer, long length)
		{
			writer.Position = this.startOffset;
			uint value = writer.InternalStream.CalculatePECheckSum(length, this.checkSumOffset);
			writer.Position = this.checkSumOffset;
			writer.WriteUInt32(value);
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x001AD598 File Offset: 0x001AD598
		private Machine GetMachine()
		{
			Machine? machine = this.options.Machine;
			if (machine == null)
			{
				return Machine.I386;
			}
			return machine.GetValueOrDefault();
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x001AD5D0 File Offset: 0x001AD5D0
		private bool Use32BitOptionalHeader()
		{
			return !this.GetMachine().Is64Bit();
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x001AD5E0 File Offset: 0x001AD5E0
		private Characteristics GetCharacteristics()
		{
			Characteristics characteristics = this.options.Characteristics ?? this.GetDefaultCharacteristics();
			if (this.IsExeFile)
			{
				characteristics &= ~Characteristics.Dll;
			}
			else
			{
				characteristics |= Characteristics.Dll;
			}
			return characteristics;
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x001AD640 File Offset: 0x001AD640
		private Characteristics GetDefaultCharacteristics()
		{
			if (this.Use32BitOptionalHeader())
			{
				return Characteristics.ExecutableImage | Characteristics.Bit32Machine;
			}
			return Characteristics.ExecutableImage | Characteristics.LargeAddressAware;
		}

		// Token: 0x04002A24 RID: 10788
		private IList<PESection> sections;

		// Token: 0x04002A25 RID: 10789
		private readonly PEHeadersOptions options;

		// Token: 0x04002A26 RID: 10790
		private FileOffset offset;

		// Token: 0x04002A27 RID: 10791
		private RVA rva;

		// Token: 0x04002A28 RID: 10792
		private uint length;

		// Token: 0x04002A29 RID: 10793
		private readonly uint sectionAlignment;

		// Token: 0x04002A2A RID: 10794
		private readonly uint fileAlignment;

		// Token: 0x04002A2B RID: 10795
		private ulong imageBase;

		// Token: 0x04002A2C RID: 10796
		private long startOffset;

		// Token: 0x04002A2D RID: 10797
		private long checkSumOffset;

		// Token: 0x04002A2E RID: 10798
		private bool isExeFile;

		// Token: 0x04002A2F RID: 10799
		private static readonly byte[] dosHeader = new byte[]
		{
			77,
			90,
			144,
			0,
			3,
			0,
			0,
			0,
			4,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			0,
			0,
			184,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			0,
			0,
			0,
			14,
			31,
			186,
			14,
			0,
			180,
			9,
			205,
			33,
			184,
			1,
			76,
			205,
			33,
			84,
			104,
			105,
			115,
			32,
			112,
			114,
			111,
			103,
			114,
			97,
			109,
			32,
			99,
			97,
			110,
			110,
			111,
			116,
			32,
			98,
			101,
			32,
			114,
			117,
			110,
			32,
			105,
			110,
			32,
			68,
			79,
			83,
			32,
			109,
			111,
			100,
			101,
			46,
			13,
			13,
			10,
			36,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};
	}
}
