using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BC0 RID: 3008
	[ComVisible(true)]
	public class METADATASTREAM_US : AbstractStructure, IMETADATASTREAM_US
	{
		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x0600795F RID: 31071 RVA: 0x0023ECA8 File Offset: 0x0023ECA8
		public List<string> UserStrings { get; }

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x06007960 RID: 31072 RVA: 0x0023ECB0 File Offset: 0x0023ECB0
		public List<Tuple<string, uint>> UserStringsAndIndices { get; }

		// Token: 0x06007961 RID: 31073 RVA: 0x0023ECB8 File Offset: 0x0023ECB8
		public METADATASTREAM_US(byte[] buff, uint offset, uint size) : base(buff, offset)
		{
			this._size = size;
			this.UserStringsAndIndices = this.ParseUserStringsAndIndices();
			this.UserStrings = (from x in this.UserStringsAndIndices
			select x.Item1).ToList<string>();
		}

		// Token: 0x06007962 RID: 31074 RVA: 0x0023ED1C File Offset: 0x0023ED1C
		public string GetUserStringAtIndex(uint index)
		{
			Tuple<string, uint> tuple = this.UserStringsAndIndices.FirstOrDefault((Tuple<string, uint> x) => x.Item2 == index);
			if (tuple == null)
			{
				return null;
			}
			return tuple.Item1;
		}

		// Token: 0x06007963 RID: 31075 RVA: 0x0023ED60 File Offset: 0x0023ED60
		private List<Tuple<string, uint>> ParseUserStringsAndIndices()
		{
			List<Tuple<string, uint>> list = new List<Tuple<string, uint>>();
			for (uint num = this.Offset + 1U; num < this.Offset + this._size; num += 1U)
			{
				if (this.Buff[(int)num] >= 128)
				{
					num += 1U;
				}
				int num2 = (int)this.Buff[(int)num];
				if (num2 == 0)
				{
					break;
				}
				num += 1U;
				string unicodeString = this.Buff.GetUnicodeString((ulong)num, num2 - 1);
				num += (uint)(num2 - 1);
				list.Add(new Tuple<string, uint>(unicodeString, num - (uint)num2 - this.Offset));
			}
			return list;
		}

		// Token: 0x04003A64 RID: 14948
		private uint _size;
	}
}
