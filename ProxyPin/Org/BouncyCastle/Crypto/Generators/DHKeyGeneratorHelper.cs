using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC.Multiplier;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003B8 RID: 952
	internal class DHKeyGeneratorHelper
	{
		// Token: 0x06001E54 RID: 7764 RVA: 0x000B17A0 File Offset: 0x000B17A0
		private DHKeyGeneratorHelper()
		{
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000B17A8 File Offset: 0x000B17A8
		internal BigInteger CalculatePrivate(DHParameters dhParams, SecureRandom random)
		{
			int l = dhParams.L;
			if (l != 0)
			{
				int num = l >> 2;
				BigInteger bigInteger;
				do
				{
					bigInteger = new BigInteger(l, random).SetBit(l - 1);
				}
				while (WNafUtilities.GetNafWeight(bigInteger) < num);
				return bigInteger;
			}
			BigInteger min = BigInteger.Two;
			int m = dhParams.M;
			if (m != 0)
			{
				min = BigInteger.One.ShiftLeft(m - 1);
			}
			BigInteger bigInteger2 = dhParams.Q;
			if (bigInteger2 == null)
			{
				bigInteger2 = dhParams.P;
			}
			BigInteger bigInteger3 = bigInteger2.Subtract(BigInteger.Two);
			int num2 = bigInteger3.BitLength >> 2;
			BigInteger bigInteger4;
			do
			{
				bigInteger4 = BigIntegers.CreateRandomInRange(min, bigInteger3, random);
			}
			while (WNafUtilities.GetNafWeight(bigInteger4) < num2);
			return bigInteger4;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000B1854 File Offset: 0x000B1854
		internal BigInteger CalculatePublic(DHParameters dhParams, BigInteger x)
		{
			return dhParams.G.ModPow(x, dhParams.P);
		}

		// Token: 0x04001419 RID: 5145
		internal static readonly DHKeyGeneratorHelper Instance = new DHKeyGeneratorHelper();
	}
}
