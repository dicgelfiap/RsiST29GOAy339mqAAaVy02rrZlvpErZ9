using System;

namespace PeNet.Structures
{
	// Token: 0x02000B9D RID: 2973
	internal class SingleIndex : IMetaDataIndex
	{
		// Token: 0x060077A1 RID: 30625 RVA: 0x0023B2FC File Offset: 0x0023B2FC
		public SingleIndex(MetadataToken token, METADATATABLESHDR.MetaDataTableInfo[] tables)
		{
			this._token = token;
			this._tables = tables;
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x060077A2 RID: 30626 RVA: 0x0023B314 File Offset: 0x0023B314
		public uint Size
		{
			get
			{
				if (this._tables[(int)this._token].RowCount > 65535U)
				{
					return 4U;
				}
				return 2U;
			}
		}

		// Token: 0x04003A3A RID: 14906
		private MetadataToken _token;

		// Token: 0x04003A3B RID: 14907
		private METADATATABLESHDR.MetaDataTableInfo[] _tables;
	}
}
