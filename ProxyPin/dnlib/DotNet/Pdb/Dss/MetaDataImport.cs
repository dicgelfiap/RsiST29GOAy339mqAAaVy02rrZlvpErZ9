using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000976 RID: 2422
	internal abstract class MetaDataImport : IMetaDataImport
	{
		// Token: 0x06005D21 RID: 23841 RVA: 0x001C0538 File Offset: 0x001C0538
		public unsafe virtual void GetTypeDefProps([In] uint td, [In] ushort* szTypeDef, [In] uint cchTypeDef, [Out] uint* pchTypeDef, [Out] uint* pdwTypeDefFlags, [Out] uint* ptkExtends)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x001C0540 File Offset: 0x001C0540
		public unsafe virtual void GetMethodProps(uint mb, uint* pClass, [In] ushort* szMethod, uint cchMethod, uint* pchMethod, uint* pdwAttr, [Out] IntPtr* ppvSigBlob, [Out] uint* pcbSigBlob, [Out] uint* pulCodeRVA, [Out] uint* pdwImplFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x001C0548 File Offset: 0x001C0548
		public unsafe virtual void GetNestedClassProps([In] uint tdNestedClass, [Out] uint* ptdEnclosingClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D24 RID: 23844 RVA: 0x001C0550 File Offset: 0x001C0550
		public unsafe virtual void GetSigFromToken(uint mdSig, byte** ppvSig, uint* pcbSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x001C0558 File Offset: 0x001C0558
		public unsafe virtual void GetTypeRefProps(uint tr, uint* ptkResolutionScope, ushort* szName, uint cchName, uint* pchName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x001C0560 File Offset: 0x001C0560
		protected unsafe void CopyTypeName(string typeNamespace, string typeName, ushort* destBuffer, uint destBufferLen, uint* requiredLength)
		{
			if (typeName == null)
			{
				typeName = string.Empty;
			}
			if (typeNamespace == null)
			{
				typeNamespace = string.Empty;
			}
			if (destBuffer != null && destBufferLen > 0U)
			{
				uint num = destBufferLen - 1U;
				uint num2 = 0U;
				if (typeNamespace.Length > 0)
				{
					int num3 = 0;
					while (num3 < typeNamespace.Length && num2 < num)
					{
						*(destBuffer++) = (ushort)typeNamespace[num3];
						num3++;
						num2 += 1U;
					}
					if (num2 < num)
					{
						*(destBuffer++) = 46;
						num2 += 1U;
					}
				}
				int num4 = 0;
				while (num4 < typeName.Length && num2 < num)
				{
					*(destBuffer++) = (ushort)typeName[num4];
					num4++;
					num2 += 1U;
				}
				*destBuffer = 0;
			}
			if (requiredLength != null)
			{
				int num5 = (typeNamespace.Length == 0) ? typeName.Length : (typeNamespace.Length + 1 + typeName.Length);
				int num6 = Math.Min(num5, (int)Math.Min(2147483647U, (destBufferLen == 0U) ? 0U : (destBufferLen - 1U)));
				if (destBuffer != null)
				{
					*requiredLength = (uint)num6;
					return;
				}
				*requiredLength = (uint)num5;
			}
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x001C0680 File Offset: 0x001C0680
		void IMetaDataImport.CloseEnum(IntPtr hEnum)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x001C0688 File Offset: 0x001C0688
		void IMetaDataImport.CountEnum(IntPtr hEnum, ref uint pulCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x001C0690 File Offset: 0x001C0690
		void IMetaDataImport.ResetEnum(IntPtr hEnum, uint ulPos)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x001C0698 File Offset: 0x001C0698
		void IMetaDataImport.EnumTypeDefs(IntPtr phEnum, uint[] rTypeDefs, uint cMax, out uint pcTypeDefs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x001C06A0 File Offset: 0x001C06A0
		void IMetaDataImport.EnumInterfaceImpls(ref IntPtr phEnum, uint td, uint[] rImpls, uint cMax, ref uint pcImpls)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x001C06A8 File Offset: 0x001C06A8
		void IMetaDataImport.EnumTypeRefs(ref IntPtr phEnum, uint[] rTypeRefs, uint cMax, ref uint pcTypeRefs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x001C06B0 File Offset: 0x001C06B0
		void IMetaDataImport.FindTypeDefByName(string szTypeDef, uint tkEnclosingClass, out uint ptd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x001C06B8 File Offset: 0x001C06B8
		void IMetaDataImport.GetScopeProps(IntPtr szName, uint cchName, out uint pchName, out Guid pmvid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x001C06C0 File Offset: 0x001C06C0
		void IMetaDataImport.GetModuleFromScope(out uint pmd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x001C06C8 File Offset: 0x001C06C8
		void IMetaDataImport.GetInterfaceImplProps(uint iiImpl, out uint pClass, out uint ptkIface)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x001C06D0 File Offset: 0x001C06D0
		void IMetaDataImport.ResolveTypeRef(uint tr, ref Guid riid, out object ppIScope, out uint ptd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x001C06D8 File Offset: 0x001C06D8
		void IMetaDataImport.EnumMembers(ref IntPtr phEnum, uint cl, uint[] rMembers, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x001C06E0 File Offset: 0x001C06E0
		void IMetaDataImport.EnumMembersWithName(ref IntPtr phEnum, uint cl, string szName, uint[] rMembers, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x001C06E8 File Offset: 0x001C06E8
		void IMetaDataImport.EnumMethods(ref IntPtr phEnum, uint cl, uint[] rMethods, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x001C06F0 File Offset: 0x001C06F0
		void IMetaDataImport.EnumMethodsWithName(ref IntPtr phEnum, uint cl, string szName, uint[] rMethods, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x001C06F8 File Offset: 0x001C06F8
		void IMetaDataImport.EnumFields(ref IntPtr phEnum, uint cl, uint[] rFields, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x001C0700 File Offset: 0x001C0700
		void IMetaDataImport.EnumFieldsWithName(ref IntPtr phEnum, uint cl, string szName, uint[] rFields, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x001C0708 File Offset: 0x001C0708
		void IMetaDataImport.EnumParams(ref IntPtr phEnum, uint mb, uint[] rParams, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x001C0710 File Offset: 0x001C0710
		void IMetaDataImport.EnumMemberRefs(ref IntPtr phEnum, uint tkParent, uint[] rMemberRefs, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x001C0718 File Offset: 0x001C0718
		void IMetaDataImport.EnumMethodImpls(ref IntPtr phEnum, uint td, uint[] rMethodBody, uint[] rMethodDecl, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x001C0720 File Offset: 0x001C0720
		void IMetaDataImport.EnumPermissionSets(ref IntPtr phEnum, uint tk, uint dwActions, uint[] rPermission, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x001C0728 File Offset: 0x001C0728
		void IMetaDataImport.FindMember(uint td, string szName, IntPtr pvSigBlob, uint cbSigBlob, out uint pmb)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x001C0730 File Offset: 0x001C0730
		void IMetaDataImport.FindMethod(uint td, string szName, IntPtr pvSigBlob, uint cbSigBlob, out uint pmb)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x001C0738 File Offset: 0x001C0738
		void IMetaDataImport.FindField(uint td, string szName, IntPtr pvSigBlob, uint cbSigBlob, out uint pmb)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x001C0740 File Offset: 0x001C0740
		void IMetaDataImport.FindMemberRef(uint td, string szName, IntPtr pvSigBlob, uint cbSigBlob, out uint pmr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x001C0748 File Offset: 0x001C0748
		void IMetaDataImport.GetMemberRefProps(uint mr, out uint ptk, IntPtr szMember, uint cchMember, out uint pchMember, out IntPtr ppvSigBlob, out uint pbSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x001C0750 File Offset: 0x001C0750
		void IMetaDataImport.EnumProperties(ref IntPtr phEnum, uint td, uint[] rProperties, uint cMax, out uint pcProperties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x001C0758 File Offset: 0x001C0758
		void IMetaDataImport.EnumEvents(ref IntPtr phEnum, uint td, uint[] rEvents, uint cMax, out uint pcEvents)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x001C0760 File Offset: 0x001C0760
		void IMetaDataImport.GetEventProps(uint ev, out uint pClass, string szEvent, uint cchEvent, out uint pchEvent, out uint pdwEventFlags, out uint ptkEventType, out uint pmdAddOn, out uint pmdRemoveOn, out uint pmdFire, uint[] rmdOtherMethod, uint cMax, out uint pcOtherMethod)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x001C0768 File Offset: 0x001C0768
		void IMetaDataImport.EnumMethodSemantics(ref IntPtr phEnum, uint mb, uint[] rEventProp, uint cMax, out uint pcEventProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x001C0770 File Offset: 0x001C0770
		void IMetaDataImport.GetMethodSemantics(uint mb, uint tkEventProp, out uint pdwSemanticsFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x001C0778 File Offset: 0x001C0778
		void IMetaDataImport.GetClassLayout(uint td, out uint pdwPackSize, out IntPtr rFieldOffset, uint cMax, out uint pcFieldOffset, out uint pulClassSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x001C0780 File Offset: 0x001C0780
		void IMetaDataImport.GetFieldMarshal(uint tk, out IntPtr ppvNativeType, out uint pcbNativeType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x001C0788 File Offset: 0x001C0788
		void IMetaDataImport.GetRVA(uint tk, out uint pulCodeRVA, out uint pdwImplFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x001C0790 File Offset: 0x001C0790
		void IMetaDataImport.GetPermissionSetProps(uint pm, out uint pdwAction, out IntPtr ppvPermission, out uint pcbPermission)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x001C0798 File Offset: 0x001C0798
		void IMetaDataImport.GetModuleRefProps(uint mur, IntPtr szName, uint cchName, out uint pchName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x001C07A0 File Offset: 0x001C07A0
		void IMetaDataImport.EnumModuleRefs(ref IntPtr phEnum, uint[] rModuleRefs, uint cmax, out uint pcModuleRefs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x001C07A8 File Offset: 0x001C07A8
		void IMetaDataImport.GetTypeSpecFromToken(uint typespec, out IntPtr ppvSig, out uint pcbSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x001C07B0 File Offset: 0x001C07B0
		void IMetaDataImport.GetNameFromToken(uint tk, out IntPtr pszUtf8NamePtr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x001C07B8 File Offset: 0x001C07B8
		void IMetaDataImport.EnumUnresolvedMethods(ref IntPtr phEnum, uint[] rMethods, uint cMax, out uint pcTokens)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x001C07C0 File Offset: 0x001C07C0
		void IMetaDataImport.GetUserString(uint stk, IntPtr szString, uint cchString, out uint pchString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x001C07C8 File Offset: 0x001C07C8
		void IMetaDataImport.GetPinvokeMap(uint tk, out uint pdwMappingFlags, IntPtr szImportName, uint cchImportName, out uint pchImportName, out uint pmrImportDLL)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x001C07D0 File Offset: 0x001C07D0
		void IMetaDataImport.EnumSignatures(ref IntPtr phEnum, uint[] rSignatures, uint cmax, out uint pcSignatures)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x001C07D8 File Offset: 0x001C07D8
		void IMetaDataImport.EnumTypeSpecs(ref IntPtr phEnum, uint[] rTypeSpecs, uint cmax, out uint pcTypeSpecs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x001C07E0 File Offset: 0x001C07E0
		void IMetaDataImport.EnumUserStrings(ref IntPtr phEnum, uint[] rStrings, uint cmax, out uint pcStrings)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x001C07E8 File Offset: 0x001C07E8
		void IMetaDataImport.GetParamForMethodIndex(uint md, uint ulParamSeq, out uint ppd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x001C07F0 File Offset: 0x001C07F0
		void IMetaDataImport.EnumCustomAttributes(IntPtr phEnum, uint tk, uint tkType, uint[] rCustomAttributes, uint cMax, out uint pcCustomAttributes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x001C07F8 File Offset: 0x001C07F8
		void IMetaDataImport.GetCustomAttributeProps(uint cv, out uint ptkObj, out uint ptkType, out IntPtr ppBlob, out uint pcbSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x001C0800 File Offset: 0x001C0800
		void IMetaDataImport.FindTypeRef(uint tkResolutionScope, string szName, out uint ptr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x001C0808 File Offset: 0x001C0808
		void IMetaDataImport.GetMemberProps(uint mb, out uint pClass, IntPtr szMember, uint cchMember, out uint pchMember, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x001C0810 File Offset: 0x001C0810
		void IMetaDataImport.GetFieldProps(uint mb, out uint pClass, IntPtr szField, uint cchField, out uint pchField, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x001C0818 File Offset: 0x001C0818
		void IMetaDataImport.GetPropertyProps(uint prop, out uint pClass, IntPtr szProperty, uint cchProperty, out uint pchProperty, out uint pdwPropFlags, out IntPtr ppvSig, out uint pbSig, out uint pdwCPlusTypeFlag, out IntPtr ppDefaultValue, out uint pcchDefaultValue, out uint pmdSetter, out uint pmdGetter, uint[] rmdOtherMethod, uint cMax, out uint pcOtherMethod)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x001C0820 File Offset: 0x001C0820
		void IMetaDataImport.GetParamProps(uint tk, out uint pmd, out uint pulSequence, IntPtr szName, uint cchName, out uint pchName, out uint pdwAttr, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x001C0828 File Offset: 0x001C0828
		void IMetaDataImport.GetCustomAttributeByName(uint tkObj, string szName, out IntPtr ppData, out uint pcbData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x001C0830 File Offset: 0x001C0830
		bool IMetaDataImport.IsValidToken(uint tk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x001C0838 File Offset: 0x001C0838
		void IMetaDataImport.GetNativeCallConvFromSig(IntPtr pvSig, uint cbSig, out uint pCallConv)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x001C0840 File Offset: 0x001C0840
		void IMetaDataImport.IsGlobal(uint pd, out int pbGlobal)
		{
			throw new NotImplementedException();
		}
	}
}
