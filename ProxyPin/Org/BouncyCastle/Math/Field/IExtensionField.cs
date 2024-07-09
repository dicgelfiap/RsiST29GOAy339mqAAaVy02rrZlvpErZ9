using System;

namespace Org.BouncyCastle.Math.Field
{
	// Token: 0x0200061B RID: 1563
	public interface IExtensionField : IFiniteField
	{
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003526 RID: 13606
		IFiniteField Subfield { get; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003527 RID: 13607
		int Degree { get; }
	}
}
