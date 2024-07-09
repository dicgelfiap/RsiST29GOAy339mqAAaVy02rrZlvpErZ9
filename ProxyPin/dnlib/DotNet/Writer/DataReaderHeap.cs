using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;
using dnlib.IO;

namespace dnlib.DotNet.Writer
{
	// Token: 0x02000896 RID: 2198
	[ComVisible(true)]
	public sealed class DataReaderHeap : HeapBase
	{
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x06005424 RID: 21540 RVA: 0x0019ACB8 File Offset: 0x0019ACB8
		public override string Name { get; }

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06005425 RID: 21541 RVA: 0x0019ACC0 File Offset: 0x0019ACC0
		internal DotNetStream OptionalOriginalStream { get; }

		// Token: 0x06005426 RID: 21542 RVA: 0x0019ACC8 File Offset: 0x0019ACC8
		public DataReaderHeap(DotNetStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.OptionalOriginalStream = stream;
			this.heapReader = stream.CreateReader();
			this.Name = stream.Name;
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x0019AD04 File Offset: 0x0019AD04
		public DataReaderHeap(string name, DataReader heapReader)
		{
			this.heapReader = heapReader;
			this.heapReader.Position = 0U;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0019AD48 File Offset: 0x0019AD48
		public override uint GetRawLength()
		{
			return this.heapReader.Length;
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0019AD58 File Offset: 0x0019AD58
		protected override void WriteToImpl(DataWriter writer)
		{
			this.heapReader.CopyTo(writer);
		}

		// Token: 0x0400286B RID: 10347
		private readonly DataReader heapReader;
	}
}
