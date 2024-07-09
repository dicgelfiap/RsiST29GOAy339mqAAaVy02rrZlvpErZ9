using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet.Writer
{
	// Token: 0x020008CD RID: 2253
	[ComVisible(true)]
	public sealed class PdbHeap : HeapBase
	{
		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06005786 RID: 22406 RVA: 0x001AC7DC File Offset: 0x001AC7DC
		public override string Name
		{
			get
			{
				return "#Pdb";
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06005787 RID: 22407 RVA: 0x001AC7E4 File Offset: 0x001AC7E4
		public byte[] PdbId
		{
			get
			{
				return this.pdbId;
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06005788 RID: 22408 RVA: 0x001AC7EC File Offset: 0x001AC7EC
		// (set) Token: 0x06005789 RID: 22409 RVA: 0x001AC7F4 File Offset: 0x001AC7F4
		public uint EntryPoint
		{
			get
			{
				return this.entryPoint;
			}
			set
			{
				this.entryPoint = value;
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x0600578A RID: 22410 RVA: 0x001AC800 File Offset: 0x001AC800
		public FileOffset PdbIdOffset
		{
			get
			{
				return base.FileOffset;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x0600578B RID: 22411 RVA: 0x001AC808 File Offset: 0x001AC808
		// (set) Token: 0x0600578C RID: 22412 RVA: 0x001AC828 File Offset: 0x001AC828
		public ulong ReferencedTypeSystemTables
		{
			get
			{
				if (!this.referencedTypeSystemTablesInitd)
				{
					throw new InvalidOperationException("ReferencedTypeSystemTables hasn't been initialized yet");
				}
				return this.referencedTypeSystemTables;
			}
			set
			{
				if (this.isReadOnly)
				{
					throw new InvalidOperationException("Size has already been calculated, can't write a new value");
				}
				this.referencedTypeSystemTables = value;
				this.referencedTypeSystemTablesInitd = true;
				this.typeSystemTablesCount = 0;
				for (ulong num = value; num != 0UL; num >>= 1)
				{
					if (((int)num & 1) != 0)
					{
						this.typeSystemTablesCount++;
					}
				}
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x0600578D RID: 22413 RVA: 0x001AC88C File Offset: 0x001AC88C
		public uint[] TypeSystemTableRows
		{
			get
			{
				return this.typeSystemTableRows;
			}
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x001AC894 File Offset: 0x001AC894
		public PdbHeap()
		{
			this.pdbId = new byte[20];
			this.typeSystemTableRows = new uint[64];
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x001AC8B8 File Offset: 0x001AC8B8
		public override uint GetRawLength()
		{
			if (!this.referencedTypeSystemTablesInitd)
			{
				throw new InvalidOperationException("ReferencedTypeSystemTables hasn't been initialized yet");
			}
			return (uint)(this.pdbId.Length + 4 + 8 + 4 * this.typeSystemTablesCount);
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x001AC8E8 File Offset: 0x001AC8E8
		protected override void WriteToImpl(DataWriter writer)
		{
			if (!this.referencedTypeSystemTablesInitd)
			{
				throw new InvalidOperationException("ReferencedTypeSystemTables hasn't been initialized yet");
			}
			writer.WriteBytes(this.pdbId);
			writer.WriteUInt32(this.entryPoint);
			writer.WriteUInt64(this.referencedTypeSystemTables);
			ulong num = this.referencedTypeSystemTables;
			int i = 0;
			while (i < this.typeSystemTableRows.Length)
			{
				if (((int)num & 1) != 0)
				{
					writer.WriteUInt32(this.typeSystemTableRows[i]);
				}
				i++;
				num >>= 1;
			}
		}

		// Token: 0x04002A00 RID: 10752
		private readonly byte[] pdbId;

		// Token: 0x04002A01 RID: 10753
		private uint entryPoint;

		// Token: 0x04002A02 RID: 10754
		private ulong referencedTypeSystemTables;

		// Token: 0x04002A03 RID: 10755
		private bool referencedTypeSystemTablesInitd;

		// Token: 0x04002A04 RID: 10756
		private int typeSystemTablesCount;

		// Token: 0x04002A05 RID: 10757
		private readonly uint[] typeSystemTableRows;
	}
}
