using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000471 RID: 1137
	public class RsaKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002348 RID: 9032 RVA: 0x000C6670 File Offset: 0x000C6670
		private static BigInteger Validate(BigInteger modulus)
		{
			if ((modulus.IntValue & 1) == 0)
			{
				throw new ArgumentException("RSA modulus is even", "modulus");
			}
			if (!modulus.Gcd(RsaKeyParameters.SmallPrimesProduct).Equals(BigInteger.One))
			{
				throw new ArgumentException("RSA modulus has a small prime factor");
			}
			return modulus;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000C66C4 File Offset: 0x000C66C4
		public RsaKeyParameters(bool isPrivate, BigInteger modulus, BigInteger exponent) : base(isPrivate)
		{
			if (modulus == null)
			{
				throw new ArgumentNullException("modulus");
			}
			if (exponent == null)
			{
				throw new ArgumentNullException("exponent");
			}
			if (modulus.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA modulus", "modulus");
			}
			if (exponent.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA exponent", "exponent");
			}
			if (!isPrivate && (exponent.IntValue & 1) == 0)
			{
				throw new ArgumentException("RSA publicExponent is even", "exponent");
			}
			this.modulus = RsaKeyParameters.Validate(modulus);
			this.exponent = exponent;
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x000C676C File Offset: 0x000C676C
		public BigInteger Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x000C6774 File Offset: 0x000C6774
		public BigInteger Exponent
		{
			get
			{
				return this.exponent;
			}
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000C677C File Offset: 0x000C677C
		public override bool Equals(object obj)
		{
			RsaKeyParameters rsaKeyParameters = obj as RsaKeyParameters;
			return rsaKeyParameters != null && (rsaKeyParameters.IsPrivate == base.IsPrivate && rsaKeyParameters.Modulus.Equals(this.modulus)) && rsaKeyParameters.Exponent.Equals(this.exponent);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000C67D8 File Offset: 0x000C67D8
		public override int GetHashCode()
		{
			return this.modulus.GetHashCode() ^ this.exponent.GetHashCode() ^ base.IsPrivate.GetHashCode();
		}

		// Token: 0x0400166A RID: 5738
		private static readonly BigInteger SmallPrimesProduct = new BigInteger("8138e8a0fcf3a4e84a771d40fd305d7f4aa59306d7251de54d98af8fe95729a1f73d893fa424cd2edc8636a6c3285e022b0e3866a565ae8108eed8591cd4fe8d2ce86165a978d719ebf647f362d33fca29cd179fb42401cbaf3df0c614056f9c8f3cfd51e474afb6bc6974f78db8aba8e9e517fded658591ab7502bd41849462f", 16);

		// Token: 0x0400166B RID: 5739
		private readonly BigInteger modulus;

		// Token: 0x0400166C RID: 5740
		private readonly BigInteger exponent;
	}
}
