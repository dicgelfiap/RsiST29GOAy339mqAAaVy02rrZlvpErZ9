using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ProtoBuf
{
	// Token: 0x02000C14 RID: 3092
	[ComVisible(true)]
	public sealed class BufferExtension : IExtension, IExtensionResettable
	{
		// Token: 0x06007B05 RID: 31493 RVA: 0x002454F0 File Offset: 0x002454F0
		void IExtensionResettable.Reset()
		{
			this.buffer = null;
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x002454FC File Offset: 0x002454FC
		int IExtension.GetLength()
		{
			if (this.buffer != null)
			{
				return this.buffer.Length;
			}
			return 0;
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x00245514 File Offset: 0x00245514
		Stream IExtension.BeginAppend()
		{
			return new MemoryStream();
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x0024551C File Offset: 0x0024551C
		void IExtension.EndAppend(Stream stream, bool commit)
		{
			try
			{
				int num;
				if (commit && (num = (int)stream.Length) > 0)
				{
					MemoryStream memoryStream = (MemoryStream)stream;
					if (this.buffer == null)
					{
						this.buffer = memoryStream.ToArray();
					}
					else
					{
						int num2 = this.buffer.Length;
						byte[] dst = new byte[num2 + num];
						Buffer.BlockCopy(this.buffer, 0, dst, 0, num2);
						Buffer.BlockCopy(Helpers.GetBuffer(memoryStream), 0, dst, num2, num);
						this.buffer = dst;
					}
				}
			}
			finally
			{
				if (stream != null)
				{
					((IDisposable)stream).Dispose();
				}
			}
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x002455C0 File Offset: 0x002455C0
		Stream IExtension.BeginQuery()
		{
			if (this.buffer != null)
			{
				return new MemoryStream(this.buffer);
			}
			return Stream.Null;
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x002455E0 File Offset: 0x002455E0
		void IExtension.EndQuery(Stream stream)
		{
			try
			{
			}
			finally
			{
				if (stream != null)
				{
					((IDisposable)stream).Dispose();
				}
			}
		}

		// Token: 0x04003B5C RID: 15196
		private byte[] buffer;
	}
}
