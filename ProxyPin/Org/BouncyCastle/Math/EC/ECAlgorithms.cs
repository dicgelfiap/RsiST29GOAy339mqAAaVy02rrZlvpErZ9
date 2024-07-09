using System;
using Org.BouncyCastle.Math.EC.Endo;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Math.Field;
using Org.BouncyCastle.Math.Raw;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200060B RID: 1547
	public class ECAlgorithms
	{
		// Token: 0x06003436 RID: 13366 RVA: 0x0011199C File Offset: 0x0011199C
		public static bool IsF2mCurve(ECCurve c)
		{
			return ECAlgorithms.IsF2mField(c.Field);
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x001119AC File Offset: 0x001119AC
		public static bool IsF2mField(IFiniteField field)
		{
			return field.Dimension > 1 && field.Characteristic.Equals(BigInteger.Two) && field is IPolynomialExtensionField;
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x001119DC File Offset: 0x001119DC
		public static bool IsFpCurve(ECCurve c)
		{
			return ECAlgorithms.IsFpField(c.Field);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x001119EC File Offset: 0x001119EC
		public static bool IsFpField(IFiniteField field)
		{
			return field.Dimension == 1;
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x001119F8 File Offset: 0x001119F8
		public static ECPoint SumOfMultiplies(ECPoint[] ps, BigInteger[] ks)
		{
			if (ps == null || ks == null || ps.Length != ks.Length || ps.Length < 1)
			{
				throw new ArgumentException("point and scalar arrays should be non-null, and of equal, non-zero, length");
			}
			int num = ps.Length;
			switch (num)
			{
			case 1:
				return ps[0].Multiply(ks[0]);
			case 2:
				return ECAlgorithms.SumOfTwoMultiplies(ps[0], ks[0], ps[1], ks[1]);
			default:
			{
				ECPoint ecpoint = ps[0];
				ECCurve curve = ecpoint.Curve;
				ECPoint[] array = new ECPoint[num];
				array[0] = ecpoint;
				for (int i = 1; i < num; i++)
				{
					array[i] = ECAlgorithms.ImportPoint(curve, ps[i]);
				}
				GlvEndomorphism glvEndomorphism = curve.GetEndomorphism() as GlvEndomorphism;
				if (glvEndomorphism != null)
				{
					return ECAlgorithms.ImplCheckResult(ECAlgorithms.ImplSumOfMultipliesGlv(array, ks, glvEndomorphism));
				}
				return ECAlgorithms.ImplCheckResult(ECAlgorithms.ImplSumOfMultiplies(array, ks));
			}
			}
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x00111B00 File Offset: 0x00111B00
		public static ECPoint SumOfTwoMultiplies(ECPoint P, BigInteger a, ECPoint Q, BigInteger b)
		{
			ECCurve curve = P.Curve;
			Q = ECAlgorithms.ImportPoint(curve, Q);
			AbstractF2mCurve abstractF2mCurve = curve as AbstractF2mCurve;
			if (abstractF2mCurve != null && abstractF2mCurve.IsKoblitz)
			{
				return ECAlgorithms.ImplCheckResult(P.Multiply(a).Add(Q.Multiply(b)));
			}
			GlvEndomorphism glvEndomorphism = curve.GetEndomorphism() as GlvEndomorphism;
			if (glvEndomorphism != null)
			{
				return ECAlgorithms.ImplCheckResult(ECAlgorithms.ImplSumOfMultipliesGlv(new ECPoint[]
				{
					P,
					Q
				}, new BigInteger[]
				{
					a,
					b
				}, glvEndomorphism));
			}
			return ECAlgorithms.ImplCheckResult(ECAlgorithms.ImplShamirsTrickWNaf(P, a, Q, b));
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x00111BB4 File Offset: 0x00111BB4
		public static ECPoint ShamirsTrick(ECPoint P, BigInteger k, ECPoint Q, BigInteger l)
		{
			ECCurve curve = P.Curve;
			Q = ECAlgorithms.ImportPoint(curve, Q);
			return ECAlgorithms.ImplCheckResult(ECAlgorithms.ImplShamirsTrickJsf(P, k, Q, l));
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x00111BE4 File Offset: 0x00111BE4
		public static ECPoint ImportPoint(ECCurve c, ECPoint p)
		{
			ECCurve curve = p.Curve;
			if (!c.Equals(curve))
			{
				throw new ArgumentException("Point must be on the same curve");
			}
			return c.ImportPoint(p);
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x00111C1C File Offset: 0x00111C1C
		public static void MontgomeryTrick(ECFieldElement[] zs, int off, int len)
		{
			ECAlgorithms.MontgomeryTrick(zs, off, len, null);
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x00111C28 File Offset: 0x00111C28
		public static void MontgomeryTrick(ECFieldElement[] zs, int off, int len, ECFieldElement scale)
		{
			ECFieldElement[] array = new ECFieldElement[len];
			array[0] = zs[off];
			int i = 0;
			while (++i < len)
			{
				array[i] = array[i - 1].Multiply(zs[off + i]);
			}
			i--;
			if (scale != null)
			{
				array[i] = array[i].Multiply(scale);
			}
			ECFieldElement ecfieldElement = array[i].Invert();
			while (i > 0)
			{
				int num = off + i--;
				ECFieldElement b = zs[num];
				zs[num] = array[i].Multiply(ecfieldElement);
				ecfieldElement = ecfieldElement.Multiply(b);
			}
			zs[off] = ecfieldElement;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x00111CE8 File Offset: 0x00111CE8
		public static ECPoint ReferenceMultiply(ECPoint p, BigInteger k)
		{
			BigInteger bigInteger = k.Abs();
			ECPoint ecpoint = p.Curve.Infinity;
			int bitLength = bigInteger.BitLength;
			if (bitLength > 0)
			{
				if (bigInteger.TestBit(0))
				{
					ecpoint = p;
				}
				for (int i = 1; i < bitLength; i++)
				{
					p = p.Twice();
					if (bigInteger.TestBit(i))
					{
						ecpoint = ecpoint.Add(p);
					}
				}
			}
			if (k.SignValue >= 0)
			{
				return ecpoint;
			}
			return ecpoint.Negate();
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x00111D68 File Offset: 0x00111D68
		public static ECPoint ValidatePoint(ECPoint p)
		{
			if (!p.IsValid())
			{
				throw new InvalidOperationException("Invalid point");
			}
			return p;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x00111D84 File Offset: 0x00111D84
		public static ECPoint CleanPoint(ECCurve c, ECPoint p)
		{
			ECCurve curve = p.Curve;
			if (!c.Equals(curve))
			{
				throw new ArgumentException("Point must be on the same curve", "p");
			}
			return c.DecodePoint(p.GetEncoded(false));
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x00111DC8 File Offset: 0x00111DC8
		internal static ECPoint ImplCheckResult(ECPoint p)
		{
			if (!p.IsValidPartial())
			{
				throw new InvalidOperationException("Invalid result");
			}
			return p;
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x00111DE4 File Offset: 0x00111DE4
		internal static ECPoint ImplShamirsTrickJsf(ECPoint P, BigInteger k, ECPoint Q, BigInteger l)
		{
			ECCurve curve = P.Curve;
			ECPoint infinity = curve.Infinity;
			ECPoint ecpoint = P.Add(Q);
			ECPoint ecpoint2 = P.Subtract(Q);
			ECPoint[] array = new ECPoint[]
			{
				Q,
				ecpoint2,
				P,
				ecpoint
			};
			curve.NormalizeAll(array);
			ECPoint[] array2 = new ECPoint[]
			{
				array[3].Negate(),
				array[2].Negate(),
				array[1].Negate(),
				array[0].Negate(),
				infinity,
				array[0],
				array[1],
				array[2],
				array[3]
			};
			byte[] array3 = WNafUtilities.GenerateJsf(k, l);
			ECPoint ecpoint3 = infinity;
			int num = array3.Length;
			while (--num >= 0)
			{
				int num2 = (int)array3[num];
				int num3 = num2 << 24 >> 28;
				int num4 = num2 << 28 >> 28;
				int num5 = 4 + num3 * 3 + num4;
				ecpoint3 = ecpoint3.TwicePlus(array2[num5]);
			}
			return ecpoint3;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x00111F50 File Offset: 0x00111F50
		internal static ECPoint ImplShamirsTrickWNaf(ECPoint P, BigInteger k, ECPoint Q, BigInteger l)
		{
			bool flag = k.SignValue < 0;
			bool flag2 = l.SignValue < 0;
			BigInteger bigInteger = k.Abs();
			BigInteger bigInteger2 = l.Abs();
			int windowSize = WNafUtilities.GetWindowSize(bigInteger.BitLength, 8);
			int windowSize2 = WNafUtilities.GetWindowSize(bigInteger2.BitLength, 8);
			WNafPreCompInfo wnafPreCompInfo = WNafUtilities.Precompute(P, windowSize, true);
			WNafPreCompInfo wnafPreCompInfo2 = WNafUtilities.Precompute(Q, windowSize2, true);
			ECCurve curve = P.Curve;
			int combSize = FixedPointUtilities.GetCombSize(curve);
			if (!flag && !flag2 && k.BitLength <= combSize && l.BitLength <= combSize && wnafPreCompInfo.IsPromoted && wnafPreCompInfo2.IsPromoted)
			{
				return ECAlgorithms.ImplShamirsTrickFixedPoint(P, k, Q, l);
			}
			int width = Math.Min(8, wnafPreCompInfo.Width);
			int width2 = Math.Min(8, wnafPreCompInfo2.Width);
			ECPoint[] preCompP = flag ? wnafPreCompInfo.PreCompNeg : wnafPreCompInfo.PreComp;
			ECPoint[] preCompQ = flag2 ? wnafPreCompInfo2.PreCompNeg : wnafPreCompInfo2.PreComp;
			ECPoint[] preCompNegP = flag ? wnafPreCompInfo.PreComp : wnafPreCompInfo.PreCompNeg;
			ECPoint[] preCompNegQ = flag2 ? wnafPreCompInfo2.PreComp : wnafPreCompInfo2.PreCompNeg;
			byte[] wnafP = WNafUtilities.GenerateWindowNaf(width, bigInteger);
			byte[] wnafQ = WNafUtilities.GenerateWindowNaf(width2, bigInteger2);
			return ECAlgorithms.ImplShamirsTrickWNaf(preCompP, preCompNegP, wnafP, preCompQ, preCompNegQ, wnafQ);
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x001120C0 File Offset: 0x001120C0
		internal static ECPoint ImplShamirsTrickWNaf(ECEndomorphism endomorphism, ECPoint P, BigInteger k, BigInteger l)
		{
			bool flag = k.SignValue < 0;
			bool flag2 = l.SignValue < 0;
			k = k.Abs();
			l = l.Abs();
			int windowSize = WNafUtilities.GetWindowSize(Math.Max(k.BitLength, l.BitLength), 8);
			WNafPreCompInfo wnafPreCompInfo = WNafUtilities.Precompute(P, windowSize, true);
			ECPoint p = EndoUtilities.MapPoint(endomorphism, P);
			WNafPreCompInfo wnafPreCompInfo2 = WNafUtilities.PrecomputeWithPointMap(p, endomorphism.PointMap, wnafPreCompInfo, true);
			int width = Math.Min(8, wnafPreCompInfo.Width);
			int width2 = Math.Min(8, wnafPreCompInfo2.Width);
			ECPoint[] preCompP = flag ? wnafPreCompInfo.PreCompNeg : wnafPreCompInfo.PreComp;
			ECPoint[] preCompQ = flag2 ? wnafPreCompInfo2.PreCompNeg : wnafPreCompInfo2.PreComp;
			ECPoint[] preCompNegP = flag ? wnafPreCompInfo.PreComp : wnafPreCompInfo.PreCompNeg;
			ECPoint[] preCompNegQ = flag2 ? wnafPreCompInfo2.PreComp : wnafPreCompInfo2.PreCompNeg;
			byte[] wnafP = WNafUtilities.GenerateWindowNaf(width, k);
			byte[] wnafQ = WNafUtilities.GenerateWindowNaf(width2, l);
			return ECAlgorithms.ImplShamirsTrickWNaf(preCompP, preCompNegP, wnafP, preCompQ, preCompNegQ, wnafQ);
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x001121DC File Offset: 0x001121DC
		private static ECPoint ImplShamirsTrickWNaf(ECPoint[] preCompP, ECPoint[] preCompNegP, byte[] wnafP, ECPoint[] preCompQ, ECPoint[] preCompNegQ, byte[] wnafQ)
		{
			int num = Math.Max(wnafP.Length, wnafQ.Length);
			ECCurve curve = preCompP[0].Curve;
			ECPoint infinity = curve.Infinity;
			ECPoint ecpoint = infinity;
			int num2 = 0;
			for (int i = num - 1; i >= 0; i--)
			{
				int num3 = (int)((i < wnafP.Length) ? ((sbyte)wnafP[i]) : 0);
				int num4 = (int)((i < wnafQ.Length) ? ((sbyte)wnafQ[i]) : 0);
				if ((num3 | num4) == 0)
				{
					num2++;
				}
				else
				{
					ECPoint ecpoint2 = infinity;
					if (num3 != 0)
					{
						int num5 = Math.Abs(num3);
						ECPoint[] array = (num3 < 0) ? preCompNegP : preCompP;
						ecpoint2 = ecpoint2.Add(array[num5 >> 1]);
					}
					if (num4 != 0)
					{
						int num6 = Math.Abs(num4);
						ECPoint[] array2 = (num4 < 0) ? preCompNegQ : preCompQ;
						ecpoint2 = ecpoint2.Add(array2[num6 >> 1]);
					}
					if (num2 > 0)
					{
						ecpoint = ecpoint.TimesPow2(num2);
						num2 = 0;
					}
					ecpoint = ecpoint.TwicePlus(ecpoint2);
				}
			}
			if (num2 > 0)
			{
				ecpoint = ecpoint.TimesPow2(num2);
			}
			return ecpoint;
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x00112310 File Offset: 0x00112310
		internal static ECPoint ImplSumOfMultiplies(ECPoint[] ps, BigInteger[] ks)
		{
			int num = ps.Length;
			bool[] array = new bool[num];
			WNafPreCompInfo[] array2 = new WNafPreCompInfo[num];
			byte[][] array3 = new byte[num][];
			for (int i = 0; i < num; i++)
			{
				BigInteger bigInteger = ks[i];
				array[i] = (bigInteger.SignValue < 0);
				bigInteger = bigInteger.Abs();
				int windowSize = WNafUtilities.GetWindowSize(bigInteger.BitLength, 8);
				WNafPreCompInfo wnafPreCompInfo = WNafUtilities.Precompute(ps[i], windowSize, true);
				int width = Math.Min(8, wnafPreCompInfo.Width);
				array2[i] = wnafPreCompInfo;
				array3[i] = WNafUtilities.GenerateWindowNaf(width, bigInteger);
			}
			return ECAlgorithms.ImplSumOfMultiplies(array, array2, array3);
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x001123C4 File Offset: 0x001123C4
		internal static ECPoint ImplSumOfMultipliesGlv(ECPoint[] ps, BigInteger[] ks, GlvEndomorphism glvEndomorphism)
		{
			BigInteger order = ps[0].Curve.Order;
			int num = ps.Length;
			BigInteger[] array = new BigInteger[num << 1];
			int i = 0;
			int num2 = 0;
			while (i < num)
			{
				BigInteger[] array2 = glvEndomorphism.DecomposeScalar(ks[i].Mod(order));
				array[num2++] = array2[0];
				array[num2++] = array2[1];
				i++;
			}
			if (glvEndomorphism.HasEfficientPointMap)
			{
				return ECAlgorithms.ImplSumOfMultiplies(glvEndomorphism, ps, array);
			}
			ECPoint[] array3 = new ECPoint[num << 1];
			int j = 0;
			int num3 = 0;
			while (j < num)
			{
				ECPoint ecpoint = ps[j];
				ECPoint ecpoint2 = EndoUtilities.MapPoint(glvEndomorphism, ecpoint);
				array3[num3++] = ecpoint;
				array3[num3++] = ecpoint2;
				j++;
			}
			return ECAlgorithms.ImplSumOfMultiplies(array3, array);
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x001124A8 File Offset: 0x001124A8
		internal static ECPoint ImplSumOfMultiplies(ECEndomorphism endomorphism, ECPoint[] ps, BigInteger[] ks)
		{
			int num = ps.Length;
			int num2 = num << 1;
			bool[] array = new bool[num2];
			WNafPreCompInfo[] array2 = new WNafPreCompInfo[num2];
			byte[][] array3 = new byte[num2][];
			ECPointMap pointMap = endomorphism.PointMap;
			for (int i = 0; i < num; i++)
			{
				int num3 = i << 1;
				int num4 = num3 + 1;
				BigInteger bigInteger = ks[num3];
				array[num3] = (bigInteger.SignValue < 0);
				bigInteger = bigInteger.Abs();
				BigInteger bigInteger2 = ks[num4];
				array[num4] = (bigInteger2.SignValue < 0);
				bigInteger2 = bigInteger2.Abs();
				int windowSize = WNafUtilities.GetWindowSize(Math.Max(bigInteger.BitLength, bigInteger2.BitLength), 8);
				ECPoint p = ps[i];
				WNafPreCompInfo wnafPreCompInfo = WNafUtilities.Precompute(p, windowSize, true);
				ECPoint p2 = EndoUtilities.MapPoint(endomorphism, p);
				WNafPreCompInfo wnafPreCompInfo2 = WNafUtilities.PrecomputeWithPointMap(p2, pointMap, wnafPreCompInfo, true);
				int width = Math.Min(8, wnafPreCompInfo.Width);
				int width2 = Math.Min(8, wnafPreCompInfo2.Width);
				array2[num3] = wnafPreCompInfo;
				array2[num4] = wnafPreCompInfo2;
				array3[num3] = WNafUtilities.GenerateWindowNaf(width, bigInteger);
				array3[num4] = WNafUtilities.GenerateWindowNaf(width2, bigInteger2);
			}
			return ECAlgorithms.ImplSumOfMultiplies(array, array2, array3);
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x001125EC File Offset: 0x001125EC
		private static ECPoint ImplSumOfMultiplies(bool[] negs, WNafPreCompInfo[] infos, byte[][] wnafs)
		{
			int num = 0;
			int num2 = wnafs.Length;
			for (int i = 0; i < num2; i++)
			{
				num = Math.Max(num, wnafs[i].Length);
			}
			ECCurve curve = infos[0].PreComp[0].Curve;
			ECPoint infinity = curve.Infinity;
			ECPoint ecpoint = infinity;
			int num3 = 0;
			for (int j = num - 1; j >= 0; j--)
			{
				ECPoint ecpoint2 = infinity;
				for (int k = 0; k < num2; k++)
				{
					byte[] array = wnafs[k];
					int num4 = (int)((j < array.Length) ? ((sbyte)array[j]) : 0);
					if (num4 != 0)
					{
						int num5 = Math.Abs(num4);
						WNafPreCompInfo wnafPreCompInfo = infos[k];
						ECPoint[] array2 = (num4 < 0 == negs[k]) ? wnafPreCompInfo.PreComp : wnafPreCompInfo.PreCompNeg;
						ecpoint2 = ecpoint2.Add(array2[num5 >> 1]);
					}
				}
				if (ecpoint2 == infinity)
				{
					num3++;
				}
				else
				{
					if (num3 > 0)
					{
						ecpoint = ecpoint.TimesPow2(num3);
						num3 = 0;
					}
					ecpoint = ecpoint.TwicePlus(ecpoint2);
				}
			}
			if (num3 > 0)
			{
				ecpoint = ecpoint.TimesPow2(num3);
			}
			return ecpoint;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x00112738 File Offset: 0x00112738
		private static ECPoint ImplShamirsTrickFixedPoint(ECPoint p, BigInteger k, ECPoint q, BigInteger l)
		{
			ECCurve curve = p.Curve;
			int combSize = FixedPointUtilities.GetCombSize(curve);
			if (k.BitLength > combSize || l.BitLength > combSize)
			{
				throw new InvalidOperationException("fixed-point comb doesn't support scalars larger than the curve order");
			}
			FixedPointPreCompInfo fixedPointPreCompInfo = FixedPointUtilities.Precompute(p);
			FixedPointPreCompInfo fixedPointPreCompInfo2 = FixedPointUtilities.Precompute(q);
			ECLookupTable lookupTable = fixedPointPreCompInfo.LookupTable;
			ECLookupTable lookupTable2 = fixedPointPreCompInfo2.LookupTable;
			int width = fixedPointPreCompInfo.Width;
			int width2 = fixedPointPreCompInfo2.Width;
			if (width != width2)
			{
				FixedPointCombMultiplier fixedPointCombMultiplier = new FixedPointCombMultiplier();
				ECPoint ecpoint = fixedPointCombMultiplier.Multiply(p, k);
				ECPoint b = fixedPointCombMultiplier.Multiply(q, l);
				return ecpoint.Add(b);
			}
			int num = width;
			int num2 = (combSize + num - 1) / num;
			ECPoint ecpoint2 = curve.Infinity;
			int num3 = num2 * num;
			uint[] array = Nat.FromBigInteger(num3, k);
			uint[] array2 = Nat.FromBigInteger(num3, l);
			int num4 = num3 - 1;
			for (int i = 0; i < num2; i++)
			{
				uint num5 = 0U;
				uint num6 = 0U;
				for (int j = num4 - i; j >= 0; j -= num2)
				{
					uint num7 = array[j >> 5] >> j;
					num5 ^= num7 >> 1;
					num5 <<= 1;
					num5 ^= num7;
					uint num8 = array2[j >> 5] >> j;
					num6 ^= num8 >> 1;
					num6 <<= 1;
					num6 ^= num8;
				}
				ECPoint ecpoint3 = lookupTable.LookupVar((int)num5);
				ECPoint b2 = lookupTable2.LookupVar((int)num6);
				ECPoint b3 = ecpoint3.Add(b2);
				ecpoint2 = ecpoint2.TwicePlus(b3);
			}
			return ecpoint2.Add(fixedPointPreCompInfo.Offset).Add(fixedPointPreCompInfo2.Offset);
		}
	}
}
