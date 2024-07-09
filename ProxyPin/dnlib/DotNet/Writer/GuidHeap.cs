using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Writer
{
	// Token: 0x0200089C RID: 2204
	[ComVisible(true)]
	public sealed class GuidHeap : HeapBase, IOffsetHeap<Guid>
	{
		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06005460 RID: 21600 RVA: 0x0019B920 File Offset: 0x0019B920
		public override string Name
		{
			get
			{
				return "#GUID";
			}
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x0019B928 File Offset: 0x0019B928
		public uint Add(Guid? guid)
		{
			if (this.isReadOnly)
			{
				throw new ModuleWriterException("Trying to modify #GUID when it's read-only");
			}
			if (guid == null)
			{
				return 0U;
			}
			uint num;
			if (this.guids.TryGetValue(guid.Value, out num))
			{
				return num;
			}
			num = (uint)(this.guids.Count + 1);
			this.guids.Add(guid.Value, num);
			return num;
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x0019B99C File Offset: 0x0019B99C
		public override uint GetRawLength()
		{
			return (uint)(this.guids.Count * 16);
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x0019B9AC File Offset: 0x0019B9AC
		protected override void WriteToImpl(DataWriter writer)
		{
			uint num = 0U;
			foreach (KeyValuePair<Guid, uint> keyValuePair in this.guids)
			{
				byte[] source;
				if (this.userRawData == null || !this.userRawData.TryGetValue(num, out source))
				{
					source = keyValuePair.Key.ToByteArray();
				}
				writer.WriteBytes(source);
				num += 16U;
			}
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x0019BA3C File Offset: 0x0019BA3C
		public int GetRawDataSize(Guid data)
		{
			return 16;
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x0019BA40 File Offset: 0x0019BA40
		public void SetRawData(uint offset, byte[] rawData)
		{
			if (rawData == null || rawData.Length != 16)
			{
				throw new ArgumentException("Invalid size of GUID raw data");
			}
			if (this.userRawData == null)
			{
				this.userRawData = new Dictionary<uint, byte[]>();
			}
			this.userRawData[offset] = rawData;
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x0019BA80 File Offset: 0x0019BA80
		public IEnumerable<KeyValuePair<uint, byte[]>> GetAllRawData()
		{
			uint offset = 0U;
			foreach (KeyValuePair<Guid, uint> keyValuePair in this.guids)
			{
				yield return new KeyValuePair<uint, byte[]>(offset, keyValuePair.Key.ToByteArray());
				offset += 16U;
			}
			Dictionary<Guid, uint>.Enumerator enumerator = default(Dictionary<Guid, uint>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400287C RID: 10364
		private readonly Dictionary<Guid, uint> guids = new Dictionary<Guid, uint>();

		// Token: 0x0400287D RID: 10365
		private Dictionary<uint, byte[]> userRawData;
	}
}
