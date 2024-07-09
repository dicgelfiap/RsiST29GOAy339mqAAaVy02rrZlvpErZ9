using System;
using System.IO;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000654 RID: 1620
	public class PgpLiteralData : PgpObject
	{
		// Token: 0x06003839 RID: 14393 RVA: 0x0012E52C File Offset: 0x0012E52C
		public PgpLiteralData(BcpgInputStream bcpgInput)
		{
			Packet packet = bcpgInput.ReadPacket();
			if (!(packet is LiteralDataPacket))
			{
				throw new IOException("unexpected packet in stream: " + packet);
			}
			this.data = (LiteralDataPacket)packet;
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600383A RID: 14394 RVA: 0x0012E574 File Offset: 0x0012E574
		public int Format
		{
			get
			{
				return this.data.Format;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x0012E584 File Offset: 0x0012E584
		public string FileName
		{
			get
			{
				return this.data.FileName;
			}
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x0012E594 File Offset: 0x0012E594
		public byte[] GetRawFileName()
		{
			return this.data.GetRawFileName();
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x0012E5A4 File Offset: 0x0012E5A4
		public DateTime ModificationTime
		{
			get
			{
				return DateTimeUtilities.UnixMsToDateTime(this.data.ModificationTime);
			}
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x0012E5B8 File Offset: 0x0012E5B8
		public Stream GetInputStream()
		{
			return this.data.GetInputStream();
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x0012E5C8 File Offset: 0x0012E5C8
		public Stream GetDataStream()
		{
			return this.GetInputStream();
		}

		// Token: 0x04001DC1 RID: 7617
		public const char Binary = 'b';

		// Token: 0x04001DC2 RID: 7618
		public const char Text = 't';

		// Token: 0x04001DC3 RID: 7619
		public const char Utf8 = 'u';

		// Token: 0x04001DC4 RID: 7620
		public const string Console = "_CONSOLE";

		// Token: 0x04001DC5 RID: 7621
		private readonly LiteralDataPacket data;
	}
}
