using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.W32Resources
{
	// Token: 0x02000730 RID: 1840
	[ComVisible(true)]
	public sealed class ResourceData : ResourceDirectoryEntry
	{
		// Token: 0x06004096 RID: 16534 RVA: 0x00161444 File Offset: 0x00161444
		public DataReader CreateReader()
		{
			return this.dataReaderFactory.CreateReader(this.resourceStartOffset, this.resourceLength);
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x00161460 File Offset: 0x00161460
		// (set) Token: 0x06004098 RID: 16536 RVA: 0x00161468 File Offset: 0x00161468
		public uint CodePage
		{
			get
			{
				return this.codePage;
			}
			set
			{
				this.codePage = value;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x00161474 File Offset: 0x00161474
		// (set) Token: 0x0600409A RID: 16538 RVA: 0x0016147C File Offset: 0x0016147C
		public uint Reserved
		{
			get
			{
				return this.reserved;
			}
			set
			{
				this.reserved = value;
			}
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x00161488 File Offset: 0x00161488
		public ResourceData(ResourceName name) : this(name, ByteArrayDataReaderFactory.Create(Array2.Empty<byte>(), null), 0U, 0U)
		{
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x001614A0 File Offset: 0x001614A0
		public ResourceData(ResourceName name, DataReaderFactory dataReaderFactory, uint offset, uint length) : this(name, dataReaderFactory, offset, length, 0U, 0U)
		{
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x001614B0 File Offset: 0x001614B0
		public ResourceData(ResourceName name, DataReaderFactory dataReaderFactory, uint offset, uint length, uint codePage, uint reserved) : base(name)
		{
			if (dataReaderFactory == null)
			{
				throw new ArgumentNullException("dataReaderFactory");
			}
			this.dataReaderFactory = dataReaderFactory;
			this.resourceStartOffset = offset;
			this.resourceLength = length;
			this.codePage = codePage;
			this.reserved = reserved;
		}

		// Token: 0x04002269 RID: 8809
		private readonly DataReaderFactory dataReaderFactory;

		// Token: 0x0400226A RID: 8810
		private readonly uint resourceStartOffset;

		// Token: 0x0400226B RID: 8811
		private readonly uint resourceLength;

		// Token: 0x0400226C RID: 8812
		private uint codePage;

		// Token: 0x0400226D RID: 8813
		private uint reserved;
	}
}
