using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200039D RID: 925
	public class RsaBlindedEngine : IAsymmetricBlockCipher
	{
		// Token: 0x06001D5E RID: 7518 RVA: 0x000A7CE8 File Offset: 0x000A7CE8
		public RsaBlindedEngine() : this(new RsaCoreEngine())
		{
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000A7CF8 File Offset: 0x000A7CF8
		public RsaBlindedEngine(IRsa rsa)
		{
			this.core = rsa;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x000A7D08 File Offset: 0x000A7D08
		public virtual string AlgorithmName
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000A7D10 File Offset: 0x000A7D10
		public virtual void Init(bool forEncryption, ICipherParameters param)
		{
			this.core.Init(forEncryption, param);
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.key = (RsaKeyParameters)parametersWithRandom.Parameters;
				if (this.key is RsaPrivateCrtKeyParameters)
				{
					this.random = parametersWithRandom.Random;
					return;
				}
				this.random = null;
				return;
			}
			else
			{
				this.key = (RsaKeyParameters)param;
				if (this.key is RsaPrivateCrtKeyParameters)
				{
					this.random = new SecureRandom();
					return;
				}
				this.random = null;
				return;
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000A7DA8 File Offset: 0x000A7DA8
		public virtual int GetInputBlockSize()
		{
			return this.core.GetInputBlockSize();
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000A7DB8 File Offset: 0x000A7DB8
		public virtual int GetOutputBlockSize()
		{
			return this.core.GetOutputBlockSize();
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x000A7DC8 File Offset: 0x000A7DC8
		public virtual byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("RSA engine not initialised");
			}
			BigInteger bigInteger = this.core.ConvertInput(inBuf, inOff, inLen);
			BigInteger bigInteger4;
			if (this.key is RsaPrivateCrtKeyParameters)
			{
				RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)this.key;
				BigInteger publicExponent = rsaPrivateCrtKeyParameters.PublicExponent;
				if (publicExponent != null)
				{
					BigInteger modulus = rsaPrivateCrtKeyParameters.Modulus;
					BigInteger bigInteger2 = BigIntegers.CreateRandomInRange(BigInteger.One, modulus.Subtract(BigInteger.One), this.random);
					BigInteger input = bigInteger2.ModPow(publicExponent, modulus).Multiply(bigInteger).Mod(modulus);
					BigInteger bigInteger3 = this.core.ProcessBlock(input);
					BigInteger val = bigInteger2.ModInverse(modulus);
					bigInteger4 = bigInteger3.Multiply(val).Mod(modulus);
					if (!bigInteger.Equals(bigInteger4.ModPow(publicExponent, modulus)))
					{
						throw new InvalidOperationException("RSA engine faulty decryption/signing detected");
					}
				}
				else
				{
					bigInteger4 = this.core.ProcessBlock(bigInteger);
				}
			}
			else
			{
				bigInteger4 = this.core.ProcessBlock(bigInteger);
			}
			return this.core.ConvertOutput(bigInteger4);
		}

		// Token: 0x04001377 RID: 4983
		private readonly IRsa core;

		// Token: 0x04001378 RID: 4984
		private RsaKeyParameters key;

		// Token: 0x04001379 RID: 4985
		private SecureRandom random;
	}
}
