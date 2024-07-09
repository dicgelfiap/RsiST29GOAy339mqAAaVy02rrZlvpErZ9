using System;
using System.Runtime.InteropServices;

namespace dnlib.IO
{
	// Token: 0x02000762 RID: 1890
	[ComVisible(true)]
	public sealed class ByteArrayDataReaderFactory : DataReaderFactory
	{
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x00164A88 File Offset: 0x00164A88
		public override string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x00164A90 File Offset: 0x00164A90
		public override uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x00164A98 File Offset: 0x00164A98
		internal byte[] DataArray
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600421F RID: 16927 RVA: 0x00164AA0 File Offset: 0x00164AA0
		internal uint DataOffset
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x00164AA4 File Offset: 0x00164AA4
		private ByteArrayDataReaderFactory(byte[] data, string filename)
		{
			this.filename = filename;
			this.length = (uint)data.Length;
			this.stream = DataStreamFactory.Create(data);
			this.data = data;
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x00164AD0 File Offset: 0x00164AD0
		public static ByteArrayDataReaderFactory Create(byte[] data, string filename)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return new ByteArrayDataReaderFactory(data, filename);
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x00164AEC File Offset: 0x00164AEC
		public static DataReader CreateReader(byte[] data)
		{
			return ByteArrayDataReaderFactory.Create(data, null).CreateReader();
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x00164AFC File Offset: 0x00164AFC
		public override DataReader CreateReader(uint offset, uint length)
		{
			return base.CreateReader(this.stream, offset, length);
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x00164B0C File Offset: 0x00164B0C
		public override void Dispose()
		{
			this.stream = EmptyDataStream.Instance;
			this.length = 0U;
			this.filename = null;
			this.data = null;
		}

		// Token: 0x04002380 RID: 9088
		private DataStream stream;

		// Token: 0x04002381 RID: 9089
		private string filename;

		// Token: 0x04002382 RID: 9090
		private uint length;

		// Token: 0x04002383 RID: 9091
		private byte[] data;
	}
}
