using System;
using System.Collections.Generic;

namespace dnlib.DotNet
{
	// Token: 0x0200085E RID: 2142
	internal struct TypeHelper
	{
		// Token: 0x060051D1 RID: 20945 RVA: 0x00194030 File Offset: 0x00194030
		internal static bool ContainsGenericParameter(StandAloneSig ss)
		{
			return ss != null && TypeHelper.ContainsGenericParameter(ss.Signature);
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x00194048 File Offset: 0x00194048
		internal static bool ContainsGenericParameter(InterfaceImpl ii)
		{
			return ii != null && TypeHelper.ContainsGenericParameter(ii.Interface);
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x00194060 File Offset: 0x00194060
		internal static bool ContainsGenericParameter(GenericParamConstraint gpc)
		{
			return gpc != null && TypeHelper.ContainsGenericParameter(gpc.Constraint);
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x00194078 File Offset: 0x00194078
		internal static bool ContainsGenericParameter(MethodSpec ms)
		{
			return ms != null;
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x00194084 File Offset: 0x00194084
		internal static bool ContainsGenericParameter(MemberRef mr)
		{
			if (mr == null)
			{
				return false;
			}
			if (TypeHelper.ContainsGenericParameter(mr.Signature))
			{
				return true;
			}
			IMemberRefParent @class = mr.Class;
			ITypeDefOrRef typeDefOrRef = @class as ITypeDefOrRef;
			if (typeDefOrRef != null)
			{
				return typeDefOrRef.ContainsGenericParameter;
			}
			MethodDef methodDef = @class as MethodDef;
			return methodDef != null && TypeHelper.ContainsGenericParameter(methodDef.Signature);
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x001940E4 File Offset: 0x001940E4
		public static bool ContainsGenericParameter(CallingConventionSig callConv)
		{
			FieldSig fieldSig = callConv as FieldSig;
			if (fieldSig != null)
			{
				return TypeHelper.ContainsGenericParameter(fieldSig);
			}
			MethodBaseSig methodBaseSig = callConv as MethodBaseSig;
			if (methodBaseSig != null)
			{
				return TypeHelper.ContainsGenericParameter(methodBaseSig);
			}
			LocalSig localSig = callConv as LocalSig;
			if (localSig != null)
			{
				return TypeHelper.ContainsGenericParameter(localSig);
			}
			GenericInstMethodSig genericInstMethodSig = callConv as GenericInstMethodSig;
			return genericInstMethodSig != null && TypeHelper.ContainsGenericParameter(genericInstMethodSig);
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x00194148 File Offset: 0x00194148
		public static bool ContainsGenericParameter(FieldSig fieldSig)
		{
			return default(TypeHelper).ContainsGenericParameterInternal(fieldSig);
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x00194168 File Offset: 0x00194168
		public static bool ContainsGenericParameter(MethodBaseSig methodSig)
		{
			return default(TypeHelper).ContainsGenericParameterInternal(methodSig);
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x00194188 File Offset: 0x00194188
		public static bool ContainsGenericParameter(LocalSig localSig)
		{
			return default(TypeHelper).ContainsGenericParameterInternal(localSig);
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x001941A8 File Offset: 0x001941A8
		public static bool ContainsGenericParameter(GenericInstMethodSig gim)
		{
			return default(TypeHelper).ContainsGenericParameterInternal(gim);
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x001941C8 File Offset: 0x001941C8
		public static bool ContainsGenericParameter(IType type)
		{
			TypeDef typeDef = type as TypeDef;
			if (typeDef != null)
			{
				return TypeHelper.ContainsGenericParameter(typeDef);
			}
			TypeRef typeRef = type as TypeRef;
			if (typeRef != null)
			{
				return TypeHelper.ContainsGenericParameter(typeRef);
			}
			TypeSpec typeSpec = type as TypeSpec;
			if (typeSpec != null)
			{
				return TypeHelper.ContainsGenericParameter(typeSpec);
			}
			TypeSig typeSig = type as TypeSig;
			if (typeSig != null)
			{
				return TypeHelper.ContainsGenericParameter(typeSig);
			}
			ExportedType exportedType = type as ExportedType;
			return exportedType != null && TypeHelper.ContainsGenericParameter(exportedType);
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x00194244 File Offset: 0x00194244
		public static bool ContainsGenericParameter(TypeDef type)
		{
			return default(TypeHelper).ContainsGenericParameterInternal(type);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x00194264 File Offset: 0x00194264
		public static bool ContainsGenericParameter(TypeRef type)
		{
			return default(TypeHelper).ContainsGenericParameterInternal(type);
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x00194284 File Offset: 0x00194284
		public static bool ContainsGenericParameter(TypeSpec type)
		{
			return default(TypeHelper).ContainsGenericParameterInternal(type);
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x001942A4 File Offset: 0x001942A4
		public static bool ContainsGenericParameter(TypeSig type)
		{
			return default(TypeHelper).ContainsGenericParameterInternal(type);
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x001942C4 File Offset: 0x001942C4
		public static bool ContainsGenericParameter(ExportedType type)
		{
			return default(TypeHelper).ContainsGenericParameterInternal(type);
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x001942E4 File Offset: 0x001942E4
		private bool ContainsGenericParameterInternal(TypeDef type)
		{
			return false;
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x001942E8 File Offset: 0x001942E8
		private bool ContainsGenericParameterInternal(TypeRef type)
		{
			return false;
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x001942EC File Offset: 0x001942EC
		private bool ContainsGenericParameterInternal(TypeSpec type)
		{
			if (type == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.ContainsGenericParameterInternal(type.TypeSig);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x00194320 File Offset: 0x00194320
		private bool ContainsGenericParameterInternal(ITypeDefOrRef tdr)
		{
			return tdr != null && this.ContainsGenericParameterInternal(tdr as TypeSpec);
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x00194338 File Offset: 0x00194338
		private bool ContainsGenericParameterInternal(TypeSig type)
		{
			if (type == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result;
			switch (type.ElementType)
			{
			case ElementType.Void:
			case ElementType.Boolean:
			case ElementType.Char:
			case ElementType.I1:
			case ElementType.U1:
			case ElementType.I2:
			case ElementType.U2:
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R4:
			case ElementType.R8:
			case ElementType.String:
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case ElementType.I:
			case ElementType.U:
			case ElementType.Object:
				result = this.ContainsGenericParameterInternal((type as TypeDefOrRefSig).TypeDefOrRef);
				goto IL_1F5;
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Array:
			case ElementType.ValueArray:
			case ElementType.SZArray:
			case ElementType.Module:
			case ElementType.Pinned:
				result = this.ContainsGenericParameterInternal((type as NonLeafSig).Next);
				goto IL_1F5;
			case ElementType.Var:
			case ElementType.MVar:
				result = true;
				goto IL_1F5;
			case ElementType.GenericInst:
			{
				GenericInstSig genericInstSig = (GenericInstSig)type;
				result = (this.ContainsGenericParameterInternal(genericInstSig.GenericType) || this.ContainsGenericParameter(genericInstSig.GenericArguments));
				goto IL_1F5;
			}
			case ElementType.FnPtr:
				result = this.ContainsGenericParameterInternal((type as FnPtrSig).Signature);
				goto IL_1F5;
			case ElementType.CModReqd:
			case ElementType.CModOpt:
				result = (this.ContainsGenericParameterInternal((type as ModifierSig).Modifier) || this.ContainsGenericParameterInternal((type as NonLeafSig).Next));
				goto IL_1F5;
			}
			result = false;
			IL_1F5:
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x0019454C File Offset: 0x0019454C
		private bool ContainsGenericParameterInternal(ExportedType type)
		{
			return false;
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x00194550 File Offset: 0x00194550
		private bool ContainsGenericParameterInternal(CallingConventionSig callConv)
		{
			FieldSig fieldSig = callConv as FieldSig;
			if (fieldSig != null)
			{
				return this.ContainsGenericParameterInternal(fieldSig);
			}
			MethodBaseSig methodBaseSig = callConv as MethodBaseSig;
			if (methodBaseSig != null)
			{
				return this.ContainsGenericParameterInternal(methodBaseSig);
			}
			LocalSig localSig = callConv as LocalSig;
			if (localSig != null)
			{
				return this.ContainsGenericParameterInternal(localSig);
			}
			GenericInstMethodSig genericInstMethodSig = callConv as GenericInstMethodSig;
			return genericInstMethodSig != null && this.ContainsGenericParameterInternal(genericInstMethodSig);
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x001945B8 File Offset: 0x001945B8
		private bool ContainsGenericParameterInternal(FieldSig fs)
		{
			if (fs == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.ContainsGenericParameterInternal(fs.Type);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x001945EC File Offset: 0x001945EC
		private bool ContainsGenericParameterInternal(MethodBaseSig mbs)
		{
			if (mbs == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.ContainsGenericParameterInternal(mbs.RetType) || this.ContainsGenericParameter(mbs.Params) || this.ContainsGenericParameter(mbs.ParamsAfterSentinel);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x00194658 File Offset: 0x00194658
		private bool ContainsGenericParameterInternal(LocalSig ls)
		{
			if (ls == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.ContainsGenericParameter(ls.Locals);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x0019468C File Offset: 0x0019468C
		private bool ContainsGenericParameterInternal(GenericInstMethodSig gim)
		{
			if (gim == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = this.ContainsGenericParameter(gim.GenericArguments);
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x001946C0 File Offset: 0x001946C0
		private bool ContainsGenericParameter(IList<TypeSig> types)
		{
			if (types == null)
			{
				return false;
			}
			if (!this.recursionCounter.Increment())
			{
				return false;
			}
			bool result = false;
			int count = types.Count;
			for (int i = 0; i < count; i++)
			{
				if (TypeHelper.ContainsGenericParameter(types[i]))
				{
					result = true;
					break;
				}
			}
			this.recursionCounter.Decrement();
			return result;
		}

		// Token: 0x040027B8 RID: 10168
		private RecursionCounter recursionCounter;
	}
}
