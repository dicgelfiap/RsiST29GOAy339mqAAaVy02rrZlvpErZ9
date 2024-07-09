using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;
using dnlib.PE;
using dnlib.W32Resources;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008C5 RID: 2245
	[ComVisible(true)]
	public class ModuleWriterOptionsBase
	{
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060056BF RID: 22207 RVA: 0x001A7E7C File Offset: 0x001A7E7C
		// (remove) Token: 0x060056C0 RID: 22208 RVA: 0x001A7EB8 File Offset: 0x001A7EB8
		public event EventHandler2<ModuleWriterEventArgs> WriterEvent;

		// Token: 0x060056C1 RID: 22209 RVA: 0x001A7EF4 File Offset: 0x001A7EF4
		internal void RaiseEvent(object sender, ModuleWriterEventArgs e)
		{
			EventHandler2<ModuleWriterEventArgs> writerEvent = this.WriterEvent;
			if (writerEvent == null)
			{
				return;
			}
			writerEvent(sender, e);
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060056C2 RID: 22210 RVA: 0x001A7F0C File Offset: 0x001A7F0C
		// (remove) Token: 0x060056C3 RID: 22211 RVA: 0x001A7F48 File Offset: 0x001A7F48
		public event EventHandler2<ModuleWriterProgressEventArgs> ProgressUpdated;

		// Token: 0x060056C4 RID: 22212 RVA: 0x001A7F84 File Offset: 0x001A7F84
		internal void RaiseEvent(object sender, ModuleWriterProgressEventArgs e)
		{
			EventHandler2<ModuleWriterProgressEventArgs> progressUpdated = this.ProgressUpdated;
			if (progressUpdated == null)
			{
				return;
			}
			progressUpdated(sender, e);
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060056C5 RID: 22213 RVA: 0x001A7F9C File Offset: 0x001A7F9C
		// (set) Token: 0x060056C6 RID: 22214 RVA: 0x001A7FA4 File Offset: 0x001A7FA4
		public ILogger Logger
		{
			get
			{
				return this.logger;
			}
			set
			{
				this.logger = value;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060056C7 RID: 22215 RVA: 0x001A7FB0 File Offset: 0x001A7FB0
		// (set) Token: 0x060056C8 RID: 22216 RVA: 0x001A7FB8 File Offset: 0x001A7FB8
		public ILogger MetadataLogger
		{
			get
			{
				return this.metadataLogger;
			}
			set
			{
				this.metadataLogger = value;
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x060056C9 RID: 22217 RVA: 0x001A7FC4 File Offset: 0x001A7FC4
		// (set) Token: 0x060056CA RID: 22218 RVA: 0x001A7FF0 File Offset: 0x001A7FF0
		public PEHeadersOptions PEHeadersOptions
		{
			get
			{
				PEHeadersOptions result;
				if ((result = this.peHeadersOptions) == null)
				{
					result = (this.peHeadersOptions = new PEHeadersOptions());
				}
				return result;
			}
			set
			{
				this.peHeadersOptions = value;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060056CB RID: 22219 RVA: 0x001A7FFC File Offset: 0x001A7FFC
		// (set) Token: 0x060056CC RID: 22220 RVA: 0x001A8028 File Offset: 0x001A8028
		public Cor20HeaderOptions Cor20HeaderOptions
		{
			get
			{
				Cor20HeaderOptions result;
				if ((result = this.cor20HeaderOptions) == null)
				{
					result = (this.cor20HeaderOptions = new Cor20HeaderOptions());
				}
				return result;
			}
			set
			{
				this.cor20HeaderOptions = value;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060056CD RID: 22221 RVA: 0x001A8034 File Offset: 0x001A8034
		// (set) Token: 0x060056CE RID: 22222 RVA: 0x001A8060 File Offset: 0x001A8060
		public MetadataOptions MetadataOptions
		{
			get
			{
				MetadataOptions result;
				if ((result = this.metadataOptions) == null)
				{
					result = (this.metadataOptions = new MetadataOptions());
				}
				return result;
			}
			set
			{
				this.metadataOptions = value;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x060056CF RID: 22223 RVA: 0x001A806C File Offset: 0x001A806C
		// (set) Token: 0x060056D0 RID: 22224 RVA: 0x001A8074 File Offset: 0x001A8074
		public bool NoWin32Resources
		{
			get
			{
				return this.noWin32Resources;
			}
			set
			{
				this.noWin32Resources = value;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x060056D1 RID: 22225 RVA: 0x001A8080 File Offset: 0x001A8080
		// (set) Token: 0x060056D2 RID: 22226 RVA: 0x001A8088 File Offset: 0x001A8088
		public Win32Resources Win32Resources
		{
			get
			{
				return this.win32Resources;
			}
			set
			{
				this.win32Resources = value;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x060056D3 RID: 22227 RVA: 0x001A8094 File Offset: 0x001A8094
		// (set) Token: 0x060056D4 RID: 22228 RVA: 0x001A809C File Offset: 0x001A809C
		public bool DelaySign
		{
			get
			{
				return this.delaySign;
			}
			set
			{
				this.delaySign = value;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x060056D5 RID: 22229 RVA: 0x001A80A8 File Offset: 0x001A80A8
		// (set) Token: 0x060056D6 RID: 22230 RVA: 0x001A80B0 File Offset: 0x001A80B0
		public StrongNameKey StrongNameKey
		{
			get
			{
				return this.strongNameKey;
			}
			set
			{
				this.strongNameKey = value;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x001A80BC File Offset: 0x001A80BC
		// (set) Token: 0x060056D8 RID: 22232 RVA: 0x001A80C4 File Offset: 0x001A80C4
		public StrongNamePublicKey StrongNamePublicKey
		{
			get
			{
				return this.strongNamePublicKey;
			}
			set
			{
				this.strongNamePublicKey = value;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x060056D9 RID: 22233 RVA: 0x001A80D0 File Offset: 0x001A80D0
		// (set) Token: 0x060056DA RID: 22234 RVA: 0x001A80D8 File Offset: 0x001A80D8
		public bool ShareMethodBodies { get; set; }

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x060056DB RID: 22235 RVA: 0x001A80E4 File Offset: 0x001A80E4
		// (set) Token: 0x060056DC RID: 22236 RVA: 0x001A80EC File Offset: 0x001A80EC
		public bool AddCheckSum { get; set; }

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x060056DD RID: 22237 RVA: 0x001A80F8 File Offset: 0x001A80F8
		public bool Is64Bit
		{
			get
			{
				return this.PEHeadersOptions.Machine != null && this.PEHeadersOptions.Machine.Value.Is64Bit();
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x060056DE RID: 22238 RVA: 0x001A8128 File Offset: 0x001A8128
		// (set) Token: 0x060056DF RID: 22239 RVA: 0x001A8130 File Offset: 0x001A8130
		public ModuleKind ModuleKind { get; set; }

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x060056E0 RID: 22240 RVA: 0x001A813C File Offset: 0x001A813C
		public bool IsExeFile
		{
			get
			{
				return this.ModuleKind != ModuleKind.Dll && this.ModuleKind != ModuleKind.NetModule;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x060056E1 RID: 22241 RVA: 0x001A8158 File Offset: 0x001A8158
		// (set) Token: 0x060056E2 RID: 22242 RVA: 0x001A8160 File Offset: 0x001A8160
		public bool WritePdb { get; set; }

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x060056E3 RID: 22243 RVA: 0x001A816C File Offset: 0x001A816C
		// (set) Token: 0x060056E4 RID: 22244 RVA: 0x001A8174 File Offset: 0x001A8174
		public PdbWriterOptions PdbOptions { get; set; }

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x060056E5 RID: 22245 RVA: 0x001A8180 File Offset: 0x001A8180
		// (set) Token: 0x060056E6 RID: 22246 RVA: 0x001A8188 File Offset: 0x001A8188
		public string PdbFileName { get; set; }

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x060056E7 RID: 22247 RVA: 0x001A8194 File Offset: 0x001A8194
		// (set) Token: 0x060056E8 RID: 22248 RVA: 0x001A819C File Offset: 0x001A819C
		public string PdbFileNameInDebugDirectory { get; set; }

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x060056E9 RID: 22249 RVA: 0x001A81A8 File Offset: 0x001A81A8
		// (set) Token: 0x060056EA RID: 22250 RVA: 0x001A81B0 File Offset: 0x001A81B0
		public Stream PdbStream { get; set; }

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x060056EB RID: 22251 RVA: 0x001A81BC File Offset: 0x001A81BC
		// (set) Token: 0x060056EC RID: 22252 RVA: 0x001A81C4 File Offset: 0x001A81C4
		public Func<Stream, uint, ContentId> GetPdbContentId { get; set; }

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x060056ED RID: 22253 RVA: 0x001A81D0 File Offset: 0x001A81D0
		// (set) Token: 0x060056EE RID: 22254 RVA: 0x001A81D8 File Offset: 0x001A81D8
		public ChecksumAlgorithm PdbChecksumAlgorithm { get; set; } = ChecksumAlgorithm.SHA256;

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x060056EF RID: 22255 RVA: 0x001A81E4 File Offset: 0x001A81E4
		// (set) Token: 0x060056F0 RID: 22256 RVA: 0x001A81EC File Offset: 0x001A81EC
		public bool AddMvidSection { get; set; }

		// Token: 0x060056F1 RID: 22257 RVA: 0x001A81F8 File Offset: 0x001A81F8
		protected ModuleWriterOptionsBase(ModuleDef module)
		{
			this.ShareMethodBodies = true;
			this.MetadataOptions.MetadataHeaderOptions.VersionString = module.RuntimeVersion;
			this.ModuleKind = module.Kind;
			this.PEHeadersOptions.Machine = new Machine?(module.Machine);
			this.PEHeadersOptions.Characteristics = new Characteristics?(module.Characteristics);
			this.PEHeadersOptions.DllCharacteristics = new DllCharacteristics?(module.DllCharacteristics);
			if (module.Kind == ModuleKind.Windows)
			{
				this.PEHeadersOptions.Subsystem = new Subsystem?(Subsystem.WindowsGui);
			}
			else
			{
				this.PEHeadersOptions.Subsystem = new Subsystem?(Subsystem.WindowsCui);
			}
			this.PEHeadersOptions.NumberOfRvaAndSizes = new uint?(16U);
			this.Cor20HeaderOptions.Flags = new ComImageFlags?(module.Cor20HeaderFlags);
			if (module.Assembly != null && !PublicKeyBase.IsNullOrEmpty2(module.Assembly.PublicKey))
			{
				this.Cor20HeaderOptions.Flags |= ComImageFlags.StrongNameSigned;
			}
			if (module.Cor20HeaderRuntimeVersion != null)
			{
				this.Cor20HeaderOptions.MajorRuntimeVersion = new ushort?((ushort)(module.Cor20HeaderRuntimeVersion.Value >> 16));
				this.Cor20HeaderOptions.MinorRuntimeVersion = new ushort?((ushort)module.Cor20HeaderRuntimeVersion.Value);
			}
			else if (module.IsClr1x)
			{
				this.Cor20HeaderOptions.MajorRuntimeVersion = new ushort?((ushort)2);
				this.Cor20HeaderOptions.MinorRuntimeVersion = new ushort?(0);
			}
			else
			{
				this.Cor20HeaderOptions.MajorRuntimeVersion = new ushort?((ushort)2);
				this.Cor20HeaderOptions.MinorRuntimeVersion = new ushort?((ushort)5);
			}
			if (module.TablesHeaderVersion != null)
			{
				this.MetadataOptions.TablesHeapOptions.MajorVersion = new byte?((byte)(module.TablesHeaderVersion.Value >> 8));
				this.MetadataOptions.TablesHeapOptions.MinorVersion = new byte?((byte)module.TablesHeaderVersion.Value);
			}
			else if (module.IsClr1x)
			{
				this.MetadataOptions.TablesHeapOptions.MajorVersion = new byte?((byte)1);
				this.MetadataOptions.TablesHeapOptions.MinorVersion = new byte?(0);
			}
			else
			{
				this.MetadataOptions.TablesHeapOptions.MajorVersion = new byte?((byte)2);
				this.MetadataOptions.TablesHeapOptions.MinorVersion = new byte?(0);
			}
			this.MetadataOptions.Flags |= MetadataFlags.AlwaysCreateGuidHeap;
			ModuleDefMD moduleDefMD = module as ModuleDefMD;
			if (moduleDefMD != null)
			{
				ImageNTHeaders imageNTHeaders = moduleDefMD.Metadata.PEImage.ImageNTHeaders;
				this.PEHeadersOptions.TimeDateStamp = new uint?(imageNTHeaders.FileHeader.TimeDateStamp);
				this.PEHeadersOptions.MajorLinkerVersion = new byte?(imageNTHeaders.OptionalHeader.MajorLinkerVersion);
				this.PEHeadersOptions.MinorLinkerVersion = new byte?(imageNTHeaders.OptionalHeader.MinorLinkerVersion);
				this.PEHeadersOptions.ImageBase = new ulong?(imageNTHeaders.OptionalHeader.ImageBase);
				this.PEHeadersOptions.MajorOperatingSystemVersion = new ushort?(imageNTHeaders.OptionalHeader.MajorOperatingSystemVersion);
				this.PEHeadersOptions.MinorOperatingSystemVersion = new ushort?(imageNTHeaders.OptionalHeader.MinorOperatingSystemVersion);
				this.PEHeadersOptions.MajorImageVersion = new ushort?(imageNTHeaders.OptionalHeader.MajorImageVersion);
				this.PEHeadersOptions.MinorImageVersion = new ushort?(imageNTHeaders.OptionalHeader.MinorImageVersion);
				this.PEHeadersOptions.MajorSubsystemVersion = new ushort?(imageNTHeaders.OptionalHeader.MajorSubsystemVersion);
				this.PEHeadersOptions.MinorSubsystemVersion = new ushort?(imageNTHeaders.OptionalHeader.MinorSubsystemVersion);
				this.PEHeadersOptions.Win32VersionValue = new uint?(imageNTHeaders.OptionalHeader.Win32VersionValue);
				this.AddCheckSum = (imageNTHeaders.OptionalHeader.CheckSum > 0U);
				this.AddMvidSection = ModuleWriterOptionsBase.HasMvidSection(moduleDefMD.Metadata.PEImage.ImageSectionHeaders);
				if (ModuleWriterOptionsBase.HasDebugDirectoryEntry(moduleDefMD.Metadata.PEImage.ImageDebugDirectories, ImageDebugType.Reproducible))
				{
					this.PdbOptions |= PdbWriterOptions.Deterministic;
				}
				if (ModuleWriterOptionsBase.HasDebugDirectoryEntry(moduleDefMD.Metadata.PEImage.ImageDebugDirectories, ImageDebugType.PdbChecksum))
				{
					this.PdbOptions |= PdbWriterOptions.PdbChecksum;
				}
				ChecksumAlgorithm pdbChecksumAlgorithm;
				if (ModuleWriterOptionsBase.TryGetPdbChecksumAlgorithm(moduleDefMD.Metadata.PEImage, moduleDefMD.Metadata.PEImage.ImageDebugDirectories, out pdbChecksumAlgorithm))
				{
					this.PdbChecksumAlgorithm = pdbChecksumAlgorithm;
				}
			}
			if (this.Is64Bit)
			{
				this.PEHeadersOptions.Characteristics &= ~Characteristics.Bit32Machine;
				this.PEHeadersOptions.Characteristics |= Characteristics.LargeAddressAware;
				return;
			}
			if (moduleDefMD == null)
			{
				this.PEHeadersOptions.Characteristics |= Characteristics.Bit32Machine;
			}
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x001A87A8 File Offset: 0x001A87A8
		private static bool HasMvidSection(IList<ImageSectionHeader> sections)
		{
			int count = sections.Count;
			for (int i = 0; i < count; i++)
			{
				ImageSectionHeader imageSectionHeader = sections[i];
				if (imageSectionHeader.VirtualSize == 16U)
				{
					byte[] name = imageSectionHeader.Name;
					if (name[0] == 46 && name[1] == 109 && name[2] == 118 && name[3] == 105 && name[4] == 100 && name[5] == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x001A8828 File Offset: 0x001A8828
		private static bool HasDebugDirectoryEntry(IList<ImageDebugDirectory> debugDirs, ImageDebugType type)
		{
			int count = debugDirs.Count;
			for (int i = 0; i < count; i++)
			{
				if (debugDirs[i].Type == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x001A8864 File Offset: 0x001A8864
		private static bool TryGetPdbChecksumAlgorithm(IPEImage peImage, IList<ImageDebugDirectory> debugDirs, out ChecksumAlgorithm pdbChecksumAlgorithm)
		{
			int count = debugDirs.Count;
			for (int i = 0; i < count; i++)
			{
				ImageDebugDirectory imageDebugDirectory = debugDirs[i];
				if (imageDebugDirectory.Type == ImageDebugType.PdbChecksum)
				{
					DataReader dataReader = peImage.CreateReader(imageDebugDirectory.AddressOfRawData, imageDebugDirectory.SizeOfData);
					if (ModuleWriterOptionsBase.TryGetPdbChecksumAlgorithm(ref dataReader, out pdbChecksumAlgorithm))
					{
						return true;
					}
				}
			}
			pdbChecksumAlgorithm = ChecksumAlgorithm.SHA256;
			return false;
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x001A88C8 File Offset: 0x001A88C8
		private static bool TryGetPdbChecksumAlgorithm(ref DataReader reader, out ChecksumAlgorithm pdbChecksumAlgorithm)
		{
			try
			{
				int num;
				if (Hasher.TryGetChecksumAlgorithm(reader.TryReadZeroTerminatedUtf8String(), out pdbChecksumAlgorithm, out num) && num == (int)reader.BytesLeft)
				{
					return true;
				}
			}
			catch (IOException)
			{
			}
			catch (ArgumentException)
			{
			}
			pdbChecksumAlgorithm = ChecksumAlgorithm.SHA256;
			return false;
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x001A8930 File Offset: 0x001A8930
		public void InitializeStrongNameSigning(ModuleDef module, StrongNameKey signatureKey)
		{
			this.StrongNameKey = signatureKey;
			this.StrongNamePublicKey = null;
			if (module.Assembly != null)
			{
				module.Assembly.CustomAttributes.RemoveAll("System.Reflection.AssemblySignatureKeyAttribute");
			}
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x001A8970 File Offset: 0x001A8970
		public void InitializeEnhancedStrongNameSigning(ModuleDef module, StrongNameKey signatureKey, StrongNamePublicKey signaturePubKey)
		{
			this.InitializeStrongNameSigning(module, signatureKey);
			this.StrongNameKey = this.StrongNameKey.WithHashAlgorithm(signaturePubKey.HashAlgorithm);
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x001A89A0 File Offset: 0x001A89A0
		public void InitializeEnhancedStrongNameSigning(ModuleDef module, StrongNameKey signatureKey, StrongNamePublicKey signaturePubKey, StrongNameKey identityKey, StrongNamePublicKey identityPubKey)
		{
			this.StrongNameKey = signatureKey.WithHashAlgorithm(signaturePubKey.HashAlgorithm);
			this.StrongNamePublicKey = identityPubKey;
			if (module.Assembly != null)
			{
				module.Assembly.UpdateOrCreateAssemblySignatureKeyAttribute(identityPubKey, identityKey, signaturePubKey);
			}
		}

		// Token: 0x04002997 RID: 10647
		private PEHeadersOptions peHeadersOptions;

		// Token: 0x04002998 RID: 10648
		private Cor20HeaderOptions cor20HeaderOptions;

		// Token: 0x04002999 RID: 10649
		private MetadataOptions metadataOptions;

		// Token: 0x0400299A RID: 10650
		private ILogger logger;

		// Token: 0x0400299B RID: 10651
		private ILogger metadataLogger;

		// Token: 0x0400299C RID: 10652
		private bool noWin32Resources;

		// Token: 0x0400299D RID: 10653
		private Win32Resources win32Resources;

		// Token: 0x0400299E RID: 10654
		private StrongNameKey strongNameKey;

		// Token: 0x0400299F RID: 10655
		private StrongNamePublicKey strongNamePublicKey;

		// Token: 0x040029A0 RID: 10656
		private bool delaySign;

		// Token: 0x040029AC RID: 10668
		private const ChecksumAlgorithm DefaultPdbChecksumAlgorithm = ChecksumAlgorithm.SHA256;
	}
}
