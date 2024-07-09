using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200039E RID: 926
	public class RsaBlindingEngine : IAsymmetricBlockCipher
	{
		// Token: 0x06001D65 RID: 7525 RVA: 0x000A7EDC File Offset: 0x000A7EDC
		public RsaBlindingEngine() : this(new RsaCoreEngine())
		{
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000A7EEC File Offset: 0x000A7EEC
		public RsaBlindingEngine(IRsa rsa)
		{
			this.core = rsa;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x000A7EFC File Offset: 0x000A7EFC
		public virtual string AlgorithmName
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000A7F04 File Offset: 0x000A7F04
		public virtual void Init(bool forEncryption, ICipherParameters param)
		{
			RsaBlindingParameters rsaBlindingParameters;
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				rsaBlindingParameters = (RsaBlindingParameters)parametersWithRandom.Parameters;
			}
			else
			{
				rsaBlindingParameters = (RsaBlindingParameters)param;
			}
			this.core.Init(forEncryption, rsaBlindingParameters.PublicKey);
			this.forEncryption = forEncryption;
			this.key = rsaBlindingParameters.PublicKey;
			this.blindingFactor = rsaBlindingParameters.BlindingFactor;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000A7F70 File Offset: 0x000A7F70
		public virtual int GetInputBlockSize()
		{
			return this.core.GetInputBlockSize();
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000A7F80 File Offset: 0x000A7F80
		public virtual int GetOutputBlockSize()
		{
			return this.core.GetOutputBlockSize();
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x000A7F90 File Offset: 0x000A7F90
		public virtual byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen)
		{
			BigInteger bigInteger = this.core.ConvertInput(inBuf, inOff, inLen);
			if (this.forEncryption)
			{
				bigInteger = this.BlindMessage(bigInteger);
			}
			else
			{
				bigInteger = this.UnblindMessage(bigInteger);
			}
			return this.core.ConvertOutput(bigInteger);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x000A7FDC File Offset: 0x000A7FDC
		private BigInteger BlindMessage(BigInteger msg)
		{
			BigInteger bigInteger = this.blindingFactor;
			bigInteger = msg.Multiply(bigInteger.ModPow(this.key.Exponent, this.key.Modulus));
			return bigInteger.Mod(this.key.Modulus);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x000A802C File Offset: 0x000A802C
		private BigInteger UnblindMessage(BigInteger blindedMsg)
		{
			BigInteger modulus = this.key.Modulus;
			BigInteger val = this.blindingFactor.ModInverse(modulus);
			BigInteger bigInteger = blindedMsg.Multiply(val);
			return bigInteger.Mod(modulus);
		}

		// Token: 0x0400137A RID: 4986
		private readonly IRsa core;

		// Token: 0x0400137B RID: 4987
		private RsaKeyParameters key;

		// Token: 0x0400137C RID: 4988
		private BigInteger blindingFactor;

		// Token: 0x0400137D RID: 4989
		private bool forEncryption;
	}
}
