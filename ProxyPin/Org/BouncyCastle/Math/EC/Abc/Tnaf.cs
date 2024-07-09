﻿using System;

namespace Org.BouncyCastle.Math.EC.Abc
{
	// Token: 0x0200056C RID: 1388
	internal class Tnaf
	{
		// Token: 0x06002B1F RID: 11039 RVA: 0x000E57CC File Offset: 0x000E57CC
		public static BigInteger Norm(sbyte mu, ZTauElement lambda)
		{
			BigInteger bigInteger = lambda.u.Multiply(lambda.u);
			BigInteger bigInteger2 = lambda.u.Multiply(lambda.v);
			BigInteger value = lambda.v.Multiply(lambda.v).ShiftLeft(1);
			BigInteger result;
			if (mu == 1)
			{
				result = bigInteger.Add(bigInteger2).Add(value);
			}
			else
			{
				if (mu != -1)
				{
					throw new ArgumentException("mu must be 1 or -1");
				}
				result = bigInteger.Subtract(bigInteger2).Add(value);
			}
			return result;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000E585C File Offset: 0x000E585C
		public static SimpleBigDecimal Norm(sbyte mu, SimpleBigDecimal u, SimpleBigDecimal v)
		{
			SimpleBigDecimal simpleBigDecimal = u.Multiply(u);
			SimpleBigDecimal b = u.Multiply(v);
			SimpleBigDecimal b2 = v.Multiply(v).ShiftLeft(1);
			SimpleBigDecimal result;
			if (mu == 1)
			{
				result = simpleBigDecimal.Add(b).Add(b2);
			}
			else
			{
				if (mu != -1)
				{
					throw new ArgumentException("mu must be 1 or -1");
				}
				result = simpleBigDecimal.Subtract(b).Add(b2);
			}
			return result;
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000E58CC File Offset: 0x000E58CC
		public static ZTauElement Round(SimpleBigDecimal lambda0, SimpleBigDecimal lambda1, sbyte mu)
		{
			int scale = lambda0.Scale;
			if (lambda1.Scale != scale)
			{
				throw new ArgumentException("lambda0 and lambda1 do not have same scale");
			}
			if (mu != 1 && mu != -1)
			{
				throw new ArgumentException("mu must be 1 or -1");
			}
			BigInteger bigInteger = lambda0.Round();
			BigInteger bigInteger2 = lambda1.Round();
			SimpleBigDecimal simpleBigDecimal = lambda0.Subtract(bigInteger);
			SimpleBigDecimal simpleBigDecimal2 = lambda1.Subtract(bigInteger2);
			SimpleBigDecimal simpleBigDecimal3 = simpleBigDecimal.Add(simpleBigDecimal);
			if (mu == 1)
			{
				simpleBigDecimal3 = simpleBigDecimal3.Add(simpleBigDecimal2);
			}
			else
			{
				simpleBigDecimal3 = simpleBigDecimal3.Subtract(simpleBigDecimal2);
			}
			SimpleBigDecimal simpleBigDecimal4 = simpleBigDecimal2.Add(simpleBigDecimal2).Add(simpleBigDecimal2);
			SimpleBigDecimal b = simpleBigDecimal4.Add(simpleBigDecimal2);
			SimpleBigDecimal simpleBigDecimal5;
			SimpleBigDecimal simpleBigDecimal6;
			if (mu == 1)
			{
				simpleBigDecimal5 = simpleBigDecimal.Subtract(simpleBigDecimal4);
				simpleBigDecimal6 = simpleBigDecimal.Add(b);
			}
			else
			{
				simpleBigDecimal5 = simpleBigDecimal.Add(simpleBigDecimal4);
				simpleBigDecimal6 = simpleBigDecimal.Subtract(b);
			}
			sbyte b2 = 0;
			sbyte b3 = 0;
			if (simpleBigDecimal3.CompareTo(BigInteger.One) >= 0)
			{
				if (simpleBigDecimal5.CompareTo(Tnaf.MinusOne) < 0)
				{
					b3 = mu;
				}
				else
				{
					b2 = 1;
				}
			}
			else if (simpleBigDecimal6.CompareTo(BigInteger.Two) >= 0)
			{
				b3 = mu;
			}
			if (simpleBigDecimal3.CompareTo(Tnaf.MinusOne) < 0)
			{
				if (simpleBigDecimal5.CompareTo(BigInteger.One) >= 0)
				{
					b3 = -mu;
				}
				else
				{
					b2 = -1;
				}
			}
			else if (simpleBigDecimal6.CompareTo(Tnaf.MinusTwo) < 0)
			{
				b3 = -mu;
			}
			BigInteger u = bigInteger.Add(BigInteger.ValueOf((long)b2));
			BigInteger v = bigInteger2.Add(BigInteger.ValueOf((long)b3));
			return new ZTauElement(u, v);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000E5A74 File Offset: 0x000E5A74
		public static SimpleBigDecimal ApproximateDivisionByN(BigInteger k, BigInteger s, BigInteger vm, sbyte a, int m, int c)
		{
			int num = (m + 5) / 2 + c;
			BigInteger val = k.ShiftRight(m - num - 2 + (int)a);
			BigInteger bigInteger = s.Multiply(val);
			BigInteger val2 = bigInteger.ShiftRight(m);
			BigInteger value = vm.Multiply(val2);
			BigInteger bigInteger2 = bigInteger.Add(value);
			BigInteger bigInteger3 = bigInteger2.ShiftRight(num - c);
			if (bigInteger2.TestBit(num - c - 1))
			{
				bigInteger3 = bigInteger3.Add(BigInteger.One);
			}
			return new SimpleBigDecimal(bigInteger3, c);
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000E5AF8 File Offset: 0x000E5AF8
		public static sbyte[] TauAdicNaf(sbyte mu, ZTauElement lambda)
		{
			if (mu != 1 && mu != -1)
			{
				throw new ArgumentException("mu must be 1 or -1");
			}
			BigInteger bigInteger = Tnaf.Norm(mu, lambda);
			int bitLength = bigInteger.BitLength;
			int num = (bitLength > 30) ? (bitLength + 4) : 34;
			sbyte[] array = new sbyte[num];
			int num2 = 0;
			int num3 = 0;
			BigInteger bigInteger2 = lambda.u;
			BigInteger bigInteger3 = lambda.v;
			while (!bigInteger2.Equals(BigInteger.Zero) || !bigInteger3.Equals(BigInteger.Zero))
			{
				if (bigInteger2.TestBit(0))
				{
					array[num2] = (sbyte)BigInteger.Two.Subtract(bigInteger2.Subtract(bigInteger3.ShiftLeft(1)).Mod(Tnaf.Four)).IntValue;
					if (array[num2] == 1)
					{
						bigInteger2 = bigInteger2.ClearBit(0);
					}
					else
					{
						bigInteger2 = bigInteger2.Add(BigInteger.One);
					}
					num3 = num2;
				}
				else
				{
					array[num2] = 0;
				}
				BigInteger bigInteger4 = bigInteger2;
				BigInteger bigInteger5 = bigInteger2.ShiftRight(1);
				if (mu == 1)
				{
					bigInteger2 = bigInteger3.Add(bigInteger5);
				}
				else
				{
					bigInteger2 = bigInteger3.Subtract(bigInteger5);
				}
				bigInteger3 = bigInteger4.ShiftRight(1).Negate();
				num2++;
			}
			num3++;
			sbyte[] array2 = new sbyte[num3];
			Array.Copy(array, 0, array2, 0, num3);
			return array2;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000E5C5C File Offset: 0x000E5C5C
		public static AbstractF2mPoint Tau(AbstractF2mPoint p)
		{
			return p.Tau();
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000E5C64 File Offset: 0x000E5C64
		public static sbyte GetMu(AbstractF2mCurve curve)
		{
			BigInteger bigInteger = curve.A.ToBigInteger();
			sbyte result;
			if (bigInteger.SignValue == 0)
			{
				result = -1;
			}
			else
			{
				if (!bigInteger.Equals(BigInteger.One))
				{
					throw new ArgumentException("No Koblitz curve (ABC), TNAF multiplication not possible");
				}
				result = 1;
			}
			return result;
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000E5CB8 File Offset: 0x000E5CB8
		public static sbyte GetMu(ECFieldElement curveA)
		{
			return curveA.IsZero ? -1 : 1;
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000E5CD0 File Offset: 0x000E5CD0
		public static sbyte GetMu(int curveA)
		{
			return (curveA == 0) ? -1 : 1;
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000E5CE0 File Offset: 0x000E5CE0
		public static BigInteger[] GetLucas(sbyte mu, int k, bool doV)
		{
			if (mu != 1 && mu != -1)
			{
				throw new ArgumentException("mu must be 1 or -1");
			}
			BigInteger bigInteger;
			BigInteger bigInteger2;
			if (doV)
			{
				bigInteger = BigInteger.Two;
				bigInteger2 = BigInteger.ValueOf((long)mu);
			}
			else
			{
				bigInteger = BigInteger.Zero;
				bigInteger2 = BigInteger.One;
			}
			for (int i = 1; i < k; i++)
			{
				BigInteger bigInteger3;
				if (mu == 1)
				{
					bigInteger3 = bigInteger2;
				}
				else
				{
					bigInteger3 = bigInteger2.Negate();
				}
				BigInteger bigInteger4 = bigInteger3.Subtract(bigInteger.ShiftLeft(1));
				bigInteger = bigInteger2;
				bigInteger2 = bigInteger4;
			}
			return new BigInteger[]
			{
				bigInteger,
				bigInteger2
			};
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000E5D8C File Offset: 0x000E5D8C
		public static BigInteger GetTw(sbyte mu, int w)
		{
			if (w != 4)
			{
				BigInteger[] lucas = Tnaf.GetLucas(mu, w, false);
				BigInteger m = BigInteger.Zero.SetBit(w);
				BigInteger val = lucas[1].ModInverse(m);
				return BigInteger.Two.Multiply(lucas[0]).Multiply(val).Mod(m);
			}
			if (mu == 1)
			{
				return BigInteger.ValueOf(6L);
			}
			return BigInteger.ValueOf(10L);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000E5E00 File Offset: 0x000E5E00
		public static BigInteger[] GetSi(AbstractF2mCurve curve)
		{
			if (!curve.IsKoblitz)
			{
				throw new ArgumentException("si is defined for Koblitz curves only");
			}
			int fieldSize = curve.FieldSize;
			int intValue = curve.A.ToBigInteger().IntValue;
			sbyte mu = Tnaf.GetMu(intValue);
			int shiftsForCofactor = Tnaf.GetShiftsForCofactor(curve.Cofactor);
			int k = fieldSize + 3 - intValue;
			BigInteger[] lucas = Tnaf.GetLucas(mu, k, false);
			if (mu == 1)
			{
				lucas[0] = lucas[0].Negate();
				lucas[1] = lucas[1].Negate();
			}
			BigInteger bigInteger = BigInteger.One.Add(lucas[1]).ShiftRight(shiftsForCofactor);
			BigInteger bigInteger2 = BigInteger.One.Add(lucas[0]).ShiftRight(shiftsForCofactor).Negate();
			return new BigInteger[]
			{
				bigInteger,
				bigInteger2
			};
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000E5EEC File Offset: 0x000E5EEC
		public static BigInteger[] GetSi(int fieldSize, int curveA, BigInteger cofactor)
		{
			sbyte mu = Tnaf.GetMu(curveA);
			int shiftsForCofactor = Tnaf.GetShiftsForCofactor(cofactor);
			int k = fieldSize + 3 - curveA;
			BigInteger[] lucas = Tnaf.GetLucas(mu, k, false);
			if (mu == 1)
			{
				lucas[0] = lucas[0].Negate();
				lucas[1] = lucas[1].Negate();
			}
			BigInteger bigInteger = BigInteger.One.Add(lucas[1]).ShiftRight(shiftsForCofactor);
			BigInteger bigInteger2 = BigInteger.One.Add(lucas[0]).ShiftRight(shiftsForCofactor).Negate();
			return new BigInteger[]
			{
				bigInteger,
				bigInteger2
			};
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000E5F9C File Offset: 0x000E5F9C
		protected static int GetShiftsForCofactor(BigInteger h)
		{
			if (h != null && h.BitLength < 4)
			{
				int intValue = h.IntValue;
				if (intValue == 2)
				{
					return 1;
				}
				if (intValue == 4)
				{
					return 2;
				}
			}
			throw new ArgumentException("h (Cofactor) must be 2 or 4");
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000E5FE4 File Offset: 0x000E5FE4
		public static ZTauElement PartModReduction(BigInteger k, int m, sbyte a, BigInteger[] s, sbyte mu, sbyte c)
		{
			BigInteger bigInteger;
			if (mu == 1)
			{
				bigInteger = s[0].Add(s[1]);
			}
			else
			{
				bigInteger = s[0].Subtract(s[1]);
			}
			BigInteger[] lucas = Tnaf.GetLucas(mu, m, true);
			BigInteger vm = lucas[1];
			SimpleBigDecimal lambda = Tnaf.ApproximateDivisionByN(k, s[0], vm, a, m, (int)c);
			SimpleBigDecimal lambda2 = Tnaf.ApproximateDivisionByN(k, s[1], vm, a, m, (int)c);
			ZTauElement ztauElement = Tnaf.Round(lambda, lambda2, mu);
			BigInteger u = k.Subtract(bigInteger.Multiply(ztauElement.u)).Subtract(BigInteger.ValueOf(2L).Multiply(s[1]).Multiply(ztauElement.v));
			BigInteger v = s[1].Multiply(ztauElement.u).Subtract(s[0].Multiply(ztauElement.v));
			return new ZTauElement(u, v);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000E60E0 File Offset: 0x000E60E0
		public static AbstractF2mPoint MultiplyRTnaf(AbstractF2mPoint p, BigInteger k)
		{
			AbstractF2mCurve abstractF2mCurve = (AbstractF2mCurve)p.Curve;
			int fieldSize = abstractF2mCurve.FieldSize;
			int intValue = abstractF2mCurve.A.ToBigInteger().IntValue;
			sbyte mu = Tnaf.GetMu(intValue);
			BigInteger[] si = abstractF2mCurve.GetSi();
			ZTauElement lambda = Tnaf.PartModReduction(k, fieldSize, (sbyte)intValue, si, mu, 10);
			return Tnaf.MultiplyTnaf(p, lambda);
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000E613C File Offset: 0x000E613C
		public static AbstractF2mPoint MultiplyTnaf(AbstractF2mPoint p, ZTauElement lambda)
		{
			AbstractF2mCurve abstractF2mCurve = (AbstractF2mCurve)p.Curve;
			sbyte mu = Tnaf.GetMu(abstractF2mCurve.A);
			sbyte[] u = Tnaf.TauAdicNaf(mu, lambda);
			return Tnaf.MultiplyFromTnaf(p, u);
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x000E6178 File Offset: 0x000E6178
		public static AbstractF2mPoint MultiplyFromTnaf(AbstractF2mPoint p, sbyte[] u)
		{
			ECCurve curve = p.Curve;
			AbstractF2mPoint abstractF2mPoint = (AbstractF2mPoint)curve.Infinity;
			AbstractF2mPoint abstractF2mPoint2 = (AbstractF2mPoint)p.Negate();
			int num = 0;
			for (int i = u.Length - 1; i >= 0; i--)
			{
				num++;
				sbyte b = u[i];
				if (b != 0)
				{
					abstractF2mPoint = abstractF2mPoint.TauPow(num);
					num = 0;
					ECPoint b2 = (b > 0) ? p : abstractF2mPoint2;
					abstractF2mPoint = (AbstractF2mPoint)abstractF2mPoint.Add(b2);
				}
			}
			if (num > 0)
			{
				abstractF2mPoint = abstractF2mPoint.TauPow(num);
			}
			return abstractF2mPoint;
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000E620C File Offset: 0x000E620C
		public static sbyte[] TauAdicWNaf(sbyte mu, ZTauElement lambda, sbyte width, BigInteger pow2w, BigInteger tw, ZTauElement[] alpha)
		{
			if (mu != 1 && mu != -1)
			{
				throw new ArgumentException("mu must be 1 or -1");
			}
			BigInteger bigInteger = Tnaf.Norm(mu, lambda);
			int bitLength = bigInteger.BitLength;
			int num = (bitLength > 30) ? (bitLength + 4 + (int)width) : ((int)(34 + width));
			sbyte[] array = new sbyte[num];
			BigInteger value = pow2w.ShiftRight(1);
			BigInteger bigInteger2 = lambda.u;
			BigInteger bigInteger3 = lambda.v;
			int num2 = 0;
			while (!bigInteger2.Equals(BigInteger.Zero) || !bigInteger3.Equals(BigInteger.Zero))
			{
				if (bigInteger2.TestBit(0))
				{
					BigInteger bigInteger4 = bigInteger2.Add(bigInteger3.Multiply(tw)).Mod(pow2w);
					sbyte b;
					if (bigInteger4.CompareTo(value) >= 0)
					{
						b = (sbyte)bigInteger4.Subtract(pow2w).IntValue;
					}
					else
					{
						b = (sbyte)bigInteger4.IntValue;
					}
					array[num2] = b;
					bool flag = true;
					if (b < 0)
					{
						flag = false;
						b = -b;
					}
					if (flag)
					{
						bigInteger2 = bigInteger2.Subtract(alpha[(int)b].u);
						bigInteger3 = bigInteger3.Subtract(alpha[(int)b].v);
					}
					else
					{
						bigInteger2 = bigInteger2.Add(alpha[(int)b].u);
						bigInteger3 = bigInteger3.Add(alpha[(int)b].v);
					}
				}
				else
				{
					array[num2] = 0;
				}
				BigInteger bigInteger5 = bigInteger2;
				if (mu == 1)
				{
					bigInteger2 = bigInteger3.Add(bigInteger2.ShiftRight(1));
				}
				else
				{
					bigInteger2 = bigInteger3.Subtract(bigInteger2.ShiftRight(1));
				}
				bigInteger3 = bigInteger5.ShiftRight(1).Negate();
				num2++;
			}
			return array;
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000E63D0 File Offset: 0x000E63D0
		public static AbstractF2mPoint[] GetPreComp(AbstractF2mPoint p, sbyte a)
		{
			sbyte[][] array = (a == 0) ? Tnaf.Alpha0Tnaf : Tnaf.Alpha1Tnaf;
			AbstractF2mPoint[] array2 = new AbstractF2mPoint[(uint)(array.Length + 1) >> 1];
			array2[0] = p;
			uint num = (uint)array.Length;
			for (uint num2 = 3U; num2 < num; num2 += 2U)
			{
				array2[(int)((UIntPtr)(num2 >> 1))] = Tnaf.MultiplyFromTnaf(p, array[(int)((UIntPtr)num2)]);
			}
			p.Curve.NormalizeAll(array2);
			return array2;
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000E6448 File Offset: 0x000E6448
		// Note: this type is marked as 'beforefieldinit'.
		static Tnaf()
		{
			ZTauElement[] array = new ZTauElement[9];
			array[1] = new ZTauElement(BigInteger.One, BigInteger.Zero);
			array[3] = new ZTauElement(Tnaf.MinusThree, Tnaf.MinusOne);
			array[5] = new ZTauElement(Tnaf.MinusOne, Tnaf.MinusOne);
			array[7] = new ZTauElement(BigInteger.One, Tnaf.MinusOne);
			Tnaf.Alpha0 = array;
			Tnaf.Alpha0Tnaf = new sbyte[][]
			{
				default(sbyte[]),
				new sbyte[]
				{
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					-1,
					0,
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					1,
					0,
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					-1,
					0,
					0,
					1
				}
			};
			array = new ZTauElement[9];
			array[1] = new ZTauElement(BigInteger.One, BigInteger.Zero);
			array[3] = new ZTauElement(Tnaf.MinusThree, BigInteger.One);
			array[5] = new ZTauElement(Tnaf.MinusOne, BigInteger.One);
			array[7] = new ZTauElement(BigInteger.One, BigInteger.One);
			Tnaf.Alpha1 = array;
			Tnaf.Alpha1Tnaf = new sbyte[][]
			{
				default(sbyte[]),
				new sbyte[]
				{
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					-1,
					0,
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					1,
					0,
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					-1,
					0,
					0,
					-1
				}
			};
		}

		// Token: 0x04001B42 RID: 6978
		public const sbyte Width = 4;

		// Token: 0x04001B43 RID: 6979
		public const sbyte Pow2Width = 16;

		// Token: 0x04001B44 RID: 6980
		private static readonly BigInteger MinusOne = BigInteger.One.Negate();

		// Token: 0x04001B45 RID: 6981
		private static readonly BigInteger MinusTwo = BigInteger.Two.Negate();

		// Token: 0x04001B46 RID: 6982
		private static readonly BigInteger MinusThree = BigInteger.Three.Negate();

		// Token: 0x04001B47 RID: 6983
		private static readonly BigInteger Four = BigInteger.ValueOf(4L);

		// Token: 0x04001B48 RID: 6984
		public static readonly ZTauElement[] Alpha0;

		// Token: 0x04001B49 RID: 6985
		public static readonly sbyte[][] Alpha0Tnaf;

		// Token: 0x04001B4A RID: 6986
		public static readonly ZTauElement[] Alpha1;

		// Token: 0x04001B4B RID: 6987
		public static readonly sbyte[][] Alpha1Tnaf;
	}
}
