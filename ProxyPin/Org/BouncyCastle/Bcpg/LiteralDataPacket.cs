using System;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002AB RID: 683
	public class LiteralDataPacket : InputStreamPacket
	{
		// Token: 0x0600152C RID: 5420 RVA: 0x000704D4 File Offset: 0x000704D4
		internal LiteralDataPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
			this.format = bcpgIn.ReadByte();
			int num = bcpgIn.ReadByte();
			this.fileName = new byte[num];
			for (int num2 = 0; num2 != num; num2++)
			{
				int num3 = bcpgIn.ReadByte();
				if (num3 < 0)
				{
					throw new IOException("literal data truncated in header");
				}
				this.fileName[num2] = (byte)num3;
			}
			this.modDate = (long)((ulong)(bcpgIn.ReadByte() << 24 | bcpgIn.ReadByte() << 16 | bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte()) * 1000UL);
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00070570 File Offset: 0x00070570
		public int Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00070578 File Offset: 0x00070578
		public long ModificationTime
		{
			get
			{
				return this.modDate;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00070580 File Offset: 0x00070580
		public string FileName
		{
			get
			{
				return Strings.FromUtf8ByteArray(this.fileName);
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00070590 File Offset: 0x00070590
		public byte[] GetRawFileName()
		{
			return Arrays.Clone(this.fileName);
		}

		// Token: 0x04000E3C RID: 3644
		private int format;

		// Token: 0x04000E3D RID: 3645
		private byte[] fileName;

		// Token: 0x04000E3E RID: 3646
		private long modDate;
	}
}
