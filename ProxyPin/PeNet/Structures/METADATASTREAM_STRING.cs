using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BBE RID: 3006
	[ComVisible(true)]
	public class METADATASTREAM_STRING : AbstractStructure, IMETADATASTREAM_STRING
	{
		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x06007956 RID: 31062 RVA: 0x0023EB4C File Offset: 0x0023EB4C
		public List<string> Strings { get; }

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x06007957 RID: 31063 RVA: 0x0023EB54 File Offset: 0x0023EB54
		public List<Tuple<string, uint>> StringsAndIndices { get; }

		// Token: 0x06007958 RID: 31064 RVA: 0x0023EB5C File Offset: 0x0023EB5C
		public METADATASTREAM_STRING(byte[] buff, uint offset, uint size) : base(buff, offset)
		{
			this._size = size;
			this.StringsAndIndices = this.ParseStringsAndIndices();
			this.Strings = (from x in this.StringsAndIndices
			select x.Item1).ToList<string>();
		}

		// Token: 0x06007959 RID: 31065 RVA: 0x0023EBC0 File Offset: 0x0023EBC0
		public string GetStringAtIndex(uint index)
		{
			Tuple<string, uint> tuple = this.StringsAndIndices.FirstOrDefault((Tuple<string, uint> x) => x.Item2 == index);
			string text = (tuple != null) ? tuple.Item1 : null;
			if (text != null)
			{
				return text;
			}
			Tuple<string, uint> tuple2 = this.StringsAndIndices.FirstOrDefault((Tuple<string, uint> x) => x.Item2 == index - 1U);
			if (tuple2 == null)
			{
				return null;
			}
			return tuple2.Item1;
		}

		// Token: 0x0600795A RID: 31066 RVA: 0x0023EC38 File Offset: 0x0023EC38
		private List<Tuple<string, uint>> ParseStringsAndIndices()
		{
			List<Tuple<string, uint>> list = new List<Tuple<string, uint>>();
			for (uint num = this.Offset; num < this.Offset + this._size; num += 1U)
			{
				uint item = num - this.Offset;
				string cstring = this.Buff.GetCString((ulong)num);
				num += (uint)cstring.Length;
				if (!string.IsNullOrWhiteSpace(cstring))
				{
					list.Add(new Tuple<string, uint>(cstring, item));
				}
			}
			return list;
		}

		// Token: 0x04003A61 RID: 14945
		private readonly uint _size;
	}
}
