using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200029F RID: 671
	public class Crc24
	{
		// Token: 0x060014EC RID: 5356 RVA: 0x0006FD40 File Offset: 0x0006FD40
		public void Update(int b)
		{
			this.crc ^= b << 16;
			for (int i = 0; i < 8; i++)
			{
				this.crc <<= 1;
				if ((this.crc & 16777216) != 0)
				{
					this.crc ^= 25578747;
				}
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0006FDA4 File Offset: 0x0006FDA4
		[Obsolete("Use 'Value' property instead")]
		public int GetValue()
		{
			return this.crc;
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0006FDAC File Offset: 0x0006FDAC
		public int Value
		{
			get
			{
				return this.crc;
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0006FDB4 File Offset: 0x0006FDB4
		public void Reset()
		{
			this.crc = 11994318;
		}

		// Token: 0x04000E1C RID: 3612
		private const int Crc24Init = 11994318;

		// Token: 0x04000E1D RID: 3613
		private const int Crc24Poly = 25578747;

		// Token: 0x04000E1E RID: 3614
		private int crc = 11994318;
	}
}
