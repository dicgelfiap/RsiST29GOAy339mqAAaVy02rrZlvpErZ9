using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020006EC RID: 1772
	public class TeeOutputStream : BaseOutputStream
	{
		// Token: 0x06003DBA RID: 15802 RVA: 0x001514A4 File Offset: 0x001514A4
		public TeeOutputStream(Stream output, Stream tee)
		{
			this.output = output;
			this.tee = tee;
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x001514BC File Offset: 0x001514BC
		public override void Close()
		{
			Platform.Dispose(this.output);
			Platform.Dispose(this.tee);
			base.Close();
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x001514DC File Offset: 0x001514DC
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.output.Write(buffer, offset, count);
			this.tee.Write(buffer, offset, count);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x001514FC File Offset: 0x001514FC
		public override void WriteByte(byte b)
		{
			this.output.WriteByte(b);
			this.tee.WriteByte(b);
		}

		// Token: 0x04001F03 RID: 7939
		private readonly Stream output;

		// Token: 0x04001F04 RID: 7940
		private readonly Stream tee;
	}
}
