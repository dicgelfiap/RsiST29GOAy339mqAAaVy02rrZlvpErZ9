using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000354 RID: 852
	public class Gost3411_2012_256Digest : Gost3411_2012Digest
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x000857AC File Offset: 0x000857AC
		public override string AlgorithmName
		{
			get
			{
				return "GOST3411-2012-256";
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000857B4 File Offset: 0x000857B4
		public Gost3411_2012_256Digest() : base(Gost3411_2012_256Digest.IV)
		{
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000857C4 File Offset: 0x000857C4
		public Gost3411_2012_256Digest(Gost3411_2012_256Digest other) : base(Gost3411_2012_256Digest.IV)
		{
			base.Reset(other);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000857D8 File Offset: 0x000857D8
		public override int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000857DC File Offset: 0x000857DC
		public override int DoFinal(byte[] output, int outOff)
		{
			byte[] array = new byte[64];
			base.DoFinal(array, 0);
			Array.Copy(array, 32, output, outOff, 32);
			return 32;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0008580C File Offset: 0x0008580C
		public override IMemoable Copy()
		{
			return new Gost3411_2012_256Digest(this);
		}

		// Token: 0x0400112F RID: 4399
		private static readonly byte[] IV = new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		};
	}
}
