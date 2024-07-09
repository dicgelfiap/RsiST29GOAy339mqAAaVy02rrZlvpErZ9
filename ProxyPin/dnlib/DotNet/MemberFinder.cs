using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet
{
	// Token: 0x02000807 RID: 2055
	[ComVisible(true)]
	public class MemberFinder
	{
		// Token: 0x06004A04 RID: 18948 RVA: 0x0017A91C File Offset: 0x0017A91C
		public MemberFinder FindAll(ModuleDef module)
		{
			this.validModule = module;
			this.objectStack = new Stack<object>(4096);
			this.Add(module);
			this.ProcessAll();
			this.objectStack = null;
			return this;
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x0017A94C File Offset: 0x0017A94C
		private void Push(object mr)
		{
			if (mr == null)
			{
				return;
			}
			this.objectStack.Push(mr);
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x0017A964 File Offset: 0x0017A964
		private void ProcessAll()
		{
			while (this.objectStack.Count > 0)
			{
				object obj = this.objectStack.Pop();
				switch (this.GetObjectType(obj))
				{
				case MemberFinder.ObjectType.Unknown:
					break;
				case MemberFinder.ObjectType.EventDef:
					this.Add((EventDef)obj);
					break;
				case MemberFinder.ObjectType.FieldDef:
					this.Add((FieldDef)obj);
					break;
				case MemberFinder.ObjectType.GenericParam:
					this.Add((GenericParam)obj);
					break;
				case MemberFinder.ObjectType.MemberRef:
					this.Add((MemberRef)obj);
					break;
				case MemberFinder.ObjectType.MethodDef:
					this.Add((MethodDef)obj);
					break;
				case MemberFinder.ObjectType.MethodSpec:
					this.Add((MethodSpec)obj);
					break;
				case MemberFinder.ObjectType.PropertyDef:
					this.Add((PropertyDef)obj);
					break;
				case MemberFinder.ObjectType.TypeDef:
					this.Add((TypeDef)obj);
					break;
				case MemberFinder.ObjectType.TypeRef:
					this.Add((TypeRef)obj);
					break;
				case MemberFinder.ObjectType.TypeSig:
					this.Add((TypeSig)obj);
					break;
				case MemberFinder.ObjectType.TypeSpec:
					this.Add((TypeSpec)obj);
					break;
				case MemberFinder.ObjectType.ExportedType:
					this.Add((ExportedType)obj);
					break;
				default:
					throw new InvalidOperationException(string.Format("Unknown type: {0}", obj.GetType()));
				}
			}
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x0017AAC0 File Offset: 0x0017AAC0
		private MemberFinder.ObjectType GetObjectType(object o)
		{
			if (o == null)
			{
				return MemberFinder.ObjectType.Unknown;
			}
			Type type = o.GetType();
			MemberFinder.ObjectType objectType;
			if (this.toObjectType.TryGetValue(type, out objectType))
			{
				return objectType;
			}
			objectType = MemberFinder.GetObjectType2(o);
			this.toObjectType[type] = objectType;
			return objectType;
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x0017AB0C File Offset: 0x0017AB0C
		private static MemberFinder.ObjectType GetObjectType2(object o)
		{
			if (o is EventDef)
			{
				return MemberFinder.ObjectType.EventDef;
			}
			if (o is FieldDef)
			{
				return MemberFinder.ObjectType.FieldDef;
			}
			if (o is GenericParam)
			{
				return MemberFinder.ObjectType.GenericParam;
			}
			if (o is MemberRef)
			{
				return MemberFinder.ObjectType.MemberRef;
			}
			if (o is MethodDef)
			{
				return MemberFinder.ObjectType.MethodDef;
			}
			if (o is MethodSpec)
			{
				return MemberFinder.ObjectType.MethodSpec;
			}
			if (o is PropertyDef)
			{
				return MemberFinder.ObjectType.PropertyDef;
			}
			if (o is TypeDef)
			{
				return MemberFinder.ObjectType.TypeDef;
			}
			if (o is TypeRef)
			{
				return MemberFinder.ObjectType.TypeRef;
			}
			if (o is TypeSig)
			{
				return MemberFinder.ObjectType.TypeSig;
			}
			if (o is TypeSpec)
			{
				return MemberFinder.ObjectType.TypeSpec;
			}
			if (o is ExportedType)
			{
				return MemberFinder.ObjectType.ExportedType;
			}
			return MemberFinder.ObjectType.Unknown;
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x0017ABC0 File Offset: 0x0017ABC0
		private void Add(ModuleDef mod)
		{
			this.Push(mod.ManagedEntryPoint);
			this.Add(mod.CustomAttributes);
			this.Add(mod.Types);
			this.Add(mod.ExportedTypes);
			if (mod.IsManifestModule)
			{
				this.Add(mod.Assembly);
			}
			this.Add(mod.VTableFixups);
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x0017AC24 File Offset: 0x0017AC24
		private void Add(VTableFixups fixups)
		{
			if (fixups == null)
			{
				return;
			}
			foreach (VTable vtable in fixups)
			{
				foreach (IMethod mr in vtable)
				{
					this.Push(mr);
				}
			}
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x0017ACB4 File Offset: 0x0017ACB4
		private void Add(AssemblyDef asm)
		{
			if (asm == null)
			{
				return;
			}
			this.Add(asm.DeclSecurities);
			this.Add(asm.CustomAttributes);
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x0017ACE4 File Offset: 0x0017ACE4
		private void Add(CallingConventionSig sig)
		{
			if (sig == null)
			{
				return;
			}
			FieldSig fieldSig = sig as FieldSig;
			if (fieldSig != null)
			{
				this.Add(fieldSig);
				return;
			}
			MethodBaseSig methodBaseSig = sig as MethodBaseSig;
			if (methodBaseSig != null)
			{
				this.Add(methodBaseSig);
				return;
			}
			LocalSig localSig = sig as LocalSig;
			if (localSig != null)
			{
				this.Add(localSig);
				return;
			}
			GenericInstMethodSig genericInstMethodSig = sig as GenericInstMethodSig;
			if (genericInstMethodSig != null)
			{
				this.Add(genericInstMethodSig);
				return;
			}
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x0017AD50 File Offset: 0x0017AD50
		private void Add(FieldSig sig)
		{
			if (sig == null)
			{
				return;
			}
			this.Add(sig.Type);
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x0017AD68 File Offset: 0x0017AD68
		private void Add(MethodBaseSig sig)
		{
			if (sig == null)
			{
				return;
			}
			this.Add(sig.RetType);
			this.Add(sig.Params);
			this.Add(sig.ParamsAfterSentinel);
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x0017ADA4 File Offset: 0x0017ADA4
		private void Add(LocalSig sig)
		{
			if (sig == null)
			{
				return;
			}
			this.Add(sig.Locals);
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x0017ADBC File Offset: 0x0017ADBC
		private void Add(GenericInstMethodSig sig)
		{
			if (sig == null)
			{
				return;
			}
			this.Add(sig.GenericArguments);
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x0017ADD4 File Offset: 0x0017ADD4
		private void Add(IEnumerable<CustomAttribute> cas)
		{
			if (cas == null)
			{
				return;
			}
			foreach (CustomAttribute ca in cas)
			{
				this.Add(ca);
			}
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x0017AE2C File Offset: 0x0017AE2C
		private void Add(CustomAttribute ca)
		{
			if (ca == null || this.CustomAttributes.ContainsKey(ca))
			{
				return;
			}
			this.CustomAttributes[ca] = true;
			this.Push(ca.Constructor);
			this.Add(ca.ConstructorArguments);
			this.Add(ca.NamedArguments);
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x0017AE88 File Offset: 0x0017AE88
		private void Add(IEnumerable<CAArgument> args)
		{
			if (args == null)
			{
				return;
			}
			foreach (CAArgument arg in args)
			{
				this.Add(arg);
			}
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x0017AEE0 File Offset: 0x0017AEE0
		private void Add(CAArgument arg)
		{
			this.Add(arg.Type);
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x0017AEF0 File Offset: 0x0017AEF0
		private void Add(IEnumerable<CANamedArgument> args)
		{
			if (args == null)
			{
				return;
			}
			foreach (CANamedArgument arg in args)
			{
				this.Add(arg);
			}
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x0017AF48 File Offset: 0x0017AF48
		private void Add(CANamedArgument arg)
		{
			if (arg == null)
			{
				return;
			}
			this.Add(arg.Type);
			this.Add(arg.Argument);
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x0017AF78 File Offset: 0x0017AF78
		private void Add(IEnumerable<DeclSecurity> decls)
		{
			if (decls == null)
			{
				return;
			}
			foreach (DeclSecurity decl in decls)
			{
				this.Add(decl);
			}
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x0017AFD0 File Offset: 0x0017AFD0
		private void Add(DeclSecurity decl)
		{
			if (decl == null)
			{
				return;
			}
			this.Add(decl.SecurityAttributes);
			this.Add(decl.CustomAttributes);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x0017B000 File Offset: 0x0017B000
		private void Add(IEnumerable<SecurityAttribute> secAttrs)
		{
			if (secAttrs == null)
			{
				return;
			}
			foreach (SecurityAttribute secAttr in secAttrs)
			{
				this.Add(secAttr);
			}
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x0017B058 File Offset: 0x0017B058
		private void Add(SecurityAttribute secAttr)
		{
			if (secAttr == null)
			{
				return;
			}
			this.Add(secAttr.AttributeType);
			this.Add(secAttr.NamedArguments);
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x0017B088 File Offset: 0x0017B088
		private void Add(ITypeDefOrRef tdr)
		{
			TypeDef typeDef = tdr as TypeDef;
			if (typeDef != null)
			{
				this.Add(typeDef);
				return;
			}
			TypeRef typeRef = tdr as TypeRef;
			if (typeRef != null)
			{
				this.Add(typeRef);
				return;
			}
			TypeSpec typeSpec = tdr as TypeSpec;
			if (typeSpec != null)
			{
				this.Add(typeSpec);
				return;
			}
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x0017B0D8 File Offset: 0x0017B0D8
		private void Add(IEnumerable<EventDef> eds)
		{
			if (eds == null)
			{
				return;
			}
			foreach (EventDef ed in eds)
			{
				this.Add(ed);
			}
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x0017B130 File Offset: 0x0017B130
		private void Add(EventDef ed)
		{
			if (ed == null || this.EventDefs.ContainsKey(ed))
			{
				return;
			}
			if (ed.DeclaringType != null && ed.DeclaringType.Module != this.validModule)
			{
				return;
			}
			this.EventDefs[ed] = true;
			this.Push(ed.EventType);
			this.Add(ed.CustomAttributes);
			this.Add(ed.AddMethod);
			this.Add(ed.InvokeMethod);
			this.Add(ed.RemoveMethod);
			this.Add(ed.OtherMethods);
			this.Add(ed.DeclaringType);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x0017B1DC File Offset: 0x0017B1DC
		private void Add(IEnumerable<FieldDef> fds)
		{
			if (fds == null)
			{
				return;
			}
			foreach (FieldDef fd in fds)
			{
				this.Add(fd);
			}
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x0017B234 File Offset: 0x0017B234
		private void Add(FieldDef fd)
		{
			if (fd == null || this.FieldDefs.ContainsKey(fd))
			{
				return;
			}
			if (fd.DeclaringType != null && fd.DeclaringType.Module != this.validModule)
			{
				return;
			}
			this.FieldDefs[fd] = true;
			this.Add(fd.CustomAttributes);
			this.Add(fd.Signature);
			this.Add(fd.DeclaringType);
			this.Add(fd.MarshalType);
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x0017B2BC File Offset: 0x0017B2BC
		private void Add(IEnumerable<GenericParam> gps)
		{
			if (gps == null)
			{
				return;
			}
			foreach (GenericParam gp in gps)
			{
				this.Add(gp);
			}
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x0017B314 File Offset: 0x0017B314
		private void Add(GenericParam gp)
		{
			if (gp == null || this.GenericParams.ContainsKey(gp))
			{
				return;
			}
			this.GenericParams[gp] = true;
			this.Push(gp.Owner);
			this.Push(gp.Kind);
			this.Add(gp.GenericParamConstraints);
			this.Add(gp.CustomAttributes);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x0017B37C File Offset: 0x0017B37C
		private void Add(IEnumerable<GenericParamConstraint> gpcs)
		{
			if (gpcs == null)
			{
				return;
			}
			foreach (GenericParamConstraint gpc in gpcs)
			{
				this.Add(gpc);
			}
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x0017B3D4 File Offset: 0x0017B3D4
		private void Add(GenericParamConstraint gpc)
		{
			if (gpc == null)
			{
				return;
			}
			this.Add(gpc.Owner);
			this.Push(gpc.Constraint);
			this.Add(gpc.CustomAttributes);
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x0017B410 File Offset: 0x0017B410
		private void Add(MemberRef mr)
		{
			if (mr == null || this.MemberRefs.ContainsKey(mr))
			{
				return;
			}
			if (mr.Module != this.validModule)
			{
				return;
			}
			this.MemberRefs[mr] = true;
			this.Push(mr.Class);
			this.Add(mr.Signature);
			this.Add(mr.CustomAttributes);
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x0017B47C File Offset: 0x0017B47C
		private void Add(IEnumerable<MethodDef> methods)
		{
			if (methods == null)
			{
				return;
			}
			foreach (MethodDef md in methods)
			{
				this.Add(md);
			}
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x0017B4D4 File Offset: 0x0017B4D4
		private void Add(MethodDef md)
		{
			if (md == null || this.MethodDefs.ContainsKey(md))
			{
				return;
			}
			if (md.DeclaringType != null && md.DeclaringType.Module != this.validModule)
			{
				return;
			}
			this.MethodDefs[md] = true;
			this.Add(md.Signature);
			this.Add(md.ParamDefs);
			this.Add(md.GenericParameters);
			this.Add(md.DeclSecurities);
			this.Add(md.MethodBody);
			this.Add(md.CustomAttributes);
			this.Add(md.Overrides);
			this.Add(md.DeclaringType);
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x0017B58C File Offset: 0x0017B58C
		private void Add(MethodBody mb)
		{
			CilBody cilBody = mb as CilBody;
			if (cilBody != null)
			{
				this.Add(cilBody);
			}
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x0017B5B4 File Offset: 0x0017B5B4
		private void Add(CilBody cb)
		{
			if (cb == null)
			{
				return;
			}
			this.Add(cb.Instructions);
			this.Add(cb.ExceptionHandlers);
			this.Add(cb.Variables);
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x0017B5F0 File Offset: 0x0017B5F0
		private void Add(IEnumerable<Instruction> instrs)
		{
			if (instrs == null)
			{
				return;
			}
			foreach (Instruction instruction in instrs)
			{
				if (instruction != null)
				{
					OperandType operandType = instruction.OpCode.OperandType;
					if (operandType <= OperandType.InlineMethod)
					{
						if (operandType != OperandType.InlineField && operandType != OperandType.InlineMethod)
						{
							continue;
						}
					}
					else
					{
						switch (operandType)
						{
						case OperandType.InlineSig:
							this.Add(instruction.Operand as CallingConventionSig);
							continue;
						case OperandType.InlineString:
						case OperandType.InlineSwitch:
							continue;
						case OperandType.InlineTok:
						case OperandType.InlineType:
							goto IL_74;
						case OperandType.InlineVar:
							break;
						default:
							if (operandType != OperandType.ShortInlineVar)
							{
								continue;
							}
							break;
						}
						Local local = instruction.Operand as Local;
						if (local != null)
						{
							this.Add(local);
							continue;
						}
						Parameter parameter = instruction.Operand as Parameter;
						if (parameter != null)
						{
							this.Add(parameter);
							continue;
						}
						continue;
					}
					IL_74:
					this.Push(instruction.Operand);
				}
			}
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x0017B700 File Offset: 0x0017B700
		private void Add(IEnumerable<ExceptionHandler> ehs)
		{
			if (ehs == null)
			{
				return;
			}
			foreach (ExceptionHandler exceptionHandler in ehs)
			{
				this.Push(exceptionHandler.CatchType);
			}
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x0017B760 File Offset: 0x0017B760
		private void Add(IEnumerable<Local> locals)
		{
			if (locals == null)
			{
				return;
			}
			foreach (Local local in locals)
			{
				this.Add(local);
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0017B7B8 File Offset: 0x0017B7B8
		private void Add(Local local)
		{
			if (local == null)
			{
				return;
			}
			this.Add(local.Type);
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x0017B7D0 File Offset: 0x0017B7D0
		private void Add(IEnumerable<Parameter> ps)
		{
			if (ps == null)
			{
				return;
			}
			foreach (Parameter param in ps)
			{
				this.Add(param);
			}
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x0017B828 File Offset: 0x0017B828
		private void Add(Parameter param)
		{
			if (param == null)
			{
				return;
			}
			this.Add(param.Type);
			this.Add(param.Method);
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x0017B858 File Offset: 0x0017B858
		private void Add(IEnumerable<ParamDef> pds)
		{
			if (pds == null)
			{
				return;
			}
			foreach (ParamDef pd in pds)
			{
				this.Add(pd);
			}
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x0017B8B0 File Offset: 0x0017B8B0
		private void Add(ParamDef pd)
		{
			if (pd == null)
			{
				return;
			}
			this.Add(pd.DeclaringMethod);
			this.Add(pd.CustomAttributes);
			this.Add(pd.MarshalType);
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x0017B8EC File Offset: 0x0017B8EC
		private void Add(MarshalType mt)
		{
			if (mt == null)
			{
				return;
			}
			NativeType nativeType = mt.NativeType;
			if (nativeType == NativeType.SafeArray)
			{
				this.Add(((SafeArrayMarshalType)mt).UserDefinedSubType);
				return;
			}
			if (nativeType != NativeType.CustomMarshaler)
			{
				return;
			}
			this.Add(((CustomMarshalType)mt).CustomMarshaler);
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x0017B940 File Offset: 0x0017B940
		private void Add(IEnumerable<MethodOverride> mos)
		{
			if (mos == null)
			{
				return;
			}
			foreach (MethodOverride mo in mos)
			{
				this.Add(mo);
			}
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x0017B998 File Offset: 0x0017B998
		private void Add(MethodOverride mo)
		{
			this.Push(mo.MethodBody);
			this.Push(mo.MethodDeclaration);
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x0017B9B4 File Offset: 0x0017B9B4
		private void Add(MethodSpec ms)
		{
			if (ms == null || this.MethodSpecs.ContainsKey(ms))
			{
				return;
			}
			if (ms.Method != null && ms.Method.DeclaringType != null && ms.Method.DeclaringType.Module != this.validModule)
			{
				return;
			}
			this.MethodSpecs[ms] = true;
			this.Push(ms.Method);
			this.Add(ms.Instantiation);
			this.Add(ms.CustomAttributes);
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x0017BA48 File Offset: 0x0017BA48
		private void Add(IEnumerable<PropertyDef> pds)
		{
			if (pds == null)
			{
				return;
			}
			foreach (PropertyDef pd in pds)
			{
				this.Add(pd);
			}
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x0017BAA0 File Offset: 0x0017BAA0
		private void Add(PropertyDef pd)
		{
			if (pd == null || this.PropertyDefs.ContainsKey(pd))
			{
				return;
			}
			if (pd.DeclaringType != null && pd.DeclaringType.Module != this.validModule)
			{
				return;
			}
			this.PropertyDefs[pd] = true;
			this.Add(pd.Type);
			this.Add(pd.CustomAttributes);
			this.Add(pd.GetMethods);
			this.Add(pd.SetMethods);
			this.Add(pd.OtherMethods);
			this.Add(pd.DeclaringType);
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x0017BB40 File Offset: 0x0017BB40
		private void Add(IEnumerable<TypeDef> tds)
		{
			if (tds == null)
			{
				return;
			}
			foreach (TypeDef td in tds)
			{
				this.Add(td);
			}
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x0017BB98 File Offset: 0x0017BB98
		private void Add(TypeDef td)
		{
			if (td == null || this.TypeDefs.ContainsKey(td))
			{
				return;
			}
			if (td.Module != this.validModule)
			{
				return;
			}
			this.TypeDefs[td] = true;
			this.Push(td.BaseType);
			this.Add(td.Fields);
			this.Add(td.Methods);
			this.Add(td.GenericParameters);
			this.Add(td.Interfaces);
			this.Add(td.DeclSecurities);
			this.Add(td.DeclaringType);
			this.Add(td.Events);
			this.Add(td.Properties);
			this.Add(td.NestedTypes);
			this.Add(td.CustomAttributes);
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x0017BC64 File Offset: 0x0017BC64
		private void Add(IEnumerable<InterfaceImpl> iis)
		{
			if (iis == null)
			{
				return;
			}
			foreach (InterfaceImpl ii in iis)
			{
				this.Add(ii);
			}
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x0017BCBC File Offset: 0x0017BCBC
		private void Add(InterfaceImpl ii)
		{
			if (ii == null)
			{
				return;
			}
			this.Push(ii.Interface);
			this.Add(ii.CustomAttributes);
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x0017BCEC File Offset: 0x0017BCEC
		private void Add(TypeRef tr)
		{
			if (tr == null || this.TypeRefs.ContainsKey(tr))
			{
				return;
			}
			if (tr.Module != this.validModule)
			{
				return;
			}
			this.TypeRefs[tr] = true;
			this.Push(tr.ResolutionScope);
			this.Add(tr.CustomAttributes);
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x0017BD4C File Offset: 0x0017BD4C
		private void Add(IEnumerable<TypeSig> tss)
		{
			if (tss == null)
			{
				return;
			}
			foreach (TypeSig ts in tss)
			{
				this.Add(ts);
			}
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x0017BDA4 File Offset: 0x0017BDA4
		private void Add(TypeSig ts)
		{
			if (ts == null || this.TypeSigs.ContainsKey(ts))
			{
				return;
			}
			if (ts.Module != this.validModule)
			{
				return;
			}
			this.TypeSigs[ts] = true;
			while (ts != null)
			{
				switch (ts.ElementType)
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
				{
					TypeDefOrRefSig typeDefOrRefSig = (TypeDefOrRefSig)ts;
					this.Push(typeDefOrRefSig.TypeDefOrRef);
					break;
				}
				case ElementType.GenericInst:
				{
					GenericInstSig genericInstSig = (GenericInstSig)ts;
					this.Add(genericInstSig.GenericType);
					this.Add(genericInstSig.GenericArguments);
					break;
				}
				case ElementType.FnPtr:
				{
					FnPtrSig fnPtrSig = (FnPtrSig)ts;
					this.Add(fnPtrSig.Signature);
					break;
				}
				case ElementType.CModReqd:
				case ElementType.CModOpt:
				{
					ModifierSig modifierSig = (ModifierSig)ts;
					this.Push(modifierSig.Modifier);
					break;
				}
				}
				ts = ts.Next;
			}
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x0017BF94 File Offset: 0x0017BF94
		private void Add(TypeSpec ts)
		{
			if (ts == null || this.TypeSpecs.ContainsKey(ts))
			{
				return;
			}
			if (ts.Module != this.validModule)
			{
				return;
			}
			this.TypeSpecs[ts] = true;
			this.Add(ts.TypeSig);
			this.Add(ts.CustomAttributes);
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x0017BFF4 File Offset: 0x0017BFF4
		private void Add(IEnumerable<ExportedType> ets)
		{
			if (ets == null)
			{
				return;
			}
			foreach (ExportedType et in ets)
			{
				this.Add(et);
			}
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x0017C04C File Offset: 0x0017C04C
		private void Add(ExportedType et)
		{
			if (et == null || this.ExportedTypes.ContainsKey(et))
			{
				return;
			}
			if (et.Module != this.validModule)
			{
				return;
			}
			this.ExportedTypes[et] = true;
			this.Add(et.CustomAttributes);
			this.Push(et.Implementation);
		}

		// Token: 0x04002557 RID: 9559
		public readonly Dictionary<CustomAttribute, bool> CustomAttributes = new Dictionary<CustomAttribute, bool>();

		// Token: 0x04002558 RID: 9560
		public readonly Dictionary<EventDef, bool> EventDefs = new Dictionary<EventDef, bool>();

		// Token: 0x04002559 RID: 9561
		public readonly Dictionary<FieldDef, bool> FieldDefs = new Dictionary<FieldDef, bool>();

		// Token: 0x0400255A RID: 9562
		public readonly Dictionary<GenericParam, bool> GenericParams = new Dictionary<GenericParam, bool>();

		// Token: 0x0400255B RID: 9563
		public readonly Dictionary<MemberRef, bool> MemberRefs = new Dictionary<MemberRef, bool>();

		// Token: 0x0400255C RID: 9564
		public readonly Dictionary<MethodDef, bool> MethodDefs = new Dictionary<MethodDef, bool>();

		// Token: 0x0400255D RID: 9565
		public readonly Dictionary<MethodSpec, bool> MethodSpecs = new Dictionary<MethodSpec, bool>();

		// Token: 0x0400255E RID: 9566
		public readonly Dictionary<PropertyDef, bool> PropertyDefs = new Dictionary<PropertyDef, bool>();

		// Token: 0x0400255F RID: 9567
		public readonly Dictionary<TypeDef, bool> TypeDefs = new Dictionary<TypeDef, bool>();

		// Token: 0x04002560 RID: 9568
		public readonly Dictionary<TypeRef, bool> TypeRefs = new Dictionary<TypeRef, bool>();

		// Token: 0x04002561 RID: 9569
		public readonly Dictionary<TypeSig, bool> TypeSigs = new Dictionary<TypeSig, bool>();

		// Token: 0x04002562 RID: 9570
		public readonly Dictionary<TypeSpec, bool> TypeSpecs = new Dictionary<TypeSpec, bool>();

		// Token: 0x04002563 RID: 9571
		public readonly Dictionary<ExportedType, bool> ExportedTypes = new Dictionary<ExportedType, bool>();

		// Token: 0x04002564 RID: 9572
		private Stack<object> objectStack;

		// Token: 0x04002565 RID: 9573
		private ModuleDef validModule;

		// Token: 0x04002566 RID: 9574
		private readonly Dictionary<Type, MemberFinder.ObjectType> toObjectType = new Dictionary<Type, MemberFinder.ObjectType>();

		// Token: 0x02000FE8 RID: 4072
		private enum ObjectType
		{
			// Token: 0x040043CB RID: 17355
			Unknown,
			// Token: 0x040043CC RID: 17356
			EventDef,
			// Token: 0x040043CD RID: 17357
			FieldDef,
			// Token: 0x040043CE RID: 17358
			GenericParam,
			// Token: 0x040043CF RID: 17359
			MemberRef,
			// Token: 0x040043D0 RID: 17360
			MethodDef,
			// Token: 0x040043D1 RID: 17361
			MethodSpec,
			// Token: 0x040043D2 RID: 17362
			PropertyDef,
			// Token: 0x040043D3 RID: 17363
			TypeDef,
			// Token: 0x040043D4 RID: 17364
			TypeRef,
			// Token: 0x040043D5 RID: 17365
			TypeSig,
			// Token: 0x040043D6 RID: 17366
			TypeSpec,
			// Token: 0x040043D7 RID: 17367
			ExportedType
		}
	}
}
