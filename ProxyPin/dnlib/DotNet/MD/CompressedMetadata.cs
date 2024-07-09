using System;
using System.Collections.Generic;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000987 RID: 2439
	internal sealed class CompressedMetadata : MetadataBase
	{
		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06005DF0 RID: 24048 RVA: 0x001C2B44 File Offset: 0x001C2B44
		public override bool IsCompressed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x001C2B48 File Offset: 0x001C2B48
		public CompressedMetadata(IPEImage peImage, ImageCor20Header cor20Header, MetadataHeader mdHeader, CLRRuntimeReaderKind runtime) : base(peImage, cor20Header, mdHeader)
		{
			this.runtime = runtime;
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x001C2B5C File Offset: 0x001C2B5C
		internal CompressedMetadata(MetadataHeader mdHeader, bool isStandalonePortablePdb, CLRRuntimeReaderKind runtime) : base(mdHeader, isStandalonePortablePdb)
		{
			this.runtime = runtime;
		}

		// Token: 0x06005DF3 RID: 24051 RVA: 0x001C2B70 File Offset: 0x001C2B70
		protected override void InitializeInternal(DataReaderFactory mdReaderFactory, uint metadataBaseOffset)
		{
			DotNetStream dotNetStream = null;
			List<DotNetStream> list = new List<DotNetStream>(this.allStreams);
			try
			{
				int i = this.mdHeader.StreamHeaders.Count - 1;
				while (i >= 0)
				{
					StreamHeader streamHeader = this.mdHeader.StreamHeaders[i];
					string name = streamHeader.Name;
					if (name == null)
					{
						goto IL_1BF;
					}
					if (!(name == "#Strings"))
					{
						if (!(name == "#US"))
						{
							if (!(name == "#Blob"))
							{
								if (!(name == "#GUID"))
								{
									if (!(name == "#~"))
									{
										if (!(name == "#Pdb"))
										{
											goto IL_1BF;
										}
										if (!this.isStandalonePortablePdb || this.pdbStream != null)
										{
											goto IL_1BF;
										}
										this.pdbStream = new PdbStream(mdReaderFactory, metadataBaseOffset, streamHeader);
										list.Add(this.pdbStream);
									}
									else
									{
										if (this.tablesStream != null)
										{
											goto IL_1BF;
										}
										this.tablesStream = new TablesStream(mdReaderFactory, metadataBaseOffset, streamHeader, this.runtime);
										list.Add(this.tablesStream);
									}
								}
								else
								{
									if (this.guidStream != null)
									{
										goto IL_1BF;
									}
									this.guidStream = new GuidStream(mdReaderFactory, metadataBaseOffset, streamHeader);
									list.Add(this.guidStream);
								}
							}
							else
							{
								if (this.blobStream != null)
								{
									goto IL_1BF;
								}
								this.blobStream = new BlobStream(mdReaderFactory, metadataBaseOffset, streamHeader);
								list.Add(this.blobStream);
							}
						}
						else
						{
							if (this.usStream != null)
							{
								goto IL_1BF;
							}
							this.usStream = new USStream(mdReaderFactory, metadataBaseOffset, streamHeader);
							list.Add(this.usStream);
						}
					}
					else
					{
						if (this.stringsStream != null)
						{
							goto IL_1BF;
						}
						this.stringsStream = new StringsStream(mdReaderFactory, metadataBaseOffset, streamHeader);
						list.Add(this.stringsStream);
					}
					IL_1D1:
					i--;
					continue;
					IL_1BF:
					dotNetStream = new CustomDotNetStream(mdReaderFactory, metadataBaseOffset, streamHeader);
					list.Add(dotNetStream);
					dotNetStream = null;
					goto IL_1D1;
				}
			}
			finally
			{
				if (dotNetStream != null)
				{
					dotNetStream.Dispose();
				}
				list.Reverse();
				this.allStreams = list;
			}
			if (this.tablesStream == null)
			{
				throw new BadImageFormatException("Missing MD stream");
			}
			if (this.pdbStream != null)
			{
				this.tablesStream.Initialize(this.pdbStream.TypeSystemTableRows);
				return;
			}
			this.tablesStream.Initialize(null);
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x001C2DD8 File Offset: 0x001C2DD8
		public override RidList GetFieldRidList(uint typeDefRid)
		{
			return this.GetRidList(this.tablesStream.TypeDefTable, typeDefRid, 4, this.tablesStream.FieldTable);
		}

		// Token: 0x06005DF5 RID: 24053 RVA: 0x001C2E08 File Offset: 0x001C2E08
		public override RidList GetMethodRidList(uint typeDefRid)
		{
			return this.GetRidList(this.tablesStream.TypeDefTable, typeDefRid, 5, this.tablesStream.MethodTable);
		}

		// Token: 0x06005DF6 RID: 24054 RVA: 0x001C2E38 File Offset: 0x001C2E38
		public override RidList GetParamRidList(uint methodRid)
		{
			return this.GetRidList(this.tablesStream.MethodTable, methodRid, 5, this.tablesStream.ParamTable);
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x001C2E68 File Offset: 0x001C2E68
		public override RidList GetEventRidList(uint eventMapRid)
		{
			return this.GetRidList(this.tablesStream.EventMapTable, eventMapRid, 1, this.tablesStream.EventTable);
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x001C2E98 File Offset: 0x001C2E98
		public override RidList GetPropertyRidList(uint propertyMapRid)
		{
			return this.GetRidList(this.tablesStream.PropertyMapTable, propertyMapRid, 1, this.tablesStream.PropertyTable);
		}

		// Token: 0x06005DF9 RID: 24057 RVA: 0x001C2EC8 File Offset: 0x001C2EC8
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
			uint num4 = (!flag || (num2 == 0U && tableSourceRid + 1U == tableSource.Rows && tableDest.Rows == 65535U)) ? num3 : num2;
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

		// Token: 0x06005DFA RID: 24058 RVA: 0x001C2F90 File Offset: 0x001C2F90
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
			return 0U;
		}

		// Token: 0x04002DE7 RID: 11751
		private readonly CLRRuntimeReaderKind runtime;
	}
}
