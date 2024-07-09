using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200098B RID: 2443
	[ComVisible(true)]
	public sealed class DotNetTableSizes
	{
		// Token: 0x06005E0F RID: 24079 RVA: 0x001C3214 File Offset: 0x001C3214
		internal static bool IsSystemTable(Table table)
		{
			return table < Table.Document;
		}

		// Token: 0x06005E10 RID: 24080 RVA: 0x001C321C File Offset: 0x001C321C
		public void InitializeSizes(bool bigStrings, bool bigGuid, bool bigBlob, IList<uint> systemRowCounts, IList<uint> debugRowCounts)
		{
			this.bigStrings = bigStrings;
			this.bigGuid = bigGuid;
			this.bigBlob = bigBlob;
			foreach (TableInfo tableInfo in this.tableInfos)
			{
				IList<uint> rowCounts = DotNetTableSizes.IsSystemTable(tableInfo.Table) ? systemRowCounts : debugRowCounts;
				int num = 0;
				foreach (ColumnInfo columnInfo in tableInfo.Columns)
				{
					columnInfo.Offset = num;
					int size = this.GetSize(columnInfo.ColumnSize, rowCounts);
					columnInfo.Size = size;
					num += size;
				}
				tableInfo.RowSize = num;
			}
		}

		// Token: 0x06005E11 RID: 24081 RVA: 0x001C32DC File Offset: 0x001C32DC
		private int GetSize(ColumnSize columnSize, IList<uint> rowCounts)
		{
			if (ColumnSize.Module <= columnSize && columnSize <= ColumnSize.CustomDebugInformation)
			{
				int num = (int)(columnSize - ColumnSize.Module);
				if (((num >= rowCounts.Count) ? 0U : rowCounts[num]) <= 65535U)
				{
					return 2;
				}
				return 4;
			}
			else if (ColumnSize.TypeDefOrRef <= columnSize && columnSize <= ColumnSize.HasCustomDebugInformation)
			{
				CodedToken codedToken;
				switch (columnSize)
				{
				case ColumnSize.TypeDefOrRef:
					codedToken = CodedToken.TypeDefOrRef;
					break;
				case ColumnSize.HasConstant:
					codedToken = CodedToken.HasConstant;
					break;
				case ColumnSize.HasCustomAttribute:
					codedToken = CodedToken.HasCustomAttribute;
					break;
				case ColumnSize.HasFieldMarshal:
					codedToken = CodedToken.HasFieldMarshal;
					break;
				case ColumnSize.HasDeclSecurity:
					codedToken = CodedToken.HasDeclSecurity;
					break;
				case ColumnSize.MemberRefParent:
					codedToken = CodedToken.MemberRefParent;
					break;
				case ColumnSize.HasSemantic:
					codedToken = CodedToken.HasSemantic;
					break;
				case ColumnSize.MethodDefOrRef:
					codedToken = CodedToken.MethodDefOrRef;
					break;
				case ColumnSize.MemberForwarded:
					codedToken = CodedToken.MemberForwarded;
					break;
				case ColumnSize.Implementation:
					codedToken = CodedToken.Implementation;
					break;
				case ColumnSize.CustomAttributeType:
					codedToken = CodedToken.CustomAttributeType;
					break;
				case ColumnSize.ResolutionScope:
					codedToken = CodedToken.ResolutionScope;
					break;
				case ColumnSize.TypeOrMethodDef:
					codedToken = CodedToken.TypeOrMethodDef;
					break;
				case ColumnSize.HasCustomDebugInformation:
					codedToken = CodedToken.HasCustomDebugInformation;
					break;
				default:
					throw new InvalidOperationException(string.Format("Invalid ColumnSize: {0}", columnSize));
				}
				CodedToken codedToken2 = codedToken;
				uint num2 = 0U;
				foreach (int num3 in codedToken2.TableTypes)
				{
					uint num4 = (num3 >= rowCounts.Count) ? 0U : rowCounts[num3];
					if (num4 > num2)
					{
						num2 = num4;
					}
				}
				if (num2 << codedToken2.Bits <= 65535U)
				{
					return 2;
				}
				return 4;
			}
			else
			{
				switch (columnSize)
				{
				case ColumnSize.Byte:
					return 1;
				case ColumnSize.Int16:
					return 2;
				case ColumnSize.UInt16:
					return 2;
				case ColumnSize.Int32:
					return 4;
				case ColumnSize.UInt32:
					return 4;
				case ColumnSize.Strings:
					if (!this.bigStrings)
					{
						return 2;
					}
					return 4;
				case ColumnSize.GUID:
					if (!this.bigGuid)
					{
						return 2;
					}
					return 4;
				case ColumnSize.Blob:
					if (!this.bigBlob)
					{
						return 2;
					}
					return 4;
				default:
					throw new InvalidOperationException(string.Format("Invalid ColumnSize: {0}", columnSize));
				}
			}
		}

		// Token: 0x06005E12 RID: 24082 RVA: 0x001C3514 File Offset: 0x001C3514
		public TableInfo[] CreateTables(byte majorVersion, byte minorVersion)
		{
			int num;
			return this.CreateTables(majorVersion, minorVersion, out num);
		}

		// Token: 0x06005E13 RID: 24083 RVA: 0x001C3530 File Offset: 0x001C3530
		public TableInfo[] CreateTables(byte majorVersion, byte minorVersion, out int maxPresentTables)
		{
			maxPresentTables = ((majorVersion == 1 && minorVersion == 0) ? 42 : 56);
			TableInfo[] array = new TableInfo[56];
			array[0] = new TableInfo(Table.Module, "Module", new ColumnInfo[]
			{
				new ColumnInfo(0, "Generation", ColumnSize.UInt16),
				new ColumnInfo(1, "Name", ColumnSize.Strings),
				new ColumnInfo(2, "Mvid", ColumnSize.GUID),
				new ColumnInfo(3, "EncId", ColumnSize.GUID),
				new ColumnInfo(4, "EncBaseId", ColumnSize.GUID)
			});
			array[1] = new TableInfo(Table.TypeRef, "TypeRef", new ColumnInfo[]
			{
				new ColumnInfo(0, "ResolutionScope", ColumnSize.ResolutionScope),
				new ColumnInfo(1, "Name", ColumnSize.Strings),
				new ColumnInfo(2, "Namespace", ColumnSize.Strings)
			});
			array[2] = new TableInfo(Table.TypeDef, "TypeDef", new ColumnInfo[]
			{
				new ColumnInfo(0, "Flags", ColumnSize.UInt32),
				new ColumnInfo(1, "Name", ColumnSize.Strings),
				new ColumnInfo(2, "Namespace", ColumnSize.Strings),
				new ColumnInfo(3, "Extends", ColumnSize.TypeDefOrRef),
				new ColumnInfo(4, "FieldList", ColumnSize.Field),
				new ColumnInfo(5, "MethodList", ColumnSize.Method)
			});
			array[3] = new TableInfo(Table.FieldPtr, "FieldPtr", new ColumnInfo[]
			{
				new ColumnInfo(0, "Field", ColumnSize.Field)
			});
			array[4] = new TableInfo(Table.Field, "Field", new ColumnInfo[]
			{
				new ColumnInfo(0, "Flags", ColumnSize.UInt16),
				new ColumnInfo(1, "Name", ColumnSize.Strings),
				new ColumnInfo(2, "Signature", ColumnSize.Blob)
			});
			array[5] = new TableInfo(Table.MethodPtr, "MethodPtr", new ColumnInfo[]
			{
				new ColumnInfo(0, "Method", ColumnSize.Method)
			});
			array[6] = new TableInfo(Table.Method, "Method", new ColumnInfo[]
			{
				new ColumnInfo(0, "RVA", ColumnSize.UInt32),
				new ColumnInfo(1, "ImplFlags", ColumnSize.UInt16),
				new ColumnInfo(2, "Flags", ColumnSize.UInt16),
				new ColumnInfo(3, "Name", ColumnSize.Strings),
				new ColumnInfo(4, "Signature", ColumnSize.Blob),
				new ColumnInfo(5, "ParamList", ColumnSize.Param)
			});
			array[7] = new TableInfo(Table.ParamPtr, "ParamPtr", new ColumnInfo[]
			{
				new ColumnInfo(0, "Param", ColumnSize.Param)
			});
			array[8] = new TableInfo(Table.Param, "Param", new ColumnInfo[]
			{
				new ColumnInfo(0, "Flags", ColumnSize.UInt16),
				new ColumnInfo(1, "Sequence", ColumnSize.UInt16),
				new ColumnInfo(2, "Name", ColumnSize.Strings)
			});
			array[9] = new TableInfo(Table.InterfaceImpl, "InterfaceImpl", new ColumnInfo[]
			{
				new ColumnInfo(0, "Class", ColumnSize.TypeDef),
				new ColumnInfo(1, "Interface", ColumnSize.TypeDefOrRef)
			});
			array[10] = new TableInfo(Table.MemberRef, "MemberRef", new ColumnInfo[]
			{
				new ColumnInfo(0, "Class", ColumnSize.MemberRefParent),
				new ColumnInfo(1, "Name", ColumnSize.Strings),
				new ColumnInfo(2, "Signature", ColumnSize.Blob)
			});
			array[11] = new TableInfo(Table.Constant, "Constant", new ColumnInfo[]
			{
				new ColumnInfo(0, "Type", ColumnSize.Byte),
				new ColumnInfo(1, "Padding", ColumnSize.Byte),
				new ColumnInfo(2, "Parent", ColumnSize.HasConstant),
				new ColumnInfo(3, "Value", ColumnSize.Blob)
			});
			array[12] = new TableInfo(Table.CustomAttribute, "CustomAttribute", new ColumnInfo[]
			{
				new ColumnInfo(0, "Parent", ColumnSize.HasCustomAttribute),
				new ColumnInfo(1, "Type", ColumnSize.CustomAttributeType),
				new ColumnInfo(2, "Value", ColumnSize.Blob)
			});
			array[13] = new TableInfo(Table.FieldMarshal, "FieldMarshal", new ColumnInfo[]
			{
				new ColumnInfo(0, "Parent", ColumnSize.HasFieldMarshal),
				new ColumnInfo(1, "NativeType", ColumnSize.Blob)
			});
			array[14] = new TableInfo(Table.DeclSecurity, "DeclSecurity", new ColumnInfo[]
			{
				new ColumnInfo(0, "Action", ColumnSize.Int16),
				new ColumnInfo(1, "Parent", ColumnSize.HasDeclSecurity),
				new ColumnInfo(2, "PermissionSet", ColumnSize.Blob)
			});
			array[15] = new TableInfo(Table.ClassLayout, "ClassLayout", new ColumnInfo[]
			{
				new ColumnInfo(0, "PackingSize", ColumnSize.UInt16),
				new ColumnInfo(1, "ClassSize", ColumnSize.UInt32),
				new ColumnInfo(2, "Parent", ColumnSize.TypeDef)
			});
			array[16] = new TableInfo(Table.FieldLayout, "FieldLayout", new ColumnInfo[]
			{
				new ColumnInfo(0, "OffSet", ColumnSize.UInt32),
				new ColumnInfo(1, "Field", ColumnSize.Field)
			});
			array[17] = new TableInfo(Table.StandAloneSig, "StandAloneSig", new ColumnInfo[]
			{
				new ColumnInfo(0, "Signature", ColumnSize.Blob)
			});
			array[18] = new TableInfo(Table.EventMap, "EventMap", new ColumnInfo[]
			{
				new ColumnInfo(0, "Parent", ColumnSize.TypeDef),
				new ColumnInfo(1, "EventList", ColumnSize.Event)
			});
			array[19] = new TableInfo(Table.EventPtr, "EventPtr", new ColumnInfo[]
			{
				new ColumnInfo(0, "Event", ColumnSize.Event)
			});
			array[20] = new TableInfo(Table.Event, "Event", new ColumnInfo[]
			{
				new ColumnInfo(0, "EventFlags", ColumnSize.UInt16),
				new ColumnInfo(1, "Name", ColumnSize.Strings),
				new ColumnInfo(2, "EventType", ColumnSize.TypeDefOrRef)
			});
			array[21] = new TableInfo(Table.PropertyMap, "PropertyMap", new ColumnInfo[]
			{
				new ColumnInfo(0, "Parent", ColumnSize.TypeDef),
				new ColumnInfo(1, "PropertyList", ColumnSize.Property)
			});
			array[22] = new TableInfo(Table.PropertyPtr, "PropertyPtr", new ColumnInfo[]
			{
				new ColumnInfo(0, "Property", ColumnSize.Property)
			});
			array[23] = new TableInfo(Table.Property, "Property", new ColumnInfo[]
			{
				new ColumnInfo(0, "PropFlags", ColumnSize.UInt16),
				new ColumnInfo(1, "Name", ColumnSize.Strings),
				new ColumnInfo(2, "Type", ColumnSize.Blob)
			});
			array[24] = new TableInfo(Table.MethodSemantics, "MethodSemantics", new ColumnInfo[]
			{
				new ColumnInfo(0, "Semantic", ColumnSize.UInt16),
				new ColumnInfo(1, "Method", ColumnSize.Method),
				new ColumnInfo(2, "Association", ColumnSize.HasSemantic)
			});
			array[25] = new TableInfo(Table.MethodImpl, "MethodImpl", new ColumnInfo[]
			{
				new ColumnInfo(0, "Class", ColumnSize.TypeDef),
				new ColumnInfo(1, "MethodBody", ColumnSize.MethodDefOrRef),
				new ColumnInfo(2, "MethodDeclaration", ColumnSize.MethodDefOrRef)
			});
			array[26] = new TableInfo(Table.ModuleRef, "ModuleRef", new ColumnInfo[]
			{
				new ColumnInfo(0, "Name", ColumnSize.Strings)
			});
			array[27] = new TableInfo(Table.TypeSpec, "TypeSpec", new ColumnInfo[]
			{
				new ColumnInfo(0, "Signature", ColumnSize.Blob)
			});
			array[28] = new TableInfo(Table.ImplMap, "ImplMap", new ColumnInfo[]
			{
				new ColumnInfo(0, "MappingFlags", ColumnSize.UInt16),
				new ColumnInfo(1, "MemberForwarded", ColumnSize.MemberForwarded),
				new ColumnInfo(2, "ImportName", ColumnSize.Strings),
				new ColumnInfo(3, "ImportScope", ColumnSize.ModuleRef)
			});
			array[29] = new TableInfo(Table.FieldRVA, "FieldRVA", new ColumnInfo[]
			{
				new ColumnInfo(0, "RVA", ColumnSize.UInt32),
				new ColumnInfo(1, "Field", ColumnSize.Field)
			});
			array[30] = new TableInfo(Table.ENCLog, "ENCLog", new ColumnInfo[]
			{
				new ColumnInfo(0, "Token", ColumnSize.UInt32),
				new ColumnInfo(1, "FuncCode", ColumnSize.UInt32)
			});
			array[31] = new TableInfo(Table.ENCMap, "ENCMap", new ColumnInfo[]
			{
				new ColumnInfo(0, "Token", ColumnSize.UInt32)
			});
			array[32] = new TableInfo(Table.Assembly, "Assembly", new ColumnInfo[]
			{
				new ColumnInfo(0, "HashAlgId", ColumnSize.UInt32),
				new ColumnInfo(1, "MajorVersion", ColumnSize.UInt16),
				new ColumnInfo(2, "MinorVersion", ColumnSize.UInt16),
				new ColumnInfo(3, "BuildNumber", ColumnSize.UInt16),
				new ColumnInfo(4, "RevisionNumber", ColumnSize.UInt16),
				new ColumnInfo(5, "Flags", ColumnSize.UInt32),
				new ColumnInfo(6, "PublicKey", ColumnSize.Blob),
				new ColumnInfo(7, "Name", ColumnSize.Strings),
				new ColumnInfo(8, "Locale", ColumnSize.Strings)
			});
			array[33] = new TableInfo(Table.AssemblyProcessor, "AssemblyProcessor", new ColumnInfo[]
			{
				new ColumnInfo(0, "Processor", ColumnSize.UInt32)
			});
			array[34] = new TableInfo(Table.AssemblyOS, "AssemblyOS", new ColumnInfo[]
			{
				new ColumnInfo(0, "OSPlatformId", ColumnSize.UInt32),
				new ColumnInfo(1, "OSMajorVersion", ColumnSize.UInt32),
				new ColumnInfo(2, "OSMinorVersion", ColumnSize.UInt32)
			});
			array[35] = new TableInfo(Table.AssemblyRef, "AssemblyRef", new ColumnInfo[]
			{
				new ColumnInfo(0, "MajorVersion", ColumnSize.UInt16),
				new ColumnInfo(1, "MinorVersion", ColumnSize.UInt16),
				new ColumnInfo(2, "BuildNumber", ColumnSize.UInt16),
				new ColumnInfo(3, "RevisionNumber", ColumnSize.UInt16),
				new ColumnInfo(4, "Flags", ColumnSize.UInt32),
				new ColumnInfo(5, "PublicKeyOrToken", ColumnSize.Blob),
				new ColumnInfo(6, "Name", ColumnSize.Strings),
				new ColumnInfo(7, "Locale", ColumnSize.Strings),
				new ColumnInfo(8, "HashValue", ColumnSize.Blob)
			});
			array[36] = new TableInfo(Table.AssemblyRefProcessor, "AssemblyRefProcessor", new ColumnInfo[]
			{
				new ColumnInfo(0, "Processor", ColumnSize.UInt32),
				new ColumnInfo(1, "AssemblyRef", ColumnSize.AssemblyRef)
			});
			array[37] = new TableInfo(Table.AssemblyRefOS, "AssemblyRefOS", new ColumnInfo[]
			{
				new ColumnInfo(0, "OSPlatformId", ColumnSize.UInt32),
				new ColumnInfo(1, "OSMajorVersion", ColumnSize.UInt32),
				new ColumnInfo(2, "OSMinorVersion", ColumnSize.UInt32),
				new ColumnInfo(3, "AssemblyRef", ColumnSize.AssemblyRef)
			});
			array[38] = new TableInfo(Table.File, "File", new ColumnInfo[]
			{
				new ColumnInfo(0, "Flags", ColumnSize.UInt32),
				new ColumnInfo(1, "Name", ColumnSize.Strings),
				new ColumnInfo(2, "HashValue", ColumnSize.Blob)
			});
			array[39] = new TableInfo(Table.ExportedType, "ExportedType", new ColumnInfo[]
			{
				new ColumnInfo(0, "Flags", ColumnSize.UInt32),
				new ColumnInfo(1, "TypeDefId", ColumnSize.UInt32),
				new ColumnInfo(2, "TypeName", ColumnSize.Strings),
				new ColumnInfo(3, "TypeNamespace", ColumnSize.Strings),
				new ColumnInfo(4, "Implementation", ColumnSize.Implementation)
			});
			array[40] = new TableInfo(Table.ManifestResource, "ManifestResource", new ColumnInfo[]
			{
				new ColumnInfo(0, "Offset", ColumnSize.UInt32),
				new ColumnInfo(1, "Flags", ColumnSize.UInt32),
				new ColumnInfo(2, "Name", ColumnSize.Strings),
				new ColumnInfo(3, "Implementation", ColumnSize.Implementation)
			});
			array[41] = new TableInfo(Table.NestedClass, "NestedClass", new ColumnInfo[]
			{
				new ColumnInfo(0, "NestedClass", ColumnSize.TypeDef),
				new ColumnInfo(1, "EnclosingClass", ColumnSize.TypeDef)
			});
			if (majorVersion == 1 && minorVersion == 1)
			{
				array[42] = new TableInfo(Table.GenericParam, "GenericParam", new ColumnInfo[]
				{
					new ColumnInfo(0, "Number", ColumnSize.UInt16),
					new ColumnInfo(1, "Flags", ColumnSize.UInt16),
					new ColumnInfo(2, "Owner", ColumnSize.TypeOrMethodDef),
					new ColumnInfo(3, "Name", ColumnSize.Strings),
					new ColumnInfo(4, "Kind", ColumnSize.TypeDefOrRef)
				});
			}
			else
			{
				array[42] = new TableInfo(Table.GenericParam, "GenericParam", new ColumnInfo[]
				{
					new ColumnInfo(0, "Number", ColumnSize.UInt16),
					new ColumnInfo(1, "Flags", ColumnSize.UInt16),
					new ColumnInfo(2, "Owner", ColumnSize.TypeOrMethodDef),
					new ColumnInfo(3, "Name", ColumnSize.Strings)
				});
			}
			array[43] = new TableInfo(Table.MethodSpec, "MethodSpec", new ColumnInfo[]
			{
				new ColumnInfo(0, "Method", ColumnSize.MethodDefOrRef),
				new ColumnInfo(1, "Instantiation", ColumnSize.Blob)
			});
			array[44] = new TableInfo(Table.GenericParamConstraint, "GenericParamConstraint", new ColumnInfo[]
			{
				new ColumnInfo(0, "Owner", ColumnSize.GenericParam),
				new ColumnInfo(1, "Constraint", ColumnSize.TypeDefOrRef)
			});
			array[45] = new TableInfo((Table)45, string.Empty, new ColumnInfo[0]);
			array[46] = new TableInfo((Table)46, string.Empty, new ColumnInfo[0]);
			array[47] = new TableInfo((Table)47, string.Empty, new ColumnInfo[0]);
			array[48] = new TableInfo(Table.Document, "Document", new ColumnInfo[]
			{
				new ColumnInfo(0, "Name", ColumnSize.Blob),
				new ColumnInfo(1, "HashAlgorithm", ColumnSize.GUID),
				new ColumnInfo(2, "Hash", ColumnSize.Blob),
				new ColumnInfo(3, "Language", ColumnSize.GUID)
			});
			array[49] = new TableInfo(Table.MethodDebugInformation, "MethodDebugInformation", new ColumnInfo[]
			{
				new ColumnInfo(0, "Document", ColumnSize.Document),
				new ColumnInfo(1, "SequencePoints", ColumnSize.Blob)
			});
			array[50] = new TableInfo(Table.LocalScope, "LocalScope", new ColumnInfo[]
			{
				new ColumnInfo(0, "Method", ColumnSize.Method),
				new ColumnInfo(1, "ImportScope", ColumnSize.ImportScope),
				new ColumnInfo(2, "VariableList", ColumnSize.LocalVariable),
				new ColumnInfo(3, "ConstantList", ColumnSize.LocalConstant),
				new ColumnInfo(4, "StartOffset", ColumnSize.UInt32),
				new ColumnInfo(5, "Length", ColumnSize.UInt32)
			});
			array[51] = new TableInfo(Table.LocalVariable, "LocalVariable", new ColumnInfo[]
			{
				new ColumnInfo(0, "Attributes", ColumnSize.UInt16),
				new ColumnInfo(1, "Index", ColumnSize.UInt16),
				new ColumnInfo(2, "Name", ColumnSize.Strings)
			});
			array[52] = new TableInfo(Table.LocalConstant, "LocalConstant", new ColumnInfo[]
			{
				new ColumnInfo(0, "Name", ColumnSize.Strings),
				new ColumnInfo(1, "Signature", ColumnSize.Blob)
			});
			array[53] = new TableInfo(Table.ImportScope, "ImportScope", new ColumnInfo[]
			{
				new ColumnInfo(0, "Parent", ColumnSize.ImportScope),
				new ColumnInfo(1, "Imports", ColumnSize.Blob)
			});
			array[54] = new TableInfo(Table.StateMachineMethod, "StateMachineMethod", new ColumnInfo[]
			{
				new ColumnInfo(0, "MoveNextMethod", ColumnSize.Method),
				new ColumnInfo(1, "KickoffMethod", ColumnSize.Method)
			});
			array[55] = new TableInfo(Table.CustomDebugInformation, "CustomDebugInformation", new ColumnInfo[]
			{
				new ColumnInfo(0, "Parent", ColumnSize.HasCustomDebugInformation),
				new ColumnInfo(1, "Kind", ColumnSize.GUID),
				new ColumnInfo(2, "Value", ColumnSize.Blob)
			});
			return this.tableInfos = array;
		}

		// Token: 0x04002DEC RID: 11756
		private bool bigStrings;

		// Token: 0x04002DED RID: 11757
		private bool bigGuid;

		// Token: 0x04002DEE RID: 11758
		private bool bigBlob;

		// Token: 0x04002DEF RID: 11759
		private TableInfo[] tableInfos;

		// Token: 0x04002DF0 RID: 11760
		internal const int normalMaxTables = 56;
	}
}
