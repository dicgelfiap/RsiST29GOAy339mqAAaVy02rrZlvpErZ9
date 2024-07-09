using System;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet.Writer
{
	// Token: 0x02000895 RID: 2197
	[ComVisible(true)]
	public class DataReaderChunk : IChunk
	{
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x06005418 RID: 21528 RVA: 0x0019ABD8 File Offset: 0x0019ABD8
		public FileOffset FileOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x0019ABE0 File Offset: 0x0019ABE0
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x0019ABE8 File Offset: 0x0019ABE8
		public DataReaderChunk(DataReader data) : this(ref data)
		{
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x0019ABF4 File Offset: 0x0019ABF4
		public DataReaderChunk(DataReader data, uint virtualSize) : this(ref data, virtualSize)
		{
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x0019AC00 File Offset: 0x0019AC00
		internal DataReaderChunk(ref DataReader data) : this(ref data, data.Length)
		{
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x0019AC10 File Offset: 0x0019AC10
		internal DataReaderChunk(ref DataReader data, uint virtualSize)
		{
			this.data = data;
			this.virtualSize = virtualSize;
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x0019AC2C File Offset: 0x0019AC2C
		public DataReader CreateReader()
		{
			return this.data;
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x0019AC34 File Offset: 0x0019AC34
		public void SetData(DataReader newData)
		{
			if (this.setOffsetCalled && newData.Length != this.data.Length)
			{
				throw new InvalidOperationException("New data must be the same size as the old data after SetOffset() has been called");
			}
			this.data = newData;
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x0019AC6C File Offset: 0x0019AC6C
		public void SetOffset(FileOffset offset, RVA rva)
		{
			this.offset = offset;
			this.rva = rva;
			this.setOffsetCalled = true;
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x0019AC84 File Offset: 0x0019AC84
		public uint GetFileLength()
		{
			return this.data.Length;
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x0019AC94 File Offset: 0x0019AC94
		public uint GetVirtualSize()
		{
			return this.virtualSize;
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x0019AC9C File Offset: 0x0019AC9C
		public void WriteTo(DataWriter writer)
		{
			this.data.Position = 0U;
			this.data.CopyTo(writer);
		}

		// Token: 0x04002864 RID: 10340
		private FileOffset offset;

		// Token: 0x04002865 RID: 10341
		private RVA rva;

		// Token: 0x04002866 RID: 10342
		private DataReader data;

		// Token: 0x04002867 RID: 10343
		private readonly uint virtualSize;

		// Token: 0x04002868 RID: 10344
		private bool setOffsetCalled;
	}
}
