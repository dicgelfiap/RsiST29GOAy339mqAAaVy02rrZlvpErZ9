using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.PE;
using dnlib.Threading;
using dnlib.W32Resources;

namespace dnlib.DotNet
{
	// Token: 0x02000821 RID: 2081
	internal readonly struct ModuleLoader
	{
		// Token: 0x06004D3D RID: 19773 RVA: 0x00182F80 File Offset: 0x00182F80
		private ModuleLoader(ModuleDef module, ICancellationToken cancellationToken)
		{
			this.module = module;
			this.cancellationToken = cancellationToken;
			this.seen = new Dictionary<object, bool>(16384);
			this.stack = new Stack<object>(16384);
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x00182FB0 File Offset: 0x00182FB0
		public static void LoadAll(ModuleDef module, ICancellationToken cancellationToken)
		{
			new ModuleLoader(module, cancellationToken).Load();
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x00182FD0 File Offset: 0x00182FD0
		private void Add(UTF8String a)
		{
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x00182FD4 File Offset: 0x00182FD4
		private void Add(Guid? a)
		{
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x00182FD8 File Offset: 0x00182FD8
		private void Add(ushort a)
		{
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x00182FDC File Offset: 0x00182FDC
		private void Add(AssemblyHashAlgorithm a)
		{
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x00182FE0 File Offset: 0x00182FE0
		private void Add(Version a)
		{
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x00182FE4 File Offset: 0x00182FE4
		private void Add(AssemblyAttributes a)
		{
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x00182FE8 File Offset: 0x00182FE8
		private void Add(PublicKeyBase a)
		{
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x00182FEC File Offset: 0x00182FEC
		private void Add(RVA a)
		{
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x00182FF0 File Offset: 0x00182FF0
		private void Add(IManagedEntryPoint a)
		{
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x00182FF4 File Offset: 0x00182FF4
		private void Add(string a)
		{
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x00182FF8 File Offset: 0x00182FF8
		private void Add(WinMDStatus a)
		{
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x00182FFC File Offset: 0x00182FFC
		private void Add(TypeAttributes a)
		{
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x00183000 File Offset: 0x00183000
		private void Add(FieldAttributes a)
		{
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x00183004 File Offset: 0x00183004
		private void Add(uint? a)
		{
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x00183008 File Offset: 0x00183008
		private void Add(byte[] a)
		{
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0018300C File Offset: 0x0018300C
		private void Add(MethodImplAttributes a)
		{
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x00183010 File Offset: 0x00183010
		private void Add(MethodAttributes a)
		{
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x00183014 File Offset: 0x00183014
		private void Add(MethodSemanticsAttributes a)
		{
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x00183018 File Offset: 0x00183018
		private void Add(ParamAttributes a)
		{
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0018301C File Offset: 0x0018301C
		private void Add(ElementType a)
		{
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x00183020 File Offset: 0x00183020
		private void Add(SecurityAction a)
		{
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x00183024 File Offset: 0x00183024
		private void Add(EventAttributes a)
		{
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x00183028 File Offset: 0x00183028
		private void Add(PropertyAttributes a)
		{
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x0018302C File Offset: 0x0018302C
		private void Add(PInvokeAttributes a)
		{
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x00183030 File Offset: 0x00183030
		private void Add(FileAttributes a)
		{
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x00183034 File Offset: 0x00183034
		private void Add(ManifestResourceAttributes a)
		{
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x00183038 File Offset: 0x00183038
		private void Add(GenericParamAttributes a)
		{
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x0018303C File Offset: 0x0018303C
		private void Add(NativeType a)
		{
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x00183040 File Offset: 0x00183040
		private void Load()
		{
			this.LoadAllTables();
			this.Load(this.module);
			this.Process();
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0018305C File Offset: 0x0018305C
		private void Process()
		{
			while (this.stack.Count != 0)
			{
				if (this.cancellationToken != null)
				{
					this.cancellationToken.ThrowIfCancellationRequested();
				}
				object o = this.stack.Pop();
				this.LoadObj(o);
			}
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x001830A8 File Offset: 0x001830A8
		private void LoadAllTables()
		{
			ITokenResolver tokenResolver = this.module;
			if (tokenResolver == null)
			{
				return;
			}
			for (Table table = Table.Module; table <= Table.GenericParamConstraint; table += 1)
			{
				uint num = 1U;
				for (;;)
				{
					IMDTokenProvider imdtokenProvider = tokenResolver.ResolveToken(new MDToken(table, num).Raw, default(GenericParamContext));
					if (imdtokenProvider == null)
					{
						break;
					}
					this.Add(imdtokenProvider);
					this.Process();
					num += 1U;
				}
			}
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x00183118 File Offset: 0x00183118
		private void LoadObj(object o)
		{
			TypeSig typeSig = o as TypeSig;
			if (typeSig != null)
			{
				this.Load(typeSig);
				return;
			}
			IMDTokenProvider imdtokenProvider = o as IMDTokenProvider;
			if (imdtokenProvider != null)
			{
				this.Load(imdtokenProvider);
				return;
			}
			CustomAttribute customAttribute = o as CustomAttribute;
			if (customAttribute != null)
			{
				this.Load(customAttribute);
				return;
			}
			SecurityAttribute securityAttribute = o as SecurityAttribute;
			if (securityAttribute != null)
			{
				this.Load(securityAttribute);
				return;
			}
			CANamedArgument canamedArgument = o as CANamedArgument;
			if (canamedArgument != null)
			{
				this.Load(canamedArgument);
				return;
			}
			Parameter parameter = o as Parameter;
			if (parameter != null)
			{
				this.Load(parameter);
				return;
			}
			PdbMethod pdbMethod = o as PdbMethod;
			if (pdbMethod != null)
			{
				this.Load(pdbMethod);
				return;
			}
			ResourceDirectory resourceDirectory = o as ResourceDirectory;
			if (resourceDirectory != null)
			{
				this.Load(resourceDirectory);
				return;
			}
			ResourceData resourceData = o as ResourceData;
			if (resourceData != null)
			{
				this.Load(resourceData);
				return;
			}
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x001831F8 File Offset: 0x001831F8
		private void Load(TypeSig ts)
		{
			if (ts == null)
			{
				return;
			}
			this.Add(ts.Next);
			switch (ts.ElementType)
			{
			case ElementType.End:
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Array:
			case ElementType.ValueArray:
			case ElementType.R:
			case ElementType.SZArray:
			case ElementType.Internal:
			case (ElementType)34:
			case (ElementType)35:
			case (ElementType)36:
			case (ElementType)37:
			case (ElementType)38:
			case (ElementType)39:
			case (ElementType)40:
			case (ElementType)41:
			case (ElementType)42:
			case (ElementType)43:
			case (ElementType)44:
			case (ElementType)45:
			case (ElementType)46:
			case (ElementType)47:
			case (ElementType)48:
			case (ElementType)49:
			case (ElementType)50:
			case (ElementType)51:
			case (ElementType)52:
			case (ElementType)53:
			case (ElementType)54:
			case (ElementType)55:
			case (ElementType)56:
			case (ElementType)57:
			case (ElementType)58:
			case (ElementType)59:
			case (ElementType)60:
			case (ElementType)61:
			case (ElementType)62:
			case ElementType.Module:
			case (ElementType)64:
			case ElementType.Sentinel:
			case (ElementType)66:
			case (ElementType)67:
			case (ElementType)68:
			case ElementType.Pinned:
				break;
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
				this.Add(((TypeDefOrRefSig)ts).TypeDefOrRef);
				return;
			case ElementType.Var:
			case ElementType.MVar:
			{
				GenericSig genericSig = (GenericSig)ts;
				this.Add(genericSig.OwnerType);
				this.Add(genericSig.OwnerMethod);
				return;
			}
			case ElementType.GenericInst:
			{
				GenericInstSig genericInstSig = (GenericInstSig)ts;
				this.Add(genericInstSig.GenericType);
				this.Add(genericInstSig.GenericArguments);
				return;
			}
			case ElementType.FnPtr:
			{
				FnPtrSig fnPtrSig = (FnPtrSig)ts;
				this.Add(fnPtrSig.Signature);
				return;
			}
			case ElementType.CModReqd:
			case ElementType.CModOpt:
			{
				ModifierSig modifierSig = (ModifierSig)ts;
				this.Add(modifierSig.Modifier);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x001833C0 File Offset: 0x001833C0
		private void Load(IMDTokenProvider mdt)
		{
			if (mdt == null)
			{
				return;
			}
			switch (mdt.MDToken.Table)
			{
			case Table.Module:
				this.Load((ModuleDef)mdt);
				return;
			case Table.TypeRef:
				this.Load((TypeRef)mdt);
				return;
			case Table.TypeDef:
				this.Load((TypeDef)mdt);
				return;
			case Table.FieldPtr:
			case Table.MethodPtr:
			case Table.ParamPtr:
			case Table.CustomAttribute:
			case Table.FieldMarshal:
			case Table.FieldLayout:
			case Table.EventMap:
			case Table.EventPtr:
			case Table.PropertyMap:
			case Table.PropertyPtr:
			case Table.MethodSemantics:
			case Table.MethodImpl:
			case Table.FieldRVA:
			case Table.ENCLog:
			case Table.ENCMap:
			case Table.AssemblyProcessor:
			case Table.AssemblyOS:
			case Table.AssemblyRefProcessor:
			case Table.AssemblyRefOS:
			case Table.NestedClass:
			case (Table)45:
			case (Table)46:
			case (Table)47:
			case Table.Document:
			case Table.MethodDebugInformation:
			case Table.LocalScope:
			case Table.LocalVariable:
			case Table.LocalConstant:
			case Table.ImportScope:
			case Table.StateMachineMethod:
			case Table.CustomDebugInformation:
				break;
			case Table.Field:
				this.Load((FieldDef)mdt);
				return;
			case Table.Method:
				this.Load((MethodDef)mdt);
				return;
			case Table.Param:
				this.Load((ParamDef)mdt);
				return;
			case Table.InterfaceImpl:
				this.Load((InterfaceImpl)mdt);
				return;
			case Table.MemberRef:
				this.Load((MemberRef)mdt);
				return;
			case Table.Constant:
				this.Load((Constant)mdt);
				return;
			case Table.DeclSecurity:
				this.Load((DeclSecurity)mdt);
				return;
			case Table.ClassLayout:
				this.Load((ClassLayout)mdt);
				return;
			case Table.StandAloneSig:
				this.Load((StandAloneSig)mdt);
				return;
			case Table.Event:
				this.Load((EventDef)mdt);
				return;
			case Table.Property:
				this.Load((PropertyDef)mdt);
				return;
			case Table.ModuleRef:
				this.Load((ModuleRef)mdt);
				return;
			case Table.TypeSpec:
				this.Load((TypeSpec)mdt);
				return;
			case Table.ImplMap:
				this.Load((ImplMap)mdt);
				return;
			case Table.Assembly:
				this.Load((AssemblyDef)mdt);
				return;
			case Table.AssemblyRef:
				this.Load((AssemblyRef)mdt);
				return;
			case Table.File:
				this.Load((FileDef)mdt);
				return;
			case Table.ExportedType:
				this.Load((ExportedType)mdt);
				return;
			case Table.ManifestResource:
			{
				Resource resource = mdt as Resource;
				if (resource != null)
				{
					this.Load(resource);
					return;
				}
				ManifestResource manifestResource = mdt as ManifestResource;
				if (manifestResource != null)
				{
					this.Load(manifestResource);
				}
				break;
			}
			case Table.GenericParam:
				this.Load((GenericParam)mdt);
				return;
			case Table.MethodSpec:
				this.Load((MethodSpec)mdt);
				return;
			case Table.GenericParamConstraint:
				this.Load((GenericParamConstraint)mdt);
				return;
			default:
				return;
			}
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x00183630 File Offset: 0x00183630
		private void Load(ModuleDef obj)
		{
			if (obj == null || obj != this.module)
			{
				return;
			}
			this.Add(obj.Generation);
			this.Add(obj.Name);
			this.Add(obj.Mvid);
			this.Add(obj.EncId);
			this.Add(obj.EncBaseId);
			this.Add(obj.CustomAttributes);
			this.Add(obj.Assembly);
			this.Add<TypeDef>(obj.Types);
			this.Add<ExportedType>(obj.ExportedTypes);
			this.Add(obj.NativeEntryPoint);
			this.Add(obj.ManagedEntryPoint);
			this.Add<Resource>(obj.Resources);
			this.Add(obj.VTableFixups);
			this.Add(obj.Location);
			this.Add(obj.Win32Resources);
			this.Add(obj.RuntimeVersion);
			this.Add(obj.WinMDStatus);
			this.Add(obj.RuntimeVersionWinMD);
			this.Add(obj.WinMDVersion);
			this.Add(obj.PdbState);
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x00183744 File Offset: 0x00183744
		private void Load(TypeRef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.ResolutionScope);
			this.Add(obj.Name);
			this.Add(obj.Namespace);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x0018378C File Offset: 0x0018378C
		private void Load(TypeDef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Module2);
			this.Add(obj.Attributes);
			this.Add(obj.Name);
			this.Add(obj.Namespace);
			this.Add(obj.BaseType);
			this.Add<FieldDef>(obj.Fields);
			this.Add<MethodDef>(obj.Methods);
			this.Add<GenericParam>(obj.GenericParameters);
			this.Add<InterfaceImpl>(obj.Interfaces);
			this.Add<DeclSecurity>(obj.DeclSecurities);
			this.Add(obj.ClassLayout);
			this.Add(obj.DeclaringType);
			this.Add(obj.DeclaringType2);
			this.Add<TypeDef>(obj.NestedTypes);
			this.Add<EventDef>(obj.Events);
			this.Add<PropertyDef>(obj.Properties);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x00183870 File Offset: 0x00183870
		private void Load(FieldDef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.CustomAttributes);
			this.Add(obj.Attributes);
			this.Add(obj.Name);
			this.Add(obj.Signature);
			this.Add(obj.FieldOffset);
			this.Add(obj.MarshalType);
			this.Add(obj.RVA);
			this.Add(obj.InitialValue);
			this.Add(obj.ImplMap);
			this.Add(obj.Constant);
			this.Add(obj.DeclaringType);
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x0018390C File Offset: 0x0018390C
		private void Load(MethodDef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.RVA);
			this.Add(obj.ImplAttributes);
			this.Add(obj.Attributes);
			this.Add(obj.Name);
			this.Add(obj.Signature);
			this.Add<ParamDef>(obj.ParamDefs);
			this.Add<GenericParam>(obj.GenericParameters);
			this.Add<DeclSecurity>(obj.DeclSecurities);
			this.Add(obj.ImplMap);
			this.Add(obj.MethodBody);
			this.Add(obj.CustomAttributes);
			this.Add(obj.Overrides);
			this.Add(obj.DeclaringType);
			this.Add(obj.Parameters);
			this.Add(obj.SemanticsAttributes);
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x001839D8 File Offset: 0x001839D8
		private void Load(ParamDef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.DeclaringMethod);
			this.Add(obj.Attributes);
			this.Add(obj.Sequence);
			this.Add(obj.Name);
			this.Add(obj.MarshalType);
			this.Add(obj.Constant);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x00183A44 File Offset: 0x00183A44
		private void Load(InterfaceImpl obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Interface);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x00183A74 File Offset: 0x00183A74
		private void Load(MemberRef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Class);
			this.Add(obj.Name);
			this.Add(obj.Signature);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x00183ABC File Offset: 0x00183ABC
		private void Load(Constant obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Type);
			object value = obj.Value;
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x00183AD8 File Offset: 0x00183AD8
		private void Load(DeclSecurity obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Action);
			this.Add(obj.SecurityAttributes);
			this.Add(obj.CustomAttributes);
			obj.GetBlob();
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x00183B1C File Offset: 0x00183B1C
		private void Load(ClassLayout obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.PackingSize);
			this.Add(new uint?(obj.ClassSize));
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x00183B54 File Offset: 0x00183B54
		private void Load(StandAloneSig obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Signature);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x00183B84 File Offset: 0x00183B84
		private void Load(EventDef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Attributes);
			this.Add(obj.Name);
			this.Add(obj.EventType);
			this.Add(obj.CustomAttributes);
			this.Add(obj.AddMethod);
			this.Add(obj.InvokeMethod);
			this.Add(obj.RemoveMethod);
			this.Add<MethodDef>(obj.OtherMethods);
			this.Add(obj.DeclaringType);
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x00183C08 File Offset: 0x00183C08
		private void Load(PropertyDef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Attributes);
			this.Add(obj.Name);
			this.Add(obj.Type);
			this.Add(obj.Constant);
			this.Add(obj.CustomAttributes);
			this.Add<MethodDef>(obj.GetMethods);
			this.Add<MethodDef>(obj.SetMethods);
			this.Add<MethodDef>(obj.OtherMethods);
			this.Add(obj.DeclaringType);
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x00183C8C File Offset: 0x00183C8C
		private void Load(ModuleRef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Name);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x00183CBC File Offset: 0x00183CBC
		private void Load(TypeSpec obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.TypeSig);
			this.Add(obj.ExtraData);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x00183CF8 File Offset: 0x00183CF8
		private void Load(ImplMap obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Attributes);
			this.Add(obj.Name);
			this.Add(obj.Module);
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x00183D34 File Offset: 0x00183D34
		private void Load(AssemblyDef obj)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.ManifestModule != this.module)
			{
				return;
			}
			this.Add(obj.HashAlgorithm);
			this.Add(obj.Version);
			this.Add(obj.Attributes);
			this.Add(obj.PublicKey);
			this.Add(obj.Name);
			this.Add(obj.Culture);
			this.Add<DeclSecurity>(obj.DeclSecurities);
			this.Add<ModuleDef>(obj.Modules);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x00183DCC File Offset: 0x00183DCC
		private void Load(AssemblyRef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Version);
			this.Add(obj.Attributes);
			this.Add(obj.PublicKeyOrToken);
			this.Add(obj.Name);
			this.Add(obj.Culture);
			this.Add(obj.Hash);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x00183E38 File Offset: 0x00183E38
		private void Load(FileDef obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Flags);
			this.Add(obj.Name);
			this.Add(obj.HashValue);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x00183E80 File Offset: 0x00183E80
		private void Load(ExportedType obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.CustomAttributes);
			this.Add(obj.Attributes);
			this.Add(new uint?(obj.TypeDefId));
			this.Add(obj.TypeName);
			this.Add(obj.TypeNamespace);
			this.Add(obj.Implementation);
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x00183EE8 File Offset: 0x00183EE8
		private void Load(Resource obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Offset);
			this.Add(obj.Name);
			this.Add(obj.Attributes);
			switch (obj.ResourceType)
			{
			case ResourceType.Embedded:
				break;
			case ResourceType.AssemblyLinked:
			{
				AssemblyLinkedResource assemblyLinkedResource = (AssemblyLinkedResource)obj;
				this.Add(assemblyLinkedResource.Assembly);
				return;
			}
			case ResourceType.Linked:
			{
				LinkedResource linkedResource = (LinkedResource)obj;
				this.Add(linkedResource.File);
				this.Add(linkedResource.Hash);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x00183F74 File Offset: 0x00183F74
		private void Load(ManifestResource obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(new uint?(obj.Offset));
			this.Add(obj.Flags);
			this.Add(obj.Name);
			this.Add(obj.Implementation);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x00183FD0 File Offset: 0x00183FD0
		private void Load(GenericParam obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Owner);
			this.Add(obj.Number);
			this.Add(obj.Flags);
			this.Add(obj.Name);
			this.Add(obj.Kind);
			this.Add<GenericParamConstraint>(obj.GenericParamConstraints);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x0018403C File Offset: 0x0018403C
		private void Load(MethodSpec obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Method);
			this.Add(obj.Instantiation);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x00184078 File Offset: 0x00184078
		private void Load(GenericParamConstraint obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Owner);
			this.Add(obj.Constraint);
			this.Add(obj.CustomAttributes);
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x001840B4 File Offset: 0x001840B4
		private void Load(CANamedArgument obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Type);
			this.Add(obj.Name);
			this.Load(obj.Argument);
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x001840F0 File Offset: 0x001840F0
		private void Load(Parameter obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Type);
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x00184108 File Offset: 0x00184108
		private void Load(SecurityAttribute obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.AttributeType);
			this.Add(obj.NamedArguments);
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x00184138 File Offset: 0x00184138
		private void Load(CustomAttribute obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Constructor);
			this.Add(obj.RawData);
			this.Add(obj.ConstructorArguments);
			this.Add(obj.NamedArguments);
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x00184180 File Offset: 0x00184180
		private void Load(MethodOverride obj)
		{
			this.Add(obj.MethodBody);
			this.Add(obj.MethodDeclaration);
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x0018419C File Offset: 0x0018419C
		private void AddCAValue(object obj)
		{
			if (obj is CAArgument)
			{
				this.Load((CAArgument)obj);
				return;
			}
			IList<CAArgument> list = obj as IList<CAArgument>;
			if (list != null)
			{
				this.Add(list);
				return;
			}
			IMDTokenProvider imdtokenProvider = obj as IMDTokenProvider;
			if (imdtokenProvider != null)
			{
				this.Add(imdtokenProvider);
				return;
			}
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x001841F0 File Offset: 0x001841F0
		private void Load(CAArgument obj)
		{
			this.Add(obj.Type);
			this.AddCAValue(obj.Value);
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x0018421C File Offset: 0x0018421C
		private void Load(PdbMethod obj)
		{
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x00184220 File Offset: 0x00184220
		private void Load(ResourceDirectory obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Add(obj.Directories);
			this.Add(obj.Data);
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x00184250 File Offset: 0x00184250
		private void Load(ResourceData obj)
		{
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x00184254 File Offset: 0x00184254
		private void AddToStack<T>(T t) where T : class
		{
			if (t == null)
			{
				return;
			}
			if (this.seen.ContainsKey(t))
			{
				return;
			}
			this.seen[t] = true;
			this.stack.Push(t);
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x001842AC File Offset: 0x001842AC
		private void Add(CustomAttribute obj)
		{
			this.AddToStack<CustomAttribute>(obj);
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x001842B8 File Offset: 0x001842B8
		private void Add(SecurityAttribute obj)
		{
			this.AddToStack<SecurityAttribute>(obj);
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x001842C4 File Offset: 0x001842C4
		private void Add(CANamedArgument obj)
		{
			this.AddToStack<CANamedArgument>(obj);
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x001842D0 File Offset: 0x001842D0
		private void Add(Parameter obj)
		{
			this.AddToStack<Parameter>(obj);
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x001842DC File Offset: 0x001842DC
		private void Add(IMDTokenProvider o)
		{
			this.AddToStack<IMDTokenProvider>(o);
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x001842E8 File Offset: 0x001842E8
		private void Add(PdbMethod pdbMethod)
		{
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x001842EC File Offset: 0x001842EC
		private void Add(TypeSig ts)
		{
			this.AddToStack<TypeSig>(ts);
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x001842F8 File Offset: 0x001842F8
		private void Add(ResourceDirectory rd)
		{
			this.AddToStack<ResourceDirectory>(rd);
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x00184304 File Offset: 0x00184304
		private void Add(ResourceData rd)
		{
			this.AddToStack<ResourceData>(rd);
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x00184310 File Offset: 0x00184310
		private void Add<T>(IList<T> list) where T : IMDTokenProvider
		{
			if (list == null)
			{
				return;
			}
			foreach (T t in list)
			{
				this.Add(t);
			}
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x00184370 File Offset: 0x00184370
		private void Add(IList<TypeSig> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (TypeSig ts in list)
			{
				this.Add(ts);
			}
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x001843C8 File Offset: 0x001843C8
		private void Add(IList<CustomAttribute> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (CustomAttribute obj in list)
			{
				this.Add(obj);
			}
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x00184420 File Offset: 0x00184420
		private void Add(IList<SecurityAttribute> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (SecurityAttribute obj in list)
			{
				this.Add(obj);
			}
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x00184478 File Offset: 0x00184478
		private void Add(IList<MethodOverride> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (MethodOverride obj in list)
			{
				this.Load(obj);
			}
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x001844D0 File Offset: 0x001844D0
		private void Add(IList<CAArgument> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (CAArgument obj in list)
			{
				this.Load(obj);
			}
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x00184528 File Offset: 0x00184528
		private void Add(IList<CANamedArgument> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (CANamedArgument obj in list)
			{
				this.Add(obj);
			}
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x00184580 File Offset: 0x00184580
		private void Add(ParameterList list)
		{
			if (list == null)
			{
				return;
			}
			foreach (Parameter obj in list)
			{
				this.Add(obj);
			}
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x001845DC File Offset: 0x001845DC
		private void Add(IList<Instruction> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (Instruction instr in list)
			{
				this.Add(instr);
			}
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x00184634 File Offset: 0x00184634
		private void Add(IList<ExceptionHandler> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (ExceptionHandler eh in list)
			{
				this.Add(eh);
			}
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x0018468C File Offset: 0x0018468C
		private void Add(IList<Local> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (Local local in list)
			{
				this.Add(local);
			}
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x001846E4 File Offset: 0x001846E4
		private void Add(IList<ResourceDirectory> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (ResourceDirectory rd in list)
			{
				this.Add(rd);
			}
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x0018473C File Offset: 0x0018473C
		private void Add(IList<ResourceData> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (ResourceData rd in list)
			{
				this.Add(rd);
			}
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x00184794 File Offset: 0x00184794
		private void Add(VTableFixups vtf)
		{
			if (vtf == null)
			{
				return;
			}
			foreach (VTable vtable in vtf)
			{
				foreach (IMethod o in vtable)
				{
					this.Add(o);
				}
			}
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x00184824 File Offset: 0x00184824
		private void Add(Win32Resources vtf)
		{
			if (vtf == null)
			{
				return;
			}
			this.Add(vtf.Root);
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x0018483C File Offset: 0x0018483C
		private void Add(CallingConventionSig sig)
		{
			MethodBaseSig methodBaseSig = sig as MethodBaseSig;
			if (methodBaseSig != null)
			{
				this.Add(methodBaseSig);
				return;
			}
			FieldSig fieldSig = sig as FieldSig;
			if (fieldSig != null)
			{
				this.Add(fieldSig);
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

		// Token: 0x06004D9F RID: 19871 RVA: 0x001848A4 File Offset: 0x001848A4
		private void Add(MethodBaseSig msig)
		{
			if (msig == null)
			{
				return;
			}
			this.Add(msig.ExtraData);
			this.Add(msig.RetType);
			this.Add(msig.Params);
			this.Add(msig.ParamsAfterSentinel);
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x001848EC File Offset: 0x001848EC
		private void Add(FieldSig fsig)
		{
			if (fsig == null)
			{
				return;
			}
			this.Add(fsig.ExtraData);
			this.Add(fsig.Type);
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x0018491C File Offset: 0x0018491C
		private void Add(LocalSig lsig)
		{
			if (lsig == null)
			{
				return;
			}
			this.Add(lsig.ExtraData);
			this.Add(lsig.Locals);
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x0018494C File Offset: 0x0018494C
		private void Add(GenericInstMethodSig gsig)
		{
			if (gsig == null)
			{
				return;
			}
			this.Add(gsig.ExtraData);
			this.Add(gsig.GenericArguments);
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x0018497C File Offset: 0x0018497C
		private void Add(MarshalType mt)
		{
			if (mt == null)
			{
				return;
			}
			this.Add(mt.NativeType);
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x00184994 File Offset: 0x00184994
		private void Add(MethodBody mb)
		{
			CilBody cilBody = mb as CilBody;
			if (cilBody != null)
			{
				this.Add(cilBody);
				return;
			}
			NativeMethodBody nativeMethodBody = mb as NativeMethodBody;
			if (nativeMethodBody != null)
			{
				this.Add(nativeMethodBody);
				return;
			}
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x001849D0 File Offset: 0x001849D0
		private void Add(NativeMethodBody body)
		{
			if (body == null)
			{
				return;
			}
			this.Add(body.RVA);
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x001849E8 File Offset: 0x001849E8
		private void Add(CilBody body)
		{
			if (body == null)
			{
				return;
			}
			this.Add(body.Instructions);
			this.Add(body.ExceptionHandlers);
			this.Add(body.Variables);
			this.Add(body.PdbMethod);
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x00184A30 File Offset: 0x00184A30
		private void Add(Instruction instr)
		{
			if (instr == null)
			{
				return;
			}
			IMDTokenProvider imdtokenProvider = instr.Operand as IMDTokenProvider;
			if (imdtokenProvider != null)
			{
				this.Add(imdtokenProvider);
				return;
			}
			Parameter parameter = instr.Operand as Parameter;
			if (parameter != null)
			{
				this.Add(parameter);
				return;
			}
			Local local = instr.Operand as Local;
			if (local != null)
			{
				this.Add(local);
				return;
			}
			CallingConventionSig callingConventionSig = instr.Operand as CallingConventionSig;
			if (callingConventionSig != null)
			{
				this.Add(callingConventionSig);
				return;
			}
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x00184AB0 File Offset: 0x00184AB0
		private void Add(ExceptionHandler eh)
		{
			if (eh == null)
			{
				return;
			}
			this.Add(eh.CatchType);
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x00184AC8 File Offset: 0x00184AC8
		private void Add(Local local)
		{
			if (local == null)
			{
				return;
			}
			this.Add(local.Type);
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x00184AE0 File Offset: 0x00184AE0
		private void Add(PdbState state)
		{
			if (state == null)
			{
				return;
			}
			this.Add(state.UserEntryPoint);
		}

		// Token: 0x04002634 RID: 9780
		private readonly ModuleDef module;

		// Token: 0x04002635 RID: 9781
		private readonly ICancellationToken cancellationToken;

		// Token: 0x04002636 RID: 9782
		private readonly Dictionary<object, bool> seen;

		// Token: 0x04002637 RID: 9783
		private readonly Stack<object> stack;
	}
}
