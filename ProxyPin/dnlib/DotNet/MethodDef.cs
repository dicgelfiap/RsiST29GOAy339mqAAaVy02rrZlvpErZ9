using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.PE;
using dnlib.Threading;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x0200080D RID: 2061
	[ComVisible(true)]
	public abstract class MethodDef : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasDeclSecurity, IFullName, IMemberRefParent, IMethodDefOrRef, ICustomAttributeType, IMethod, ITokenOperand, IGenericParameterProvider, IIsTypeOrMethod, IMemberRef, IOwnerModule, IMemberForwarded, ITypeOrMethodDef, IManagedEntryPoint, IHasCustomDebugInformation, IListListener<GenericParam>, IListListener<ParamDef>, IMemberDef, IDnlibDef
	{
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06004A8C RID: 19084 RVA: 0x0017CA24 File Offset: 0x0017CA24
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.Method, this.rid);
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x0017CA34 File Offset: 0x0017CA34
		// (set) Token: 0x06004A8E RID: 19086 RVA: 0x0017CA3C File Offset: 0x0017CA3C
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

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x0017CA48 File Offset: 0x0017CA48
		public int HasCustomAttributeTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06004A90 RID: 19088 RVA: 0x0017CA4C File Offset: 0x0017CA4C
		public int HasDeclSecurityTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x0017CA50 File Offset: 0x0017CA50
		public int MemberRefParentTag
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06004A92 RID: 19090 RVA: 0x0017CA54 File Offset: 0x0017CA54
		public int MethodDefOrRefTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x0017CA58 File Offset: 0x0017CA58
		public int MemberForwardedTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06004A94 RID: 19092 RVA: 0x0017CA5C File Offset: 0x0017CA5C
		public int CustomAttributeTypeTag
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06004A95 RID: 19093 RVA: 0x0017CA60 File Offset: 0x0017CA60
		public int TypeOrMethodDefTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06004A96 RID: 19094 RVA: 0x0017CA64 File Offset: 0x0017CA64
		// (set) Token: 0x06004A97 RID: 19095 RVA: 0x0017CA6C File Offset: 0x0017CA6C
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
			set
			{
				this.rva = value;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06004A98 RID: 19096 RVA: 0x0017CA78 File Offset: 0x0017CA78
		// (set) Token: 0x06004A99 RID: 19097 RVA: 0x0017CA84 File Offset: 0x0017CA84
		public MethodImplAttributes ImplAttributes
		{
			get
			{
				return (MethodImplAttributes)this.implAttributes;
			}
			set
			{
				this.implAttributes = (int)value;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x0017CA90 File Offset: 0x0017CA90
		// (set) Token: 0x06004A9B RID: 19099 RVA: 0x0017CA9C File Offset: 0x0017CA9C
		public MethodAttributes Attributes
		{
			get
			{
				return (MethodAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06004A9C RID: 19100 RVA: 0x0017CAA8 File Offset: 0x0017CAA8
		// (set) Token: 0x06004A9D RID: 19101 RVA: 0x0017CAB0 File Offset: 0x0017CAB0
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

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06004A9E RID: 19102 RVA: 0x0017CABC File Offset: 0x0017CABC
		// (set) Token: 0x06004A9F RID: 19103 RVA: 0x0017CAC4 File Offset: 0x0017CAC4
		public CallingConventionSig Signature
		{
			get
			{
				return this.signature;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06004AA0 RID: 19104 RVA: 0x0017CAD0 File Offset: 0x0017CAD0
		public IList<ParamDef> ParamDefs
		{
			get
			{
				if (this.paramDefs == null)
				{
					this.InitializeParamDefs();
				}
				return this.paramDefs;
			}
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x0017CAEC File Offset: 0x0017CAEC
		protected virtual void InitializeParamDefs()
		{
			Interlocked.CompareExchange<LazyList<ParamDef>>(ref this.paramDefs, new LazyList<ParamDef>(this), null);
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06004AA2 RID: 19106 RVA: 0x0017CB04 File Offset: 0x0017CB04
		public IList<GenericParam> GenericParameters
		{
			get
			{
				if (this.genericParameters == null)
				{
					this.InitializeGenericParameters();
				}
				return this.genericParameters;
			}
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0017CB20 File Offset: 0x0017CB20
		protected virtual void InitializeGenericParameters()
		{
			Interlocked.CompareExchange<LazyList<GenericParam>>(ref this.genericParameters, new LazyList<GenericParam>(this), null);
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x0017CB38 File Offset: 0x0017CB38
		public IList<DeclSecurity> DeclSecurities
		{
			get
			{
				if (this.declSecurities == null)
				{
					this.InitializeDeclSecurities();
				}
				return this.declSecurities;
			}
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x0017CB54 File Offset: 0x0017CB54
		protected virtual void InitializeDeclSecurities()
		{
			Interlocked.CompareExchange<IList<DeclSecurity>>(ref this.declSecurities, new List<DeclSecurity>(), null);
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x0017CB68 File Offset: 0x0017CB68
		// (set) Token: 0x06004AA7 RID: 19111 RVA: 0x0017CB84 File Offset: 0x0017CB84
		public ImplMap ImplMap
		{
			get
			{
				if (!this.implMap_isInitialized)
				{
					this.InitializeImplMap();
				}
				return this.implMap;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.implMap = value;
					this.implMap_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x0017CBCC File Offset: 0x0017CBCC
		private void InitializeImplMap()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.implMap_isInitialized)
				{
					this.implMap = this.GetImplMap_NoLock();
					this.implMap_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x0017CC28 File Offset: 0x0017CC28
		protected virtual ImplMap GetImplMap_NoLock()
		{
			return null;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x0017CC2C File Offset: 0x0017CC2C
		protected void ResetImplMap()
		{
			this.implMap_isInitialized = false;
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06004AAB RID: 19115 RVA: 0x0017CC38 File Offset: 0x0017CC38
		// (set) Token: 0x06004AAC RID: 19116 RVA: 0x0017CC54 File Offset: 0x0017CC54
		public MethodBody MethodBody
		{
			get
			{
				if (!this.methodBody_isInitialized)
				{
					this.InitializeMethodBody();
				}
				return this.methodBody;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.methodBody = value;
					this.methodBody_isInitialized = true;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x0017CC9C File Offset: 0x0017CC9C
		private void InitializeMethodBody()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (!this.methodBody_isInitialized)
				{
					this.methodBody = this.GetMethodBody_NoLock();
					this.methodBody_isInitialized = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x0017CCF8 File Offset: 0x0017CCF8
		public void FreeMethodBody()
		{
			if (!this.CanFreeMethodBody)
			{
				return;
			}
			if (!this.methodBody_isInitialized)
			{
				return;
			}
			this.theLock.EnterWriteLock();
			try
			{
				this.methodBody = null;
				this.methodBody_isInitialized = false;
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x0017CD58 File Offset: 0x0017CD58
		protected virtual MethodBody GetMethodBody_NoLock()
		{
			return null;
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x0017CD5C File Offset: 0x0017CD5C
		protected virtual bool CanFreeMethodBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06004AB1 RID: 19121 RVA: 0x0017CD60 File Offset: 0x0017CD60
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

		// Token: 0x06004AB2 RID: 19122 RVA: 0x0017CD7C File Offset: 0x0017CD7C
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06004AB3 RID: 19123 RVA: 0x0017CD90 File Offset: 0x0017CD90
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06004AB4 RID: 19124 RVA: 0x0017CD94 File Offset: 0x0017CD94
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06004AB5 RID: 19125 RVA: 0x0017CDA4 File Offset: 0x0017CDA4
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

		// Token: 0x06004AB6 RID: 19126 RVA: 0x0017CDC0 File Offset: 0x0017CDC0
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x0017CDD4 File Offset: 0x0017CDD4
		public IList<MethodOverride> Overrides
		{
			get
			{
				if (this.overrides == null)
				{
					this.InitializeOverrides();
				}
				return this.overrides;
			}
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x0017CDF0 File Offset: 0x0017CDF0
		protected virtual void InitializeOverrides()
		{
			Interlocked.CompareExchange<IList<MethodOverride>>(ref this.overrides, new List<MethodOverride>(), null);
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x0017CE04 File Offset: 0x0017CE04
		// (set) Token: 0x06004ABA RID: 19130 RVA: 0x0017CE0C File Offset: 0x0017CE0C
		public MethodExportInfo ExportInfo
		{
			get
			{
				return this.exportInfo;
			}
			set
			{
				this.exportInfo = value;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06004ABB RID: 19131 RVA: 0x0017CE18 File Offset: 0x0017CE18
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06004ABC RID: 19132 RVA: 0x0017CE28 File Offset: 0x0017CE28
		public bool HasDeclSecurities
		{
			get
			{
				return this.DeclSecurities.Count > 0;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06004ABD RID: 19133 RVA: 0x0017CE38 File Offset: 0x0017CE38
		public bool HasParamDefs
		{
			get
			{
				return this.ParamDefs.Count > 0;
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06004ABE RID: 19134 RVA: 0x0017CE48 File Offset: 0x0017CE48
		// (set) Token: 0x06004ABF RID: 19135 RVA: 0x0017CE50 File Offset: 0x0017CE50
		public TypeDef DeclaringType
		{
			get
			{
				return this.declaringType2;
			}
			set
			{
				TypeDef typeDef = this.DeclaringType2;
				if (typeDef == value)
				{
					return;
				}
				if (typeDef != null)
				{
					typeDef.Methods.Remove(this);
				}
				if (value != null)
				{
					value.Methods.Add(this);
				}
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06004AC0 RID: 19136 RVA: 0x0017CE98 File Offset: 0x0017CE98
		ITypeDefOrRef IMemberRef.DeclaringType
		{
			get
			{
				return this.declaringType2;
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06004AC1 RID: 19137 RVA: 0x0017CEA0 File Offset: 0x0017CEA0
		// (set) Token: 0x06004AC2 RID: 19138 RVA: 0x0017CEA8 File Offset: 0x0017CEA8
		public TypeDef DeclaringType2
		{
			get
			{
				return this.declaringType2;
			}
			set
			{
				this.declaringType2 = value;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06004AC3 RID: 19139 RVA: 0x0017CEB4 File Offset: 0x0017CEB4
		public ModuleDef Module
		{
			get
			{
				TypeDef typeDef = this.declaringType2;
				if (typeDef == null)
				{
					return null;
				}
				return typeDef.Module;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06004AC4 RID: 19140 RVA: 0x0017CECC File Offset: 0x0017CECC
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06004AC5 RID: 19141 RVA: 0x0017CED0 File Offset: 0x0017CED0
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x0017CED4 File Offset: 0x0017CED4
		bool IMemberRef.IsField
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06004AC7 RID: 19143 RVA: 0x0017CED8 File Offset: 0x0017CED8
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x0017CEDC File Offset: 0x0017CEDC
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06004AC9 RID: 19145 RVA: 0x0017CEE0 File Offset: 0x0017CEE0
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06004ACA RID: 19146 RVA: 0x0017CEE4 File Offset: 0x0017CEE4
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06004ACB RID: 19147 RVA: 0x0017CEE8 File Offset: 0x0017CEE8
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06004ACC RID: 19148 RVA: 0x0017CEEC File Offset: 0x0017CEEC
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06004ACD RID: 19149 RVA: 0x0017CEF0 File Offset: 0x0017CEF0
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06004ACE RID: 19150 RVA: 0x0017CEF4 File Offset: 0x0017CEF4
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x0017CEF8 File Offset: 0x0017CEF8
		bool IMemberRef.IsEventDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06004AD0 RID: 19152 RVA: 0x0017CEFC File Offset: 0x0017CEFC
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06004AD1 RID: 19153 RVA: 0x0017CF00 File Offset: 0x0017CF00
		// (set) Token: 0x06004AD2 RID: 19154 RVA: 0x0017CF20 File Offset: 0x0017CF20
		public CilBody Body
		{
			get
			{
				if (!this.methodBody_isInitialized)
				{
					this.InitializeMethodBody();
				}
				return this.methodBody as CilBody;
			}
			set
			{
				this.MethodBody = value;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06004AD3 RID: 19155 RVA: 0x0017CF2C File Offset: 0x0017CF2C
		// (set) Token: 0x06004AD4 RID: 19156 RVA: 0x0017CF4C File Offset: 0x0017CF4C
		public NativeMethodBody NativeBody
		{
			get
			{
				if (!this.methodBody_isInitialized)
				{
					this.InitializeMethodBody();
				}
				return this.methodBody as NativeMethodBody;
			}
			set
			{
				this.MethodBody = value;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x0017CF58 File Offset: 0x0017CF58
		public bool HasGenericParameters
		{
			get
			{
				return this.GenericParameters.Count > 0;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06004AD6 RID: 19158 RVA: 0x0017CF68 File Offset: 0x0017CF68
		public bool HasBody
		{
			get
			{
				return this.Body != null;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x0017CF78 File Offset: 0x0017CF78
		public bool HasOverrides
		{
			get
			{
				return this.Overrides.Count > 0;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06004AD8 RID: 19160 RVA: 0x0017CF88 File Offset: 0x0017CF88
		public bool HasImplMap
		{
			get
			{
				return this.ImplMap != null;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06004AD9 RID: 19161 RVA: 0x0017CF98 File Offset: 0x0017CF98
		public string FullName
		{
			get
			{
				TypeDef typeDef = this.declaringType2;
				return FullNameFactory.MethodFullName((typeDef != null) ? typeDef.FullName : null, this.name, this.MethodSig, null, null, this, null);
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06004ADA RID: 19162 RVA: 0x0017CFDC File Offset: 0x0017CFDC
		// (set) Token: 0x06004ADB RID: 19163 RVA: 0x0017CFEC File Offset: 0x0017CFEC
		public MethodSig MethodSig
		{
			get
			{
				return this.signature as MethodSig;
			}
			set
			{
				this.signature = value;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06004ADC RID: 19164 RVA: 0x0017CFF8 File Offset: 0x0017CFF8
		public ParameterList Parameters
		{
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06004ADD RID: 19165 RVA: 0x0017D000 File Offset: 0x0017D000
		int IGenericParameterProvider.NumberOfGenericParameters
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				if (methodSig != null)
				{
					return (int)methodSig.GenParamCount;
				}
				return 0;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06004ADE RID: 19166 RVA: 0x0017D028 File Offset: 0x0017D028
		public bool HasThis
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				return methodSig != null && methodSig.HasThis;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06004ADF RID: 19167 RVA: 0x0017D050 File Offset: 0x0017D050
		public bool ExplicitThis
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				return methodSig != null && methodSig.ExplicitThis;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x0017D078 File Offset: 0x0017D078
		public CallingConvention CallingConvention
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				if (methodSig != null)
				{
					return methodSig.CallingConvention & CallingConvention.Mask;
				}
				return CallingConvention.Default;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06004AE1 RID: 19169 RVA: 0x0017D0A4 File Offset: 0x0017D0A4
		// (set) Token: 0x06004AE2 RID: 19170 RVA: 0x0017D0BC File Offset: 0x0017D0BC
		public TypeSig ReturnType
		{
			get
			{
				MethodSig methodSig = this.MethodSig;
				if (methodSig == null)
				{
					return null;
				}
				return methodSig.RetType;
			}
			set
			{
				MethodSig methodSig = this.MethodSig;
				if (methodSig != null)
				{
					methodSig.RetType = value;
				}
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x0017D0E4 File Offset: 0x0017D0E4
		public bool HasReturnType
		{
			get
			{
				return this.ReturnType.RemovePinnedAndModifiers().GetElementType() != ElementType.Void;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x0017D0FC File Offset: 0x0017D0FC
		// (set) Token: 0x06004AE5 RID: 19173 RVA: 0x0017D11C File Offset: 0x0017D11C
		public MethodSemanticsAttributes SemanticsAttributes
		{
			get
			{
				if ((this.semAttrs & MethodDef.SEMATTRS_INITD) == 0)
				{
					this.InitializeSemanticsAttributes();
				}
				return (MethodSemanticsAttributes)this.semAttrs;
			}
			set
			{
				this.semAttrs = ((int)value | MethodDef.SEMATTRS_INITD);
			}
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x0017D12C File Offset: 0x0017D12C
		protected virtual void InitializeSemanticsAttributes()
		{
			this.semAttrs = (0 | MethodDef.SEMATTRS_INITD);
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x0017D13C File Offset: 0x0017D13C
		private void ModifyAttributes(bool set, MethodSemanticsAttributes flags)
		{
			if ((this.semAttrs & MethodDef.SEMATTRS_INITD) == 0)
			{
				this.InitializeSemanticsAttributes();
			}
			if (set)
			{
				this.semAttrs |= (int)flags;
				return;
			}
			this.semAttrs &= (int)(~(int)flags);
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x0017D17C File Offset: 0x0017D17C
		private void ModifyAttributes(MethodAttributes andMask, MethodAttributes orMask)
		{
			this.attributes = ((this.attributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x0017D190 File Offset: 0x0017D190
		private void ModifyAttributes(bool set, MethodAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x0017D1B8 File Offset: 0x0017D1B8
		private void ModifyImplAttributes(MethodImplAttributes andMask, MethodImplAttributes orMask)
		{
			this.implAttributes = ((this.implAttributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x0017D1CC File Offset: 0x0017D1CC
		private void ModifyImplAttributes(bool set, MethodImplAttributes flags)
		{
			if (set)
			{
				this.implAttributes |= (int)flags;
				return;
			}
			this.implAttributes &= (int)(~(int)flags);
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06004AEC RID: 19180 RVA: 0x0017D1F4 File Offset: 0x0017D1F4
		// (set) Token: 0x06004AED RID: 19181 RVA: 0x0017D200 File Offset: 0x0017D200
		public MethodAttributes Access
		{
			get
			{
				return (MethodAttributes)this.attributes & MethodAttributes.MemberAccessMask;
			}
			set
			{
				this.ModifyAttributes(~MethodAttributes.MemberAccessMask, value & MethodAttributes.MemberAccessMask);
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x0017D210 File Offset: 0x0017D210
		public bool IsCompilerControlled
		{
			get
			{
				return this.IsPrivateScope;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06004AEF RID: 19183 RVA: 0x0017D218 File Offset: 0x0017D218
		public bool IsPrivateScope
		{
			get
			{
				return ((ushort)this.attributes & 7) == 0;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06004AF0 RID: 19184 RVA: 0x0017D228 File Offset: 0x0017D228
		public bool IsPrivate
		{
			get
			{
				return ((ushort)this.attributes & 7) == 1;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06004AF1 RID: 19185 RVA: 0x0017D238 File Offset: 0x0017D238
		public bool IsFamilyAndAssembly
		{
			get
			{
				return ((ushort)this.attributes & 7) == 2;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06004AF2 RID: 19186 RVA: 0x0017D248 File Offset: 0x0017D248
		public bool IsAssembly
		{
			get
			{
				return ((ushort)this.attributes & 7) == 3;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06004AF3 RID: 19187 RVA: 0x0017D258 File Offset: 0x0017D258
		public bool IsFamily
		{
			get
			{
				return ((ushort)this.attributes & 7) == 4;
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x0017D268 File Offset: 0x0017D268
		public bool IsFamilyOrAssembly
		{
			get
			{
				return ((ushort)this.attributes & 7) == 5;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x0017D278 File Offset: 0x0017D278
		public bool IsPublic
		{
			get
			{
				return ((ushort)this.attributes & 7) == 6;
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06004AF6 RID: 19190 RVA: 0x0017D288 File Offset: 0x0017D288
		// (set) Token: 0x06004AF7 RID: 19191 RVA: 0x0017D298 File Offset: 0x0017D298
		public bool IsStatic
		{
			get
			{
				return ((ushort)this.attributes & 16) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.Static);
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06004AF8 RID: 19192 RVA: 0x0017D2A4 File Offset: 0x0017D2A4
		// (set) Token: 0x06004AF9 RID: 19193 RVA: 0x0017D2B4 File Offset: 0x0017D2B4
		public bool IsFinal
		{
			get
			{
				return ((ushort)this.attributes & 32) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.Final);
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06004AFA RID: 19194 RVA: 0x0017D2C0 File Offset: 0x0017D2C0
		// (set) Token: 0x06004AFB RID: 19195 RVA: 0x0017D2D0 File Offset: 0x0017D2D0
		public bool IsVirtual
		{
			get
			{
				return ((ushort)this.attributes & 64) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.Virtual);
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06004AFC RID: 19196 RVA: 0x0017D2DC File Offset: 0x0017D2DC
		// (set) Token: 0x06004AFD RID: 19197 RVA: 0x0017D2F0 File Offset: 0x0017D2F0
		public bool IsHideBySig
		{
			get
			{
				return ((ushort)this.attributes & 128) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.HideBySig);
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x0017D300 File Offset: 0x0017D300
		// (set) Token: 0x06004AFF RID: 19199 RVA: 0x0017D314 File Offset: 0x0017D314
		public bool IsNewSlot
		{
			get
			{
				return ((ushort)this.attributes & 256) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.VtableLayoutMask);
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06004B00 RID: 19200 RVA: 0x0017D324 File Offset: 0x0017D324
		// (set) Token: 0x06004B01 RID: 19201 RVA: 0x0017D338 File Offset: 0x0017D338
		public bool IsReuseSlot
		{
			get
			{
				return ((ushort)this.attributes & 256) == 0;
			}
			set
			{
				this.ModifyAttributes(!value, MethodAttributes.VtableLayoutMask);
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06004B02 RID: 19202 RVA: 0x0017D34C File Offset: 0x0017D34C
		// (set) Token: 0x06004B03 RID: 19203 RVA: 0x0017D360 File Offset: 0x0017D360
		public bool IsCheckAccessOnOverride
		{
			get
			{
				return ((ushort)this.attributes & 512) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.CheckAccessOnOverride);
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06004B04 RID: 19204 RVA: 0x0017D370 File Offset: 0x0017D370
		// (set) Token: 0x06004B05 RID: 19205 RVA: 0x0017D384 File Offset: 0x0017D384
		public bool IsAbstract
		{
			get
			{
				return ((ushort)this.attributes & 1024) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.Abstract);
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x0017D394 File Offset: 0x0017D394
		// (set) Token: 0x06004B07 RID: 19207 RVA: 0x0017D3A8 File Offset: 0x0017D3A8
		public bool IsSpecialName
		{
			get
			{
				return ((ushort)this.attributes & 2048) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.SpecialName);
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06004B08 RID: 19208 RVA: 0x0017D3B8 File Offset: 0x0017D3B8
		// (set) Token: 0x06004B09 RID: 19209 RVA: 0x0017D3CC File Offset: 0x0017D3CC
		public bool IsPinvokeImpl
		{
			get
			{
				return ((ushort)this.attributes & 8192) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.PinvokeImpl);
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06004B0A RID: 19210 RVA: 0x0017D3DC File Offset: 0x0017D3DC
		// (set) Token: 0x06004B0B RID: 19211 RVA: 0x0017D3EC File Offset: 0x0017D3EC
		public bool IsUnmanagedExport
		{
			get
			{
				return ((ushort)this.attributes & 8) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.UnmanagedExport);
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06004B0C RID: 19212 RVA: 0x0017D3F8 File Offset: 0x0017D3F8
		// (set) Token: 0x06004B0D RID: 19213 RVA: 0x0017D40C File Offset: 0x0017D40C
		public bool IsRuntimeSpecialName
		{
			get
			{
				return ((ushort)this.attributes & 4096) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.RTSpecialName);
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06004B0E RID: 19214 RVA: 0x0017D41C File Offset: 0x0017D41C
		// (set) Token: 0x06004B0F RID: 19215 RVA: 0x0017D430 File Offset: 0x0017D430
		public bool HasSecurity
		{
			get
			{
				return ((ushort)this.attributes & 16384) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.HasSecurity);
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06004B10 RID: 19216 RVA: 0x0017D440 File Offset: 0x0017D440
		// (set) Token: 0x06004B11 RID: 19217 RVA: 0x0017D454 File Offset: 0x0017D454
		public bool IsRequireSecObject
		{
			get
			{
				return ((ushort)this.attributes & 32768) > 0;
			}
			set
			{
				this.ModifyAttributes(value, MethodAttributes.RequireSecObject);
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06004B12 RID: 19218 RVA: 0x0017D464 File Offset: 0x0017D464
		// (set) Token: 0x06004B13 RID: 19219 RVA: 0x0017D470 File Offset: 0x0017D470
		public MethodImplAttributes CodeType
		{
			get
			{
				return (MethodImplAttributes)this.implAttributes & MethodImplAttributes.CodeTypeMask;
			}
			set
			{
				this.ModifyImplAttributes(~MethodImplAttributes.CodeTypeMask, value & MethodImplAttributes.CodeTypeMask);
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06004B14 RID: 19220 RVA: 0x0017D480 File Offset: 0x0017D480
		public bool IsIL
		{
			get
			{
				return ((ushort)this.implAttributes & 3) == 0;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06004B15 RID: 19221 RVA: 0x0017D490 File Offset: 0x0017D490
		public bool IsNative
		{
			get
			{
				return ((ushort)this.implAttributes & 3) == 1;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x0017D4A0 File Offset: 0x0017D4A0
		public bool IsOPTIL
		{
			get
			{
				return ((ushort)this.implAttributes & 3) == 2;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06004B17 RID: 19223 RVA: 0x0017D4B0 File Offset: 0x0017D4B0
		public bool IsRuntime
		{
			get
			{
				return ((ushort)this.implAttributes & 3) == 3;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x0017D4C0 File Offset: 0x0017D4C0
		// (set) Token: 0x06004B19 RID: 19225 RVA: 0x0017D4D0 File Offset: 0x0017D4D0
		public bool IsUnmanaged
		{
			get
			{
				return ((ushort)this.implAttributes & 4) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.ManagedMask);
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x0017D4DC File Offset: 0x0017D4DC
		// (set) Token: 0x06004B1B RID: 19227 RVA: 0x0017D4EC File Offset: 0x0017D4EC
		public bool IsManaged
		{
			get
			{
				return ((ushort)this.implAttributes & 4) == 0;
			}
			set
			{
				this.ModifyImplAttributes(!value, MethodImplAttributes.ManagedMask);
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06004B1C RID: 19228 RVA: 0x0017D4FC File Offset: 0x0017D4FC
		// (set) Token: 0x06004B1D RID: 19229 RVA: 0x0017D50C File Offset: 0x0017D50C
		public bool IsForwardRef
		{
			get
			{
				return ((ushort)this.implAttributes & 16) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.ForwardRef);
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06004B1E RID: 19230 RVA: 0x0017D518 File Offset: 0x0017D518
		// (set) Token: 0x06004B1F RID: 19231 RVA: 0x0017D52C File Offset: 0x0017D52C
		public bool IsPreserveSig
		{
			get
			{
				return ((ushort)this.implAttributes & 128) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.PreserveSig);
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06004B20 RID: 19232 RVA: 0x0017D53C File Offset: 0x0017D53C
		// (set) Token: 0x06004B21 RID: 19233 RVA: 0x0017D550 File Offset: 0x0017D550
		public bool IsInternalCall
		{
			get
			{
				return ((ushort)this.implAttributes & 4096) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.InternalCall);
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06004B22 RID: 19234 RVA: 0x0017D560 File Offset: 0x0017D560
		// (set) Token: 0x06004B23 RID: 19235 RVA: 0x0017D570 File Offset: 0x0017D570
		public bool IsSynchronized
		{
			get
			{
				return ((ushort)this.implAttributes & 32) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.Synchronized);
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06004B24 RID: 19236 RVA: 0x0017D57C File Offset: 0x0017D57C
		// (set) Token: 0x06004B25 RID: 19237 RVA: 0x0017D58C File Offset: 0x0017D58C
		public bool IsNoInlining
		{
			get
			{
				return ((ushort)this.implAttributes & 8) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.NoInlining);
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06004B26 RID: 19238 RVA: 0x0017D598 File Offset: 0x0017D598
		// (set) Token: 0x06004B27 RID: 19239 RVA: 0x0017D5AC File Offset: 0x0017D5AC
		public bool IsAggressiveInlining
		{
			get
			{
				return ((ushort)this.implAttributes & 256) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.AggressiveInlining);
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06004B28 RID: 19240 RVA: 0x0017D5BC File Offset: 0x0017D5BC
		// (set) Token: 0x06004B29 RID: 19241 RVA: 0x0017D5CC File Offset: 0x0017D5CC
		public bool IsNoOptimization
		{
			get
			{
				return ((ushort)this.implAttributes & 64) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.NoOptimization);
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06004B2A RID: 19242 RVA: 0x0017D5D8 File Offset: 0x0017D5D8
		// (set) Token: 0x06004B2B RID: 19243 RVA: 0x0017D5EC File Offset: 0x0017D5EC
		public bool IsAggressiveOptimization
		{
			get
			{
				return ((ushort)this.implAttributes & 512) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.AggressiveOptimization);
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06004B2C RID: 19244 RVA: 0x0017D5FC File Offset: 0x0017D5FC
		// (set) Token: 0x06004B2D RID: 19245 RVA: 0x0017D610 File Offset: 0x0017D610
		public bool HasSecurityMitigations
		{
			get
			{
				return ((ushort)this.implAttributes & 1024) > 0;
			}
			set
			{
				this.ModifyImplAttributes(value, MethodImplAttributes.SecurityMitigations);
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06004B2E RID: 19246 RVA: 0x0017D620 File Offset: 0x0017D620
		// (set) Token: 0x06004B2F RID: 19247 RVA: 0x0017D630 File Offset: 0x0017D630
		public bool IsSetter
		{
			get
			{
				return (this.SemanticsAttributes & MethodSemanticsAttributes.Setter) > MethodSemanticsAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, MethodSemanticsAttributes.Setter);
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06004B30 RID: 19248 RVA: 0x0017D63C File Offset: 0x0017D63C
		// (set) Token: 0x06004B31 RID: 19249 RVA: 0x0017D64C File Offset: 0x0017D64C
		public bool IsGetter
		{
			get
			{
				return (this.SemanticsAttributes & MethodSemanticsAttributes.Getter) > MethodSemanticsAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, MethodSemanticsAttributes.Getter);
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06004B32 RID: 19250 RVA: 0x0017D658 File Offset: 0x0017D658
		// (set) Token: 0x06004B33 RID: 19251 RVA: 0x0017D668 File Offset: 0x0017D668
		public bool IsOther
		{
			get
			{
				return (this.SemanticsAttributes & MethodSemanticsAttributes.Other) > MethodSemanticsAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, MethodSemanticsAttributes.Other);
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06004B34 RID: 19252 RVA: 0x0017D674 File Offset: 0x0017D674
		// (set) Token: 0x06004B35 RID: 19253 RVA: 0x0017D684 File Offset: 0x0017D684
		public bool IsAddOn
		{
			get
			{
				return (this.SemanticsAttributes & MethodSemanticsAttributes.AddOn) > MethodSemanticsAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, MethodSemanticsAttributes.AddOn);
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06004B36 RID: 19254 RVA: 0x0017D690 File Offset: 0x0017D690
		// (set) Token: 0x06004B37 RID: 19255 RVA: 0x0017D6A0 File Offset: 0x0017D6A0
		public bool IsRemoveOn
		{
			get
			{
				return (this.SemanticsAttributes & MethodSemanticsAttributes.RemoveOn) > MethodSemanticsAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, MethodSemanticsAttributes.RemoveOn);
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06004B38 RID: 19256 RVA: 0x0017D6AC File Offset: 0x0017D6AC
		// (set) Token: 0x06004B39 RID: 19257 RVA: 0x0017D6BC File Offset: 0x0017D6BC
		public bool IsFire
		{
			get
			{
				return (this.SemanticsAttributes & MethodSemanticsAttributes.Fire) > MethodSemanticsAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, MethodSemanticsAttributes.Fire);
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06004B3A RID: 19258 RVA: 0x0017D6C8 File Offset: 0x0017D6C8
		public bool IsStaticConstructor
		{
			get
			{
				return this.IsRuntimeSpecialName && UTF8String.Equals(this.name, MethodDef.StaticConstructorName);
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06004B3B RID: 19259 RVA: 0x0017D6E8 File Offset: 0x0017D6E8
		public bool IsInstanceConstructor
		{
			get
			{
				return this.IsRuntimeSpecialName && UTF8String.Equals(this.name, MethodDef.InstanceConstructorName);
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06004B3C RID: 19260 RVA: 0x0017D708 File Offset: 0x0017D708
		public bool IsConstructor
		{
			get
			{
				return this.IsStaticConstructor || this.IsInstanceConstructor;
			}
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x0017D720 File Offset: 0x0017D720
		void IListListener<GenericParam>.OnLazyAdd(int index, ref GenericParam value)
		{
			this.OnLazyAdd2(index, ref value);
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x0017D72C File Offset: 0x0017D72C
		internal virtual void OnLazyAdd2(int index, ref GenericParam value)
		{
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x0017D730 File Offset: 0x0017D730
		void IListListener<GenericParam>.OnAdd(int index, GenericParam value)
		{
			if (value.Owner != null)
			{
				throw new InvalidOperationException("Generic param is already owned by another type/method. Set Owner to null first.");
			}
			value.Owner = this;
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x0017D750 File Offset: 0x0017D750
		void IListListener<GenericParam>.OnRemove(int index, GenericParam value)
		{
			value.Owner = null;
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x0017D75C File Offset: 0x0017D75C
		void IListListener<GenericParam>.OnResize(int index)
		{
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x0017D760 File Offset: 0x0017D760
		void IListListener<GenericParam>.OnClear()
		{
			foreach (GenericParam genericParam in this.genericParameters.GetEnumerable_NoLock())
			{
				genericParam.Owner = null;
			}
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x0017D7BC File Offset: 0x0017D7BC
		void IListListener<ParamDef>.OnLazyAdd(int index, ref ParamDef value)
		{
			this.OnLazyAdd2(index, ref value);
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x0017D7C8 File Offset: 0x0017D7C8
		internal virtual void OnLazyAdd2(int index, ref ParamDef value)
		{
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x0017D7CC File Offset: 0x0017D7CC
		void IListListener<ParamDef>.OnAdd(int index, ParamDef value)
		{
			if (value.DeclaringMethod != null)
			{
				throw new InvalidOperationException("Param is already owned by another method. Set DeclaringMethod to null first.");
			}
			value.DeclaringMethod = this;
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x0017D7EC File Offset: 0x0017D7EC
		void IListListener<ParamDef>.OnRemove(int index, ParamDef value)
		{
			value.DeclaringMethod = null;
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x0017D7F8 File Offset: 0x0017D7F8
		void IListListener<ParamDef>.OnResize(int index)
		{
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x0017D7FC File Offset: 0x0017D7FC
		void IListListener<ParamDef>.OnClear()
		{
			foreach (ParamDef paramDef in this.paramDefs.GetEnumerable_NoLock())
			{
				paramDef.DeclaringMethod = null;
			}
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x0017D858 File Offset: 0x0017D858
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x0400258A RID: 9610
		internal static readonly UTF8String StaticConstructorName = ".cctor";

		// Token: 0x0400258B RID: 9611
		internal static readonly UTF8String InstanceConstructorName = ".ctor";

		// Token: 0x0400258C RID: 9612
		protected uint rid;

		// Token: 0x0400258D RID: 9613
		private readonly Lock theLock = Lock.Create();

		// Token: 0x0400258E RID: 9614
		protected ParameterList parameterList;

		// Token: 0x0400258F RID: 9615
		protected RVA rva;

		// Token: 0x04002590 RID: 9616
		protected int implAttributes;

		// Token: 0x04002591 RID: 9617
		protected int attributes;

		// Token: 0x04002592 RID: 9618
		protected UTF8String name;

		// Token: 0x04002593 RID: 9619
		protected CallingConventionSig signature;

		// Token: 0x04002594 RID: 9620
		protected LazyList<ParamDef> paramDefs;

		// Token: 0x04002595 RID: 9621
		protected LazyList<GenericParam> genericParameters;

		// Token: 0x04002596 RID: 9622
		protected IList<DeclSecurity> declSecurities;

		// Token: 0x04002597 RID: 9623
		protected ImplMap implMap;

		// Token: 0x04002598 RID: 9624
		protected bool implMap_isInitialized;

		// Token: 0x04002599 RID: 9625
		protected MethodBody methodBody;

		// Token: 0x0400259A RID: 9626
		protected bool methodBody_isInitialized;

		// Token: 0x0400259B RID: 9627
		protected CustomAttributeCollection customAttributes;

		// Token: 0x0400259C RID: 9628
		protected IList<PdbCustomDebugInfo> customDebugInfos;

		// Token: 0x0400259D RID: 9629
		protected IList<MethodOverride> overrides;

		// Token: 0x0400259E RID: 9630
		protected MethodExportInfo exportInfo;

		// Token: 0x0400259F RID: 9631
		protected TypeDef declaringType2;

		// Token: 0x040025A0 RID: 9632
		protected internal static int SEMATTRS_INITD = int.MinValue;

		// Token: 0x040025A1 RID: 9633
		protected internal int semAttrs;
	}
}
