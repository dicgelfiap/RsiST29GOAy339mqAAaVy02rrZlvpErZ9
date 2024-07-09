using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000570 RID: 1392
	public abstract class AbstractECLookupTable : ECLookupTable
	{
		// Token: 0x06002B5F RID: 11103
		public abstract ECPoint Lookup(int index);

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002B60 RID: 11104
		public abstract int Size { get; }

		// Token: 0x06002B61 RID: 11105 RVA: 0x000E6E60 File Offset: 0x000E6E60
		public virtual ECPoint LookupVar(int index)
		{
			return this.Lookup(index);
		}
	}
}
