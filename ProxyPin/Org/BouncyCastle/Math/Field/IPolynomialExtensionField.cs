using System;

namespace Org.BouncyCastle.Math.Field
{
	// Token: 0x0200061C RID: 1564
	public interface IPolynomialExtensionField : IExtensionField, IFiniteField
	{
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003528 RID: 13608
		IPolynomial MinimalPolynomial { get; }
	}
}
