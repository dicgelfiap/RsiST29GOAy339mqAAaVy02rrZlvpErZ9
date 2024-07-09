using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002A6 RID: 678
	public class ECSecretBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x06001514 RID: 5396 RVA: 0x00070270 File Offset: 0x00070270
		public ECSecretBcpgKey(BcpgInputStream bcpgIn)
		{
			this.x = new MPInteger(bcpgIn);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00070284 File Offset: 0x00070284
		public ECSecretBcpgKey(BigInteger x)
		{
			this.x = new MPInteger(x);
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x00070298 File Offset: 0x00070298
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x000702A0 File Offset: 0x000702A0
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

		// Token: 0x06001518 RID: 5400 RVA: 0x000702D4 File Offset: 0x000702D4
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObject(this.x);
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x000702E4 File Offset: 0x000702E4
		public virtual BigInteger X
		{
			get
			{
				return this.x.Value;
			}
		}

		// Token: 0x04000E29 RID: 3625
		internal MPInteger x;
	}
}
