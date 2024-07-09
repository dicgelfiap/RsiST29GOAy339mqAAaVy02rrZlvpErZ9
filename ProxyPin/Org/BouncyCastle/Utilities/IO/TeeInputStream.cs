using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020006EB RID: 1771
	public class TeeInputStream : BaseInputStream
	{
		// Token: 0x06003DB6 RID: 15798 RVA: 0x00151400 File Offset: 0x00151400
		public TeeInputStream(Stream input, Stream tee)
		{
			this.input = input;
			this.tee = tee;
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x00151418 File Offset: 0x00151418
		public override void Close()
		{
			Platform.Dispose(this.input);
			Platform.Dispose(this.tee);
			base.Close();
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x00151438 File Offset: 0x00151438
		public override int Read(byte[] buf, int off, int len)
		{
			int num = this.input.Read(buf, off, len);
			if (num > 0)
			{
				this.tee.Write(buf, off, num);
			}
			return num;
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x00151470 File Offset: 0x00151470
		public override int ReadByte()
		{
			int num = this.input.ReadByte();
			if (num >= 0)
			{
				this.tee.WriteByte((byte)num);
			}
			return num;
		}

		// Token: 0x04001F01 RID: 7937
		private readonly Stream input;

		// Token: 0x04001F02 RID: 7938
		private readonly Stream tee;
	}
}
