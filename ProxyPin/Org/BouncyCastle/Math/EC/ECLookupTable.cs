using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200056F RID: 1391
	public interface ECLookupTable
	{
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002B5C RID: 11100
		int Size { get; }

		// Token: 0x06002B5D RID: 11101
		ECPoint Lookup(int index);

		// Token: 0x06002B5E RID: 11102
		ECPoint LookupVar(int index);
	}
}
