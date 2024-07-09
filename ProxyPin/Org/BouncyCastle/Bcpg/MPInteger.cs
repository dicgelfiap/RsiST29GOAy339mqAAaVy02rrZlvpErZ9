using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020002AE RID: 686
	public class MPInteger : BcpgObject
	{
		// Token: 0x06001537 RID: 5431 RVA: 0x00070668 File Offset: 0x00070668
		public MPInteger(BcpgInputStream bcpgIn)
		{
			if (bcpgIn == null)
			{
				throw new ArgumentNullException("bcpgIn");
			}
			int num = bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte();
			byte[] array = new byte[(num + 7) / 8];
			bcpgIn.ReadFully(array);
			this.val = new BigInteger(1, array);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000706C0 File Offset: 0x000706C0
		public MPInteger(BigInteger val)
		{
			if (val == null)
			{
				throw new ArgumentNullException("val");
			}
			if (val.SignValue < 0)
			{
				throw new ArgumentException("Values must be positive", "val");
			}
			this.val = val;
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x000706FC File Offset: 0x000706FC
		public BigInteger Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00070704 File Offset: 0x00070704
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteShort((short)this.val.BitLength);
			bcpgOut.Write(this.val.ToByteArrayUnsigned());
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00070738 File Offset: 0x00070738
		internal static void Encode(BcpgOutputStream bcpgOut, BigInteger val)
		{
			bcpgOut.WriteShort((short)val.BitLength);
			bcpgOut.Write(val.ToByteArrayUnsigned());
		}

		// Token: 0x04000E41 RID: 3649
		private readonly BigInteger val;
	}
}
