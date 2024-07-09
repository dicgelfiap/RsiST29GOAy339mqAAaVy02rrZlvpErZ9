using System;

namespace PeNet.Structures
{
	// Token: 0x02000B9E RID: 2974
	internal class CodedIndex : IMetaDataIndex
	{
		// Token: 0x060077A3 RID: 30627 RVA: 0x0023B33C File Offset: 0x0023B33C
		public CodedIndex(METADATATABLESHDR.MetaDataTableInfo[] tables, params byte[] tokens)
		{
			this._tokens = tokens;
			this._tables = tables;
			this._tagBitCount = (int)Math.Ceiling(Math.Log((double)tokens.Length, 2.0));
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x060077A4 RID: 30628 RVA: 0x0023B370 File Offset: 0x0023B370
		public uint Size
		{
			get
			{
				uint num = 0U;
				for (int i = 0; i < this._tokens.Length; i++)
				{
					MetadataToken? table = this.GetTable(i);
					if (table != null)
					{
						uint rowCount = this._tables[(int)table.Value].RowCount;
						if (rowCount > num)
						{
							num = rowCount;
						}
					}
				}
				int num2 = 16 - this._tagBitCount;
				if (num >= 1U << num2)
				{
					return 4U;
				}
				return 2U;
			}
		}

		// Token: 0x060077A5 RID: 30629 RVA: 0x0023B3EC File Offset: 0x0023B3EC
		private MetadataToken? GetTable(int tag)
		{
			byte b = this._tokens[tag];
			if (b != 255)
			{
				return new MetadataToken?((MetadataToken)b);
			}
			return null;
		}

		// Token: 0x04003A3C RID: 14908
		private const byte unused = 255;

		// Token: 0x04003A3D RID: 14909
		private readonly byte[] _tokens;

		// Token: 0x04003A3E RID: 14910
		private readonly METADATATABLESHDR.MetaDataTableInfo[] _tables;

		// Token: 0x04003A3F RID: 14911
		private readonly int _tagBitCount;
	}
}
