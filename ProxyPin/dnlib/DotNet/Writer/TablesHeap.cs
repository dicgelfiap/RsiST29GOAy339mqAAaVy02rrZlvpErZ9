using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008DE RID: 2270
	[ComVisible(true)]
	public sealed class TablesHeap : IHeap, IChunk
	{
		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06005852 RID: 22610 RVA: 0x001B1B5C File Offset: 0x001B1B5C
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06005853 RID: 22611 RVA: 0x001B1B64 File Offset: 0x001B1B64
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06005854 RID: 22612 RVA: 0x001B1B6C File Offset: 0x001B1B6C
		public string Name
		{
			get
			{
				if (!this.IsENC)
				{
					return "#~";
				}
				return "#-";
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06005855 RID: 22613 RVA: 0x001B1B84 File Offset: 0x001B1B84
		public bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06005856 RID: 22614 RVA: 0x001B1B88 File Offset: 0x001B1B88
		public bool IsENC
		{
			get
			{
				if (this.options.UseENC != null)
				{
					return this.options.UseENC.Value;
				}
				return this.hasDeletedRows || !this.FieldPtrTable.IsEmpty || !this.MethodPtrTable.IsEmpty || !this.ParamPtrTable.IsEmpty || !this.EventPtrTable.IsEmpty || !this.PropertyPtrTable.IsEmpty || (!this.InterfaceImplTable.IsEmpty && !this.InterfaceImplTable.IsSorted) || (!this.ConstantTable.IsEmpty && !this.ConstantTable.IsSorted) || (!this.CustomAttributeTable.IsEmpty && !this.CustomAttributeTable.IsSorted) || (!this.FieldMarshalTable.IsEmpty && !this.FieldMarshalTable.IsSorted) || (!this.DeclSecurityTable.IsEmpty && !this.DeclSecurityTable.IsSorted) || (!this.ClassLayoutTable.IsEmpty && !this.ClassLayoutTable.IsSorted) || (!this.FieldLayoutTable.IsEmpty && !this.FieldLayoutTable.IsSorted) || (!this.EventMapTable.IsEmpty && !this.EventMapTable.IsSorted) || (!this.PropertyMapTable.IsEmpty && !this.PropertyMapTable.IsSorted) || (!this.MethodSemanticsTable.IsEmpty && !this.MethodSemanticsTable.IsSorted) || (!this.MethodImplTable.IsEmpty && !this.MethodImplTable.IsSorted) || (!this.ImplMapTable.IsEmpty && !this.ImplMapTable.IsSorted) || (!this.FieldRVATable.IsEmpty && !this.FieldRVATable.IsSorted) || (!this.NestedClassTable.IsEmpty && !this.NestedClassTable.IsSorted) || (!this.GenericParamTable.IsEmpty && !this.GenericParamTable.IsSorted) || (!this.GenericParamConstraintTable.IsEmpty && !this.GenericParamConstraintTable.IsSorted);
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06005857 RID: 22615 RVA: 0x001B1E1C File Offset: 0x001B1E1C
		// (set) Token: 0x06005858 RID: 22616 RVA: 0x001B1E24 File Offset: 0x001B1E24
		public bool HasDeletedRows
		{
			get
			{
				return this.hasDeletedRows;
			}
			set
			{
				this.hasDeletedRows = value;
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06005859 RID: 22617 RVA: 0x001B1E30 File Offset: 0x001B1E30
		// (set) Token: 0x0600585A RID: 22618 RVA: 0x001B1E38 File Offset: 0x001B1E38
		public bool BigStrings
		{
			get
			{
				return this.bigStrings;
			}
			set
			{
				this.bigStrings = value;
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x0600585B RID: 22619 RVA: 0x001B1E44 File Offset: 0x001B1E44
		// (set) Token: 0x0600585C RID: 22620 RVA: 0x001B1E4C File Offset: 0x001B1E4C
		public bool BigGuid
		{
			get
			{
				return this.bigGuid;
			}
			set
			{
				this.bigGuid = value;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x0600585D RID: 22621 RVA: 0x001B1E58 File Offset: 0x001B1E58
		// (set) Token: 0x0600585E RID: 22622 RVA: 0x001B1E60 File Offset: 0x001B1E60
		public bool BigBlob
		{
			get
			{
				return this.bigBlob;
			}
			set
			{
				this.bigBlob = value;
			}
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x001B1E6C File Offset: 0x001B1E6C
		public TablesHeap(Metadata metadata, TablesHeapOptions options)
		{
			this.metadata = metadata;
			this.options = (options ?? new TablesHeapOptions());
			this.hasDeletedRows = this.options.HasDeletedRows.GetValueOrDefault();
			this.Tables = new IMDTable[]
			{
				this.ModuleTable,
				this.TypeRefTable,
				this.TypeDefTable,
				this.FieldPtrTable,
				this.FieldTable,
				this.MethodPtrTable,
				this.MethodTable,
				this.ParamPtrTable,
				this.ParamTable,
				this.InterfaceImplTable,
				this.MemberRefTable,
				this.ConstantTable,
				this.CustomAttributeTable,
				this.FieldMarshalTable,
				this.DeclSecurityTable,
				this.ClassLayoutTable,
				this.FieldLayoutTable,
				this.StandAloneSigTable,
				this.EventMapTable,
				this.EventPtrTable,
				this.EventTable,
				this.PropertyMapTable,
				this.PropertyPtrTable,
				this.PropertyTable,
				this.MethodSemanticsTable,
				this.MethodImplTable,
				this.ModuleRefTable,
				this.TypeSpecTable,
				this.ImplMapTable,
				this.FieldRVATable,
				this.ENCLogTable,
				this.ENCMapTable,
				this.AssemblyTable,
				this.AssemblyProcessorTable,
				this.AssemblyOSTable,
				this.AssemblyRefTable,
				this.AssemblyRefProcessorTable,
				this.AssemblyRefOSTable,
				this.FileTable,
				this.ExportedTypeTable,
				this.ManifestResourceTable,
				this.NestedClassTable,
				this.GenericParamTable,
				this.MethodSpecTable,
				this.GenericParamConstraintTable,
				new MDTable<TablesHeap.RawDummyRow>((Table)45, TablesHeap.RawDummyRow.Comparer),
				new MDTable<TablesHeap.RawDummyRow>((Table)46, TablesHeap.RawDummyRow.Comparer),
				new MDTable<TablesHeap.RawDummyRow>((Table)47, TablesHeap.RawDummyRow.Comparer),
				this.DocumentTable,
				this.MethodDebugInformationTable,
				this.LocalScopeTable,
				this.LocalVariableTable,
				this.LocalConstantTable,
				this.ImportScopeTable,
				this.StateMachineMethodTable,
				this.CustomDebugInformationTable
			};
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x001B24AC File Offset: 0x001B24AC
		public void SetReadOnly()
		{
			IMDTable[] tables = this.Tables;
			for (int i = 0; i < tables.Length; i++)
			{
				tables[i].SetReadOnly();
			}
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x001B24E4 File Offset: 0x001B24E4
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x001B24F4 File Offset: 0x001B24F4
		public uint GetFileLength()
		{
			if (this.length == 0U)
			{
				this.CalculateLength();
			}
			return Utils.AlignUp(this.length, 4U);
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x001B2514 File Offset: 0x001B2514
		public uint GetVirtualSize()
		{
			return this.GetFileLength();
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x001B251C File Offset: 0x001B251C
		public void CalculateLength()
		{
			if (this.length != 0U)
			{
				return;
			}
			this.SetReadOnly();
			this.majorVersion = (this.options.MajorVersion ?? 2);
			this.minorVersion = this.options.MinorVersion.GetValueOrDefault();
			if (((int)this.majorVersion << 8 | (int)this.minorVersion) <= 256 && (!this.GenericParamTable.IsEmpty || !this.MethodSpecTable.IsEmpty || !this.GenericParamConstraintTable.IsEmpty))
			{
				throw new ModuleWriterException("Tables heap version <= v1.0 but generic tables are not empty");
			}
			DotNetTableSizes dotNetTableSizes = new DotNetTableSizes();
			TableInfo[] array = dotNetTableSizes.CreateTables(this.majorVersion, this.minorVersion);
			uint[] rowCounts = this.GetRowCounts();
			dotNetTableSizes.InitializeSizes(this.bigStrings, this.bigGuid, this.bigBlob, this.systemTables ?? rowCounts, rowCounts);
			for (int i = 0; i < this.Tables.Length; i++)
			{
				this.Tables[i].TableInfo = array[i];
			}
			this.length = 24U;
			foreach (IMDTable imdtable in this.Tables)
			{
				if (!imdtable.IsEmpty)
				{
					this.length += (uint)(4 + imdtable.TableInfo.RowSize * imdtable.Rows);
				}
			}
			if (this.options.ExtraData != null)
			{
				this.length += 4U;
			}
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x001B26CC File Offset: 0x001B26CC
		private uint[] GetRowCounts()
		{
			uint[] array = new uint[this.Tables.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (uint)this.Tables[i].Rows;
			}
			return array;
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x001B2714 File Offset: 0x001B2714
		internal void GetSystemTableRows(out ulong mask, uint[] tables)
		{
			if (tables.Length != 64)
			{
				throw new InvalidOperationException();
			}
			ulong validMask = this.GetValidMask();
			ulong num = 1UL;
			mask = 0UL;
			int i = 0;
			while (i < 64)
			{
				if (DotNetTableSizes.IsSystemTable((Table)i))
				{
					if ((validMask & num) != 0UL)
					{
						tables[i] = (uint)this.Tables[i].Rows;
						mask |= num;
					}
					else
					{
						tables[i] = 0U;
					}
				}
				else
				{
					tables[i] = 0U;
				}
				i++;
				num <<= 1;
			}
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x001B2798 File Offset: 0x001B2798
		internal void SetSystemTableRows(uint[] systemTables)
		{
			this.systemTables = (uint[])systemTables.Clone();
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x001B27AC File Offset: 0x001B27AC
		public void WriteTo(DataWriter writer)
		{
			writer.WriteUInt32(this.options.Reserved1.GetValueOrDefault());
			writer.WriteByte(this.majorVersion);
			writer.WriteByte(this.minorVersion);
			writer.WriteByte((byte)this.GetMDStreamFlags());
			writer.WriteByte(this.GetLog2Rid());
			writer.WriteUInt64(this.GetValidMask());
			writer.WriteUInt64(this.GetSortedMask());
			foreach (IMDTable imdtable in this.Tables)
			{
				if (!imdtable.IsEmpty)
				{
					writer.WriteInt32(imdtable.Rows);
				}
			}
			if (this.options.ExtraData != null)
			{
				writer.WriteUInt32(this.options.ExtraData.Value);
			}
			writer.Write(this.metadata, this.ModuleTable);
			writer.Write(this.metadata, this.TypeRefTable);
			writer.Write(this.metadata, this.TypeDefTable);
			writer.Write(this.metadata, this.FieldPtrTable);
			writer.Write(this.metadata, this.FieldTable);
			writer.Write(this.metadata, this.MethodPtrTable);
			writer.Write(this.metadata, this.MethodTable);
			writer.Write(this.metadata, this.ParamPtrTable);
			writer.Write(this.metadata, this.ParamTable);
			writer.Write(this.metadata, this.InterfaceImplTable);
			writer.Write(this.metadata, this.MemberRefTable);
			writer.Write(this.metadata, this.ConstantTable);
			writer.Write(this.metadata, this.CustomAttributeTable);
			writer.Write(this.metadata, this.FieldMarshalTable);
			writer.Write(this.metadata, this.DeclSecurityTable);
			writer.Write(this.metadata, this.ClassLayoutTable);
			writer.Write(this.metadata, this.FieldLayoutTable);
			writer.Write(this.metadata, this.StandAloneSigTable);
			writer.Write(this.metadata, this.EventMapTable);
			writer.Write(this.metadata, this.EventPtrTable);
			writer.Write(this.metadata, this.EventTable);
			writer.Write(this.metadata, this.PropertyMapTable);
			writer.Write(this.metadata, this.PropertyPtrTable);
			writer.Write(this.metadata, this.PropertyTable);
			writer.Write(this.metadata, this.MethodSemanticsTable);
			writer.Write(this.metadata, this.MethodImplTable);
			writer.Write(this.metadata, this.ModuleRefTable);
			writer.Write(this.metadata, this.TypeSpecTable);
			writer.Write(this.metadata, this.ImplMapTable);
			writer.Write(this.metadata, this.FieldRVATable);
			writer.Write(this.metadata, this.ENCLogTable);
			writer.Write(this.metadata, this.ENCMapTable);
			writer.Write(this.metadata, this.AssemblyTable);
			writer.Write(this.metadata, this.AssemblyProcessorTable);
			writer.Write(this.metadata, this.AssemblyOSTable);
			writer.Write(this.metadata, this.AssemblyRefTable);
			writer.Write(this.metadata, this.AssemblyRefProcessorTable);
			writer.Write(this.metadata, this.AssemblyRefOSTable);
			writer.Write(this.metadata, this.FileTable);
			writer.Write(this.metadata, this.ExportedTypeTable);
			writer.Write(this.metadata, this.ManifestResourceTable);
			writer.Write(this.metadata, this.NestedClassTable);
			writer.Write(this.metadata, this.GenericParamTable);
			writer.Write(this.metadata, this.MethodSpecTable);
			writer.Write(this.metadata, this.GenericParamConstraintTable);
			writer.Write(this.metadata, this.DocumentTable);
			writer.Write(this.metadata, this.MethodDebugInformationTable);
			writer.Write(this.metadata, this.LocalScopeTable);
			writer.Write(this.metadata, this.LocalVariableTable);
			writer.Write(this.metadata, this.LocalConstantTable);
			writer.Write(this.metadata, this.ImportScopeTable);
			writer.Write(this.metadata, this.StateMachineMethodTable);
			writer.Write(this.metadata, this.CustomDebugInformationTable);
			writer.WriteZeroes((int)(Utils.AlignUp(this.length, 4U) - this.length));
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x001B2C50 File Offset: 0x001B2C50
		private MDStreamFlags GetMDStreamFlags()
		{
			MDStreamFlags mdstreamFlags = (MDStreamFlags)0;
			if (this.bigStrings)
			{
				mdstreamFlags |= MDStreamFlags.BigStrings;
			}
			if (this.bigGuid)
			{
				mdstreamFlags |= MDStreamFlags.BigGUID;
			}
			if (this.bigBlob)
			{
				mdstreamFlags |= MDStreamFlags.BigBlob;
			}
			if (this.options.ExtraData != null)
			{
				mdstreamFlags |= MDStreamFlags.ExtraData;
			}
			if (this.hasDeletedRows)
			{
				mdstreamFlags |= MDStreamFlags.HasDelete;
			}
			return mdstreamFlags;
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x001B2CC0 File Offset: 0x001B2CC0
		private byte GetLog2Rid()
		{
			return 1;
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x001B2CC4 File Offset: 0x001B2CC4
		private ulong GetValidMask()
		{
			ulong num = 0UL;
			foreach (IMDTable imdtable in this.Tables)
			{
				if (!imdtable.IsEmpty)
				{
					num |= 1UL << (int)imdtable.Table;
				}
			}
			return num;
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x001B2D14 File Offset: 0x001B2D14
		private ulong GetSortedMask()
		{
			ulong num = 0UL;
			foreach (IMDTable imdtable in this.Tables)
			{
				if (imdtable.IsSorted)
				{
					num |= 1UL << (int)imdtable.Table;
				}
			}
			return num;
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x001B2D64 File Offset: 0x001B2D64
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04002A89 RID: 10889
		private uint length;

		// Token: 0x04002A8A RID: 10890
		private byte majorVersion;

		// Token: 0x04002A8B RID: 10891
		private byte minorVersion;

		// Token: 0x04002A8C RID: 10892
		private bool bigStrings;

		// Token: 0x04002A8D RID: 10893
		private bool bigGuid;

		// Token: 0x04002A8E RID: 10894
		private bool bigBlob;

		// Token: 0x04002A8F RID: 10895
		private bool hasDeletedRows;

		// Token: 0x04002A90 RID: 10896
		private readonly Metadata metadata;

		// Token: 0x04002A91 RID: 10897
		private readonly TablesHeapOptions options;

		// Token: 0x04002A92 RID: 10898
		private FileOffset offset;

		// Token: 0x04002A93 RID: 10899
		private RVA rva;

		// Token: 0x04002A94 RID: 10900
		public readonly MDTable<RawModuleRow> ModuleTable = new MDTable<RawModuleRow>(Table.Module, RawRowEqualityComparer.Instance);

		// Token: 0x04002A95 RID: 10901
		public readonly MDTable<RawTypeRefRow> TypeRefTable = new MDTable<RawTypeRefRow>(Table.TypeRef, RawRowEqualityComparer.Instance);

		// Token: 0x04002A96 RID: 10902
		public readonly MDTable<RawTypeDefRow> TypeDefTable = new MDTable<RawTypeDefRow>(Table.TypeDef, RawRowEqualityComparer.Instance);

		// Token: 0x04002A97 RID: 10903
		public readonly MDTable<RawFieldPtrRow> FieldPtrTable = new MDTable<RawFieldPtrRow>(Table.FieldPtr, RawRowEqualityComparer.Instance);

		// Token: 0x04002A98 RID: 10904
		public readonly MDTable<RawFieldRow> FieldTable = new MDTable<RawFieldRow>(Table.Field, RawRowEqualityComparer.Instance);

		// Token: 0x04002A99 RID: 10905
		public readonly MDTable<RawMethodPtrRow> MethodPtrTable = new MDTable<RawMethodPtrRow>(Table.MethodPtr, RawRowEqualityComparer.Instance);

		// Token: 0x04002A9A RID: 10906
		public readonly MDTable<RawMethodRow> MethodTable = new MDTable<RawMethodRow>(Table.Method, RawRowEqualityComparer.Instance);

		// Token: 0x04002A9B RID: 10907
		public readonly MDTable<RawParamPtrRow> ParamPtrTable = new MDTable<RawParamPtrRow>(Table.ParamPtr, RawRowEqualityComparer.Instance);

		// Token: 0x04002A9C RID: 10908
		public readonly MDTable<RawParamRow> ParamTable = new MDTable<RawParamRow>(Table.Param, RawRowEqualityComparer.Instance);

		// Token: 0x04002A9D RID: 10909
		public readonly MDTable<RawInterfaceImplRow> InterfaceImplTable = new MDTable<RawInterfaceImplRow>(Table.InterfaceImpl, RawRowEqualityComparer.Instance);

		// Token: 0x04002A9E RID: 10910
		public readonly MDTable<RawMemberRefRow> MemberRefTable = new MDTable<RawMemberRefRow>(Table.MemberRef, RawRowEqualityComparer.Instance);

		// Token: 0x04002A9F RID: 10911
		public readonly MDTable<RawConstantRow> ConstantTable = new MDTable<RawConstantRow>(Table.Constant, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA0 RID: 10912
		public readonly MDTable<RawCustomAttributeRow> CustomAttributeTable = new MDTable<RawCustomAttributeRow>(Table.CustomAttribute, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA1 RID: 10913
		public readonly MDTable<RawFieldMarshalRow> FieldMarshalTable = new MDTable<RawFieldMarshalRow>(Table.FieldMarshal, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA2 RID: 10914
		public readonly MDTable<RawDeclSecurityRow> DeclSecurityTable = new MDTable<RawDeclSecurityRow>(Table.DeclSecurity, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA3 RID: 10915
		public readonly MDTable<RawClassLayoutRow> ClassLayoutTable = new MDTable<RawClassLayoutRow>(Table.ClassLayout, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA4 RID: 10916
		public readonly MDTable<RawFieldLayoutRow> FieldLayoutTable = new MDTable<RawFieldLayoutRow>(Table.FieldLayout, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA5 RID: 10917
		public readonly MDTable<RawStandAloneSigRow> StandAloneSigTable = new MDTable<RawStandAloneSigRow>(Table.StandAloneSig, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA6 RID: 10918
		public readonly MDTable<RawEventMapRow> EventMapTable = new MDTable<RawEventMapRow>(Table.EventMap, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA7 RID: 10919
		public readonly MDTable<RawEventPtrRow> EventPtrTable = new MDTable<RawEventPtrRow>(Table.EventPtr, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA8 RID: 10920
		public readonly MDTable<RawEventRow> EventTable = new MDTable<RawEventRow>(Table.Event, RawRowEqualityComparer.Instance);

		// Token: 0x04002AA9 RID: 10921
		public readonly MDTable<RawPropertyMapRow> PropertyMapTable = new MDTable<RawPropertyMapRow>(Table.PropertyMap, RawRowEqualityComparer.Instance);

		// Token: 0x04002AAA RID: 10922
		public readonly MDTable<RawPropertyPtrRow> PropertyPtrTable = new MDTable<RawPropertyPtrRow>(Table.PropertyPtr, RawRowEqualityComparer.Instance);

		// Token: 0x04002AAB RID: 10923
		public readonly MDTable<RawPropertyRow> PropertyTable = new MDTable<RawPropertyRow>(Table.Property, RawRowEqualityComparer.Instance);

		// Token: 0x04002AAC RID: 10924
		public readonly MDTable<RawMethodSemanticsRow> MethodSemanticsTable = new MDTable<RawMethodSemanticsRow>(Table.MethodSemantics, RawRowEqualityComparer.Instance);

		// Token: 0x04002AAD RID: 10925
		public readonly MDTable<RawMethodImplRow> MethodImplTable = new MDTable<RawMethodImplRow>(Table.MethodImpl, RawRowEqualityComparer.Instance);

		// Token: 0x04002AAE RID: 10926
		public readonly MDTable<RawModuleRefRow> ModuleRefTable = new MDTable<RawModuleRefRow>(Table.ModuleRef, RawRowEqualityComparer.Instance);

		// Token: 0x04002AAF RID: 10927
		public readonly MDTable<RawTypeSpecRow> TypeSpecTable = new MDTable<RawTypeSpecRow>(Table.TypeSpec, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB0 RID: 10928
		public readonly MDTable<RawImplMapRow> ImplMapTable = new MDTable<RawImplMapRow>(Table.ImplMap, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB1 RID: 10929
		public readonly MDTable<RawFieldRVARow> FieldRVATable = new MDTable<RawFieldRVARow>(Table.FieldRVA, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB2 RID: 10930
		public readonly MDTable<RawENCLogRow> ENCLogTable = new MDTable<RawENCLogRow>(Table.ENCLog, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB3 RID: 10931
		public readonly MDTable<RawENCMapRow> ENCMapTable = new MDTable<RawENCMapRow>(Table.ENCMap, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB4 RID: 10932
		public readonly MDTable<RawAssemblyRow> AssemblyTable = new MDTable<RawAssemblyRow>(Table.Assembly, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB5 RID: 10933
		public readonly MDTable<RawAssemblyProcessorRow> AssemblyProcessorTable = new MDTable<RawAssemblyProcessorRow>(Table.AssemblyProcessor, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB6 RID: 10934
		public readonly MDTable<RawAssemblyOSRow> AssemblyOSTable = new MDTable<RawAssemblyOSRow>(Table.AssemblyOS, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB7 RID: 10935
		public readonly MDTable<RawAssemblyRefRow> AssemblyRefTable = new MDTable<RawAssemblyRefRow>(Table.AssemblyRef, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB8 RID: 10936
		public readonly MDTable<RawAssemblyRefProcessorRow> AssemblyRefProcessorTable = new MDTable<RawAssemblyRefProcessorRow>(Table.AssemblyRefProcessor, RawRowEqualityComparer.Instance);

		// Token: 0x04002AB9 RID: 10937
		public readonly MDTable<RawAssemblyRefOSRow> AssemblyRefOSTable = new MDTable<RawAssemblyRefOSRow>(Table.AssemblyRefOS, RawRowEqualityComparer.Instance);

		// Token: 0x04002ABA RID: 10938
		public readonly MDTable<RawFileRow> FileTable = new MDTable<RawFileRow>(Table.File, RawRowEqualityComparer.Instance);

		// Token: 0x04002ABB RID: 10939
		public readonly MDTable<RawExportedTypeRow> ExportedTypeTable = new MDTable<RawExportedTypeRow>(Table.ExportedType, RawRowEqualityComparer.Instance);

		// Token: 0x04002ABC RID: 10940
		public readonly MDTable<RawManifestResourceRow> ManifestResourceTable = new MDTable<RawManifestResourceRow>(Table.ManifestResource, RawRowEqualityComparer.Instance);

		// Token: 0x04002ABD RID: 10941
		public readonly MDTable<RawNestedClassRow> NestedClassTable = new MDTable<RawNestedClassRow>(Table.NestedClass, RawRowEqualityComparer.Instance);

		// Token: 0x04002ABE RID: 10942
		public readonly MDTable<RawGenericParamRow> GenericParamTable = new MDTable<RawGenericParamRow>(Table.GenericParam, RawRowEqualityComparer.Instance);

		// Token: 0x04002ABF RID: 10943
		public readonly MDTable<RawMethodSpecRow> MethodSpecTable = new MDTable<RawMethodSpecRow>(Table.MethodSpec, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC0 RID: 10944
		public readonly MDTable<RawGenericParamConstraintRow> GenericParamConstraintTable = new MDTable<RawGenericParamConstraintRow>(Table.GenericParamConstraint, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC1 RID: 10945
		public readonly MDTable<RawDocumentRow> DocumentTable = new MDTable<RawDocumentRow>(Table.Document, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC2 RID: 10946
		public readonly MDTable<RawMethodDebugInformationRow> MethodDebugInformationTable = new MDTable<RawMethodDebugInformationRow>(Table.MethodDebugInformation, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC3 RID: 10947
		public readonly MDTable<RawLocalScopeRow> LocalScopeTable = new MDTable<RawLocalScopeRow>(Table.LocalScope, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC4 RID: 10948
		public readonly MDTable<RawLocalVariableRow> LocalVariableTable = new MDTable<RawLocalVariableRow>(Table.LocalVariable, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC5 RID: 10949
		public readonly MDTable<RawLocalConstantRow> LocalConstantTable = new MDTable<RawLocalConstantRow>(Table.LocalConstant, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC6 RID: 10950
		public readonly MDTable<RawImportScopeRow> ImportScopeTable = new MDTable<RawImportScopeRow>(Table.ImportScope, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC7 RID: 10951
		public readonly MDTable<RawStateMachineMethodRow> StateMachineMethodTable = new MDTable<RawStateMachineMethodRow>(Table.StateMachineMethod, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC8 RID: 10952
		public readonly MDTable<RawCustomDebugInformationRow> CustomDebugInformationTable = new MDTable<RawCustomDebugInformationRow>(Table.CustomDebugInformation, RawRowEqualityComparer.Instance);

		// Token: 0x04002AC9 RID: 10953
		public readonly IMDTable[] Tables;

		// Token: 0x04002ACA RID: 10954
		private uint[] systemTables;

		// Token: 0x0200102B RID: 4139
		private struct RawDummyRow
		{
			// Token: 0x040044EA RID: 17642
			public static readonly IEqualityComparer<TablesHeap.RawDummyRow> Comparer = new TablesHeap.RawDummyRow.RawDummyRowEqualityComparer();

			// Token: 0x02001211 RID: 4625
			private sealed class RawDummyRowEqualityComparer : IEqualityComparer<TablesHeap.RawDummyRow>
			{
				// Token: 0x0600969E RID: 38558 RVA: 0x002CC778 File Offset: 0x002CC778
				public bool Equals(TablesHeap.RawDummyRow x, TablesHeap.RawDummyRow y)
				{
					throw new NotSupportedException();
				}

				// Token: 0x0600969F RID: 38559 RVA: 0x002CC780 File Offset: 0x002CC780
				public int GetHashCode(TablesHeap.RawDummyRow obj)
				{
					throw new NotSupportedException();
				}
			}
		}
	}
}
