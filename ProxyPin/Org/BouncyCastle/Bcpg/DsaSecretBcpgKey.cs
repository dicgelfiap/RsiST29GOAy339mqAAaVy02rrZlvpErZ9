using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002A2 RID: 674
	public class DsaSecretBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x060014FA RID: 5370 RVA: 0x0006FF08 File Offset: 0x0006FF08
		public DsaSecretBcpgKey(BcpgInputStream bcpgIn)
		{
			this.x = new MPInteger(bcpgIn);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0006FF1C File Offset: 0x0006FF1C
		public DsaSecretBcpgKey(BigInteger x)
		{
			this.x = new MPInteger(x);
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0006FF30 File Offset: 0x0006FF30
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0006FF38 File Offset: 0x0006FF38
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

		// Token: 0x060014FE RID: 5374 RVA: 0x0006FF6C File Offset: 0x0006FF6C
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObject(this.x);
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0006FF7C File Offset: 0x0006FF7C
		public BigInteger X
		{
			get
			{
				return this.x.Value;
			}
		}

		// Token: 0x04000E23 RID: 3619
		internal MPInteger x;
	}
}
