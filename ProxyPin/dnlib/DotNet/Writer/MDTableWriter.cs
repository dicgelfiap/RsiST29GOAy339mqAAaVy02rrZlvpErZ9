using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008AD RID: 2221
	[ComVisible(true)]
	public static class MDTableWriter
	{
		// Token: 0x060054F3 RID: 21747 RVA: 0x0019DBA8 File Offset: 0x0019DBA8
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawModuleRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[1];
			ColumnInfo columnInfo2 = columns[2];
			ColumnInfo columnInfo3 = columns[3];
			ColumnInfo columnInfo4 = columns[4];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawModuleRow rawModuleRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawModuleRow.Generation);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawModuleRow.Name));
				columnInfo2.Write24(writer, rawModuleRow.Mvid);
				columnInfo3.Write24(writer, rawModuleRow.EncId);
				columnInfo4.Write24(writer, rawModuleRow.EncBaseId);
			}
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0019DC4C File Offset: 0x0019DC4C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawTypeRefRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			ColumnInfo columnInfo3 = columns[2];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawTypeRefRow rawTypeRefRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawTypeRefRow.ResolutionScope);
				columnInfo2.Write24(writer, stringsHeap.GetOffset(rawTypeRefRow.Name));
				columnInfo3.Write24(writer, stringsHeap.GetOffset(rawTypeRefRow.Namespace));
			}
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x0019DCD4 File Offset: 0x0019DCD4
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawTypeDefRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[1];
			ColumnInfo columnInfo2 = columns[2];
			ColumnInfo columnInfo3 = columns[3];
			ColumnInfo columnInfo4 = columns[4];
			ColumnInfo columnInfo5 = columns[5];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawTypeDefRow rawTypeDefRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawTypeDefRow.Flags);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawTypeDefRow.Name));
				columnInfo2.Write24(writer, stringsHeap.GetOffset(rawTypeDefRow.Namespace));
				columnInfo3.Write24(writer, rawTypeDefRow.Extends);
				columnInfo4.Write24(writer, rawTypeDefRow.FieldList);
				columnInfo5.Write24(writer, rawTypeDefRow.MethodList);
			}
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0019DD94 File Offset: 0x0019DD94
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawFieldPtrRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[0];
			for (int i = 0; i < table.Rows; i++)
			{
				RawFieldPtrRow rawFieldPtrRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawFieldPtrRow.Field);
			}
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0019DDE4 File Offset: 0x0019DDE4
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawFieldRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[1];
			ColumnInfo columnInfo2 = columns[2];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawFieldRow rawFieldRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawFieldRow.Flags);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawFieldRow.Name));
				columnInfo2.Write24(writer, rawFieldRow.Signature);
			}
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x0019DE5C File Offset: 0x0019DE5C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawMethodPtrRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[0];
			for (int i = 0; i < table.Rows; i++)
			{
				RawMethodPtrRow rawMethodPtrRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawMethodPtrRow.Method);
			}
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x0019DEAC File Offset: 0x0019DEAC
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawMethodRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[3];
			ColumnInfo columnInfo2 = columns[4];
			ColumnInfo columnInfo3 = columns[5];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawMethodRow rawMethodRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawMethodRow.RVA);
				writer.WriteUInt16(rawMethodRow.ImplFlags);
				writer.WriteUInt16(rawMethodRow.Flags);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawMethodRow.Name));
				columnInfo2.Write24(writer, rawMethodRow.Signature);
				columnInfo3.Write24(writer, rawMethodRow.ParamList);
			}
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x0019DF58 File Offset: 0x0019DF58
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawParamPtrRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[0];
			for (int i = 0; i < table.Rows; i++)
			{
				RawParamPtrRow rawParamPtrRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawParamPtrRow.Param);
			}
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x0019DFA8 File Offset: 0x0019DFA8
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawParamRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[2];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawParamRow rawParamRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawParamRow.Flags);
				writer.WriteUInt16(rawParamRow.Sequence);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawParamRow.Name));
			}
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x0019E01C File Offset: 0x0019E01C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawInterfaceImplRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawInterfaceImplRow rawInterfaceImplRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawInterfaceImplRow.Class);
				columnInfo2.Write24(writer, rawInterfaceImplRow.Interface);
			}
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x0019E078 File Offset: 0x0019E078
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawMemberRefRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			ColumnInfo columnInfo3 = columns[2];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawMemberRefRow rawMemberRefRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawMemberRefRow.Class);
				columnInfo2.Write24(writer, stringsHeap.GetOffset(rawMemberRefRow.Name));
				columnInfo3.Write24(writer, rawMemberRefRow.Signature);
			}
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0019E0FC File Offset: 0x0019E0FC
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawConstantRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[2];
			ColumnInfo columnInfo2 = columns[3];
			for (int i = 0; i < table.Rows; i++)
			{
				RawConstantRow rawConstantRow = table[(uint)(i + 1)];
				writer.WriteByte(rawConstantRow.Type);
				writer.WriteByte(rawConstantRow.Padding);
				columnInfo.Write24(writer, rawConstantRow.Parent);
				columnInfo2.Write24(writer, rawConstantRow.Value);
			}
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x0019E170 File Offset: 0x0019E170
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawCustomAttributeRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			ColumnInfo columnInfo3 = columns[2];
			for (int i = 0; i < table.Rows; i++)
			{
				RawCustomAttributeRow rawCustomAttributeRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawCustomAttributeRow.Parent);
				columnInfo2.Write24(writer, rawCustomAttributeRow.Type);
				columnInfo3.Write24(writer, rawCustomAttributeRow.Value);
			}
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x0019E1E0 File Offset: 0x0019E1E0
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawFieldMarshalRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawFieldMarshalRow rawFieldMarshalRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawFieldMarshalRow.Parent);
				columnInfo2.Write24(writer, rawFieldMarshalRow.NativeType);
			}
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x0019E23C File Offset: 0x0019E23C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawDeclSecurityRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[1];
			ColumnInfo columnInfo2 = columns[2];
			for (int i = 0; i < table.Rows; i++)
			{
				RawDeclSecurityRow rawDeclSecurityRow = table[(uint)(i + 1)];
				writer.WriteInt16(rawDeclSecurityRow.Action);
				columnInfo.Write24(writer, rawDeclSecurityRow.Parent);
				columnInfo2.Write24(writer, rawDeclSecurityRow.PermissionSet);
			}
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x0019E2A4 File Offset: 0x0019E2A4
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawClassLayoutRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[2];
			for (int i = 0; i < table.Rows; i++)
			{
				RawClassLayoutRow rawClassLayoutRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawClassLayoutRow.PackingSize);
				writer.WriteUInt32(rawClassLayoutRow.ClassSize);
				columnInfo.Write24(writer, rawClassLayoutRow.Parent);
			}
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0019E30C File Offset: 0x0019E30C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawFieldLayoutRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawFieldLayoutRow rawFieldLayoutRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawFieldLayoutRow.OffSet);
				columnInfo.Write24(writer, rawFieldLayoutRow.Field);
			}
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x0019E368 File Offset: 0x0019E368
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawStandAloneSigRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[0];
			for (int i = 0; i < table.Rows; i++)
			{
				RawStandAloneSigRow rawStandAloneSigRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawStandAloneSigRow.Signature);
			}
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x0019E3B8 File Offset: 0x0019E3B8
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawEventMapRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawEventMapRow rawEventMapRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawEventMapRow.Parent);
				columnInfo2.Write24(writer, rawEventMapRow.EventList);
			}
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x0019E414 File Offset: 0x0019E414
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawEventPtrRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[0];
			for (int i = 0; i < table.Rows; i++)
			{
				RawEventPtrRow rawEventPtrRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawEventPtrRow.Event);
			}
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x0019E464 File Offset: 0x0019E464
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawEventRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[1];
			ColumnInfo columnInfo2 = columns[2];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawEventRow rawEventRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawEventRow.EventFlags);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawEventRow.Name));
				columnInfo2.Write24(writer, rawEventRow.EventType);
			}
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x0019E4DC File Offset: 0x0019E4DC
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawPropertyMapRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawPropertyMapRow rawPropertyMapRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawPropertyMapRow.Parent);
				columnInfo2.Write24(writer, rawPropertyMapRow.PropertyList);
			}
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x0019E538 File Offset: 0x0019E538
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawPropertyPtrRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[0];
			for (int i = 0; i < table.Rows; i++)
			{
				RawPropertyPtrRow rawPropertyPtrRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawPropertyPtrRow.Property);
			}
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x0019E588 File Offset: 0x0019E588
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawPropertyRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[1];
			ColumnInfo columnInfo2 = columns[2];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawPropertyRow rawPropertyRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawPropertyRow.PropFlags);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawPropertyRow.Name));
				columnInfo2.Write24(writer, rawPropertyRow.Type);
			}
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x0019E600 File Offset: 0x0019E600
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawMethodSemanticsRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[1];
			ColumnInfo columnInfo2 = columns[2];
			for (int i = 0; i < table.Rows; i++)
			{
				RawMethodSemanticsRow rawMethodSemanticsRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawMethodSemanticsRow.Semantic);
				columnInfo.Write24(writer, rawMethodSemanticsRow.Method);
				columnInfo2.Write24(writer, rawMethodSemanticsRow.Association);
			}
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x0019E668 File Offset: 0x0019E668
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawMethodImplRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			ColumnInfo columnInfo3 = columns[2];
			for (int i = 0; i < table.Rows; i++)
			{
				RawMethodImplRow rawMethodImplRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawMethodImplRow.Class);
				columnInfo2.Write24(writer, rawMethodImplRow.MethodBody);
				columnInfo3.Write24(writer, rawMethodImplRow.MethodDeclaration);
			}
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x0019E6D8 File Offset: 0x0019E6D8
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawModuleRefRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[0];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawModuleRefRow rawModuleRefRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawModuleRefRow.Name));
			}
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x0019E734 File Offset: 0x0019E734
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawTypeSpecRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[0];
			for (int i = 0; i < table.Rows; i++)
			{
				RawTypeSpecRow rawTypeSpecRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawTypeSpecRow.Signature);
			}
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x0019E784 File Offset: 0x0019E784
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawImplMapRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[1];
			ColumnInfo columnInfo2 = columns[2];
			ColumnInfo columnInfo3 = columns[3];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawImplMapRow rawImplMapRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawImplMapRow.MappingFlags);
				columnInfo.Write24(writer, rawImplMapRow.MemberForwarded);
				columnInfo2.Write24(writer, stringsHeap.GetOffset(rawImplMapRow.ImportName));
				columnInfo3.Write24(writer, rawImplMapRow.ImportScope);
			}
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x0019E814 File Offset: 0x0019E814
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawFieldRVARow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawFieldRVARow rawFieldRVARow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawFieldRVARow.RVA);
				columnInfo.Write24(writer, rawFieldRVARow.Field);
			}
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x0019E870 File Offset: 0x0019E870
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawENCLogRow> table)
		{
			for (int i = 0; i < table.Rows; i++)
			{
				RawENCLogRow rawENCLogRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawENCLogRow.Token);
				writer.WriteUInt32(rawENCLogRow.FuncCode);
			}
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x0019E8B8 File Offset: 0x0019E8B8
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawENCMapRow> table)
		{
			for (int i = 0; i < table.Rows; i++)
			{
				RawENCMapRow rawENCMapRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawENCMapRow.Token);
			}
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x0019E8F4 File Offset: 0x0019E8F4
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawAssemblyRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[6];
			ColumnInfo columnInfo2 = columns[7];
			ColumnInfo columnInfo3 = columns[8];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawAssemblyRow rawAssemblyRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawAssemblyRow.HashAlgId);
				writer.WriteUInt16(rawAssemblyRow.MajorVersion);
				writer.WriteUInt16(rawAssemblyRow.MinorVersion);
				writer.WriteUInt16(rawAssemblyRow.BuildNumber);
				writer.WriteUInt16(rawAssemblyRow.RevisionNumber);
				writer.WriteUInt32(rawAssemblyRow.Flags);
				columnInfo.Write24(writer, rawAssemblyRow.PublicKey);
				columnInfo2.Write24(writer, stringsHeap.GetOffset(rawAssemblyRow.Name));
				columnInfo3.Write24(writer, stringsHeap.GetOffset(rawAssemblyRow.Locale));
			}
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x0019E9D0 File Offset: 0x0019E9D0
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawAssemblyProcessorRow> table)
		{
			for (int i = 0; i < table.Rows; i++)
			{
				RawAssemblyProcessorRow rawAssemblyProcessorRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawAssemblyProcessorRow.Processor);
			}
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x0019EA0C File Offset: 0x0019EA0C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawAssemblyOSRow> table)
		{
			for (int i = 0; i < table.Rows; i++)
			{
				RawAssemblyOSRow rawAssemblyOSRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawAssemblyOSRow.OSPlatformId);
				writer.WriteUInt32(rawAssemblyOSRow.OSMajorVersion);
				writer.WriteUInt32(rawAssemblyOSRow.OSMinorVersion);
			}
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x0019EA60 File Offset: 0x0019EA60
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawAssemblyRefRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[5];
			ColumnInfo columnInfo2 = columns[6];
			ColumnInfo columnInfo3 = columns[7];
			ColumnInfo columnInfo4 = columns[8];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawAssemblyRefRow rawAssemblyRefRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawAssemblyRefRow.MajorVersion);
				writer.WriteUInt16(rawAssemblyRefRow.MinorVersion);
				writer.WriteUInt16(rawAssemblyRefRow.BuildNumber);
				writer.WriteUInt16(rawAssemblyRefRow.RevisionNumber);
				writer.WriteUInt32(rawAssemblyRefRow.Flags);
				columnInfo.Write24(writer, rawAssemblyRefRow.PublicKeyOrToken);
				columnInfo2.Write24(writer, stringsHeap.GetOffset(rawAssemblyRefRow.Name));
				columnInfo3.Write24(writer, stringsHeap.GetOffset(rawAssemblyRefRow.Locale));
				columnInfo4.Write24(writer, rawAssemblyRefRow.HashValue);
			}
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x0019EB44 File Offset: 0x0019EB44
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawAssemblyRefProcessorRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawAssemblyRefProcessorRow rawAssemblyRefProcessorRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawAssemblyRefProcessorRow.Processor);
				columnInfo.Write24(writer, rawAssemblyRefProcessorRow.AssemblyRef);
			}
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x0019EBA0 File Offset: 0x0019EBA0
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawAssemblyRefOSRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[3];
			for (int i = 0; i < table.Rows; i++)
			{
				RawAssemblyRefOSRow rawAssemblyRefOSRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawAssemblyRefOSRow.OSPlatformId);
				writer.WriteUInt32(rawAssemblyRefOSRow.OSMajorVersion);
				writer.WriteUInt32(rawAssemblyRefOSRow.OSMinorVersion);
				columnInfo.Write24(writer, rawAssemblyRefOSRow.AssemblyRef);
			}
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x0019EC14 File Offset: 0x0019EC14
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawFileRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[1];
			ColumnInfo columnInfo2 = columns[2];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawFileRow rawFileRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawFileRow.Flags);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawFileRow.Name));
				columnInfo2.Write24(writer, rawFileRow.HashValue);
			}
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x0019EC8C File Offset: 0x0019EC8C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawExportedTypeRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[2];
			ColumnInfo columnInfo2 = columns[3];
			ColumnInfo columnInfo3 = columns[4];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawExportedTypeRow rawExportedTypeRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawExportedTypeRow.Flags);
				writer.WriteUInt32(rawExportedTypeRow.TypeDefId);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawExportedTypeRow.TypeName));
				columnInfo2.Write24(writer, stringsHeap.GetOffset(rawExportedTypeRow.TypeNamespace));
				columnInfo3.Write24(writer, rawExportedTypeRow.Implementation);
			}
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0019ED30 File Offset: 0x0019ED30
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawManifestResourceRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[2];
			ColumnInfo columnInfo2 = columns[3];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawManifestResourceRow rawManifestResourceRow = table[(uint)(i + 1)];
				writer.WriteUInt32(rawManifestResourceRow.Offset);
				writer.WriteUInt32(rawManifestResourceRow.Flags);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawManifestResourceRow.Name));
				columnInfo2.Write24(writer, rawManifestResourceRow.Implementation);
			}
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0019EDB8 File Offset: 0x0019EDB8
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawNestedClassRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawNestedClassRow rawNestedClassRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawNestedClassRow.NestedClass);
				columnInfo2.Write24(writer, rawNestedClassRow.EnclosingClass);
			}
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x0019EE14 File Offset: 0x0019EE14
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawGenericParamRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[2];
			ColumnInfo columnInfo2 = columns[3];
			StringsHeap stringsHeap = metadata.StringsHeap;
			if (columns.Length >= 5)
			{
				ColumnInfo columnInfo3 = columns[4];
				for (int i = 0; i < table.Rows; i++)
				{
					RawGenericParamRow rawGenericParamRow = table[(uint)(i + 1)];
					writer.WriteUInt16(rawGenericParamRow.Number);
					writer.WriteUInt16(rawGenericParamRow.Flags);
					columnInfo.Write24(writer, rawGenericParamRow.Owner);
					columnInfo2.Write24(writer, stringsHeap.GetOffset(rawGenericParamRow.Name));
					columnInfo3.Write24(writer, rawGenericParamRow.Kind);
				}
				return;
			}
			for (int j = 0; j < table.Rows; j++)
			{
				RawGenericParamRow rawGenericParamRow2 = table[(uint)(j + 1)];
				writer.WriteUInt16(rawGenericParamRow2.Number);
				writer.WriteUInt16(rawGenericParamRow2.Flags);
				columnInfo.Write24(writer, rawGenericParamRow2.Owner);
				columnInfo2.Write24(writer, stringsHeap.GetOffset(rawGenericParamRow2.Name));
			}
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0019EF2C File Offset: 0x0019EF2C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawMethodSpecRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawMethodSpecRow rawMethodSpecRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawMethodSpecRow.Method);
				columnInfo2.Write24(writer, rawMethodSpecRow.Instantiation);
			}
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x0019EF88 File Offset: 0x0019EF88
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawGenericParamConstraintRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawGenericParamConstraintRow rawGenericParamConstraintRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawGenericParamConstraintRow.Owner);
				columnInfo2.Write24(writer, rawGenericParamConstraintRow.Constraint);
			}
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x0019EFE4 File Offset: 0x0019EFE4
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawDocumentRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			ColumnInfo columnInfo3 = columns[2];
			ColumnInfo columnInfo4 = columns[3];
			for (int i = 0; i < table.Rows; i++)
			{
				RawDocumentRow rawDocumentRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawDocumentRow.Name);
				columnInfo2.Write24(writer, rawDocumentRow.HashAlgorithm);
				columnInfo3.Write24(writer, rawDocumentRow.Hash);
				columnInfo4.Write24(writer, rawDocumentRow.Language);
			}
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0019F06C File Offset: 0x0019F06C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawMethodDebugInformationRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawMethodDebugInformationRow rawMethodDebugInformationRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawMethodDebugInformationRow.Document);
				columnInfo2.Write24(writer, rawMethodDebugInformationRow.SequencePoints);
			}
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0019F0C8 File Offset: 0x0019F0C8
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawLocalScopeRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			ColumnInfo columnInfo3 = columns[2];
			ColumnInfo columnInfo4 = columns[3];
			for (int i = 0; i < table.Rows; i++)
			{
				RawLocalScopeRow rawLocalScopeRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawLocalScopeRow.Method);
				columnInfo2.Write24(writer, rawLocalScopeRow.ImportScope);
				columnInfo3.Write24(writer, rawLocalScopeRow.VariableList);
				columnInfo4.Write24(writer, rawLocalScopeRow.ConstantList);
				writer.WriteUInt32(rawLocalScopeRow.StartOffset);
				writer.WriteUInt32(rawLocalScopeRow.Length);
			}
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x0019F16C File Offset: 0x0019F16C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawLocalVariableRow> table)
		{
			ColumnInfo columnInfo = table.TableInfo.Columns[2];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawLocalVariableRow rawLocalVariableRow = table[(uint)(i + 1)];
				writer.WriteUInt16(rawLocalVariableRow.Attributes);
				writer.WriteUInt16(rawLocalVariableRow.Index);
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawLocalVariableRow.Name));
			}
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x0019F1E0 File Offset: 0x0019F1E0
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawLocalConstantRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			StringsHeap stringsHeap = metadata.StringsHeap;
			for (int i = 0; i < table.Rows; i++)
			{
				RawLocalConstantRow rawLocalConstantRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, stringsHeap.GetOffset(rawLocalConstantRow.Name));
				columnInfo2.Write24(writer, rawLocalConstantRow.Signature);
			}
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x0019F24C File Offset: 0x0019F24C
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawImportScopeRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawImportScopeRow rawImportScopeRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawImportScopeRow.Parent);
				columnInfo2.Write24(writer, rawImportScopeRow.Imports);
			}
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x0019F2A8 File Offset: 0x0019F2A8
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawStateMachineMethodRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			for (int i = 0; i < table.Rows; i++)
			{
				RawStateMachineMethodRow rawStateMachineMethodRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawStateMachineMethodRow.MoveNextMethod);
				columnInfo2.Write24(writer, rawStateMachineMethodRow.KickoffMethod);
			}
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x0019F304 File Offset: 0x0019F304
		public static void Write(this DataWriter writer, Metadata metadata, MDTable<RawCustomDebugInformationRow> table)
		{
			ColumnInfo[] columns = table.TableInfo.Columns;
			ColumnInfo columnInfo = columns[0];
			ColumnInfo columnInfo2 = columns[1];
			ColumnInfo columnInfo3 = columns[2];
			for (int i = 0; i < table.Rows; i++)
			{
				RawCustomDebugInformationRow rawCustomDebugInformationRow = table[(uint)(i + 1)];
				columnInfo.Write24(writer, rawCustomDebugInformationRow.Parent);
				columnInfo2.Write24(writer, rawCustomDebugInformationRow.Kind);
				columnInfo3.Write24(writer, rawCustomDebugInformationRow.Value);
			}
		}
	}
}
