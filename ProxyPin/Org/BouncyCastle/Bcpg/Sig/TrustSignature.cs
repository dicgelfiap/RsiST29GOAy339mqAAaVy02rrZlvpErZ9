using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000294 RID: 660
	public class TrustSignature : SignatureSubpacket
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x0006E074 File Offset: 0x0006E074
		private static byte[] IntToByteArray(int v1, int v2)
		{
			return new byte[]
			{
				(byte)v1,
				(byte)v2
			};
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0006E098 File Offset: 0x0006E098
		public TrustSignature(bool critical, bool isLongLength, byte[] data) : base(SignatureSubpacketTag.TrustSig, critical, isLongLength, data)
		{
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0006E0A4 File Offset: 0x0006E0A4
		public TrustSignature(bool critical, int depth, int trustAmount) : base(SignatureSubpacketTag.TrustSig, critical, false, TrustSignature.IntToByteArray(depth, trustAmount))
		{
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0006E0B8 File Offset: 0x0006E0B8
		public int Depth
		{
			get
			{
				return (int)(this.data[0] & byte.MaxValue);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0006E0C8 File Offset: 0x0006E0C8
		public int TrustAmount
		{
			get
			{
				return (int)(this.data[1] & byte.MaxValue);
			}
		}
	}
}
