using System;
using System.IO;
using Org.BouncyCastle.Apache.Bzip2;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000646 RID: 1606
	public class PgpCompressedData : PgpObject
	{
		// Token: 0x060037C5 RID: 14277 RVA: 0x0012AC4C File Offset: 0x0012AC4C
		public PgpCompressedData(BcpgInputStream bcpgInput)
		{
			Packet packet = bcpgInput.ReadPacket();
			if (!(packet is CompressedDataPacket))
			{
				throw new IOException("unexpected packet in stream: " + packet);
			}
			this.data = (CompressedDataPacket)packet;
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x0012AC94 File Offset: 0x0012AC94
		public CompressionAlgorithmTag Algorithm
		{
			get
			{
				return this.data.Algorithm;
			}
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x0012ACA4 File Offset: 0x0012ACA4
		public Stream GetInputStream()
		{
			return this.data.GetInputStream();
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x0012ACB4 File Offset: 0x0012ACB4
		public Stream GetDataStream()
		{
			switch (this.Algorithm)
			{
			case CompressionAlgorithmTag.Uncompressed:
				return this.GetInputStream();
			case CompressionAlgorithmTag.Zip:
				return new ZInputStream(this.GetInputStream(), true);
			case CompressionAlgorithmTag.ZLib:
				return new ZInputStream(this.GetInputStream());
			case CompressionAlgorithmTag.BZip2:
				return new CBZip2InputStream(this.GetInputStream());
			default:
				throw new PgpException("can't recognise compression algorithm: " + this.Algorithm);
			}
		}

		// Token: 0x04001D70 RID: 7536
		private readonly CompressedDataPacket data;
	}
}
