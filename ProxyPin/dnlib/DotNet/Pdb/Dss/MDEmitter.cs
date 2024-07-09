using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using dnlib.DotNet.MD;
using dnlib.DotNet.Writer;

namespace dnlib.DotNet.Pdb.Dss
{
	// Token: 0x02000975 RID: 2421
	internal sealed class MDEmitter : MetaDataImport, IMetaDataEmit
	{
		// Token: 0x06005CEC RID: 23788 RVA: 0x001C006C File Offset: 0x001C006C
		public MDEmitter(dnlib.DotNet.Writer.Metadata metadata)
		{
			this.metadata = metadata;
			this.tokenToTypeDef = new Dictionary<uint, TypeDef>(metadata.TablesHeap.TypeDefTable.Rows);
			this.tokenToMethodDef = new Dictionary<uint, MethodDef>(metadata.TablesHeap.MethodTable.Rows);
			foreach (TypeDef typeDef in metadata.Module.GetTypes())
			{
				if (typeDef != null)
				{
					this.tokenToTypeDef.Add(new MDToken(Table.TypeDef, metadata.GetRid(typeDef)).Raw, typeDef);
					foreach (MethodDef methodDef in typeDef.Methods)
					{
						if (methodDef != null)
						{
							this.tokenToMethodDef.Add(new MDToken(Table.Method, metadata.GetRid(methodDef)).Raw, methodDef);
						}
					}
				}
			}
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x001C0198 File Offset: 0x001C0198
		public unsafe override void GetMethodProps(uint mb, uint* pClass, ushort* szMethod, uint cchMethod, uint* pchMethod, uint* pdwAttr, IntPtr* ppvSigBlob, uint* pcbSigBlob, uint* pulCodeRVA, uint* pdwImplFlags)
		{
			if (mb >> 24 != 6U)
			{
				throw new ArgumentException();
			}
			MethodDef methodDef = this.tokenToMethodDef[mb];
			RawMethodRow rawMethodRow = this.metadata.TablesHeap.MethodTable[mb & 16777215U];
			if (pClass != null)
			{
				*pClass = new MDToken(Table.TypeDef, this.metadata.GetRid(methodDef.DeclaringType)).Raw;
			}
			if (pdwAttr != null)
			{
				*pdwAttr = (uint)rawMethodRow.Flags;
			}
			if (ppvSigBlob != null)
			{
				*ppvSigBlob = IntPtr.Zero;
			}
			if (pcbSigBlob != null)
			{
				*pcbSigBlob = 0U;
			}
			if (pulCodeRVA != null)
			{
				*pulCodeRVA = rawMethodRow.RVA;
			}
			if (pdwImplFlags != null)
			{
				*pdwImplFlags = (uint)rawMethodRow.ImplFlags;
			}
			string text = methodDef.Name.String ?? string.Empty;
			int num = (int)Math.Min((uint)(text.Length + 1), cchMethod);
			if (szMethod != null)
			{
				int i = 0;
				while (i < num - 1)
				{
					*szMethod = (ushort)text[i];
					i++;
					szMethod++;
				}
				if (num > 0)
				{
					*szMethod = 0;
				}
			}
			if (pchMethod != null)
			{
				*pchMethod = (uint)num;
			}
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x001C02C0 File Offset: 0x001C02C0
		public unsafe override void GetTypeDefProps(uint td, ushort* szTypeDef, uint cchTypeDef, uint* pchTypeDef, uint* pdwTypeDefFlags, uint* ptkExtends)
		{
			if (td >> 24 != 2U)
			{
				throw new ArgumentException();
			}
			TypeDef typeDef = this.tokenToTypeDef[td];
			RawTypeDefRow rawTypeDefRow = this.metadata.TablesHeap.TypeDefTable[td & 16777215U];
			if (pdwTypeDefFlags != null)
			{
				*pdwTypeDefFlags = rawTypeDefRow.Flags;
			}
			if (ptkExtends != null)
			{
				*ptkExtends = rawTypeDefRow.Extends;
			}
			base.CopyTypeName(typeDef.Namespace, typeDef.Name, szTypeDef, cchTypeDef, pchTypeDef);
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x001C034C File Offset: 0x001C034C
		public unsafe override void GetNestedClassProps(uint tdNestedClass, uint* ptdEnclosingClass)
		{
			if (tdNestedClass >> 24 != 2U)
			{
				throw new ArgumentException();
			}
			TypeDef declaringType = this.tokenToTypeDef[tdNestedClass].DeclaringType;
			if (ptdEnclosingClass != null)
			{
				if (declaringType == null)
				{
					*ptdEnclosingClass = 0U;
					return;
				}
				*ptdEnclosingClass = new MDToken(Table.TypeDef, this.metadata.GetRid(declaringType)).Raw;
			}
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x001C03AC File Offset: 0x001C03AC
		void IMetaDataEmit.GetTokenFromSig(IntPtr pvSig, uint cbSig, out uint pmsig)
		{
			pmsig = 285212672U;
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x001C03B8 File Offset: 0x001C03B8
		void IMetaDataEmit.SetModuleProps(string szName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x001C03C0 File Offset: 0x001C03C0
		void IMetaDataEmit.Save(string szFile, uint dwSaveFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x001C03C8 File Offset: 0x001C03C8
		void IMetaDataEmit.SaveToStream(IStream pIStream, uint dwSaveFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x001C03D0 File Offset: 0x001C03D0
		void IMetaDataEmit.GetSaveSize(int fSave, out uint pdwSaveSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x001C03D8 File Offset: 0x001C03D8
		void IMetaDataEmit.DefineTypeDef(string szTypeDef, uint dwTypeDefFlags, uint tkExtends, uint[] rtkImplements, out uint ptd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x001C03E0 File Offset: 0x001C03E0
		void IMetaDataEmit.DefineNestedType(string szTypeDef, uint dwTypeDefFlags, uint tkExtends, uint[] rtkImplements, uint tdEncloser, out uint ptd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x001C03E8 File Offset: 0x001C03E8
		void IMetaDataEmit.SetHandler(object pUnk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x001C03F0 File Offset: 0x001C03F0
		void IMetaDataEmit.DefineMethod(uint td, string szName, uint dwMethodFlags, IntPtr pvSigBlob, uint cbSigBlob, uint ulCodeRVA, uint dwImplFlags, out uint pmd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x001C03F8 File Offset: 0x001C03F8
		void IMetaDataEmit.DefineMethodImpl(uint td, uint tkBody, uint tkDecl)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CFA RID: 23802 RVA: 0x001C0400 File Offset: 0x001C0400
		void IMetaDataEmit.DefineTypeRefByName(uint tkResolutionScope, string szName, out uint ptr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x001C0408 File Offset: 0x001C0408
		void IMetaDataEmit.DefineImportType(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint tdImport, IntPtr pAssemEmit, out uint ptr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x001C0410 File Offset: 0x001C0410
		void IMetaDataEmit.DefineMemberRef(uint tkImport, string szName, IntPtr pvSigBlob, uint cbSigBlob, out uint pmr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x001C0418 File Offset: 0x001C0418
		void IMetaDataEmit.DefineImportMember(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport pImport, uint mbMember, IntPtr pAssemEmit, uint tkParent, out uint pmr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x001C0420 File Offset: 0x001C0420
		void IMetaDataEmit.DefineEvent(uint td, string szEvent, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, uint[] rmdOtherMethods, out uint pmdEvent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x001C0428 File Offset: 0x001C0428
		void IMetaDataEmit.SetClassLayout(uint td, uint dwPackSize, IntPtr rFieldOffsets, uint ulClassSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x001C0430 File Offset: 0x001C0430
		void IMetaDataEmit.DeleteClassLayout(uint td)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x001C0438 File Offset: 0x001C0438
		void IMetaDataEmit.SetFieldMarshal(uint tk, IntPtr pvNativeType, uint cbNativeType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x001C0440 File Offset: 0x001C0440
		void IMetaDataEmit.DeleteFieldMarshal(uint tk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x001C0448 File Offset: 0x001C0448
		void IMetaDataEmit.DefinePermissionSet(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission, out uint ppm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x001C0450 File Offset: 0x001C0450
		void IMetaDataEmit.SetRVA(uint md, uint ulRVA)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x001C0458 File Offset: 0x001C0458
		void IMetaDataEmit.DefineModuleRef(string szName, out uint pmur)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x001C0460 File Offset: 0x001C0460
		void IMetaDataEmit.SetParent(uint mr, uint tk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x001C0468 File Offset: 0x001C0468
		void IMetaDataEmit.GetTokenFromTypeSpec(IntPtr pvSig, uint cbSig, out uint ptypespec)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x001C0470 File Offset: 0x001C0470
		void IMetaDataEmit.SaveToMemory(out IntPtr pbData, uint cbData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x001C0478 File Offset: 0x001C0478
		void IMetaDataEmit.DefineUserString(string szString, uint cchString, out uint pstk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x001C0480 File Offset: 0x001C0480
		void IMetaDataEmit.DeleteToken(uint tkObj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x001C0488 File Offset: 0x001C0488
		void IMetaDataEmit.SetMethodProps(uint md, uint dwMethodFlags, uint ulCodeRVA, uint dwImplFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x001C0490 File Offset: 0x001C0490
		void IMetaDataEmit.SetTypeDefProps(uint td, uint dwTypeDefFlags, uint tkExtends, uint[] rtkImplements)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D0D RID: 23821 RVA: 0x001C0498 File Offset: 0x001C0498
		void IMetaDataEmit.SetEventProps(uint ev, uint dwEventFlags, uint tkEventType, uint mdAddOn, uint mdRemoveOn, uint mdFire, uint[] rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x001C04A0 File Offset: 0x001C04A0
		void IMetaDataEmit.SetPermissionSetProps(uint tk, uint dwAction, IntPtr pvPermission, uint cbPermission, out uint ppm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D0F RID: 23823 RVA: 0x001C04A8 File Offset: 0x001C04A8
		void IMetaDataEmit.DefinePinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D10 RID: 23824 RVA: 0x001C04B0 File Offset: 0x001C04B0
		void IMetaDataEmit.SetPinvokeMap(uint tk, uint dwMappingFlags, string szImportName, uint mrImportDLL)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x001C04B8 File Offset: 0x001C04B8
		void IMetaDataEmit.DeletePinvokeMap(uint tk)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D12 RID: 23826 RVA: 0x001C04C0 File Offset: 0x001C04C0
		void IMetaDataEmit.DefineCustomAttribute(uint tkOwner, uint tkCtor, IntPtr pCustomAttribute, uint cbCustomAttribute, out uint pcv)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D13 RID: 23827 RVA: 0x001C04C8 File Offset: 0x001C04C8
		void IMetaDataEmit.SetCustomAttributeValue(uint pcv, IntPtr pCustomAttribute, uint cbCustomAttribute)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x001C04D0 File Offset: 0x001C04D0
		void IMetaDataEmit.DefineField(uint td, string szName, uint dwFieldFlags, IntPtr pvSigBlob, uint cbSigBlob, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, out uint pmd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x001C04D8 File Offset: 0x001C04D8
		void IMetaDataEmit.DefineProperty(uint td, string szProperty, uint dwPropFlags, IntPtr pvSig, uint cbSig, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, uint[] rmdOtherMethods, out uint pmdProp)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D16 RID: 23830 RVA: 0x001C04E0 File Offset: 0x001C04E0
		void IMetaDataEmit.DefineParam(uint md, uint ulParamSeq, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, out uint ppd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x001C04E8 File Offset: 0x001C04E8
		void IMetaDataEmit.SetFieldProps(uint fd, uint dwFieldFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x001C04F0 File Offset: 0x001C04F0
		void IMetaDataEmit.SetPropertyProps(uint pr, uint dwPropFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue, uint mdSetter, uint mdGetter, uint[] rmdOtherMethods)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x001C04F8 File Offset: 0x001C04F8
		void IMetaDataEmit.SetParamProps(uint pd, string szName, uint dwParamFlags, uint dwCPlusTypeFlag, IntPtr pValue, uint cchValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x001C0500 File Offset: 0x001C0500
		void IMetaDataEmit.DefineSecurityAttributeSet(uint tkObj, IntPtr rSecAttrs, uint cSecAttrs, out uint pulErrorAttr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x001C0508 File Offset: 0x001C0508
		void IMetaDataEmit.ApplyEditAndContinue(object pImport)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x001C0510 File Offset: 0x001C0510
		void IMetaDataEmit.TranslateSigWithScope(IntPtr pAssemImport, IntPtr pbHashValue, uint cbHashValue, IMetaDataImport import, IntPtr pbSigBlob, uint cbSigBlob, IntPtr pAssemEmit, IMetaDataEmit emit, IntPtr pvTranslatedSig, uint cbTranslatedSigMax, out uint pcbTranslatedSig)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x001C0518 File Offset: 0x001C0518
		void IMetaDataEmit.SetMethodImplFlags(uint md, uint dwImplFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x001C0520 File Offset: 0x001C0520
		void IMetaDataEmit.SetFieldRVA(uint fd, uint ulRVA)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x001C0528 File Offset: 0x001C0528
		void IMetaDataEmit.Merge(IMetaDataImport pImport, IntPtr pHostMapToken, object pHandler)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D20 RID: 23840 RVA: 0x001C0530 File Offset: 0x001C0530
		void IMetaDataEmit.MergeEnd()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002D52 RID: 11602
		private readonly dnlib.DotNet.Writer.Metadata metadata;

		// Token: 0x04002D53 RID: 11603
		private readonly Dictionary<uint, TypeDef> tokenToTypeDef;

		// Token: 0x04002D54 RID: 11604
		private readonly Dictionary<uint, MethodDef> tokenToMethodDef;
	}
}
