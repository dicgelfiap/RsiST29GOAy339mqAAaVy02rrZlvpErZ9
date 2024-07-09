using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200099A RID: 2458
	[ComVisible(true)]
	public sealed class RawRowEqualityComparer : IEqualityComparer<RawModuleRow>, IEqualityComparer<RawTypeRefRow>, IEqualityComparer<RawTypeDefRow>, IEqualityComparer<RawFieldPtrRow>, IEqualityComparer<RawFieldRow>, IEqualityComparer<RawMethodPtrRow>, IEqualityComparer<RawMethodRow>, IEqualityComparer<RawParamPtrRow>, IEqualityComparer<RawParamRow>, IEqualityComparer<RawInterfaceImplRow>, IEqualityComparer<RawMemberRefRow>, IEqualityComparer<RawConstantRow>, IEqualityComparer<RawCustomAttributeRow>, IEqualityComparer<RawFieldMarshalRow>, IEqualityComparer<RawDeclSecurityRow>, IEqualityComparer<RawClassLayoutRow>, IEqualityComparer<RawFieldLayoutRow>, IEqualityComparer<RawStandAloneSigRow>, IEqualityComparer<RawEventMapRow>, IEqualityComparer<RawEventPtrRow>, IEqualityComparer<RawEventRow>, IEqualityComparer<RawPropertyMapRow>, IEqualityComparer<RawPropertyPtrRow>, IEqualityComparer<RawPropertyRow>, IEqualityComparer<RawMethodSemanticsRow>, IEqualityComparer<RawMethodImplRow>, IEqualityComparer<RawModuleRefRow>, IEqualityComparer<RawTypeSpecRow>, IEqualityComparer<RawImplMapRow>, IEqualityComparer<RawFieldRVARow>, IEqualityComparer<RawENCLogRow>, IEqualityComparer<RawENCMapRow>, IEqualityComparer<RawAssemblyRow>, IEqualityComparer<RawAssemblyProcessorRow>, IEqualityComparer<RawAssemblyOSRow>, IEqualityComparer<RawAssemblyRefRow>, IEqualityComparer<RawAssemblyRefProcessorRow>, IEqualityComparer<RawAssemblyRefOSRow>, IEqualityComparer<RawFileRow>, IEqualityComparer<RawExportedTypeRow>, IEqualityComparer<RawManifestResourceRow>, IEqualityComparer<RawNestedClassRow>, IEqualityComparer<RawGenericParamRow>, IEqualityComparer<RawMethodSpecRow>, IEqualityComparer<RawGenericParamConstraintRow>, IEqualityComparer<RawDocumentRow>, IEqualityComparer<RawMethodDebugInformationRow>, IEqualityComparer<RawLocalScopeRow>, IEqualityComparer<RawLocalVariableRow>, IEqualityComparer<RawLocalConstantRow>, IEqualityComparer<RawImportScopeRow>, IEqualityComparer<RawStateMachineMethodRow>, IEqualityComparer<RawCustomDebugInformationRow>
	{
		// Token: 0x06005EE1 RID: 24289 RVA: 0x001C7408 File Offset: 0x001C7408
		private static int rol(uint val, int shift)
		{
			return (int)(val << shift | val >> 32 - shift);
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x001C741C File Offset: 0x001C741C
		public bool Equals(RawModuleRow x, RawModuleRow y)
		{
			return x.Generation == y.Generation && x.Name == y.Name && x.Mvid == y.Mvid && x.EncId == y.EncId && x.EncBaseId == y.EncBaseId;
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x001C7484 File Offset: 0x001C7484
		public int GetHashCode(RawModuleRow obj)
		{
			return (int)obj.Generation + RawRowEqualityComparer.rol(obj.Name, 3) + RawRowEqualityComparer.rol(obj.Mvid, 7) + RawRowEqualityComparer.rol(obj.EncId, 11) + RawRowEqualityComparer.rol(obj.EncBaseId, 15);
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x001C74C4 File Offset: 0x001C74C4
		public bool Equals(RawTypeRefRow x, RawTypeRefRow y)
		{
			return x.ResolutionScope == y.ResolutionScope && x.Name == y.Name && x.Namespace == y.Namespace;
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x001C74F8 File Offset: 0x001C74F8
		public int GetHashCode(RawTypeRefRow obj)
		{
			return (int)(obj.ResolutionScope + (uint)RawRowEqualityComparer.rol(obj.Name, 3) + (uint)RawRowEqualityComparer.rol(obj.Namespace, 7));
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x001C751C File Offset: 0x001C751C
		public bool Equals(RawTypeDefRow x, RawTypeDefRow y)
		{
			return x.Flags == y.Flags && x.Name == y.Name && x.Namespace == y.Namespace && x.Extends == y.Extends && x.FieldList == y.FieldList && x.MethodList == y.MethodList;
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x001C7594 File Offset: 0x001C7594
		public int GetHashCode(RawTypeDefRow obj)
		{
			return (int)(obj.Flags + (uint)RawRowEqualityComparer.rol(obj.Name, 3) + (uint)RawRowEqualityComparer.rol(obj.Namespace, 7) + (uint)RawRowEqualityComparer.rol(obj.Extends, 11) + (uint)RawRowEqualityComparer.rol(obj.FieldList, 15) + (uint)RawRowEqualityComparer.rol(obj.MethodList, 19));
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x001C75F0 File Offset: 0x001C75F0
		public bool Equals(RawFieldPtrRow x, RawFieldPtrRow y)
		{
			return x.Field == y.Field;
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x001C7600 File Offset: 0x001C7600
		public int GetHashCode(RawFieldPtrRow obj)
		{
			return (int)obj.Field;
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x001C7608 File Offset: 0x001C7608
		public bool Equals(RawFieldRow x, RawFieldRow y)
		{
			return x.Flags == y.Flags && x.Name == y.Name && x.Signature == y.Signature;
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x001C763C File Offset: 0x001C763C
		public int GetHashCode(RawFieldRow obj)
		{
			return (int)obj.Flags + RawRowEqualityComparer.rol(obj.Name, 3) + RawRowEqualityComparer.rol(obj.Signature, 7);
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x001C7660 File Offset: 0x001C7660
		public bool Equals(RawMethodPtrRow x, RawMethodPtrRow y)
		{
			return x.Method == y.Method;
		}

		// Token: 0x06005EED RID: 24301 RVA: 0x001C7670 File Offset: 0x001C7670
		public int GetHashCode(RawMethodPtrRow obj)
		{
			return (int)obj.Method;
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x001C7678 File Offset: 0x001C7678
		public bool Equals(RawMethodRow x, RawMethodRow y)
		{
			return x.RVA == y.RVA && x.ImplFlags == y.ImplFlags && x.Flags == y.Flags && x.Name == y.Name && x.Signature == y.Signature && x.ParamList == y.ParamList;
		}

		// Token: 0x06005EEF RID: 24303 RVA: 0x001C76F0 File Offset: 0x001C76F0
		public int GetHashCode(RawMethodRow obj)
		{
			return (int)(obj.RVA + (uint)RawRowEqualityComparer.rol((uint)obj.ImplFlags, 3) + (uint)RawRowEqualityComparer.rol((uint)obj.Flags, 7) + (uint)RawRowEqualityComparer.rol(obj.Name, 11) + (uint)RawRowEqualityComparer.rol(obj.Signature, 15) + (uint)RawRowEqualityComparer.rol(obj.ParamList, 19));
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x001C774C File Offset: 0x001C774C
		public bool Equals(RawParamPtrRow x, RawParamPtrRow y)
		{
			return x.Param == y.Param;
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x001C775C File Offset: 0x001C775C
		public int GetHashCode(RawParamPtrRow obj)
		{
			return (int)obj.Param;
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x001C7764 File Offset: 0x001C7764
		public bool Equals(RawParamRow x, RawParamRow y)
		{
			return x.Flags == y.Flags && x.Sequence == y.Sequence && x.Name == y.Name;
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x001C7798 File Offset: 0x001C7798
		public int GetHashCode(RawParamRow obj)
		{
			return (int)obj.Flags + RawRowEqualityComparer.rol((uint)obj.Sequence, 3) + RawRowEqualityComparer.rol(obj.Name, 7);
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x001C77BC File Offset: 0x001C77BC
		public bool Equals(RawInterfaceImplRow x, RawInterfaceImplRow y)
		{
			return x.Class == y.Class && x.Interface == y.Interface;
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x001C77E0 File Offset: 0x001C77E0
		public int GetHashCode(RawInterfaceImplRow obj)
		{
			return (int)(obj.Class + (uint)RawRowEqualityComparer.rol(obj.Interface, 3));
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x001C77F8 File Offset: 0x001C77F8
		public bool Equals(RawMemberRefRow x, RawMemberRefRow y)
		{
			return x.Class == y.Class && x.Name == y.Name && x.Signature == y.Signature;
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x001C782C File Offset: 0x001C782C
		public int GetHashCode(RawMemberRefRow obj)
		{
			return (int)(obj.Class + (uint)RawRowEqualityComparer.rol(obj.Name, 3) + (uint)RawRowEqualityComparer.rol(obj.Signature, 7));
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x001C7850 File Offset: 0x001C7850
		public bool Equals(RawConstantRow x, RawConstantRow y)
		{
			return x.Type == y.Type && x.Padding == y.Padding && x.Parent == y.Parent && x.Value == y.Value;
		}

		// Token: 0x06005EF9 RID: 24313 RVA: 0x001C78A4 File Offset: 0x001C78A4
		public int GetHashCode(RawConstantRow obj)
		{
			return (int)obj.Type + RawRowEqualityComparer.rol((uint)obj.Padding, 3) + RawRowEqualityComparer.rol(obj.Parent, 7) + RawRowEqualityComparer.rol(obj.Value, 11);
		}

		// Token: 0x06005EFA RID: 24314 RVA: 0x001C78D4 File Offset: 0x001C78D4
		public bool Equals(RawCustomAttributeRow x, RawCustomAttributeRow y)
		{
			return x.Parent == y.Parent && x.Type == y.Type && x.Value == y.Value;
		}

		// Token: 0x06005EFB RID: 24315 RVA: 0x001C7908 File Offset: 0x001C7908
		public int GetHashCode(RawCustomAttributeRow obj)
		{
			return (int)(obj.Parent + (uint)RawRowEqualityComparer.rol(obj.Type, 3) + (uint)RawRowEqualityComparer.rol(obj.Value, 7));
		}

		// Token: 0x06005EFC RID: 24316 RVA: 0x001C792C File Offset: 0x001C792C
		public bool Equals(RawFieldMarshalRow x, RawFieldMarshalRow y)
		{
			return x.Parent == y.Parent && x.NativeType == y.NativeType;
		}

		// Token: 0x06005EFD RID: 24317 RVA: 0x001C7950 File Offset: 0x001C7950
		public int GetHashCode(RawFieldMarshalRow obj)
		{
			return (int)(obj.Parent + (uint)RawRowEqualityComparer.rol(obj.NativeType, 3));
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x001C7968 File Offset: 0x001C7968
		public bool Equals(RawDeclSecurityRow x, RawDeclSecurityRow y)
		{
			return x.Action == y.Action && x.Parent == y.Parent && x.PermissionSet == y.PermissionSet;
		}

		// Token: 0x06005EFF RID: 24319 RVA: 0x001C799C File Offset: 0x001C799C
		public int GetHashCode(RawDeclSecurityRow obj)
		{
			return (int)obj.Action + RawRowEqualityComparer.rol(obj.Parent, 3) + RawRowEqualityComparer.rol(obj.PermissionSet, 7);
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x001C79C0 File Offset: 0x001C79C0
		public bool Equals(RawClassLayoutRow x, RawClassLayoutRow y)
		{
			return x.PackingSize == y.PackingSize && x.ClassSize == y.ClassSize && x.Parent == y.Parent;
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x001C79F4 File Offset: 0x001C79F4
		public int GetHashCode(RawClassLayoutRow obj)
		{
			return (int)obj.PackingSize + RawRowEqualityComparer.rol(obj.ClassSize, 3) + RawRowEqualityComparer.rol(obj.Parent, 7);
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x001C7A18 File Offset: 0x001C7A18
		public bool Equals(RawFieldLayoutRow x, RawFieldLayoutRow y)
		{
			return x.OffSet == y.OffSet && x.Field == y.Field;
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x001C7A3C File Offset: 0x001C7A3C
		public int GetHashCode(RawFieldLayoutRow obj)
		{
			return (int)(obj.OffSet + (uint)RawRowEqualityComparer.rol(obj.Field, 3));
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x001C7A54 File Offset: 0x001C7A54
		public bool Equals(RawStandAloneSigRow x, RawStandAloneSigRow y)
		{
			return x.Signature == y.Signature;
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x001C7A64 File Offset: 0x001C7A64
		public int GetHashCode(RawStandAloneSigRow obj)
		{
			return (int)obj.Signature;
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x001C7A6C File Offset: 0x001C7A6C
		public bool Equals(RawEventMapRow x, RawEventMapRow y)
		{
			return x.Parent == y.Parent && x.EventList == y.EventList;
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x001C7A90 File Offset: 0x001C7A90
		public int GetHashCode(RawEventMapRow obj)
		{
			return (int)(obj.Parent + (uint)RawRowEqualityComparer.rol(obj.EventList, 3));
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x001C7AA8 File Offset: 0x001C7AA8
		public bool Equals(RawEventPtrRow x, RawEventPtrRow y)
		{
			return x.Event == y.Event;
		}

		// Token: 0x06005F09 RID: 24329 RVA: 0x001C7AB8 File Offset: 0x001C7AB8
		public int GetHashCode(RawEventPtrRow obj)
		{
			return (int)obj.Event;
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x001C7AC0 File Offset: 0x001C7AC0
		public bool Equals(RawEventRow x, RawEventRow y)
		{
			return x.EventFlags == y.EventFlags && x.Name == y.Name && x.EventType == y.EventType;
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x001C7AF4 File Offset: 0x001C7AF4
		public int GetHashCode(RawEventRow obj)
		{
			return (int)obj.EventFlags + RawRowEqualityComparer.rol(obj.Name, 3) + RawRowEqualityComparer.rol(obj.EventType, 7);
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x001C7B18 File Offset: 0x001C7B18
		public bool Equals(RawPropertyMapRow x, RawPropertyMapRow y)
		{
			return x.Parent == y.Parent && x.PropertyList == y.PropertyList;
		}

		// Token: 0x06005F0D RID: 24333 RVA: 0x001C7B3C File Offset: 0x001C7B3C
		public int GetHashCode(RawPropertyMapRow obj)
		{
			return (int)(obj.Parent + (uint)RawRowEqualityComparer.rol(obj.PropertyList, 3));
		}

		// Token: 0x06005F0E RID: 24334 RVA: 0x001C7B54 File Offset: 0x001C7B54
		public bool Equals(RawPropertyPtrRow x, RawPropertyPtrRow y)
		{
			return x.Property == y.Property;
		}

		// Token: 0x06005F0F RID: 24335 RVA: 0x001C7B64 File Offset: 0x001C7B64
		public int GetHashCode(RawPropertyPtrRow obj)
		{
			return (int)obj.Property;
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x001C7B6C File Offset: 0x001C7B6C
		public bool Equals(RawPropertyRow x, RawPropertyRow y)
		{
			return x.PropFlags == y.PropFlags && x.Name == y.Name && x.Type == y.Type;
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x001C7BA0 File Offset: 0x001C7BA0
		public int GetHashCode(RawPropertyRow obj)
		{
			return (int)obj.PropFlags + RawRowEqualityComparer.rol(obj.Name, 3) + RawRowEqualityComparer.rol(obj.Type, 7);
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x001C7BC4 File Offset: 0x001C7BC4
		public bool Equals(RawMethodSemanticsRow x, RawMethodSemanticsRow y)
		{
			return x.Semantic == y.Semantic && x.Method == y.Method && x.Association == y.Association;
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x001C7BF8 File Offset: 0x001C7BF8
		public int GetHashCode(RawMethodSemanticsRow obj)
		{
			return (int)obj.Semantic + RawRowEqualityComparer.rol(obj.Method, 3) + RawRowEqualityComparer.rol(obj.Association, 7);
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x001C7C1C File Offset: 0x001C7C1C
		public bool Equals(RawMethodImplRow x, RawMethodImplRow y)
		{
			return x.Class == y.Class && x.MethodBody == y.MethodBody && x.MethodDeclaration == y.MethodDeclaration;
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x001C7C50 File Offset: 0x001C7C50
		public int GetHashCode(RawMethodImplRow obj)
		{
			return (int)(obj.Class + (uint)RawRowEqualityComparer.rol(obj.MethodBody, 3) + (uint)RawRowEqualityComparer.rol(obj.MethodDeclaration, 7));
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x001C7C74 File Offset: 0x001C7C74
		public bool Equals(RawModuleRefRow x, RawModuleRefRow y)
		{
			return x.Name == y.Name;
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x001C7C84 File Offset: 0x001C7C84
		public int GetHashCode(RawModuleRefRow obj)
		{
			return (int)obj.Name;
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x001C7C8C File Offset: 0x001C7C8C
		public bool Equals(RawTypeSpecRow x, RawTypeSpecRow y)
		{
			return x.Signature == y.Signature;
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x001C7C9C File Offset: 0x001C7C9C
		public int GetHashCode(RawTypeSpecRow obj)
		{
			return (int)obj.Signature;
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x001C7CA4 File Offset: 0x001C7CA4
		public bool Equals(RawImplMapRow x, RawImplMapRow y)
		{
			return x.MappingFlags == y.MappingFlags && x.MemberForwarded == y.MemberForwarded && x.ImportName == y.ImportName && x.ImportScope == y.ImportScope;
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x001C7CF8 File Offset: 0x001C7CF8
		public int GetHashCode(RawImplMapRow obj)
		{
			return (int)obj.MappingFlags + RawRowEqualityComparer.rol(obj.MemberForwarded, 3) + RawRowEqualityComparer.rol(obj.ImportName, 7) + RawRowEqualityComparer.rol(obj.ImportScope, 11);
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x001C7D28 File Offset: 0x001C7D28
		public bool Equals(RawFieldRVARow x, RawFieldRVARow y)
		{
			return x.RVA == y.RVA && x.Field == y.Field;
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x001C7D4C File Offset: 0x001C7D4C
		public int GetHashCode(RawFieldRVARow obj)
		{
			return (int)(obj.RVA + (uint)RawRowEqualityComparer.rol(obj.Field, 3));
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x001C7D64 File Offset: 0x001C7D64
		public bool Equals(RawENCLogRow x, RawENCLogRow y)
		{
			return x.Token == y.Token && x.FuncCode == y.FuncCode;
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x001C7D88 File Offset: 0x001C7D88
		public int GetHashCode(RawENCLogRow obj)
		{
			return (int)(obj.Token + (uint)RawRowEqualityComparer.rol(obj.FuncCode, 3));
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x001C7DA0 File Offset: 0x001C7DA0
		public bool Equals(RawENCMapRow x, RawENCMapRow y)
		{
			return x.Token == y.Token;
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x001C7DB0 File Offset: 0x001C7DB0
		public int GetHashCode(RawENCMapRow obj)
		{
			return (int)obj.Token;
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x001C7DB8 File Offset: 0x001C7DB8
		public bool Equals(RawAssemblyRow x, RawAssemblyRow y)
		{
			return x.HashAlgId == y.HashAlgId && x.MajorVersion == y.MajorVersion && x.MinorVersion == y.MinorVersion && x.BuildNumber == y.BuildNumber && x.RevisionNumber == y.RevisionNumber && x.Flags == y.Flags && x.PublicKey == y.PublicKey && x.Name == y.Name && x.Locale == y.Locale;
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x001C7E64 File Offset: 0x001C7E64
		public int GetHashCode(RawAssemblyRow obj)
		{
			return (int)(obj.HashAlgId + (uint)RawRowEqualityComparer.rol((uint)obj.MajorVersion, 3) + (uint)RawRowEqualityComparer.rol((uint)obj.MinorVersion, 7) + (uint)RawRowEqualityComparer.rol((uint)obj.BuildNumber, 11) + (uint)RawRowEqualityComparer.rol((uint)obj.RevisionNumber, 15) + (uint)RawRowEqualityComparer.rol(obj.Flags, 19) + (uint)RawRowEqualityComparer.rol(obj.PublicKey, 23) + (uint)RawRowEqualityComparer.rol(obj.Name, 27) + (uint)RawRowEqualityComparer.rol(obj.Locale, 31));
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x001C7EEC File Offset: 0x001C7EEC
		public bool Equals(RawAssemblyProcessorRow x, RawAssemblyProcessorRow y)
		{
			return x.Processor == y.Processor;
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x001C7EFC File Offset: 0x001C7EFC
		public int GetHashCode(RawAssemblyProcessorRow obj)
		{
			return (int)obj.Processor;
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x001C7F04 File Offset: 0x001C7F04
		public bool Equals(RawAssemblyOSRow x, RawAssemblyOSRow y)
		{
			return x.OSPlatformId == y.OSPlatformId && x.OSMajorVersion == y.OSMajorVersion && x.OSMinorVersion == y.OSMinorVersion;
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x001C7F38 File Offset: 0x001C7F38
		public int GetHashCode(RawAssemblyOSRow obj)
		{
			return (int)(obj.OSPlatformId + (uint)RawRowEqualityComparer.rol(obj.OSMajorVersion, 3) + (uint)RawRowEqualityComparer.rol(obj.OSMinorVersion, 7));
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x001C7F5C File Offset: 0x001C7F5C
		public bool Equals(RawAssemblyRefRow x, RawAssemblyRefRow y)
		{
			return x.MajorVersion == y.MajorVersion && x.MinorVersion == y.MinorVersion && x.BuildNumber == y.BuildNumber && x.RevisionNumber == y.RevisionNumber && x.Flags == y.Flags && x.PublicKeyOrToken == y.PublicKeyOrToken && x.Name == y.Name && x.Locale == y.Locale && x.HashValue == y.HashValue;
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x001C8008 File Offset: 0x001C8008
		public int GetHashCode(RawAssemblyRefRow obj)
		{
			return (int)obj.MajorVersion + RawRowEqualityComparer.rol((uint)obj.MinorVersion, 3) + RawRowEqualityComparer.rol((uint)obj.BuildNumber, 7) + RawRowEqualityComparer.rol((uint)obj.RevisionNumber, 11) + RawRowEqualityComparer.rol(obj.Flags, 15) + RawRowEqualityComparer.rol(obj.PublicKeyOrToken, 19) + RawRowEqualityComparer.rol(obj.Name, 23) + RawRowEqualityComparer.rol(obj.Locale, 27) + RawRowEqualityComparer.rol(obj.HashValue, 31);
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x001C8090 File Offset: 0x001C8090
		public bool Equals(RawAssemblyRefProcessorRow x, RawAssemblyRefProcessorRow y)
		{
			return x.Processor == y.Processor && x.AssemblyRef == y.AssemblyRef;
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x001C80B4 File Offset: 0x001C80B4
		public int GetHashCode(RawAssemblyRefProcessorRow obj)
		{
			return (int)(obj.Processor + (uint)RawRowEqualityComparer.rol(obj.AssemblyRef, 3));
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x001C80CC File Offset: 0x001C80CC
		public bool Equals(RawAssemblyRefOSRow x, RawAssemblyRefOSRow y)
		{
			return x.OSPlatformId == y.OSPlatformId && x.OSMajorVersion == y.OSMajorVersion && x.OSMinorVersion == y.OSMinorVersion && x.AssemblyRef == y.AssemblyRef;
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x001C8120 File Offset: 0x001C8120
		public int GetHashCode(RawAssemblyRefOSRow obj)
		{
			return (int)(obj.OSPlatformId + (uint)RawRowEqualityComparer.rol(obj.OSMajorVersion, 3) + (uint)RawRowEqualityComparer.rol(obj.OSMinorVersion, 7) + (uint)RawRowEqualityComparer.rol(obj.AssemblyRef, 11));
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x001C8150 File Offset: 0x001C8150
		public bool Equals(RawFileRow x, RawFileRow y)
		{
			return x.Flags == y.Flags && x.Name == y.Name && x.HashValue == y.HashValue;
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x001C8184 File Offset: 0x001C8184
		public int GetHashCode(RawFileRow obj)
		{
			return (int)(obj.Flags + (uint)RawRowEqualityComparer.rol(obj.Name, 3) + (uint)RawRowEqualityComparer.rol(obj.HashValue, 7));
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x001C81A8 File Offset: 0x001C81A8
		public bool Equals(RawExportedTypeRow x, RawExportedTypeRow y)
		{
			return x.Flags == y.Flags && x.TypeDefId == y.TypeDefId && x.TypeName == y.TypeName && x.TypeNamespace == y.TypeNamespace && x.Implementation == y.Implementation;
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x001C8210 File Offset: 0x001C8210
		public int GetHashCode(RawExportedTypeRow obj)
		{
			return (int)(obj.Flags + (uint)RawRowEqualityComparer.rol(obj.TypeDefId, 3) + (uint)RawRowEqualityComparer.rol(obj.TypeName, 7) + (uint)RawRowEqualityComparer.rol(obj.TypeNamespace, 11) + (uint)RawRowEqualityComparer.rol(obj.Implementation, 15));
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x001C8250 File Offset: 0x001C8250
		public bool Equals(RawManifestResourceRow x, RawManifestResourceRow y)
		{
			return x.Offset == y.Offset && x.Flags == y.Flags && x.Name == y.Name && x.Implementation == y.Implementation;
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x001C82A4 File Offset: 0x001C82A4
		public int GetHashCode(RawManifestResourceRow obj)
		{
			return (int)(obj.Offset + (uint)RawRowEqualityComparer.rol(obj.Flags, 3) + (uint)RawRowEqualityComparer.rol(obj.Name, 7) + (uint)RawRowEqualityComparer.rol(obj.Implementation, 11));
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x001C82D4 File Offset: 0x001C82D4
		public bool Equals(RawNestedClassRow x, RawNestedClassRow y)
		{
			return x.NestedClass == y.NestedClass && x.EnclosingClass == y.EnclosingClass;
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x001C82F8 File Offset: 0x001C82F8
		public int GetHashCode(RawNestedClassRow obj)
		{
			return (int)(obj.NestedClass + (uint)RawRowEqualityComparer.rol(obj.EnclosingClass, 3));
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x001C8310 File Offset: 0x001C8310
		public bool Equals(RawGenericParamRow x, RawGenericParamRow y)
		{
			return x.Number == y.Number && x.Flags == y.Flags && x.Owner == y.Owner && x.Name == y.Name && x.Kind == y.Kind;
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x001C8378 File Offset: 0x001C8378
		public int GetHashCode(RawGenericParamRow obj)
		{
			return (int)obj.Number + RawRowEqualityComparer.rol((uint)obj.Flags, 3) + RawRowEqualityComparer.rol(obj.Owner, 7) + RawRowEqualityComparer.rol(obj.Name, 11) + RawRowEqualityComparer.rol(obj.Kind, 15);
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x001C83B8 File Offset: 0x001C83B8
		public bool Equals(RawMethodSpecRow x, RawMethodSpecRow y)
		{
			return x.Method == y.Method && x.Instantiation == y.Instantiation;
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x001C83DC File Offset: 0x001C83DC
		public int GetHashCode(RawMethodSpecRow obj)
		{
			return (int)(obj.Method + (uint)RawRowEqualityComparer.rol(obj.Instantiation, 3));
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x001C83F4 File Offset: 0x001C83F4
		public bool Equals(RawGenericParamConstraintRow x, RawGenericParamConstraintRow y)
		{
			return x.Owner == y.Owner && x.Constraint == y.Constraint;
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x001C8418 File Offset: 0x001C8418
		public int GetHashCode(RawGenericParamConstraintRow obj)
		{
			return (int)(obj.Owner + (uint)RawRowEqualityComparer.rol(obj.Constraint, 3));
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x001C8430 File Offset: 0x001C8430
		public bool Equals(RawDocumentRow x, RawDocumentRow y)
		{
			return x.Name == y.Name && x.HashAlgorithm == y.HashAlgorithm && x.Hash == y.Hash && x.Language == y.Language;
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x001C8484 File Offset: 0x001C8484
		public int GetHashCode(RawDocumentRow obj)
		{
			return (int)(obj.Name + (uint)RawRowEqualityComparer.rol(obj.HashAlgorithm, 3) + (uint)RawRowEqualityComparer.rol(obj.Hash, 7) + (uint)RawRowEqualityComparer.rol(obj.Language, 11));
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x001C84B4 File Offset: 0x001C84B4
		public bool Equals(RawMethodDebugInformationRow x, RawMethodDebugInformationRow y)
		{
			return x.Document == y.Document && x.SequencePoints == y.SequencePoints;
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x001C84D8 File Offset: 0x001C84D8
		public int GetHashCode(RawMethodDebugInformationRow obj)
		{
			return (int)(obj.Document + (uint)RawRowEqualityComparer.rol(obj.SequencePoints, 3));
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x001C84F0 File Offset: 0x001C84F0
		public bool Equals(RawLocalScopeRow x, RawLocalScopeRow y)
		{
			return x.Method == y.Method && x.ImportScope == y.ImportScope && x.VariableList == y.VariableList && x.ConstantList == y.ConstantList && x.StartOffset == y.StartOffset && x.Length == y.Length;
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x001C8568 File Offset: 0x001C8568
		public int GetHashCode(RawLocalScopeRow obj)
		{
			return (int)(obj.Method + (uint)RawRowEqualityComparer.rol(obj.ImportScope, 3) + (uint)RawRowEqualityComparer.rol(obj.VariableList, 7) + (uint)RawRowEqualityComparer.rol(obj.ConstantList, 11) + (uint)RawRowEqualityComparer.rol(obj.StartOffset, 15) + (uint)RawRowEqualityComparer.rol(obj.Length, 19));
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x001C85C4 File Offset: 0x001C85C4
		public bool Equals(RawLocalVariableRow x, RawLocalVariableRow y)
		{
			return x.Attributes == y.Attributes && x.Index == y.Index && x.Name == y.Name;
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x001C85F8 File Offset: 0x001C85F8
		public int GetHashCode(RawLocalVariableRow obj)
		{
			return (int)obj.Attributes + RawRowEqualityComparer.rol((uint)obj.Index, 3) + RawRowEqualityComparer.rol(obj.Name, 7);
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x001C861C File Offset: 0x001C861C
		public bool Equals(RawLocalConstantRow x, RawLocalConstantRow y)
		{
			return x.Name == y.Name && x.Signature == y.Signature;
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x001C8640 File Offset: 0x001C8640
		public int GetHashCode(RawLocalConstantRow obj)
		{
			return (int)(obj.Name + (uint)RawRowEqualityComparer.rol(obj.Signature, 3));
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x001C8658 File Offset: 0x001C8658
		public bool Equals(RawImportScopeRow x, RawImportScopeRow y)
		{
			return x.Parent == y.Parent && x.Imports == y.Imports;
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x001C867C File Offset: 0x001C867C
		public int GetHashCode(RawImportScopeRow obj)
		{
			return (int)(obj.Parent + (uint)RawRowEqualityComparer.rol(obj.Imports, 3));
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x001C8694 File Offset: 0x001C8694
		public bool Equals(RawStateMachineMethodRow x, RawStateMachineMethodRow y)
		{
			return x.MoveNextMethod == y.MoveNextMethod && x.KickoffMethod == y.KickoffMethod;
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x001C86B8 File Offset: 0x001C86B8
		public int GetHashCode(RawStateMachineMethodRow obj)
		{
			return (int)(obj.MoveNextMethod + (uint)RawRowEqualityComparer.rol(obj.KickoffMethod, 3));
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x001C86D0 File Offset: 0x001C86D0
		public bool Equals(RawCustomDebugInformationRow x, RawCustomDebugInformationRow y)
		{
			return x.Parent == y.Parent && x.Kind == y.Kind && x.Value == y.Value;
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x001C8704 File Offset: 0x001C8704
		public int GetHashCode(RawCustomDebugInformationRow obj)
		{
			return (int)(obj.Parent + (uint)RawRowEqualityComparer.rol(obj.Kind, 3) + (uint)RawRowEqualityComparer.rol(obj.Value, 7));
		}

		// Token: 0x04002E56 RID: 11862
		public static readonly RawRowEqualityComparer Instance = new RawRowEqualityComparer();
	}
}
