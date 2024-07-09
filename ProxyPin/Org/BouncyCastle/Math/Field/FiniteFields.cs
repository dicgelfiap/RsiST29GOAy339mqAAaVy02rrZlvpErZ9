﻿using System;

namespace Org.BouncyCastle.Math.Field
{
	// Token: 0x02000619 RID: 1561
	public abstract class FiniteFields
	{
		// Token: 0x06003520 RID: 13600 RVA: 0x00118108 File Offset: 0x00118108
		public static IPolynomialExtensionField GetBinaryExtensionField(int[] exponents)
		{
			if (exponents[0] != 0)
			{
				throw new ArgumentException("Irreducible polynomials in GF(2) must have constant term", "exponents");
			}
			for (int i = 1; i < exponents.Length; i++)
			{
				if (exponents[i] <= exponents[i - 1])
				{
					throw new ArgumentException("Polynomial exponents must be montonically increasing", "exponents");
				}
			}
			return new GenericPolynomialExtensionField(FiniteFields.GF_2, new GF2Polynomial(exponents));
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x00118170 File Offset: 0x00118170
		public static IFiniteField GetPrimeField(BigInteger characteristic)
		{
			int bitLength = characteristic.BitLength;
			if (characteristic.SignValue <= 0 || bitLength < 2)
			{
				throw new ArgumentException("Must be >= 2", "characteristic");
			}
			if (bitLength < 3)
			{
				switch (characteristic.IntValue)
				{
				case 2:
					return FiniteFields.GF_2;
				case 3:
					return FiniteFields.GF_3;
				}
			}
			return new PrimeField(characteristic);
		}

		// Token: 0x04001D17 RID: 7447
		internal static readonly IFiniteField GF_2 = new PrimeField(BigInteger.ValueOf(2L));

		// Token: 0x04001D18 RID: 7448
		internal static readonly IFiniteField GF_3 = new PrimeField(BigInteger.ValueOf(3L));
	}
}
