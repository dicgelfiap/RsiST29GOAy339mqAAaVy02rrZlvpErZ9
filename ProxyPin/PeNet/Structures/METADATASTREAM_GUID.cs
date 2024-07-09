using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace PeNet.Structures
{
	// Token: 0x02000BBC RID: 3004
	[ComVisible(true)]
	public class METADATASTREAM_GUID : AbstractStructure, IMETADATASTREAM_GUID
	{
		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x0600794D RID: 31053 RVA: 0x0023EA10 File Offset: 0x0023EA10
		public List<Guid> Guids { get; }

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x0600794E RID: 31054 RVA: 0x0023EA18 File Offset: 0x0023EA18
		public List<Tuple<Guid, uint>> GuidsAndIndices { get; }

		// Token: 0x0600794F RID: 31055 RVA: 0x0023EA20 File Offset: 0x0023EA20
		public METADATASTREAM_GUID(byte[] buff, uint offset, uint size) : base(buff, offset)
		{
			this._size = size;
			this.GuidsAndIndices = this.ParseGuidsAndIndices();
			this.Guids = (from x in this.GuidsAndIndices
			select x.Item1).ToList<Guid>();
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x0023EA84 File Offset: 0x0023EA84
		public Guid? GetGuidAtIndex(uint index)
		{
			Tuple<Guid, uint> tuple = this.GuidsAndIndices.FirstOrDefault((Tuple<Guid, uint> x) => x.Item2 == index);
			if (tuple == null)
			{
				return null;
			}
			return new Guid?(tuple.Item1);
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x0023EAD4 File Offset: 0x0023EAD4
		private List<Tuple<Guid, uint>> ParseGuidsAndIndices()
		{
			List<Tuple<Guid, uint>> list = new List<Tuple<Guid, uint>>((int)(this._size / 16U));
			for (uint num = this.Offset; num < this.Offset + this._size; num += 16U)
			{
				byte[] array = new byte[16];
				Array.Copy(this.Buff, (long)((ulong)num), array, 0L, 16L);
				list.Add(new Tuple<Guid, uint>(new Guid(array), (uint)(list.Count + 1)));
			}
			return list;
		}

		// Token: 0x04003A5E RID: 14942
		private readonly uint _size;
	}
}
