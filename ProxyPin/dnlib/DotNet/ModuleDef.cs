using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.DotNet.Writer;
using dnlib.PE;
using dnlib.Threading;
using dnlib.Utils;
using dnlib.W32Resources;

namespace dnlib.DotNet
{
	// Token: 0x0200081C RID: 2076
	[ComVisible(true)]
	public abstract class ModuleDef : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasCustomDebugInformation, IResolutionScope, IFullName, IDisposable, IListListener<TypeDef>, IModule, IScope, ITypeDefFinder, IDnlibDef, ITokenResolver, ISignatureReaderHelper
	{
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06004BC0 RID: 19392 RVA: 0x0017EA2C File Offset: 0x0017EA2C
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.Module, this.rid);
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x0017EA3C File Offset: 0x0017EA3C
		// (set) Token: 0x06004BC2 RID: 19394 RVA: 0x0017EA44 File Offset: 0x0017EA44
		public uint Rid
		{
			get
			{
				return this.rid;
			}
			set
			{
				this.rid = value;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06004BC3 RID: 19395 RVA: 0x0017EA50 File Offset: 0x0017EA50
		public int HasCustomAttributeTag
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06004BC4 RID: 19396 RVA: 0x0017EA54 File Offset: 0x0017EA54
		public int ResolutionScopeTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06004BC5 RID: 19397 RVA: 0x0017EA58 File Offset: 0x0017EA58
		// (set) Token: 0x06004BC6 RID: 19398 RVA: 0x0017EA60 File Offset: 0x0017EA60
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06004BC7 RID: 19399 RVA: 0x0017EA6C File Offset: 0x0017EA6C
		public ScopeType ScopeType
		{
			get
			{
				return ScopeType.ModuleDef;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x0017EA70 File Offset: 0x0017EA70
		public string ScopeName
		{
			get
			{
				return this.FullName;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06004BC9 RID: 19401 RVA: 0x0017EA78 File Offset: 0x0017EA78
		// (set) Token: 0x06004BCA RID: 19402 RVA: 0x0017EA80 File Offset: 0x0017EA80
		public ushort Generation
		{
			get
			{
				return this.generation;
			}
			set
			{
				this.generation = value;
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06004BCB RID: 19403 RVA: 0x0017EA8C File Offset: 0x0017EA8C
		// (set) Token: 0x06004BCC RID: 19404 RVA: 0x0017EA94 File Offset: 0x0017EA94
		public UTF8String Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x0017EAA0 File Offset: 0x0017EAA0
		// (set) Token: 0x06004BCE RID: 19406 RVA: 0x0017EAA8 File Offset: 0x0017EAA8
		public Guid? Mvid
		{
			get
			{
				return this.mvid;
			}
			set
			{
				this.mvid = value;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x0017EAB4 File Offset: 0x0017EAB4
		// (set) Token: 0x06004BD0 RID: 19408 RVA: 0x0017EABC File Offset: 0x0017EABC
		public Guid? EncId
		{
			get
			{
				return this.encId;
			}
			set
			{
				this.encId = value;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x0017EAC8 File Offset: 0x0017EAC8
		// (set) Token: 0x06004BD2 RID: 19410 RVA: 0x0017EAD0 File Offset: 0x0017EAD0
		public Guid? EncBaseId
		{
			get
			{
				return this.encBaseId;
			}
			set
			{
				this.encBaseId = value;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06004BD3 RID: 19411 RVA: 0x0017EADC File Offset: 0x0017EADC
		public CustomAttributeCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.InitializeCustomAttributes();
				}
				return this.customAttributes;
			}
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x0017EAF8 File Offset: 0x0017EAF8
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06004BD5 RID: 19413 RVA: 0x0017EB0C File Offset: 0x0017EB0C
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06004BD6 RID: 19414 RVA: 0x0017EB10 File Offset: 0x0017EB10
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06004BD7 RID: 19415 RVA: 0x0017EB20 File Offset: 0x0017EB20
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				if (this.customDebugInfos == null)
				{
					this.InitializeCustomDebugInfos();
				}
				return this.customDebugInfos;
			}
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0017EB3C File Offset: 0x0017EB3C
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06004BD9 RID: 19417 RVA: 0x0017EB50 File Offset: 0x0017EB50
		// (set) Token: 0x06004BDA RID: 19418 RVA: 0x0017EB58 File Offset: 0x0017EB58
		public AssemblyDef Assembly
		{
			get
			{
				return this.assembly;
			}
			internal set
			{
				this.assembly = value;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06004BDB RID: 19419 RVA: 0x0017EB64 File Offset: 0x0017EB64
		public IList<TypeDef> Types
		{
			get
			{
				if (this.types == null)
				{
					this.InitializeTypes();
				}
				return this.types;
			}
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x0017EB80 File Offset: 0x0017EB80
		protected virtual void InitializeTypes()
		{
			Interlocked.CompareExchange<LazyList<TypeDef>>(ref this.types, new LazyList<TypeDef>(this), null);
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x0017EB98 File Offset: 0x0017EB98
		public IList<ExportedType> ExportedTypes
		{
			get
			{
				if (this.exportedTypes == null)
				{
					this.InitializeExportedTypes();
				}
				return this.exportedTypes;
			}
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0017EBB4 File Offset: 0x0017EBB4
		protected virtual void InitializeExportedTypes()
		{
			Interlocked.CompareExchange<IList<ExportedType>>(ref this.exportedTypes, new List<ExportedType>(), null);
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06004BDF RID: 19423 RVA: 0x0017EBC8 File Offset: 0x0017EBC8
		// (set) Token: 0x06004BE0 RID: 19424 RVA: 0x0017EBE4 File Offset: 0x0017EBE4
		public RVA NativeEntryPoint
		{
			get
			{
				if (!this.nativeAndManagedEntryPoint_initialized)
				{
					this.InitializeNativeAndManagedEntryPoint();
				}
				return this.nativeEntryPoint;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.nativeEntryPoint = value;
					this.managedEntryPoint = null;
					this.Cor20HeaderFlags |= ComImageFlags.NativeEntryPoint;
					this.nativeAndManagedEntryPoint_initialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06004BE1 RID: 19425 RVA: 0x0017EC44 File Offset: 0x0017EC44
		// (set) Token: 0x06004BE2 RID: 19426 RVA: 0x0017EC60 File Offset: 0x0017EC60
		public IManagedEntryPoint ManagedEntryPoint
		{
			get
			{
				if (!this.nativeAndManagedEntryPoint_initialized)
				{
					this.InitializeNativeAndManagedEntryPoint();
				}
				return this.managedEntryPoint;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.nativeEntryPoint = (RVA)0U;
					this.managedEntryPoint = value;
					this.Cor20HeaderFlags &= ~ComImageFlags.NativeEntryPoint;
					this.nativeAndManagedEntryPoint_initialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0017ECC0 File Offset: 0x0017ECC0
		private void InitializeNativeAndManagedEntryPoint()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.nativeAndManagedEntryPoint_initialized)
				{
					this.nativeEntryPoint = this.GetNativeEntryPoint_NoLock();
					this.managedEntryPoint = this.GetManagedEntryPoint_NoLock();
					this.nativeAndManagedEntryPoint_initialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x0017ED28 File Offset: 0x0017ED28
		protected virtual RVA GetNativeEntryPoint_NoLock()
		{
			return (RVA)0U;
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x0017ED2C File Offset: 0x0017ED2C
		protected virtual IManagedEntryPoint GetManagedEntryPoint_NoLock()
		{
			return null;
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x0017ED30 File Offset: 0x0017ED30
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x0017ED40 File Offset: 0x0017ED40
		// (set) Token: 0x06004BE8 RID: 19432 RVA: 0x0017ED50 File Offset: 0x0017ED50
		public MethodDef EntryPoint
		{
			get
			{
				return this.ManagedEntryPoint as MethodDef;
			}
			set
			{
				this.ManagedEntryPoint = value;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06004BE9 RID: 19433 RVA: 0x0017ED5C File Offset: 0x0017ED5C
		public bool IsNativeEntryPointValid
		{
			get
			{
				return this.NativeEntryPoint > (RVA)0U;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06004BEA RID: 19434 RVA: 0x0017ED68 File Offset: 0x0017ED68
		public bool IsManagedEntryPointValid
		{
			get
			{
				return this.ManagedEntryPoint != null;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06004BEB RID: 19435 RVA: 0x0017ED78 File Offset: 0x0017ED78
		public bool IsEntryPointValid
		{
			get
			{
				return this.EntryPoint != null;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06004BEC RID: 19436 RVA: 0x0017ED88 File Offset: 0x0017ED88
		public ResourceCollection Resources
		{
			get
			{
				if (this.resources == null)
				{
					this.InitializeResources();
				}
				return this.resources;
			}
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0017EDA4 File Offset: 0x0017EDA4
		protected virtual void InitializeResources()
		{
			Interlocked.CompareExchange<ResourceCollection>(ref this.resources, new ResourceCollection(), null);
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06004BEE RID: 19438 RVA: 0x0017EDB8 File Offset: 0x0017EDB8
		// (set) Token: 0x06004BEF RID: 19439 RVA: 0x0017EDD4 File Offset: 0x0017EDD4
		public VTableFixups VTableFixups
		{
			get
			{
				if (!this.vtableFixups_isInitialized)
				{
					this.InitializeVTableFixups();
				}
				return this.vtableFixups;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.vtableFixups = value;
					this.vtableFixups_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x0017EE1C File Offset: 0x0017EE1C
		private void InitializeVTableFixups()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.vtableFixups_isInitialized)
				{
					this.vtableFixups = this.GetVTableFixups_NoLock();
					this.vtableFixups_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x0017EE78 File Offset: 0x0017EE78
		protected virtual VTableFixups GetVTableFixups_NoLock()
		{
			return null;
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x0017EE7C File Offset: 0x0017EE7C
		public bool HasTypes
		{
			get
			{
				return this.Types.Count > 0;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06004BF3 RID: 19443 RVA: 0x0017EE8C File Offset: 0x0017EE8C
		public bool HasExportedTypes
		{
			get
			{
				return this.ExportedTypes.Count > 0;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x0017EE9C File Offset: 0x0017EE9C
		public bool HasResources
		{
			get
			{
				return this.Resources.Count > 0;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x0017EEAC File Offset: 0x0017EEAC
		public string FullName
		{
			get
			{
				return UTF8String.ToSystemStringOrEmpty(this.name);
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x0017EEBC File Offset: 0x0017EEBC
		// (set) Token: 0x06004BF7 RID: 19447 RVA: 0x0017EEC4 File Offset: 0x0017EEC4
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x0017EED0 File Offset: 0x0017EED0
		public ICorLibTypes CorLibTypes
		{
			get
			{
				return this.corLibTypes;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06004BF9 RID: 19449 RVA: 0x0017EED8 File Offset: 0x0017EED8
		private TypeDefFinder TypeDefFinder
		{
			get
			{
				if (this.typeDefFinder == null)
				{
					Interlocked.CompareExchange<TypeDefFinder>(ref this.typeDefFinder, new TypeDefFinder(this.Types), null);
				}
				return this.typeDefFinder;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06004BFA RID: 19450 RVA: 0x0017EF04 File Offset: 0x0017EF04
		// (set) Token: 0x06004BFB RID: 19451 RVA: 0x0017EF2C File Offset: 0x0017EF2C
		public ModuleContext Context
		{
			get
			{
				if (this.context == null)
				{
					Interlocked.CompareExchange<ModuleContext>(ref this.context, new ModuleContext(), null);
				}
				return this.context;
			}
			set
			{
				this.context = (value ?? new ModuleContext());
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06004BFC RID: 19452 RVA: 0x0017EF44 File Offset: 0x0017EF44
		// (set) Token: 0x06004BFD RID: 19453 RVA: 0x0017EF54 File Offset: 0x0017EF54
		public bool EnableTypeDefFindCache
		{
			get
			{
				return this.TypeDefFinder.IsCacheEnabled;
			}
			set
			{
				this.TypeDefFinder.IsCacheEnabled = value;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06004BFE RID: 19454 RVA: 0x0017EF64 File Offset: 0x0017EF64
		public bool IsManifestModule
		{
			get
			{
				AssemblyDef assemblyDef = this.assembly;
				return assemblyDef != null && assemblyDef.ManifestModule == this;
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06004BFF RID: 19455 RVA: 0x0017EF90 File Offset: 0x0017EF90
		public TypeDef GlobalType
		{
			get
			{
				if (this.Types.Count != 0)
				{
					return this.Types[0];
				}
				return null;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06004C00 RID: 19456 RVA: 0x0017EFB0 File Offset: 0x0017EFB0
		// (set) Token: 0x06004C01 RID: 19457 RVA: 0x0017EFB8 File Offset: 0x0017EFB8
		public bool? IsCoreLibraryModule { get; set; }

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06004C02 RID: 19458 RVA: 0x0017EFC4 File Offset: 0x0017EFC4
		// (set) Token: 0x06004C03 RID: 19459 RVA: 0x0017EFE0 File Offset: 0x0017EFE0
		public Win32Resources Win32Resources
		{
			get
			{
				if (!this.win32Resources_isInitialized)
				{
					this.InitializeWin32Resources();
				}
				return this.win32Resources;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.win32Resources = value;
					this.win32Resources_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x0017F028 File Offset: 0x0017F028
		private void InitializeWin32Resources()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.win32Resources_isInitialized)
				{
					this.win32Resources = this.GetWin32Resources_NoLock();
					this.win32Resources_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x0017F084 File Offset: 0x0017F084
		protected virtual Win32Resources GetWin32Resources_NoLock()
		{
			return null;
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06004C06 RID: 19462 RVA: 0x0017F088 File Offset: 0x0017F088
		public PdbState PdbState
		{
			get
			{
				return this.pdbState;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06004C07 RID: 19463 RVA: 0x0017F090 File Offset: 0x0017F090
		// (set) Token: 0x06004C08 RID: 19464 RVA: 0x0017F098 File Offset: 0x0017F098
		public ModuleKind Kind { get; set; }

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06004C09 RID: 19465 RVA: 0x0017F0A4 File Offset: 0x0017F0A4
		// (set) Token: 0x06004C0A RID: 19466 RVA: 0x0017F0AC File Offset: 0x0017F0AC
		public Characteristics Characteristics { get; set; }

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06004C0B RID: 19467 RVA: 0x0017F0B8 File Offset: 0x0017F0B8
		// (set) Token: 0x06004C0C RID: 19468 RVA: 0x0017F0C0 File Offset: 0x0017F0C0
		public DllCharacteristics DllCharacteristics { get; set; }

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06004C0D RID: 19469 RVA: 0x0017F0CC File Offset: 0x0017F0CC
		// (set) Token: 0x06004C0E RID: 19470 RVA: 0x0017F0D4 File Offset: 0x0017F0D4
		public string RuntimeVersion
		{
			get
			{
				return this.runtimeVersion;
			}
			set
			{
				if (this.runtimeVersion != value)
				{
					this.runtimeVersion = value;
					this.cachedWinMDStatus = null;
					this.runtimeVersionWinMD = null;
					this.winMDVersion = null;
				}
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x0017F108 File Offset: 0x0017F108
		public WinMDStatus WinMDStatus
		{
			get
			{
				WinMDStatus? winMDStatus = this.cachedWinMDStatus;
				if (winMDStatus != null)
				{
					return winMDStatus.Value;
				}
				winMDStatus = new WinMDStatus?(ModuleDef.CalculateWinMDStatus(this.RuntimeVersion));
				this.cachedWinMDStatus = winMDStatus;
				return winMDStatus.Value;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06004C10 RID: 19472 RVA: 0x0017F154 File Offset: 0x0017F154
		public bool IsWinMD
		{
			get
			{
				return this.WinMDStatus > WinMDStatus.None;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06004C11 RID: 19473 RVA: 0x0017F160 File Offset: 0x0017F160
		public bool IsManagedWinMD
		{
			get
			{
				return this.WinMDStatus == WinMDStatus.Managed;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06004C12 RID: 19474 RVA: 0x0017F16C File Offset: 0x0017F16C
		public bool IsPureWinMD
		{
			get
			{
				return this.WinMDStatus == WinMDStatus.Pure;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06004C13 RID: 19475 RVA: 0x0017F178 File Offset: 0x0017F178
		public string RuntimeVersionWinMD
		{
			get
			{
				string text = this.runtimeVersionWinMD;
				if (text != null)
				{
					return text;
				}
				return this.runtimeVersionWinMD = ModuleDef.CalculateRuntimeVersionWinMD(this.RuntimeVersion);
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x0017F1AC File Offset: 0x0017F1AC
		public string WinMDVersion
		{
			get
			{
				string text = this.winMDVersion;
				if (text != null)
				{
					return text;
				}
				return this.winMDVersion = ModuleDef.CalculateWinMDVersion(this.RuntimeVersion);
			}
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x0017F1E0 File Offset: 0x0017F1E0
		private static WinMDStatus CalculateWinMDStatus(string version)
		{
			if (version == null)
			{
				return WinMDStatus.None;
			}
			if (!version.StartsWith("WindowsRuntime ", StringComparison.Ordinal))
			{
				return WinMDStatus.None;
			}
			if (version.IndexOf(';') >= 0)
			{
				return WinMDStatus.Managed;
			}
			return WinMDStatus.Pure;
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x0017F210 File Offset: 0x0017F210
		private static string CalculateRuntimeVersionWinMD(string version)
		{
			if (version == null)
			{
				return null;
			}
			if (!version.StartsWith("WindowsRuntime ", StringComparison.Ordinal))
			{
				return null;
			}
			int num = version.IndexOf(';');
			if (num < 0)
			{
				return null;
			}
			string text = version.Substring(num + 1);
			if (text.StartsWith("CLR", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Substring(3);
			}
			return text.TrimStart(new char[]
			{
				' '
			});
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x0017F284 File Offset: 0x0017F284
		private static string CalculateWinMDVersion(string version)
		{
			if (version == null)
			{
				return null;
			}
			if (!version.StartsWith("WindowsRuntime ", StringComparison.Ordinal))
			{
				return null;
			}
			int num = version.IndexOf(';');
			if (num < 0)
			{
				return version;
			}
			return version.Substring(0, num);
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x0017F2CC File Offset: 0x0017F2CC
		public bool IsClr10
		{
			get
			{
				string text = this.RuntimeVersion ?? string.Empty;
				return text.StartsWith("v1.0") || text.StartsWith("v1.x86") || text == "retail" || text == "COMPLUS";
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06004C19 RID: 19481 RVA: 0x0017F330 File Offset: 0x0017F330
		public bool IsClr10Exactly
		{
			get
			{
				return this.RuntimeVersion == "v1.0.3705" || this.RuntimeVersion == "v1.x86ret" || this.RuntimeVersion == "retail" || this.RuntimeVersion == "COMPLUS";
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x0017F394 File Offset: 0x0017F394
		public bool IsClr11
		{
			get
			{
				return (this.RuntimeVersion ?? string.Empty).StartsWith("v1.1");
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x0017F3B4 File Offset: 0x0017F3B4
		public bool IsClr11Exactly
		{
			get
			{
				return this.RuntimeVersion == "v1.1.4322";
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x0017F3C8 File Offset: 0x0017F3C8
		public bool IsClr1x
		{
			get
			{
				return this.IsClr10 || this.IsClr11;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06004C1D RID: 19485 RVA: 0x0017F3E0 File Offset: 0x0017F3E0
		public bool IsClr1xExactly
		{
			get
			{
				return this.IsClr10Exactly || this.IsClr11Exactly;
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06004C1E RID: 19486 RVA: 0x0017F3F8 File Offset: 0x0017F3F8
		public bool IsClr20
		{
			get
			{
				return (this.RuntimeVersion ?? string.Empty).StartsWith("v2.0");
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06004C1F RID: 19487 RVA: 0x0017F418 File Offset: 0x0017F418
		public bool IsClr20Exactly
		{
			get
			{
				return this.RuntimeVersion == "v2.0.50727";
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06004C20 RID: 19488 RVA: 0x0017F42C File Offset: 0x0017F42C
		public bool IsClr40
		{
			get
			{
				return (this.RuntimeVersion ?? string.Empty).StartsWith("v4.0");
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06004C21 RID: 19489 RVA: 0x0017F44C File Offset: 0x0017F44C
		public bool IsClr40Exactly
		{
			get
			{
				return this.RuntimeVersion == "v4.0.30319";
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06004C22 RID: 19490 RVA: 0x0017F460 File Offset: 0x0017F460
		public bool IsEcma2002
		{
			get
			{
				return this.RuntimeVersion == "Standard CLI 2002";
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06004C23 RID: 19491 RVA: 0x0017F474 File Offset: 0x0017F474
		public bool IsEcma2005
		{
			get
			{
				return this.RuntimeVersion == "Standard CLI 2005";
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06004C24 RID: 19492 RVA: 0x0017F488 File Offset: 0x0017F488
		// (set) Token: 0x06004C25 RID: 19493 RVA: 0x0017F490 File Offset: 0x0017F490
		public Machine Machine { get; set; }

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06004C26 RID: 19494 RVA: 0x0017F49C File Offset: 0x0017F49C
		public bool IsI386
		{
			get
			{
				return this.Machine.IsI386();
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06004C27 RID: 19495 RVA: 0x0017F4AC File Offset: 0x0017F4AC
		public bool IsIA64
		{
			get
			{
				return this.Machine == Machine.IA64;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06004C28 RID: 19496 RVA: 0x0017F4BC File Offset: 0x0017F4BC
		public bool IsAMD64
		{
			get
			{
				return this.Machine.IsAMD64();
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06004C29 RID: 19497 RVA: 0x0017F4CC File Offset: 0x0017F4CC
		public bool IsARM
		{
			get
			{
				return this.Machine.IsARMNT();
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06004C2A RID: 19498 RVA: 0x0017F4DC File Offset: 0x0017F4DC
		public bool IsARM64
		{
			get
			{
				return this.Machine.IsARM64();
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x0017F4EC File Offset: 0x0017F4EC
		// (set) Token: 0x06004C2C RID: 19500 RVA: 0x0017F4F4 File Offset: 0x0017F4F4
		public ComImageFlags Cor20HeaderFlags
		{
			get
			{
				return (ComImageFlags)this.cor20HeaderFlags;
			}
			set
			{
				this.cor20HeaderFlags = (int)value;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x0017F500 File Offset: 0x0017F500
		// (set) Token: 0x06004C2E RID: 19502 RVA: 0x0017F508 File Offset: 0x0017F508
		public uint? Cor20HeaderRuntimeVersion { get; set; }

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x0017F514 File Offset: 0x0017F514
		// (set) Token: 0x06004C30 RID: 19504 RVA: 0x0017F51C File Offset: 0x0017F51C
		public ushort? TablesHeaderVersion { get; set; }

		// Token: 0x06004C31 RID: 19505 RVA: 0x0017F528 File Offset: 0x0017F528
		private void ModifyComImageFlags(bool set, ComImageFlags flags)
		{
			int num;
			int value;
			do
			{
				num = this.cor20HeaderFlags;
				if (set)
				{
					value = (num | (int)flags);
				}
				else
				{
					value = (num & (int)(~(int)flags));
				}
			}
			while (Interlocked.CompareExchange(ref this.cor20HeaderFlags, value, num) != num);
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06004C32 RID: 19506 RVA: 0x0017F564 File Offset: 0x0017F564
		// (set) Token: 0x06004C33 RID: 19507 RVA: 0x0017F574 File Offset: 0x0017F574
		public bool IsILOnly
		{
			get
			{
				return (this.cor20HeaderFlags & 1) != 0;
			}
			set
			{
				this.ModifyComImageFlags(value, ComImageFlags.ILOnly);
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x0017F580 File Offset: 0x0017F580
		// (set) Token: 0x06004C35 RID: 19509 RVA: 0x0017F590 File Offset: 0x0017F590
		public bool Is32BitRequired
		{
			get
			{
				return (this.cor20HeaderFlags & 2) != 0;
			}
			set
			{
				this.ModifyComImageFlags(value, ComImageFlags.Bit32Required);
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06004C36 RID: 19510 RVA: 0x0017F59C File Offset: 0x0017F59C
		// (set) Token: 0x06004C37 RID: 19511 RVA: 0x0017F5AC File Offset: 0x0017F5AC
		public bool IsStrongNameSigned
		{
			get
			{
				return (this.cor20HeaderFlags & 8) != 0;
			}
			set
			{
				this.ModifyComImageFlags(value, ComImageFlags.StrongNameSigned);
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x0017F5B8 File Offset: 0x0017F5B8
		// (set) Token: 0x06004C39 RID: 19513 RVA: 0x0017F5C8 File Offset: 0x0017F5C8
		public bool HasNativeEntryPoint
		{
			get
			{
				return (this.cor20HeaderFlags & 16) != 0;
			}
			set
			{
				this.ModifyComImageFlags(value, ComImageFlags.NativeEntryPoint);
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x0017F5D4 File Offset: 0x0017F5D4
		// (set) Token: 0x06004C3B RID: 19515 RVA: 0x0017F5E8 File Offset: 0x0017F5E8
		public bool Is32BitPreferred
		{
			get
			{
				return (this.cor20HeaderFlags & 131072) != 0;
			}
			set
			{
				this.ModifyComImageFlags(value, ComImageFlags.Bit32Preferred);
			}
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0017F5F8 File Offset: 0x0017F5F8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x0017F608 File Offset: 0x0017F608
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			TypeDefFinder typeDefFinder = this.typeDefFinder;
			if (typeDefFinder != null)
			{
				typeDefFinder.Dispose();
				this.typeDefFinder = null;
			}
			PdbState pdbState = this.pdbState;
			if (pdbState != null)
			{
				pdbState.Dispose();
			}
			this.pdbState = null;
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x0017F658 File Offset: 0x0017F658
		public IEnumerable<TypeDef> GetTypes()
		{
			return AllTypesHelper.Types(this.Types);
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x0017F668 File Offset: 0x0017F668
		public void AddAsNonNestedType(TypeDef typeDef)
		{
			if (typeDef == null)
			{
				return;
			}
			typeDef.DeclaringType = null;
			this.Types.Add(typeDef);
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0017F684 File Offset: 0x0017F684
		public T UpdateRowId<T>(T tableRow) where T : IMDTokenProvider
		{
			if (tableRow != null && tableRow.Rid == 0U)
			{
				tableRow.Rid = this.GetNextFreeRid(tableRow.MDToken.Table);
			}
			return tableRow;
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0017F6DC File Offset: 0x0017F6DC
		public T ForceUpdateRowId<T>(T tableRow) where T : IMDTokenProvider
		{
			if (tableRow != null)
			{
				tableRow.Rid = this.GetNextFreeRid(tableRow.MDToken.Table);
			}
			return tableRow;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0017F724 File Offset: 0x0017F724
		private uint GetNextFreeRid(Table table)
		{
			int[] array = this.lastUsedRids;
			if ((ulong)table >= (ulong)((long)array.Length))
			{
				return 0U;
			}
			return (uint)(Interlocked.Increment(ref array[(int)table]) & 16777215);
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0017F75C File Offset: 0x0017F75C
		public ITypeDefOrRef Import(Type type)
		{
			return new Importer(this).Import(type);
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x0017F77C File Offset: 0x0017F77C
		public TypeSig ImportAsTypeSig(Type type)
		{
			return new Importer(this).ImportAsTypeSig(type);
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x0017F79C File Offset: 0x0017F79C
		public MemberRef Import(FieldInfo fieldInfo)
		{
			return (MemberRef)new Importer(this).Import(fieldInfo);
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0017F7C4 File Offset: 0x0017F7C4
		public IMethod Import(MethodBase methodBase)
		{
			return new Importer(this).Import(methodBase);
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0017F7E4 File Offset: 0x0017F7E4
		public IType Import(IType type)
		{
			return new Importer(this).Import(type);
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0017F804 File Offset: 0x0017F804
		public TypeRef Import(TypeDef type)
		{
			return (TypeRef)new Importer(this).Import(type);
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0017F82C File Offset: 0x0017F82C
		public TypeRef Import(TypeRef type)
		{
			return (TypeRef)new Importer(this).Import(type);
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0017F854 File Offset: 0x0017F854
		public TypeSpec Import(TypeSpec type)
		{
			return new Importer(this).Import(type);
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0017F874 File Offset: 0x0017F874
		public TypeSig Import(TypeSig type)
		{
			return new Importer(this).Import(type);
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0017F894 File Offset: 0x0017F894
		public MemberRef Import(IField field)
		{
			return (MemberRef)new Importer(this).Import(field);
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0017F8BC File Offset: 0x0017F8BC
		public MemberRef Import(FieldDef field)
		{
			return (MemberRef)new Importer(this).Import(field);
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0017F8E4 File Offset: 0x0017F8E4
		public IMethod Import(IMethod method)
		{
			return new Importer(this).Import(method);
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0017F904 File Offset: 0x0017F904
		public MemberRef Import(MethodDef method)
		{
			return (MemberRef)new Importer(this).Import(method);
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0017F92C File Offset: 0x0017F92C
		public MethodSpec Import(MethodSpec method)
		{
			return new Importer(this).Import(method);
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x0017F94C File Offset: 0x0017F94C
		public MemberRef Import(MemberRef memberRef)
		{
			return new Importer(this).Import(memberRef);
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x0017F96C File Offset: 0x0017F96C
		public void Write(string filename)
		{
			this.Write(filename, null);
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x0017F978 File Offset: 0x0017F978
		public void Write(string filename, ModuleWriterOptions options)
		{
			new ModuleWriter(this, options ?? new ModuleWriterOptions(this)).Write(filename);
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x0017F994 File Offset: 0x0017F994
		public void Write(Stream dest)
		{
			this.Write(dest, null);
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x0017F9A0 File Offset: 0x0017F9A0
		public void Write(Stream dest, ModuleWriterOptions options)
		{
			new ModuleWriter(this, options ?? new ModuleWriterOptions(this)).Write(dest);
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x0017F9BC File Offset: 0x0017F9BC
		public void ResetTypeDefFindCache()
		{
			this.TypeDefFinder.ResetCache();
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0017F9CC File Offset: 0x0017F9CC
		public ResourceData FindWin32ResourceData(ResourceName type, ResourceName name, ResourceName langId)
		{
			Win32Resources win32Resources = this.Win32Resources;
			if (win32Resources == null)
			{
				return null;
			}
			return win32Resources.Find(type, name, langId);
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x0017F9E8 File Offset: 0x0017F9E8
		public void CreatePdbState(PdbFileKind pdbFileKind)
		{
			this.SetPdbState(new PdbState(this, pdbFileKind));
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x0017F9F8 File Offset: 0x0017F9F8
		public void SetPdbState(PdbState pdbState)
		{
			if (pdbState == null)
			{
				throw new ArgumentNullException("pdbState");
			}
			if (Interlocked.CompareExchange<PdbState>(ref this.pdbState, pdbState, null) != null)
			{
				throw new InvalidOperationException("PDB file has already been initialized");
			}
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x0017FA28 File Offset: 0x0017FA28
		private uint GetCor20RuntimeVersion()
		{
			uint? cor20HeaderRuntimeVersion = this.Cor20HeaderRuntimeVersion;
			if (cor20HeaderRuntimeVersion != null)
			{
				return cor20HeaderRuntimeVersion.Value;
			}
			if (!this.IsClr1x)
			{
				return 131077U;
			}
			return 131072U;
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x0017FA6C File Offset: 0x0017FA6C
		public int GetPointerSize()
		{
			return this.GetPointerSize(4);
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x0017FA78 File Offset: 0x0017FA78
		public int GetPointerSize(int defaultPointerSize)
		{
			return this.GetPointerSize(defaultPointerSize, defaultPointerSize);
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x0017FA84 File Offset: 0x0017FA84
		public int GetPointerSize(int defaultPointerSize, int prefer32bitPointerSize)
		{
			Machine machine = this.Machine;
			if (machine.Is64Bit())
			{
				return 8;
			}
			if (!machine.IsI386())
			{
				return 4;
			}
			if (this.GetCor20RuntimeVersion() < 131077U)
			{
				return 4;
			}
			ComImageFlags comImageFlags = (ComImageFlags)this.cor20HeaderFlags;
			if ((comImageFlags & ComImageFlags.ILOnly) == (ComImageFlags)0U)
			{
				return 4;
			}
			ComImageFlags comImageFlags2 = comImageFlags & (ComImageFlags.Bit32Required | ComImageFlags.Bit32Preferred);
			if (comImageFlags2 <= ComImageFlags.Bit32Required)
			{
				if (comImageFlags2 != (ComImageFlags)0U)
				{
					if (comImageFlags2 == ComImageFlags.Bit32Required)
					{
						return 4;
					}
				}
			}
			else if (comImageFlags2 != ComImageFlags.Bit32Preferred)
			{
				if (comImageFlags2 == (ComImageFlags.Bit32Required | ComImageFlags.Bit32Preferred))
				{
					return prefer32bitPointerSize;
				}
			}
			return defaultPointerSize;
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0017FB1C File Offset: 0x0017FB1C
		void IListListener<TypeDef>.OnLazyAdd(int index, ref TypeDef value)
		{
			value.Module2 = this;
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x0017FB28 File Offset: 0x0017FB28
		void IListListener<TypeDef>.OnAdd(int index, TypeDef value)
		{
			if (value.DeclaringType != null)
			{
				throw new InvalidOperationException("Nested type is already owned by another type. Set DeclaringType to null first.");
			}
			if (value.Module != null)
			{
				throw new InvalidOperationException("Type is already owned by another module. Remove it from that module's type list.");
			}
			value.Module2 = this;
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x0017FB60 File Offset: 0x0017FB60
		void IListListener<TypeDef>.OnRemove(int index, TypeDef value)
		{
			value.Module2 = null;
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0017FB6C File Offset: 0x0017FB6C
		void IListListener<TypeDef>.OnResize(int index)
		{
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0017FB70 File Offset: 0x0017FB70
		void IListListener<TypeDef>.OnClear()
		{
			foreach (TypeDef typeDef in this.types.GetEnumerable_NoLock())
			{
				typeDef.Module2 = null;
			}
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x0017FBCC File Offset: 0x0017FBCC
		public TypeDef Find(string fullName, bool isReflectionName)
		{
			return this.TypeDefFinder.Find(fullName, isReflectionName);
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x0017FBDC File Offset: 0x0017FBDC
		public TypeDef Find(TypeRef typeRef)
		{
			return this.TypeDefFinder.Find(typeRef);
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x0017FBEC File Offset: 0x0017FBEC
		public TypeDef Find(ITypeDefOrRef typeRef)
		{
			TypeDef typeDef = typeRef as TypeDef;
			if (typeDef != null)
			{
				if (typeDef.Module != this)
				{
					return null;
				}
				return typeDef;
			}
			else
			{
				TypeRef typeRef2 = typeRef as TypeRef;
				if (typeRef2 != null)
				{
					return this.Find(typeRef2);
				}
				TypeSpec typeSpec = typeRef as TypeSpec;
				if (typeSpec == null)
				{
					return null;
				}
				TypeDefOrRefSig typeDefOrRefSig = typeSpec.TypeSig as TypeDefOrRefSig;
				if (typeDefOrRefSig == null)
				{
					return null;
				}
				typeDef = typeDefOrRefSig.TypeDef;
				if (typeDef != null)
				{
					if (typeDef.Module != this)
					{
						return null;
					}
					return typeDef;
				}
				else
				{
					typeRef2 = typeDefOrRefSig.TypeRef;
					if (typeRef2 != null)
					{
						return this.Find(typeRef2);
					}
					return null;
				}
			}
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x0017FC88 File Offset: 0x0017FC88
		public static ModuleContext CreateModuleContext()
		{
			ModuleContext moduleContext = new ModuleContext();
			AssemblyResolver assemblyResolver = new AssemblyResolver(moduleContext);
			Resolver resolver = new Resolver(assemblyResolver);
			moduleContext.AssemblyResolver = assemblyResolver;
			moduleContext.Resolver = resolver;
			assemblyResolver.DefaultModuleContext = moduleContext;
			return moduleContext;
		}

		// Token: 0x06004C67 RID: 19559 RVA: 0x0017FCC4 File Offset: 0x0017FCC4
		public virtual void LoadEverything(ICancellationToken cancellationToken = null)
		{
			ModuleLoader.LoadAll(this, cancellationToken);
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x0017FCD0 File Offset: 0x0017FCD0
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x0017FCD8 File Offset: 0x0017FCD8
		public IMDTokenProvider ResolveToken(MDToken mdToken)
		{
			return this.ResolveToken(mdToken.Raw, default(GenericParamContext));
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x0017FD00 File Offset: 0x0017FD00
		public IMDTokenProvider ResolveToken(MDToken mdToken, GenericParamContext gpContext)
		{
			return this.ResolveToken(mdToken.Raw, gpContext);
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x0017FD10 File Offset: 0x0017FD10
		public IMDTokenProvider ResolveToken(int token)
		{
			return this.ResolveToken((uint)token, default(GenericParamContext));
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x0017FD34 File Offset: 0x0017FD34
		public IMDTokenProvider ResolveToken(int token, GenericParamContext gpContext)
		{
			return this.ResolveToken((uint)token, gpContext);
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x0017FD40 File Offset: 0x0017FD40
		public IMDTokenProvider ResolveToken(uint token)
		{
			return this.ResolveToken(token, default(GenericParamContext));
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x0017FD64 File Offset: 0x0017FD64
		public virtual IMDTokenProvider ResolveToken(uint token, GenericParamContext gpContext)
		{
			return null;
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0017FD68 File Offset: 0x0017FD68
		public IEnumerable<AssemblyRef> GetAssemblyRefs()
		{
			uint rid = 1U;
			for (;;)
			{
				AssemblyRef assemblyRef = this.ResolveToken(new MDToken(Table.AssemblyRef, rid).Raw) as AssemblyRef;
				if (assemblyRef == null)
				{
					break;
				}
				yield return assemblyRef;
				uint num = rid;
				rid = num + 1U;
			}
			yield break;
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x0017FD78 File Offset: 0x0017FD78
		public IEnumerable<ModuleRef> GetModuleRefs()
		{
			uint rid = 1U;
			for (;;)
			{
				ModuleRef moduleRef = this.ResolveToken(new MDToken(Table.ModuleRef, rid).Raw) as ModuleRef;
				if (moduleRef == null)
				{
					break;
				}
				yield return moduleRef;
				uint num = rid;
				rid = num + 1U;
			}
			yield break;
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0017FD88 File Offset: 0x0017FD88
		public IEnumerable<MemberRef> GetMemberRefs()
		{
			return this.GetMemberRefs(default(GenericParamContext));
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0017FDA8 File Offset: 0x0017FDA8
		public IEnumerable<MemberRef> GetMemberRefs(GenericParamContext gpContext)
		{
			uint rid = 1U;
			for (;;)
			{
				MemberRef memberRef = this.ResolveToken(new MDToken(Table.MemberRef, rid).Raw, gpContext) as MemberRef;
				if (memberRef == null)
				{
					break;
				}
				yield return memberRef;
				uint num = rid;
				rid = num + 1U;
			}
			yield break;
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0017FDC0 File Offset: 0x0017FDC0
		public IEnumerable<TypeRef> GetTypeRefs()
		{
			uint rid = 1U;
			for (;;)
			{
				TypeRef typeRef = this.ResolveToken(new MDToken(Table.TypeRef, rid).Raw) as TypeRef;
				if (typeRef == null)
				{
					break;
				}
				yield return typeRef;
				uint num = rid;
				rid = num + 1U;
			}
			yield break;
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0017FDD0 File Offset: 0x0017FDD0
		public AssemblyRef GetAssemblyRef(UTF8String simpleName)
		{
			AssemblyRef assemblyRef = null;
			foreach (AssemblyRef assemblyRef2 in this.GetAssemblyRefs())
			{
				if (!(assemblyRef2.Name != simpleName) && ModuleDef.IsGreaterAssemblyRefVersion(assemblyRef, assemblyRef2))
				{
					assemblyRef = assemblyRef2;
				}
			}
			return assemblyRef;
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x0017FE44 File Offset: 0x0017FE44
		protected static bool IsGreaterAssemblyRefVersion(AssemblyRef found, AssemblyRef newOne)
		{
			if (found == null)
			{
				return true;
			}
			Version version = found.Version;
			Version version2 = newOne.Version;
			return version == null || (version2 != null && version2 >= version);
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x0017FE84 File Offset: 0x0017FE84
		ITypeDefOrRef ISignatureReaderHelper.ResolveTypeDefOrRef(uint codedToken, GenericParamContext gpContext)
		{
			uint token;
			if (!CodedToken.TypeDefOrRef.Decode(codedToken, out token))
			{
				return null;
			}
			return this.ResolveToken(token) as ITypeDefOrRef;
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0017FEB8 File Offset: 0x0017FEB8
		TypeSig ISignatureReaderHelper.ConvertRTInternalAddress(IntPtr address)
		{
			return null;
		}

		// Token: 0x040025E1 RID: 9697
		protected const Characteristics DefaultCharacteristics = Characteristics.ExecutableImage | Characteristics.Bit32Machine;

		// Token: 0x040025E2 RID: 9698
		protected const DllCharacteristics DefaultDllCharacteristics = DllCharacteristics.DynamicBase | DllCharacteristics.NxCompat | DllCharacteristics.NoSeh | DllCharacteristics.TerminalServerAware;

		// Token: 0x040025E3 RID: 9699
		protected uint rid;

		// Token: 0x040025E4 RID: 9700
		private readonly Lock theLock = Lock.Create();

		// Token: 0x040025E5 RID: 9701
		protected ICorLibTypes corLibTypes;

		// Token: 0x040025E6 RID: 9702
		protected PdbState pdbState;

		// Token: 0x040025E7 RID: 9703
		private TypeDefFinder typeDefFinder;

		// Token: 0x040025E8 RID: 9704
		protected readonly int[] lastUsedRids = new int[64];

		// Token: 0x040025E9 RID: 9705
		protected ModuleContext context;

		// Token: 0x040025EA RID: 9706
		private object tag;

		// Token: 0x040025EB RID: 9707
		protected ushort generation;

		// Token: 0x040025EC RID: 9708
		protected UTF8String name;

		// Token: 0x040025ED RID: 9709
		protected Guid? mvid;

		// Token: 0x040025EE RID: 9710
		protected Guid? encId;

		// Token: 0x040025EF RID: 9711
		protected Guid? encBaseId;

		// Token: 0x040025F0 RID: 9712
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040025F1 RID: 9713
		protected IList<PdbCustomDebugInfo> customDebugInfos;

		// Token: 0x040025F2 RID: 9714
		protected AssemblyDef assembly;

		// Token: 0x040025F3 RID: 9715
		protected LazyList<TypeDef> types;

		// Token: 0x040025F4 RID: 9716
		protected IList<ExportedType> exportedTypes;

		// Token: 0x040025F5 RID: 9717
		protected RVA nativeEntryPoint;

		// Token: 0x040025F6 RID: 9718
		protected IManagedEntryPoint managedEntryPoint;

		// Token: 0x040025F7 RID: 9719
		protected bool nativeAndManagedEntryPoint_initialized;

		// Token: 0x040025F8 RID: 9720
		protected ResourceCollection resources;

		// Token: 0x040025F9 RID: 9721
		protected VTableFixups vtableFixups;

		// Token: 0x040025FA RID: 9722
		protected bool vtableFixups_isInitialized;

		// Token: 0x040025FB RID: 9723
		protected string location;

		// Token: 0x040025FD RID: 9725
		protected Win32Resources win32Resources;

		// Token: 0x040025FE RID: 9726
		protected bool win32Resources_isInitialized;

		// Token: 0x04002602 RID: 9730
		private string runtimeVersion;

		// Token: 0x04002603 RID: 9731
		private WinMDStatus? cachedWinMDStatus;

		// Token: 0x04002604 RID: 9732
		private string runtimeVersionWinMD;

		// Token: 0x04002605 RID: 9733
		private string winMDVersion;

		// Token: 0x04002607 RID: 9735
		protected int cor20HeaderFlags;
	}
}
