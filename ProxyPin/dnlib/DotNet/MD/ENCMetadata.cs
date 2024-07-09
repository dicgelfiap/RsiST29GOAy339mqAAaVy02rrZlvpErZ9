using System;
using System.Collections.Generic;
using dnlib.IO;
using dnlib.PE;
using dnlib.Threading;

namespace dnlib.DotNet.MD
{
	// Token: 0x0200098C RID: 2444
	internal sealed class ENCMetadata : MetadataBase
	{
		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06005E15 RID: 24085 RVA: 0x001C4458 File Offset: 0x001C4458
		public override bool IsCompressed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x001C445C File Offset: 0x001C445C
		public ENCMetadata(IPEImage peImage, ImageCor20Header cor20Header, MetadataHeader mdHeader, CLRRuntimeReaderKind runtime) : base(peImage, cor20Header, mdHeader)
		{
			this.runtime = runtime;
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x001C4488 File Offset: 0x001C4488
		internal ENCMetadata(MetadataHeader mdHeader, bool isStandalonePortablePdb, CLRRuntimeReaderKind runtime) : base(mdHeader, isStandalonePortablePdb)
		{
			this.runtime = runtime;
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x001C44B0 File Offset: 0x001C44B0
		protected override void InitializeInternal(DataReaderFactory mdReaderFactory, uint metadataBaseOffset)
		{
			DotNetStream dotNetStream = null;
			try
			{
				if (this.runtime == CLRRuntimeReaderKind.Mono)
				{
					List<DotNetStream> list = new List<DotNetStream>(this.allStreams);
					int i = this.mdHeader.StreamHeaders.Count - 1;
					while (i >= 0)
					{
						StreamHeader streamHeader = this.mdHeader.StreamHeaders[i];
						string text = streamHeader.Name;
						if (text == null)
						{
							goto IL_27E;
						}
						uint num = dnlib.<PrivateImplementationDetails>.ComputeStringHash(text);
						if (num <= 617129517U)
						{
							if (num != 368124450U)
							{
								if (num != 491825896U)
								{
									if (num != 617129517U)
									{
										goto IL_27E;
									}
									if (!(text == "#-"))
									{
										goto IL_27E;
									}
								}
								else
								{
									if (!(text == "#Strings"))
									{
										goto IL_27E;
									}
									if (this.stringsStream == null)
									{
										this.stringsStream = new StringsStream(mdReaderFactory, metadataBaseOffset, streamHeader);
										list.Add(this.stringsStream);
										goto IL_290;
									}
									goto IL_27E;
								}
							}
							else
							{
								if (!(text == "#US"))
								{
									goto IL_27E;
								}
								if (this.usStream == null)
								{
									this.usStream = new USStream(mdReaderFactory, metadataBaseOffset, streamHeader);
									list.Add(this.usStream);
									goto IL_290;
								}
								goto IL_27E;
							}
						}
						else if (num <= 1422005491U)
						{
							if (num != 1372122372U)
							{
								if (num != 1422005491U)
								{
									goto IL_27E;
								}
								if (!(text == "#GUID"))
								{
									goto IL_27E;
								}
								if (this.guidStream == null)
								{
									this.guidStream = new GuidStream(mdReaderFactory, metadataBaseOffset, streamHeader);
									list.Add(this.guidStream);
									goto IL_290;
								}
								goto IL_27E;
							}
							else if (!(text == "#~"))
							{
								goto IL_27E;
							}
						}
						else if (num != 1638201209U)
						{
							if (num != 2979271308U)
							{
								goto IL_27E;
							}
							if (!(text == "#Pdb"))
							{
								goto IL_27E;
							}
							if (this.isStandalonePortablePdb && this.pdbStream == null)
							{
								this.pdbStream = new PdbStream(mdReaderFactory, metadataBaseOffset, streamHeader);
								list.Add(this.pdbStream);
								goto IL_290;
							}
							goto IL_27E;
						}
						else
						{
							if (!(text == "#Blob"))
							{
								goto IL_27E;
							}
							if (this.blobStream == null)
							{
								this.blobStream = new BlobStream(mdReaderFactory, metadataBaseOffset, streamHeader);
								list.Add(this.blobStream);
								goto IL_290;
							}
							goto IL_27E;
						}
						if (this.tablesStream != null)
						{
							goto IL_27E;
						}
						this.tablesStream = new TablesStream(mdReaderFactory, metadataBaseOffset, streamHeader, this.runtime);
						list.Add(this.tablesStream);
						IL_290:
						i--;
						continue;
						IL_27E:
						dotNetStream = new CustomDotNetStream(mdReaderFactory, metadataBaseOffset, streamHeader);
						list.Add(dotNetStream);
						dotNetStream = null;
						goto IL_290;
					}
					list.Reverse();
					this.allStreams = list;
				}
				else
				{
					foreach (StreamHeader streamHeader2 in this.mdHeader.StreamHeaders)
					{
						string text = streamHeader2.Name.ToUpperInvariant();
						if (text != null)
						{
							uint num = dnlib.<PrivateImplementationDetails>.ComputeStringHash(text);
							if (num <= 617129517U)
							{
								if (num != 368124450U)
								{
									if (num != 485378073U)
									{
										if (num != 617129517U)
										{
											goto IL_542;
										}
										if (!(text == "#-"))
										{
											goto IL_542;
										}
									}
									else
									{
										if (!(text == "#BLOB"))
										{
											goto IL_542;
										}
										if (this.blobStream == null)
										{
											this.blobStream = new BlobStream(mdReaderFactory, metadataBaseOffset, streamHeader2);
											this.allStreams.Add(this.blobStream);
											continue;
										}
										goto IL_542;
									}
								}
								else
								{
									if (!(text == "#US"))
									{
										goto IL_542;
									}
									if (this.usStream == null)
									{
										this.usStream = new USStream(mdReaderFactory, metadataBaseOffset, streamHeader2);
										this.allStreams.Add(this.usStream);
										continue;
									}
									goto IL_542;
								}
							}
							else if (num <= 1422005491U)
							{
								if (num != 1372122372U)
								{
									if (num != 1422005491U)
									{
										goto IL_542;
									}
									if (!(text == "#GUID"))
									{
										goto IL_542;
									}
									if (this.guidStream == null)
									{
										this.guidStream = new GuidStream(mdReaderFactory, metadataBaseOffset, streamHeader2);
										this.allStreams.Add(this.guidStream);
										continue;
									}
									goto IL_542;
								}
								else if (!(text == "#~"))
								{
									goto IL_542;
								}
							}
							else if (num != 2447610380U)
							{
								if (num != 3982554728U)
								{
									goto IL_542;
								}
								if (!(text == "#STRINGS"))
								{
									goto IL_542;
								}
								if (this.stringsStream == null)
								{
									this.stringsStream = new StringsStream(mdReaderFactory, metadataBaseOffset, streamHeader2);
									this.allStreams.Add(this.stringsStream);
									continue;
								}
								goto IL_542;
							}
							else
							{
								if (!(text == "#PDB"))
								{
									goto IL_542;
								}
								if (this.isStandalonePortablePdb && this.pdbStream == null && streamHeader2.Name == "#Pdb")
								{
									this.pdbStream = new PdbStream(mdReaderFactory, metadataBaseOffset, streamHeader2);
									this.allStreams.Add(this.pdbStream);
									continue;
								}
								goto IL_542;
							}
							if (this.tablesStream == null)
							{
								this.tablesStream = new TablesStream(mdReaderFactory, metadataBaseOffset, streamHeader2, this.runtime);
								this.allStreams.Add(this.tablesStream);
								continue;
							}
						}
						IL_542:
						dotNetStream = new CustomDotNetStream(mdReaderFactory, metadataBaseOffset, streamHeader2);
						this.allStreams.Add(dotNetStream);
						dotNetStream = null;
					}
				}
			}
			finally
			{
				if (dotNetStream != null)
				{
					dotNetStream.Dispose();
				}
			}
			if (this.tablesStream == null)
			{
				throw new BadImageFormatException("Missing MD stream");
			}
			if (this.pdbStream != null)
			{
				this.tablesStream.Initialize(this.pdbStream.TypeSystemTableRows);
			}
			else
			{
				this.tablesStream.Initialize(null);
			}
			this.hasFieldPtr = !this.tablesStream.FieldPtrTable.IsEmpty;
			this.hasMethodPtr = !this.tablesStream.MethodPtrTable.IsEmpty;
			this.hasParamPtr = !this.tablesStream.ParamPtrTable.IsEmpty;
			this.hasEventPtr = !this.tablesStream.EventPtrTable.IsEmpty;
			this.hasPropertyPtr = !this.tablesStream.PropertyPtrTable.IsEmpty;
			this.hasDeletedRows = this.tablesStream.HasDelete;
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x001C4B50 File Offset: 0x001C4B50
		public override RidList GetTypeDefRidList()
		{
			if (!this.hasDeletedRows)
			{
				return base.GetTypeDefRidList();
			}
			uint rows = this.tablesStream.TypeDefTable.Rows;
			List<uint> list = new List<uint>((int)rows);
			for (uint num = 1U; num <= rows; num += 1U)
			{
				RawTypeDefRow rawTypeDefRow;
				if (this.tablesStream.TryReadTypeDefRow(num, out rawTypeDefRow) && !this.stringsStream.ReadNoNull(rawTypeDefRow.Name).StartsWith(ENCMetadata.DeletedName))
				{
					list.Add(num);
				}
			}
			return RidList.Create(list);
		}

		// Token: 0x06005E1A RID: 24090 RVA: 0x001C4BE0 File Offset: 0x001C4BE0
		public override RidList GetExportedTypeRidList()
		{
			if (!this.hasDeletedRows)
			{
				return base.GetExportedTypeRidList();
			}
			uint rows = this.tablesStream.ExportedTypeTable.Rows;
			List<uint> list = new List<uint>((int)rows);
			for (uint num = 1U; num <= rows; num += 1U)
			{
				RawExportedTypeRow rawExportedTypeRow;
				if (this.tablesStream.TryReadExportedTypeRow(num, out rawExportedTypeRow) && !this.stringsStream.ReadNoNull(rawExportedTypeRow.TypeName).StartsWith(ENCMetadata.DeletedName))
				{
					list.Add(num);
				}
			}
			return RidList.Create(list);
		}

		// Token: 0x06005E1B RID: 24091 RVA: 0x001C4C70 File Offset: 0x001C4C70
		private uint ToFieldRid(uint listRid)
		{
			if (!this.hasFieldPtr)
			{
				return listRid;
			}
			uint result;
			if (!this.tablesStream.TryReadColumn24(this.tablesStream.FieldPtrTable, listRid, 0, out result))
			{
				return 0U;
			}
			return result;
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x001C4CB0 File Offset: 0x001C4CB0
		private uint ToMethodRid(uint listRid)
		{
			if (!this.hasMethodPtr)
			{
				return listRid;
			}
			uint result;
			if (!this.tablesStream.TryReadColumn24(this.tablesStream.MethodPtrTable, listRid, 0, out result))
			{
				return 0U;
			}
			return result;
		}

		// Token: 0x06005E1D RID: 24093 RVA: 0x001C4CF0 File Offset: 0x001C4CF0
		private uint ToParamRid(uint listRid)
		{
			if (!this.hasParamPtr)
			{
				return listRid;
			}
			uint result;
			if (!this.tablesStream.TryReadColumn24(this.tablesStream.ParamPtrTable, listRid, 0, out result))
			{
				return 0U;
			}
			return result;
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x001C4D30 File Offset: 0x001C4D30
		private uint ToEventRid(uint listRid)
		{
			if (!this.hasEventPtr)
			{
				return listRid;
			}
			uint result;
			if (!this.tablesStream.TryReadColumn24(this.tablesStream.EventPtrTable, listRid, 0, out result))
			{
				return 0U;
			}
			return result;
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x001C4D70 File Offset: 0x001C4D70
		private uint ToPropertyRid(uint listRid)
		{
			if (!this.hasPropertyPtr)
			{
				return listRid;
			}
			uint result;
			if (!this.tablesStream.TryReadColumn24(this.tablesStream.PropertyPtrTable, listRid, 0, out result))
			{
				return 0U;
			}
			return result;
		}

		// Token: 0x06005E20 RID: 24096 RVA: 0x001C4DB0 File Offset: 0x001C4DB0
		public override RidList GetFieldRidList(uint typeDefRid)
		{
			RidList ridList = this.GetRidList(this.tablesStream.TypeDefTable, typeDefRid, 4, this.tablesStream.FieldTable);
			if (ridList.Count == 0 || (!this.hasFieldPtr && !this.hasDeletedRows))
			{
				return ridList;
			}
			MDTable fieldTable = this.tablesStream.FieldTable;
			List<uint> list = new List<uint>(ridList.Count);
			for (int i = 0; i < ridList.Count; i++)
			{
				uint num = this.ToFieldRid(ridList[i]);
				RawFieldRow rawFieldRow;
				if (!fieldTable.IsInvalidRID(num) && (!this.hasDeletedRows || (this.tablesStream.TryReadFieldRow(num, out rawFieldRow) && ((rawFieldRow.Flags & 1024) == 0 || !this.stringsStream.ReadNoNull(rawFieldRow.Name).StartsWith(ENCMetadata.DeletedName)))))
				{
					list.Add(num);
				}
			}
			return RidList.Create(list);
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x001C4EB8 File Offset: 0x001C4EB8
		public override RidList GetMethodRidList(uint typeDefRid)
		{
			RidList ridList = this.GetRidList(this.tablesStream.TypeDefTable, typeDefRid, 5, this.tablesStream.MethodTable);
			if (ridList.Count == 0 || (!this.hasMethodPtr && !this.hasDeletedRows))
			{
				return ridList;
			}
			MDTable methodTable = this.tablesStream.MethodTable;
			List<uint> list = new List<uint>(ridList.Count);
			for (int i = 0; i < ridList.Count; i++)
			{
				uint num = this.ToMethodRid(ridList[i]);
				RawMethodRow rawMethodRow;
				if (!methodTable.IsInvalidRID(num) && (!this.hasDeletedRows || (this.tablesStream.TryReadMethodRow(num, out rawMethodRow) && ((rawMethodRow.Flags & 4096) == 0 || !this.stringsStream.ReadNoNull(rawMethodRow.Name).StartsWith(ENCMetadata.DeletedName)))))
				{
					list.Add(num);
				}
			}
			return RidList.Create(list);
		}

		// Token: 0x06005E22 RID: 24098 RVA: 0x001C4FC0 File Offset: 0x001C4FC0
		public override RidList GetParamRidList(uint methodRid)
		{
			RidList ridList = this.GetRidList(this.tablesStream.MethodTable, methodRid, 5, this.tablesStream.ParamTable);
			if (ridList.Count == 0 || !this.hasParamPtr)
			{
				return ridList;
			}
			MDTable paramTable = this.tablesStream.ParamTable;
			List<uint> list = new List<uint>(ridList.Count);
			for (int i = 0; i < ridList.Count; i++)
			{
				uint num = this.ToParamRid(ridList[i]);
				if (!paramTable.IsInvalidRID(num))
				{
					list.Add(num);
				}
			}
			return RidList.Create(list);
		}

		// Token: 0x06005E23 RID: 24099 RVA: 0x001C5064 File Offset: 0x001C5064
		public override RidList GetEventRidList(uint eventMapRid)
		{
			RidList ridList = this.GetRidList(this.tablesStream.EventMapTable, eventMapRid, 1, this.tablesStream.EventTable);
			if (ridList.Count == 0 || (!this.hasEventPtr && !this.hasDeletedRows))
			{
				return ridList;
			}
			MDTable eventTable = this.tablesStream.EventTable;
			List<uint> list = new List<uint>(ridList.Count);
			for (int i = 0; i < ridList.Count; i++)
			{
				uint num = this.ToEventRid(ridList[i]);
				RawEventRow rawEventRow;
				if (!eventTable.IsInvalidRID(num) && (!this.hasDeletedRows || (this.tablesStream.TryReadEventRow(num, out rawEventRow) && ((rawEventRow.EventFlags & 1024) == 0 || !this.stringsStream.ReadNoNull(rawEventRow.Name).StartsWith(ENCMetadata.DeletedName)))))
				{
					list.Add(num);
				}
			}
			return RidList.Create(list);
		}

		// Token: 0x06005E24 RID: 24100 RVA: 0x001C516C File Offset: 0x001C516C
		public override RidList GetPropertyRidList(uint propertyMapRid)
		{
			RidList ridList = this.GetRidList(this.tablesStream.PropertyMapTable, propertyMapRid, 1, this.tablesStream.PropertyTable);
			if (ridList.Count == 0 || (!this.hasPropertyPtr && !this.hasDeletedRows))
			{
				return ridList;
			}
			MDTable propertyTable = this.tablesStream.PropertyTable;
			List<uint> list = new List<uint>(ridList.Count);
			for (int i = 0; i < ridList.Count; i++)
			{
				uint num = this.ToPropertyRid(ridList[i]);
				RawPropertyRow rawPropertyRow;
				if (!propertyTable.IsInvalidRID(num) && (!this.hasDeletedRows || (this.tablesStream.TryReadPropertyRow(num, out rawPropertyRow) && ((rawPropertyRow.PropFlags & 1024) == 0 || !this.stringsStream.ReadNoNull(rawPropertyRow.Name).StartsWith(ENCMetadata.DeletedName)))))
				{
					list.Add(num);
				}
			}
			return RidList.Create(list);
		}

		// Token: 0x06005E25 RID: 24101 RVA: 0x001C5274 File Offset: 0x001C5274
		private RidList GetRidList(MDTable tableSource, uint tableSourceRid, int colIndex, MDTable tableDest)
		{
			ColumnInfo column = tableSource.TableInfo.Columns[colIndex];
			uint num;
			if (!this.tablesStream.TryReadColumn24(tableSource, tableSourceRid, column, out num))
			{
				return RidList.Empty;
			}
			uint num2;
			bool flag = this.tablesStream.TryReadColumn24(tableSource, tableSourceRid + 1U, column, out num2);
			uint num3 = tableDest.Rows + 1U;
			if (num == 0U || num >= num3)
			{
				return RidList.Empty;
			}
			uint num4 = (flag && num2 != 0U) ? num2 : num3;
			if (num4 < num)
			{
				num4 = num;
			}
			if (num4 > num3)
			{
				num4 = num3;
			}
			return RidList.Create(num, num4 - num);
		}

		// Token: 0x06005E26 RID: 24102 RVA: 0x001C531C File Offset: 0x001C531C
		protected override uint BinarySearch(MDTable tableSource, int keyColIndex, uint key)
		{
			ColumnInfo column = tableSource.TableInfo.Columns[keyColIndex];
			uint num = 1U;
			uint num2 = tableSource.Rows;
			while (num <= num2)
			{
				uint num3 = (num + num2) / 2U;
				uint num4;
				if (!this.tablesStream.TryReadColumn24(tableSource, num3, column, out num4))
				{
					break;
				}
				if (key == num4)
				{
					return num3;
				}
				if (num4 > key)
				{
					num2 = num3 - 1U;
				}
				else
				{
					num = num3 + 1U;
				}
			}
			if (tableSource.Table == Table.GenericParam && !this.tablesStream.IsSorted(tableSource))
			{
				return this.LinearSearch(tableSource, keyColIndex, key);
			}
			return 0U;
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x001C53B4 File Offset: 0x001C53B4
		private uint LinearSearch(MDTable tableSource, int keyColIndex, uint key)
		{
			if (tableSource == null)
			{
				return 0U;
			}
			ColumnInfo column = tableSource.TableInfo.Columns[keyColIndex];
			uint num = 1U;
			uint num2;
			while (num <= tableSource.Rows && this.tablesStream.TryReadColumn24(tableSource, num, column, out num2))
			{
				if (key == num2)
				{
					return num;
				}
				num += 1U;
			}
			return 0U;
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x001C5414 File Offset: 0x001C5414
		protected override RidList FindAllRowsUnsorted(MDTable tableSource, int keyColIndex, uint key)
		{
			if (this.tablesStream.IsSorted(tableSource))
			{
				return base.FindAllRows(tableSource, keyColIndex, key);
			}
			this.theLock.EnterWriteLock();
			MetadataBase.SortedTable sortedTable;
			try
			{
				if (!this.sortedTables.TryGetValue(tableSource.Table, out sortedTable))
				{
					sortedTable = (this.sortedTables[tableSource.Table] = new MetadataBase.SortedTable(tableSource, keyColIndex));
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return sortedTable.FindAllRows(key);
		}

		// Token: 0x04002DF1 RID: 11761
		private static readonly UTF8String DeletedName = "_Deleted";

		// Token: 0x04002DF2 RID: 11762
		private bool hasMethodPtr;

		// Token: 0x04002DF3 RID: 11763
		private bool hasFieldPtr;

		// Token: 0x04002DF4 RID: 11764
		private bool hasParamPtr;

		// Token: 0x04002DF5 RID: 11765
		private bool hasEventPtr;

		// Token: 0x04002DF6 RID: 11766
		private bool hasPropertyPtr;

		// Token: 0x04002DF7 RID: 11767
		private bool hasDeletedRows;

		// Token: 0x04002DF8 RID: 11768
		private readonly CLRRuntimeReaderKind runtime;

		// Token: 0x04002DF9 RID: 11769
		private readonly Dictionary<Table, MetadataBase.SortedTable> sortedTables = new Dictionary<Table, MetadataBase.SortedTable>();

		// Token: 0x04002DFA RID: 11770
		private readonly Lock theLock = Lock.Create();
	}
}
