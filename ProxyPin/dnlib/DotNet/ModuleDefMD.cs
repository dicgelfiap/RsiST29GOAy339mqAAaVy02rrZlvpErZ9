using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.DotNet.Writer;
using dnlib.IO;
using dnlib.PE;
using dnlib.Utils;
using dnlib.W32Resources;

namespace dnlib.DotNet
{
	// Token: 0x0200081F RID: 2079
	[ComVisible(true)]
	public sealed class ModuleDefMD : ModuleDefMD2, IInstructionOperandResolver, ITokenResolver, IStringResolver
	{
		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06004C85 RID: 19589 RVA: 0x00180294 File Offset: 0x00180294
		// (set) Token: 0x06004C86 RID: 19590 RVA: 0x0018029C File Offset: 0x0018029C
		public IMethodDecrypter MethodDecrypter
		{
			get
			{
				return this.methodDecrypter;
			}
			set
			{
				this.methodDecrypter = value;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x001802A8 File Offset: 0x001802A8
		// (set) Token: 0x06004C88 RID: 19592 RVA: 0x001802B0 File Offset: 0x001802B0
		public IStringDecrypter StringDecrypter
		{
			get
			{
				return this.stringDecrypter;
			}
			set
			{
				this.stringDecrypter = value;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x001802BC File Offset: 0x001802BC
		public dnlib.DotNet.MD.Metadata Metadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06004C8A RID: 19594 RVA: 0x001802C4 File Offset: 0x001802C4
		public TablesStream TablesStream
		{
			get
			{
				return this.metadata.TablesStream;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06004C8B RID: 19595 RVA: 0x001802D4 File Offset: 0x001802D4
		public StringsStream StringsStream
		{
			get
			{
				return this.metadata.StringsStream;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06004C8C RID: 19596 RVA: 0x001802E4 File Offset: 0x001802E4
		public BlobStream BlobStream
		{
			get
			{
				return this.metadata.BlobStream;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06004C8D RID: 19597 RVA: 0x001802F4 File Offset: 0x001802F4
		public GuidStream GuidStream
		{
			get
			{
				return this.metadata.GuidStream;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06004C8E RID: 19598 RVA: 0x00180304 File Offset: 0x00180304
		public USStream USStream
		{
			get
			{
				return this.metadata.USStream;
			}
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x00180314 File Offset: 0x00180314
		protected override void InitializeTypes()
		{
			RidList nonNestedClassRidList = this.Metadata.GetNonNestedClassRidList();
			LazyList<TypeDef, RidList> value = new LazyList<TypeDef, RidList>(nonNestedClassRidList.Count, this, nonNestedClassRidList, (RidList list2, int index) => this.ResolveTypeDef(list2[index]));
			Interlocked.CompareExchange<LazyList<TypeDef>>(ref this.types, value, null);
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x0018035C File Offset: 0x0018035C
		protected override void InitializeExportedTypes()
		{
			RidList exportedTypeRidList = this.Metadata.GetExportedTypeRidList();
			LazyList<ExportedType, RidList> value = new LazyList<ExportedType, RidList>(exportedTypeRidList.Count, exportedTypeRidList, (RidList list2, int i) => this.ResolveExportedType(list2[i]));
			Interlocked.CompareExchange<IList<ExportedType>>(ref this.exportedTypes, value, null);
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x001803A4 File Offset: 0x001803A4
		protected override void InitializeResources()
		{
			ResourceCollection value = new ResourceCollection((int)this.TablesStream.ManifestResourceTable.Rows, null, (object ctx, int i) => this.CreateResource((uint)(i + 1)));
			Interlocked.CompareExchange<ResourceCollection>(ref this.resources, value, null);
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x001803E8 File Offset: 0x001803E8
		protected override Win32Resources GetWin32Resources_NoLock()
		{
			return this.metadata.PEImage.Win32Resources;
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x001803FC File Offset: 0x001803FC
		protected override VTableFixups GetVTableFixups_NoLock()
		{
			ImageDataDirectory vtableFixups = this.metadata.ImageCor20Header.VTableFixups;
			if (vtableFixups.VirtualAddress == (RVA)0U || vtableFixups.Size == 0U)
			{
				return null;
			}
			return new VTableFixups(this);
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x0018043C File Offset: 0x0018043C
		public static ModuleDefMD Load(string fileName, ModuleContext context)
		{
			return ModuleDefMD.Load(fileName, new ModuleCreationOptions(context));
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x0018044C File Offset: 0x0018044C
		public static ModuleDefMD Load(string fileName, ModuleCreationOptions options = null)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(fileName, (options != null) ? options.Runtime : CLRRuntimeReaderKind.CLR), options);
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x0018046C File Offset: 0x0018046C
		public static ModuleDefMD Load(byte[] data, ModuleContext context)
		{
			return ModuleDefMD.Load(data, new ModuleCreationOptions(context));
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0018047C File Offset: 0x0018047C
		public static ModuleDefMD Load(byte[] data, ModuleCreationOptions options = null)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(data, (options != null) ? options.Runtime : CLRRuntimeReaderKind.CLR), options);
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x0018049C File Offset: 0x0018049C
		public static ModuleDefMD Load(Module mod)
		{
			return ModuleDefMD.Load(mod, null, ModuleDefMD.GetImageLayout(mod));
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x001804AC File Offset: 0x001804AC
		public static ModuleDefMD Load(Module mod, ModuleContext context)
		{
			return ModuleDefMD.Load(mod, new ModuleCreationOptions(context), ModuleDefMD.GetImageLayout(mod));
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x001804C0 File Offset: 0x001804C0
		public static ModuleDefMD Load(Module mod, ModuleCreationOptions options)
		{
			return ModuleDefMD.Load(mod, options, ModuleDefMD.GetImageLayout(mod));
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x001804D0 File Offset: 0x001804D0
		private static ImageLayout GetImageLayout(Module mod)
		{
			string fullyQualifiedName = mod.FullyQualifiedName;
			if (fullyQualifiedName.Length > 0 && fullyQualifiedName[0] == '<' && fullyQualifiedName[fullyQualifiedName.Length - 1] == '>')
			{
				return ImageLayout.File;
			}
			return ImageLayout.Memory;
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x0018051C File Offset: 0x0018051C
		public static ModuleDefMD Load(Module mod, ModuleContext context, ImageLayout imageLayout)
		{
			return ModuleDefMD.Load(mod, new ModuleCreationOptions(context), imageLayout);
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x0018052C File Offset: 0x0018052C
		private static IntPtr GetModuleHandle(Module mod)
		{
			return Marshal.GetHINSTANCE(mod);
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x00180534 File Offset: 0x00180534
		public static ModuleDefMD Load(Module mod, ModuleCreationOptions options, ImageLayout imageLayout)
		{
			IntPtr moduleHandle = ModuleDefMD.GetModuleHandle(mod);
			if (moduleHandle != IntPtr.Zero && moduleHandle != new IntPtr(-1))
			{
				return ModuleDefMD.Load(moduleHandle, options, imageLayout);
			}
			string fullyQualifiedName = mod.FullyQualifiedName;
			if (string.IsNullOrEmpty(fullyQualifiedName) || fullyQualifiedName[0] == '<')
			{
				throw new InvalidOperationException(string.Format("Module {0} has no HINSTANCE", mod));
			}
			return ModuleDefMD.Load(fullyQualifiedName, options);
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x001805B0 File Offset: 0x001805B0
		public static ModuleDefMD Load(IntPtr addr)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(addr, CLRRuntimeReaderKind.CLR), null);
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x001805C0 File Offset: 0x001805C0
		public static ModuleDefMD Load(IntPtr addr, ModuleContext context)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(addr, CLRRuntimeReaderKind.CLR), new ModuleCreationOptions(context));
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x001805D4 File Offset: 0x001805D4
		public static ModuleDefMD Load(IntPtr addr, ModuleCreationOptions options)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(addr, (options != null) ? options.Runtime : CLRRuntimeReaderKind.CLR), options);
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x001805F4 File Offset: 0x001805F4
		public static ModuleDefMD Load(IPEImage peImage)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(peImage, CLRRuntimeReaderKind.CLR), null);
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x00180604 File Offset: 0x00180604
		public static ModuleDefMD Load(IPEImage peImage, ModuleContext context)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(peImage, CLRRuntimeReaderKind.CLR), new ModuleCreationOptions(context));
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x00180618 File Offset: 0x00180618
		public static ModuleDefMD Load(IPEImage peImage, ModuleCreationOptions options)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(peImage, (options != null) ? options.Runtime : CLRRuntimeReaderKind.CLR), options);
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x00180638 File Offset: 0x00180638
		public static ModuleDefMD Load(IntPtr addr, ModuleContext context, ImageLayout imageLayout)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(addr, imageLayout, CLRRuntimeReaderKind.CLR), new ModuleCreationOptions(context));
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x00180650 File Offset: 0x00180650
		public static ModuleDefMD Load(IntPtr addr, ModuleCreationOptions options, ImageLayout imageLayout)
		{
			return ModuleDefMD.Load(MetadataFactory.Load(addr, imageLayout, (options != null) ? options.Runtime : CLRRuntimeReaderKind.CLR), options);
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x00180674 File Offset: 0x00180674
		public static ModuleDefMD Load(Stream stream)
		{
			return ModuleDefMD.Load(stream, null);
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x00180680 File Offset: 0x00180680
		public static ModuleDefMD Load(Stream stream, ModuleContext context)
		{
			return ModuleDefMD.Load(stream, new ModuleCreationOptions(context));
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x00180690 File Offset: 0x00180690
		public static ModuleDefMD Load(Stream stream, ModuleCreationOptions options)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (stream.Length > 2147483647L)
			{
				throw new ArgumentException("Stream is too big");
			}
			byte[] array = new byte[(int)stream.Length];
			stream.Position = 0L;
			if (stream.Read(array, 0, array.Length) != array.Length)
			{
				throw new IOException("Could not read all bytes from the stream");
			}
			return ModuleDefMD.Load(array, options);
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x00180708 File Offset: 0x00180708
		internal static ModuleDefMD Load(MetadataBase metadata, ModuleCreationOptions options)
		{
			return new ModuleDefMD(metadata, options);
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x00180714 File Offset: 0x00180714
		private ModuleDefMD(MetadataBase metadata, ModuleCreationOptions options) : base(null, 1U)
		{
			if (options == null)
			{
				options = ModuleCreationOptions.Default;
			}
			this.metadata = metadata;
			this.context = options.Context;
			this.Initialize();
			base.InitializeFromRawRow();
			this.location = (metadata.PEImage.Filename ?? string.Empty);
			base.Kind = this.GetKind();
			base.Characteristics = this.Metadata.PEImage.ImageNTHeaders.FileHeader.Characteristics;
			base.DllCharacteristics = this.Metadata.PEImage.ImageNTHeaders.OptionalHeader.DllCharacteristics;
			base.RuntimeVersion = this.Metadata.VersionString;
			base.Machine = this.Metadata.PEImage.ImageNTHeaders.FileHeader.Machine;
			base.Cor20HeaderFlags = this.Metadata.ImageCor20Header.Flags;
			base.Cor20HeaderRuntimeVersion = new uint?((uint)((int)this.Metadata.ImageCor20Header.MajorRuntimeVersion << 16 | (int)this.Metadata.ImageCor20Header.MinorRuntimeVersion));
			base.TablesHeaderVersion = new ushort?(this.Metadata.TablesStream.Version);
			AssemblyRef corLibAssemblyRef;
			if ((corLibAssemblyRef = options.CorLibAssemblyRef) == null)
			{
				corLibAssemblyRef = (this.FindCorLibAssemblyRef() ?? this.CreateDefaultCorLibAssemblyRef());
			}
			this.corLibTypes = new CorLibTypes(this, corLibAssemblyRef);
			this.InitializePdb(options);
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x00180888 File Offset: 0x00180888
		private void InitializePdb(ModuleCreationOptions options)
		{
			if (options == null)
			{
				return;
			}
			this.LoadPdb(this.CreateSymbolReader(options));
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x001808A0 File Offset: 0x001808A0
		private SymbolReader CreateSymbolReader(ModuleCreationOptions options)
		{
			if (options.PdbFileOrData != null)
			{
				string text = options.PdbFileOrData as string;
				if (!string.IsNullOrEmpty(text))
				{
					SymbolReader symbolReader = SymbolReaderFactory.Create(options.PdbOptions, this.metadata, text);
					if (symbolReader != null)
					{
						return symbolReader;
					}
				}
				byte[] array = options.PdbFileOrData as byte[];
				if (array != null)
				{
					return SymbolReaderFactory.Create(options.PdbOptions, this.metadata, array);
				}
				DataReaderFactory dataReaderFactory = options.PdbFileOrData as DataReaderFactory;
				if (dataReaderFactory != null)
				{
					return SymbolReaderFactory.Create(options.PdbOptions, this.metadata, dataReaderFactory);
				}
			}
			if (options.TryToLoadPdbFromDisk)
			{
				return SymbolReaderFactory.CreateFromAssemblyFile(options.PdbOptions, this.metadata, this.location ?? string.Empty);
			}
			return null;
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x00180968 File Offset: 0x00180968
		public void LoadPdb(SymbolReader symbolReader)
		{
			if (symbolReader == null)
			{
				return;
			}
			if (this.pdbState != null)
			{
				throw new InvalidOperationException("PDB file has already been initialized");
			}
			if (Interlocked.CompareExchange<PdbState>(ref this.pdbState, new PdbState(symbolReader, this), null) != null)
			{
				throw new InvalidOperationException("PDB file has already been initialized");
			}
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x001809BC File Offset: 0x001809BC
		public void LoadPdb(string pdbFileName)
		{
			this.LoadPdb(PdbReaderOptions.None, pdbFileName);
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x001809C8 File Offset: 0x001809C8
		public void LoadPdb(PdbReaderOptions options, string pdbFileName)
		{
			this.LoadPdb(SymbolReaderFactory.Create(options, this.metadata, pdbFileName));
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x001809E0 File Offset: 0x001809E0
		public void LoadPdb(byte[] pdbData)
		{
			this.LoadPdb(PdbReaderOptions.None, pdbData);
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x001809EC File Offset: 0x001809EC
		public void LoadPdb(PdbReaderOptions options, byte[] pdbData)
		{
			this.LoadPdb(SymbolReaderFactory.Create(options, this.metadata, pdbData));
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x00180A04 File Offset: 0x00180A04
		public void LoadPdb(DataReaderFactory pdbStream)
		{
			this.LoadPdb(PdbReaderOptions.None, pdbStream);
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x00180A10 File Offset: 0x00180A10
		public void LoadPdb(PdbReaderOptions options, DataReaderFactory pdbStream)
		{
			this.LoadPdb(SymbolReaderFactory.Create(options, this.metadata, pdbStream));
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x00180A28 File Offset: 0x00180A28
		public void LoadPdb()
		{
			this.LoadPdb(PdbReaderOptions.None);
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x00180A34 File Offset: 0x00180A34
		public void LoadPdb(PdbReaderOptions options)
		{
			this.LoadPdb(SymbolReaderFactory.CreateFromAssemblyFile(options, this.metadata, this.location ?? string.Empty));
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x00180A6C File Offset: 0x00180A6C
		internal void InitializeCustomDebugInfos(MDToken token, GenericParamContext gpContext, IList<PdbCustomDebugInfo> result)
		{
			PdbState pdbState = this.pdbState;
			if (pdbState == null)
			{
				return;
			}
			pdbState.InitializeCustomDebugInfos(token, gpContext, result);
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x00180A94 File Offset: 0x00180A94
		private ModuleKind GetKind()
		{
			if (this.TablesStream.AssemblyTable.Rows < 1U)
			{
				return ModuleKind.NetModule;
			}
			IPEImage peimage = this.Metadata.PEImage;
			if ((peimage.ImageNTHeaders.FileHeader.Characteristics & Characteristics.Dll) != ~(Characteristics.RelocsStripped | Characteristics.ExecutableImage | Characteristics.LineNumsStripped | Characteristics.LocalSymsStripped | Characteristics.AggressiveWsTrim | Characteristics.LargeAddressAware | Characteristics.Reserved1 | Characteristics.BytesReversedLo | Characteristics.Bit32Machine | Characteristics.DebugStripped | Characteristics.RemovableRunFromSwap | Characteristics.NetRunFromSwap | Characteristics.System | Characteristics.Dll | Characteristics.UpSystemOnly | Characteristics.BytesReversedHi))
			{
				return ModuleKind.Dll;
			}
			ModuleKind result;
			if (peimage.ImageNTHeaders.OptionalHeader.Subsystem == Subsystem.WindowsCui)
			{
				result = ModuleKind.Console;
			}
			else
			{
				result = ModuleKind.Windows;
			}
			return result;
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x00180B08 File Offset: 0x00180B08
		private void Initialize()
		{
			TablesStream tablesStream = this.metadata.TablesStream;
			this.listModuleDefMD = new SimpleLazyList<ModuleDefMD2>(tablesStream.ModuleTable.Rows, delegate(uint rid2)
			{
				if (rid2 != 1U)
				{
					return new ModuleDefMD2(this, rid2);
				}
				return this;
			});
			this.listTypeRefMD = new SimpleLazyList<TypeRefMD>(tablesStream.TypeRefTable.Rows, (uint rid2) => new TypeRefMD(this, rid2));
			this.listTypeDefMD = new SimpleLazyList<TypeDefMD>(tablesStream.TypeDefTable.Rows, (uint rid2) => new TypeDefMD(this, rid2));
			this.listFieldDefMD = new SimpleLazyList<FieldDefMD>(tablesStream.FieldTable.Rows, (uint rid2) => new FieldDefMD(this, rid2));
			this.listMethodDefMD = new SimpleLazyList<MethodDefMD>(tablesStream.MethodTable.Rows, (uint rid2) => new MethodDefMD(this, rid2));
			this.listParamDefMD = new SimpleLazyList<ParamDefMD>(tablesStream.ParamTable.Rows, (uint rid2) => new ParamDefMD(this, rid2));
			this.listInterfaceImplMD = new SimpleLazyList2<InterfaceImplMD>(tablesStream.InterfaceImplTable.Rows, (uint rid2, GenericParamContext gpContext) => new InterfaceImplMD(this, rid2, gpContext));
			this.listMemberRefMD = new SimpleLazyList2<MemberRefMD>(tablesStream.MemberRefTable.Rows, (uint rid2, GenericParamContext gpContext) => new MemberRefMD(this, rid2, gpContext));
			this.listConstantMD = new SimpleLazyList<ConstantMD>(tablesStream.ConstantTable.Rows, (uint rid2) => new ConstantMD(this, rid2));
			this.listDeclSecurityMD = new SimpleLazyList<DeclSecurityMD>(tablesStream.DeclSecurityTable.Rows, (uint rid2) => new DeclSecurityMD(this, rid2));
			this.listClassLayoutMD = new SimpleLazyList<ClassLayoutMD>(tablesStream.ClassLayoutTable.Rows, (uint rid2) => new ClassLayoutMD(this, rid2));
			this.listStandAloneSigMD = new SimpleLazyList2<StandAloneSigMD>(tablesStream.StandAloneSigTable.Rows, (uint rid2, GenericParamContext gpContext) => new StandAloneSigMD(this, rid2, gpContext));
			this.listEventDefMD = new SimpleLazyList<EventDefMD>(tablesStream.EventTable.Rows, (uint rid2) => new EventDefMD(this, rid2));
			this.listPropertyDefMD = new SimpleLazyList<PropertyDefMD>(tablesStream.PropertyTable.Rows, (uint rid2) => new PropertyDefMD(this, rid2));
			this.listModuleRefMD = new SimpleLazyList<ModuleRefMD>(tablesStream.ModuleRefTable.Rows, (uint rid2) => new ModuleRefMD(this, rid2));
			this.listTypeSpecMD = new SimpleLazyList2<TypeSpecMD>(tablesStream.TypeSpecTable.Rows, (uint rid2, GenericParamContext gpContext) => new TypeSpecMD(this, rid2, gpContext));
			this.listImplMapMD = new SimpleLazyList<ImplMapMD>(tablesStream.ImplMapTable.Rows, (uint rid2) => new ImplMapMD(this, rid2));
			this.listAssemblyDefMD = new SimpleLazyList<AssemblyDefMD>(tablesStream.AssemblyTable.Rows, (uint rid2) => new AssemblyDefMD(this, rid2));
			this.listFileDefMD = new SimpleLazyList<FileDefMD>(tablesStream.FileTable.Rows, (uint rid2) => new FileDefMD(this, rid2));
			this.listAssemblyRefMD = new SimpleLazyList<AssemblyRefMD>(tablesStream.AssemblyRefTable.Rows, (uint rid2) => new AssemblyRefMD(this, rid2));
			this.listExportedTypeMD = new SimpleLazyList<ExportedTypeMD>(tablesStream.ExportedTypeTable.Rows, (uint rid2) => new ExportedTypeMD(this, rid2));
			this.listManifestResourceMD = new SimpleLazyList<ManifestResourceMD>(tablesStream.ManifestResourceTable.Rows, (uint rid2) => new ManifestResourceMD(this, rid2));
			this.listGenericParamMD = new SimpleLazyList<GenericParamMD>(tablesStream.GenericParamTable.Rows, (uint rid2) => new GenericParamMD(this, rid2));
			this.listMethodSpecMD = new SimpleLazyList2<MethodSpecMD>(tablesStream.MethodSpecTable.Rows, (uint rid2, GenericParamContext gpContext) => new MethodSpecMD(this, rid2, gpContext));
			this.listGenericParamConstraintMD = new SimpleLazyList2<GenericParamConstraintMD>(tablesStream.GenericParamConstraintTable.Rows, (uint rid2, GenericParamContext gpContext) => new GenericParamConstraintMD(this, rid2, gpContext));
			for (int i = 0; i < 64; i++)
			{
				MDTable mdtable = this.TablesStream.Get((Table)i);
				this.lastUsedRids[i] = (int)((mdtable == null) ? 0U : mdtable.Rows);
			}
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x00180EB0 File Offset: 0x00180EB0
		private AssemblyRef FindCorLibAssemblyRef()
		{
			uint rows = this.TablesStream.AssemblyRefTable.Rows;
			AssemblyRef assemblyRef = null;
			int num = int.MinValue;
			for (uint num2 = 1U; num2 <= rows; num2 += 1U)
			{
				AssemblyRef assemblyRef2 = this.ResolveAssemblyRef(num2);
				int num3;
				if (ModuleDefMD.preferredCorLibs.TryGetValue(assemblyRef2.FullName, out num3) && num3 > num)
				{
					num = num3;
					assemblyRef = assemblyRef2;
				}
			}
			if (assemblyRef != null)
			{
				return assemblyRef;
			}
			foreach (string value in ModuleDefMD.corlibs)
			{
				for (uint num4 = 1U; num4 <= rows; num4 += 1U)
				{
					AssemblyRef assemblyRef3 = this.ResolveAssemblyRef(num4);
					if (UTF8String.ToSystemStringOrEmpty(assemblyRef3.Name).Equals(value, StringComparison.OrdinalIgnoreCase) && ModuleDef.IsGreaterAssemblyRefVersion(assemblyRef, assemblyRef3))
					{
						assemblyRef = assemblyRef3;
					}
				}
				if (assemblyRef != null)
				{
					return assemblyRef;
				}
			}
			AssemblyDef assembly = base.Assembly;
			if (assembly != null && (assembly.IsCorLib() || base.Find("System.Object", false) != null))
			{
				base.IsCoreLibraryModule = new bool?(true);
				return base.UpdateRowId<AssemblyRefUser>(new AssemblyRefUser(assembly));
			}
			return assemblyRef;
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x00180FE0 File Offset: 0x00180FE0
		private AssemblyRef CreateDefaultCorLibAssemblyRef()
		{
			AssemblyRef alternativeCorLibReference = this.GetAlternativeCorLibReference();
			if (alternativeCorLibReference != null)
			{
				return base.UpdateRowId<AssemblyRef>(alternativeCorLibReference);
			}
			if (base.IsClr40)
			{
				return base.UpdateRowId<AssemblyRefUser>(AssemblyRefUser.CreateMscorlibReferenceCLR40());
			}
			if (base.IsClr20)
			{
				return base.UpdateRowId<AssemblyRefUser>(AssemblyRefUser.CreateMscorlibReferenceCLR20());
			}
			if (base.IsClr11)
			{
				return base.UpdateRowId<AssemblyRefUser>(AssemblyRefUser.CreateMscorlibReferenceCLR11());
			}
			if (base.IsClr10)
			{
				return base.UpdateRowId<AssemblyRefUser>(AssemblyRefUser.CreateMscorlibReferenceCLR10());
			}
			return base.UpdateRowId<AssemblyRefUser>(AssemblyRefUser.CreateMscorlibReferenceCLR40());
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x00181070 File Offset: 0x00181070
		private AssemblyRef GetAlternativeCorLibReference()
		{
			foreach (AssemblyRef assemblyRef in base.GetAssemblyRefs())
			{
				if (ModuleDefMD.IsAssemblyRef(assemblyRef, ModuleDefMD.systemRuntimeName, ModuleDefMD.contractsPublicKeyToken))
				{
					return assemblyRef;
				}
			}
			foreach (AssemblyRef assemblyRef2 in base.GetAssemblyRefs())
			{
				if (ModuleDefMD.IsAssemblyRef(assemblyRef2, ModuleDefMD.corefxName, ModuleDefMD.contractsPublicKeyToken))
				{
					return assemblyRef2;
				}
			}
			return null;
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x00181138 File Offset: 0x00181138
		private static bool IsAssemblyRef(AssemblyRef asmRef, UTF8String name, PublicKeyToken token)
		{
			if (asmRef.Name != name)
			{
				return false;
			}
			PublicKeyBase publicKeyOrToken = asmRef.PublicKeyOrToken;
			return publicKeyOrToken != null && token.Equals(publicKeyOrToken.Token);
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x00181178 File Offset: 0x00181178
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				MetadataBase metadataBase = this.metadata;
				if (metadataBase != null)
				{
					metadataBase.Dispose();
				}
				this.metadata = null;
			}
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x001811B0 File Offset: 0x001811B0
		public override IMDTokenProvider ResolveToken(uint token, GenericParamContext gpContext)
		{
			uint rid = MDToken.ToRID(token);
			switch (MDToken.ToTable(token))
			{
			case Table.Module:
				return this.ResolveModule(rid);
			case Table.TypeRef:
				return this.ResolveTypeRef(rid);
			case Table.TypeDef:
				return this.ResolveTypeDef(rid);
			case Table.Field:
				return this.ResolveField(rid);
			case Table.Method:
				return this.ResolveMethod(rid);
			case Table.Param:
				return this.ResolveParam(rid);
			case Table.InterfaceImpl:
				return this.ResolveInterfaceImpl(rid, gpContext);
			case Table.MemberRef:
				return this.ResolveMemberRef(rid, gpContext);
			case Table.Constant:
				return this.ResolveConstant(rid);
			case Table.DeclSecurity:
				return this.ResolveDeclSecurity(rid);
			case Table.ClassLayout:
				return this.ResolveClassLayout(rid);
			case Table.StandAloneSig:
				return this.ResolveStandAloneSig(rid, gpContext);
			case Table.Event:
				return this.ResolveEvent(rid);
			case Table.Property:
				return this.ResolveProperty(rid);
			case Table.ModuleRef:
				return this.ResolveModuleRef(rid);
			case Table.TypeSpec:
				return this.ResolveTypeSpec(rid, gpContext);
			case Table.ImplMap:
				return this.ResolveImplMap(rid);
			case Table.Assembly:
				return this.ResolveAssembly(rid);
			case Table.AssemblyRef:
				return this.ResolveAssemblyRef(rid);
			case Table.File:
				return this.ResolveFile(rid);
			case Table.ExportedType:
				return this.ResolveExportedType(rid);
			case Table.ManifestResource:
				return this.ResolveManifestResource(rid);
			case Table.GenericParam:
				return this.ResolveGenericParam(rid);
			case Table.MethodSpec:
				return this.ResolveMethodSpec(rid, gpContext);
			case Table.GenericParamConstraint:
				return this.ResolveGenericParamConstraint(rid, gpContext);
			}
			return null;
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x001813DC File Offset: 0x001813DC
		public ModuleDef ResolveModule(uint rid)
		{
			return this.listModuleDefMD[rid - 1U];
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x001813EC File Offset: 0x001813EC
		public TypeRef ResolveTypeRef(uint rid)
		{
			return this.listTypeRefMD[rid - 1U];
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x001813FC File Offset: 0x001813FC
		public TypeDef ResolveTypeDef(uint rid)
		{
			return this.listTypeDefMD[rid - 1U];
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0018140C File Offset: 0x0018140C
		public FieldDef ResolveField(uint rid)
		{
			return this.listFieldDefMD[rid - 1U];
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0018141C File Offset: 0x0018141C
		public MethodDef ResolveMethod(uint rid)
		{
			return this.listMethodDefMD[rid - 1U];
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0018142C File Offset: 0x0018142C
		public ParamDef ResolveParam(uint rid)
		{
			return this.listParamDefMD[rid - 1U];
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0018143C File Offset: 0x0018143C
		public InterfaceImpl ResolveInterfaceImpl(uint rid)
		{
			return this.listInterfaceImplMD[rid - 1U, default(GenericParamContext)];
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x00181464 File Offset: 0x00181464
		public InterfaceImpl ResolveInterfaceImpl(uint rid, GenericParamContext gpContext)
		{
			return this.listInterfaceImplMD[rid - 1U, gpContext];
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x00181478 File Offset: 0x00181478
		public MemberRef ResolveMemberRef(uint rid)
		{
			return this.listMemberRefMD[rid - 1U, default(GenericParamContext)];
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x001814A0 File Offset: 0x001814A0
		public MemberRef ResolveMemberRef(uint rid, GenericParamContext gpContext)
		{
			return this.listMemberRefMD[rid - 1U, gpContext];
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x001814B4 File Offset: 0x001814B4
		public Constant ResolveConstant(uint rid)
		{
			return this.listConstantMD[rid - 1U];
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x001814C4 File Offset: 0x001814C4
		public DeclSecurity ResolveDeclSecurity(uint rid)
		{
			return this.listDeclSecurityMD[rid - 1U];
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x001814D4 File Offset: 0x001814D4
		public ClassLayout ResolveClassLayout(uint rid)
		{
			return this.listClassLayoutMD[rid - 1U];
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x001814E4 File Offset: 0x001814E4
		public StandAloneSig ResolveStandAloneSig(uint rid)
		{
			return this.listStandAloneSigMD[rid - 1U, default(GenericParamContext)];
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x0018150C File Offset: 0x0018150C
		public StandAloneSig ResolveStandAloneSig(uint rid, GenericParamContext gpContext)
		{
			return this.listStandAloneSigMD[rid - 1U, gpContext];
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x00181520 File Offset: 0x00181520
		public EventDef ResolveEvent(uint rid)
		{
			return this.listEventDefMD[rid - 1U];
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x00181530 File Offset: 0x00181530
		public PropertyDef ResolveProperty(uint rid)
		{
			return this.listPropertyDefMD[rid - 1U];
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x00181540 File Offset: 0x00181540
		public ModuleRef ResolveModuleRef(uint rid)
		{
			return this.listModuleRefMD[rid - 1U];
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x00181550 File Offset: 0x00181550
		public TypeSpec ResolveTypeSpec(uint rid)
		{
			return this.listTypeSpecMD[rid - 1U, default(GenericParamContext)];
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x00181578 File Offset: 0x00181578
		public TypeSpec ResolveTypeSpec(uint rid, GenericParamContext gpContext)
		{
			return this.listTypeSpecMD[rid - 1U, gpContext];
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x0018158C File Offset: 0x0018158C
		public ImplMap ResolveImplMap(uint rid)
		{
			return this.listImplMapMD[rid - 1U];
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x0018159C File Offset: 0x0018159C
		public AssemblyDef ResolveAssembly(uint rid)
		{
			return this.listAssemblyDefMD[rid - 1U];
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x001815AC File Offset: 0x001815AC
		public AssemblyRef ResolveAssemblyRef(uint rid)
		{
			return this.listAssemblyRefMD[rid - 1U];
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x001815BC File Offset: 0x001815BC
		public FileDef ResolveFile(uint rid)
		{
			return this.listFileDefMD[rid - 1U];
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x001815CC File Offset: 0x001815CC
		public ExportedType ResolveExportedType(uint rid)
		{
			return this.listExportedTypeMD[rid - 1U];
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x001815DC File Offset: 0x001815DC
		public ManifestResource ResolveManifestResource(uint rid)
		{
			return this.listManifestResourceMD[rid - 1U];
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x001815EC File Offset: 0x001815EC
		public GenericParam ResolveGenericParam(uint rid)
		{
			return this.listGenericParamMD[rid - 1U];
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x001815FC File Offset: 0x001815FC
		public MethodSpec ResolveMethodSpec(uint rid)
		{
			return this.listMethodSpecMD[rid - 1U, default(GenericParamContext)];
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x00181624 File Offset: 0x00181624
		public MethodSpec ResolveMethodSpec(uint rid, GenericParamContext gpContext)
		{
			return this.listMethodSpecMD[rid - 1U, gpContext];
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x00181638 File Offset: 0x00181638
		public GenericParamConstraint ResolveGenericParamConstraint(uint rid)
		{
			return this.listGenericParamConstraintMD[rid - 1U, default(GenericParamContext)];
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x00181660 File Offset: 0x00181660
		public GenericParamConstraint ResolveGenericParamConstraint(uint rid, GenericParamContext gpContext)
		{
			return this.listGenericParamConstraintMD[rid - 1U, gpContext];
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x00181674 File Offset: 0x00181674
		public ITypeDefOrRef ResolveTypeDefOrRef(uint codedToken)
		{
			return this.ResolveTypeDefOrRef(codedToken, default(GenericParamContext));
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x00181698 File Offset: 0x00181698
		public ITypeDefOrRef ResolveTypeDefOrRef(uint codedToken, GenericParamContext gpContext)
		{
			uint token;
			if (!CodedToken.TypeDefOrRef.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			ITypeDefOrRef result;
			if (table != Table.TypeRef)
			{
				if (table != Table.TypeDef)
				{
					if (table != Table.TypeSpec)
					{
						result = null;
					}
					else
					{
						result = this.ResolveTypeSpec(rid, gpContext);
					}
				}
				else
				{
					result = this.ResolveTypeDef(rid);
				}
			}
			else
			{
				result = this.ResolveTypeRef(rid);
			}
			return result;
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x00181714 File Offset: 0x00181714
		public IHasConstant ResolveHasConstant(uint codedToken)
		{
			uint token;
			if (!CodedToken.HasConstant.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			IHasConstant result;
			if (table != Table.Field)
			{
				if (table != Table.Param)
				{
					if (table != Table.Property)
					{
						result = null;
					}
					else
					{
						result = this.ResolveProperty(rid);
					}
				}
				else
				{
					result = this.ResolveParam(rid);
				}
			}
			else
			{
				result = this.ResolveField(rid);
			}
			return result;
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0018178C File Offset: 0x0018178C
		public IHasCustomAttribute ResolveHasCustomAttribute(uint codedToken)
		{
			return this.ResolveHasCustomAttribute(codedToken, default(GenericParamContext));
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x001817B0 File Offset: 0x001817B0
		public IHasCustomAttribute ResolveHasCustomAttribute(uint codedToken, GenericParamContext gpContext)
		{
			uint token;
			if (!CodedToken.HasCustomAttribute.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			switch (table)
			{
			case Table.Module:
				return this.ResolveModule(rid);
			case Table.TypeRef:
				return this.ResolveTypeRef(rid);
			case Table.TypeDef:
				return this.ResolveTypeDef(rid);
			case Table.FieldPtr:
			case Table.MethodPtr:
			case Table.ParamPtr:
			case Table.Constant:
			case Table.CustomAttribute:
			case Table.FieldMarshal:
			case Table.ClassLayout:
			case Table.FieldLayout:
			case Table.EventMap:
			case Table.EventPtr:
				break;
			case Table.Field:
				return this.ResolveField(rid);
			case Table.Method:
				return this.ResolveMethod(rid);
			case Table.Param:
				return this.ResolveParam(rid);
			case Table.InterfaceImpl:
				return this.ResolveInterfaceImpl(rid, gpContext);
			case Table.MemberRef:
				return this.ResolveMemberRef(rid, gpContext);
			case Table.DeclSecurity:
				return this.ResolveDeclSecurity(rid);
			case Table.StandAloneSig:
				return this.ResolveStandAloneSig(rid, gpContext);
			case Table.Event:
				return this.ResolveEvent(rid);
			default:
				switch (table)
				{
				case Table.Property:
					return this.ResolveProperty(rid);
				case Table.MethodSemantics:
				case Table.MethodImpl:
					break;
				case Table.ModuleRef:
					return this.ResolveModuleRef(rid);
				case Table.TypeSpec:
					return this.ResolveTypeSpec(rid, gpContext);
				default:
					switch (table)
					{
					case Table.Assembly:
						return this.ResolveAssembly(rid);
					case Table.AssemblyRef:
						return this.ResolveAssemblyRef(rid);
					case Table.File:
						return this.ResolveFile(rid);
					case Table.ExportedType:
						return this.ResolveExportedType(rid);
					case Table.ManifestResource:
						return this.ResolveManifestResource(rid);
					case Table.GenericParam:
						return this.ResolveGenericParam(rid);
					case Table.MethodSpec:
						return this.ResolveMethodSpec(rid, gpContext);
					case Table.GenericParamConstraint:
						return this.ResolveGenericParamConstraint(rid, gpContext);
					}
					break;
				}
				break;
			}
			return null;
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x001819C4 File Offset: 0x001819C4
		public IHasFieldMarshal ResolveHasFieldMarshal(uint codedToken)
		{
			uint token;
			if (!CodedToken.HasFieldMarshal.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			IHasFieldMarshal result;
			if (table != Table.Field)
			{
				if (table != Table.Param)
				{
					result = null;
				}
				else
				{
					result = this.ResolveParam(rid);
				}
			}
			else
			{
				result = this.ResolveField(rid);
			}
			return result;
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x00181A28 File Offset: 0x00181A28
		public IHasDeclSecurity ResolveHasDeclSecurity(uint codedToken)
		{
			uint token;
			if (!CodedToken.HasDeclSecurity.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			IHasDeclSecurity result;
			if (table != Table.TypeDef)
			{
				if (table != Table.Method)
				{
					if (table != Table.Assembly)
					{
						result = null;
					}
					else
					{
						result = this.ResolveAssembly(rid);
					}
				}
				else
				{
					result = this.ResolveMethod(rid);
				}
			}
			else
			{
				result = this.ResolveTypeDef(rid);
			}
			return result;
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x00181AA0 File Offset: 0x00181AA0
		public IMemberRefParent ResolveMemberRefParent(uint codedToken)
		{
			return this.ResolveMemberRefParent(codedToken, default(GenericParamContext));
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x00181AC4 File Offset: 0x00181AC4
		public IMemberRefParent ResolveMemberRefParent(uint codedToken, GenericParamContext gpContext)
		{
			uint token;
			if (!CodedToken.MemberRefParent.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			if (table <= Table.TypeDef)
			{
				if (table == Table.TypeRef)
				{
					return this.ResolveTypeRef(rid);
				}
				if (table == Table.TypeDef)
				{
					return this.ResolveTypeDef(rid);
				}
			}
			else
			{
				if (table == Table.Method)
				{
					return this.ResolveMethod(rid);
				}
				if (table == Table.ModuleRef)
				{
					return this.ResolveModuleRef(rid);
				}
				if (table == Table.TypeSpec)
				{
					return this.ResolveTypeSpec(rid, gpContext);
				}
			}
			return null;
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x00181B74 File Offset: 0x00181B74
		public IHasSemantic ResolveHasSemantic(uint codedToken)
		{
			uint token;
			if (!CodedToken.HasSemantic.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			IHasSemantic result;
			if (table != Table.Event)
			{
				if (table != Table.Property)
				{
					result = null;
				}
				else
				{
					result = this.ResolveProperty(rid);
				}
			}
			else
			{
				result = this.ResolveEvent(rid);
			}
			return result;
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x00181BDC File Offset: 0x00181BDC
		public IMethodDefOrRef ResolveMethodDefOrRef(uint codedToken)
		{
			return this.ResolveMethodDefOrRef(codedToken, default(GenericParamContext));
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x00181C00 File Offset: 0x00181C00
		public IMethodDefOrRef ResolveMethodDefOrRef(uint codedToken, GenericParamContext gpContext)
		{
			uint token;
			if (!CodedToken.MethodDefOrRef.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			IMethodDefOrRef result;
			if (table != Table.Method)
			{
				if (table != Table.MemberRef)
				{
					result = null;
				}
				else
				{
					result = this.ResolveMemberRef(rid, gpContext);
				}
			}
			else
			{
				result = this.ResolveMethod(rid);
			}
			return result;
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x00181C68 File Offset: 0x00181C68
		public IMemberForwarded ResolveMemberForwarded(uint codedToken)
		{
			uint token;
			if (!CodedToken.MemberForwarded.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			IMemberForwarded result;
			if (table != Table.Field)
			{
				if (table != Table.Method)
				{
					result = null;
				}
				else
				{
					result = this.ResolveMethod(rid);
				}
			}
			else
			{
				result = this.ResolveField(rid);
			}
			return result;
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x00181CCC File Offset: 0x00181CCC
		public IImplementation ResolveImplementation(uint codedToken)
		{
			uint token;
			if (!CodedToken.Implementation.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			switch (MDToken.ToTable(token))
			{
			case Table.AssemblyRef:
				return this.ResolveAssemblyRef(rid);
			case Table.File:
				return this.ResolveFile(rid);
			case Table.ExportedType:
				return this.ResolveExportedType(rid);
			}
			return null;
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x00181D4C File Offset: 0x00181D4C
		public ICustomAttributeType ResolveCustomAttributeType(uint codedToken)
		{
			return this.ResolveCustomAttributeType(codedToken, default(GenericParamContext));
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x00181D70 File Offset: 0x00181D70
		public ICustomAttributeType ResolveCustomAttributeType(uint codedToken, GenericParamContext gpContext)
		{
			uint token;
			if (!CodedToken.CustomAttributeType.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			ICustomAttributeType result;
			if (table != Table.Method)
			{
				if (table != Table.MemberRef)
				{
					result = null;
				}
				else
				{
					result = this.ResolveMemberRef(rid, gpContext);
				}
			}
			else
			{
				result = this.ResolveMethod(rid);
			}
			return result;
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x00181DD8 File Offset: 0x00181DD8
		public IResolutionScope ResolveResolutionScope(uint codedToken)
		{
			uint token;
			if (!CodedToken.ResolutionScope.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			if (table <= Table.TypeRef)
			{
				if (table == Table.Module)
				{
					return this.ResolveModule(rid);
				}
				if (table == Table.TypeRef)
				{
					return this.ResolveTypeRef(rid);
				}
			}
			else
			{
				if (table == Table.ModuleRef)
				{
					return this.ResolveModuleRef(rid);
				}
				if (table == Table.AssemblyRef)
				{
					return this.ResolveAssemblyRef(rid);
				}
			}
			return null;
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x00181E70 File Offset: 0x00181E70
		public ITypeOrMethodDef ResolveTypeOrMethodDef(uint codedToken)
		{
			uint token;
			if (!CodedToken.TypeOrMethodDef.Decode(codedToken, out token))
			{
				return null;
			}
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			ITypeOrMethodDef result;
			if (table != Table.TypeDef)
			{
				if (table != Table.Method)
				{
					result = null;
				}
				else
				{
					result = this.ResolveMethod(rid);
				}
			}
			else
			{
				result = this.ResolveTypeDef(rid);
			}
			return result;
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x00181ED4 File Offset: 0x00181ED4
		public CallingConventionSig ReadSignature(uint sig)
		{
			return SignatureReader.ReadSig(this, sig, default(GenericParamContext));
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x00181EF8 File Offset: 0x00181EF8
		public CallingConventionSig ReadSignature(uint sig, GenericParamContext gpContext)
		{
			return SignatureReader.ReadSig(this, sig, gpContext);
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x00181F04 File Offset: 0x00181F04
		public TypeSig ReadTypeSignature(uint sig)
		{
			return SignatureReader.ReadTypeSig(this, sig, default(GenericParamContext));
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x00181F28 File Offset: 0x00181F28
		public TypeSig ReadTypeSignature(uint sig, GenericParamContext gpContext)
		{
			return SignatureReader.ReadTypeSig(this, sig, gpContext);
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x00181F34 File Offset: 0x00181F34
		public TypeSig ReadTypeSignature(uint sig, out byte[] extraData)
		{
			return SignatureReader.ReadTypeSig(this, sig, default(GenericParamContext), out extraData);
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x00181F58 File Offset: 0x00181F58
		public TypeSig ReadTypeSignature(uint sig, GenericParamContext gpContext, out byte[] extraData)
		{
			return SignatureReader.ReadTypeSig(this, sig, gpContext, out extraData);
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x00181F64 File Offset: 0x00181F64
		internal MarshalType ReadMarshalType(Table table, uint rid, GenericParamContext gpContext)
		{
			RawFieldMarshalRow rawFieldMarshalRow;
			if (!this.TablesStream.TryReadFieldMarshalRow(this.Metadata.GetFieldMarshalRid(table, rid), out rawFieldMarshalRow))
			{
				return null;
			}
			return MarshalBlobReader.Read(this, rawFieldMarshalRow.NativeType, gpContext);
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x00181FA4 File Offset: 0x00181FA4
		public CilBody ReadCilBody(IList<Parameter> parameters, RVA rva)
		{
			return this.ReadCilBody(parameters, rva, default(GenericParamContext));
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x00181FC8 File Offset: 0x00181FC8
		public CilBody ReadCilBody(IList<Parameter> parameters, RVA rva, GenericParamContext gpContext)
		{
			if (rva == (RVA)0U)
			{
				return new CilBody();
			}
			DataReader reader = this.metadata.PEImage.CreateReader();
			reader.Position = (uint)this.metadata.PEImage.ToFileOffset(rva);
			return MethodBodyReader.CreateCilBody(this, reader, parameters, gpContext);
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x00182018 File Offset: 0x00182018
		internal TypeDef GetOwnerType(FieldDefMD field)
		{
			return this.ResolveTypeDef(this.Metadata.GetOwnerTypeOfField(field.OrigRid));
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x00182040 File Offset: 0x00182040
		internal TypeDef GetOwnerType(MethodDefMD method)
		{
			return this.ResolveTypeDef(this.Metadata.GetOwnerTypeOfMethod(method.OrigRid));
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x00182068 File Offset: 0x00182068
		internal TypeDef GetOwnerType(EventDefMD evt)
		{
			return this.ResolveTypeDef(this.Metadata.GetOwnerTypeOfEvent(evt.OrigRid));
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x00182090 File Offset: 0x00182090
		internal TypeDef GetOwnerType(PropertyDefMD property)
		{
			return this.ResolveTypeDef(this.Metadata.GetOwnerTypeOfProperty(property.OrigRid));
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x001820B8 File Offset: 0x001820B8
		internal ITypeOrMethodDef GetOwner(GenericParamMD gp)
		{
			return this.ResolveTypeOrMethodDef(this.Metadata.GetOwnerOfGenericParam(gp.OrigRid));
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x001820E0 File Offset: 0x001820E0
		internal GenericParam GetOwner(GenericParamConstraintMD gpc)
		{
			return this.ResolveGenericParam(this.Metadata.GetOwnerOfGenericParamConstraint(gpc.OrigRid));
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x00182108 File Offset: 0x00182108
		internal MethodDef GetOwner(ParamDefMD pd)
		{
			return this.ResolveMethod(this.Metadata.GetOwnerOfParam(pd.OrigRid));
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x00182130 File Offset: 0x00182130
		internal ModuleDefMD ReadModule(uint fileRid, AssemblyDef owner)
		{
			FileDef fileDef = this.ResolveFile(fileRid);
			if (fileDef == null)
			{
				return null;
			}
			if (!fileDef.ContainsMetadata)
			{
				return null;
			}
			string validFilename = ModuleDefMD.GetValidFilename(this.GetBaseDirectoryOfImage(), UTF8String.ToSystemString(fileDef.Name));
			if (validFilename == null)
			{
				return null;
			}
			ModuleDefMD moduleDefMD;
			try
			{
				moduleDefMD = ModuleDefMD.Load(validFilename, null);
			}
			catch
			{
				moduleDefMD = null;
			}
			if (moduleDefMD != null)
			{
				moduleDefMD.context = this.context;
				AssemblyDef assembly = moduleDefMD.Assembly;
				if (assembly != null && assembly != owner)
				{
					assembly.Modules.Remove(moduleDefMD);
				}
			}
			return moduleDefMD;
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x001821D4 File Offset: 0x001821D4
		internal RidList GetModuleRidList()
		{
			if (this.moduleRidList == null)
			{
				this.InitializeModuleList();
			}
			return this.moduleRidList.Value;
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x001821F4 File Offset: 0x001821F4
		private void InitializeModuleList()
		{
			if (this.moduleRidList != null)
			{
				return;
			}
			uint rows = this.TablesStream.FileTable.Rows;
			List<uint> list = new List<uint>((int)rows);
			string baseDirectoryOfImage = this.GetBaseDirectoryOfImage();
			for (uint num = 1U; num <= rows; num += 1U)
			{
				FileDef fileDef = this.ResolveFile(num);
				if (fileDef != null && fileDef.ContainsMetadata && ModuleDefMD.GetValidFilename(baseDirectoryOfImage, UTF8String.ToSystemString(fileDef.Name)) != null)
				{
					list.Add(num);
				}
			}
			Interlocked.CompareExchange<StrongBox<RidList>>(ref this.moduleRidList, new StrongBox<RidList>(RidList.Create(list)), null);
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x00182294 File Offset: 0x00182294
		private static string GetValidFilename(string baseDir, string name)
		{
			if (baseDir == null)
			{
				return null;
			}
			string text;
			try
			{
				if (name.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
				{
					return null;
				}
				text = Path.Combine(baseDir, name);
				if (text != Path.GetFullPath(text))
				{
					return null;
				}
				if (!File.Exists(text))
				{
					return null;
				}
			}
			catch
			{
				return null;
			}
			return text;
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x00182314 File Offset: 0x00182314
		private string GetBaseDirectoryOfImage()
		{
			string location = base.Location;
			if (string.IsNullOrEmpty(location))
			{
				return null;
			}
			try
			{
				return Path.GetDirectoryName(location);
			}
			catch (IOException)
			{
			}
			catch (ArgumentException)
			{
			}
			return null;
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x00182370 File Offset: 0x00182370
		private Resource CreateResource(uint rid)
		{
			RawManifestResourceRow rawManifestResourceRow;
			if (!this.TablesStream.TryReadManifestResourceRow(rid, out rawManifestResourceRow))
			{
				return new EmbeddedResource(UTF8String.Empty, Array2.Empty<byte>(), (ManifestResourceAttributes)0U)
				{
					Rid = rid
				};
			}
			MDToken mdtoken;
			if (!CodedToken.Implementation.Decode(rawManifestResourceRow.Implementation, out mdtoken))
			{
				return new EmbeddedResource(UTF8String.Empty, Array2.Empty<byte>(), (ManifestResourceAttributes)0U)
				{
					Rid = rid
				};
			}
			ManifestResource manifestResource = this.ResolveManifestResource(rid);
			if (manifestResource == null)
			{
				return new EmbeddedResource(UTF8String.Empty, Array2.Empty<byte>(), (ManifestResourceAttributes)0U)
				{
					Rid = rid
				};
			}
			if (mdtoken.Rid == 0U)
			{
				DataReaderFactory dataReaderFactory;
				uint offset;
				uint length;
				if (this.TryCreateResourceStream(manifestResource.Offset, out dataReaderFactory, out offset, out length))
				{
					return new EmbeddedResource(manifestResource.Name, dataReaderFactory, offset, length, manifestResource.Flags)
					{
						Rid = rid,
						Offset = new uint?(manifestResource.Offset)
					};
				}
				return new EmbeddedResource(manifestResource.Name, Array2.Empty<byte>(), manifestResource.Flags)
				{
					Rid = rid,
					Offset = new uint?(manifestResource.Offset)
				};
			}
			else
			{
				FileDef fileDef = manifestResource.Implementation as FileDef;
				if (fileDef != null)
				{
					return new LinkedResource(manifestResource.Name, fileDef, manifestResource.Flags)
					{
						Rid = rid,
						Offset = new uint?(manifestResource.Offset)
					};
				}
				AssemblyRef assemblyRef = manifestResource.Implementation as AssemblyRef;
				if (assemblyRef != null)
				{
					return new AssemblyLinkedResource(manifestResource.Name, assemblyRef, manifestResource.Flags)
					{
						Rid = rid,
						Offset = new uint?(manifestResource.Offset)
					};
				}
				return new EmbeddedResource(manifestResource.Name, Array2.Empty<byte>(), manifestResource.Flags)
				{
					Rid = rid,
					Offset = new uint?(manifestResource.Offset)
				};
			}
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x00182530 File Offset: 0x00182530
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		private bool TryCreateResourceStream(uint offset, out DataReaderFactory dataReaderFactory, out uint resourceOffset, out uint resourceLength)
		{
			dataReaderFactory = null;
			resourceOffset = 0U;
			resourceLength = 0U;
			try
			{
				IPEImage peimage = this.metadata.PEImage;
				ImageDataDirectory resources = this.metadata.ImageCor20Header.Resources;
				if (resources.VirtualAddress == (RVA)0U || resources.Size == 0U)
				{
					return false;
				}
				DataReader dataReader = peimage.CreateReader();
				uint num = (uint)peimage.ToFileOffset(resources.VirtualAddress);
				if (num == 0U || (ulong)num + (ulong)offset > (ulong)-1)
				{
					return false;
				}
				if ((ulong)offset + 4UL > (ulong)resources.Size)
				{
					return false;
				}
				if ((ulong)num + (ulong)offset + 4UL > (ulong)dataReader.Length)
				{
					return false;
				}
				dataReader.Position = num + offset;
				resourceLength = dataReader.ReadUInt32();
				resourceOffset = dataReader.Position;
				if (resourceLength == 0U || (ulong)dataReader.Position + (ulong)resourceLength > (ulong)dataReader.Length)
				{
					return false;
				}
				if ((ulong)dataReader.Position - (ulong)num + (ulong)resourceLength - 1UL >= (ulong)resources.Size)
				{
					return false;
				}
				if (peimage.MayHaveInvalidAddresses)
				{
					DataReader dataReader2 = peimage.CreateReader((FileOffset)dataReader.Position, resourceLength);
					while (dataReader2.Position < dataReader2.Length)
					{
						dataReader2.ReadByte();
						dataReader2.Position += Math.Min(dataReader2.BytesLeft, 4096U);
					}
					dataReader2.Position = dataReader2.Length - 1U;
					dataReader2.ReadByte();
				}
				dataReaderFactory = peimage.DataReaderFactory;
				return true;
			}
			catch (IOException)
			{
			}
			catch (AccessViolationException)
			{
			}
			return false;
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x00182718 File Offset: 0x00182718
		public CustomAttribute ReadCustomAttribute(uint caRid)
		{
			return this.ReadCustomAttribute(caRid, default(GenericParamContext));
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0018273C File Offset: 0x0018273C
		public CustomAttribute ReadCustomAttribute(uint caRid, GenericParamContext gpContext)
		{
			RawCustomAttributeRow rawCustomAttributeRow;
			if (!this.TablesStream.TryReadCustomAttributeRow(caRid, out rawCustomAttributeRow))
			{
				return null;
			}
			return CustomAttributeReader.Read(this, this.ResolveCustomAttributeType(rawCustomAttributeRow.Type, gpContext), rawCustomAttributeRow.Value, gpContext);
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x0018277C File Offset: 0x0018277C
		public byte[] ReadDataAt(RVA rva, int size)
		{
			if (size < 0)
			{
				return null;
			}
			DataReader dataReader = this.Metadata.PEImage.CreateReader(rva, (uint)size);
			if ((ulong)dataReader.Length < (ulong)((long)size))
			{
				return null;
			}
			return dataReader.ReadBytes(size);
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x001827C4 File Offset: 0x001827C4
		public RVA GetNativeEntryPoint()
		{
			dnlib.DotNet.MD.ImageCor20Header imageCor20Header = this.Metadata.ImageCor20Header;
			if ((imageCor20Header.Flags & ComImageFlags.NativeEntryPoint) == (ComImageFlags)0U)
			{
				return (RVA)0U;
			}
			return (RVA)imageCor20Header.EntryPointToken_or_RVA;
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x001827F8 File Offset: 0x001827F8
		public IManagedEntryPoint GetManagedEntryPoint()
		{
			dnlib.DotNet.MD.ImageCor20Header imageCor20Header = this.Metadata.ImageCor20Header;
			if ((imageCor20Header.Flags & ComImageFlags.NativeEntryPoint) != (ComImageFlags)0U)
			{
				return null;
			}
			return base.ResolveToken(imageCor20Header.EntryPointToken_or_RVA) as IManagedEntryPoint;
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x00182838 File Offset: 0x00182838
		internal FieldDefMD ReadField(uint rid)
		{
			return new FieldDefMD(this, rid);
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x00182844 File Offset: 0x00182844
		internal MethodDefMD ReadMethod(uint rid)
		{
			return new MethodDefMD(this, rid);
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x00182850 File Offset: 0x00182850
		internal EventDefMD ReadEvent(uint rid)
		{
			return new EventDefMD(this, rid);
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x0018285C File Offset: 0x0018285C
		internal PropertyDefMD ReadProperty(uint rid)
		{
			return new PropertyDefMD(this, rid);
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x00182868 File Offset: 0x00182868
		internal ParamDefMD ReadParam(uint rid)
		{
			return new ParamDefMD(this, rid);
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x00182874 File Offset: 0x00182874
		internal GenericParamMD ReadGenericParam(uint rid)
		{
			return new GenericParamMD(this, rid);
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x00182880 File Offset: 0x00182880
		internal GenericParamConstraintMD ReadGenericParamConstraint(uint rid)
		{
			return new GenericParamConstraintMD(this, rid, default(GenericParamContext));
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x001828A4 File Offset: 0x001828A4
		internal GenericParamConstraintMD ReadGenericParamConstraint(uint rid, GenericParamContext gpContext)
		{
			return new GenericParamConstraintMD(this, rid, gpContext);
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x001828B0 File Offset: 0x001828B0
		internal dnlib.DotNet.Emit.MethodBody ReadMethodBody(MethodDefMD method, RVA rva, MethodImplAttributes implAttrs, GenericParamContext gpContext)
		{
			IMethodDecrypter methodDecrypter = this.methodDecrypter;
			dnlib.DotNet.Emit.MethodBody methodBody;
			if (methodDecrypter != null && methodDecrypter.GetMethodBody(method.OrigRid, rva, method.Parameters, gpContext, out methodBody))
			{
				CilBody cilBody = methodBody as CilBody;
				if (cilBody != null)
				{
					return this.InitializeBodyFromPdb(method, cilBody);
				}
				return methodBody;
			}
			else
			{
				if (rva == (RVA)0U)
				{
					return null;
				}
				MethodImplAttributes methodImplAttributes = implAttrs & MethodImplAttributes.CodeTypeMask;
				if (methodImplAttributes == MethodImplAttributes.IL)
				{
					return this.InitializeBodyFromPdb(method, this.ReadCilBody(method.Parameters, rva, gpContext));
				}
				if (methodImplAttributes == MethodImplAttributes.Native)
				{
					return new NativeMethodBody(rva);
				}
				return null;
			}
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x0018293C File Offset: 0x0018293C
		private CilBody InitializeBodyFromPdb(MethodDefMD method, CilBody body)
		{
			PdbState pdbState = this.pdbState;
			if (pdbState != null)
			{
				pdbState.InitializeMethodBody(this, method, body);
			}
			return body;
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x00182964 File Offset: 0x00182964
		internal void InitializeCustomDebugInfos(MethodDefMD method, CilBody body, IList<PdbCustomDebugInfo> customDebugInfos)
		{
			if (body == null)
			{
				return;
			}
			PdbState pdbState = this.pdbState;
			if (pdbState != null)
			{
				pdbState.InitializeCustomDebugInfos(method, body, customDebugInfos);
			}
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x00182994 File Offset: 0x00182994
		public string ReadUserString(uint token)
		{
			IStringDecrypter stringDecrypter = this.stringDecrypter;
			if (stringDecrypter != null)
			{
				string text = stringDecrypter.ReadUserString(token);
				if (text != null)
				{
					return text;
				}
			}
			return this.USStream.ReadNoNull(token & 16777215U);
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x001829D4 File Offset: 0x001829D4
		internal MethodExportInfo GetExportInfo(uint methodRid)
		{
			if (this.methodExportInfoProvider == null)
			{
				this.InitializeMethodExportInfoProvider();
			}
			return this.methodExportInfoProvider.GetMethodExportInfo(100663296U + methodRid);
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x001829FC File Offset: 0x001829FC
		private void InitializeMethodExportInfoProvider()
		{
			Interlocked.CompareExchange<MethodExportInfoProvider>(ref this.methodExportInfoProvider, new MethodExportInfoProvider(this), null);
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x00182A14 File Offset: 0x00182A14
		public void NativeWrite(string filename)
		{
			this.NativeWrite(filename, null);
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x00182A20 File Offset: 0x00182A20
		public void NativeWrite(string filename, NativeModuleWriterOptions options)
		{
			new NativeModuleWriter(this, options ?? new NativeModuleWriterOptions(this, true)).Write(filename);
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x00182A40 File Offset: 0x00182A40
		public void NativeWrite(Stream dest)
		{
			this.NativeWrite(dest, null);
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x00182A4C File Offset: 0x00182A4C
		public void NativeWrite(Stream dest, NativeModuleWriterOptions options)
		{
			new NativeModuleWriter(this, options ?? new NativeModuleWriterOptions(this, true)).Write(dest);
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x00182A6C File Offset: 0x00182A6C
		public byte[] ReadBlob(uint token)
		{
			uint rid = MDToken.ToRID(token);
			Table table = MDToken.ToTable(token);
			RawFileRow rawFileRow;
			if (table <= Table.TypeSpec)
			{
				switch (table)
				{
				case Table.Field:
				{
					RawFieldRow rawFieldRow;
					if (this.TablesStream.TryReadFieldRow(rid, out rawFieldRow))
					{
						return this.BlobStream.Read(rawFieldRow.Signature);
					}
					break;
				}
				case Table.MethodPtr:
				case Table.ParamPtr:
				case Table.Param:
				case Table.InterfaceImpl:
				case Table.ClassLayout:
				case Table.FieldLayout:
					break;
				case Table.Method:
				{
					RawMethodRow rawMethodRow;
					if (this.TablesStream.TryReadMethodRow(rid, out rawMethodRow))
					{
						return this.BlobStream.Read(rawMethodRow.Signature);
					}
					break;
				}
				case Table.MemberRef:
				{
					RawMemberRefRow rawMemberRefRow;
					if (this.TablesStream.TryReadMemberRefRow(rid, out rawMemberRefRow))
					{
						return this.BlobStream.Read(rawMemberRefRow.Signature);
					}
					break;
				}
				case Table.Constant:
				{
					RawConstantRow rawConstantRow;
					if (this.TablesStream.TryReadConstantRow(rid, out rawConstantRow))
					{
						return this.BlobStream.Read(rawConstantRow.Value);
					}
					break;
				}
				case Table.CustomAttribute:
				{
					RawCustomAttributeRow rawCustomAttributeRow;
					if (this.TablesStream.TryReadCustomAttributeRow(rid, out rawCustomAttributeRow))
					{
						return this.BlobStream.Read(rawCustomAttributeRow.Value);
					}
					break;
				}
				case Table.FieldMarshal:
				{
					RawFieldMarshalRow rawFieldMarshalRow;
					if (this.TablesStream.TryReadFieldMarshalRow(rid, out rawFieldMarshalRow))
					{
						return this.BlobStream.Read(rawFieldMarshalRow.NativeType);
					}
					break;
				}
				case Table.DeclSecurity:
				{
					RawDeclSecurityRow rawDeclSecurityRow;
					if (this.TablesStream.TryReadDeclSecurityRow(rid, out rawDeclSecurityRow))
					{
						return this.BlobStream.Read(rawDeclSecurityRow.PermissionSet);
					}
					break;
				}
				case Table.StandAloneSig:
				{
					RawStandAloneSigRow rawStandAloneSigRow;
					if (this.TablesStream.TryReadStandAloneSigRow(rid, out rawStandAloneSigRow))
					{
						return this.BlobStream.Read(rawStandAloneSigRow.Signature);
					}
					break;
				}
				default:
				{
					RawPropertyRow rawPropertyRow;
					if (table != Table.Property)
					{
						if (table == Table.TypeSpec)
						{
							RawTypeSpecRow rawTypeSpecRow;
							if (this.TablesStream.TryReadTypeSpecRow(rid, out rawTypeSpecRow))
							{
								return this.BlobStream.Read(rawTypeSpecRow.Signature);
							}
						}
					}
					else if (this.TablesStream.TryReadPropertyRow(rid, out rawPropertyRow))
					{
						return this.BlobStream.Read(rawPropertyRow.Type);
					}
					break;
				}
				}
			}
			else if (table <= Table.AssemblyRef)
			{
				RawAssemblyRow rawAssemblyRow;
				if (table != Table.Assembly)
				{
					if (table == Table.AssemblyRef)
					{
						RawAssemblyRefRow rawAssemblyRefRow;
						if (this.TablesStream.TryReadAssemblyRefRow(rid, out rawAssemblyRefRow))
						{
							return this.BlobStream.Read(rawAssemblyRefRow.PublicKeyOrToken);
						}
					}
				}
				else if (this.TablesStream.TryReadAssemblyRow(rid, out rawAssemblyRow))
				{
					return this.BlobStream.Read(rawAssemblyRow.PublicKey);
				}
			}
			else if (table != Table.File)
			{
				if (table == Table.MethodSpec)
				{
					RawMethodSpecRow rawMethodSpecRow;
					if (this.TablesStream.TryReadMethodSpecRow(rid, out rawMethodSpecRow))
					{
						return this.BlobStream.Read(rawMethodSpecRow.Instantiation);
					}
				}
			}
			else if (this.TablesStream.TryReadFileRow(rid, out rawFileRow))
			{
				return this.BlobStream.Read(rawFileRow.HashValue);
			}
			return null;
		}

		// Token: 0x0400260C RID: 9740
		private MetadataBase metadata;

		// Token: 0x0400260D RID: 9741
		private IMethodDecrypter methodDecrypter;

		// Token: 0x0400260E RID: 9742
		private IStringDecrypter stringDecrypter;

		// Token: 0x0400260F RID: 9743
		private StrongBox<RidList> moduleRidList;

		// Token: 0x04002610 RID: 9744
		private SimpleLazyList<ModuleDefMD2> listModuleDefMD;

		// Token: 0x04002611 RID: 9745
		private SimpleLazyList<TypeRefMD> listTypeRefMD;

		// Token: 0x04002612 RID: 9746
		private SimpleLazyList<TypeDefMD> listTypeDefMD;

		// Token: 0x04002613 RID: 9747
		private SimpleLazyList<FieldDefMD> listFieldDefMD;

		// Token: 0x04002614 RID: 9748
		private SimpleLazyList<MethodDefMD> listMethodDefMD;

		// Token: 0x04002615 RID: 9749
		private SimpleLazyList<ParamDefMD> listParamDefMD;

		// Token: 0x04002616 RID: 9750
		private SimpleLazyList2<InterfaceImplMD> listInterfaceImplMD;

		// Token: 0x04002617 RID: 9751
		private SimpleLazyList2<MemberRefMD> listMemberRefMD;

		// Token: 0x04002618 RID: 9752
		private SimpleLazyList<ConstantMD> listConstantMD;

		// Token: 0x04002619 RID: 9753
		private SimpleLazyList<DeclSecurityMD> listDeclSecurityMD;

		// Token: 0x0400261A RID: 9754
		private SimpleLazyList<ClassLayoutMD> listClassLayoutMD;

		// Token: 0x0400261B RID: 9755
		private SimpleLazyList2<StandAloneSigMD> listStandAloneSigMD;

		// Token: 0x0400261C RID: 9756
		private SimpleLazyList<EventDefMD> listEventDefMD;

		// Token: 0x0400261D RID: 9757
		private SimpleLazyList<PropertyDefMD> listPropertyDefMD;

		// Token: 0x0400261E RID: 9758
		private SimpleLazyList<ModuleRefMD> listModuleRefMD;

		// Token: 0x0400261F RID: 9759
		private SimpleLazyList2<TypeSpecMD> listTypeSpecMD;

		// Token: 0x04002620 RID: 9760
		private SimpleLazyList<ImplMapMD> listImplMapMD;

		// Token: 0x04002621 RID: 9761
		private SimpleLazyList<AssemblyDefMD> listAssemblyDefMD;

		// Token: 0x04002622 RID: 9762
		private SimpleLazyList<AssemblyRefMD> listAssemblyRefMD;

		// Token: 0x04002623 RID: 9763
		private SimpleLazyList<FileDefMD> listFileDefMD;

		// Token: 0x04002624 RID: 9764
		private SimpleLazyList<ExportedTypeMD> listExportedTypeMD;

		// Token: 0x04002625 RID: 9765
		private SimpleLazyList<ManifestResourceMD> listManifestResourceMD;

		// Token: 0x04002626 RID: 9766
		private SimpleLazyList<GenericParamMD> listGenericParamMD;

		// Token: 0x04002627 RID: 9767
		private SimpleLazyList2<MethodSpecMD> listMethodSpecMD;

		// Token: 0x04002628 RID: 9768
		private SimpleLazyList2<GenericParamConstraintMD> listGenericParamConstraintMD;

		// Token: 0x04002629 RID: 9769
		private static readonly Dictionary<string, int> preferredCorLibs = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
				100
			},
			{
				"mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
				90
			},
			{
				"mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
				60
			},
			{
				"mscorlib, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
				50
			},
			{
				"mscorlib, Version=5.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
				80
			},
			{
				"mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
				70
			},
			{
				"mscorlib, Version=3.5.0.0, Culture=neutral, PublicKeyToken=e92a8b81eba7ceb7",
				60
			},
			{
				"mscorlib, Version=3.5.0.0, Culture=neutral, PublicKeyToken=969db8053d3322ac",
				60
			},
			{
				"mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=969db8053d3322ac",
				50
			}
		};

		// Token: 0x0400262A RID: 9770
		private static readonly string[] corlibs = new string[]
		{
			"System.Private.CoreLib",
			"System.Runtime",
			"netstandard",
			"mscorlib"
		};

		// Token: 0x0400262B RID: 9771
		private static readonly UTF8String systemRuntimeName = new UTF8String("System.Runtime");

		// Token: 0x0400262C RID: 9772
		private static readonly UTF8String corefxName = new UTF8String("corefx");

		// Token: 0x0400262D RID: 9773
		private static readonly PublicKeyToken contractsPublicKeyToken = new PublicKeyToken("b03f5f7f11d50a3a");

		// Token: 0x0400262E RID: 9774
		private MethodExportInfoProvider methodExportInfoProvider;
	}
}
