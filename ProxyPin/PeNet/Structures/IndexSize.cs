using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PeNet.Structures
{
	// Token: 0x02000B9C RID: 2972
	[ComVisible(true)]
	public class IndexSize
	{
		// Token: 0x0600779F RID: 30623 RVA: 0x0023B0B4 File Offset: 0x0023B0B4
		public IndexSize(METADATATABLESHDR.MetaDataTableInfo[] tables)
		{
			this._index = new Dictionary<Index, IMetaDataIndex>
			{
				{
					Index.MethodDef,
					new SingleIndex(MetadataToken.MethodDef, tables)
				},
				{
					Index.Field,
					new SingleIndex(MetadataToken.Field, tables)
				},
				{
					Index.Param,
					new SingleIndex(MetadataToken.Parameter, tables)
				},
				{
					Index.TypeDef,
					new SingleIndex(MetadataToken.TypeDef, tables)
				},
				{
					Index.Event,
					new SingleIndex(MetadataToken.Event, tables)
				},
				{
					Index.Property,
					new SingleIndex(MetadataToken.Property, tables)
				},
				{
					Index.ModuleRef,
					new SingleIndex(MetadataToken.ModuleReference, tables)
				},
				{
					Index.AssemblyRef,
					new SingleIndex(MetadataToken.AssemblyReference, tables)
				},
				{
					Index.GenericParam,
					new SingleIndex(MetadataToken.GenericParameter, tables)
				},
				{
					Index.TypeDefOrRef,
					new CodedIndex(tables, new byte[]
					{
						2,
						1,
						27
					})
				},
				{
					Index.HasConstant,
					new CodedIndex(tables, new byte[]
					{
						4,
						8,
						23
					})
				},
				{
					Index.HasCustomAttribute,
					new CodedIndex(tables, new byte[]
					{
						6,
						4,
						1,
						2,
						8,
						9,
						10,
						0,
						14,
						23,
						20,
						17,
						26,
						27,
						32,
						35,
						4,
						39,
						40
					})
				},
				{
					Index.HasFieldMarshal,
					new CodedIndex(tables, new byte[]
					{
						4,
						8
					})
				},
				{
					Index.HasDeclSecurity,
					new CodedIndex(tables, new byte[]
					{
						2,
						6,
						32
					})
				},
				{
					Index.MemberRefParent,
					new CodedIndex(tables, new byte[]
					{
						2,
						1,
						26,
						6,
						27
					})
				},
				{
					Index.HasSemantics,
					new CodedIndex(tables, new byte[]
					{
						20,
						23
					})
				},
				{
					Index.MethodDefOrRef,
					new CodedIndex(tables, new byte[]
					{
						6,
						10
					})
				},
				{
					Index.MemberForwarded,
					new CodedIndex(tables, new byte[]
					{
						4,
						6
					})
				},
				{
					Index.Implementation,
					new CodedIndex(tables, new byte[]
					{
						4,
						35,
						39
					})
				},
				{
					Index.CustomAttributeType,
					new CodedIndex(tables, new byte[]
					{
						byte.MaxValue,
						byte.MaxValue,
						6,
						10,
						byte.MaxValue
					})
				},
				{
					Index.ResolutionScope,
					new CodedIndex(tables, new byte[]
					{
						0,
						26,
						35,
						1
					})
				},
				{
					Index.TypeOrMethodDef,
					new CodedIndex(tables, new byte[]
					{
						2,
						6
					})
				}
			};
		}

		// Token: 0x17001922 RID: 6434
		public uint this[Index index]
		{
			get
			{
				return this._index[index].Size;
			}
		}

		// Token: 0x04003A38 RID: 14904
		private const byte unused = 255;

		// Token: 0x04003A39 RID: 14905
		private Dictionary<Index, IMetaDataIndex> _index;
	}
}
