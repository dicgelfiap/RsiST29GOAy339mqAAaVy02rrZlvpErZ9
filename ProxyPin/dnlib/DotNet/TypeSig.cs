using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet
{
	// Token: 0x02000866 RID: 2150
	[ComVisible(true)]
	public abstract class TypeSig : IType, IFullName, IOwnerModule, ICodedToken, IMDTokenProvider, IGenericParameterProvider, IIsTypeOrMethod, IContainsGenericParameter
	{
		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06005265 RID: 21093
		public abstract TypeSig Next { get; }

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06005266 RID: 21094
		public abstract ElementType ElementType { get; }

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06005267 RID: 21095 RVA: 0x00195E3C File Offset: 0x00195E3C
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.TypeSpec, this.rid);
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06005268 RID: 21096 RVA: 0x00195E4C File Offset: 0x00195E4C
		// (set) Token: 0x06005269 RID: 21097 RVA: 0x00195E54 File Offset: 0x00195E54
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

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x0600526A RID: 21098 RVA: 0x00195E60 File Offset: 0x00195E60
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600526B RID: 21099 RVA: 0x00195E64 File Offset: 0x00195E64
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x0600526C RID: 21100 RVA: 0x00195E68 File Offset: 0x00195E68
		int IGenericParameterProvider.NumberOfGenericParameters
		{
			get
			{
				GenericInstSig genericInstSig = this.RemovePinnedAndModifiers() as GenericInstSig;
				if (genericInstSig != null)
				{
					return genericInstSig.GenericArguments.Count;
				}
				return 0;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x0600526D RID: 21101 RVA: 0x00195E98 File Offset: 0x00195E98
		public bool IsValueType
		{
			get
			{
				TypeSig typeSig = this.RemovePinnedAndModifiers();
				if (typeSig == null)
				{
					return false;
				}
				if (typeSig.ElementType == ElementType.GenericInst)
				{
					typeSig = ((GenericInstSig)typeSig).GenericType;
					if (typeSig == null)
					{
						return false;
					}
				}
				return typeSig.ElementType.IsValueType();
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600526E RID: 21102 RVA: 0x00195EE4 File Offset: 0x00195EE4
		public bool IsPrimitive
		{
			get
			{
				return this.ElementType.IsPrimitive();
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600526F RID: 21103 RVA: 0x00195EF4 File Offset: 0x00195EF4
		public string TypeName
		{
			get
			{
				return FullNameFactory.Name(this, false, null);
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06005270 RID: 21104 RVA: 0x00195F00 File Offset: 0x00195F00
		// (set) Token: 0x06005271 RID: 21105 RVA: 0x00195F10 File Offset: 0x00195F10
		UTF8String IFullName.Name
		{
			get
			{
				return new UTF8String(FullNameFactory.Name(this, false, null));
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06005272 RID: 21106 RVA: 0x00195F18 File Offset: 0x00195F18
		public string ReflectionName
		{
			get
			{
				return FullNameFactory.Name(this, true, null);
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06005273 RID: 21107 RVA: 0x00195F24 File Offset: 0x00195F24
		public string Namespace
		{
			get
			{
				return FullNameFactory.Namespace(this, false, null);
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06005274 RID: 21108 RVA: 0x00195F30 File Offset: 0x00195F30
		public string ReflectionNamespace
		{
			get
			{
				return FullNameFactory.Namespace(this, true, null);
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06005275 RID: 21109 RVA: 0x00195F3C File Offset: 0x00195F3C
		public string FullName
		{
			get
			{
				return FullNameFactory.FullName(this, false, null, null, null, null);
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06005276 RID: 21110 RVA: 0x00195F4C File Offset: 0x00195F4C
		public string ReflectionFullName
		{
			get
			{
				return FullNameFactory.FullName(this, true, null, null, null, null);
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06005277 RID: 21111 RVA: 0x00195F5C File Offset: 0x00195F5C
		public string AssemblyQualifiedName
		{
			get
			{
				return FullNameFactory.AssemblyQualifiedName(this, null, null);
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06005278 RID: 21112 RVA: 0x00195F68 File Offset: 0x00195F68
		public IAssembly DefinitionAssembly
		{
			get
			{
				return FullNameFactory.DefinitionAssembly(this);
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06005279 RID: 21113 RVA: 0x00195F70 File Offset: 0x00195F70
		public IScope Scope
		{
			get
			{
				return FullNameFactory.Scope(this);
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x0600527A RID: 21114 RVA: 0x00195F78 File Offset: 0x00195F78
		public ITypeDefOrRef ScopeType
		{
			get
			{
				return FullNameFactory.ScopeType(this);
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x0600527B RID: 21115 RVA: 0x00195F80 File Offset: 0x00195F80
		public ModuleDef Module
		{
			get
			{
				return FullNameFactory.OwnerModule(this);
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x0600527C RID: 21116 RVA: 0x00195F88 File Offset: 0x00195F88
		public bool IsTypeDefOrRef
		{
			get
			{
				return this is TypeDefOrRefSig;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x0600527D RID: 21117 RVA: 0x00195F94 File Offset: 0x00195F94
		public bool IsCorLibType
		{
			get
			{
				return this is CorLibTypeSig;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x00195FA0 File Offset: 0x00195FA0
		public bool IsClassSig
		{
			get
			{
				return this is ClassSig;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600527F RID: 21119 RVA: 0x00195FAC File Offset: 0x00195FAC
		public bool IsValueTypeSig
		{
			get
			{
				return this is ValueTypeSig;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06005280 RID: 21120 RVA: 0x00195FB8 File Offset: 0x00195FB8
		public bool IsGenericParameter
		{
			get
			{
				return this is GenericSig;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06005281 RID: 21121 RVA: 0x00195FC4 File Offset: 0x00195FC4
		public bool IsGenericTypeParameter
		{
			get
			{
				return this is GenericVar;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06005282 RID: 21122 RVA: 0x00195FD0 File Offset: 0x00195FD0
		public bool IsGenericMethodParameter
		{
			get
			{
				return this is GenericMVar;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06005283 RID: 21123 RVA: 0x00195FDC File Offset: 0x00195FDC
		public bool IsSentinel
		{
			get
			{
				return this is SentinelSig;
			}
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06005284 RID: 21124 RVA: 0x00195FE8 File Offset: 0x00195FE8
		public bool IsFunctionPointer
		{
			get
			{
				return this is FnPtrSig;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06005285 RID: 21125 RVA: 0x00195FF4 File Offset: 0x00195FF4
		public bool IsGenericInstanceType
		{
			get
			{
				return this is GenericInstSig;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06005286 RID: 21126 RVA: 0x00196000 File Offset: 0x00196000
		public bool IsPointer
		{
			get
			{
				return this is PtrSig;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06005287 RID: 21127 RVA: 0x0019600C File Offset: 0x0019600C
		public bool IsByRef
		{
			get
			{
				return this is ByRefSig;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06005288 RID: 21128 RVA: 0x00196018 File Offset: 0x00196018
		public bool IsSingleOrMultiDimensionalArray
		{
			get
			{
				return this is ArraySigBase;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06005289 RID: 21129 RVA: 0x00196024 File Offset: 0x00196024
		public bool IsArray
		{
			get
			{
				return this is ArraySig;
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x0600528A RID: 21130 RVA: 0x00196030 File Offset: 0x00196030
		public bool IsSZArray
		{
			get
			{
				return this is SZArraySig;
			}
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x0600528B RID: 21131 RVA: 0x0019603C File Offset: 0x0019603C
		public bool IsModifier
		{
			get
			{
				return this is ModifierSig;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x0600528C RID: 21132 RVA: 0x00196048 File Offset: 0x00196048
		public bool IsRequiredModifier
		{
			get
			{
				return this is CModReqdSig;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x0600528D RID: 21133 RVA: 0x00196054 File Offset: 0x00196054
		public bool IsOptionalModifier
		{
			get
			{
				return this is CModOptSig;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x00196060 File Offset: 0x00196060
		public bool IsPinned
		{
			get
			{
				return this is PinnedSig;
			}
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x0600528F RID: 21135 RVA: 0x0019606C File Offset: 0x0019606C
		public bool IsValueArray
		{
			get
			{
				return this is ValueArraySig;
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06005290 RID: 21136 RVA: 0x00196078 File Offset: 0x00196078
		public bool IsModuleSig
		{
			get
			{
				return this is ModuleSig;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06005291 RID: 21137 RVA: 0x00196084 File Offset: 0x00196084
		public bool ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0019608C File Offset: 0x0019608C
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040027CA RID: 10186
		private uint rid;
	}
}
