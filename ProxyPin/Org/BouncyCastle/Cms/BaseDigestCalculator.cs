using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002D1 RID: 721
	internal class BaseDigestCalculator : IDigestCalculator
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x0007311C File Offset: 0x0007311C
		internal BaseDigestCalculator(byte[] digest)
		{
			this.digest = digest;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0007312C File Offset: 0x0007312C
		public byte[] GetDigest()
		{
			return Arrays.Clone(this.digest);
		}

		// Token: 0x04000EED RID: 3821
		private readonly byte[] digest;
	}
}
