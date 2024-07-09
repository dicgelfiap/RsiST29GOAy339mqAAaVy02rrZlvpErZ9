using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002A8 RID: 680
	public class ElGamalSecretBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x00070400 File Offset: 0x00070400
		public ElGamalSecretBcpgKey(BcpgInputStream bcpgIn)
		{
			this.x = new MPInteger(bcpgIn);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00070414 File Offset: 0x00070414
		public ElGamalSecretBcpgKey(BigInteger x)
		{
			this.x = new MPInteger(x);
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00070428 File Offset: 0x00070428
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00070430 File Offset: 0x00070430
		public BigInteger X
		{
			get
			{
				return this.x.Value;
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00070440 File Offset: 0x00070440
		public override byte[] GetEncoded()
		{
			byte[] result;
			try
			{
				result = base.GetEncoded();
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00070474 File Offset: 0x00070474
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObject(this.x);
		}

		// Token: 0x04000E2D RID: 3629
		internal MPInteger x;
	}
}
