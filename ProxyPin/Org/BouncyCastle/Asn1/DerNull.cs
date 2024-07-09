using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200024F RID: 591
	public class DerNull : Asn1Null
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x00069B38 File Offset: 0x00069B38
		[Obsolete("Use static Instance object")]
		public DerNull()
		{
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00069B4C File Offset: 0x00069B4C
		protected internal DerNull(int dummy)
		{
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00069B60 File Offset: 0x00069B60
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(5, this.zeroBytes);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00069B70 File Offset: 0x00069B70
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			return asn1Object is DerNull;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00069B7C File Offset: 0x00069B7C
		protected override int Asn1GetHashCode()
		{
			return -1;
		}

		// Token: 0x04000D8C RID: 3468
		public static readonly DerNull Instance = new DerNull(0);

		// Token: 0x04000D8D RID: 3469
		private byte[] zeroBytes = new byte[0];
	}
}
