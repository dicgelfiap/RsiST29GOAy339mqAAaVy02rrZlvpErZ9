using System;
using System.Runtime.InteropServices;

namespace dnlib.IO
{
	// Token: 0x02000770 RID: 1904
	[ComVisible(true)]
	public sealed class NativeMemoryDataReaderFactory : DataReaderFactory
	{
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060042C0 RID: 17088 RVA: 0x001661A8 File Offset: 0x001661A8
		public override string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x001661B0 File Offset: 0x001661B0
		public override uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x001661B8 File Offset: 0x001661B8
		private unsafe NativeMemoryDataReaderFactory(byte* data, uint length, string filename)
		{
			this.filename = filename;
			this.length = length;
			this.stream = DataStreamFactory.Create(data);
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x001661DC File Offset: 0x001661DC
		internal void SetLength(uint length)
		{
			this.length = length;
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x001661E8 File Offset: 0x001661E8
		public unsafe static NativeMemoryDataReaderFactory Create(byte* data, uint length, string filename)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return new NativeMemoryDataReaderFactory(data, length, filename);
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x00166204 File Offset: 0x00166204
		public override DataReader CreateReader(uint offset, uint length)
		{
			return base.CreateReader(this.stream, offset, length);
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x00166214 File Offset: 0x00166214
		public override void Dispose()
		{
			this.stream = EmptyDataStream.Instance;
			this.length = 0U;
			this.filename = null;
		}

		// Token: 0x0400239B RID: 9115
		private DataStream stream;

		// Token: 0x0400239C RID: 9116
		private string filename;

		// Token: 0x0400239D RID: 9117
		private uint length;
	}
}
