using System;
using Org.BouncyCastle.Math.EC.Endo;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x020005F7 RID: 1527
	public class GlvMultiplier : AbstractECMultiplier
	{
		// Token: 0x06003327 RID: 13095 RVA: 0x00108B00 File Offset: 0x00108B00
		public GlvMultiplier(ECCurve curve, GlvEndomorphism glvEndomorphism)
		{
			if (curve == null || curve.Order == null)
			{
				throw new ArgumentException("Need curve with known group order", "curve");
			}
			this.curve = curve;
			this.glvEndomorphism = glvEndomorphism;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x00108B38 File Offset: 0x00108B38
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			if (!this.curve.Equals(p.Curve))
			{
				throw new InvalidOperationException();
			}
			BigInteger order = p.Curve.Order;
			BigInteger[] array = this.glvEndomorphism.DecomposeScalar(k.Mod(order));
			BigInteger k2 = array[0];
			BigInteger l = array[1];
			if (this.glvEndomorphism.HasEfficientPointMap)
			{
				return ECAlgorithms.ImplShamirsTrickWNaf(this.glvEndomorphism, p, k2, l);
			}
			ECPoint q = EndoUtilities.MapPoint(this.glvEndomorphism, p);
			return ECAlgorithms.ImplShamirsTrickWNaf(p, k2, q, l);
		}

		// Token: 0x04001C8B RID: 7307
		protected readonly ECCurve curve;

		// Token: 0x04001C8C RID: 7308
		protected readonly GlvEndomorphism glvEndomorphism;
	}
}
