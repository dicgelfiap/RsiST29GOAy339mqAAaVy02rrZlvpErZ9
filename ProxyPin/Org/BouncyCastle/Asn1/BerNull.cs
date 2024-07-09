using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000250 RID: 592
	public class BerNull : DerNull
	{
		// Token: 0x060012FB RID: 4859 RVA: 0x00069B90 File Offset: 0x00069B90
		[Obsolete("Use static Instance object")]
		public BerNull()
		{
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00069B98 File Offset: 0x00069B98
		private BerNull(int dummy) : base(dummy)
		{
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00069BA4 File Offset: 0x00069BA4
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(5);
				return;
			}
			base.Encode(derOut);
		}

		// Token: 0x04000D8E RID: 3470
		public new static readonly BerNull Instance = new BerNull(0);
	}
}
