using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000579 RID: 1401
	public abstract class ECPointBase : ECPoint
	{
		// Token: 0x06002BEA RID: 11242 RVA: 0x000E83AC File Offset: 0x000E83AC
		protected internal ECPointBase(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000E83BC File Offset: 0x000E83BC
		protected internal ECPointBase(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000E83CC File Offset: 0x000E83CC
		public override byte[] GetEncoded(bool compressed)
		{
			if (base.IsInfinity)
			{
				return new byte[1];
			}
			ECPoint ecpoint = this.Normalize();
			byte[] encoded = ecpoint.XCoord.GetEncoded();
			if (compressed)
			{
				byte[] array = new byte[encoded.Length + 1];
				array[0] = (ecpoint.CompressionYTilde ? 3 : 2);
				Array.Copy(encoded, 0, array, 1, encoded.Length);
				return array;
			}
			byte[] encoded2 = ecpoint.YCoord.GetEncoded();
			byte[] array2 = new byte[encoded.Length + encoded2.Length + 1];
			array2[0] = 4;
			Array.Copy(encoded, 0, array2, 1, encoded.Length);
			Array.Copy(encoded2, 0, array2, encoded.Length + 1, encoded2.Length);
			return array2;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000E8478 File Offset: 0x000E8478
		public override ECPoint Multiply(BigInteger k)
		{
			return this.Curve.GetMultiplier().Multiply(this, k);
		}
	}
}
