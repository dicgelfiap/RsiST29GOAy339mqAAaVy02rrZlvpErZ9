using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000967 RID: 2407
	[ComVisible(true)]
	[Guid("7DAC8207-D3AE-4C75-9B67-92801A497D44")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IMetaDataImport
	{
		// Token: 0x06005C24 RID: 23588
		void CloseEnum(IntPtr hEnum);

		// Token: 0x06005C25 RID: 23589
		void CountEnum(IntPtr hEnum, ref uint pulCount);

		// Token: 0x06005C26 RID: 23590
		void ResetEnum(IntPtr hEnum, uint ulPos);

		// Token: 0x06005C27 RID: 23591
		void EnumTypeDefs(IntPtr phEnum, uint[] rTypeDefs, uint cMax, out uint pcTypeDefs);

		// Token: 0x06005C28 RID: 23592
		void EnumInterfaceImpls(ref IntPtr phEnum, uint td, uint[] rImpls, uint cMax, ref uint pcImpls);

		// Token: 0x06005C29 RID: 23593
		void EnumTypeRefs(ref IntPtr phEnum, uint[] rTypeRefs, uint cMax, ref uint pcTypeRefs);

		// Token: 0x06005C2A RID: 23594
		void FindTypeDefByName([MarshalAs(UnmanagedType.LPWStr)] [In] string szTypeDef, [In] uint tkEnclosingClass, out uint ptd);

		// Token: 0x06005C2B RID: 23595
		void GetScopeProps([Out] IntPtr szName, [In] uint cchName, out uint pchName, out Guid pmvid);

		// Token: 0x06005C2C RID: 23596
		void GetModuleFromScope(out uint pmd);

		// Token: 0x06005C2D RID: 23597
		unsafe void GetTypeDefProps([In] uint td, [In] ushort* szTypeDef, [In] uint cchTypeDef, [Out] uint* pchTypeDef, [Out] uint* pdwTypeDefFlags, [Out] uint* ptkExtends);

		// Token: 0x06005C2E RID: 23598
		void GetInterfaceImplProps([In] uint iiImpl, out uint pClass, out uint ptkIface);

		// Token: 0x06005C2F RID: 23599
		unsafe void GetTypeRefProps([In] uint tr, [Out] uint* ptkResolutionScope, [Out] ushort* szName, [In] uint cchName, [Out] uint* pchName);

		// Token: 0x06005C30 RID: 23600
		void ResolveTypeRef(uint tr, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppIScope, out uint ptd);

		// Token: 0x06005C31 RID: 23601
		void EnumMembers([In] [Out] ref IntPtr phEnum, [In] uint cl, [Out] uint[] rMembers, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C32 RID: 23602
		void EnumMembersWithName([In] [Out] ref IntPtr phEnum, [In] uint cl, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [Out] uint[] rMembers, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C33 RID: 23603
		void EnumMethods([In] [Out] ref IntPtr phEnum, [In] uint cl, [Out] uint[] rMethods, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C34 RID: 23604
		void EnumMethodsWithName([In] [Out] ref IntPtr phEnum, [In] uint cl, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, uint[] rMethods, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C35 RID: 23605
		void EnumFields([In] [Out] ref IntPtr phEnum, [In] uint cl, [Out] uint[] rFields, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C36 RID: 23606
		void EnumFieldsWithName([In] [Out] ref IntPtr phEnum, [In] uint cl, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [Out] uint[] rFields, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C37 RID: 23607
		void EnumParams([In] [Out] ref IntPtr phEnum, [In] uint mb, [Out] uint[] rParams, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C38 RID: 23608
		void EnumMemberRefs([In] [Out] ref IntPtr phEnum, [In] uint tkParent, [Out] uint[] rMemberRefs, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C39 RID: 23609
		void EnumMethodImpls([In] [Out] ref IntPtr phEnum, [In] uint td, [Out] uint[] rMethodBody, [Out] uint[] rMethodDecl, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C3A RID: 23610
		void EnumPermissionSets([In] [Out] ref IntPtr phEnum, [In] uint tk, [In] uint dwActions, [Out] uint[] rPermission, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C3B RID: 23611
		void FindMember([In] uint td, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [In] IntPtr pvSigBlob, [In] uint cbSigBlob, out uint pmb);

		// Token: 0x06005C3C RID: 23612
		void FindMethod([In] uint td, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [In] IntPtr pvSigBlob, [In] uint cbSigBlob, out uint pmb);

		// Token: 0x06005C3D RID: 23613
		void FindField([In] uint td, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [In] IntPtr pvSigBlob, [In] uint cbSigBlob, out uint pmb);

		// Token: 0x06005C3E RID: 23614
		void FindMemberRef([In] uint td, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [In] IntPtr pvSigBlob, [In] uint cbSigBlob, out uint pmr);

		// Token: 0x06005C3F RID: 23615
		unsafe void GetMethodProps(uint mb, uint* pClass, [In] ushort* szMethod, uint cchMethod, uint* pchMethod, uint* pdwAttr, [Out] IntPtr* ppvSigBlob, [Out] uint* pcbSigBlob, [Out] uint* pulCodeRVA, [Out] uint* pdwImplFlags);

		// Token: 0x06005C40 RID: 23616
		void GetMemberRefProps([In] uint mr, out uint ptk, [Out] IntPtr szMember, [In] uint cchMember, out uint pchMember, out IntPtr ppvSigBlob, out uint pbSig);

		// Token: 0x06005C41 RID: 23617
		void EnumProperties([In] [Out] ref IntPtr phEnum, [In] uint td, [Out] uint[] rProperties, [In] uint cMax, out uint pcProperties);

		// Token: 0x06005C42 RID: 23618
		void EnumEvents([In] [Out] ref IntPtr phEnum, [In] uint td, [Out] uint[] rEvents, [In] uint cMax, out uint pcEvents);

		// Token: 0x06005C43 RID: 23619
		void GetEventProps([In] uint ev, out uint pClass, [MarshalAs(UnmanagedType.LPWStr)] [Out] string szEvent, [In] uint cchEvent, out uint pchEvent, out uint pdwEventFlags, out uint ptkEventType, out uint pmdAddOn, out uint pmdRemoveOn, out uint pmdFire, [In] [Out] uint[] rmdOtherMethod, [In] uint cMax, out uint pcOtherMethod);

		// Token: 0x06005C44 RID: 23620
		void EnumMethodSemantics([In] [Out] ref IntPtr phEnum, [In] uint mb, [In] [Out] uint[] rEventProp, [In] uint cMax, out uint pcEventProp);

		// Token: 0x06005C45 RID: 23621
		void GetMethodSemantics([In] uint mb, [In] uint tkEventProp, out uint pdwSemanticsFlags);

		// Token: 0x06005C46 RID: 23622
		void GetClassLayout([In] uint td, out uint pdwPackSize, out IntPtr rFieldOffset, [In] uint cMax, out uint pcFieldOffset, out uint pulClassSize);

		// Token: 0x06005C47 RID: 23623
		void GetFieldMarshal([In] uint tk, out IntPtr ppvNativeType, out uint pcbNativeType);

		// Token: 0x06005C48 RID: 23624
		void GetRVA(uint tk, out uint pulCodeRVA, out uint pdwImplFlags);

		// Token: 0x06005C49 RID: 23625
		void GetPermissionSetProps([In] uint pm, out uint pdwAction, out IntPtr ppvPermission, out uint pcbPermission);

		// Token: 0x06005C4A RID: 23626
		unsafe void GetSigFromToken([In] uint mdSig, [Out] byte** ppvSig, [Out] uint* pcbSig);

		// Token: 0x06005C4B RID: 23627
		void GetModuleRefProps([In] uint mur, [Out] IntPtr szName, [In] uint cchName, out uint pchName);

		// Token: 0x06005C4C RID: 23628
		void EnumModuleRefs([In] [Out] ref IntPtr phEnum, [Out] uint[] rModuleRefs, [In] uint cmax, out uint pcModuleRefs);

		// Token: 0x06005C4D RID: 23629
		void GetTypeSpecFromToken([In] uint typespec, out IntPtr ppvSig, out uint pcbSig);

		// Token: 0x06005C4E RID: 23630
		void GetNameFromToken([In] uint tk, out IntPtr pszUtf8NamePtr);

		// Token: 0x06005C4F RID: 23631
		void EnumUnresolvedMethods([In] [Out] ref IntPtr phEnum, [Out] uint[] rMethods, [In] uint cMax, out uint pcTokens);

		// Token: 0x06005C50 RID: 23632
		void GetUserString([In] uint stk, [Out] IntPtr szString, [In] uint cchString, out uint pchString);

		// Token: 0x06005C51 RID: 23633
		void GetPinvokeMap([In] uint tk, out uint pdwMappingFlags, [Out] IntPtr szImportName, [In] uint cchImportName, out uint pchImportName, out uint pmrImportDLL);

		// Token: 0x06005C52 RID: 23634
		void EnumSignatures([In] [Out] ref IntPtr phEnum, [Out] uint[] rSignatures, [In] uint cmax, out uint pcSignatures);

		// Token: 0x06005C53 RID: 23635
		void EnumTypeSpecs([In] [Out] ref IntPtr phEnum, [Out] uint[] rTypeSpecs, [In] uint cmax, out uint pcTypeSpecs);

		// Token: 0x06005C54 RID: 23636
		void EnumUserStrings([In] [Out] ref IntPtr phEnum, [Out] uint[] rStrings, [In] uint cmax, out uint pcStrings);

		// Token: 0x06005C55 RID: 23637
		void GetParamForMethodIndex([In] uint md, [In] uint ulParamSeq, out uint ppd);

		// Token: 0x06005C56 RID: 23638
		void EnumCustomAttributes([In] [Out] IntPtr phEnum, [In] uint tk, [In] uint tkType, [Out] uint[] rCustomAttributes, [In] uint cMax, out uint pcCustomAttributes);

		// Token: 0x06005C57 RID: 23639
		void GetCustomAttributeProps([In] uint cv, out uint ptkObj, out uint ptkType, out IntPtr ppBlob, out uint pcbSize);

		// Token: 0x06005C58 RID: 23640
		void FindTypeRef([In] uint tkResolutionScope, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, out uint ptr);

		// Token: 0x06005C59 RID: 23641
		void GetMemberProps(uint mb, out uint pClass, IntPtr szMember, uint cchMember, out uint pchMember, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pulCodeRVA, out uint pdwImplFlags, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

		// Token: 0x06005C5A RID: 23642
		void GetFieldProps(uint mb, out uint pClass, IntPtr szField, uint cchField, out uint pchField, out uint pdwAttr, out IntPtr ppvSigBlob, out uint pcbSigBlob, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

		// Token: 0x06005C5B RID: 23643
		void GetPropertyProps([In] uint prop, out uint pClass, [Out] IntPtr szProperty, [In] uint cchProperty, out uint pchProperty, out uint pdwPropFlags, out IntPtr ppvSig, out uint pbSig, out uint pdwCPlusTypeFlag, out IntPtr ppDefaultValue, out uint pcchDefaultValue, out uint pmdSetter, out uint pmdGetter, [In] [Out] uint[] rmdOtherMethod, [In] uint cMax, out uint pcOtherMethod);

		// Token: 0x06005C5C RID: 23644
		void GetParamProps([In] uint tk, out uint pmd, out uint pulSequence, [Out] IntPtr szName, [Out] uint cchName, out uint pchName, out uint pdwAttr, out uint pdwCPlusTypeFlag, out IntPtr ppValue, out uint pcchValue);

		// Token: 0x06005C5D RID: 23645
		void GetCustomAttributeByName([In] uint tkObj, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, out IntPtr ppData, out uint pcbData);

		// Token: 0x06005C5E RID: 23646
		bool IsValidToken([In] uint tk);

		// Token: 0x06005C5F RID: 23647
		unsafe void GetNestedClassProps([In] uint tdNestedClass, [Out] uint* ptdEnclosingClass);

		// Token: 0x06005C60 RID: 23648
		void GetNativeCallConvFromSig([In] IntPtr pvSig, [In] uint cbSig, out uint pCallConv);

		// Token: 0x06005C61 RID: 23649
		void IsGlobal([In] uint pd, out int pbGlobal);
	}
}
