using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000578 RID: 1400
	public interface IPreCompCallback
	{
		// Token: 0x06002BE9 RID: 11241
		PreCompInfo Precompute(PreCompInfo existing);
	}
}
