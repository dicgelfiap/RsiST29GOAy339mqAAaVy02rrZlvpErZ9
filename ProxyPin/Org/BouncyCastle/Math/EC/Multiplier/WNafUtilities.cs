using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000600 RID: 1536
	public abstract class WNafUtilities
	{
		// Token: 0x0600334E RID: 13134 RVA: 0x001091BC File Offset: 0x001091BC
		public static void ConfigureBasepoint(ECPoint p)
		{
			ECCurve curve = p.Curve;
			if (curve == null)
			{
				return;
			}
			BigInteger order = curve.Order;
			int bits = (order == null) ? (curve.FieldSize + 1) : order.BitLength;
			int confWidth = Math.Min(WNafUtilities.MAX_WIDTH, WNafUtilities.GetWindowSize(bits) + 3);
			curve.Precompute(p, WNafUtilities.PRECOMP_NAME, new WNafUtilities.ConfigureBasepointCallback(curve, confWidth));
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x00109224 File Offset: 0x00109224
		public static int[] GenerateCompactNaf(BigInteger k)
		{
			if (k.BitLength >> 16 != 0)
			{
				throw new ArgumentException("must have bitlength < 2^16", "k");
			}
			if (k.SignValue == 0)
			{
				return Arrays.EmptyInts;
			}
			BigInteger bigInteger = k.ShiftLeft(1).Add(k);
			int bitLength = bigInteger.BitLength;
			int[] array = new int[bitLength >> 1];
			BigInteger bigInteger2 = bigInteger.Xor(k);
			int num = bitLength - 1;
			int num2 = 0;
			int num3 = 0;
			for (int i = 1; i < num; i++)
			{
				if (!bigInteger2.TestBit(i))
				{
					num3++;
				}
				else
				{
					int num4 = k.TestBit(i) ? -1 : 1;
					array[num2++] = (num4 << 16 | num3);
					num3 = 1;
					i++;
				}
			}
			array[num2++] = (65536 | num3);
			if (array.Length > num2)
			{
				array = WNafUtilities.Trim(array, num2);
			}
			return array;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x00109318 File Offset: 0x00109318
		public static int[] GenerateCompactWindowNaf(int width, BigInteger k)
		{
			if (width == 2)
			{
				return WNafUtilities.GenerateCompactNaf(k);
			}
			if (width < 2 || width > 16)
			{
				throw new ArgumentException("must be in the range [2, 16]", "width");
			}
			if (k.BitLength >> 16 != 0)
			{
				throw new ArgumentException("must have bitlength < 2^16", "k");
			}
			if (k.SignValue == 0)
			{
				return Arrays.EmptyInts;
			}
			int[] array = new int[k.BitLength / width + 1];
			int num = 1 << width;
			int num2 = num - 1;
			int num3 = num >> 1;
			bool flag = false;
			int num4 = 0;
			int i = 0;
			while (i <= k.BitLength)
			{
				if (k.TestBit(i) == flag)
				{
					i++;
				}
				else
				{
					k = k.ShiftRight(i);
					int num5 = k.IntValue & num2;
					if (flag)
					{
						num5++;
					}
					flag = ((num5 & num3) != 0);
					if (flag)
					{
						num5 -= num;
					}
					int num6 = (num4 > 0) ? (i - 1) : i;
					array[num4++] = (num5 << 16 | num6);
					i = width;
				}
			}
			if (array.Length > num4)
			{
				array = WNafUtilities.Trim(array, num4);
			}
			return array;
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x00109450 File Offset: 0x00109450
		public static byte[] GenerateJsf(BigInteger g, BigInteger h)
		{
			int num = Math.Max(g.BitLength, h.BitLength) + 1;
			byte[] array = new byte[num];
			BigInteger bigInteger = g;
			BigInteger bigInteger2 = h;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			while ((num3 | num4) != 0 || bigInteger.BitLength > num5 || bigInteger2.BitLength > num5)
			{
				int num6 = (int)(((uint)bigInteger.IntValue >> num5) + (uint)num3 & 7U);
				int num7 = (int)(((uint)bigInteger2.IntValue >> num5) + (uint)num4 & 7U);
				int num8 = num6 & 1;
				if (num8 != 0)
				{
					num8 -= (num6 & 2);
					if (num6 + num8 == 4 && (num7 & 3) == 2)
					{
						num8 = -num8;
					}
				}
				int num9 = num7 & 1;
				if (num9 != 0)
				{
					num9 -= (num7 & 2);
					if (num7 + num9 == 4 && (num6 & 3) == 2)
					{
						num9 = -num9;
					}
				}
				if (num3 << 1 == 1 + num8)
				{
					num3 ^= 1;
				}
				if (num4 << 1 == 1 + num9)
				{
					num4 ^= 1;
				}
				if (++num5 == 30)
				{
					num5 = 0;
					bigInteger = bigInteger.ShiftRight(30);
					bigInteger2 = bigInteger2.ShiftRight(30);
				}
				array[num2++] = (byte)(num8 << 4 | (num9 & 15));
			}
			if (array.Length > num2)
			{
				array = WNafUtilities.Trim(array, num2);
			}
			return array;
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x001095AC File Offset: 0x001095AC
		public static byte[] GenerateNaf(BigInteger k)
		{
			if (k.SignValue == 0)
			{
				return Arrays.EmptyBytes;
			}
			BigInteger bigInteger = k.ShiftLeft(1).Add(k);
			int num = bigInteger.BitLength - 1;
			byte[] array = new byte[num];
			BigInteger bigInteger2 = bigInteger.Xor(k);
			for (int i = 1; i < num; i++)
			{
				if (bigInteger2.TestBit(i))
				{
					array[i - 1] = (byte)(k.TestBit(i) ? -1 : 1);
					i++;
				}
			}
			array[num - 1] = 1;
			return array;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x0010963C File Offset: 0x0010963C
		public static byte[] GenerateWindowNaf(int width, BigInteger k)
		{
			if (width == 2)
			{
				return WNafUtilities.GenerateNaf(k);
			}
			if (width < 2 || width > 8)
			{
				throw new ArgumentException("must be in the range [2, 8]", "width");
			}
			if (k.SignValue == 0)
			{
				return Arrays.EmptyBytes;
			}
			byte[] array = new byte[k.BitLength + 1];
			int num = 1 << width;
			int num2 = num - 1;
			int num3 = num >> 1;
			bool flag = false;
			int num4 = 0;
			int i = 0;
			while (i <= k.BitLength)
			{
				if (k.TestBit(i) == flag)
				{
					i++;
				}
				else
				{
					k = k.ShiftRight(i);
					int num5 = k.IntValue & num2;
					if (flag)
					{
						num5++;
					}
					flag = ((num5 & num3) != 0);
					if (flag)
					{
						num5 -= num;
					}
					num4 += ((num4 > 0) ? (i - 1) : i);
					array[num4++] = (byte)num5;
					i = width;
				}
			}
			if (array.Length > num4)
			{
				array = WNafUtilities.Trim(array, num4);
			}
			return array;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x00109754 File Offset: 0x00109754
		public static int GetNafWeight(BigInteger k)
		{
			if (k.SignValue == 0)
			{
				return 0;
			}
			BigInteger bigInteger = k.ShiftLeft(1).Add(k);
			BigInteger bigInteger2 = bigInteger.Xor(k);
			return bigInteger2.BitCount;
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x00109790 File Offset: 0x00109790
		public static WNafPreCompInfo GetWNafPreCompInfo(ECPoint p)
		{
			return WNafUtilities.GetWNafPreCompInfo(p.Curve.GetPreCompInfo(p, WNafUtilities.PRECOMP_NAME));
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x001097A8 File Offset: 0x001097A8
		public static WNafPreCompInfo GetWNafPreCompInfo(PreCompInfo preCompInfo)
		{
			return preCompInfo as WNafPreCompInfo;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x001097B0 File Offset: 0x001097B0
		public static int GetWindowSize(int bits)
		{
			return WNafUtilities.GetWindowSize(bits, WNafUtilities.DEFAULT_WINDOW_SIZE_CUTOFFS, WNafUtilities.MAX_WIDTH);
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x001097C4 File Offset: 0x001097C4
		public static int GetWindowSize(int bits, int maxWidth)
		{
			return WNafUtilities.GetWindowSize(bits, WNafUtilities.DEFAULT_WINDOW_SIZE_CUTOFFS, maxWidth);
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x001097D4 File Offset: 0x001097D4
		public static int GetWindowSize(int bits, int[] windowSizeCutoffs)
		{
			return WNafUtilities.GetWindowSize(bits, windowSizeCutoffs, WNafUtilities.MAX_WIDTH);
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x001097E4 File Offset: 0x001097E4
		public static int GetWindowSize(int bits, int[] windowSizeCutoffs, int maxWidth)
		{
			int num = 0;
			while (num < windowSizeCutoffs.Length && bits >= windowSizeCutoffs[num])
			{
				num++;
			}
			return Math.Max(2, Math.Min(maxWidth, num + 2));
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x00109820 File Offset: 0x00109820
		[Obsolete]
		public static ECPoint MapPointWithPrecomp(ECPoint p, int minWidth, bool includeNegated, ECPointMap pointMap)
		{
			ECCurve curve = p.Curve;
			WNafPreCompInfo infoP = WNafUtilities.Precompute(p, minWidth, includeNegated);
			ECPoint ecpoint = pointMap.Map(p);
			curve.Precompute(ecpoint, WNafUtilities.PRECOMP_NAME, new WNafUtilities.MapPointCallback(infoP, includeNegated, pointMap));
			return ecpoint;
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x00109860 File Offset: 0x00109860
		public static WNafPreCompInfo Precompute(ECPoint p, int minWidth, bool includeNegated)
		{
			return (WNafPreCompInfo)p.Curve.Precompute(p, WNafUtilities.PRECOMP_NAME, new WNafUtilities.PrecomputeCallback(p, minWidth, includeNegated));
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x00109890 File Offset: 0x00109890
		public static WNafPreCompInfo PrecomputeWithPointMap(ECPoint p, ECPointMap pointMap, WNafPreCompInfo fromWNaf, bool includeNegated)
		{
			return (WNafPreCompInfo)p.Curve.Precompute(p, WNafUtilities.PRECOMP_NAME, new WNafUtilities.PrecomputeWithPointMapCallback(p, pointMap, fromWNaf, includeNegated));
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x001098C0 File Offset: 0x001098C0
		private static byte[] Trim(byte[] a, int length)
		{
			byte[] array = new byte[length];
			Array.Copy(a, 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x001098E8 File Offset: 0x001098E8
		private static int[] Trim(int[] a, int length)
		{
			int[] array = new int[length];
			Array.Copy(a, 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x00109910 File Offset: 0x00109910
		private static ECPoint[] ResizeTable(ECPoint[] a, int length)
		{
			ECPoint[] array = new ECPoint[length];
			Array.Copy(a, 0, array, 0, a.Length);
			return array;
		}

		// Token: 0x04001C99 RID: 7321
		public static readonly string PRECOMP_NAME = "bc_wnaf";

		// Token: 0x04001C9A RID: 7322
		private static readonly int[] DEFAULT_WINDOW_SIZE_CUTOFFS = new int[]
		{
			13,
			41,
			121,
			337,
			897,
			2305
		};

		// Token: 0x04001C9B RID: 7323
		private static readonly int MAX_WIDTH = 16;

		// Token: 0x04001C9C RID: 7324
		private static readonly ECPoint[] EMPTY_POINTS = new ECPoint[0];

		// Token: 0x02000E4C RID: 3660
		private class ConfigureBasepointCallback : IPreCompCallback
		{
			// Token: 0x06008D1F RID: 36127 RVA: 0x002A5470 File Offset: 0x002A5470
			internal ConfigureBasepointCallback(ECCurve curve, int confWidth)
			{
				this.m_curve = curve;
				this.m_confWidth = confWidth;
			}

			// Token: 0x06008D20 RID: 36128 RVA: 0x002A5488 File Offset: 0x002A5488
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				WNafPreCompInfo wnafPreCompInfo = existing as WNafPreCompInfo;
				if (wnafPreCompInfo != null && wnafPreCompInfo.ConfWidth == this.m_confWidth)
				{
					wnafPreCompInfo.PromotionCountdown = 0;
					return wnafPreCompInfo;
				}
				WNafPreCompInfo wnafPreCompInfo2 = new WNafPreCompInfo();
				wnafPreCompInfo2.PromotionCountdown = 0;
				wnafPreCompInfo2.ConfWidth = this.m_confWidth;
				if (wnafPreCompInfo != null)
				{
					wnafPreCompInfo2.PreComp = wnafPreCompInfo.PreComp;
					wnafPreCompInfo2.PreCompNeg = wnafPreCompInfo.PreCompNeg;
					wnafPreCompInfo2.Twice = wnafPreCompInfo.Twice;
					wnafPreCompInfo2.Width = wnafPreCompInfo.Width;
				}
				return wnafPreCompInfo2;
			}

			// Token: 0x040041FF RID: 16895
			private readonly ECCurve m_curve;

			// Token: 0x04004200 RID: 16896
			private readonly int m_confWidth;
		}

		// Token: 0x02000E4D RID: 3661
		private class MapPointCallback : IPreCompCallback
		{
			// Token: 0x06008D21 RID: 36129 RVA: 0x002A5510 File Offset: 0x002A5510
			internal MapPointCallback(WNafPreCompInfo infoP, bool includeNegated, ECPointMap pointMap)
			{
				this.m_infoP = infoP;
				this.m_includeNegated = includeNegated;
				this.m_pointMap = pointMap;
			}

			// Token: 0x06008D22 RID: 36130 RVA: 0x002A5530 File Offset: 0x002A5530
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				WNafPreCompInfo wnafPreCompInfo = new WNafPreCompInfo();
				wnafPreCompInfo.ConfWidth = this.m_infoP.ConfWidth;
				ECPoint twice = this.m_infoP.Twice;
				if (twice != null)
				{
					ECPoint twice2 = this.m_pointMap.Map(twice);
					wnafPreCompInfo.Twice = twice2;
				}
				ECPoint[] preComp = this.m_infoP.PreComp;
				ECPoint[] array = new ECPoint[preComp.Length];
				for (int i = 0; i < preComp.Length; i++)
				{
					array[i] = this.m_pointMap.Map(preComp[i]);
				}
				wnafPreCompInfo.PreComp = array;
				wnafPreCompInfo.Width = this.m_infoP.Width;
				if (this.m_includeNegated)
				{
					ECPoint[] array2 = new ECPoint[array.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = array[j].Negate();
					}
					wnafPreCompInfo.PreCompNeg = array2;
				}
				return wnafPreCompInfo;
			}

			// Token: 0x04004201 RID: 16897
			private readonly WNafPreCompInfo m_infoP;

			// Token: 0x04004202 RID: 16898
			private readonly bool m_includeNegated;

			// Token: 0x04004203 RID: 16899
			private readonly ECPointMap m_pointMap;
		}

		// Token: 0x02000E4E RID: 3662
		private class PrecomputeCallback : IPreCompCallback
		{
			// Token: 0x06008D23 RID: 36131 RVA: 0x002A562C File Offset: 0x002A562C
			internal PrecomputeCallback(ECPoint p, int minWidth, bool includeNegated)
			{
				this.m_p = p;
				this.m_minWidth = minWidth;
				this.m_includeNegated = includeNegated;
			}

			// Token: 0x06008D24 RID: 36132 RVA: 0x002A564C File Offset: 0x002A564C
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				WNafPreCompInfo wnafPreCompInfo = existing as WNafPreCompInfo;
				int num = Math.Max(2, Math.Min(WNafUtilities.MAX_WIDTH, this.m_minWidth));
				int num2 = 1 << num - 2;
				if (this.CheckExisting(wnafPreCompInfo, num, num2, this.m_includeNegated))
				{
					wnafPreCompInfo.DecrementPromotionCountdown();
					return wnafPreCompInfo;
				}
				WNafPreCompInfo wnafPreCompInfo2 = new WNafPreCompInfo();
				ECCurve curve = this.m_p.Curve;
				ECPoint[] array = null;
				ECPoint[] array2 = null;
				ECPoint ecpoint = null;
				if (wnafPreCompInfo != null)
				{
					int promotionCountdown = wnafPreCompInfo.DecrementPromotionCountdown();
					wnafPreCompInfo2.PromotionCountdown = promotionCountdown;
					int confWidth = wnafPreCompInfo.ConfWidth;
					wnafPreCompInfo2.ConfWidth = confWidth;
					array = wnafPreCompInfo.PreComp;
					array2 = wnafPreCompInfo.PreCompNeg;
					ecpoint = wnafPreCompInfo.Twice;
				}
				num = Math.Min(WNafUtilities.MAX_WIDTH, Math.Max(wnafPreCompInfo2.ConfWidth, num));
				num2 = 1 << num - 2;
				int num3 = 0;
				if (array == null)
				{
					array = WNafUtilities.EMPTY_POINTS;
				}
				else
				{
					num3 = array.Length;
				}
				if (num3 < num2)
				{
					array = WNafUtilities.ResizeTable(array, num2);
					if (num2 == 1)
					{
						array[0] = this.m_p.Normalize();
					}
					else
					{
						int i = num3;
						if (i == 0)
						{
							array[0] = this.m_p;
							i = 1;
						}
						ECFieldElement ecfieldElement = null;
						if (num2 == 2)
						{
							array[1] = this.m_p.ThreeTimes();
						}
						else
						{
							ECPoint ecpoint2 = ecpoint;
							ECPoint ecpoint3 = array[i - 1];
							if (ecpoint2 == null)
							{
								ecpoint2 = array[0].Twice();
								ecpoint = ecpoint2;
								if (!ecpoint.IsInfinity && ECAlgorithms.IsFpCurve(curve) && curve.FieldSize >= 64)
								{
									switch (curve.CoordinateSystem)
									{
									case 2:
									case 3:
									case 4:
									{
										ecfieldElement = ecpoint.GetZCoord(0);
										ecpoint2 = curve.CreatePoint(ecpoint.XCoord.ToBigInteger(), ecpoint.YCoord.ToBigInteger());
										ECFieldElement ecfieldElement2 = ecfieldElement.Square();
										ECFieldElement scale = ecfieldElement2.Multiply(ecfieldElement);
										ecpoint3 = ecpoint3.ScaleX(ecfieldElement2).ScaleY(scale);
										if (num3 == 0)
										{
											array[0] = ecpoint3;
										}
										break;
									}
									}
								}
							}
							while (i < num2)
							{
								ecpoint3 = (array[i++] = ecpoint3.Add(ecpoint2));
							}
						}
						curve.NormalizeAll(array, num3, num2 - num3, ecfieldElement);
					}
				}
				if (this.m_includeNegated)
				{
					int j;
					if (array2 == null)
					{
						j = 0;
						array2 = new ECPoint[num2];
					}
					else
					{
						j = array2.Length;
						if (j < num2)
						{
							array2 = WNafUtilities.ResizeTable(array2, num2);
						}
					}
					while (j < num2)
					{
						array2[j] = array[j].Negate();
						j++;
					}
				}
				wnafPreCompInfo2.PreComp = array;
				wnafPreCompInfo2.PreCompNeg = array2;
				wnafPreCompInfo2.Twice = ecpoint;
				wnafPreCompInfo2.Width = num;
				return wnafPreCompInfo2;
			}

			// Token: 0x06008D25 RID: 36133 RVA: 0x002A5930 File Offset: 0x002A5930
			private bool CheckExisting(WNafPreCompInfo existingWNaf, int width, int reqPreCompLen, bool includeNegated)
			{
				return existingWNaf != null && existingWNaf.Width >= Math.Max(existingWNaf.ConfWidth, width) && this.CheckTable(existingWNaf.PreComp, reqPreCompLen) && (!includeNegated || this.CheckTable(existingWNaf.PreCompNeg, reqPreCompLen));
			}

			// Token: 0x06008D26 RID: 36134 RVA: 0x002A5988 File Offset: 0x002A5988
			private bool CheckTable(ECPoint[] table, int reqLen)
			{
				return table != null && table.Length >= reqLen;
			}

			// Token: 0x04004204 RID: 16900
			private readonly ECPoint m_p;

			// Token: 0x04004205 RID: 16901
			private readonly int m_minWidth;

			// Token: 0x04004206 RID: 16902
			private readonly bool m_includeNegated;
		}

		// Token: 0x02000E4F RID: 3663
		private class PrecomputeWithPointMapCallback : IPreCompCallback
		{
			// Token: 0x06008D27 RID: 36135 RVA: 0x002A599C File Offset: 0x002A599C
			internal PrecomputeWithPointMapCallback(ECPoint point, ECPointMap pointMap, WNafPreCompInfo fromWNaf, bool includeNegated)
			{
				this.m_point = point;
				this.m_pointMap = pointMap;
				this.m_fromWNaf = fromWNaf;
				this.m_includeNegated = includeNegated;
			}

			// Token: 0x06008D28 RID: 36136 RVA: 0x002A59C4 File Offset: 0x002A59C4
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				WNafPreCompInfo wnafPreCompInfo = existing as WNafPreCompInfo;
				int width = this.m_fromWNaf.Width;
				int reqPreCompLen = this.m_fromWNaf.PreComp.Length;
				if (this.CheckExisting(wnafPreCompInfo, width, reqPreCompLen, this.m_includeNegated))
				{
					wnafPreCompInfo.DecrementPromotionCountdown();
					return wnafPreCompInfo;
				}
				WNafPreCompInfo wnafPreCompInfo2 = new WNafPreCompInfo();
				wnafPreCompInfo2.PromotionCountdown = this.m_fromWNaf.PromotionCountdown;
				ECPoint twice = this.m_fromWNaf.Twice;
				if (twice != null)
				{
					ECPoint twice2 = this.m_pointMap.Map(twice);
					wnafPreCompInfo2.Twice = twice2;
				}
				ECPoint[] preComp = this.m_fromWNaf.PreComp;
				ECPoint[] array = new ECPoint[preComp.Length];
				for (int i = 0; i < preComp.Length; i++)
				{
					array[i] = this.m_pointMap.Map(preComp[i]);
				}
				wnafPreCompInfo2.PreComp = array;
				wnafPreCompInfo2.Width = width;
				if (this.m_includeNegated)
				{
					ECPoint[] array2 = new ECPoint[array.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = array[j].Negate();
					}
					wnafPreCompInfo2.PreCompNeg = array2;
				}
				return wnafPreCompInfo2;
			}

			// Token: 0x06008D29 RID: 36137 RVA: 0x002A5AFC File Offset: 0x002A5AFC
			private bool CheckExisting(WNafPreCompInfo existingWNaf, int width, int reqPreCompLen, bool includeNegated)
			{
				return existingWNaf != null && existingWNaf.Width >= width && this.CheckTable(existingWNaf.PreComp, reqPreCompLen) && (!includeNegated || this.CheckTable(existingWNaf.PreCompNeg, reqPreCompLen));
			}

			// Token: 0x06008D2A RID: 36138 RVA: 0x002A5B4C File Offset: 0x002A5B4C
			private bool CheckTable(ECPoint[] table, int reqLen)
			{
				return table != null && table.Length >= reqLen;
			}

			// Token: 0x04004207 RID: 16903
			private readonly ECPoint m_point;

			// Token: 0x04004208 RID: 16904
			private readonly ECPointMap m_pointMap;

			// Token: 0x04004209 RID: 16905
			private readonly WNafPreCompInfo m_fromWNaf;

			// Token: 0x0400420A RID: 16906
			private readonly bool m_includeNegated;
		}
	}
}
