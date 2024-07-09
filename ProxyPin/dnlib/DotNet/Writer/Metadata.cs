using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.DotNet.Pdb.Portable;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008B5 RID: 2229
	[ComVisible(true)]
	public abstract class Metadata : IReuseChunk, IChunk, ISignatureWriterHelper, IWriterError, ITokenProvider, ICustomAttributeWriterHelper, IFullNameFactoryHelper, IPortablePdbCustomDebugInfoWriterHelper
	{
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06005544 RID: 21828 RVA: 0x0019F8CC File Offset: 0x0019F8CC
		// (remove) Token: 0x06005545 RID: 21829 RVA: 0x0019F908 File Offset: 0x0019F908
		public event EventHandler2<MetadataWriterEventArgs> MetadataEvent;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06005546 RID: 21830 RVA: 0x0019F944 File Offset: 0x0019F944
		// (remove) Token: 0x06005547 RID: 21831 RVA: 0x0019F980 File Offset: 0x0019F980
		public event EventHandler2<MetadataProgressEventArgs> ProgressUpdated;

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06005548 RID: 21832 RVA: 0x0019F9BC File Offset: 0x0019F9BC
		// (set) Token: 0x06005549 RID: 21833 RVA: 0x0019F9C4 File Offset: 0x0019F9C4
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

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x0600554A RID: 21834 RVA: 0x0019F9D0 File Offset: 0x0019F9D0
		public ModuleDef Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x0600554B RID: 21835 RVA: 0x0019F9D8 File Offset: 0x0019F9D8
		public UniqueChunkList<ByteArrayChunk> Constants
		{
			get
			{
				return this.constants;
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x0600554C RID: 21836 RVA: 0x0019F9E0 File Offset: 0x0019F9E0
		public MethodBodyChunks MethodBodyChunks
		{
			get
			{
				return this.methodBodies;
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x0600554D RID: 21837 RVA: 0x0019F9E8 File Offset: 0x0019F9E8
		public NetResources NetResources
		{
			get
			{
				return this.netResources;
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x0600554E RID: 21838 RVA: 0x0019F9F0 File Offset: 0x0019F9F0
		public MetadataHeader MetadataHeader
		{
			get
			{
				return this.metadataHeader;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x0600554F RID: 21839 RVA: 0x0019F9F8 File Offset: 0x0019F9F8
		public TablesHeap TablesHeap
		{
			get
			{
				return this.tablesHeap;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06005550 RID: 21840 RVA: 0x0019FA00 File Offset: 0x0019FA00
		public StringsHeap StringsHeap
		{
			get
			{
				return this.stringsHeap;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06005551 RID: 21841 RVA: 0x0019FA08 File Offset: 0x0019FA08
		public USHeap USHeap
		{
			get
			{
				return this.usHeap;
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06005552 RID: 21842 RVA: 0x0019FA10 File Offset: 0x0019FA10
		public GuidHeap GuidHeap
		{
			get
			{
				return this.guidHeap;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06005553 RID: 21843 RVA: 0x0019FA18 File Offset: 0x0019FA18
		public BlobHeap BlobHeap
		{
			get
			{
				return this.blobHeap;
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06005554 RID: 21844 RVA: 0x0019FA20 File Offset: 0x0019FA20
		public PdbHeap PdbHeap
		{
			get
			{
				return this.pdbHeap;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06005555 RID: 21845 RVA: 0x0019FA28 File Offset: 0x0019FA28
		public List<MethodDef> ExportedMethods
		{
			get
			{
				return this.exportedMethods;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06005556 RID: 21846 RVA: 0x0019FA30 File Offset: 0x0019FA30
		// (set) Token: 0x06005557 RID: 21847 RVA: 0x0019FA38 File Offset: 0x0019FA38
		internal byte[] AssemblyPublicKey { get; set; }

		// Token: 0x06005558 RID: 21848 RVA: 0x0019FA44 File Offset: 0x0019FA44
		public static Metadata Create(ModuleDef module, UniqueChunkList<ByteArrayChunk> constants, MethodBodyChunks methodBodies, NetResources netResources, MetadataOptions options = null, DebugMetadataKind debugKind = DebugMetadataKind.None)
		{
			if (options == null)
			{
				options = new MetadataOptions();
			}
			if ((options.Flags & MetadataFlags.PreserveRids) != (MetadataFlags)0U && module is ModuleDefMD)
			{
				return new PreserveTokensMetadata(module, constants, methodBodies, netResources, options, debugKind, false);
			}
			return new NormalMetadata(module, constants, methodBodies, netResources, options, debugKind, false);
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x06005559 RID: 21849 RVA: 0x0019FAA0 File Offset: 0x0019FAA0
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x0600555A RID: 21850 RVA: 0x0019FAA8 File Offset: 0x0019FAA8
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x0600555B RID: 21851 RVA: 0x0019FAB0 File Offset: 0x0019FAB0
		public bool PreserveTypeRefRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveTypeRefRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x0600555C RID: 21852 RVA: 0x0019FAC4 File Offset: 0x0019FAC4
		public bool PreserveTypeDefRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveTypeDefRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x0600555D RID: 21853 RVA: 0x0019FAD8 File Offset: 0x0019FAD8
		public bool PreserveFieldRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveFieldRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x0600555E RID: 21854 RVA: 0x0019FAEC File Offset: 0x0019FAEC
		public bool PreserveMethodRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveMethodRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x0600555F RID: 21855 RVA: 0x0019FB00 File Offset: 0x0019FB00
		public bool PreserveParamRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveParamRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06005560 RID: 21856 RVA: 0x0019FB14 File Offset: 0x0019FB14
		public bool PreserveMemberRefRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveMemberRefRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06005561 RID: 21857 RVA: 0x0019FB28 File Offset: 0x0019FB28
		public bool PreserveStandAloneSigRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveStandAloneSigRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06005562 RID: 21858 RVA: 0x0019FB3C File Offset: 0x0019FB3C
		public bool PreserveEventRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveEventRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06005563 RID: 21859 RVA: 0x0019FB54 File Offset: 0x0019FB54
		public bool PreservePropertyRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreservePropertyRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06005564 RID: 21860 RVA: 0x0019FB6C File Offset: 0x0019FB6C
		public bool PreserveTypeSpecRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveTypeSpecRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06005565 RID: 21861 RVA: 0x0019FB84 File Offset: 0x0019FB84
		public bool PreserveMethodSpecRids
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveMethodSpecRids) > (MetadataFlags)0U;
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06005566 RID: 21862 RVA: 0x0019FB9C File Offset: 0x0019FB9C
		// (set) Token: 0x06005567 RID: 21863 RVA: 0x0019FBB4 File Offset: 0x0019FBB4
		public bool PreserveStringsOffsets
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveStringsOffsets) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.PreserveStringsOffsets;
					return;
				}
				this.options.Flags &= ~MetadataFlags.PreserveStringsOffsets;
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06005568 RID: 21864 RVA: 0x0019FBEC File Offset: 0x0019FBEC
		// (set) Token: 0x06005569 RID: 21865 RVA: 0x0019FC04 File Offset: 0x0019FC04
		public bool PreserveUSOffsets
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveUSOffsets) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.PreserveUSOffsets;
					return;
				}
				this.options.Flags &= ~MetadataFlags.PreserveUSOffsets;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x0600556A RID: 21866 RVA: 0x0019FC3C File Offset: 0x0019FC3C
		// (set) Token: 0x0600556B RID: 21867 RVA: 0x0019FC54 File Offset: 0x0019FC54
		public bool PreserveBlobOffsets
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveBlobOffsets) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.PreserveBlobOffsets;
					return;
				}
				this.options.Flags &= ~MetadataFlags.PreserveBlobOffsets;
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x0600556C RID: 21868 RVA: 0x0019FC8C File Offset: 0x0019FC8C
		// (set) Token: 0x0600556D RID: 21869 RVA: 0x0019FCA4 File Offset: 0x0019FCA4
		public bool PreserveExtraSignatureData
		{
			get
			{
				return (this.options.Flags & MetadataFlags.PreserveExtraSignatureData) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.PreserveExtraSignatureData;
					return;
				}
				this.options.Flags &= ~MetadataFlags.PreserveExtraSignatureData;
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x0600556E RID: 21870 RVA: 0x0019FCDC File Offset: 0x0019FCDC
		// (set) Token: 0x0600556F RID: 21871 RVA: 0x0019FCF4 File Offset: 0x0019FCF4
		public bool KeepOldMaxStack
		{
			get
			{
				return (this.options.Flags & MetadataFlags.KeepOldMaxStack) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.KeepOldMaxStack;
					return;
				}
				this.options.Flags &= ~MetadataFlags.KeepOldMaxStack;
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x0019FD2C File Offset: 0x0019FD2C
		// (set) Token: 0x06005571 RID: 21873 RVA: 0x0019FD44 File Offset: 0x0019FD44
		public bool AlwaysCreateGuidHeap
		{
			get
			{
				return (this.options.Flags & MetadataFlags.AlwaysCreateGuidHeap) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.AlwaysCreateGuidHeap;
					return;
				}
				this.options.Flags &= ~MetadataFlags.AlwaysCreateGuidHeap;
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06005572 RID: 21874 RVA: 0x0019FD7C File Offset: 0x0019FD7C
		// (set) Token: 0x06005573 RID: 21875 RVA: 0x0019FD94 File Offset: 0x0019FD94
		public bool AlwaysCreateStringsHeap
		{
			get
			{
				return (this.options.Flags & MetadataFlags.AlwaysCreateStringsHeap) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.AlwaysCreateStringsHeap;
					return;
				}
				this.options.Flags &= ~MetadataFlags.AlwaysCreateStringsHeap;
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x0019FDCC File Offset: 0x0019FDCC
		// (set) Token: 0x06005575 RID: 21877 RVA: 0x0019FDE4 File Offset: 0x0019FDE4
		public bool AlwaysCreateUSHeap
		{
			get
			{
				return (this.options.Flags & MetadataFlags.AlwaysCreateUSHeap) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.AlwaysCreateUSHeap;
					return;
				}
				this.options.Flags &= ~MetadataFlags.AlwaysCreateUSHeap;
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06005576 RID: 21878 RVA: 0x0019FE1C File Offset: 0x0019FE1C
		// (set) Token: 0x06005577 RID: 21879 RVA: 0x0019FE34 File Offset: 0x0019FE34
		public bool AlwaysCreateBlobHeap
		{
			get
			{
				return (this.options.Flags & MetadataFlags.AlwaysCreateBlobHeap) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.AlwaysCreateBlobHeap;
					return;
				}
				this.options.Flags &= ~MetadataFlags.AlwaysCreateBlobHeap;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06005578 RID: 21880 RVA: 0x0019FE6C File Offset: 0x0019FE6C
		// (set) Token: 0x06005579 RID: 21881 RVA: 0x0019FE84 File Offset: 0x0019FE84
		public bool RoslynSortInterfaceImpl
		{
			get
			{
				return (this.options.Flags & MetadataFlags.RoslynSortInterfaceImpl) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.RoslynSortInterfaceImpl;
					return;
				}
				this.options.Flags &= ~MetadataFlags.RoslynSortInterfaceImpl;
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x0600557A RID: 21882 RVA: 0x0019FEBC File Offset: 0x0019FEBC
		// (set) Token: 0x0600557B RID: 21883 RVA: 0x0019FED4 File Offset: 0x0019FED4
		public bool NoMethodBodies
		{
			get
			{
				return (this.options.Flags & MetadataFlags.NoMethodBodies) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.NoMethodBodies;
					return;
				}
				this.options.Flags &= ~MetadataFlags.NoMethodBodies;
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x0600557C RID: 21884 RVA: 0x0019FF0C File Offset: 0x0019FF0C
		// (set) Token: 0x0600557D RID: 21885 RVA: 0x0019FF24 File Offset: 0x0019FF24
		public bool NoDotNetResources
		{
			get
			{
				return (this.options.Flags & MetadataFlags.NoDotNetResources) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.NoDotNetResources;
					return;
				}
				this.options.Flags &= ~MetadataFlags.NoDotNetResources;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x0600557E RID: 21886 RVA: 0x0019FF5C File Offset: 0x0019FF5C
		// (set) Token: 0x0600557F RID: 21887 RVA: 0x0019FF74 File Offset: 0x0019FF74
		public bool NoFieldData
		{
			get
			{
				return (this.options.Flags & MetadataFlags.NoFieldData) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.NoFieldData;
					return;
				}
				this.options.Flags &= ~MetadataFlags.NoFieldData;
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06005580 RID: 21888 RVA: 0x0019FFAC File Offset: 0x0019FFAC
		// (set) Token: 0x06005581 RID: 21889 RVA: 0x0019FFC4 File Offset: 0x0019FFC4
		public bool OptimizeCustomAttributeSerializedTypeNames
		{
			get
			{
				return (this.options.Flags & MetadataFlags.OptimizeCustomAttributeSerializedTypeNames) > (MetadataFlags)0U;
			}
			set
			{
				if (value)
				{
					this.options.Flags |= MetadataFlags.OptimizeCustomAttributeSerializedTypeNames;
					return;
				}
				this.options.Flags &= ~MetadataFlags.OptimizeCustomAttributeSerializedTypeNames;
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06005582 RID: 21890 RVA: 0x0019FFFC File Offset: 0x0019FFFC
		// (set) Token: 0x06005583 RID: 21891 RVA: 0x001A0004 File Offset: 0x001A0004
		internal bool KeepFieldRVA { get; set; }

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06005584 RID: 21892
		protected abstract int NumberOfMethods { get; }

		// Token: 0x06005585 RID: 21893 RVA: 0x001A0010 File Offset: 0x001A0010
		internal Metadata(ModuleDef module, UniqueChunkList<ByteArrayChunk> constants, MethodBodyChunks methodBodies, NetResources netResources, MetadataOptions options, DebugMetadataKind debugKind, bool isStandaloneDebugMetadata)
		{
			this.module = module;
			this.constants = constants;
			this.methodBodies = methodBodies;
			this.netResources = netResources;
			this.options = (options ?? new MetadataOptions());
			this.metadataHeader = new MetadataHeader(isStandaloneDebugMetadata ? this.options.DebugMetadataHeaderOptions : this.options.MetadataHeaderOptions);
			this.tablesHeap = new TablesHeap(this, isStandaloneDebugMetadata ? this.options.DebugTablesHeapOptions : this.options.TablesHeapOptions);
			this.stringsHeap = new StringsHeap();
			this.usHeap = new USHeap();
			this.guidHeap = new GuidHeap();
			this.blobHeap = new BlobHeap();
			this.pdbHeap = new PdbHeap();
			this.isStandaloneDebugMetadata = isStandaloneDebugMetadata;
			if (debugKind == DebugMetadataKind.None)
			{
				return;
			}
			if (debugKind == DebugMetadataKind.Standalone)
			{
				this.debugMetadata = new NormalMetadata(module, constants, methodBodies, netResources, options, DebugMetadataKind.None, true);
				return;
			}
			throw new ArgumentOutOfRangeException("debugKind");
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x001A02B8 File Offset: 0x001A02B8
		public uint GetRid(ModuleDef module)
		{
			uint result;
			this.moduleDefInfos.TryGetRid(module, out result);
			return result;
		}

		// Token: 0x06005587 RID: 21895
		public abstract uint GetRid(TypeRef tr);

		// Token: 0x06005588 RID: 21896
		public abstract uint GetRid(TypeDef td);

		// Token: 0x06005589 RID: 21897
		public abstract uint GetRid(FieldDef fd);

		// Token: 0x0600558A RID: 21898
		public abstract uint GetRid(MethodDef md);

		// Token: 0x0600558B RID: 21899
		public abstract uint GetRid(ParamDef pd);

		// Token: 0x0600558C RID: 21900 RVA: 0x001A02DC File Offset: 0x001A02DC
		public uint GetRid(InterfaceImpl ii)
		{
			uint result;
			this.interfaceImplInfos.TryGetRid(ii, out result);
			return result;
		}

		// Token: 0x0600558D RID: 21901
		public abstract uint GetRid(MemberRef mr);

		// Token: 0x0600558E RID: 21902 RVA: 0x001A0300 File Offset: 0x001A0300
		public uint GetConstantRid(IHasConstant hc)
		{
			uint result;
			this.hasConstantInfos.TryGetRid(hc, out result);
			return result;
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x001A0324 File Offset: 0x001A0324
		public uint GetCustomAttributeRid(CustomAttribute ca)
		{
			uint result;
			this.customAttributeInfos.TryGetRid(ca, out result);
			return result;
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x001A0348 File Offset: 0x001A0348
		public uint GetFieldMarshalRid(IHasFieldMarshal hfm)
		{
			uint result;
			this.fieldMarshalInfos.TryGetRid(hfm, out result);
			return result;
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x001A036C File Offset: 0x001A036C
		public uint GetRid(DeclSecurity ds)
		{
			uint result;
			this.declSecurityInfos.TryGetRid(ds, out result);
			return result;
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x001A0390 File Offset: 0x001A0390
		public uint GetClassLayoutRid(TypeDef td)
		{
			uint result;
			this.classLayoutInfos.TryGetRid(td, out result);
			return result;
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x001A03B4 File Offset: 0x001A03B4
		public uint GetFieldLayoutRid(FieldDef fd)
		{
			uint result;
			this.fieldLayoutInfos.TryGetRid(fd, out result);
			return result;
		}

		// Token: 0x06005594 RID: 21908
		public abstract uint GetRid(StandAloneSig sas);

		// Token: 0x06005595 RID: 21909 RVA: 0x001A03D8 File Offset: 0x001A03D8
		public uint GetEventMapRid(TypeDef td)
		{
			uint result;
			this.eventMapInfos.TryGetRid(td, out result);
			return result;
		}

		// Token: 0x06005596 RID: 21910
		public abstract uint GetRid(EventDef ed);

		// Token: 0x06005597 RID: 21911 RVA: 0x001A03FC File Offset: 0x001A03FC
		public uint GetPropertyMapRid(TypeDef td)
		{
			uint result;
			this.propertyMapInfos.TryGetRid(td, out result);
			return result;
		}

		// Token: 0x06005598 RID: 21912
		public abstract uint GetRid(PropertyDef pd);

		// Token: 0x06005599 RID: 21913 RVA: 0x001A0420 File Offset: 0x001A0420
		public uint GetMethodSemanticsRid(MethodDef md)
		{
			uint result;
			this.methodSemanticsInfos.TryGetRid(md, out result);
			return result;
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x001A0444 File Offset: 0x001A0444
		public uint GetRid(ModuleRef mr)
		{
			uint result;
			this.moduleRefInfos.TryGetRid(mr, out result);
			return result;
		}

		// Token: 0x0600559B RID: 21915
		public abstract uint GetRid(TypeSpec ts);

		// Token: 0x0600559C RID: 21916 RVA: 0x001A0468 File Offset: 0x001A0468
		public uint GetImplMapRid(IMemberForwarded mf)
		{
			uint result;
			this.implMapInfos.TryGetRid(mf, out result);
			return result;
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x001A048C File Offset: 0x001A048C
		public uint GetFieldRVARid(FieldDef fd)
		{
			uint result;
			this.fieldRVAInfos.TryGetRid(fd, out result);
			return result;
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x001A04B0 File Offset: 0x001A04B0
		public uint GetRid(AssemblyDef asm)
		{
			uint result;
			this.assemblyInfos.TryGetRid(asm, out result);
			return result;
		}

		// Token: 0x0600559F RID: 21919 RVA: 0x001A04D4 File Offset: 0x001A04D4
		public uint GetRid(AssemblyRef asmRef)
		{
			uint result;
			this.assemblyRefInfos.TryGetRid(asmRef, out result);
			return result;
		}

		// Token: 0x060055A0 RID: 21920 RVA: 0x001A04F8 File Offset: 0x001A04F8
		public uint GetRid(FileDef fd)
		{
			uint result;
			this.fileDefInfos.TryGetRid(fd, out result);
			return result;
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x001A051C File Offset: 0x001A051C
		public uint GetRid(ExportedType et)
		{
			uint result;
			this.exportedTypeInfos.TryGetRid(et, out result);
			return result;
		}

		// Token: 0x060055A2 RID: 21922 RVA: 0x001A0540 File Offset: 0x001A0540
		public uint GetManifestResourceRid(Resource resource)
		{
			uint result;
			this.manifestResourceInfos.TryGetRid(resource, out result);
			return result;
		}

		// Token: 0x060055A3 RID: 21923 RVA: 0x001A0564 File Offset: 0x001A0564
		public uint GetNestedClassRid(TypeDef td)
		{
			uint result;
			this.nestedClassInfos.TryGetRid(td, out result);
			return result;
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x001A0588 File Offset: 0x001A0588
		public uint GetRid(GenericParam gp)
		{
			uint result;
			this.genericParamInfos.TryGetRid(gp, out result);
			return result;
		}

		// Token: 0x060055A5 RID: 21925
		public abstract uint GetRid(MethodSpec ms);

		// Token: 0x060055A6 RID: 21926 RVA: 0x001A05AC File Offset: 0x001A05AC
		public uint GetRid(GenericParamConstraint gpc)
		{
			uint result;
			this.genericParamConstraintInfos.TryGetRid(gpc, out result);
			return result;
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x001A05D0 File Offset: 0x001A05D0
		public uint GetRid(PdbDocument doc)
		{
			if (this.debugMetadata == null)
			{
				return 0U;
			}
			uint result;
			this.debugMetadata.pdbDocumentInfos.TryGetRid(doc, out result);
			return result;
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x001A0604 File Offset: 0x001A0604
		public uint GetRid(PdbScope scope)
		{
			if (this.debugMetadata == null)
			{
				return 0U;
			}
			uint result;
			this.debugMetadata.localScopeInfos.TryGetRid(scope, out result);
			return result;
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x001A0638 File Offset: 0x001A0638
		public uint GetRid(PdbLocal local)
		{
			if (this.debugMetadata == null)
			{
				return 0U;
			}
			uint result;
			this.debugMetadata.localVariableInfos.TryGetRid(local, out result);
			return result;
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x001A066C File Offset: 0x001A066C
		public uint GetRid(PdbConstant constant)
		{
			if (this.debugMetadata == null)
			{
				return 0U;
			}
			uint result;
			this.debugMetadata.localConstantInfos.TryGetRid(constant, out result);
			return result;
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x001A06A0 File Offset: 0x001A06A0
		public uint GetRid(PdbImportScope importScope)
		{
			if (this.debugMetadata == null)
			{
				return 0U;
			}
			uint result;
			this.debugMetadata.importScopeInfos.TryGetRid(importScope, out result);
			return result;
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x001A06D4 File Offset: 0x001A06D4
		public uint GetStateMachineMethodRid(PdbAsyncMethodCustomDebugInfo asyncMethod)
		{
			if (this.debugMetadata == null)
			{
				return 0U;
			}
			uint result;
			this.debugMetadata.stateMachineMethodInfos.TryGetRid(asyncMethod, out result);
			return result;
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x001A0708 File Offset: 0x001A0708
		public uint GetStateMachineMethodRid(PdbIteratorMethodCustomDebugInfo iteratorMethod)
		{
			if (this.debugMetadata == null)
			{
				return 0U;
			}
			uint result;
			this.debugMetadata.stateMachineMethodInfos.TryGetRid(iteratorMethod, out result);
			return result;
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x001A073C File Offset: 0x001A073C
		public uint GetCustomDebugInfoRid(PdbCustomDebugInfo cdi)
		{
			if (this.debugMetadata == null)
			{
				return 0U;
			}
			uint result;
			this.debugMetadata.customDebugInfos.TryGetRid(cdi, out result);
			return result;
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x001A0770 File Offset: 0x001A0770
		public MethodBody GetMethodBody(MethodDef md)
		{
			if (md == null)
			{
				return null;
			}
			MethodBody result;
			this.methodToBody.TryGetValue(md, out result);
			return result;
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x001A079C File Offset: 0x001A079C
		public uint GetLocalVarSigToken(MethodDef md)
		{
			MethodBody methodBody = this.GetMethodBody(md);
			if (methodBody == null)
			{
				return 0U;
			}
			return methodBody.LocalVarSigTok;
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x001A07B4 File Offset: 0x001A07B4
		public DataReaderChunk GetChunk(EmbeddedResource er)
		{
			if (er == null)
			{
				return null;
			}
			DataReaderChunk result;
			this.embeddedResourceToByteArray.TryGetValue(er, out result);
			return result;
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x001A07E0 File Offset: 0x001A07E0
		public ByteArrayChunk GetInitialValueChunk(FieldDef fd)
		{
			if (fd == null)
			{
				return null;
			}
			ByteArrayChunk result;
			this.fieldToInitialValue.TryGetValue(fd, out result);
			return result;
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x001A080C File Offset: 0x001A080C
		private ILogger GetLogger()
		{
			return this.logger ?? DummyLogger.ThrowModuleWriterExceptionOnErrorInstance;
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x001A0820 File Offset: 0x001A0820
		protected void Error(string message, params object[] args)
		{
			this.GetLogger().Log(this, LoggerEvent.Error, message, args);
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x001A0834 File Offset: 0x001A0834
		protected void Warning(string message, params object[] args)
		{
			this.GetLogger().Log(this, LoggerEvent.Warning, message, args);
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x001A0848 File Offset: 0x001A0848
		protected void OnMetadataEvent(MetadataEvent evt)
		{
			this.RaiseProgress(evt, 0.0);
			EventHandler2<MetadataWriterEventArgs> metadataEvent = this.MetadataEvent;
			if (metadataEvent == null)
			{
				return;
			}
			metadataEvent(this, new MetadataWriterEventArgs(this, evt));
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x001A0878 File Offset: 0x001A0878
		protected void RaiseProgress(MetadataEvent evt, double subProgress)
		{
			subProgress = Math.Min(1.0, Math.Max(0.0, subProgress));
			double num = Metadata.eventToProgress[(int)evt];
			double num2 = Metadata.eventToProgress[(int)(evt + 1)];
			double num3 = num + (num2 - num) * subProgress;
			num3 = Math.Min(1.0, Math.Max(0.0, num3));
			EventHandler2<MetadataProgressEventArgs> progressUpdated = this.ProgressUpdated;
			if (progressUpdated == null)
			{
				return;
			}
			progressUpdated(this, new MetadataProgressEventArgs(this, num3));
		}

		// Token: 0x060055B8 RID: 21944 RVA: 0x001A08FC File Offset: 0x001A08FC
		public void CreateTables()
		{
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.BeginCreateTables);
			if (this.module.Types.Count == 0 || this.module.Types[0] == null)
			{
				throw new ModuleWriterException("Missing global <Module> type");
			}
			ModuleDefMD moduleDefMD = this.module as ModuleDefMD;
			if (moduleDefMD != null)
			{
				if (this.PreserveStringsOffsets)
				{
					this.stringsHeap.Populate(moduleDefMD.StringsStream);
				}
				if (this.PreserveUSOffsets)
				{
					this.usHeap.Populate(moduleDefMD.USStream);
				}
				if (this.PreserveBlobOffsets)
				{
					this.blobHeap.Populate(moduleDefMD.BlobStream);
				}
			}
			this.Create();
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x001A09B8 File Offset: 0x001A09B8
		private void UpdateMethodRvas()
		{
			foreach (KeyValuePair<MethodDef, MethodBody> keyValuePair in this.methodToBody)
			{
				MethodDef key = keyValuePair.Key;
				MethodBody value = keyValuePair.Value;
				uint rid = this.GetRid(key);
				RawMethodRow rawMethodRow = this.tablesHeap.MethodTable[rid];
				rawMethodRow = new RawMethodRow((uint)value.RVA, rawMethodRow.ImplFlags, rawMethodRow.Flags, rawMethodRow.Name, rawMethodRow.Signature, rawMethodRow.ParamList);
				this.tablesHeap.MethodTable[rid] = rawMethodRow;
			}
			foreach (KeyValuePair<MethodDef, NativeMethodBody> keyValuePair2 in this.methodToNativeBody)
			{
				MethodDef key2 = keyValuePair2.Key;
				NativeMethodBody value2 = keyValuePair2.Value;
				uint rid2 = this.GetRid(key2);
				RawMethodRow rawMethodRow2 = this.tablesHeap.MethodTable[rid2];
				rawMethodRow2 = new RawMethodRow((uint)value2.RVA, rawMethodRow2.ImplFlags, rawMethodRow2.Flags, rawMethodRow2.Name, rawMethodRow2.Signature, rawMethodRow2.ParamList);
				this.tablesHeap.MethodTable[rid2] = rawMethodRow2;
			}
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x001A0B3C File Offset: 0x001A0B3C
		private void UpdateFieldRvas()
		{
			foreach (KeyValuePair<FieldDef, ByteArrayChunk> keyValuePair in this.fieldToInitialValue)
			{
				FieldDef key = keyValuePair.Key;
				ByteArrayChunk value = keyValuePair.Value;
				uint rid = this.fieldRVAInfos.Rid(key);
				RawFieldRVARow rawFieldRVARow = this.tablesHeap.FieldRVATable[rid];
				rawFieldRVARow = new RawFieldRVARow((uint)value.RVA, rawFieldRVARow.Field);
				this.tablesHeap.FieldRVATable[rid] = rawFieldRVARow;
			}
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x001A0BE8 File Offset: 0x001A0BE8
		private void Create()
		{
			this.Initialize();
			this.allTypeDefs = this.GetAllTypeDefs();
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.AllocateTypeDefRids);
			this.AllocateTypeDefRids();
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.AllocateMemberDefRids);
			this.AllocateMemberDefRids();
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.MemberDefRidsAllocated);
			this.AddModule(this.module);
			this.AddPdbDocuments();
			this.InitializeMethodDebugInformation();
			this.InitializeTypeDefsAndMemberDefs();
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.MemberDefsInitialized);
			this.InitializeVTableFixups();
			this.AddExportedTypes();
			this.InitializeEntryPoint();
			if (this.module.Assembly != null)
			{
				this.AddAssembly(this.module.Assembly, this.AssemblyPublicKey);
			}
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.BeforeSortTables);
			this.SortTables();
			this.InitializeGenericParamConstraintTable();
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.MostTablesSorted);
			this.WriteTypeDefAndMemberDefCustomAttributesAndCustomDebugInfos();
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.MemberDefCustomAttributesWritten);
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.BeginAddResources);
			this.AddResources(this.module.Resources);
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.EndAddResources);
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.BeginWriteMethodBodies);
			this.WriteMethodBodies();
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.EndWriteMethodBodies);
			this.BeforeSortingCustomAttributes();
			this.InitializeCustomAttributeAndCustomDebugInfoTables();
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.OnAllTablesSorted);
			this.EverythingInitialized();
			this.OnMetadataEvent(dnlib.DotNet.Writer.MetadataEvent.EndCreateTables);
		}

		// Token: 0x060055BC RID: 21948 RVA: 0x001A0D0C File Offset: 0x001A0D0C
		private void InitializeTypeDefsAndMemberDefs()
		{
			int num = this.allTypeDefs.Length;
			int num2 = 0;
			int num3 = 0;
			int num4 = num / 5;
			foreach (TypeDef typeDef in this.allTypeDefs)
			{
				if (num2++ == num4 && num3 < 5)
				{
					this.RaiseProgress(dnlib.DotNet.Writer.MetadataEvent.MemberDefRidsAllocated, (double)num2 / (double)num);
					num3++;
					num4 = (int)((double)num / 5.0 * (double)(num3 + 1));
				}
				if (typeDef == null)
				{
					this.Error("TypeDef is null", new object[0]);
				}
				else
				{
					uint rid = this.GetRid(typeDef);
					RawTypeDefRow rawTypeDefRow = this.tablesHeap.TypeDefTable[rid];
					rawTypeDefRow = new RawTypeDefRow((uint)typeDef.Attributes, this.stringsHeap.Add(typeDef.Name), this.stringsHeap.Add(typeDef.Namespace), (typeDef.BaseType == null) ? 0U : this.AddTypeDefOrRef(typeDef.BaseType), rawTypeDefRow.FieldList, rawTypeDefRow.MethodList);
					this.tablesHeap.TypeDefTable[rid] = rawTypeDefRow;
					this.AddGenericParams(new MDToken(Table.TypeDef, rid), typeDef.GenericParameters);
					this.AddDeclSecurities(new MDToken(Table.TypeDef, rid), typeDef.DeclSecurities);
					this.AddInterfaceImpls(rid, typeDef.Interfaces);
					this.AddClassLayout(typeDef);
					this.AddNestedType(typeDef, typeDef.DeclaringType);
					IList<FieldDef> fields = typeDef.Fields;
					int count = fields.Count;
					for (int j = 0; j < count; j++)
					{
						FieldDef fieldDef = fields[j];
						if (fieldDef == null)
						{
							this.Error("Field is null. TypeDef {0} ({1:X8})", new object[]
							{
								typeDef,
								typeDef.MDToken.Raw
							});
						}
						else
						{
							uint rid2 = this.GetRid(fieldDef);
							RawFieldRow value = new RawFieldRow((ushort)fieldDef.Attributes, this.stringsHeap.Add(fieldDef.Name), this.GetSignature(fieldDef.Signature));
							this.tablesHeap.FieldTable[rid2] = value;
							this.AddFieldLayout(fieldDef);
							this.AddFieldMarshal(new MDToken(Table.Field, rid2), fieldDef);
							this.AddFieldRVA(fieldDef);
							this.AddImplMap(new MDToken(Table.Field, rid2), fieldDef);
							this.AddConstant(new MDToken(Table.Field, rid2), fieldDef);
						}
					}
					IList<MethodDef> methods = typeDef.Methods;
					count = methods.Count;
					for (int k = 0; k < count; k++)
					{
						MethodDef methodDef = methods[k];
						if (methodDef == null)
						{
							this.Error("Method is null. TypeDef {0} ({1:X8})", new object[]
							{
								typeDef,
								typeDef.MDToken.Raw
							});
						}
						else
						{
							if (methodDef.ExportInfo != null)
							{
								this.ExportedMethods.Add(methodDef);
							}
							uint rid3 = this.GetRid(methodDef);
							RawMethodRow rawMethodRow = this.tablesHeap.MethodTable[rid3];
							rawMethodRow = new RawMethodRow(rawMethodRow.RVA, (ushort)methodDef.ImplAttributes, (ushort)methodDef.Attributes, this.stringsHeap.Add(methodDef.Name), this.GetSignature(methodDef.Signature), rawMethodRow.ParamList);
							this.tablesHeap.MethodTable[rid3] = rawMethodRow;
							this.AddGenericParams(new MDToken(Table.Method, rid3), methodDef.GenericParameters);
							this.AddDeclSecurities(new MDToken(Table.Method, rid3), methodDef.DeclSecurities);
							this.AddImplMap(new MDToken(Table.Method, rid3), methodDef);
							this.AddMethodImpls(methodDef, methodDef.Overrides);
							IList<ParamDef> paramDefs = methodDef.ParamDefs;
							int count2 = paramDefs.Count;
							for (int l = 0; l < count2; l++)
							{
								ParamDef paramDef = paramDefs[l];
								if (paramDef == null)
								{
									this.Error("Param is null. Method {0} ({1:X8})", new object[]
									{
										methodDef,
										methodDef.MDToken.Raw
									});
								}
								else
								{
									uint rid4 = this.GetRid(paramDef);
									RawParamRow value2 = new RawParamRow((ushort)paramDef.Attributes, paramDef.Sequence, this.stringsHeap.Add(paramDef.Name));
									this.tablesHeap.ParamTable[rid4] = value2;
									this.AddConstant(new MDToken(Table.Param, rid4), paramDef);
									this.AddFieldMarshal(new MDToken(Table.Param, rid4), paramDef);
								}
							}
						}
					}
					IList<EventDef> events = typeDef.Events;
					count = events.Count;
					for (int m = 0; m < count; m++)
					{
						EventDef eventDef = events[m];
						if (eventDef == null)
						{
							this.Error("Event is null. TypeDef {0} ({1:X8})", new object[]
							{
								typeDef,
								typeDef.MDToken.Raw
							});
						}
						else
						{
							uint rid5 = this.GetRid(eventDef);
							RawEventRow value3 = new RawEventRow((ushort)eventDef.Attributes, this.stringsHeap.Add(eventDef.Name), this.AddTypeDefOrRef(eventDef.EventType));
							this.tablesHeap.EventTable[rid5] = value3;
							this.AddMethodSemantics(eventDef);
						}
					}
					IList<PropertyDef> properties = typeDef.Properties;
					count = properties.Count;
					for (int n = 0; n < count; n++)
					{
						PropertyDef propertyDef = properties[n];
						if (propertyDef == null)
						{
							this.Error("Property is null. TypeDef {0} ({1:X8})", new object[]
							{
								typeDef,
								typeDef.MDToken.Raw
							});
						}
						else
						{
							uint rid6 = this.GetRid(propertyDef);
							RawPropertyRow value4 = new RawPropertyRow((ushort)propertyDef.Attributes, this.stringsHeap.Add(propertyDef.Name), this.GetSignature(propertyDef.Type));
							this.tablesHeap.PropertyTable[rid6] = value4;
							this.AddConstant(new MDToken(Table.Property, rid6), propertyDef);
							this.AddMethodSemantics(propertyDef);
						}
					}
				}
			}
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x001A1330 File Offset: 0x001A1330
		private void WriteTypeDefAndMemberDefCustomAttributesAndCustomDebugInfos()
		{
			int num = this.allTypeDefs.Length;
			int num2 = 0;
			int num3 = 0;
			int num4 = num / 5;
			foreach (TypeDef typeDef in this.allTypeDefs)
			{
				if (num2++ == num4 && num3 < 5)
				{
					this.RaiseProgress(dnlib.DotNet.Writer.MetadataEvent.MostTablesSorted, (double)num2 / (double)num);
					num3++;
					num4 = (int)((double)num / 5.0 * (double)(num3 + 1));
				}
				if (typeDef != null)
				{
					if (typeDef.HasCustomAttributes || typeDef.HasCustomDebugInfos)
					{
						uint rid = this.GetRid(typeDef);
						this.AddCustomAttributes(Table.TypeDef, rid, typeDef);
						this.AddCustomDebugInformationList(Table.TypeDef, rid, typeDef);
					}
					IList<FieldDef> fields = typeDef.Fields;
					int count = fields.Count;
					for (int j = 0; j < count; j++)
					{
						FieldDef fieldDef = fields[j];
						if (fieldDef != null && (fieldDef.HasCustomAttributes || fieldDef.HasCustomDebugInfos))
						{
							uint rid = this.GetRid(fieldDef);
							this.AddCustomAttributes(Table.Field, rid, fieldDef);
							this.AddCustomDebugInformationList(Table.Field, rid, fieldDef);
						}
					}
					IList<MethodDef> methods = typeDef.Methods;
					count = methods.Count;
					for (int k = 0; k < count; k++)
					{
						MethodDef methodDef = methods[k];
						if (methodDef != null)
						{
							if (methodDef.HasCustomAttributes)
							{
								uint rid = this.GetRid(methodDef);
								this.AddCustomAttributes(Table.Method, rid, methodDef);
							}
							IList<ParamDef> paramDefs = methodDef.ParamDefs;
							int count2 = paramDefs.Count;
							for (int l = 0; l < count2; l++)
							{
								ParamDef paramDef = paramDefs[l];
								if (paramDef != null && (paramDef.HasCustomAttributes || paramDef.HasCustomDebugInfos))
								{
									uint rid = this.GetRid(paramDef);
									this.AddCustomAttributes(Table.Param, rid, paramDef);
									this.AddCustomDebugInformationList(Table.Param, rid, paramDef);
								}
							}
						}
					}
					IList<EventDef> events = typeDef.Events;
					count = events.Count;
					for (int m = 0; m < count; m++)
					{
						EventDef eventDef = events[m];
						if (eventDef != null && (eventDef.HasCustomAttributes || eventDef.HasCustomDebugInfos))
						{
							uint rid = this.GetRid(eventDef);
							this.AddCustomAttributes(Table.Event, rid, eventDef);
							this.AddCustomDebugInformationList(Table.Event, rid, eventDef);
						}
					}
					IList<PropertyDef> properties = typeDef.Properties;
					count = properties.Count;
					for (int n = 0; n < count; n++)
					{
						PropertyDef propertyDef = properties[n];
						if (propertyDef != null && (propertyDef.HasCustomAttributes || propertyDef.HasCustomDebugInfos))
						{
							uint rid = this.GetRid(propertyDef);
							this.AddCustomAttributes(Table.Property, rid, propertyDef);
							this.AddCustomDebugInformationList(Table.Property, rid, propertyDef);
						}
					}
				}
			}
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x001A1614 File Offset: 0x001A1614
		private void InitializeVTableFixups()
		{
			VTableFixups vtableFixups = this.module.VTableFixups;
			if (vtableFixups == null || vtableFixups.VTables.Count == 0)
			{
				return;
			}
			foreach (VTable vtable in vtableFixups)
			{
				if (vtable == null)
				{
					this.Error("VTable is null", new object[0]);
				}
				else
				{
					foreach (IMethod method in vtable)
					{
						if (method != null)
						{
							this.AddMDTokenProvider(method);
						}
					}
				}
			}
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x001A16E8 File Offset: 0x001A16E8
		private void AddExportedTypes()
		{
			IList<ExportedType> exportedTypes = this.module.ExportedTypes;
			int count = exportedTypes.Count;
			for (int i = 0; i < count; i++)
			{
				this.AddExportedType(exportedTypes[i]);
			}
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x001A172C File Offset: 0x001A172C
		private void InitializeEntryPoint()
		{
			FileDef fileDef = this.module.ManagedEntryPoint as FileDef;
			if (fileDef != null)
			{
				this.AddFile(fileDef);
			}
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x001A175C File Offset: 0x001A175C
		private void SortTables()
		{
			this.classLayoutInfos.Sort((Metadata.SortedRows<TypeDef, RawClassLayoutRow>.Info a, Metadata.SortedRows<TypeDef, RawClassLayoutRow>.Info b) => a.row.Parent.CompareTo(b.row.Parent));
			this.hasConstantInfos.Sort((Metadata.SortedRows<IHasConstant, RawConstantRow>.Info a, Metadata.SortedRows<IHasConstant, RawConstantRow>.Info b) => a.row.Parent.CompareTo(b.row.Parent));
			this.declSecurityInfos.Sort((Metadata.SortedRows<DeclSecurity, RawDeclSecurityRow>.Info a, Metadata.SortedRows<DeclSecurity, RawDeclSecurityRow>.Info b) => a.row.Parent.CompareTo(b.row.Parent));
			this.fieldLayoutInfos.Sort((Metadata.SortedRows<FieldDef, RawFieldLayoutRow>.Info a, Metadata.SortedRows<FieldDef, RawFieldLayoutRow>.Info b) => a.row.Field.CompareTo(b.row.Field));
			this.fieldMarshalInfos.Sort((Metadata.SortedRows<IHasFieldMarshal, RawFieldMarshalRow>.Info a, Metadata.SortedRows<IHasFieldMarshal, RawFieldMarshalRow>.Info b) => a.row.Parent.CompareTo(b.row.Parent));
			this.fieldRVAInfos.Sort((Metadata.SortedRows<FieldDef, RawFieldRVARow>.Info a, Metadata.SortedRows<FieldDef, RawFieldRVARow>.Info b) => a.row.Field.CompareTo(b.row.Field));
			this.implMapInfos.Sort((Metadata.SortedRows<IMemberForwarded, RawImplMapRow>.Info a, Metadata.SortedRows<IMemberForwarded, RawImplMapRow>.Info b) => a.row.MemberForwarded.CompareTo(b.row.MemberForwarded));
			this.methodImplInfos.Sort((Metadata.SortedRows<MethodDef, RawMethodImplRow>.Info a, Metadata.SortedRows<MethodDef, RawMethodImplRow>.Info b) => a.row.Class.CompareTo(b.row.Class));
			this.methodSemanticsInfos.Sort((Metadata.SortedRows<MethodDef, RawMethodSemanticsRow>.Info a, Metadata.SortedRows<MethodDef, RawMethodSemanticsRow>.Info b) => a.row.Association.CompareTo(b.row.Association));
			this.nestedClassInfos.Sort((Metadata.SortedRows<TypeDef, RawNestedClassRow>.Info a, Metadata.SortedRows<TypeDef, RawNestedClassRow>.Info b) => a.row.NestedClass.CompareTo(b.row.NestedClass));
			this.genericParamInfos.Sort(delegate(Metadata.SortedRows<GenericParam, RawGenericParamRow>.Info a, Metadata.SortedRows<GenericParam, RawGenericParamRow>.Info b)
			{
				if (a.row.Owner != b.row.Owner)
				{
					return a.row.Owner.CompareTo(b.row.Owner);
				}
				return a.row.Number.CompareTo(b.row.Number);
			});
			if (this.RoslynSortInterfaceImpl)
			{
				this.interfaceImplInfos.Sort((Metadata.SortedRows<InterfaceImpl, RawInterfaceImplRow>.Info a, Metadata.SortedRows<InterfaceImpl, RawInterfaceImplRow>.Info b) => a.row.Class.CompareTo(b.row.Class));
			}
			else
			{
				this.interfaceImplInfos.Sort(delegate(Metadata.SortedRows<InterfaceImpl, RawInterfaceImplRow>.Info a, Metadata.SortedRows<InterfaceImpl, RawInterfaceImplRow>.Info b)
				{
					if (a.row.Class != b.row.Class)
					{
						return a.row.Class.CompareTo(b.row.Class);
					}
					return a.row.Interface.CompareTo(b.row.Interface);
				});
			}
			this.tablesHeap.ClassLayoutTable.IsSorted = true;
			this.tablesHeap.ConstantTable.IsSorted = true;
			this.tablesHeap.DeclSecurityTable.IsSorted = true;
			this.tablesHeap.FieldLayoutTable.IsSorted = true;
			this.tablesHeap.FieldMarshalTable.IsSorted = true;
			this.tablesHeap.FieldRVATable.IsSorted = true;
			this.tablesHeap.GenericParamTable.IsSorted = true;
			this.tablesHeap.ImplMapTable.IsSorted = true;
			this.tablesHeap.InterfaceImplTable.IsSorted = true;
			this.tablesHeap.MethodImplTable.IsSorted = true;
			this.tablesHeap.MethodSemanticsTable.IsSorted = true;
			this.tablesHeap.NestedClassTable.IsSorted = true;
			this.tablesHeap.EventMapTable.IsSorted = true;
			this.tablesHeap.PropertyMapTable.IsSorted = true;
			foreach (Metadata.SortedRows<TypeDef, RawClassLayoutRow>.Info info in this.classLayoutInfos.infos)
			{
				this.tablesHeap.ClassLayoutTable.Create(info.row);
			}
			foreach (Metadata.SortedRows<IHasConstant, RawConstantRow>.Info info2 in this.hasConstantInfos.infos)
			{
				this.tablesHeap.ConstantTable.Create(info2.row);
			}
			foreach (Metadata.SortedRows<DeclSecurity, RawDeclSecurityRow>.Info info3 in this.declSecurityInfos.infos)
			{
				this.tablesHeap.DeclSecurityTable.Create(info3.row);
			}
			foreach (Metadata.SortedRows<FieldDef, RawFieldLayoutRow>.Info info4 in this.fieldLayoutInfos.infos)
			{
				this.tablesHeap.FieldLayoutTable.Create(info4.row);
			}
			foreach (Metadata.SortedRows<IHasFieldMarshal, RawFieldMarshalRow>.Info info5 in this.fieldMarshalInfos.infos)
			{
				this.tablesHeap.FieldMarshalTable.Create(info5.row);
			}
			foreach (Metadata.SortedRows<FieldDef, RawFieldRVARow>.Info info6 in this.fieldRVAInfos.infos)
			{
				this.tablesHeap.FieldRVATable.Create(info6.row);
			}
			foreach (Metadata.SortedRows<GenericParam, RawGenericParamRow>.Info info7 in this.genericParamInfos.infos)
			{
				this.tablesHeap.GenericParamTable.Create(info7.row);
			}
			foreach (Metadata.SortedRows<IMemberForwarded, RawImplMapRow>.Info info8 in this.implMapInfos.infos)
			{
				this.tablesHeap.ImplMapTable.Create(info8.row);
			}
			foreach (Metadata.SortedRows<InterfaceImpl, RawInterfaceImplRow>.Info info9 in this.interfaceImplInfos.infos)
			{
				this.tablesHeap.InterfaceImplTable.Create(info9.row);
			}
			foreach (Metadata.SortedRows<MethodDef, RawMethodImplRow>.Info info10 in this.methodImplInfos.infos)
			{
				this.tablesHeap.MethodImplTable.Create(info10.row);
			}
			foreach (Metadata.SortedRows<MethodDef, RawMethodSemanticsRow>.Info info11 in this.methodSemanticsInfos.infos)
			{
				this.tablesHeap.MethodSemanticsTable.Create(info11.row);
			}
			foreach (Metadata.SortedRows<TypeDef, RawNestedClassRow>.Info info12 in this.nestedClassInfos.infos)
			{
				this.tablesHeap.NestedClassTable.Create(info12.row);
			}
			foreach (Metadata.SortedRows<InterfaceImpl, RawInterfaceImplRow>.Info info13 in this.interfaceImplInfos.infos)
			{
				if (info13.data.HasCustomAttributes || info13.data.HasCustomDebugInfos)
				{
					uint rid = this.interfaceImplInfos.Rid(info13.data);
					this.AddCustomAttributes(Table.InterfaceImpl, rid, info13.data);
					this.AddCustomDebugInformationList(Table.InterfaceImpl, rid, info13.data);
				}
			}
			foreach (Metadata.SortedRows<DeclSecurity, RawDeclSecurityRow>.Info info14 in this.declSecurityInfos.infos)
			{
				if (info14.data.HasCustomAttributes || info14.data.HasCustomDebugInfos)
				{
					uint rid2 = this.declSecurityInfos.Rid(info14.data);
					this.AddCustomAttributes(Table.DeclSecurity, rid2, info14.data);
					this.AddCustomDebugInformationList(Table.DeclSecurity, rid2, info14.data);
				}
			}
			foreach (Metadata.SortedRows<GenericParam, RawGenericParamRow>.Info info15 in this.genericParamInfos.infos)
			{
				if (info15.data.HasCustomAttributes || info15.data.HasCustomDebugInfos)
				{
					uint rid3 = this.genericParamInfos.Rid(info15.data);
					this.AddCustomAttributes(Table.GenericParam, rid3, info15.data);
					this.AddCustomDebugInformationList(Table.GenericParam, rid3, info15.data);
				}
			}
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x001A2110 File Offset: 0x001A2110
		private void InitializeGenericParamConstraintTable()
		{
			foreach (TypeDef typeDef in this.allTypeDefs)
			{
				if (typeDef != null)
				{
					this.AddGenericParamConstraints(typeDef.GenericParameters);
					IList<MethodDef> methods = typeDef.Methods;
					int count = methods.Count;
					for (int j = 0; j < count; j++)
					{
						MethodDef methodDef = methods[j];
						if (methodDef != null)
						{
							this.AddGenericParamConstraints(methodDef.GenericParameters);
						}
					}
				}
			}
			this.genericParamConstraintInfos.Sort((Metadata.SortedRows<GenericParamConstraint, RawGenericParamConstraintRow>.Info a, Metadata.SortedRows<GenericParamConstraint, RawGenericParamConstraintRow>.Info b) => a.row.Owner.CompareTo(b.row.Owner));
			this.tablesHeap.GenericParamConstraintTable.IsSorted = true;
			foreach (Metadata.SortedRows<GenericParamConstraint, RawGenericParamConstraintRow>.Info info in this.genericParamConstraintInfos.infos)
			{
				this.tablesHeap.GenericParamConstraintTable.Create(info.row);
			}
			foreach (Metadata.SortedRows<GenericParamConstraint, RawGenericParamConstraintRow>.Info info2 in this.genericParamConstraintInfos.infos)
			{
				if (info2.data.HasCustomAttributes || info2.data.HasCustomDebugInfos)
				{
					uint rid = this.genericParamConstraintInfos.Rid(info2.data);
					this.AddCustomAttributes(Table.GenericParamConstraint, rid, info2.data);
					this.AddCustomDebugInformationList(Table.GenericParamConstraint, rid, info2.data);
				}
			}
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x001A22D4 File Offset: 0x001A22D4
		private void InitializeCustomAttributeAndCustomDebugInfoTables()
		{
			this.customAttributeInfos.Sort((Metadata.SortedRows<CustomAttribute, RawCustomAttributeRow>.Info a, Metadata.SortedRows<CustomAttribute, RawCustomAttributeRow>.Info b) => a.row.Parent.CompareTo(b.row.Parent));
			this.tablesHeap.CustomAttributeTable.IsSorted = true;
			foreach (Metadata.SortedRows<CustomAttribute, RawCustomAttributeRow>.Info info in this.customAttributeInfos.infos)
			{
				this.tablesHeap.CustomAttributeTable.Create(info.row);
			}
			if (this.debugMetadata != null)
			{
				this.debugMetadata.stateMachineMethodInfos.Sort((Metadata.SortedRows<PdbCustomDebugInfo, RawStateMachineMethodRow>.Info a, Metadata.SortedRows<PdbCustomDebugInfo, RawStateMachineMethodRow>.Info b) => a.row.MoveNextMethod.CompareTo(b.row.MoveNextMethod));
				this.debugMetadata.tablesHeap.StateMachineMethodTable.IsSorted = true;
				foreach (Metadata.SortedRows<PdbCustomDebugInfo, RawStateMachineMethodRow>.Info info2 in this.debugMetadata.stateMachineMethodInfos.infos)
				{
					this.debugMetadata.tablesHeap.StateMachineMethodTable.Create(info2.row);
				}
				this.debugMetadata.customDebugInfos.Sort((Metadata.SortedRows<PdbCustomDebugInfo, RawCustomDebugInformationRow>.Info a, Metadata.SortedRows<PdbCustomDebugInfo, RawCustomDebugInformationRow>.Info b) => a.row.Parent.CompareTo(b.row.Parent));
				this.debugMetadata.tablesHeap.CustomDebugInformationTable.IsSorted = true;
				foreach (Metadata.SortedRows<PdbCustomDebugInfo, RawCustomDebugInformationRow>.Info info3 in this.debugMetadata.customDebugInfos.infos)
				{
					this.debugMetadata.tablesHeap.CustomDebugInformationTable.Create(info3.row);
				}
			}
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x001A24EC File Offset: 0x001A24EC
		private void WriteMethodBodies()
		{
			if (this.NoMethodBodies)
			{
				return;
			}
			int numberOfMethods = this.NumberOfMethods;
			int num = 0;
			int num2 = 0;
			int num3 = numberOfMethods / 40;
			NormalMetadata normalMetadata = this.debugMetadata;
			MethodBodyChunks methodBodyChunks = this.methodBodies;
			Dictionary<MethodDef, MethodBody> dictionary = this.methodToBody;
			List<Metadata.MethodScopeDebugInfo> list;
			List<PdbScope> list2;
			SerializerMethodContext serializerMethodContext;
			if (normalMetadata == null)
			{
				list = null;
				list2 = null;
				serializerMethodContext = null;
			}
			else
			{
				list = new List<Metadata.MethodScopeDebugInfo>();
				list2 = new List<PdbScope>();
				serializerMethodContext = this.AllocSerializerMethodContext();
			}
			bool keepOldMaxStack = this.KeepOldMaxStack;
			MethodBodyWriter methodBodyWriter = new MethodBodyWriter(this);
			foreach (TypeDef typeDef in this.allTypeDefs)
			{
				if (typeDef != null)
				{
					IList<MethodDef> methods = typeDef.Methods;
					for (int j = 0; j < methods.Count; j++)
					{
						MethodDef methodDef = methods[j];
						if (methodDef != null)
						{
							if (num++ == num3 && num2 < 40)
							{
								this.RaiseProgress(dnlib.DotNet.Writer.MetadataEvent.BeginWriteMethodBodies, (double)num / (double)numberOfMethods);
								num2++;
								num3 = (int)((double)numberOfMethods / 40.0 * (double)(num2 + 1));
							}
							uint localVarSigToken = 0U;
							CilBody body = methodDef.Body;
							if (body != null)
							{
								if (body.Instructions.Count != 0 || body.Variables.Count != 0)
								{
									methodBodyWriter.Reset(body, keepOldMaxStack || body.KeepOldMaxStack);
									methodBodyWriter.Write();
									RVA origRva = methodDef.RVA;
									uint metadataBodySize = body.MetadataBodySize;
									MethodBody value = methodBodyChunks.Add(new MethodBody(methodBodyWriter.Code, methodBodyWriter.ExtraSections, methodBodyWriter.LocalVarSigTok), origRva, metadataBodySize);
									dictionary[methodDef] = value;
									localVarSigToken = methodBodyWriter.LocalVarSigTok;
								}
							}
							else
							{
								NativeMethodBody nativeBody = methodDef.NativeBody;
								if (nativeBody != null)
								{
									this.methodToNativeBody[methodDef] = nativeBody;
								}
								else if (methodDef.MethodBody != null)
								{
									this.Error("Unsupported method body", new object[0]);
								}
							}
							if (normalMetadata != null)
							{
								uint rid = this.GetRid(methodDef);
								if (body != null)
								{
									PdbMethod pdbMethod = body.PdbMethod;
									if (pdbMethod != null && !Metadata.IsEmptyRootScope(body, pdbMethod.Scope))
									{
										serializerMethodContext.SetBody(methodDef);
										list2.Add(pdbMethod.Scope);
										while (list2.Count > 0)
										{
											PdbScope pdbScope = list2[list2.Count - 1];
											list2.RemoveAt(list2.Count - 1);
											list2.AddRange(pdbScope.Scopes);
											uint num4 = serializerMethodContext.GetOffset(pdbScope.Start);
											uint num5 = serializerMethodContext.GetOffset(pdbScope.End);
											list.Add(new Metadata.MethodScopeDebugInfo
											{
												MethodRid = rid,
												Scope = pdbScope,
												ScopeStart = num4,
												ScopeLength = num5 - num4
											});
										}
									}
								}
								this.AddCustomDebugInformationList(methodDef, rid, localVarSigToken);
							}
						}
					}
				}
			}
			if (normalMetadata != null)
			{
				list.Sort(delegate(Metadata.MethodScopeDebugInfo a, Metadata.MethodScopeDebugInfo b)
				{
					int num6 = a.MethodRid.CompareTo(b.MethodRid);
					if (num6 != 0)
					{
						return num6;
					}
					num6 = a.ScopeStart.CompareTo(b.ScopeStart);
					if (num6 != 0)
					{
						return num6;
					}
					return b.ScopeLength.CompareTo(a.ScopeLength);
				});
				foreach (Metadata.MethodScopeDebugInfo methodScopeDebugInfo in list)
				{
					uint rid2 = (uint)(normalMetadata.localScopeInfos.infos.Count + 1);
					RawLocalScopeRow row = new RawLocalScopeRow(methodScopeDebugInfo.MethodRid, this.AddImportScope(methodScopeDebugInfo.Scope.ImportScope), (uint)(normalMetadata.tablesHeap.LocalVariableTable.Rows + 1), (uint)(normalMetadata.tablesHeap.LocalConstantTable.Rows + 1), methodScopeDebugInfo.ScopeStart, methodScopeDebugInfo.ScopeLength);
					normalMetadata.localScopeInfos.Add(methodScopeDebugInfo.Scope, row);
					IList<PdbLocal> variables = methodScopeDebugInfo.Scope.Variables;
					int count = variables.Count;
					for (int k = 0; k < count; k++)
					{
						PdbLocal local = variables[k];
						this.AddLocalVariable(local);
					}
					IList<PdbConstant> list3 = methodScopeDebugInfo.Scope.Constants;
					count = list3.Count;
					for (int l = 0; l < count; l++)
					{
						PdbConstant constant = list3[l];
						this.AddLocalConstant(constant);
					}
					this.AddCustomDebugInformationList(Table.LocalScope, rid2, methodScopeDebugInfo.Scope.CustomDebugInfos);
				}
				normalMetadata.tablesHeap.LocalScopeTable.IsSorted = true;
				foreach (Metadata.SortedRows<PdbScope, RawLocalScopeRow>.Info info in normalMetadata.localScopeInfos.infos)
				{
					normalMetadata.tablesHeap.LocalScopeTable.Create(info.row);
				}
			}
			if (serializerMethodContext != null)
			{
				this.Free(ref serializerMethodContext);
			}
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x001A2A10 File Offset: 0x001A2A10
		private static bool IsEmptyRootScope(CilBody cilBody, PdbScope scope)
		{
			return scope.Variables.Count == 0 && scope.Constants.Count == 0 && scope.Namespaces.Count == 0 && scope.ImportScope == null && scope.Scopes.Count == 0 && scope.CustomDebugInfos.Count == 0 && scope.End == null && (cilBody.Instructions.Count == 0 || cilBody.Instructions[0] == scope.Start);
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x001A2AC0 File Offset: 0x001A2AC0
		protected static bool IsEmpty<T>(IList<T> list) where T : class
		{
			if (list == null)
			{
				return true;
			}
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (list[i] != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x001A2B04 File Offset: 0x001A2B04
		public MDToken GetToken(object o)
		{
			IMDTokenProvider imdtokenProvider = o as IMDTokenProvider;
			if (imdtokenProvider != null)
			{
				return new MDToken(imdtokenProvider.MDToken.Table, this.AddMDTokenProvider(imdtokenProvider));
			}
			string text = o as string;
			if (text != null)
			{
				return new MDToken((Table)112, this.usHeap.Add(text));
			}
			MethodSig methodSig = o as MethodSig;
			if (methodSig != null)
			{
				return new MDToken(Table.StandAloneSig, this.AddStandAloneSig(methodSig, methodSig.OriginalToken));
			}
			FieldSig fieldSig = o as FieldSig;
			if (fieldSig != null)
			{
				return new MDToken(Table.StandAloneSig, this.AddStandAloneSig(fieldSig, 0U));
			}
			if (o == null)
			{
				this.Error("Instruction operand is null", new object[0]);
			}
			else
			{
				this.Error("Invalid instruction operand", new object[0]);
			}
			return new MDToken((Table)255, 16777215);
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x001A2BDC File Offset: 0x001A2BDC
		public virtual MDToken GetToken(IList<TypeSig> locals, uint origToken)
		{
			if (locals == null || locals.Count == 0)
			{
				return new MDToken(Table.Module, 0);
			}
			RawStandAloneSigRow row = new RawStandAloneSigRow(this.GetSignature(new LocalSig(locals, false)));
			uint rid = this.tablesHeap.StandAloneSigTable.Add(row);
			return new MDToken(Table.StandAloneSig, rid);
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x001A2C34 File Offset: 0x001A2C34
		protected virtual uint AddStandAloneSig(MethodSig methodSig, uint origToken)
		{
			if (methodSig == null)
			{
				this.Error("StandAloneSig: MethodSig is null", new object[0]);
				return 0U;
			}
			RawStandAloneSigRow row = new RawStandAloneSigRow(this.GetSignature(methodSig));
			return this.tablesHeap.StandAloneSigTable.Add(row);
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x001A2C80 File Offset: 0x001A2C80
		protected virtual uint AddStandAloneSig(FieldSig fieldSig, uint origToken)
		{
			if (fieldSig == null)
			{
				this.Error("StandAloneSig: FieldSig is null", new object[0]);
				return 0U;
			}
			RawStandAloneSigRow row = new RawStandAloneSigRow(this.GetSignature(fieldSig));
			return this.tablesHeap.StandAloneSigTable.Add(row);
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x001A2CCC File Offset: 0x001A2CCC
		private uint AddMDTokenProvider(IMDTokenProvider tp)
		{
			if (tp != null)
			{
				switch (tp.MDToken.Table)
				{
				case Table.Module:
					return this.AddModule((ModuleDef)tp);
				case Table.TypeRef:
					return this.AddTypeRef((TypeRef)tp);
				case Table.TypeDef:
					return this.GetRid((TypeDef)tp);
				case Table.Field:
					return this.GetRid((FieldDef)tp);
				case Table.Method:
					return this.GetRid((MethodDef)tp);
				case Table.Param:
					return this.GetRid((ParamDef)tp);
				case Table.MemberRef:
					return this.AddMemberRef((MemberRef)tp);
				case Table.StandAloneSig:
					return this.AddStandAloneSig((StandAloneSig)tp);
				case Table.Event:
					return this.GetRid((EventDef)tp);
				case Table.Property:
					return this.GetRid((PropertyDef)tp);
				case Table.ModuleRef:
					return this.AddModuleRef((ModuleRef)tp);
				case Table.TypeSpec:
					return this.AddTypeSpec((TypeSpec)tp);
				case Table.Assembly:
					return this.AddAssembly((AssemblyDef)tp, null);
				case Table.AssemblyRef:
					return this.AddAssemblyRef((AssemblyRef)tp);
				case Table.File:
					return this.AddFile((FileDef)tp);
				case Table.ExportedType:
					return this.AddExportedType((ExportedType)tp);
				case Table.MethodSpec:
					return this.AddMethodSpec((MethodSpec)tp);
				}
			}
			if (tp == null)
			{
				this.Error("IMDTokenProvider is null", new object[0]);
			}
			else
			{
				this.Error("Invalid IMDTokenProvider", new object[0]);
			}
			return 0U;
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x001A2EEC File Offset: 0x001A2EEC
		protected uint AddTypeDefOrRef(ITypeDefOrRef tdr)
		{
			if (tdr == null)
			{
				this.Error("TypeDefOrRef is null", new object[0]);
				return 0U;
			}
			MDToken token = new MDToken(tdr.MDToken.Table, this.AddMDTokenProvider(tdr));
			uint result;
			if (!CodedToken.TypeDefOrRef.Encode(token, out result))
			{
				this.Error("Can't encode TypeDefOrRef token {0:X8}", new object[]
				{
					token.Raw
				});
				result = 0U;
			}
			return result;
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x001A2F68 File Offset: 0x001A2F68
		protected uint AddResolutionScope(IResolutionScope rs)
		{
			if (rs == null)
			{
				this.Error("ResolutionScope is null", new object[0]);
				return 0U;
			}
			MDToken token = new MDToken(rs.MDToken.Table, this.AddMDTokenProvider(rs));
			uint result;
			if (!CodedToken.ResolutionScope.Encode(token, out result))
			{
				this.Error("Can't encode ResolutionScope token {0:X8}", new object[]
				{
					token.Raw
				});
				result = 0U;
			}
			return result;
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x001A2FE4 File Offset: 0x001A2FE4
		protected uint AddMethodDefOrRef(IMethodDefOrRef mdr)
		{
			if (mdr == null)
			{
				this.Error("MethodDefOrRef is null", new object[0]);
				return 0U;
			}
			MDToken token = new MDToken(mdr.MDToken.Table, this.AddMDTokenProvider(mdr));
			uint result;
			if (!CodedToken.MethodDefOrRef.Encode(token, out result))
			{
				this.Error("Can't encode MethodDefOrRef token {0:X8}", new object[]
				{
					token.Raw
				});
				result = 0U;
			}
			return result;
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x001A3060 File Offset: 0x001A3060
		protected uint AddMemberRefParent(IMemberRefParent parent)
		{
			if (parent == null)
			{
				this.Error("MemberRefParent is null", new object[0]);
				return 0U;
			}
			MDToken token = new MDToken(parent.MDToken.Table, this.AddMDTokenProvider(parent));
			uint result;
			if (!CodedToken.MemberRefParent.Encode(token, out result))
			{
				this.Error("Can't encode MemberRefParent token {0:X8}", new object[]
				{
					token.Raw
				});
				result = 0U;
			}
			return result;
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x001A30DC File Offset: 0x001A30DC
		protected uint AddImplementation(IImplementation impl)
		{
			if (impl == null)
			{
				this.Error("Implementation is null", new object[0]);
				return 0U;
			}
			MDToken token = new MDToken(impl.MDToken.Table, this.AddMDTokenProvider(impl));
			uint result;
			if (!CodedToken.Implementation.Encode(token, out result))
			{
				this.Error("Can't encode Implementation token {0:X8}", new object[]
				{
					token.Raw
				});
				result = 0U;
			}
			return result;
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x001A3158 File Offset: 0x001A3158
		protected uint AddCustomAttributeType(ICustomAttributeType cat)
		{
			if (cat == null)
			{
				this.Error("CustomAttributeType is null", new object[0]);
				return 0U;
			}
			MDToken token = new MDToken(cat.MDToken.Table, this.AddMDTokenProvider(cat));
			uint result;
			if (!CodedToken.CustomAttributeType.Encode(token, out result))
			{
				this.Error("Can't encode CustomAttributeType token {0:X8}", new object[]
				{
					token.Raw
				});
				result = 0U;
			}
			return result;
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x001A31D4 File Offset: 0x001A31D4
		protected void AddNestedType(TypeDef nestedType, TypeDef declaringType)
		{
			if (nestedType == null || declaringType == null)
			{
				return;
			}
			uint rid = this.GetRid(nestedType);
			uint rid2 = this.GetRid(declaringType);
			if (rid == 0U || rid2 == 0U)
			{
				return;
			}
			RawNestedClassRow row = new RawNestedClassRow(rid, rid2);
			this.nestedClassInfos.Add(declaringType, row);
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x001A3228 File Offset: 0x001A3228
		protected uint AddModule(ModuleDef module)
		{
			if (module == null)
			{
				this.Error("Module is null", new object[0]);
				return 0U;
			}
			if (this.module != module)
			{
				this.Error("Module {0} must be referenced with a ModuleRef, not a ModuleDef", new object[]
				{
					module
				});
			}
			uint num;
			if (this.moduleDefInfos.TryGetRid(module, out num))
			{
				return num;
			}
			RawModuleRow row = new RawModuleRow(module.Generation, this.stringsHeap.Add(module.Name), this.guidHeap.Add(module.Mvid), this.guidHeap.Add(module.EncId), this.guidHeap.Add(module.EncBaseId));
			num = this.tablesHeap.ModuleTable.Add(row);
			this.moduleDefInfos.Add(module, num);
			this.AddCustomAttributes(Table.Module, num, module);
			this.AddCustomDebugInformationList(Table.Module, num, module);
			return num;
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x001A330C File Offset: 0x001A330C
		protected uint AddModuleRef(ModuleRef modRef)
		{
			if (modRef == null)
			{
				this.Error("ModuleRef is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.moduleRefInfos.TryGetRid(modRef, out num))
			{
				return num;
			}
			RawModuleRefRow row = new RawModuleRefRow(this.stringsHeap.Add(modRef.Name));
			num = this.tablesHeap.ModuleRefTable.Add(row);
			this.moduleRefInfos.Add(modRef, num);
			this.AddCustomAttributes(Table.ModuleRef, num, modRef);
			this.AddCustomDebugInformationList(Table.ModuleRef, num, modRef);
			return num;
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x001A3398 File Offset: 0x001A3398
		protected uint AddAssemblyRef(AssemblyRef asmRef)
		{
			if (asmRef == null)
			{
				this.Error("AssemblyRef is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.assemblyRefInfos.TryGetRid(asmRef, out num))
			{
				return num;
			}
			Version version = Utils.CreateVersionWithNoUndefinedValues(asmRef.Version);
			RawAssemblyRefRow row = new RawAssemblyRefRow((ushort)version.Major, (ushort)version.Minor, (ushort)version.Build, (ushort)version.Revision, (uint)asmRef.Attributes, this.blobHeap.Add(PublicKeyBase.GetRawData(asmRef.PublicKeyOrToken)), this.stringsHeap.Add(asmRef.Name), this.stringsHeap.Add(asmRef.Culture), this.blobHeap.Add(asmRef.Hash));
			num = this.tablesHeap.AssemblyRefTable.Add(row);
			this.assemblyRefInfos.Add(asmRef, num);
			this.AddCustomAttributes(Table.AssemblyRef, num, asmRef);
			this.AddCustomDebugInformationList(Table.AssemblyRef, num, asmRef);
			return num;
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x001A348C File Offset: 0x001A348C
		protected uint AddAssembly(AssemblyDef asm, byte[] publicKey)
		{
			if (asm == null)
			{
				this.Error("Assembly is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.assemblyInfos.TryGetRid(asm, out num))
			{
				return num;
			}
			AssemblyAttributes assemblyAttributes = asm.Attributes;
			if (publicKey != null)
			{
				assemblyAttributes |= AssemblyAttributes.PublicKey;
			}
			else
			{
				publicKey = PublicKeyBase.GetRawData(asm.PublicKeyOrToken);
			}
			Version version = Utils.CreateVersionWithNoUndefinedValues(asm.Version);
			RawAssemblyRow row = new RawAssemblyRow((uint)asm.HashAlgorithm, (ushort)version.Major, (ushort)version.Minor, (ushort)version.Build, (ushort)version.Revision, (uint)assemblyAttributes, this.blobHeap.Add(publicKey), this.stringsHeap.Add(asm.Name), this.stringsHeap.Add(asm.Culture));
			num = this.tablesHeap.AssemblyTable.Add(row);
			this.assemblyInfos.Add(asm, num);
			this.AddDeclSecurities(new MDToken(Table.Assembly, num), asm.DeclSecurities);
			this.AddCustomAttributes(Table.Assembly, num, asm);
			this.AddCustomDebugInformationList(Table.Assembly, num, asm);
			return num;
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x001A359C File Offset: 0x001A359C
		protected void AddGenericParams(MDToken token, IList<GenericParam> gps)
		{
			if (gps == null)
			{
				return;
			}
			int count = gps.Count;
			for (int i = 0; i < count; i++)
			{
				this.AddGenericParam(token, gps[i]);
			}
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x001A35D8 File Offset: 0x001A35D8
		protected void AddGenericParam(MDToken owner, GenericParam gp)
		{
			if (gp == null)
			{
				this.Error("GenericParam is null", new object[0]);
				return;
			}
			uint owner2;
			if (!CodedToken.TypeOrMethodDef.Encode(owner, out owner2))
			{
				this.Error("Can't encode TypeOrMethodDef token {0:X8}", new object[]
				{
					owner.Raw
				});
				owner2 = 0U;
			}
			RawGenericParamRow row = new RawGenericParamRow(gp.Number, (ushort)gp.Flags, owner2, this.stringsHeap.Add(gp.Name), (gp.Kind == null) ? 0U : this.AddTypeDefOrRef(gp.Kind));
			this.genericParamInfos.Add(gp, row);
		}

		// Token: 0x060055D9 RID: 21977 RVA: 0x001A3684 File Offset: 0x001A3684
		private void AddGenericParamConstraints(IList<GenericParam> gps)
		{
			if (gps == null)
			{
				return;
			}
			int count = gps.Count;
			for (int i = 0; i < count; i++)
			{
				GenericParam genericParam = gps[i];
				if (genericParam != null)
				{
					uint gpRid = this.genericParamInfos.Rid(genericParam);
					this.AddGenericParamConstraints(gpRid, genericParam.GenericParamConstraints);
				}
			}
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x001A36DC File Offset: 0x001A36DC
		protected void AddGenericParamConstraints(uint gpRid, IList<GenericParamConstraint> constraints)
		{
			if (constraints == null)
			{
				return;
			}
			int count = constraints.Count;
			for (int i = 0; i < count; i++)
			{
				this.AddGenericParamConstraint(gpRid, constraints[i]);
			}
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x001A3718 File Offset: 0x001A3718
		protected void AddGenericParamConstraint(uint gpRid, GenericParamConstraint gpc)
		{
			if (gpc == null)
			{
				this.Error("GenericParamConstraint is null", new object[0]);
				return;
			}
			RawGenericParamConstraintRow row = new RawGenericParamConstraintRow(gpRid, this.AddTypeDefOrRef(gpc.Constraint));
			this.genericParamConstraintInfos.Add(gpc, row);
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x001A3764 File Offset: 0x001A3764
		protected void AddInterfaceImpls(uint typeDefRid, IList<InterfaceImpl> ifaces)
		{
			int count = ifaces.Count;
			for (int i = 0; i < count; i++)
			{
				InterfaceImpl interfaceImpl = ifaces[i];
				if (interfaceImpl != null)
				{
					RawInterfaceImplRow row = new RawInterfaceImplRow(typeDefRid, this.AddTypeDefOrRef(interfaceImpl.Interface));
					this.interfaceImplInfos.Add(interfaceImpl, row);
				}
			}
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x001A37BC File Offset: 0x001A37BC
		protected void AddFieldLayout(FieldDef field)
		{
			if (field == null || field.FieldOffset == null)
			{
				return;
			}
			uint rid = this.GetRid(field);
			RawFieldLayoutRow row = new RawFieldLayoutRow(field.FieldOffset.Value, rid);
			this.fieldLayoutInfos.Add(field, row);
		}

		// Token: 0x060055DE RID: 21982 RVA: 0x001A3814 File Offset: 0x001A3814
		protected void AddFieldMarshal(MDToken parent, IHasFieldMarshal hfm)
		{
			if (hfm == null || hfm.MarshalType == null)
			{
				return;
			}
			MarshalType marshalType = hfm.MarshalType;
			uint parent2;
			if (!CodedToken.HasFieldMarshal.Encode(parent, out parent2))
			{
				this.Error("Can't encode HasFieldMarshal token {0:X8}", new object[]
				{
					parent.Raw
				});
				parent2 = 0U;
			}
			RawFieldMarshalRow row = new RawFieldMarshalRow(parent2, this.blobHeap.Add(MarshalBlobWriter.Write(this.module, marshalType, this, this.OptimizeCustomAttributeSerializedTypeNames)));
			this.fieldMarshalInfos.Add(hfm, row);
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x001A38A8 File Offset: 0x001A38A8
		protected void AddFieldRVA(FieldDef field)
		{
			if (this.NoFieldData)
			{
				return;
			}
			if (field.RVA != (RVA)0U && this.KeepFieldRVA)
			{
				uint rid = this.GetRid(field);
				RawFieldRVARow row = new RawFieldRVARow((uint)field.RVA, rid);
				this.fieldRVAInfos.Add(field, row);
				return;
			}
			if (field == null || field.InitialValue == null)
			{
				return;
			}
			byte[] initialValue = field.InitialValue;
			if (!Metadata.VerifyFieldSize(field, initialValue.Length))
			{
				this.Error("Field {0} ({1:X8}) initial value size != size of field type", new object[]
				{
					field,
					field.MDToken.Raw
				});
			}
			uint rid2 = this.GetRid(field);
			ByteArrayChunk value = this.constants.Add(new ByteArrayChunk(initialValue), 8U);
			this.fieldToInitialValue[field] = value;
			RawFieldRVARow row2 = new RawFieldRVARow(0U, rid2);
			this.fieldRVAInfos.Add(field, row2);
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x001A3994 File Offset: 0x001A3994
		private static bool VerifyFieldSize(FieldDef field, int size)
		{
			return field != null && field.FieldSig != null && (ulong)field.GetFieldSize() == (ulong)((long)size);
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x001A39B8 File Offset: 0x001A39B8
		protected void AddImplMap(MDToken parent, IMemberForwarded mf)
		{
			if (mf == null || mf.ImplMap == null)
			{
				return;
			}
			ImplMap implMap = mf.ImplMap;
			uint memberForwarded;
			if (!CodedToken.MemberForwarded.Encode(parent, out memberForwarded))
			{
				this.Error("Can't encode MemberForwarded token {0:X8}", new object[]
				{
					parent.Raw
				});
				memberForwarded = 0U;
			}
			RawImplMapRow row = new RawImplMapRow((ushort)implMap.Attributes, memberForwarded, this.stringsHeap.Add(implMap.Name), this.AddModuleRef(implMap.Module));
			this.implMapInfos.Add(mf, row);
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x001A3A50 File Offset: 0x001A3A50
		protected void AddConstant(MDToken parent, IHasConstant hc)
		{
			if (hc == null || hc.Constant == null)
			{
				return;
			}
			Constant constant = hc.Constant;
			uint parent2;
			if (!CodedToken.HasConstant.Encode(parent, out parent2))
			{
				this.Error("Can't encode HasConstant token {0:X8}", new object[]
				{
					parent.Raw
				});
				parent2 = 0U;
			}
			RawConstantRow row = new RawConstantRow((byte)constant.Type, 0, parent2, this.blobHeap.Add(this.GetConstantValueAsByteArray(constant.Type, constant.Value)));
			this.hasConstantInfos.Add(hc, row);
		}

		// Token: 0x060055E3 RID: 21987 RVA: 0x001A3AE8 File Offset: 0x001A3AE8
		private byte[] GetConstantValueAsByteArray(ElementType etype, object o)
		{
			if (o != null)
			{
				TypeCode typeCode = Type.GetTypeCode(o.GetType());
				switch (typeCode)
				{
				case TypeCode.Boolean:
					this.VerifyConstantType(etype, ElementType.Boolean);
					return BitConverter.GetBytes((bool)o);
				case TypeCode.Char:
					this.VerifyConstantType(etype, ElementType.Char);
					return BitConverter.GetBytes((char)o);
				case TypeCode.SByte:
					this.VerifyConstantType(etype, ElementType.I1);
					return new byte[]
					{
						(byte)((sbyte)o)
					};
				case TypeCode.Byte:
					this.VerifyConstantType(etype, ElementType.U1);
					return new byte[]
					{
						(byte)o
					};
				case TypeCode.Int16:
					this.VerifyConstantType(etype, ElementType.I2);
					return BitConverter.GetBytes((short)o);
				case TypeCode.UInt16:
					this.VerifyConstantType(etype, ElementType.U2);
					return BitConverter.GetBytes((ushort)o);
				case TypeCode.Int32:
					this.VerifyConstantType(etype, ElementType.I4);
					return BitConverter.GetBytes((int)o);
				case TypeCode.UInt32:
					this.VerifyConstantType(etype, ElementType.U4);
					return BitConverter.GetBytes((uint)o);
				case TypeCode.Int64:
					this.VerifyConstantType(etype, ElementType.I8);
					return BitConverter.GetBytes((long)o);
				case TypeCode.UInt64:
					this.VerifyConstantType(etype, ElementType.U8);
					return BitConverter.GetBytes((ulong)o);
				case TypeCode.Single:
					this.VerifyConstantType(etype, ElementType.R4);
					return BitConverter.GetBytes((float)o);
				case TypeCode.Double:
					this.VerifyConstantType(etype, ElementType.R8);
					return BitConverter.GetBytes((double)o);
				case TypeCode.String:
					this.VerifyConstantType(etype, ElementType.String);
					return Encoding.Unicode.GetBytes((string)o);
				}
				this.Error("Invalid constant type: {0}", new object[]
				{
					typeCode
				});
				return Metadata.constantDefaultByteArray;
			}
			if (etype == ElementType.Class)
			{
				return Metadata.constantClassByteArray;
			}
			this.Error("Constant is null", new object[0]);
			return Metadata.constantDefaultByteArray;
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x001A3CB4 File Offset: 0x001A3CB4
		private void VerifyConstantType(ElementType realType, ElementType expectedType)
		{
			if (realType != expectedType)
			{
				this.Error("Constant value's type is the wrong type: {0} != {1}", new object[]
				{
					realType,
					expectedType
				});
			}
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x001A3CE0 File Offset: 0x001A3CE0
		protected void AddDeclSecurities(MDToken parent, IList<DeclSecurity> declSecurities)
		{
			if (declSecurities == null)
			{
				return;
			}
			uint parent2;
			if (!CodedToken.HasDeclSecurity.Encode(parent, out parent2))
			{
				this.Error("Can't encode HasDeclSecurity token {0:X8}", new object[]
				{
					parent.Raw
				});
				parent2 = 0U;
			}
			DataWriterContext context = this.AllocBinaryWriterContext();
			int count = declSecurities.Count;
			for (int i = 0; i < count; i++)
			{
				DeclSecurity declSecurity = declSecurities[i];
				if (declSecurity != null)
				{
					RawDeclSecurityRow row = new RawDeclSecurityRow((short)declSecurity.Action, parent2, this.blobHeap.Add(DeclSecurityWriter.Write(this.module, declSecurity.SecurityAttributes, this, this.OptimizeCustomAttributeSerializedTypeNames, context)));
					this.declSecurityInfos.Add(declSecurity, row);
				}
			}
			this.Free(ref context);
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x001A3DA4 File Offset: 0x001A3DA4
		protected void AddMethodSemantics(EventDef evt)
		{
			if (evt == null)
			{
				this.Error("Event is null", new object[0]);
				return;
			}
			uint rid = this.GetRid(evt);
			if (rid == 0U)
			{
				return;
			}
			MDToken owner = new MDToken(Table.Event, rid);
			this.AddMethodSemantics(owner, evt.AddMethod, MethodSemanticsAttributes.AddOn);
			this.AddMethodSemantics(owner, evt.RemoveMethod, MethodSemanticsAttributes.RemoveOn);
			this.AddMethodSemantics(owner, evt.InvokeMethod, MethodSemanticsAttributes.Fire);
			this.AddMethodSemantics(owner, evt.OtherMethods, MethodSemanticsAttributes.Other);
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x001A3E20 File Offset: 0x001A3E20
		protected void AddMethodSemantics(PropertyDef prop)
		{
			if (prop == null)
			{
				this.Error("Property is null", new object[0]);
				return;
			}
			uint rid = this.GetRid(prop);
			if (rid == 0U)
			{
				return;
			}
			MDToken owner = new MDToken(Table.Property, rid);
			this.AddMethodSemantics(owner, prop.GetMethods, MethodSemanticsAttributes.Getter);
			this.AddMethodSemantics(owner, prop.SetMethods, MethodSemanticsAttributes.Setter);
			this.AddMethodSemantics(owner, prop.OtherMethods, MethodSemanticsAttributes.Other);
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x001A3E8C File Offset: 0x001A3E8C
		private void AddMethodSemantics(MDToken owner, IList<MethodDef> methods, MethodSemanticsAttributes attrs)
		{
			if (methods == null)
			{
				return;
			}
			int count = methods.Count;
			for (int i = 0; i < count; i++)
			{
				this.AddMethodSemantics(owner, methods[i], attrs);
			}
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x001A3ECC File Offset: 0x001A3ECC
		private void AddMethodSemantics(MDToken owner, MethodDef method, MethodSemanticsAttributes flags)
		{
			if (method == null)
			{
				return;
			}
			uint rid = this.GetRid(method);
			if (rid == 0U)
			{
				return;
			}
			uint association;
			if (!CodedToken.HasSemantic.Encode(owner, out association))
			{
				this.Error("Can't encode HasSemantic token {0:X8}", new object[]
				{
					owner.Raw
				});
				association = 0U;
			}
			RawMethodSemanticsRow row = new RawMethodSemanticsRow((ushort)flags, rid, association);
			this.methodSemanticsInfos.Add(method, row);
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x001A3F40 File Offset: 0x001A3F40
		private void AddMethodImpls(MethodDef method, IList<MethodOverride> overrides)
		{
			if (overrides == null)
			{
				return;
			}
			if (method.DeclaringType == null)
			{
				this.Error("Method declaring type is null. Method {0} ({1:X8})", new object[]
				{
					method,
					method.MDToken.Raw
				});
				return;
			}
			if (overrides.Count != 0)
			{
				uint rid = this.GetRid(method.DeclaringType);
				int count = overrides.Count;
				for (int i = 0; i < count; i++)
				{
					MethodOverride methodOverride = overrides[i];
					RawMethodImplRow row = new RawMethodImplRow(rid, this.AddMethodDefOrRef(methodOverride.MethodBody), this.AddMethodDefOrRef(methodOverride.MethodDeclaration));
					this.methodImplInfos.Add(method, row);
				}
			}
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x001A3FF8 File Offset: 0x001A3FF8
		protected void AddClassLayout(TypeDef type)
		{
			if (type == null || type.ClassLayout == null)
			{
				return;
			}
			uint rid = this.GetRid(type);
			ClassLayout classLayout = type.ClassLayout;
			RawClassLayoutRow row = new RawClassLayoutRow(classLayout.PackingSize, classLayout.ClassSize, rid);
			this.classLayoutInfos.Add(type, row);
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x001A404C File Offset: 0x001A404C
		private void AddResources(IList<Resource> resources)
		{
			if (this.NoDotNetResources)
			{
				return;
			}
			if (resources == null)
			{
				return;
			}
			int count = resources.Count;
			for (int i = 0; i < count; i++)
			{
				this.AddResource(resources[i]);
			}
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x001A4094 File Offset: 0x001A4094
		private void AddResource(Resource resource)
		{
			EmbeddedResource embeddedResource = resource as EmbeddedResource;
			if (embeddedResource != null)
			{
				this.AddEmbeddedResource(embeddedResource);
				return;
			}
			AssemblyLinkedResource assemblyLinkedResource = resource as AssemblyLinkedResource;
			if (assemblyLinkedResource != null)
			{
				this.AddAssemblyLinkedResource(assemblyLinkedResource);
				return;
			}
			LinkedResource linkedResource = resource as LinkedResource;
			if (linkedResource != null)
			{
				this.AddLinkedResource(linkedResource);
				return;
			}
			if (resource == null)
			{
				this.Error("Resource is null", new object[0]);
				return;
			}
			this.Error("Invalid resource type: {0}", new object[]
			{
				resource.GetType()
			});
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x001A411C File Offset: 0x001A411C
		private uint AddEmbeddedResource(EmbeddedResource er)
		{
			if (er == null)
			{
				this.Error("EmbeddedResource is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.manifestResourceInfos.TryGetRid(er, out num))
			{
				return num;
			}
			RawManifestResourceRow row = new RawManifestResourceRow(this.netResources.NextOffset, (uint)er.Attributes, this.stringsHeap.Add(er.Name), 0U);
			num = this.tablesHeap.ManifestResourceTable.Add(row);
			this.manifestResourceInfos.Add(er, num);
			this.embeddedResourceToByteArray[er] = this.netResources.Add(er.CreateReader());
			return num;
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x001A41C4 File Offset: 0x001A41C4
		private uint AddAssemblyLinkedResource(AssemblyLinkedResource alr)
		{
			if (alr == null)
			{
				this.Error("AssemblyLinkedResource is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.manifestResourceInfos.TryGetRid(alr, out num))
			{
				return num;
			}
			RawManifestResourceRow row = new RawManifestResourceRow(0U, (uint)alr.Attributes, this.stringsHeap.Add(alr.Name), this.AddImplementation(alr.Assembly));
			num = this.tablesHeap.ManifestResourceTable.Add(row);
			this.manifestResourceInfos.Add(alr, num);
			return num;
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x001A4250 File Offset: 0x001A4250
		private uint AddLinkedResource(LinkedResource lr)
		{
			if (lr == null)
			{
				this.Error("LinkedResource is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.manifestResourceInfos.TryGetRid(lr, out num))
			{
				return num;
			}
			RawManifestResourceRow row = new RawManifestResourceRow(0U, (uint)lr.Attributes, this.stringsHeap.Add(lr.Name), this.AddImplementation(lr.File));
			num = this.tablesHeap.ManifestResourceTable.Add(row);
			this.manifestResourceInfos.Add(lr, num);
			return num;
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x001A42DC File Offset: 0x001A42DC
		protected uint AddFile(FileDef file)
		{
			if (file == null)
			{
				this.Error("FileDef is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.fileDefInfos.TryGetRid(file, out num))
			{
				return num;
			}
			RawFileRow row = new RawFileRow((uint)file.Flags, this.stringsHeap.Add(file.Name), this.blobHeap.Add(file.HashValue));
			num = this.tablesHeap.FileTable.Add(row);
			this.fileDefInfos.Add(file, num);
			this.AddCustomAttributes(Table.File, num, file);
			this.AddCustomDebugInformationList(Table.File, num, file);
			return num;
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x001A4380 File Offset: 0x001A4380
		protected uint AddExportedType(ExportedType et)
		{
			if (et == null)
			{
				this.Error("ExportedType is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.exportedTypeInfos.TryGetRid(et, out num))
			{
				return num;
			}
			this.exportedTypeInfos.Add(et, 0U);
			RawExportedTypeRow row = new RawExportedTypeRow((uint)et.Attributes, et.TypeDefId, this.stringsHeap.Add(et.TypeName), this.stringsHeap.Add(et.TypeNamespace), this.AddImplementation(et.Implementation));
			num = this.tablesHeap.ExportedTypeTable.Add(row);
			this.exportedTypeInfos.SetRid(et, num);
			this.AddCustomAttributes(Table.ExportedType, num, et);
			this.AddCustomDebugInformationList(Table.ExportedType, num, et);
			return num;
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x001A4444 File Offset: 0x001A4444
		protected uint GetSignature(TypeSig ts, byte[] extraData)
		{
			byte[] data;
			if (ts == null)
			{
				this.Error("TypeSig is null", new object[0]);
				data = null;
			}
			else
			{
				DataWriterContext context = this.AllocBinaryWriterContext();
				data = SignatureWriter.Write(this, ts, context);
				this.Free(ref context);
			}
			this.AppendExtraData(ref data, extraData);
			return this.blobHeap.Add(data);
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x001A44A0 File Offset: 0x001A44A0
		protected uint GetSignature(CallingConventionSig sig)
		{
			if (sig == null)
			{
				this.Error("CallingConventionSig is null", new object[0]);
				return 0U;
			}
			DataWriterContext context = this.AllocBinaryWriterContext();
			byte[] data = SignatureWriter.Write(this, sig, context);
			this.Free(ref context);
			this.AppendExtraData(ref data, sig.ExtraData);
			return this.blobHeap.Add(data);
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x001A44FC File Offset: 0x001A44FC
		private void AppendExtraData(ref byte[] blob, byte[] extraData)
		{
			if (this.PreserveExtraSignatureData && extraData != null && extraData.Length != 0)
			{
				int num = (blob == null) ? 0 : blob.Length;
				Array.Resize<byte>(ref blob, num + extraData.Length);
				Array.Copy(extraData, 0, blob, num, extraData.Length);
			}
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x001A4550 File Offset: 0x001A4550
		protected void AddCustomAttributes(Table table, uint rid, IHasCustomAttribute hca)
		{
			this.AddCustomAttributes(table, rid, hca.CustomAttributes);
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x001A4560 File Offset: 0x001A4560
		private void AddCustomAttributes(Table table, uint rid, CustomAttributeCollection caList)
		{
			MDToken token = new MDToken(table, rid);
			int count = caList.Count;
			for (int i = 0; i < count; i++)
			{
				this.AddCustomAttribute(token, caList[i]);
			}
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x001A45A0 File Offset: 0x001A45A0
		private void AddCustomAttribute(MDToken token, CustomAttribute ca)
		{
			if (ca == null)
			{
				this.Error("Custom attribute is null", new object[0]);
				return;
			}
			uint parent;
			if (!CodedToken.HasCustomAttribute.Encode(token, out parent))
			{
				this.Error("Can't encode HasCustomAttribute token {0:X8}", new object[]
				{
					token.Raw
				});
				parent = 0U;
			}
			DataWriterContext context = this.AllocBinaryWriterContext();
			byte[] data = CustomAttributeWriter.Write(this, ca, context);
			this.Free(ref context);
			RawCustomAttributeRow row = new RawCustomAttributeRow(parent, this.AddCustomAttributeType(ca.Constructor), this.blobHeap.Add(data));
			this.customAttributeInfos.Add(ca, row);
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x001A4644 File Offset: 0x001A4644
		private void AddCustomDebugInformationList(MethodDef method, uint rid, uint localVarSigToken)
		{
			if (this.debugMetadata == null)
			{
				return;
			}
			SerializerMethodContext serializerMethodContext = this.AllocSerializerMethodContext();
			serializerMethodContext.SetBody(method);
			if (method.CustomDebugInfos.Count != 0)
			{
				this.AddCustomDebugInformationCore(serializerMethodContext, Table.Method, rid, method.CustomDebugInfos);
			}
			this.AddMethodDebugInformation(method, rid, localVarSigToken);
			this.Free(ref serializerMethodContext);
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x001A46A0 File Offset: 0x001A46A0
		private void AddMethodDebugInformation(MethodDef method, uint rid, uint localVarSigToken)
		{
			CilBody body = method.Body;
			if (body == null)
			{
				return;
			}
			PdbDocument pdbDocument;
			PdbDocument pdbDocument2;
			bool flag;
			Metadata.GetSingleDocument(body, out pdbDocument, out pdbDocument2, out flag);
			if (flag)
			{
				return;
			}
			DataWriterContext dataWriterContext = this.AllocBinaryWriterContext();
			MemoryStream outStream = dataWriterContext.OutStream;
			DataWriter writer = dataWriterContext.Writer;
			outStream.SetLength(0L);
			outStream.Position = 0L;
			writer.WriteCompressedUInt32(localVarSigToken);
			if (pdbDocument == null)
			{
				writer.WriteCompressedUInt32(this.VerifyGetRid(pdbDocument2));
			}
			IList<Instruction> instructions = body.Instructions;
			PdbDocument pdbDocument3 = pdbDocument2;
			uint num = uint.MaxValue;
			int num2 = -1;
			int num3 = 0;
			uint num4 = 0U;
			int i = 0;
			while (i < instructions.Count)
			{
				Instruction instruction = instructions[i];
				SequencePoint sequencePoint = instruction.SequencePoint;
				if (sequencePoint != null)
				{
					if (sequencePoint.Document == null)
					{
						this.Error("PDB document is null", new object[0]);
						return;
					}
					if (pdbDocument3 != sequencePoint.Document)
					{
						pdbDocument3 = sequencePoint.Document;
						writer.WriteCompressedUInt32(0U);
						writer.WriteCompressedUInt32(this.VerifyGetRid(pdbDocument3));
					}
					if (num == 4294967295U)
					{
						writer.WriteCompressedUInt32(num4);
					}
					else
					{
						writer.WriteCompressedUInt32(num4 - num);
					}
					num = num4;
					if (sequencePoint.StartLine == 16707566 && sequencePoint.EndLine == 16707566)
					{
						writer.WriteCompressedUInt32(0U);
						writer.WriteCompressedUInt32(0U);
					}
					else
					{
						uint num5 = (uint)(sequencePoint.EndLine - sequencePoint.StartLine);
						int value = sequencePoint.EndColumn - sequencePoint.StartColumn;
						writer.WriteCompressedUInt32(num5);
						if (num5 == 0U)
						{
							writer.WriteCompressedUInt32((uint)value);
						}
						else
						{
							writer.WriteCompressedInt32(value);
						}
						if (num2 < 0)
						{
							writer.WriteCompressedUInt32((uint)sequencePoint.StartLine);
							writer.WriteCompressedUInt32((uint)sequencePoint.StartColumn);
						}
						else
						{
							writer.WriteCompressedInt32(sequencePoint.StartLine - num2);
							writer.WriteCompressedInt32(sequencePoint.StartColumn - num3);
						}
						num2 = sequencePoint.StartLine;
						num3 = sequencePoint.StartColumn;
					}
				}
				i++;
				num4 += (uint)instruction.GetSize();
			}
			byte[] data = outStream.ToArray();
			RawMethodDebugInformationRow value2 = new RawMethodDebugInformationRow((pdbDocument == null) ? 0U : this.AddPdbDocument(pdbDocument), this.debugMetadata.blobHeap.Add(data));
			this.debugMetadata.tablesHeap.MethodDebugInformationTable[rid] = value2;
			this.debugMetadata.methodDebugInformationInfosUsed = true;
			this.Free(ref dataWriterContext);
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x001A4928 File Offset: 0x001A4928
		private uint VerifyGetRid(PdbDocument doc)
		{
			uint result;
			if (!this.debugMetadata.pdbDocumentInfos.TryGetRid(doc, out result))
			{
				this.Error("PDB document has been removed", new object[0]);
				return 0U;
			}
			return result;
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x001A4968 File Offset: 0x001A4968
		private static void GetSingleDocument(CilBody body, out PdbDocument singleDoc, out PdbDocument firstDoc, out bool hasNoSeqPoints)
		{
			IList<Instruction> instructions = body.Instructions;
			int num = 0;
			singleDoc = null;
			firstDoc = null;
			for (int i = 0; i < instructions.Count; i++)
			{
				SequencePoint sequencePoint = instructions[i].SequencePoint;
				if (sequencePoint != null)
				{
					PdbDocument document = sequencePoint.Document;
					if (document != null)
					{
						if (firstDoc == null)
						{
							firstDoc = document;
						}
						if (singleDoc != document)
						{
							singleDoc = document;
							num++;
							if (num > 1)
							{
								break;
							}
						}
					}
				}
			}
			hasNoSeqPoints = (num == 0);
			if (num != 1)
			{
				singleDoc = null;
			}
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x001A49F4 File Offset: 0x001A49F4
		protected void AddCustomDebugInformationList(Table table, uint rid, IHasCustomDebugInformation hcdi)
		{
			if (this.debugMetadata == null)
			{
				return;
			}
			if (hcdi.CustomDebugInfos.Count == 0)
			{
				return;
			}
			SerializerMethodContext serializerMethodContext = this.AllocSerializerMethodContext();
			serializerMethodContext.SetBody(null);
			this.AddCustomDebugInformationCore(serializerMethodContext, table, rid, hcdi.CustomDebugInfos);
			this.Free(ref serializerMethodContext);
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x001A4A48 File Offset: 0x001A4A48
		private void AddCustomDebugInformationList(Table table, uint rid, IList<PdbCustomDebugInfo> cdis)
		{
			if (this.debugMetadata == null)
			{
				return;
			}
			if (cdis.Count == 0)
			{
				return;
			}
			SerializerMethodContext serializerMethodContext = this.AllocSerializerMethodContext();
			serializerMethodContext.SetBody(null);
			this.AddCustomDebugInformationCore(serializerMethodContext, table, rid, cdis);
			this.Free(ref serializerMethodContext);
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x001A4A94 File Offset: 0x001A4A94
		private void AddCustomDebugInformationCore(SerializerMethodContext serializerMethodContext, Table table, uint rid, IList<PdbCustomDebugInfo> cdis)
		{
			MDToken token = new MDToken(table, rid);
			uint encodedToken;
			if (!CodedToken.HasCustomDebugInformation.Encode(token, out encodedToken))
			{
				this.Error("Couldn't encode HasCustomDebugInformation token {0:X8}", new object[]
				{
					token.Raw
				});
				return;
			}
			for (int i = 0; i < cdis.Count; i++)
			{
				PdbCustomDebugInfo pdbCustomDebugInfo = cdis[i];
				if (pdbCustomDebugInfo == null)
				{
					this.Error("Custom debug info is null", new object[0]);
				}
				else
				{
					this.AddCustomDebugInformation(serializerMethodContext, token.Raw, encodedToken, pdbCustomDebugInfo);
				}
			}
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x001A4B2C File Offset: 0x001A4B2C
		private void AddCustomDebugInformation(SerializerMethodContext serializerMethodContext, uint token, uint encodedToken, PdbCustomDebugInfo cdi)
		{
			PdbCustomDebugInfoKind kind = cdi.Kind;
			switch (kind)
			{
			case PdbCustomDebugInfoKind.Unknown:
			case PdbCustomDebugInfoKind.TupleElementNames_PortablePdb:
			case PdbCustomDebugInfoKind.DefaultNamespace:
			case PdbCustomDebugInfoKind.DynamicLocalVariables:
			case PdbCustomDebugInfoKind.EmbeddedSource:
			case PdbCustomDebugInfoKind.SourceLink:
				goto IL_88;
			case PdbCustomDebugInfoKind.SourceServer:
				break;
			case PdbCustomDebugInfoKind.AsyncMethod:
				this.AddCustomDebugInformationCore(serializerMethodContext, encodedToken, cdi, CustomDebugInfoGuids.AsyncMethodSteppingInformationBlob);
				this.AddStateMachineMethod(cdi, token, ((PdbAsyncMethodCustomDebugInfo)cdi).KickoffMethod);
				return;
			case PdbCustomDebugInfoKind.IteratorMethod:
				this.AddStateMachineMethod(cdi, token, ((PdbIteratorMethodCustomDebugInfo)cdi).KickoffMethod);
				return;
			default:
				switch (kind)
				{
				case PdbCustomDebugInfoKind.UsingGroups:
				case PdbCustomDebugInfoKind.ForwardMethodInfo:
				case PdbCustomDebugInfoKind.ForwardModuleInfo:
				case PdbCustomDebugInfoKind.StateMachineTypeName:
				case PdbCustomDebugInfoKind.DynamicLocals:
				case PdbCustomDebugInfoKind.TupleElementNames:
					break;
				case PdbCustomDebugInfoKind.StateMachineHoistedLocalScopes:
				case PdbCustomDebugInfoKind.EditAndContinueLocalSlotMap:
				case PdbCustomDebugInfoKind.EditAndContinueLambdaMap:
					goto IL_88;
				default:
					this.Error("Unknown custom debug info {0}", new object[]
					{
						cdi.Kind.ToString()
					});
					return;
				}
				break;
			}
			this.Error("Unsupported custom debug info {0}", new object[]
			{
				cdi.Kind
			});
			return;
			IL_88:
			this.AddCustomDebugInformationCore(serializerMethodContext, encodedToken, cdi, cdi.Guid);
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x001A4C3C File Offset: 0x001A4C3C
		private void AddStateMachineMethod(PdbCustomDebugInfo cdi, uint moveNextMethodToken, MethodDef kickoffMethod)
		{
			if (kickoffMethod == null)
			{
				this.Error("KickoffMethod is null", new object[0]);
				return;
			}
			RawStateMachineMethodRow row = new RawStateMachineMethodRow(new MDToken(moveNextMethodToken).Rid, this.GetRid(kickoffMethod));
			this.debugMetadata.stateMachineMethodInfos.Add(cdi, row);
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x001A4C94 File Offset: 0x001A4C94
		private void AddCustomDebugInformationCore(SerializerMethodContext serializerMethodContext, uint encodedToken, PdbCustomDebugInfo cdi, Guid cdiGuid)
		{
			DataWriterContext context = this.AllocBinaryWriterContext();
			byte[] data = PortablePdbCustomDebugInfoWriter.Write(this, serializerMethodContext, this, cdi, context);
			this.Free(ref context);
			RawCustomDebugInformationRow row = new RawCustomDebugInformationRow(encodedToken, this.debugMetadata.guidHeap.Add(new Guid?(cdiGuid)), this.debugMetadata.blobHeap.Add(data));
			this.debugMetadata.customDebugInfos.Add(cdi, row);
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x001A4D04 File Offset: 0x001A4D04
		private void InitializeMethodDebugInformation()
		{
			if (this.debugMetadata == null)
			{
				return;
			}
			int numberOfMethods = this.NumberOfMethods;
			for (int i = 0; i < numberOfMethods; i++)
			{
				this.debugMetadata.tablesHeap.MethodDebugInformationTable.Create(default(RawMethodDebugInformationRow));
			}
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x001A4D58 File Offset: 0x001A4D58
		private void AddPdbDocuments()
		{
			if (this.debugMetadata == null)
			{
				return;
			}
			foreach (PdbDocument doc in this.module.PdbState.Documents)
			{
				this.AddPdbDocument(doc);
			}
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x001A4DC8 File Offset: 0x001A4DC8
		private uint AddPdbDocument(PdbDocument doc)
		{
			if (doc == null)
			{
				this.Error("PdbDocument is null", new object[0]);
				return 0U;
			}
			uint num;
			if (this.debugMetadata.pdbDocumentInfos.TryGetRid(doc, out num))
			{
				return num;
			}
			RawDocumentRow row = new RawDocumentRow(this.GetDocumentNameBlobOffset(doc.Url), this.debugMetadata.guidHeap.Add(new Guid?(doc.CheckSumAlgorithmId)), this.debugMetadata.blobHeap.Add(doc.CheckSum), this.debugMetadata.guidHeap.Add(new Guid?(doc.Language)));
			num = this.debugMetadata.tablesHeap.DocumentTable.Add(row);
			this.debugMetadata.pdbDocumentInfos.Add(doc, num);
			this.AddCustomDebugInformationList(Table.Document, num, doc.CustomDebugInfos);
			return num;
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x001A4EA4 File Offset: 0x001A4EA4
		private uint GetDocumentNameBlobOffset(string name)
		{
			if (name == null)
			{
				this.Error("Document name is null", new object[0]);
				name = string.Empty;
			}
			DataWriterContext dataWriterContext = this.AllocBinaryWriterContext();
			MemoryStream outStream = dataWriterContext.OutStream;
			DataWriter writer = dataWriterContext.Writer;
			outStream.SetLength(0L);
			outStream.Position = 0L;
			string[] array = name.Split(Metadata.directorySeparatorCharArray);
			if (array.Length == 1)
			{
				writer.WriteByte(0);
			}
			else
			{
				writer.WriteBytes(Metadata.directorySeparatorCharUtf8);
			}
			foreach (string s in array)
			{
				uint value = this.debugMetadata.blobHeap.Add(Encoding.UTF8.GetBytes(s));
				writer.WriteCompressedUInt32(value);
			}
			uint result = this.debugMetadata.blobHeap.Add(outStream.ToArray());
			this.Free(ref dataWriterContext);
			return result;
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x001A4F88 File Offset: 0x001A4F88
		private uint AddImportScope(PdbImportScope scope)
		{
			if (scope == null)
			{
				return 0U;
			}
			uint num;
			if (this.debugMetadata.importScopeInfos.TryGetRid(scope, out num))
			{
				if (num == 0U)
				{
					this.Error("PdbImportScope has an infinite Parent loop", new object[0]);
				}
				return num;
			}
			this.debugMetadata.importScopeInfos.Add(scope, 0U);
			DataWriterContext dataWriterContext = this.AllocBinaryWriterContext();
			MemoryStream outStream = dataWriterContext.OutStream;
			DataWriter writer = dataWriterContext.Writer;
			outStream.SetLength(0L);
			outStream.Position = 0L;
			ImportScopeBlobWriter.Write(this, this, writer, this.debugMetadata.blobHeap, scope.Imports);
			byte[] data = outStream.ToArray();
			this.Free(ref dataWriterContext);
			RawImportScopeRow row = new RawImportScopeRow(this.AddImportScope(scope.Parent), this.debugMetadata.blobHeap.Add(data));
			num = this.debugMetadata.tablesHeap.ImportScopeTable.Add(row);
			this.debugMetadata.importScopeInfos.SetRid(scope, num);
			this.AddCustomDebugInformationList(Table.ImportScope, num, scope.CustomDebugInfos);
			return num;
		}

		// Token: 0x06005608 RID: 22024 RVA: 0x001A5090 File Offset: 0x001A5090
		private void AddLocalVariable(PdbLocal local)
		{
			if (local == null)
			{
				this.Error("PDB local is null", new object[0]);
				return;
			}
			RawLocalVariableRow row = new RawLocalVariableRow((ushort)local.Attributes, (ushort)local.Index, this.debugMetadata.stringsHeap.Add(local.Name));
			uint rid = this.debugMetadata.tablesHeap.LocalVariableTable.Create(row);
			this.debugMetadata.localVariableInfos.Add(local, rid);
			this.AddCustomDebugInformationList(Table.LocalVariable, rid, local.CustomDebugInfos);
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x001A5124 File Offset: 0x001A5124
		private void AddLocalConstant(PdbConstant constant)
		{
			if (constant == null)
			{
				this.Error("PDB constant is null", new object[0]);
				return;
			}
			DataWriterContext dataWriterContext = this.AllocBinaryWriterContext();
			MemoryStream outStream = dataWriterContext.OutStream;
			DataWriter writer = dataWriterContext.Writer;
			outStream.SetLength(0L);
			outStream.Position = 0L;
			LocalConstantSigBlobWriter.Write(this, this, writer, constant.Type, constant.Value);
			byte[] data = outStream.ToArray();
			this.Free(ref dataWriterContext);
			RawLocalConstantRow row = new RawLocalConstantRow(this.debugMetadata.stringsHeap.Add(constant.Name), this.debugMetadata.blobHeap.Add(data));
			uint rid = this.debugMetadata.tablesHeap.LocalConstantTable.Create(row);
			this.debugMetadata.localConstantInfos.Add(constant, rid);
			this.AddCustomDebugInformationList(Table.LocalConstant, rid, constant.CustomDebugInfos);
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x001A5204 File Offset: 0x001A5204
		internal void WritePortablePdb(Stream output, uint entryPointToken, out long pdbIdOffset)
		{
			if (this.debugMetadata == null)
			{
				throw new InvalidOperationException();
			}
			PdbHeap pdbHeap = this.debugMetadata.PdbHeap;
			pdbHeap.EntryPoint = entryPointToken;
			ulong referencedTypeSystemTables;
			this.tablesHeap.GetSystemTableRows(out referencedTypeSystemTables, pdbHeap.TypeSystemTableRows);
			this.debugMetadata.tablesHeap.SetSystemTableRows(pdbHeap.TypeSystemTableRows);
			if (!this.debugMetadata.methodDebugInformationInfosUsed)
			{
				this.debugMetadata.tablesHeap.MethodDebugInformationTable.Reset();
			}
			pdbHeap.ReferencedTypeSystemTables = referencedTypeSystemTables;
			DataWriter writer = new DataWriter(output);
			this.debugMetadata.OnBeforeSetOffset();
			this.debugMetadata.SetOffset((FileOffset)0U, (RVA)0U);
			this.debugMetadata.GetFileLength();
			this.debugMetadata.VerifyWriteTo(writer);
			pdbIdOffset = (long)((ulong)pdbHeap.PdbIdOffset);
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x001A52D0 File Offset: 0x001A52D0
		uint ISignatureWriterHelper.ToEncodedToken(ITypeDefOrRef typeDefOrRef)
		{
			return this.AddTypeDefOrRef(typeDefOrRef);
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x001A52DC File Offset: 0x001A52DC
		void IWriterError.Error(string message)
		{
			this.Error(message, new object[0]);
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x001A52EC File Offset: 0x001A52EC
		bool IFullNameFactoryHelper.MustUseAssemblyName(IType type)
		{
			return FullNameFactory.MustUseAssemblyName(this.module, type, this.OptimizeCustomAttributeSerializedTypeNames);
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x001A5300 File Offset: 0x001A5300
		protected virtual void Initialize()
		{
		}

		// Token: 0x0600560F RID: 22031
		protected abstract TypeDef[] GetAllTypeDefs();

		// Token: 0x06005610 RID: 22032
		protected abstract void AllocateTypeDefRids();

		// Token: 0x06005611 RID: 22033
		protected abstract void AllocateMemberDefRids();

		// Token: 0x06005612 RID: 22034
		protected abstract uint AddTypeRef(TypeRef tr);

		// Token: 0x06005613 RID: 22035
		protected abstract uint AddTypeSpec(TypeSpec ts);

		// Token: 0x06005614 RID: 22036
		protected abstract uint AddMemberRef(MemberRef mr);

		// Token: 0x06005615 RID: 22037
		protected abstract uint AddStandAloneSig(StandAloneSig sas);

		// Token: 0x06005616 RID: 22038
		protected abstract uint AddMethodSpec(MethodSpec ms);

		// Token: 0x06005617 RID: 22039 RVA: 0x001A5304 File Offset: 0x001A5304
		protected virtual void BeforeSortingCustomAttributes()
		{
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x001A5308 File Offset: 0x001A5308
		protected virtual void EverythingInitialized()
		{
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x001A530C File Offset: 0x001A530C
		bool IReuseChunk.CanReuse(RVA origRva, uint origSize)
		{
			if (this.length == 0U)
			{
				throw new InvalidOperationException();
			}
			return this.length <= origSize;
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x001A532C File Offset: 0x001A532C
		internal void OnBeforeSetOffset()
		{
			this.stringsHeap.AddOptimizedStringsAndSetReadOnly();
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x001A533C File Offset: 0x001A533C
		public void SetOffset(FileOffset offset, RVA rva)
		{
			bool flag = this.offset == (FileOffset)0U;
			this.offset = offset;
			this.rva = rva;
			if (flag)
			{
				this.blobHeap.SetReadOnly();
				this.guidHeap.SetReadOnly();
				this.tablesHeap.SetReadOnly();
				this.pdbHeap.SetReadOnly();
				this.tablesHeap.BigStrings = this.stringsHeap.IsBig;
				this.tablesHeap.BigBlob = this.blobHeap.IsBig;
				this.tablesHeap.BigGuid = this.guidHeap.IsBig;
				this.metadataHeader.Heaps = this.GetHeaps();
			}
			this.metadataHeader.SetOffset(offset, rva);
			uint fileLength = this.metadataHeader.GetFileLength();
			offset += fileLength;
			rva += fileLength;
			foreach (IHeap heap in this.metadataHeader.Heaps)
			{
				offset = offset.AlignUp(4U);
				rva = rva.AlignUp(4U);
				heap.SetOffset(offset, rva);
				fileLength = heap.GetFileLength();
				offset += fileLength;
				rva += fileLength;
			}
			if (!flag && this.length != rva - this.rva)
			{
				throw new InvalidOperationException();
			}
			this.length = rva - this.rva;
			if (!this.isStandaloneDebugMetadata && flag)
			{
				this.UpdateMethodAndFieldRvas();
			}
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x001A54BC File Offset: 0x001A54BC
		internal void UpdateMethodAndFieldRvas()
		{
			this.UpdateMethodRvas();
			this.UpdateFieldRvas();
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x001A54CC File Offset: 0x001A54CC
		private IList<IHeap> GetHeaps()
		{
			List<IHeap> list = new List<IHeap>();
			if (this.isStandaloneDebugMetadata)
			{
				list.Add(this.pdbHeap);
				list.Add(this.tablesHeap);
				if (!this.stringsHeap.IsEmpty)
				{
					list.Add(this.stringsHeap);
				}
				if (!this.usHeap.IsEmpty)
				{
					list.Add(this.usHeap);
				}
				if (!this.guidHeap.IsEmpty)
				{
					list.Add(this.guidHeap);
				}
				if (!this.blobHeap.IsEmpty)
				{
					list.Add(this.blobHeap);
				}
			}
			else
			{
				list.Add(this.tablesHeap);
				if (!this.stringsHeap.IsEmpty || this.AlwaysCreateStringsHeap)
				{
					list.Add(this.stringsHeap);
				}
				if (!this.usHeap.IsEmpty || this.AlwaysCreateUSHeap)
				{
					list.Add(this.usHeap);
				}
				if (!this.guidHeap.IsEmpty || this.AlwaysCreateGuidHeap)
				{
					list.Add(this.guidHeap);
				}
				if (!this.blobHeap.IsEmpty || this.AlwaysCreateBlobHeap)
				{
					list.Add(this.blobHeap);
				}
				list.AddRange(this.options.CustomHeaps);
				this.options.RaiseMetadataHeapsAdded(new MetadataHeapsAddedEventArgs(this, list));
			}
			return list;
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x001A5648 File Offset: 0x001A5648
		public uint GetFileLength()
		{
			return this.length;
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x001A5650 File Offset: 0x001A5650
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x001A5658 File Offset: 0x001A5658
		public void WriteTo(DataWriter writer)
		{
			RVA rva = this.rva;
			this.metadataHeader.VerifyWriteTo(writer);
			rva += this.metadataHeader.GetFileLength();
			foreach (IHeap heap in this.metadataHeader.Heaps)
			{
				writer.WriteZeroes((int)(rva.AlignUp(4U) - rva));
				rva = rva.AlignUp(4U);
				heap.VerifyWriteTo(writer);
				rva += heap.GetFileLength();
			}
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x001A56F4 File Offset: 0x001A56F4
		protected static List<ParamDef> Sort(IEnumerable<ParamDef> pds)
		{
			List<ParamDef> list = new List<ParamDef>(pds);
			list.Sort(delegate(ParamDef a, ParamDef b)
			{
				if (a == null)
				{
					return -1;
				}
				if (b == null)
				{
					return 1;
				}
				return a.Sequence.CompareTo(b.Sequence);
			});
			return list;
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x001A5724 File Offset: 0x001A5724
		private DataWriterContext AllocBinaryWriterContext()
		{
			if (this.binaryWriterContexts.Count == 0)
			{
				return new DataWriterContext();
			}
			DataWriterContext result = this.binaryWriterContexts[this.binaryWriterContexts.Count - 1];
			this.binaryWriterContexts.RemoveAt(this.binaryWriterContexts.Count - 1);
			return result;
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x001A577C File Offset: 0x001A577C
		private void Free(ref DataWriterContext ctx)
		{
			this.binaryWriterContexts.Add(ctx);
			ctx = null;
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x001A5790 File Offset: 0x001A5790
		private SerializerMethodContext AllocSerializerMethodContext()
		{
			if (this.serializerMethodContexts.Count == 0)
			{
				return new SerializerMethodContext(this);
			}
			SerializerMethodContext result = this.serializerMethodContexts[this.serializerMethodContexts.Count - 1];
			this.serializerMethodContexts.RemoveAt(this.serializerMethodContexts.Count - 1);
			return result;
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x001A57E8 File Offset: 0x001A57E8
		private void Free(ref SerializerMethodContext ctx)
		{
			this.serializerMethodContexts.Add(ctx);
			ctx = null;
		}

		// Token: 0x040028F4 RID: 10484
		private uint length;

		// Token: 0x040028F5 RID: 10485
		private FileOffset offset;

		// Token: 0x040028F6 RID: 10486
		private RVA rva;

		// Token: 0x040028F7 RID: 10487
		private readonly MetadataOptions options;

		// Token: 0x040028F8 RID: 10488
		private ILogger logger;

		// Token: 0x040028F9 RID: 10489
		private readonly NormalMetadata debugMetadata;

		// Token: 0x040028FA RID: 10490
		private readonly bool isStandaloneDebugMetadata;

		// Token: 0x040028FB RID: 10491
		internal readonly ModuleDef module;

		// Token: 0x040028FC RID: 10492
		internal readonly UniqueChunkList<ByteArrayChunk> constants;

		// Token: 0x040028FD RID: 10493
		internal readonly MethodBodyChunks methodBodies;

		// Token: 0x040028FE RID: 10494
		internal readonly NetResources netResources;

		// Token: 0x040028FF RID: 10495
		internal readonly MetadataHeader metadataHeader;

		// Token: 0x04002900 RID: 10496
		internal readonly PdbHeap pdbHeap;

		// Token: 0x04002901 RID: 10497
		internal readonly TablesHeap tablesHeap;

		// Token: 0x04002902 RID: 10498
		internal readonly StringsHeap stringsHeap;

		// Token: 0x04002903 RID: 10499
		internal readonly USHeap usHeap;

		// Token: 0x04002904 RID: 10500
		internal readonly GuidHeap guidHeap;

		// Token: 0x04002905 RID: 10501
		internal readonly BlobHeap blobHeap;

		// Token: 0x04002906 RID: 10502
		internal TypeDef[] allTypeDefs;

		// Token: 0x04002907 RID: 10503
		internal readonly Metadata.Rows<ModuleDef> moduleDefInfos = new Metadata.Rows<ModuleDef>();

		// Token: 0x04002908 RID: 10504
		internal readonly Metadata.SortedRows<InterfaceImpl, RawInterfaceImplRow> interfaceImplInfos = new Metadata.SortedRows<InterfaceImpl, RawInterfaceImplRow>();

		// Token: 0x04002909 RID: 10505
		internal readonly Metadata.SortedRows<IHasConstant, RawConstantRow> hasConstantInfos = new Metadata.SortedRows<IHasConstant, RawConstantRow>();

		// Token: 0x0400290A RID: 10506
		internal readonly Metadata.SortedRows<CustomAttribute, RawCustomAttributeRow> customAttributeInfos = new Metadata.SortedRows<CustomAttribute, RawCustomAttributeRow>();

		// Token: 0x0400290B RID: 10507
		internal readonly Metadata.SortedRows<IHasFieldMarshal, RawFieldMarshalRow> fieldMarshalInfos = new Metadata.SortedRows<IHasFieldMarshal, RawFieldMarshalRow>();

		// Token: 0x0400290C RID: 10508
		internal readonly Metadata.SortedRows<DeclSecurity, RawDeclSecurityRow> declSecurityInfos = new Metadata.SortedRows<DeclSecurity, RawDeclSecurityRow>();

		// Token: 0x0400290D RID: 10509
		internal readonly Metadata.SortedRows<TypeDef, RawClassLayoutRow> classLayoutInfos = new Metadata.SortedRows<TypeDef, RawClassLayoutRow>();

		// Token: 0x0400290E RID: 10510
		internal readonly Metadata.SortedRows<FieldDef, RawFieldLayoutRow> fieldLayoutInfos = new Metadata.SortedRows<FieldDef, RawFieldLayoutRow>();

		// Token: 0x0400290F RID: 10511
		internal readonly Metadata.Rows<TypeDef> eventMapInfos = new Metadata.Rows<TypeDef>();

		// Token: 0x04002910 RID: 10512
		internal readonly Metadata.Rows<TypeDef> propertyMapInfos = new Metadata.Rows<TypeDef>();

		// Token: 0x04002911 RID: 10513
		internal readonly Metadata.SortedRows<MethodDef, RawMethodSemanticsRow> methodSemanticsInfos = new Metadata.SortedRows<MethodDef, RawMethodSemanticsRow>();

		// Token: 0x04002912 RID: 10514
		internal readonly Metadata.SortedRows<MethodDef, RawMethodImplRow> methodImplInfos = new Metadata.SortedRows<MethodDef, RawMethodImplRow>();

		// Token: 0x04002913 RID: 10515
		internal readonly Metadata.Rows<ModuleRef> moduleRefInfos = new Metadata.Rows<ModuleRef>();

		// Token: 0x04002914 RID: 10516
		internal readonly Metadata.SortedRows<IMemberForwarded, RawImplMapRow> implMapInfos = new Metadata.SortedRows<IMemberForwarded, RawImplMapRow>();

		// Token: 0x04002915 RID: 10517
		internal readonly Metadata.SortedRows<FieldDef, RawFieldRVARow> fieldRVAInfos = new Metadata.SortedRows<FieldDef, RawFieldRVARow>();

		// Token: 0x04002916 RID: 10518
		internal readonly Metadata.Rows<AssemblyDef> assemblyInfos = new Metadata.Rows<AssemblyDef>();

		// Token: 0x04002917 RID: 10519
		internal readonly Metadata.Rows<AssemblyRef> assemblyRefInfos = new Metadata.Rows<AssemblyRef>();

		// Token: 0x04002918 RID: 10520
		internal readonly Metadata.Rows<FileDef> fileDefInfos = new Metadata.Rows<FileDef>();

		// Token: 0x04002919 RID: 10521
		internal readonly Metadata.Rows<ExportedType> exportedTypeInfos = new Metadata.Rows<ExportedType>();

		// Token: 0x0400291A RID: 10522
		internal readonly Metadata.Rows<Resource> manifestResourceInfos = new Metadata.Rows<Resource>();

		// Token: 0x0400291B RID: 10523
		internal readonly Metadata.SortedRows<TypeDef, RawNestedClassRow> nestedClassInfos = new Metadata.SortedRows<TypeDef, RawNestedClassRow>();

		// Token: 0x0400291C RID: 10524
		internal readonly Metadata.SortedRows<GenericParam, RawGenericParamRow> genericParamInfos = new Metadata.SortedRows<GenericParam, RawGenericParamRow>();

		// Token: 0x0400291D RID: 10525
		internal readonly Metadata.SortedRows<GenericParamConstraint, RawGenericParamConstraintRow> genericParamConstraintInfos = new Metadata.SortedRows<GenericParamConstraint, RawGenericParamConstraintRow>();

		// Token: 0x0400291E RID: 10526
		internal readonly Dictionary<MethodDef, MethodBody> methodToBody = new Dictionary<MethodDef, MethodBody>();

		// Token: 0x0400291F RID: 10527
		internal readonly Dictionary<MethodDef, NativeMethodBody> methodToNativeBody = new Dictionary<MethodDef, NativeMethodBody>();

		// Token: 0x04002920 RID: 10528
		internal readonly Dictionary<EmbeddedResource, DataReaderChunk> embeddedResourceToByteArray = new Dictionary<EmbeddedResource, DataReaderChunk>();

		// Token: 0x04002921 RID: 10529
		private readonly Dictionary<FieldDef, ByteArrayChunk> fieldToInitialValue = new Dictionary<FieldDef, ByteArrayChunk>();

		// Token: 0x04002922 RID: 10530
		private readonly Metadata.Rows<PdbDocument> pdbDocumentInfos = new Metadata.Rows<PdbDocument>();

		// Token: 0x04002923 RID: 10531
		private bool methodDebugInformationInfosUsed;

		// Token: 0x04002924 RID: 10532
		private readonly Metadata.SortedRows<PdbScope, RawLocalScopeRow> localScopeInfos = new Metadata.SortedRows<PdbScope, RawLocalScopeRow>();

		// Token: 0x04002925 RID: 10533
		private readonly Metadata.Rows<PdbLocal> localVariableInfos = new Metadata.Rows<PdbLocal>();

		// Token: 0x04002926 RID: 10534
		private readonly Metadata.Rows<PdbConstant> localConstantInfos = new Metadata.Rows<PdbConstant>();

		// Token: 0x04002927 RID: 10535
		private readonly Metadata.Rows<PdbImportScope> importScopeInfos = new Metadata.Rows<PdbImportScope>();

		// Token: 0x04002928 RID: 10536
		private readonly Metadata.SortedRows<PdbCustomDebugInfo, RawStateMachineMethodRow> stateMachineMethodInfos = new Metadata.SortedRows<PdbCustomDebugInfo, RawStateMachineMethodRow>();

		// Token: 0x04002929 RID: 10537
		private readonly Metadata.SortedRows<PdbCustomDebugInfo, RawCustomDebugInformationRow> customDebugInfos = new Metadata.SortedRows<PdbCustomDebugInfo, RawCustomDebugInformationRow>();

		// Token: 0x0400292A RID: 10538
		private readonly List<DataWriterContext> binaryWriterContexts = new List<DataWriterContext>();

		// Token: 0x0400292B RID: 10539
		private readonly List<SerializerMethodContext> serializerMethodContexts = new List<SerializerMethodContext>();

		// Token: 0x0400292C RID: 10540
		private readonly List<MethodDef> exportedMethods = new List<MethodDef>();

		// Token: 0x04002931 RID: 10545
		private static readonly double[] eventToProgress = new double[]
		{
			0.0,
			0.00134240009466231,
			0.00257484711254305,
			0.0762721800615359,
			0.196633787905108,
			0.207788892253819,
			0.270543867900699,
			0.451478814851716,
			0.451478949929206,
			0.454664752528583,
			0.454664887606073,
			0.992591810143725,
			0.999984331011171,
			1.0,
			1.0
		};

		// Token: 0x04002932 RID: 10546
		private static readonly byte[] constantClassByteArray = new byte[4];

		// Token: 0x04002933 RID: 10547
		private static readonly byte[] constantDefaultByteArray = new byte[8];

		// Token: 0x04002934 RID: 10548
		private static readonly byte[] directorySeparatorCharUtf8 = Encoding.UTF8.GetBytes(Path.DirectorySeparatorChar.ToString());

		// Token: 0x04002935 RID: 10549
		private static readonly char[] directorySeparatorCharArray = new char[]
		{
			Path.DirectorySeparatorChar
		};

		// Token: 0x04002936 RID: 10550
		private const uint HEAP_ALIGNMENT = 4U;

		// Token: 0x0200101B RID: 4123
		internal sealed class SortedRows<T, TRow> where T : class where TRow : struct
		{
			// Token: 0x06008F45 RID: 36677 RVA: 0x002ABAB8 File Offset: 0x002ABAB8
			public void Add(T data, TRow row)
			{
				if (this.isSorted)
				{
					throw new ModuleWriterException(string.Format("Adding a row after it's been sorted. Table: {0}", row.GetType()));
				}
				this.infos.Add(new Metadata.SortedRows<T, TRow>.Info(data, ref row));
				this.toRid[data] = (uint)(this.toRid.Count + 1);
			}

			// Token: 0x06008F46 RID: 36678 RVA: 0x002ABB20 File Offset: 0x002ABB20
			public void Sort(Comparison<Metadata.SortedRows<T, TRow>.Info> comparison)
			{
				this.infos.Sort(this.CreateComparison(comparison));
				this.toRid.Clear();
				for (int i = 0; i < this.infos.Count; i++)
				{
					this.toRid[this.infos[i].data] = (uint)(i + 1);
				}
				this.isSorted = true;
			}

			// Token: 0x06008F47 RID: 36679 RVA: 0x002ABB90 File Offset: 0x002ABB90
			private Comparison<Metadata.SortedRows<T, TRow>.Info> CreateComparison(Comparison<Metadata.SortedRows<T, TRow>.Info> comparison)
			{
				return delegate(Metadata.SortedRows<T, TRow>.Info a, Metadata.SortedRows<T, TRow>.Info b)
				{
					int num = comparison(a, b);
					if (num != 0)
					{
						return num;
					}
					return this.toRid[a.data].CompareTo(this.toRid[b.data]);
				};
			}

			// Token: 0x06008F48 RID: 36680 RVA: 0x002ABBB0 File Offset: 0x002ABBB0
			public uint Rid(T data)
			{
				return this.toRid[data];
			}

			// Token: 0x06008F49 RID: 36681 RVA: 0x002ABBC0 File Offset: 0x002ABBC0
			public bool TryGetRid(T data, out uint rid)
			{
				if (data == null)
				{
					rid = 0U;
					return false;
				}
				return this.toRid.TryGetValue(data, out rid);
			}

			// Token: 0x0400449C RID: 17564
			public List<Metadata.SortedRows<T, TRow>.Info> infos = new List<Metadata.SortedRows<T, TRow>.Info>();

			// Token: 0x0400449D RID: 17565
			private Dictionary<T, uint> toRid = new Dictionary<T, uint>();

			// Token: 0x0400449E RID: 17566
			private bool isSorted;

			// Token: 0x0200120E RID: 4622
			public struct Info
			{
				// Token: 0x06009698 RID: 38552 RVA: 0x002CC6D4 File Offset: 0x002CC6D4
				public Info(T data, ref TRow row)
				{
					this.data = data;
					this.row = row;
				}

				// Token: 0x04004F13 RID: 20243
				public readonly T data;

				// Token: 0x04004F14 RID: 20244
				public TRow row;
			}
		}

		// Token: 0x0200101C RID: 4124
		internal sealed class Rows<T> where T : class
		{
			// Token: 0x17001E03 RID: 7683
			// (get) Token: 0x06008F4B RID: 36683 RVA: 0x002ABC00 File Offset: 0x002ABC00
			public int Count
			{
				get
				{
					return this.dict.Count;
				}
			}

			// Token: 0x06008F4C RID: 36684 RVA: 0x002ABC10 File Offset: 0x002ABC10
			public bool TryGetRid(T value, out uint rid)
			{
				if (value == null)
				{
					rid = 0U;
					return false;
				}
				return this.dict.TryGetValue(value, out rid);
			}

			// Token: 0x06008F4D RID: 36685 RVA: 0x002ABC30 File Offset: 0x002ABC30
			public bool Exists(T value)
			{
				return this.dict.ContainsKey(value);
			}

			// Token: 0x06008F4E RID: 36686 RVA: 0x002ABC40 File Offset: 0x002ABC40
			public void Add(T value, uint rid)
			{
				this.dict.Add(value, rid);
			}

			// Token: 0x06008F4F RID: 36687 RVA: 0x002ABC50 File Offset: 0x002ABC50
			public uint Rid(T value)
			{
				return this.dict[value];
			}

			// Token: 0x06008F50 RID: 36688 RVA: 0x002ABC60 File Offset: 0x002ABC60
			public void SetRid(T value, uint rid)
			{
				this.dict[value] = rid;
			}

			// Token: 0x0400449F RID: 17567
			private Dictionary<T, uint> dict = new Dictionary<T, uint>();
		}

		// Token: 0x0200101D RID: 4125
		private struct MethodScopeDebugInfo
		{
			// Token: 0x040044A0 RID: 17568
			public uint MethodRid;

			// Token: 0x040044A1 RID: 17569
			public PdbScope Scope;

			// Token: 0x040044A2 RID: 17570
			public uint ScopeStart;

			// Token: 0x040044A3 RID: 17571
			public uint ScopeLength;
		}
	}
}
