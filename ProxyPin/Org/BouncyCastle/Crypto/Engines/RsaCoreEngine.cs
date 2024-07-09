using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003A0 RID: 928
	public class RsaCoreEngine : IRsa
	{
		// Token: 0x06001D74 RID: 7540 RVA: 0x000A806C File Offset: 0x000A806C
		private void CheckInitialised()
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("RSA engine not initialised");
			}
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x000A8084 File Offset: 0x000A8084
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			if (!(parameters is RsaKeyParameters))
			{
				throw new InvalidKeyException("Not an RSA key");
			}
			this.key = (RsaKeyParameters)parameters;
			this.forEncryption = forEncryption;
			this.bitSize = this.key.Modulus.BitLength;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x000A80EC File Offset: 0x000A80EC
		public virtual int GetInputBlockSize()
		{
			this.CheckInitialised();
			if (this.forEncryption)
			{
				return (this.bitSize - 1) / 8;
			}
			return (this.bitSize + 7) / 8;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x000A8114 File Offset: 0x000A8114
		public virtual int GetOutputBlockSize()
		{
			this.CheckInitialised();
			if (this.forEncryption)
			{
				return (this.bitSize + 7) / 8;
			}
			return (this.bitSize - 1) / 8;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x000A813C File Offset: 0x000A813C
		public virtual BigInteger ConvertInput(byte[] inBuf, int inOff, int inLen)
		{
			this.CheckInitialised();
			int num = (this.bitSize + 7) / 8;
			if (inLen > num)
			{
				throw new DataLengthException("input too large for RSA cipher.");
			}
			BigInteger bigInteger = new BigInteger(1, inBuf, inOff, inLen);
			if (bigInteger.CompareTo(this.key.Modulus) >= 0)
			{
				throw new DataLengthException("input too large for RSA cipher.");
			}
			return bigInteger;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x000A81A0 File Offset: 0x000A81A0
		public virtual byte[] ConvertOutput(BigInteger result)
		{
			this.CheckInitialised();
			byte[] array = result.ToByteArrayUnsigned();
			if (this.forEncryption)
			{
				int outputBlockSize = this.GetOutputBlockSize();
				if (array.Length < outputBlockSize)
				{
					byte[] array2 = new byte[outputBlockSize];
					array.CopyTo(array2, array2.Length - array.Length);
					array = array2;
				}
			}
			return array;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x000A81F4 File Offset: 0x000A81F4
		public virtual BigInteger ProcessBlock(BigInteger input)
		{
			this.CheckInitialised();
			if (this.key is RsaPrivateCrtKeyParameters)
			{
				RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)this.key;
				BigInteger p = rsaPrivateCrtKeyParameters.P;
				BigInteger q = rsaPrivateCrtKeyParameters.Q;
				BigInteger dp = rsaPrivateCrtKeyParameters.DP;
				BigInteger dq = rsaPrivateCrtKeyParameters.DQ;
				BigInteger qinv = rsaPrivateCrtKeyParameters.QInv;
				BigInteger bigInteger = input.Remainder(p).ModPow(dp, p);
				BigInteger bigInteger2 = input.Remainder(q).ModPow(dq, q);
				BigInteger bigInteger3 = bigInteger.Subtract(bigInteger2);
				bigInteger3 = bigInteger3.Multiply(qinv);
				bigInteger3 = bigInteger3.Mod(p);
				BigInteger bigInteger4 = bigInteger3.Multiply(q);
				return bigInteger4.Add(bigInteger2);
			}
			return input.ModPow(this.key.Exponent, this.key.Modulus);
		}

		// Token: 0x0400137E RID: 4990
		private RsaKeyParameters key;

		// Token: 0x0400137F RID: 4991
		private bool forEncryption;

		// Token: 0x04001380 RID: 4992
		private int bitSize;
	}
}
