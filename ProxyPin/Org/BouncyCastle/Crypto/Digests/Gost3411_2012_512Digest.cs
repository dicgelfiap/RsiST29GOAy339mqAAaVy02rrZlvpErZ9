using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000355 RID: 853
	public class Gost3411_2012_512Digest : Gost3411_2012Digest
	{
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x00085830 File Offset: 0x00085830
		public override string AlgorithmName
		{
			get
			{
				return "GOST3411-2012-512";
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00085838 File Offset: 0x00085838
		public Gost3411_2012_512Digest() : base(Gost3411_2012_512Digest.IV)
		{
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00085848 File Offset: 0x00085848
		public Gost3411_2012_512Digest(Gost3411_2012_512Digest other) : base(Gost3411_2012_512Digest.IV)
		{
			base.Reset(other);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0008585C File Offset: 0x0008585C
		public override int GetDigestSize()
		{
			return 64;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00085860 File Offset: 0x00085860
		public override IMemoable Copy()
		{
			return new Gost3411_2012_512Digest(this);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00085868 File Offset: 0x00085868
		// Note: this type is marked as 'beforefieldinit'.
		static Gost3411_2012_512Digest()
		{
			byte[] iv = new byte[64];
			Gost3411_2012_512Digest.IV = iv;
		}

		// Token: 0x04001130 RID: 4400
		private static readonly byte[] IV;
	}
}
