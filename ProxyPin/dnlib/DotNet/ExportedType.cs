using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Threading;

namespace dnlib.DotNet
{
	// Token: 0x020007A7 RID: 1959
	[ComVisible(true)]
	public abstract class ExportedType : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IImplementation, IFullName, IHasCustomDebugInformation, IType, IOwnerModule, IGenericParameterProvider, IIsTypeOrMethod, IContainsGenericParameter
	{
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x0600462D RID: 17965 RVA: 0x0016FA60 File Offset: 0x0016FA60
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.ExportedType, this.rid);
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x0016FA70 File Offset: 0x0016FA70
		// (set) Token: 0x0600462F RID: 17967 RVA: 0x0016FA78 File Offset: 0x0016FA78
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

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004630 RID: 17968 RVA: 0x0016FA84 File Offset: 0x0016FA84
		public int HasCustomAttributeTag
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004631 RID: 17969 RVA: 0x0016FA88 File Offset: 0x0016FA88
		public int ImplementationTag
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004632 RID: 17970 RVA: 0x0016FA8C File Offset: 0x0016FA8C
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

		// Token: 0x06004633 RID: 17971 RVA: 0x0016FAA8 File Offset: 0x0016FAA8
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x0016FABC File Offset: 0x0016FABC
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06004635 RID: 17973 RVA: 0x0016FACC File Offset: 0x0016FACC
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06004636 RID: 17974 RVA: 0x0016FAD0 File Offset: 0x0016FAD0
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x0016FAE0 File Offset: 0x0016FAE0
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

		// Token: 0x06004638 RID: 17976 RVA: 0x0016FAFC File Offset: 0x0016FAFC
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06004639 RID: 17977 RVA: 0x0016FB10 File Offset: 0x0016FB10
		public bool IsValueType
		{
			get
			{
				TypeDef typeDef = this.Resolve();
				return typeDef != null && typeDef.IsValueType;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x0016FB38 File Offset: 0x0016FB38
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitive();
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600463B RID: 17979 RVA: 0x0016FB40 File Offset: 0x0016FB40
		string IType.TypeName
		{
			get
			{
				return FullNameFactory.Name(this, false, null);
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x0016FB4C File Offset: 0x0016FB4C
		// (set) Token: 0x0600463D RID: 17981 RVA: 0x0016FB54 File Offset: 0x0016FB54
		public UTF8String Name
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x0016FB60 File Offset: 0x0016FB60
		public string ReflectionName
		{
			get
			{
				return FullNameFactory.Name(this, true, null);
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600463F RID: 17983 RVA: 0x0016FB6C File Offset: 0x0016FB6C
		public string Namespace
		{
			get
			{
				return FullNameFactory.Namespace(this, false, null);
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004640 RID: 17984 RVA: 0x0016FB78 File Offset: 0x0016FB78
		public string ReflectionNamespace
		{
			get
			{
				return FullNameFactory.Namespace(this, true, null);
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06004641 RID: 17985 RVA: 0x0016FB84 File Offset: 0x0016FB84
		public string FullName
		{
			get
			{
				return FullNameFactory.FullName(this, false, null, null);
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x0016FB90 File Offset: 0x0016FB90
		public string ReflectionFullName
		{
			get
			{
				return FullNameFactory.FullName(this, true, null, null);
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x0016FB9C File Offset: 0x0016FB9C
		public string AssemblyQualifiedName
		{
			get
			{
				return FullNameFactory.AssemblyQualifiedName(this, null, null);
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x0016FBA8 File Offset: 0x0016FBA8
		public IAssembly DefinitionAssembly
		{
			get
			{
				return FullNameFactory.DefinitionAssembly(this);
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x0016FBB0 File Offset: 0x0016FBB0
		public IScope Scope
		{
			get
			{
				return FullNameFactory.Scope(this);
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004646 RID: 17990 RVA: 0x0016FBB8 File Offset: 0x0016FBB8
		public ITypeDefOrRef ScopeType
		{
			get
			{
				return FullNameFactory.ScopeType(this);
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x0016FBC0 File Offset: 0x0016FBC0
		public bool ContainsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x0016FBC4 File Offset: 0x0016FBC4
		public ModuleDef Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004649 RID: 17993 RVA: 0x0016FBCC File Offset: 0x0016FBCC
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x0600464A RID: 17994 RVA: 0x0016FBD0 File Offset: 0x0016FBD0
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600464B RID: 17995 RVA: 0x0016FBD4 File Offset: 0x0016FBD4
		int IGenericParameterProvider.NumberOfGenericParameters
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600464C RID: 17996 RVA: 0x0016FBD8 File Offset: 0x0016FBD8
		// (set) Token: 0x0600464D RID: 17997 RVA: 0x0016FBE0 File Offset: 0x0016FBE0
		public TypeAttributes Attributes
		{
			get
			{
				return (TypeAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600464E RID: 17998 RVA: 0x0016FBEC File Offset: 0x0016FBEC
		// (set) Token: 0x0600464F RID: 17999 RVA: 0x0016FBF4 File Offset: 0x0016FBF4
		public uint TypeDefId
		{
			get
			{
				return this.typeDefId;
			}
			set
			{
				this.typeDefId = value;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x0016FC00 File Offset: 0x0016FC00
		// (set) Token: 0x06004651 RID: 18001 RVA: 0x0016FC08 File Offset: 0x0016FC08
		public UTF8String TypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004652 RID: 18002 RVA: 0x0016FC14 File Offset: 0x0016FC14
		// (set) Token: 0x06004653 RID: 18003 RVA: 0x0016FC1C File Offset: 0x0016FC1C
		public UTF8String TypeNamespace
		{
			get
			{
				return this.typeNamespace;
			}
			set
			{
				this.typeNamespace = value;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004654 RID: 18004 RVA: 0x0016FC28 File Offset: 0x0016FC28
		// (set) Token: 0x06004655 RID: 18005 RVA: 0x0016FC44 File Offset: 0x0016FC44
		public IImplementation Implementation
		{
			get
			{
				if (!this.implementation_isInitialized)
				{
					this.InitializeImplementation();
				}
				return this.implementation;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.implementation = value;
					this.implementation_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x0016FC8C File Offset: 0x0016FC8C
		private void InitializeImplementation()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.implementation_isInitialized)
				{
					this.implementation = this.GetImplementation_NoLock();
					this.implementation_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x0016FCE8 File Offset: 0x0016FCE8
		protected virtual IImplementation GetImplementation_NoLock()
		{
			return null;
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x0016FCEC File Offset: 0x0016FCEC
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x0016FCFC File Offset: 0x0016FCFC
		public ExportedType DeclaringType
		{
			get
			{
				if (!this.implementation_isInitialized)
				{
					this.InitializeImplementation();
				}
				return this.implementation as ExportedType;
			}
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x0016FD1C File Offset: 0x0016FD1C
		private void ModifyAttributes(TypeAttributes andMask, TypeAttributes orMask)
		{
			this.attributes = ((this.attributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x0016FD30 File Offset: 0x0016FD30
		private void ModifyAttributes(bool set, TypeAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x0600465C RID: 18012 RVA: 0x0016FD58 File Offset: 0x0016FD58
		// (set) Token: 0x0600465D RID: 18013 RVA: 0x0016FD64 File Offset: 0x0016FD64
		public TypeAttributes Visibility
		{
			get
			{
				return (TypeAttributes)(this.attributes & 7);
			}
			set
			{
				this.ModifyAttributes(~TypeAttributes.VisibilityMask, value & TypeAttributes.VisibilityMask);
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x0600465E RID: 18014 RVA: 0x0016FD74 File Offset: 0x0016FD74
		public bool IsNotPublic
		{
			get
			{
				return (this.attributes & 7) == 0;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x0600465F RID: 18015 RVA: 0x0016FD84 File Offset: 0x0016FD84
		public bool IsPublic
		{
			get
			{
				return (this.attributes & 7) == 1;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004660 RID: 18016 RVA: 0x0016FD94 File Offset: 0x0016FD94
		public bool IsNestedPublic
		{
			get
			{
				return (this.attributes & 7) == 2;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004661 RID: 18017 RVA: 0x0016FDA4 File Offset: 0x0016FDA4
		public bool IsNestedPrivate
		{
			get
			{
				return (this.attributes & 7) == 3;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004662 RID: 18018 RVA: 0x0016FDB4 File Offset: 0x0016FDB4
		public bool IsNestedFamily
		{
			get
			{
				return (this.attributes & 7) == 4;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004663 RID: 18019 RVA: 0x0016FDC4 File Offset: 0x0016FDC4
		public bool IsNestedAssembly
		{
			get
			{
				return (this.attributes & 7) == 5;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004664 RID: 18020 RVA: 0x0016FDD4 File Offset: 0x0016FDD4
		public bool IsNestedFamilyAndAssembly
		{
			get
			{
				return (this.attributes & 7) == 6;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004665 RID: 18021 RVA: 0x0016FDE4 File Offset: 0x0016FDE4
		public bool IsNestedFamilyOrAssembly
		{
			get
			{
				return (this.attributes & 7) == 7;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x0016FDF4 File Offset: 0x0016FDF4
		// (set) Token: 0x06004667 RID: 18023 RVA: 0x0016FE00 File Offset: 0x0016FE00
		public TypeAttributes Layout
		{
			get
			{
				return (TypeAttributes)(this.attributes & 24);
			}
			set
			{
				this.ModifyAttributes(~TypeAttributes.LayoutMask, value & TypeAttributes.LayoutMask);
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004668 RID: 18024 RVA: 0x0016FE10 File Offset: 0x0016FE10
		public bool IsAutoLayout
		{
			get
			{
				return (this.attributes & 24) == 0;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004669 RID: 18025 RVA: 0x0016FE20 File Offset: 0x0016FE20
		public bool IsSequentialLayout
		{
			get
			{
				return (this.attributes & 24) == 8;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600466A RID: 18026 RVA: 0x0016FE30 File Offset: 0x0016FE30
		public bool IsExplicitLayout
		{
			get
			{
				return (this.attributes & 24) == 16;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600466B RID: 18027 RVA: 0x0016FE40 File Offset: 0x0016FE40
		// (set) Token: 0x0600466C RID: 18028 RVA: 0x0016FE50 File Offset: 0x0016FE50
		public bool IsInterface
		{
			get
			{
				return (this.attributes & 32) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.ClassSemanticsMask);
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600466D RID: 18029 RVA: 0x0016FE5C File Offset: 0x0016FE5C
		// (set) Token: 0x0600466E RID: 18030 RVA: 0x0016FE6C File Offset: 0x0016FE6C
		public bool IsClass
		{
			get
			{
				return (this.attributes & 32) == 0;
			}
			set
			{
				this.ModifyAttributes(!value, TypeAttributes.ClassSemanticsMask);
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600466F RID: 18031 RVA: 0x0016FE7C File Offset: 0x0016FE7C
		// (set) Token: 0x06004670 RID: 18032 RVA: 0x0016FE90 File Offset: 0x0016FE90
		public bool IsAbstract
		{
			get
			{
				return (this.attributes & 128) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Abstract);
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004671 RID: 18033 RVA: 0x0016FEA0 File Offset: 0x0016FEA0
		// (set) Token: 0x06004672 RID: 18034 RVA: 0x0016FEB4 File Offset: 0x0016FEB4
		public bool IsSealed
		{
			get
			{
				return (this.attributes & 256) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Sealed);
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004673 RID: 18035 RVA: 0x0016FEC4 File Offset: 0x0016FEC4
		// (set) Token: 0x06004674 RID: 18036 RVA: 0x0016FED8 File Offset: 0x0016FED8
		public bool IsSpecialName
		{
			get
			{
				return (this.attributes & 1024) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.SpecialName);
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x0016FEE8 File Offset: 0x0016FEE8
		// (set) Token: 0x06004676 RID: 18038 RVA: 0x0016FEFC File Offset: 0x0016FEFC
		public bool IsImport
		{
			get
			{
				return (this.attributes & 4096) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Import);
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x0016FF0C File Offset: 0x0016FF0C
		// (set) Token: 0x06004678 RID: 18040 RVA: 0x0016FF20 File Offset: 0x0016FF20
		public bool IsSerializable
		{
			get
			{
				return (this.attributes & 8192) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Serializable);
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004679 RID: 18041 RVA: 0x0016FF30 File Offset: 0x0016FF30
		// (set) Token: 0x0600467A RID: 18042 RVA: 0x0016FF44 File Offset: 0x0016FF44
		public bool IsWindowsRuntime
		{
			get
			{
				return (this.attributes & 16384) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.WindowsRuntime);
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x0600467B RID: 18043 RVA: 0x0016FF54 File Offset: 0x0016FF54
		// (set) Token: 0x0600467C RID: 18044 RVA: 0x0016FF64 File Offset: 0x0016FF64
		public TypeAttributes StringFormat
		{
			get
			{
				return (TypeAttributes)(this.attributes & 196608);
			}
			set
			{
				this.ModifyAttributes(~TypeAttributes.StringFormatMask, value & TypeAttributes.StringFormatMask);
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600467D RID: 18045 RVA: 0x0016FF78 File Offset: 0x0016FF78
		public bool IsAnsiClass
		{
			get
			{
				return (this.attributes & 196608) == 0;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600467E RID: 18046 RVA: 0x0016FF8C File Offset: 0x0016FF8C
		public bool IsUnicodeClass
		{
			get
			{
				return (this.attributes & 196608) == 65536;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600467F RID: 18047 RVA: 0x0016FFA4 File Offset: 0x0016FFA4
		public bool IsAutoClass
		{
			get
			{
				return (this.attributes & 196608) == 131072;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004680 RID: 18048 RVA: 0x0016FFBC File Offset: 0x0016FFBC
		public bool IsCustomFormatClass
		{
			get
			{
				return (this.attributes & 196608) == 196608;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x0016FFD4 File Offset: 0x0016FFD4
		// (set) Token: 0x06004682 RID: 18050 RVA: 0x0016FFE8 File Offset: 0x0016FFE8
		public bool IsBeforeFieldInit
		{
			get
			{
				return (this.attributes & 1048576) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.BeforeFieldInit);
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004683 RID: 18051 RVA: 0x0016FFF8 File Offset: 0x0016FFF8
		// (set) Token: 0x06004684 RID: 18052 RVA: 0x0017000C File Offset: 0x0017000C
		public bool IsForwarder
		{
			get
			{
				return (this.attributes & 2097152) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.Forwarder);
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004685 RID: 18053 RVA: 0x0017001C File Offset: 0x0017001C
		// (set) Token: 0x06004686 RID: 18054 RVA: 0x00170030 File Offset: 0x00170030
		public bool IsRuntimeSpecialName
		{
			get
			{
				return (this.attributes & 2048) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.RTSpecialName);
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x00170040 File Offset: 0x00170040
		// (set) Token: 0x06004688 RID: 18056 RVA: 0x00170054 File Offset: 0x00170054
		public bool HasSecurity
		{
			get
			{
				return (this.attributes & 262144) != 0;
			}
			set
			{
				this.ModifyAttributes(value, TypeAttributes.HasSecurity);
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004689 RID: 18057 RVA: 0x00170064 File Offset: 0x00170064
		public bool MovedToAnotherAssembly
		{
			get
			{
				ExportedType exportedType = this;
				for (int i = 0; i < 50; i++)
				{
					IImplementation implementation = exportedType.Implementation;
					if (implementation is AssemblyRef)
					{
						return exportedType.IsForwarder;
					}
					exportedType = (implementation as ExportedType);
					if (exportedType == null)
					{
						break;
					}
				}
				return false;
			}
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x001700B0 File Offset: 0x001700B0
		public TypeDef Resolve()
		{
			return this.Resolve(null);
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x001700BC File Offset: 0x001700BC
		public TypeDef Resolve(ModuleDef sourceModule)
		{
			if (this.module == null)
			{
				return null;
			}
			return ExportedType.Resolve(sourceModule, this);
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x001700D4 File Offset: 0x001700D4
		private static TypeDef Resolve(ModuleDef sourceModule, ExportedType et)
		{
			int num = 0;
			while (num < 50 && et != null && et.module != null)
			{
				AssemblyDef assemblyDef = et.module.Context.AssemblyResolver.Resolve(et.DefinitionAssembly, sourceModule ?? et.module);
				if (assemblyDef == null)
				{
					break;
				}
				TypeDef typeDef = assemblyDef.Find(et.FullName, false);
				if (typeDef != null)
				{
					return typeDef;
				}
				et = ExportedType.FindExportedType(assemblyDef, et);
				num++;
			}
			return null;
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x00170158 File Offset: 0x00170158
		private static ExportedType FindExportedType(AssemblyDef asm, ExportedType et)
		{
			IList<ModuleDef> modules = asm.Modules;
			int count = modules.Count;
			for (int i = 0; i < count; i++)
			{
				IList<ExportedType> exportedTypes = modules[i].ExportedTypes;
				int count2 = exportedTypes.Count;
				for (int j = 0; j < count2; j++)
				{
					ExportedType exportedType = exportedTypes[j];
					if (new SigComparer(SigComparerOptions.DontCompareTypeScope).Equals(et, exportedType))
					{
						return exportedType;
					}
				}
			}
			return null;
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x001701D4 File Offset: 0x001701D4
		public TypeDef ResolveThrow()
		{
			TypeDef typeDef = this.Resolve();
			if (typeDef != null)
			{
				return typeDef;
			}
			throw new TypeResolveException(string.Format("Could not resolve type: {0} ({1})", this, this.DefinitionAssembly));
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x0017020C File Offset: 0x0017020C
		public TypeRef ToTypeRef()
		{
			TypeRef typeRef = null;
			TypeRef typeRef2 = null;
			ModuleDef moduleDef = this.module;
			IImplementation implementation = this;
			int num = 0;
			while (num < 50 && implementation != null)
			{
				ExportedType exportedType = implementation as ExportedType;
				if (exportedType != null)
				{
					TypeRefUser typeRefUser = moduleDef.UpdateRowId<TypeRefUser>(new TypeRefUser(moduleDef, exportedType.TypeNamespace, exportedType.TypeName));
					if (typeRef == null)
					{
						typeRef = typeRefUser;
					}
					if (typeRef2 != null)
					{
						typeRef2.ResolutionScope = typeRefUser;
					}
					typeRef2 = typeRefUser;
					implementation = exportedType.Implementation;
					num++;
				}
				else
				{
					AssemblyRef assemblyRef = implementation as AssemblyRef;
					if (assemblyRef != null)
					{
						typeRef2.ResolutionScope = assemblyRef;
						return typeRef;
					}
					FileDef fileDef = implementation as FileDef;
					if (fileDef != null)
					{
						typeRef2.ResolutionScope = ExportedType.FindModule(moduleDef, fileDef);
						return typeRef;
					}
					break;
				}
			}
			return typeRef;
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x001702D4 File Offset: 0x001702D4
		private static ModuleDef FindModule(ModuleDef module, FileDef file)
		{
			if (module == null || file == null)
			{
				return null;
			}
			if (UTF8String.CaseInsensitiveEquals(module.Name, file.Name))
			{
				return module;
			}
			AssemblyDef assembly = module.Assembly;
			if (assembly == null)
			{
				return null;
			}
			return assembly.FindModule(file.Name);
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x00170328 File Offset: 0x00170328
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04002498 RID: 9368
		protected uint rid;

		// Token: 0x04002499 RID: 9369
		private readonly Lock theLock = Lock.Create();

		// Token: 0x0400249A RID: 9370
		protected ModuleDef module;

		// Token: 0x0400249B RID: 9371
		protected CustomAttributeCollection customAttributes;

		// Token: 0x0400249C RID: 9372
		protected IList<PdbCustomDebugInfo> customDebugInfos;

		// Token: 0x0400249D RID: 9373
		protected int attributes;

		// Token: 0x0400249E RID: 9374
		protected uint typeDefId;

		// Token: 0x0400249F RID: 9375
		protected UTF8String typeName;

		// Token: 0x040024A0 RID: 9376
		protected UTF8String typeNamespace;

		// Token: 0x040024A1 RID: 9377
		protected IImplementation implementation;

		// Token: 0x040024A2 RID: 9378
		protected bool implementation_isInitialized;

		// Token: 0x040024A3 RID: 9379
		private const int MAX_LOOP_ITERS = 50;
	}
}
