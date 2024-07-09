using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.MD
{
	// Token: 0x02000999 RID: 2457
	[ComVisible(true)]
	public sealed class PdbStream : HeapStream
	{
		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06005ED8 RID: 24280 RVA: 0x001C7330 File Offset: 0x001C7330
		// (set) Token: 0x06005ED9 RID: 24281 RVA: 0x001C7338 File Offset: 0x001C7338
		public byte[] Id { get; private set; }

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06005EDA RID: 24282 RVA: 0x001C7344 File Offset: 0x001C7344
		// (set) Token: 0x06005EDB RID: 24283 RVA: 0x001C734C File Offset: 0x001C734C
		public MDToken EntryPoint { get; private set; }

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x001C7358 File Offset: 0x001C7358
		// (set) Token: 0x06005EDD RID: 24285 RVA: 0x001C7360 File Offset: 0x001C7360
		public ulong ReferencedTypeSystemTables { get; private set; }

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06005EDE RID: 24286 RVA: 0x001C736C File Offset: 0x001C736C
		// (set) Token: 0x06005EDF RID: 24287 RVA: 0x001C7374 File Offset: 0x001C7374
		public uint[] TypeSystemTableRows { get; private set; }

		// Token: 0x06005EE0 RID: 24288 RVA: 0x001C7380 File Offset: 0x001C7380
		public PdbStream(DataReaderFactory mdReaderFactory, uint metadataBaseOffset, StreamHeader streamHeader) : base(mdReaderFactory, metadataBaseOffset, streamHeader)
		{
			DataReader dataReader = base.CreateReader();
			this.Id = dataReader.ReadBytes(20);
			this.EntryPoint = new MDToken(dataReader.ReadUInt32());
			ulong num = dataReader.ReadUInt64();
			this.ReferencedTypeSystemTables = num;
			uint[] array = new uint[64];
			int i = 0;
			while (i < array.Length)
			{
				if (((uint)num & 1U) != 0U)
				{
					array[i] = dataReader.ReadUInt32();
				}
				i++;
				num >>= 1;
			}
			this.TypeSystemTableRows = array;
		}
	}
}
