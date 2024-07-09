using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;
using dnlib.PE;
using dnlib.W32Resources;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008BF RID: 2239
	[ComVisible(true)]
	public sealed class ModuleWriter : ModuleWriterBase
	{
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06005697 RID: 22167 RVA: 0x001A73E0 File Offset: 0x001A73E0
		public override ModuleDef Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06005698 RID: 22168 RVA: 0x001A73E8 File Offset: 0x001A73E8
		public override ModuleWriterOptionsBase TheOptions
		{
			get
			{
				return this.Options;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06005699 RID: 22169 RVA: 0x001A73F0 File Offset: 0x001A73F0
		// (set) Token: 0x0600569A RID: 22170 RVA: 0x001A7424 File Offset: 0x001A7424
		public ModuleWriterOptions Options
		{
			get
			{
				ModuleWriterOptions result;
				if ((result = this.options) == null)
				{
					result = (this.options = new ModuleWriterOptions(this.module));
				}
				return result;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x0600569B RID: 22171 RVA: 0x001A7430 File Offset: 0x001A7430
		public override List<PESection> Sections
		{
			get
			{
				return this.sections;
			}
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x001A7438 File Offset: 0x001A7438
		public override void AddSection(PESection section)
		{
			if (this.sections.Count > 0 && this.sections[this.sections.Count - 1] == this.relocSection)
			{
				this.sections.Insert(this.sections.Count - 1, section);
				return;
			}
			this.sections.Add(section);
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x0600569D RID: 22173 RVA: 0x001A74A4 File Offset: 0x001A74A4
		public override PESection TextSection
		{
			get
			{
				return this.textSection;
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x0600569E RID: 22174 RVA: 0x001A74AC File Offset: 0x001A74AC
		internal PESection SdataSection
		{
			get
			{
				return this.sdataSection;
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x0600569F RID: 22175 RVA: 0x001A74B4 File Offset: 0x001A74B4
		public override PESection RsrcSection
		{
			get
			{
				return this.rsrcSection;
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x060056A0 RID: 22176 RVA: 0x001A74BC File Offset: 0x001A74BC
		public PESection RelocSection
		{
			get
			{
				return this.relocSection;
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x060056A1 RID: 22177 RVA: 0x001A74C4 File Offset: 0x001A74C4
		public PEHeaders PEHeaders
		{
			get
			{
				return this.peHeaders;
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x060056A2 RID: 22178 RVA: 0x001A74CC File Offset: 0x001A74CC
		public ImportAddressTable ImportAddressTable
		{
			get
			{
				return this.importAddressTable;
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x060056A3 RID: 22179 RVA: 0x001A74D4 File Offset: 0x001A74D4
		public ImageCor20Header ImageCor20Header
		{
			get
			{
				return this.imageCor20Header;
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x060056A4 RID: 22180 RVA: 0x001A74DC File Offset: 0x001A74DC
		public ImportDirectory ImportDirectory
		{
			get
			{
				return this.importDirectory;
			}
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060056A5 RID: 22181 RVA: 0x001A74E4 File Offset: 0x001A74E4
		public StartupStub StartupStub
		{
			get
			{
				return this.startupStub;
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x060056A6 RID: 22182 RVA: 0x001A74EC File Offset: 0x001A74EC
		public RelocDirectory RelocDirectory
		{
			get
			{
				return this.relocDirectory;
			}
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x001A74F4 File Offset: 0x001A74F4
		public ModuleWriter(ModuleDef module) : this(module, null)
		{
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x001A7500 File Offset: 0x001A7500
		public ModuleWriter(ModuleDef module, ModuleWriterOptions options)
		{
			this.module = module;
			this.options = options;
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x001A7518 File Offset: 0x001A7518
		protected override long WriteImpl()
		{
			this.Initialize();
			this.metadata.CreateTables();
			return this.WriteFile();
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x001A7534 File Offset: 0x001A7534
		private void Initialize()
		{
			this.CreateSections();
			base.OnWriterEvent(ModuleWriterEvent.PESectionsCreated);
			this.CreateChunks();
			base.OnWriterEvent(ModuleWriterEvent.ChunksCreated);
			this.AddChunksToSections();
			base.OnWriterEvent(ModuleWriterEvent.ChunksAddedToSections);
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x001A756C File Offset: 0x001A756C
		protected override Win32Resources GetWin32Resources()
		{
			if (this.Options.NoWin32Resources)
			{
				return null;
			}
			return this.Options.Win32Resources ?? this.module.Win32Resources;
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x001A75AC File Offset: 0x001A75AC
		private void CreateSections()
		{
			this.sections = new List<PESection>();
			if (this.TheOptions.AddMvidSection)
			{
				this.sections.Add(this.mvidSection = new PESection(".mvid", 1107296320U));
			}
			this.sections.Add(this.textSection = new PESection(".text", 1610612768U));
			this.sections.Add(this.sdataSection = new PESection(".sdata", 3221225536U));
			if (this.GetWin32Resources() != null)
			{
				this.sections.Add(this.rsrcSection = new PESection(".rsrc", 1073741888U));
			}
			this.sections.Add(this.relocSection = new PESection(".reloc", 1107296320U));
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x001A7694 File Offset: 0x001A7694
		private void CreateChunks()
		{
			this.peHeaders = new PEHeaders(this.Options.PEHeadersOptions);
			Machine machine = this.Options.PEHeadersOptions.Machine ?? Machine.I386;
			bool is64bit = machine.Is64Bit();
			this.relocDirectory = new RelocDirectory(machine);
			if (machine.IsI386())
			{
				this.needStartupStub = true;
			}
			this.importAddressTable = new ImportAddressTable(is64bit);
			this.importDirectory = new ImportDirectory(is64bit);
			this.startupStub = new StartupStub(this.relocDirectory, machine, delegate(string format, object[] args)
			{
				base.Error(format, args);
			});
			base.CreateStrongNameSignature();
			this.imageCor20Header = new ImageCor20Header(this.Options.Cor20HeaderOptions);
			base.CreateMetadataChunks(this.module);
			this.managedExportsWriter = new ManagedExportsWriter(UTF8String.ToSystemStringOrEmpty(this.module.Name), machine, this.relocDirectory, this.metadata, this.peHeaders, delegate(string format, object[] args)
			{
				base.Error(format, args);
			});
			base.CreateDebugDirectory();
			this.importDirectory.IsExeFile = this.Options.IsExeFile;
			this.peHeaders.IsExeFile = this.Options.IsExeFile;
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x001A77DC File Offset: 0x001A77DC
		private void AddChunksToSections()
		{
			uint alignment = (this.Options.PEHeadersOptions.Machine ?? Machine.I386).Is64Bit() ? 8U : 4U;
			if (this.mvidSection != null)
			{
				this.mvidSection.Add(new ByteArrayChunk((this.module.Mvid ?? Guid.Empty).ToByteArray()), 1U);
			}
			this.textSection.Add(this.importAddressTable, alignment);
			this.textSection.Add(this.imageCor20Header, 4U);
			this.textSection.Add(this.strongNameSignature, 4U);
			this.managedExportsWriter.AddTextChunks(this.textSection);
			this.textSection.Add(this.constants, 8U);
			this.textSection.Add(this.methodBodies, 4U);
			this.textSection.Add(this.netResources, 4U);
			this.textSection.Add(this.metadata, 4U);
			this.textSection.Add(this.debugDirectory, 4U);
			this.textSection.Add(this.importDirectory, alignment);
			this.textSection.Add(this.startupStub, this.startupStub.Alignment);
			this.managedExportsWriter.AddSdataChunks(this.sdataSection);
			if (this.GetWin32Resources() != null)
			{
				this.rsrcSection.Add(this.win32Resources, 8U);
			}
			this.relocSection.Add(this.relocDirectory, 4U);
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x001A798C File Offset: 0x001A798C
		private long WriteFile()
		{
			this.managedExportsWriter.AddExportedMethods(this.metadata.ExportedMethods, base.GetTimeDateStamp());
			if (this.managedExportsWriter.HasExports)
			{
				this.needStartupStub = true;
			}
			base.OnWriterEvent(ModuleWriterEvent.BeginWritePdb);
			base.WritePdbFile();
			base.OnWriterEvent(ModuleWriterEvent.EndWritePdb);
			this.metadata.OnBeforeSetOffset();
			base.OnWriterEvent(ModuleWriterEvent.BeginCalculateRvasAndFileOffsets);
			List<IChunk> list = new List<IChunk>();
			list.Add(this.peHeaders);
			if (!this.managedExportsWriter.HasExports)
			{
				this.sections.Remove(this.sdataSection);
			}
			if (!this.relocDirectory.NeedsRelocSection && !this.managedExportsWriter.HasExports && !this.needStartupStub)
			{
				this.sections.Remove(this.relocSection);
			}
			this.importAddressTable.Enable = this.needStartupStub;
			this.importDirectory.Enable = this.needStartupStub;
			this.startupStub.Enable = this.needStartupStub;
			foreach (PESection item in this.sections)
			{
				list.Add(item);
			}
			this.peHeaders.PESections = this.sections;
			int num = this.sections.IndexOf(this.relocSection);
			if (num >= 0 && num != this.sections.Count - 1)
			{
				throw new InvalidOperationException("Reloc section must be the last section, use AddSection() to add a section");
			}
			base.CalculateRvasAndFileOffsets(list, (FileOffset)0U, (RVA)0U, this.peHeaders.FileAlignment, this.peHeaders.SectionAlignment);
			base.OnWriterEvent(ModuleWriterEvent.EndCalculateRvasAndFileOffsets);
			this.InitializeChunkProperties();
			base.OnWriterEvent(ModuleWriterEvent.BeginWriteChunks);
			DataWriter dataWriter = new DataWriter(this.destStream);
			base.WriteChunks(dataWriter, list, (FileOffset)0U, this.peHeaders.FileAlignment);
			long num2 = dataWriter.Position - this.destStreamBaseOffset;
			base.OnWriterEvent(ModuleWriterEvent.EndWriteChunks);
			base.OnWriterEvent(ModuleWriterEvent.BeginStrongNameSign);
			if (this.Options.StrongNameKey != null)
			{
				base.StrongNameSign((long)((ulong)this.strongNameSignature.FileOffset));
			}
			base.OnWriterEvent(ModuleWriterEvent.EndStrongNameSign);
			base.OnWriterEvent(ModuleWriterEvent.BeginWritePEChecksum);
			if (this.Options.AddCheckSum)
			{
				this.peHeaders.WriteCheckSum(dataWriter, num2);
			}
			base.OnWriterEvent(ModuleWriterEvent.EndWritePEChecksum);
			return num2;
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x001A7C00 File Offset: 0x001A7C00
		private void InitializeChunkProperties()
		{
			this.Options.Cor20HeaderOptions.EntryPoint = new uint?(this.GetEntryPoint());
			this.importAddressTable.ImportDirectory = this.importDirectory;
			this.importDirectory.ImportAddressTable = this.importAddressTable;
			this.startupStub.ImportDirectory = this.importDirectory;
			this.startupStub.PEHeaders = this.peHeaders;
			this.peHeaders.StartupStub = this.startupStub;
			this.peHeaders.ImageCor20Header = this.imageCor20Header;
			this.peHeaders.ImportAddressTable = this.importAddressTable;
			this.peHeaders.ImportDirectory = this.importDirectory;
			this.peHeaders.Win32Resources = this.win32Resources;
			this.peHeaders.RelocDirectory = this.relocDirectory;
			this.peHeaders.DebugDirectory = this.debugDirectory;
			this.imageCor20Header.Metadata = this.metadata;
			this.imageCor20Header.NetResources = this.netResources;
			this.imageCor20Header.StrongNameSignature = this.strongNameSignature;
			this.managedExportsWriter.InitializeChunkProperties();
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x001A7D28 File Offset: 0x001A7D28
		private uint GetEntryPoint()
		{
			MethodDef methodDef = this.module.ManagedEntryPoint as MethodDef;
			if (methodDef != null)
			{
				return new MDToken(Table.Method, this.metadata.GetRid(methodDef)).Raw;
			}
			FileDef fileDef = this.module.ManagedEntryPoint as FileDef;
			if (fileDef != null)
			{
				return new MDToken(Table.File, this.metadata.GetRid(fileDef)).Raw;
			}
			uint nativeEntryPoint = (uint)this.module.NativeEntryPoint;
			if (nativeEntryPoint != 0U)
			{
				return nativeEntryPoint;
			}
			return 0U;
		}

		// Token: 0x04002979 RID: 10617
		private const uint DEFAULT_RELOC_ALIGNMENT = 4U;

		// Token: 0x0400297A RID: 10618
		private const uint MVID_ALIGNMENT = 1U;

		// Token: 0x0400297B RID: 10619
		private readonly ModuleDef module;

		// Token: 0x0400297C RID: 10620
		private ModuleWriterOptions options;

		// Token: 0x0400297D RID: 10621
		private List<PESection> sections;

		// Token: 0x0400297E RID: 10622
		private PESection mvidSection;

		// Token: 0x0400297F RID: 10623
		private PESection textSection;

		// Token: 0x04002980 RID: 10624
		private PESection sdataSection;

		// Token: 0x04002981 RID: 10625
		private PESection rsrcSection;

		// Token: 0x04002982 RID: 10626
		private PESection relocSection;

		// Token: 0x04002983 RID: 10627
		private PEHeaders peHeaders;

		// Token: 0x04002984 RID: 10628
		private ImportAddressTable importAddressTable;

		// Token: 0x04002985 RID: 10629
		private ImageCor20Header imageCor20Header;

		// Token: 0x04002986 RID: 10630
		private ImportDirectory importDirectory;

		// Token: 0x04002987 RID: 10631
		private StartupStub startupStub;

		// Token: 0x04002988 RID: 10632
		private RelocDirectory relocDirectory;

		// Token: 0x04002989 RID: 10633
		private ManagedExportsWriter managedExportsWriter;

		// Token: 0x0400298A RID: 10634
		private bool needStartupStub;
	}
}
