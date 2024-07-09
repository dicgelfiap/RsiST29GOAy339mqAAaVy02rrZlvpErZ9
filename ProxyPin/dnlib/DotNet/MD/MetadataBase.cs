using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000996 RID: 2454
	internal abstract class MetadataBase : Metadata
	{
		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06005E81 RID: 24193 RVA: 0x001C5864 File Offset: 0x001C5864
		public override bool IsStandalonePortablePdb
		{
			get
			{
				return this.isStandalonePortablePdb;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06005E82 RID: 24194 RVA: 0x001C586C File Offset: 0x001C586C
		public override ImageCor20Header ImageCor20Header
		{
			get
			{
				return this.cor20Header;
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06005E83 RID: 24195 RVA: 0x001C5874 File Offset: 0x001C5874
		public override uint Version
		{
			get
			{
				return (uint)((int)this.mdHeader.MajorVersion << 16 | (int)this.mdHeader.MinorVersion);
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06005E84 RID: 24196 RVA: 0x001C5890 File Offset: 0x001C5890
		public override string VersionString
		{
			get
			{
				return this.mdHeader.VersionString;
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06005E85 RID: 24197 RVA: 0x001C58A0 File Offset: 0x001C58A0
		public override IPEImage PEImage
		{
			get
			{
				return this.peImage;
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06005E86 RID: 24198 RVA: 0x001C58A8 File Offset: 0x001C58A8
		public override MetadataHeader MetadataHeader
		{
			get
			{
				return this.mdHeader;
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06005E87 RID: 24199 RVA: 0x001C58B0 File Offset: 0x001C58B0
		public override StringsStream StringsStream
		{
			get
			{
				return this.stringsStream;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06005E88 RID: 24200 RVA: 0x001C58B8 File Offset: 0x001C58B8
		public override USStream USStream
		{
			get
			{
				return this.usStream;
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06005E89 RID: 24201 RVA: 0x001C58C0 File Offset: 0x001C58C0
		public override BlobStream BlobStream
		{
			get
			{
				return this.blobStream;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06005E8A RID: 24202 RVA: 0x001C58C8 File Offset: 0x001C58C8
		public override GuidStream GuidStream
		{
			get
			{
				return this.guidStream;
			}
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06005E8B RID: 24203 RVA: 0x001C58D0 File Offset: 0x001C58D0
		public override TablesStream TablesStream
		{
			get
			{
				return this.tablesStream;
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06005E8C RID: 24204 RVA: 0x001C58D8 File Offset: 0x001C58D8
		public override PdbStream PdbStream
		{
			get
			{
				return this.pdbStream;
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06005E8D RID: 24205 RVA: 0x001C58E0 File Offset: 0x001C58E0
		public override IList<DotNetStream> AllStreams
		{
			get
			{
				return this.allStreams;
			}
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x001C58E8 File Offset: 0x001C58E8
		protected MetadataBase(IPEImage peImage, ImageCor20Header cor20Header, MetadataHeader mdHeader)
		{
			try
			{
				this.allStreams = new List<DotNetStream>();
				this.peImage = peImage;
				this.cor20Header = cor20Header;
				this.mdHeader = mdHeader;
				this.isStandalonePortablePdb = false;
			}
			catch
			{
				if (peImage != null)
				{
					peImage.Dispose();
				}
				throw;
			}
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x001C5948 File Offset: 0x001C5948
		internal MetadataBase(MetadataHeader mdHeader, bool isStandalonePortablePdb)
		{
			this.allStreams = new List<DotNetStream>();
			this.peImage = null;
			this.cor20Header = null;
			this.mdHeader = mdHeader;
			this.isStandalonePortablePdb = isStandalonePortablePdb;
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x001C5978 File Offset: 0x001C5978
		public void Initialize(DataReaderFactory mdReaderFactory)
		{
			this.mdReaderFactoryToDisposeLater = mdReaderFactory;
			uint metadataBaseOffset;
			if (this.peImage != null)
			{
				metadataBaseOffset = (uint)this.peImage.ToFileOffset(this.cor20Header.Metadata.VirtualAddress);
				mdReaderFactory = this.peImage.DataReaderFactory;
			}
			else
			{
				metadataBaseOffset = 0U;
			}
			this.InitializeInternal(mdReaderFactory, metadataBaseOffset);
			if (this.tablesStream == null)
			{
				throw new BadImageFormatException("Missing MD stream");
			}
			if (this.isStandalonePortablePdb && this.pdbStream == null)
			{
				throw new BadImageFormatException("Missing #Pdb stream");
			}
			this.InitializeNonExistentHeaps();
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x001C5A10 File Offset: 0x001C5A10
		protected void InitializeNonExistentHeaps()
		{
			if (this.stringsStream == null)
			{
				this.stringsStream = new StringsStream();
			}
			if (this.usStream == null)
			{
				this.usStream = new USStream();
			}
			if (this.blobStream == null)
			{
				this.blobStream = new BlobStream();
			}
			if (this.guidStream == null)
			{
				this.guidStream = new GuidStream();
			}
		}

		// Token: 0x06005E92 RID: 24210
		protected abstract void InitializeInternal(DataReaderFactory mdReaderFactory, uint metadataBaseOffset);

		// Token: 0x06005E93 RID: 24211 RVA: 0x001C5A7C File Offset: 0x001C5A7C
		public override RidList GetTypeDefRidList()
		{
			return RidList.Create(1U, this.tablesStream.TypeDefTable.Rows);
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x001C5A94 File Offset: 0x001C5A94
		public override RidList GetExportedTypeRidList()
		{
			return RidList.Create(1U, this.tablesStream.ExportedTypeTable.Rows);
		}

		// Token: 0x06005E95 RID: 24213
		protected abstract uint BinarySearch(MDTable tableSource, int keyColIndex, uint key);

		// Token: 0x06005E96 RID: 24214 RVA: 0x001C5AAC File Offset: 0x001C5AAC
		protected RidList FindAllRows(MDTable tableSource, int keyColIndex, uint key)
		{
			uint num = this.BinarySearch(tableSource, keyColIndex, key);
			if (tableSource.IsInvalidRID(num))
			{
				return RidList.Empty;
			}
			uint num2 = num + 1U;
			ColumnInfo column = tableSource.TableInfo.Columns[keyColIndex];
			while (num > 1U)
			{
				uint num3;
				if (!this.tablesStream.TryReadColumn24(tableSource, num - 1U, column, out num3) || key != num3)
				{
					IL_83:
					uint num4;
					while (num2 <= tableSource.Rows && this.tablesStream.TryReadColumn24(tableSource, num2, column, out num4) && key == num4)
					{
						num2 += 1U;
					}
					return RidList.Create(num, num2 - num);
				}
				num -= 1U;
			}
			goto IL_83;
		}

		// Token: 0x06005E97 RID: 24215 RVA: 0x001C5B54 File Offset: 0x001C5B54
		protected virtual RidList FindAllRowsUnsorted(MDTable tableSource, int keyColIndex, uint key)
		{
			return this.FindAllRows(tableSource, keyColIndex, key);
		}

		// Token: 0x06005E98 RID: 24216 RVA: 0x001C5B60 File Offset: 0x001C5B60
		public override RidList GetInterfaceImplRidList(uint typeDefRid)
		{
			return this.FindAllRowsUnsorted(this.tablesStream.InterfaceImplTable, 0, typeDefRid);
		}

		// Token: 0x06005E99 RID: 24217 RVA: 0x001C5B78 File Offset: 0x001C5B78
		public override RidList GetGenericParamRidList(Table table, uint rid)
		{
			uint key;
			if (!CodedToken.TypeOrMethodDef.Encode(new MDToken(table, rid), out key))
			{
				return RidList.Empty;
			}
			return this.FindAllRowsUnsorted(this.tablesStream.GenericParamTable, 2, key);
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x001C5BBC File Offset: 0x001C5BBC
		public override RidList GetGenericParamConstraintRidList(uint genericParamRid)
		{
			return this.FindAllRowsUnsorted(this.tablesStream.GenericParamConstraintTable, 0, genericParamRid);
		}

		// Token: 0x06005E9B RID: 24219 RVA: 0x001C5BD4 File Offset: 0x001C5BD4
		public override RidList GetCustomAttributeRidList(Table table, uint rid)
		{
			uint key;
			if (!CodedToken.HasCustomAttribute.Encode(new MDToken(table, rid), out key))
			{
				return RidList.Empty;
			}
			return this.FindAllRowsUnsorted(this.tablesStream.CustomAttributeTable, 0, key);
		}

		// Token: 0x06005E9C RID: 24220 RVA: 0x001C5C18 File Offset: 0x001C5C18
		public override RidList GetDeclSecurityRidList(Table table, uint rid)
		{
			uint key;
			if (!CodedToken.HasDeclSecurity.Encode(new MDToken(table, rid), out key))
			{
				return RidList.Empty;
			}
			return this.FindAllRowsUnsorted(this.tablesStream.DeclSecurityTable, 1, key);
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x001C5C5C File Offset: 0x001C5C5C
		public override RidList GetMethodSemanticsRidList(Table table, uint rid)
		{
			uint key;
			if (!CodedToken.HasSemantic.Encode(new MDToken(table, rid), out key))
			{
				return RidList.Empty;
			}
			return this.FindAllRowsUnsorted(this.tablesStream.MethodSemanticsTable, 2, key);
		}

		// Token: 0x06005E9E RID: 24222 RVA: 0x001C5CA0 File Offset: 0x001C5CA0
		public override RidList GetMethodImplRidList(uint typeDefRid)
		{
			return this.FindAllRowsUnsorted(this.tablesStream.MethodImplTable, 0, typeDefRid);
		}

		// Token: 0x06005E9F RID: 24223 RVA: 0x001C5CB8 File Offset: 0x001C5CB8
		public override uint GetClassLayoutRid(uint typeDefRid)
		{
			RidList ridList = this.FindAllRowsUnsorted(this.tablesStream.ClassLayoutTable, 2, typeDefRid);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x001C5CF4 File Offset: 0x001C5CF4
		public override uint GetFieldLayoutRid(uint fieldRid)
		{
			RidList ridList = this.FindAllRowsUnsorted(this.tablesStream.FieldLayoutTable, 1, fieldRid);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x001C5D30 File Offset: 0x001C5D30
		public override uint GetFieldMarshalRid(Table table, uint rid)
		{
			uint key;
			if (!CodedToken.HasFieldMarshal.Encode(new MDToken(table, rid), out key))
			{
				return 0U;
			}
			RidList ridList = this.FindAllRowsUnsorted(this.tablesStream.FieldMarshalTable, 0, key);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x001C5D88 File Offset: 0x001C5D88
		public override uint GetFieldRVARid(uint fieldRid)
		{
			RidList ridList = this.FindAllRowsUnsorted(this.tablesStream.FieldRVATable, 1, fieldRid);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x001C5DC4 File Offset: 0x001C5DC4
		public override uint GetImplMapRid(Table table, uint rid)
		{
			uint key;
			if (!CodedToken.MemberForwarded.Encode(new MDToken(table, rid), out key))
			{
				return 0U;
			}
			RidList ridList = this.FindAllRowsUnsorted(this.tablesStream.ImplMapTable, 1, key);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x001C5E1C File Offset: 0x001C5E1C
		public override uint GetNestedClassRid(uint typeDefRid)
		{
			RidList ridList = this.FindAllRowsUnsorted(this.tablesStream.NestedClassTable, 0, typeDefRid);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x001C5E58 File Offset: 0x001C5E58
		public override uint GetEventMapRid(uint typeDefRid)
		{
			if (this.eventMapSortedTable == null)
			{
				Interlocked.CompareExchange<MetadataBase.SortedTable>(ref this.eventMapSortedTable, new MetadataBase.SortedTable(this.tablesStream.EventMapTable, 0), null);
			}
			RidList ridList = this.eventMapSortedTable.FindAllRows(typeDefRid);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x001C5EB8 File Offset: 0x001C5EB8
		public override uint GetPropertyMapRid(uint typeDefRid)
		{
			if (this.propertyMapSortedTable == null)
			{
				Interlocked.CompareExchange<MetadataBase.SortedTable>(ref this.propertyMapSortedTable, new MetadataBase.SortedTable(this.tablesStream.PropertyMapTable, 0), null);
			}
			RidList ridList = this.propertyMapSortedTable.FindAllRows(typeDefRid);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EA7 RID: 24231 RVA: 0x001C5F18 File Offset: 0x001C5F18
		public override uint GetConstantRid(Table table, uint rid)
		{
			uint key;
			if (!CodedToken.HasConstant.Encode(new MDToken(table, rid), out key))
			{
				return 0U;
			}
			RidList ridList = this.FindAllRowsUnsorted(this.tablesStream.ConstantTable, 2, key);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EA8 RID: 24232 RVA: 0x001C5F70 File Offset: 0x001C5F70
		public override uint GetOwnerTypeOfField(uint fieldRid)
		{
			if (this.fieldRidToTypeDefRid == null)
			{
				this.InitializeInverseFieldOwnerRidList();
			}
			uint num = fieldRid - 1U;
			if ((ulong)num >= (ulong)((long)this.fieldRidToTypeDefRid.Length))
			{
				return 0U;
			}
			return this.fieldRidToTypeDefRid[(int)num];
		}

		// Token: 0x06005EA9 RID: 24233 RVA: 0x001C5FB0 File Offset: 0x001C5FB0
		private void InitializeInverseFieldOwnerRidList()
		{
			if (this.fieldRidToTypeDefRid != null)
			{
				return;
			}
			uint[] array = new uint[this.tablesStream.FieldTable.Rows];
			RidList typeDefRidList = this.GetTypeDefRidList();
			for (int i = 0; i < typeDefRidList.Count; i++)
			{
				uint num = typeDefRidList[i];
				RidList fieldRidList = this.GetFieldRidList(num);
				for (int j = 0; j < fieldRidList.Count; j++)
				{
					uint num2 = fieldRidList[j] - 1U;
					if (array[(int)num2] == 0U)
					{
						array[(int)num2] = num;
					}
				}
			}
			Interlocked.CompareExchange<uint[]>(ref this.fieldRidToTypeDefRid, array, null);
		}

		// Token: 0x06005EAA RID: 24234 RVA: 0x001C6054 File Offset: 0x001C6054
		public override uint GetOwnerTypeOfMethod(uint methodRid)
		{
			if (this.methodRidToTypeDefRid == null)
			{
				this.InitializeInverseMethodOwnerRidList();
			}
			uint num = methodRid - 1U;
			if ((ulong)num >= (ulong)((long)this.methodRidToTypeDefRid.Length))
			{
				return 0U;
			}
			return this.methodRidToTypeDefRid[(int)num];
		}

		// Token: 0x06005EAB RID: 24235 RVA: 0x001C6094 File Offset: 0x001C6094
		private void InitializeInverseMethodOwnerRidList()
		{
			if (this.methodRidToTypeDefRid != null)
			{
				return;
			}
			uint[] array = new uint[this.tablesStream.MethodTable.Rows];
			RidList typeDefRidList = this.GetTypeDefRidList();
			for (int i = 0; i < typeDefRidList.Count; i++)
			{
				uint num = typeDefRidList[i];
				RidList methodRidList = this.GetMethodRidList(num);
				for (int j = 0; j < methodRidList.Count; j++)
				{
					uint num2 = methodRidList[j] - 1U;
					if (array[(int)num2] == 0U)
					{
						array[(int)num2] = num;
					}
				}
			}
			Interlocked.CompareExchange<uint[]>(ref this.methodRidToTypeDefRid, array, null);
		}

		// Token: 0x06005EAC RID: 24236 RVA: 0x001C6138 File Offset: 0x001C6138
		public override uint GetOwnerTypeOfEvent(uint eventRid)
		{
			if (this.eventRidToTypeDefRid == null)
			{
				this.InitializeInverseEventOwnerRidList();
			}
			uint num = eventRid - 1U;
			if ((ulong)num >= (ulong)((long)this.eventRidToTypeDefRid.Length))
			{
				return 0U;
			}
			return this.eventRidToTypeDefRid[(int)num];
		}

		// Token: 0x06005EAD RID: 24237 RVA: 0x001C6178 File Offset: 0x001C6178
		private void InitializeInverseEventOwnerRidList()
		{
			if (this.eventRidToTypeDefRid != null)
			{
				return;
			}
			uint[] array = new uint[this.tablesStream.EventTable.Rows];
			RidList typeDefRidList = this.GetTypeDefRidList();
			for (int i = 0; i < typeDefRidList.Count; i++)
			{
				uint num = typeDefRidList[i];
				RidList eventRidList = this.GetEventRidList(this.GetEventMapRid(num));
				for (int j = 0; j < eventRidList.Count; j++)
				{
					uint num2 = eventRidList[j] - 1U;
					if (array[(int)num2] == 0U)
					{
						array[(int)num2] = num;
					}
				}
			}
			Interlocked.CompareExchange<uint[]>(ref this.eventRidToTypeDefRid, array, null);
		}

		// Token: 0x06005EAE RID: 24238 RVA: 0x001C6224 File Offset: 0x001C6224
		public override uint GetOwnerTypeOfProperty(uint propertyRid)
		{
			if (this.propertyRidToTypeDefRid == null)
			{
				this.InitializeInversePropertyOwnerRidList();
			}
			uint num = propertyRid - 1U;
			if ((ulong)num >= (ulong)((long)this.propertyRidToTypeDefRid.Length))
			{
				return 0U;
			}
			return this.propertyRidToTypeDefRid[(int)num];
		}

		// Token: 0x06005EAF RID: 24239 RVA: 0x001C6264 File Offset: 0x001C6264
		private void InitializeInversePropertyOwnerRidList()
		{
			if (this.propertyRidToTypeDefRid != null)
			{
				return;
			}
			uint[] array = new uint[this.tablesStream.PropertyTable.Rows];
			RidList typeDefRidList = this.GetTypeDefRidList();
			for (int i = 0; i < typeDefRidList.Count; i++)
			{
				uint num = typeDefRidList[i];
				RidList propertyRidList = this.GetPropertyRidList(this.GetPropertyMapRid(num));
				for (int j = 0; j < propertyRidList.Count; j++)
				{
					uint num2 = propertyRidList[j] - 1U;
					if (array[(int)num2] == 0U)
					{
						array[(int)num2] = num;
					}
				}
			}
			Interlocked.CompareExchange<uint[]>(ref this.propertyRidToTypeDefRid, array, null);
		}

		// Token: 0x06005EB0 RID: 24240 RVA: 0x001C6310 File Offset: 0x001C6310
		public override uint GetOwnerOfGenericParam(uint gpRid)
		{
			if (this.gpRidToOwnerRid == null)
			{
				this.InitializeInverseGenericParamOwnerRidList();
			}
			uint num = gpRid - 1U;
			if ((ulong)num >= (ulong)((long)this.gpRidToOwnerRid.Length))
			{
				return 0U;
			}
			return this.gpRidToOwnerRid[(int)num];
		}

		// Token: 0x06005EB1 RID: 24241 RVA: 0x001C6350 File Offset: 0x001C6350
		private void InitializeInverseGenericParamOwnerRidList()
		{
			if (this.gpRidToOwnerRid != null)
			{
				return;
			}
			MDTable genericParamTable = this.tablesStream.GenericParamTable;
			uint[] array = new uint[genericParamTable.Rows];
			ColumnInfo column = genericParamTable.TableInfo.Columns[2];
			Dictionary<uint, bool> dictionary = new Dictionary<uint, bool>();
			for (uint num = 1U; num <= genericParamTable.Rows; num += 1U)
			{
				uint key;
				if (this.tablesStream.TryReadColumn24(genericParamTable, num, column, out key))
				{
					dictionary[key] = true;
				}
			}
			List<uint> list = new List<uint>(dictionary.Keys);
			list.Sort();
			for (int i = 0; i < list.Count; i++)
			{
				uint token;
				if (CodedToken.TypeOrMethodDef.Decode(list[i], out token))
				{
					RidList genericParamRidList = this.GetGenericParamRidList(MDToken.ToTable(token), MDToken.ToRID(token));
					for (int j = 0; j < genericParamRidList.Count; j++)
					{
						uint num2 = genericParamRidList[j] - 1U;
						if (array[(int)num2] == 0U)
						{
							array[(int)num2] = list[i];
						}
					}
				}
			}
			Interlocked.CompareExchange<uint[]>(ref this.gpRidToOwnerRid, array, null);
		}

		// Token: 0x06005EB2 RID: 24242 RVA: 0x001C647C File Offset: 0x001C647C
		public override uint GetOwnerOfGenericParamConstraint(uint gpcRid)
		{
			if (this.gpcRidToOwnerRid == null)
			{
				this.InitializeInverseGenericParamConstraintOwnerRidList();
			}
			uint num = gpcRid - 1U;
			if ((ulong)num >= (ulong)((long)this.gpcRidToOwnerRid.Length))
			{
				return 0U;
			}
			return this.gpcRidToOwnerRid[(int)num];
		}

		// Token: 0x06005EB3 RID: 24243 RVA: 0x001C64BC File Offset: 0x001C64BC
		private void InitializeInverseGenericParamConstraintOwnerRidList()
		{
			if (this.gpcRidToOwnerRid != null)
			{
				return;
			}
			MDTable genericParamConstraintTable = this.tablesStream.GenericParamConstraintTable;
			uint[] array = new uint[genericParamConstraintTable.Rows];
			ColumnInfo column = genericParamConstraintTable.TableInfo.Columns[0];
			Dictionary<uint, bool> dictionary = new Dictionary<uint, bool>();
			for (uint num = 1U; num <= genericParamConstraintTable.Rows; num += 1U)
			{
				uint key;
				if (this.tablesStream.TryReadColumn24(genericParamConstraintTable, num, column, out key))
				{
					dictionary[key] = true;
				}
			}
			List<uint> list = new List<uint>(dictionary.Keys);
			list.Sort();
			for (int i = 0; i < list.Count; i++)
			{
				uint num2 = list[i];
				RidList genericParamConstraintRidList = this.GetGenericParamConstraintRidList(num2);
				for (int j = 0; j < genericParamConstraintRidList.Count; j++)
				{
					uint num3 = genericParamConstraintRidList[j] - 1U;
					if (array[(int)num3] == 0U)
					{
						array[(int)num3] = num2;
					}
				}
			}
			Interlocked.CompareExchange<uint[]>(ref this.gpcRidToOwnerRid, array, null);
		}

		// Token: 0x06005EB4 RID: 24244 RVA: 0x001C65C8 File Offset: 0x001C65C8
		public override uint GetOwnerOfParam(uint paramRid)
		{
			if (this.paramRidToOwnerRid == null)
			{
				this.InitializeInverseParamOwnerRidList();
			}
			uint num = paramRid - 1U;
			if ((ulong)num >= (ulong)((long)this.paramRidToOwnerRid.Length))
			{
				return 0U;
			}
			return this.paramRidToOwnerRid[(int)num];
		}

		// Token: 0x06005EB5 RID: 24245 RVA: 0x001C6608 File Offset: 0x001C6608
		private void InitializeInverseParamOwnerRidList()
		{
			if (this.paramRidToOwnerRid != null)
			{
				return;
			}
			uint[] array = new uint[this.tablesStream.ParamTable.Rows];
			MDTable methodTable = this.tablesStream.MethodTable;
			for (uint num = 1U; num <= methodTable.Rows; num += 1U)
			{
				RidList paramRidList = this.GetParamRidList(num);
				for (int i = 0; i < paramRidList.Count; i++)
				{
					uint num2 = paramRidList[i] - 1U;
					if (array[(int)num2] == 0U)
					{
						array[(int)num2] = num;
					}
				}
			}
			Interlocked.CompareExchange<uint[]>(ref this.paramRidToOwnerRid, array, null);
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x001C66A8 File Offset: 0x001C66A8
		public override RidList GetNestedClassRidList(uint typeDefRid)
		{
			if (this.typeDefRidToNestedClasses == null)
			{
				this.InitializeNestedClassesDictionary();
			}
			List<uint> rids;
			if (this.typeDefRidToNestedClasses.TryGetValue(typeDefRid, out rids))
			{
				return RidList.Create(rids);
			}
			return RidList.Empty;
		}

		// Token: 0x06005EB7 RID: 24247 RVA: 0x001C66EC File Offset: 0x001C66EC
		private void InitializeNestedClassesDictionary()
		{
			MDTable nestedClassTable = this.tablesStream.NestedClassTable;
			MDTable typeDefTable = this.tablesStream.TypeDefTable;
			Dictionary<uint, bool> dictionary = null;
			RidList typeDefRidList = this.GetTypeDefRidList();
			if (typeDefRidList.Count != (int)typeDefTable.Rows)
			{
				dictionary = new Dictionary<uint, bool>(typeDefRidList.Count);
				for (int i = 0; i < typeDefRidList.Count; i++)
				{
					dictionary[typeDefRidList[i]] = true;
				}
			}
			Dictionary<uint, bool> dictionary2 = new Dictionary<uint, bool>((int)nestedClassTable.Rows);
			List<uint> list = new List<uint>((int)nestedClassTable.Rows);
			for (uint num = 1U; num <= nestedClassTable.Rows; num += 1U)
			{
				RawNestedClassRow rawNestedClassRow;
				if ((dictionary == null || dictionary.ContainsKey(num)) && this.tablesStream.TryReadNestedClassRow(num, out rawNestedClassRow) && typeDefTable.IsValidRID(rawNestedClassRow.NestedClass) && typeDefTable.IsValidRID(rawNestedClassRow.EnclosingClass) && !dictionary2.ContainsKey(rawNestedClassRow.NestedClass))
				{
					dictionary2[rawNestedClassRow.NestedClass] = true;
					list.Add(rawNestedClassRow.NestedClass);
				}
			}
			Dictionary<uint, List<uint>> dictionary3 = new Dictionary<uint, List<uint>>();
			int count = list.Count;
			for (int j = 0; j < count; j++)
			{
				uint num2 = list[j];
				RawNestedClassRow rawNestedClassRow2;
				if (this.tablesStream.TryReadNestedClassRow(this.GetNestedClassRid(num2), out rawNestedClassRow2))
				{
					List<uint> list2;
					if (!dictionary3.TryGetValue(rawNestedClassRow2.EnclosingClass, out list2))
					{
						list2 = (dictionary3[rawNestedClassRow2.EnclosingClass] = new List<uint>());
					}
					list2.Add(num2);
				}
			}
			List<uint> list3 = new List<uint>((int)((ulong)typeDefTable.Rows - (ulong)((long)dictionary2.Count)));
			for (uint num3 = 1U; num3 <= typeDefTable.Rows; num3 += 1U)
			{
				if ((dictionary == null || dictionary.ContainsKey(num3)) && !dictionary2.ContainsKey(num3))
				{
					list3.Add(num3);
				}
			}
			Interlocked.CompareExchange<StrongBox<RidList>>(ref this.nonNestedTypes, new StrongBox<RidList>(RidList.Create(list3)), null);
			Interlocked.CompareExchange<Dictionary<uint, List<uint>>>(ref this.typeDefRidToNestedClasses, dictionary3, null);
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x001C6918 File Offset: 0x001C6918
		public override RidList GetNonNestedClassRidList()
		{
			if (this.typeDefRidToNestedClasses == null)
			{
				this.InitializeNestedClassesDictionary();
			}
			return this.nonNestedTypes.Value;
		}

		// Token: 0x06005EB9 RID: 24249 RVA: 0x001C6938 File Offset: 0x001C6938
		public override RidList GetLocalScopeRidList(uint methodRid)
		{
			return this.FindAllRows(this.tablesStream.LocalScopeTable, 0, methodRid);
		}

		// Token: 0x06005EBA RID: 24250 RVA: 0x001C6950 File Offset: 0x001C6950
		public override uint GetStateMachineMethodRid(uint methodRid)
		{
			RidList ridList = this.FindAllRows(this.tablesStream.StateMachineMethodTable, 0, methodRid);
			if (ridList.Count != 0)
			{
				return ridList[0];
			}
			return 0U;
		}

		// Token: 0x06005EBB RID: 24251 RVA: 0x001C698C File Offset: 0x001C698C
		public override RidList GetCustomDebugInformationRidList(Table table, uint rid)
		{
			uint key;
			if (!CodedToken.HasCustomDebugInformation.Encode(new MDToken(table, rid), out key))
			{
				return RidList.Empty;
			}
			return this.FindAllRows(this.tablesStream.CustomDebugInformationTable, 0, key);
		}

		// Token: 0x06005EBC RID: 24252 RVA: 0x001C69D0 File Offset: 0x001C69D0
		public override void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005EBD RID: 24253 RVA: 0x001C69E0 File Offset: 0x001C69E0
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			IPEImage ipeimage = this.peImage;
			if (ipeimage != null)
			{
				ipeimage.Dispose();
			}
			StringsStream stringsStream = this.stringsStream;
			if (stringsStream != null)
			{
				stringsStream.Dispose();
			}
			USStream usstream = this.usStream;
			if (usstream != null)
			{
				usstream.Dispose();
			}
			BlobStream blobStream = this.blobStream;
			if (blobStream != null)
			{
				blobStream.Dispose();
			}
			GuidStream guidStream = this.guidStream;
			if (guidStream != null)
			{
				guidStream.Dispose();
			}
			TablesStream tablesStream = this.tablesStream;
			if (tablesStream != null)
			{
				tablesStream.Dispose();
			}
			IList<DotNetStream> list = this.allStreams;
			if (list != null)
			{
				foreach (DotNetStream dotNetStream in list)
				{
					if (dotNetStream != null)
					{
						dotNetStream.Dispose();
					}
				}
			}
			DataReaderFactory dataReaderFactory = this.mdReaderFactoryToDisposeLater;
			if (dataReaderFactory != null)
			{
				dataReaderFactory.Dispose();
			}
			this.peImage = null;
			this.cor20Header = null;
			this.mdHeader = null;
			this.stringsStream = null;
			this.usStream = null;
			this.blobStream = null;
			this.guidStream = null;
			this.tablesStream = null;
			this.allStreams = null;
			this.fieldRidToTypeDefRid = null;
			this.methodRidToTypeDefRid = null;
			this.typeDefRidToNestedClasses = null;
			this.mdReaderFactoryToDisposeLater = null;
		}

		// Token: 0x04002E30 RID: 11824
		protected IPEImage peImage;

		// Token: 0x04002E31 RID: 11825
		protected ImageCor20Header cor20Header;

		// Token: 0x04002E32 RID: 11826
		protected MetadataHeader mdHeader;

		// Token: 0x04002E33 RID: 11827
		protected StringsStream stringsStream;

		// Token: 0x04002E34 RID: 11828
		protected USStream usStream;

		// Token: 0x04002E35 RID: 11829
		protected BlobStream blobStream;

		// Token: 0x04002E36 RID: 11830
		protected GuidStream guidStream;

		// Token: 0x04002E37 RID: 11831
		protected TablesStream tablesStream;

		// Token: 0x04002E38 RID: 11832
		protected PdbStream pdbStream;

		// Token: 0x04002E39 RID: 11833
		protected IList<DotNetStream> allStreams;

		// Token: 0x04002E3A RID: 11834
		protected readonly bool isStandalonePortablePdb;

		// Token: 0x04002E3B RID: 11835
		private uint[] fieldRidToTypeDefRid;

		// Token: 0x04002E3C RID: 11836
		private uint[] methodRidToTypeDefRid;

		// Token: 0x04002E3D RID: 11837
		private uint[] eventRidToTypeDefRid;

		// Token: 0x04002E3E RID: 11838
		private uint[] propertyRidToTypeDefRid;

		// Token: 0x04002E3F RID: 11839
		private uint[] gpRidToOwnerRid;

		// Token: 0x04002E40 RID: 11840
		private uint[] gpcRidToOwnerRid;

		// Token: 0x04002E41 RID: 11841
		private uint[] paramRidToOwnerRid;

		// Token: 0x04002E42 RID: 11842
		private Dictionary<uint, List<uint>> typeDefRidToNestedClasses;

		// Token: 0x04002E43 RID: 11843
		private StrongBox<RidList> nonNestedTypes;

		// Token: 0x04002E44 RID: 11844
		private DataReaderFactory mdReaderFactoryToDisposeLater;

		// Token: 0x04002E45 RID: 11845
		private MetadataBase.SortedTable eventMapSortedTable;

		// Token: 0x04002E46 RID: 11846
		private MetadataBase.SortedTable propertyMapSortedTable;

		// Token: 0x0200103E RID: 4158
		protected sealed class SortedTable
		{
			// Token: 0x06008FC3 RID: 36803 RVA: 0x002AD460 File Offset: 0x002AD460
			public SortedTable(MDTable mdTable, int keyColIndex)
			{
				this.InitializeKeys(mdTable, keyColIndex);
				Array.Sort<MetadataBase.SortedTable.RowInfo>(this.rows);
			}

			// Token: 0x06008FC4 RID: 36804 RVA: 0x002AD47C File Offset: 0x002AD47C
			private void InitializeKeys(MDTable mdTable, int keyColIndex)
			{
				ColumnInfo columnInfo = mdTable.TableInfo.Columns[keyColIndex];
				this.rows = new MetadataBase.SortedTable.RowInfo[mdTable.Rows + 1U];
				if (mdTable.Rows == 0U)
				{
					return;
				}
				DataReader dataReader = mdTable.DataReader;
				dataReader.Position = (uint)columnInfo.Offset;
				uint num = (uint)(mdTable.TableInfo.RowSize - columnInfo.Size);
				for (uint num2 = 1U; num2 <= mdTable.Rows; num2 += 1U)
				{
					this.rows[(int)num2] = new MetadataBase.SortedTable.RowInfo(num2, columnInfo.Unsafe_Read24(ref dataReader));
					if (num2 < mdTable.Rows)
					{
						dataReader.Position += num;
					}
				}
			}

			// Token: 0x06008FC5 RID: 36805 RVA: 0x002AD530 File Offset: 0x002AD530
			private int BinarySearch(uint key)
			{
				int num = 1;
				int num2 = this.rows.Length - 1;
				while (num <= num2 && num2 != -1)
				{
					int num3 = (num + num2) / 2;
					uint key2 = this.rows[num3].key;
					if (key == key2)
					{
						return num3;
					}
					if (key2 > key)
					{
						num2 = num3 - 1;
					}
					else
					{
						num = num3 + 1;
					}
				}
				return 0;
			}

			// Token: 0x06008FC6 RID: 36806 RVA: 0x002AD594 File Offset: 0x002AD594
			public RidList FindAllRows(uint key)
			{
				int i = this.BinarySearch(key);
				if (i == 0)
				{
					return RidList.Empty;
				}
				int num = i + 1;
				while (i > 1)
				{
					if (key != this.rows[i - 1].key)
					{
						IL_5E:
						while (num < this.rows.Length && key == this.rows[num].key)
						{
							num++;
						}
						List<uint> list = new List<uint>(num - i);
						for (int j = i; j < num; j++)
						{
							list.Add(this.rows[j].rid);
						}
						return RidList.Create(list);
					}
					i--;
				}
				goto IL_5E;
			}

			// Token: 0x0400452B RID: 17707
			private MetadataBase.SortedTable.RowInfo[] rows;

			// Token: 0x02001213 RID: 4627
			[DebuggerDisplay("{rid} {key}")]
			private readonly struct RowInfo : IComparable<MetadataBase.SortedTable.RowInfo>
			{
				// Token: 0x060096A6 RID: 38566 RVA: 0x002CC7C0 File Offset: 0x002CC7C0
				public RowInfo(uint rid, uint key)
				{
					this.rid = rid;
					this.key = key;
				}

				// Token: 0x060096A7 RID: 38567 RVA: 0x002CC7D0 File Offset: 0x002CC7D0
				public int CompareTo(MetadataBase.SortedTable.RowInfo other)
				{
					if (this.key < other.key)
					{
						return -1;
					}
					if (this.key > other.key)
					{
						return 1;
					}
					return this.rid.CompareTo(other.rid);
				}

				// Token: 0x04004F1B RID: 20251
				public readonly uint rid;

				// Token: 0x04004F1C RID: 20252
				public readonly uint key;
			}
		}
	}
}
