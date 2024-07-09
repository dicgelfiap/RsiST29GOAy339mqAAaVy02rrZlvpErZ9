using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000968 RID: 2408
	[ComVisible(true)]
	[Guid("BA3FEE4C-ECB9-4E41-83B7-183FA41CD859")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IMetaDataEmit
	{
		// Token: 0x06005C62 RID: 23650
		void SetModuleProps([MarshalAs(UnmanagedType.LPWStr)] [In] string szName);

		// Token: 0x06005C63 RID: 23651
		void Save([MarshalAs(UnmanagedType.LPWStr)] [In] string szFile, [In] uint dwSaveFlags);

		// Token: 0x06005C64 RID: 23652
		void SaveToStream([In] IStream pIStream, [In] uint dwSaveFlags);

		// Token: 0x06005C65 RID: 23653
		void GetSaveSize([In] int fSave, out uint pdwSaveSize);

		// Token: 0x06005C66 RID: 23654
		void DefineTypeDef([MarshalAs(UnmanagedType.LPWStr)] [In] string szTypeDef, [In] uint dwTypeDefFlags, [In] uint tkExtends, [In] uint[] rtkImplements, out uint ptd);

		// Token: 0x06005C67 RID: 23655
		void DefineNestedType([MarshalAs(UnmanagedType.LPWStr)] [In] string szTypeDef, [In] uint dwTypeDefFlags, [In] uint tkExtends, [In] uint[] rtkImplements, [In] uint tdEncloser, out uint ptd);

		// Token: 0x06005C68 RID: 23656
		void SetHandler([MarshalAs(UnmanagedType.IUnknown)] [In] object pUnk);

		// Token: 0x06005C69 RID: 23657
		void DefineMethod(uint td, [MarshalAs(UnmanagedType.LPWStr)] string szName, uint dwMethodFlags, [In] IntPtr pvSigBlob, [In] uint cbSigBlob, uint ulCodeRVA, uint dwImplFlags, out uint pmd);

		// Token: 0x06005C6A RID: 23658
		void DefineMethodImpl([In] uint td, [In] uint tkBody, [In] uint tkDecl);

		// Token: 0x06005C6B RID: 23659
		void DefineTypeRefByName([In] uint tkResolutionScope, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, out uint ptr);

		// Token: 0x06005C6C RID: 23660
		void DefineImportType([In] IntPtr pAssemImport, [In] IntPtr pbHashValue, [In] uint cbHashValue, [In] IMetaDataImport pImport, [In] uint tdImport, [In] IntPtr pAssemEmit, out uint ptr);

		// Token: 0x06005C6D RID: 23661
		void DefineMemberRef([In] uint tkImport, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [In] IntPtr pvSigBlob, [In] uint cbSigBlob, out uint pmr);

		// Token: 0x06005C6E RID: 23662
		void DefineImportMember([In] IntPtr pAssemImport, [In] IntPtr pbHashValue, [In] uint cbHashValue, [In] IMetaDataImport pImport, [In] uint mbMember, [In] IntPtr pAssemEmit, [In] uint tkParent, out uint pmr);

		// Token: 0x06005C6F RID: 23663
		void DefineEvent([In] uint td, [MarshalAs(UnmanagedType.LPWStr)] [In] string szEvent, [In] uint dwEventFlags, [In] uint tkEventType, [In] uint mdAddOn, [In] uint mdRemoveOn, [In] uint mdFire, [In] uint[] rmdOtherMethods, out uint pmdEvent);

		// Token: 0x06005C70 RID: 23664
		void SetClassLayout([In] uint td, [In] uint dwPackSize, [In] IntPtr rFieldOffsets, [In] uint ulClassSize);

		// Token: 0x06005C71 RID: 23665
		void DeleteClassLayout([In] uint td);

		// Token: 0x06005C72 RID: 23666
		void SetFieldMarshal([In] uint tk, [In] IntPtr pvNativeType, [In] uint cbNativeType);

		// Token: 0x06005C73 RID: 23667
		void DeleteFieldMarshal([In] uint tk);

		// Token: 0x06005C74 RID: 23668
		void DefinePermissionSet([In] uint tk, [In] uint dwAction, [In] IntPtr pvPermission, [In] uint cbPermission, out uint ppm);

		// Token: 0x06005C75 RID: 23669
		void SetRVA([In] uint md, [In] uint ulRVA);

		// Token: 0x06005C76 RID: 23670
		void GetTokenFromSig([In] IntPtr pvSig, [In] uint cbSig, out uint pmsig);

		// Token: 0x06005C77 RID: 23671
		void DefineModuleRef([MarshalAs(UnmanagedType.LPWStr)] [In] string szName, out uint pmur);

		// Token: 0x06005C78 RID: 23672
		void SetParent([In] uint mr, [In] uint tk);

		// Token: 0x06005C79 RID: 23673
		void GetTokenFromTypeSpec([In] IntPtr pvSig, [In] uint cbSig, out uint ptypespec);

		// Token: 0x06005C7A RID: 23674
		void SaveToMemory(out IntPtr pbData, [In] uint cbData);

		// Token: 0x06005C7B RID: 23675
		void DefineUserString([MarshalAs(UnmanagedType.LPWStr)] [In] string szString, [In] uint cchString, out uint pstk);

		// Token: 0x06005C7C RID: 23676
		void DeleteToken([In] uint tkObj);

		// Token: 0x06005C7D RID: 23677
		void SetMethodProps([In] uint md, [In] uint dwMethodFlags, [In] uint ulCodeRVA, [In] uint dwImplFlags);

		// Token: 0x06005C7E RID: 23678
		void SetTypeDefProps([In] uint td, [In] uint dwTypeDefFlags, [In] uint tkExtends, [In] uint[] rtkImplements);

		// Token: 0x06005C7F RID: 23679
		void SetEventProps([In] uint ev, [In] uint dwEventFlags, [In] uint tkEventType, [In] uint mdAddOn, [In] uint mdRemoveOn, [In] uint mdFire, [In] uint[] rmdOtherMethods);

		// Token: 0x06005C80 RID: 23680
		void SetPermissionSetProps([In] uint tk, [In] uint dwAction, [In] IntPtr pvPermission, [In] uint cbPermission, out uint ppm);

		// Token: 0x06005C81 RID: 23681
		void DefinePinvokeMap([In] uint tk, [In] uint dwMappingFlags, [MarshalAs(UnmanagedType.LPWStr)] [In] string szImportName, [In] uint mrImportDLL);

		// Token: 0x06005C82 RID: 23682
		void SetPinvokeMap([In] uint tk, [In] uint dwMappingFlags, [MarshalAs(UnmanagedType.LPWStr)] [In] string szImportName, [In] uint mrImportDLL);

		// Token: 0x06005C83 RID: 23683
		void DeletePinvokeMap([In] uint tk);

		// Token: 0x06005C84 RID: 23684
		void DefineCustomAttribute([In] uint tkOwner, [In] uint tkCtor, [In] IntPtr pCustomAttribute, [In] uint cbCustomAttribute, out uint pcv);

		// Token: 0x06005C85 RID: 23685
		void SetCustomAttributeValue([In] uint pcv, [In] IntPtr pCustomAttribute, [In] uint cbCustomAttribute);

		// Token: 0x06005C86 RID: 23686
		void DefineField(uint td, [MarshalAs(UnmanagedType.LPWStr)] string szName, uint dwFieldFlags, [In] IntPtr pvSigBlob, [In] uint cbSigBlob, [In] uint dwCPlusTypeFlag, [In] IntPtr pValue, [In] uint cchValue, out uint pmd);

		// Token: 0x06005C87 RID: 23687
		void DefineProperty([In] uint td, [MarshalAs(UnmanagedType.LPWStr)] [In] string szProperty, [In] uint dwPropFlags, [In] IntPtr pvSig, [In] uint cbSig, [In] uint dwCPlusTypeFlag, [In] IntPtr pValue, [In] uint cchValue, [In] uint mdSetter, [In] uint mdGetter, [In] uint[] rmdOtherMethods, out uint pmdProp);

		// Token: 0x06005C88 RID: 23688
		void DefineParam([In] uint md, [In] uint ulParamSeq, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [In] uint dwParamFlags, [In] uint dwCPlusTypeFlag, [In] IntPtr pValue, [In] uint cchValue, out uint ppd);

		// Token: 0x06005C89 RID: 23689
		void SetFieldProps([In] uint fd, [In] uint dwFieldFlags, [In] uint dwCPlusTypeFlag, [In] IntPtr pValue, [In] uint cchValue);

		// Token: 0x06005C8A RID: 23690
		void SetPropertyProps([In] uint pr, [In] uint dwPropFlags, [In] uint dwCPlusTypeFlag, [In] IntPtr pValue, [In] uint cchValue, [In] uint mdSetter, [In] uint mdGetter, [In] uint[] rmdOtherMethods);

		// Token: 0x06005C8B RID: 23691
		void SetParamProps([In] uint pd, [MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [In] uint dwParamFlags, [In] uint dwCPlusTypeFlag, [Out] IntPtr pValue, [In] uint cchValue);

		// Token: 0x06005C8C RID: 23692
		void DefineSecurityAttributeSet([In] uint tkObj, [In] IntPtr rSecAttrs, [In] uint cSecAttrs, out uint pulErrorAttr);

		// Token: 0x06005C8D RID: 23693
		void ApplyEditAndContinue([MarshalAs(UnmanagedType.IUnknown)] [In] object pImport);

		// Token: 0x06005C8E RID: 23694
		void TranslateSigWithScope([In] IntPtr pAssemImport, [In] IntPtr pbHashValue, [In] uint cbHashValue, [In] IMetaDataImport import, [In] IntPtr pbSigBlob, [In] uint cbSigBlob, [In] IntPtr pAssemEmit, [In] IMetaDataEmit emit, [Out] IntPtr pvTranslatedSig, uint cbTranslatedSigMax, out uint pcbTranslatedSig);

		// Token: 0x06005C8F RID: 23695
		void SetMethodImplFlags([In] uint md, uint dwImplFlags);

		// Token: 0x06005C90 RID: 23696
		void SetFieldRVA([In] uint fd, [In] uint ulRVA);

		// Token: 0x06005C91 RID: 23697
		void Merge([In] IMetaDataImport pImport, [In] IntPtr pHostMapToken, [MarshalAs(UnmanagedType.IUnknown)] [In] object pHandler);

		// Token: 0x06005C92 RID: 23698
		void MergeEnd();
	}
}
