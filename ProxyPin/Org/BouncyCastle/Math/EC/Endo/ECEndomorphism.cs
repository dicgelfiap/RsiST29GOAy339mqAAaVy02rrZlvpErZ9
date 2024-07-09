using System;

namespace Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x020005E7 RID: 1511
	public interface ECEndomorphism
	{
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060032E6 RID: 13030
		ECPointMap PointMap { get; }

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060032E7 RID: 13031
		bool HasEfficientPointMap { get; }
	}
}
