using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using PeNet.Structures.MetaDataTables;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BC2 RID: 3010
	[ComVisible(true)]
	public class METADATATABLESHDR : AbstractStructure, IMETADATATABLESHDR
	{
		// Token: 0x06007967 RID: 31079 RVA: 0x0023EDF0 File Offset: 0x0023EDF0
		public METADATATABLESHDR(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x06007968 RID: 31080 RVA: 0x0023EDFC File Offset: 0x0023EDFC
		// (set) Token: 0x06007969 RID: 31081 RVA: 0x0023EE10 File Offset: 0x0023EE10
		public uint Reserved1
		{
			get
			{
				return this.Buff.BytesToUInt32(this.Offset);
			}
			set
			{
				this.Buff.SetUInt32(this.Offset, value);
			}
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x0600796A RID: 31082 RVA: 0x0023EE24 File Offset: 0x0023EE24
		// (set) Token: 0x0600796B RID: 31083 RVA: 0x0023EE38 File Offset: 0x0023EE38
		public byte MajorVersion
		{
			get
			{
				return this.Buff[(int)(this.Offset + 4U)];
			}
			set
			{
				this.Buff[(int)(this.Offset + 4U)] = value;
			}
		}

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x0600796C RID: 31084 RVA: 0x0023EE4C File Offset: 0x0023EE4C
		// (set) Token: 0x0600796D RID: 31085 RVA: 0x0023EE60 File Offset: 0x0023EE60
		public byte MinorVersion
		{
			get
			{
				return this.Buff[(int)(this.Offset + 5U)];
			}
			set
			{
				this.Buff[(int)(this.Offset + 5U)] = value;
			}
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x0600796E RID: 31086 RVA: 0x0023EE74 File Offset: 0x0023EE74
		// (set) Token: 0x0600796F RID: 31087 RVA: 0x0023EE88 File Offset: 0x0023EE88
		public byte HeapSizes
		{
			get
			{
				return this.Buff[(int)(this.Offset + 6U)];
			}
			set
			{
				this.Buff[(int)(this.Offset + 6U)] = value;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x06007970 RID: 31088 RVA: 0x0023EE9C File Offset: 0x0023EE9C
		// (set) Token: 0x06007971 RID: 31089 RVA: 0x0023EEB0 File Offset: 0x0023EEB0
		public byte Reserved2
		{
			get
			{
				return this.Buff[(int)(this.Offset + 7U)];
			}
			set
			{
				this.Buff[(int)(this.Offset + 7U)] = value;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x06007972 RID: 31090 RVA: 0x0023EEC4 File Offset: 0x0023EEC4
		// (set) Token: 0x06007973 RID: 31091 RVA: 0x0023EEDC File Offset: 0x0023EEDC
		public ulong Valid
		{
			get
			{
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 8U));
			}
			set
			{
				this.Buff.SetUInt64((ulong)(this.Offset + 8U), value);
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x06007974 RID: 31092 RVA: 0x0023EEF4 File Offset: 0x0023EEF4
		// (set) Token: 0x06007975 RID: 31093 RVA: 0x0023EF0C File Offset: 0x0023EF0C
		public ulong MaskSorted
		{
			get
			{
				return this.Buff.BytesToUInt64((ulong)(this.Offset + 16U));
			}
			set
			{
				this.Buff.SetUInt64((ulong)(this.Offset + 16U), value);
			}
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x06007976 RID: 31094 RVA: 0x0023EF24 File Offset: 0x0023EF24
		public List<METADATATABLESHDR.MetaDataTableInfo> TableDefinitions
		{
			get
			{
				if (this._tableDefinitions != null)
				{
					return this._tableDefinitions;
				}
				this._tableDefinitions = this.ParseTableDefinitions();
				return this._tableDefinitions;
			}
		}

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x06007977 RID: 31095 RVA: 0x0023EF4C File Offset: 0x0023EF4C
		public Tables Tables
		{
			get
			{
				if (this._tables == null)
				{
					this._tables = this.ParseMetaDataTables();
				}
				return this._tables;
			}
		}

		// Token: 0x06007978 RID: 31096 RVA: 0x0023EF6C File Offset: 0x0023EF6C
		private List<METADATATABLESHDR.MetaDataTableInfo> ParseTableDefinitions()
		{
			HeapSizes heapSizes = new HeapSizes(this.HeapSizes);
			METADATATABLESHDR.MetaDataTableInfo[] array = new METADATATABLESHDR.MetaDataTableInfo[64];
			uint num = this.Offset + 24U;
			List<string> list = FlagResolver.ResolveMaskValidFlags(this.Valid);
			int num2 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if ((this.Valid & 1UL << i) != 0UL)
				{
					array[i].RowCount = this.Buff.BytesToUInt32(num + (uint)(num2 * 4));
					array[i].Name = list[num2];
					num2++;
				}
			}
			IndexSize indexSize = new IndexSize(array);
			array[0].BytesPerRow = 2U + heapSizes.String + heapSizes.Guid * 3U;
			array[1].BytesPerRow = indexSize[Index.ResolutionScope] + heapSizes.String * 2U;
			array[2].BytesPerRow = 4U + heapSizes.String * 2U + indexSize[Index.TypeDefOrRef] + indexSize[Index.Field] + indexSize[Index.MethodDef];
			array[4].BytesPerRow = 2U + heapSizes.String + heapSizes.Blob;
			array[6].BytesPerRow = 8U + heapSizes.String + heapSizes.Blob + this.GetIndexSize(MetadataToken.Parameter, array);
			array[8].BytesPerRow = 4U + heapSizes.String;
			array[9].BytesPerRow = this.GetIndexSize(MetadataToken.TypeDef, array) + indexSize[Index.TypeDefOrRef];
			array[10].BytesPerRow = indexSize[Index.MemberRefParent] + heapSizes.String + heapSizes.Blob;
			array[11].BytesPerRow = 2U + indexSize[Index.HasConstant] + heapSizes.Blob;
			array[12].BytesPerRow = indexSize[Index.HasCustomAttribute] + indexSize[Index.CustomAttributeType] + heapSizes.Blob;
			array[13].BytesPerRow = indexSize[Index.HasFieldMarshal] + heapSizes.Blob;
			array[14].BytesPerRow = 2U + indexSize[Index.HasDeclSecurity] + heapSizes.Blob;
			array[15].BytesPerRow = 6U + this.GetIndexSize(MetadataToken.TypeDef, array);
			array[16].BytesPerRow = 4U + this.GetIndexSize(MetadataToken.Field, array);
			array[17].BytesPerRow = heapSizes.Blob;
			array[18].BytesPerRow = this.GetIndexSize(MetadataToken.TypeDef, array) + this.GetIndexSize(MetadataToken.Event, array);
			array[20].BytesPerRow = 2U + heapSizes.String + indexSize[Index.TypeDefOrRef];
			array[21].BytesPerRow = this.GetIndexSize(MetadataToken.TypeDef, array) + this.GetIndexSize(MetadataToken.Property, array);
			array[23].BytesPerRow = 2U + heapSizes.String + heapSizes.Blob;
			array[24].BytesPerRow = 2U + this.GetIndexSize(MetadataToken.MethodDef, array) + indexSize[Index.HasSemantics];
			array[25].BytesPerRow = this.GetIndexSize(MetadataToken.TypeDef, array) + indexSize[Index.MethodDefOrRef] * 2U;
			array[26].BytesPerRow = heapSizes.String;
			array[27].BytesPerRow = heapSizes.Blob;
			array[28].BytesPerRow = 2U + indexSize[Index.MemberForwarded] + heapSizes.String + this.GetIndexSize(MetadataToken.ModuleReference, array);
			array[29].BytesPerRow = 4U + this.GetIndexSize(MetadataToken.Field, array);
			array[32].BytesPerRow = 16U + heapSizes.Blob + heapSizes.String * 2U;
			array[33].BytesPerRow = 4U;
			array[34].BytesPerRow = 12U;
			array[35].BytesPerRow = 12U + heapSizes.Blob * 2U + heapSizes.String * 2U;
			array[36].BytesPerRow = 4U + this.GetIndexSize(MetadataToken.AssemblyReference, array);
			array[37].BytesPerRow = 12U + this.GetIndexSize(MetadataToken.AssemblyReference, array);
			array[38].BytesPerRow = 4U + heapSizes.String + heapSizes.Blob;
			array[39].BytesPerRow = 8U + heapSizes.String * 2U + indexSize[Index.Implementation];
			array[40].BytesPerRow = 8U + heapSizes.String + indexSize[Index.Implementation];
			array[41].BytesPerRow = this.GetIndexSize(MetadataToken.NestedClass, array) * 2U;
			array[42].BytesPerRow = 4U + indexSize[Index.TypeOrMethodDef] + heapSizes.String;
			array[43].BytesPerRow = indexSize[Index.MethodDefOrRef] + heapSizes.Blob;
			array[44].BytesPerRow = this.GetIndexSize(MetadataToken.GenericParameter, array) + indexSize[Index.TypeDefOrRef];
			uint num3 = 0U;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].Offset = num3;
				num3 += array[j].BytesPerRow * array[j].RowCount;
			}
			return array.ToList<METADATATABLESHDR.MetaDataTableInfo>();
		}

		// Token: 0x06007979 RID: 31097 RVA: 0x0023F4A8 File Offset: 0x0023F4A8
		private Tables ParseMetaDataTables()
		{
			return new Tables
			{
				Module = this.ParseTable<Module>(MetadataToken.Module),
				TypeRef = this.ParseTable<TypeRef>(MetadataToken.TypeReference),
				TypeDef = this.ParseTable<TypeDef>(MetadataToken.TypeDef),
				Field = this.ParseTable<Field>(MetadataToken.Field),
				MethodDef = this.ParseTable<MethodDef>(MetadataToken.MethodDef),
				Param = this.ParseTable<Param>(MetadataToken.Parameter),
				InterfaceImpl = this.ParseTable<InterfaceImpl>(MetadataToken.InterfaceImplementation),
				MemberRef = this.ParseTable<MemberRef>(MetadataToken.MemberReference),
				Constant = this.ParseTable<Constant>(MetadataToken.Constant),
				CustomAttribute = this.ParseTable<CustomAttribute>(MetadataToken.CustomAttribute),
				FieldMarshal = this.ParseTable<FieldMarshal>(MetadataToken.FieldMarshal),
				DeclSecurity = this.ParseTable<DeclSecurity>(MetadataToken.DeclarativeSecurity),
				ClassLayout = this.ParseTable<ClassLayout>(MetadataToken.ClassLayout),
				FieldLayout = this.ParseTable<FieldLayout>(MetadataToken.FieldLayout),
				StandAloneSig = this.ParseTable<StandAloneSig>(MetadataToken.StandAloneSignature),
				EventMap = this.ParseTable<EventMap>(MetadataToken.EventMap),
				Event = this.ParseTable<Event>(MetadataToken.Event),
				PropertyMap = this.ParseTable<PropertyMap>(MetadataToken.PropertyMap),
				Property = this.ParseTable<Property>(MetadataToken.Property),
				MethodSemantic = this.ParseTable<MethodSemantics>(MetadataToken.MethodSemantics),
				MethodImpl = this.ParseTable<MethodImpl>(MetadataToken.MethodImplementation),
				ModuleRef = this.ParseTable<ModuleRef>(MetadataToken.ModuleReference),
				TypeSpec = this.ParseTable<TypeSpec>(MetadataToken.TypeSpecification),
				ImplMap = this.ParseTable<ImplMap>(MetadataToken.ImplementationMap),
				FieldRVA = this.ParseTable<FieldRVA>(MetadataToken.FieldRVA),
				Assembly = this.ParseTable<Assembly>(MetadataToken.Assembly),
				AssemblyProcessor = this.ParseTable<AssemblyProcessor>(MetadataToken.AssemblyProcessor),
				AssemblyOS = this.ParseTable<AssemblyOS>(MetadataToken.AssemblyOS),
				AssemblyRef = this.ParseTable<AssemblyRef>(MetadataToken.AssemblyReference),
				AssemblyRefProcessor = this.ParseTable<AssemblyRefProcessor>(MetadataToken.AssemblyReferenceProcessor),
				AssemblyRefOS = this.ParseTable<AssemblyRefOS>(MetadataToken.AssemblyReferenceOS),
				File = this.ParseTable<File>(MetadataToken.File),
				ExportedType = this.ParseTable<ExportedType>(MetadataToken.ExportedType),
				ManifestResource = this.ParseTable<ManifestResource>(MetadataToken.ManifestResource),
				NestedClass = this.ParseTable<NestedClass>(MetadataToken.NestedClass),
				GenericParam = this.ParseTable<GenericParam>(MetadataToken.GenericParameter),
				GenericParamConstraints = this.ParseTable<GenericParamConstraint>(MetadataToken.GenericParameterConstraint)
			};
		}

		// Token: 0x0600797A RID: 31098 RVA: 0x0023F6C0 File Offset: 0x0023F6C0
		private List<T> ParseTable<T>(MetadataToken token) where T : AbstractTable
		{
			HeapSizes heapSizes = new HeapSizes(this.HeapSizes);
			IndexSize indexSize = new IndexSize(this.TableDefinitions.ToArray());
			uint num = (uint)((ulong)(this.Offset + 24U) + (ulong)((long)this.HammingWeight(this.Valid) * 4L));
			METADATATABLESHDR.MetaDataTableInfo metaDataTableInfo = this.TableDefinitions[(int)token];
			List<T> list = new List<T>();
			if (metaDataTableInfo.RowCount != 0U)
			{
				for (uint num2 = 0U; num2 < metaDataTableInfo.RowCount; num2 += 1U)
				{
					list.Add((T)((object)Activator.CreateInstance(typeof(T), new object[]
					{
						this.Buff,
						num + metaDataTableInfo.Offset + metaDataTableInfo.BytesPerRow * num2,
						heapSizes,
						indexSize
					})));
				}
			}
			if (list.Count != 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x0600797B RID: 31099 RVA: 0x0023F7A4 File Offset: 0x0023F7A4
		private int HammingWeight(ulong value)
		{
			int num = 0;
			while (value != 0UL)
			{
				num++;
				value &= value - 1UL;
			}
			return num;
		}

		// Token: 0x0600797C RID: 31100 RVA: 0x0023F7CC File Offset: 0x0023F7CC
		private uint GetIndexSize(MetadataToken table, METADATATABLESHDR.MetaDataTableInfo[] tables)
		{
			if (tables[(int)table].RowCount > 65535U)
			{
				return 4U;
			}
			return 2U;
		}

		// Token: 0x04003A67 RID: 14951
		private List<METADATATABLESHDR.MetaDataTableInfo> _tableDefinitions;

		// Token: 0x04003A68 RID: 14952
		private Tables _tables;

		// Token: 0x02001163 RID: 4451
		public struct MetaDataTableInfo
		{
			// Token: 0x17001E6E RID: 7790
			// (get) Token: 0x0600930E RID: 37646 RVA: 0x002C2164 File Offset: 0x002C2164
			// (set) Token: 0x0600930F RID: 37647 RVA: 0x002C216C File Offset: 0x002C216C
			public uint RowCount { get; set; }

			// Token: 0x17001E6F RID: 7791
			// (get) Token: 0x06009310 RID: 37648 RVA: 0x002C2178 File Offset: 0x002C2178
			// (set) Token: 0x06009311 RID: 37649 RVA: 0x002C2180 File Offset: 0x002C2180
			public string Name { get; set; }

			// Token: 0x17001E70 RID: 7792
			// (get) Token: 0x06009312 RID: 37650 RVA: 0x002C218C File Offset: 0x002C218C
			// (set) Token: 0x06009313 RID: 37651 RVA: 0x002C2194 File Offset: 0x002C2194
			public uint Offset { get; set; }

			// Token: 0x17001E71 RID: 7793
			// (get) Token: 0x06009314 RID: 37652 RVA: 0x002C21A0 File Offset: 0x002C21A0
			// (set) Token: 0x06009315 RID: 37653 RVA: 0x002C21A8 File Offset: 0x002C21A8
			public uint BytesPerRow { get; set; }
		}
	}
}
