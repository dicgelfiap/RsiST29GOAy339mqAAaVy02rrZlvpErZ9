using System;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003CB RID: 971
	public class KdfFeedbackBytesGenerator : IMacDerivationFunction, IDerivationFunction
	{
		// Token: 0x06001EAC RID: 7852 RVA: 0x000B40B8 File Offset: 0x000B40B8
		public KdfFeedbackBytesGenerator(IMac prf)
		{
			this.prf = prf;
			this.h = prf.GetMacSize();
			this.k = new byte[this.h];
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000B40E4 File Offset: 0x000B40E4
		public void Init(IDerivationParameters parameters)
		{
			KdfFeedbackParameters kdfFeedbackParameters = parameters as KdfFeedbackParameters;
			if (kdfFeedbackParameters == null)
			{
				throw new ArgumentException("Wrong type of arguments given");
			}
			this.prf.Init(new KeyParameter(kdfFeedbackParameters.Ki));
			this.fixedInputData = kdfFeedbackParameters.FixedInputData;
			int r = kdfFeedbackParameters.R;
			this.ios = new byte[r / 8];
			if (kdfFeedbackParameters.UseCounter)
			{
				BigInteger bigInteger = KdfFeedbackBytesGenerator.Two.Pow(r).Multiply(BigInteger.ValueOf((long)this.h));
				this.maxSizeExcl = ((bigInteger.CompareTo(KdfFeedbackBytesGenerator.IntegerMax) == 1) ? int.MaxValue : bigInteger.IntValue);
			}
			else
			{
				this.maxSizeExcl = int.MaxValue;
			}
			this.iv = kdfFeedbackParameters.Iv;
			this.useCounter = kdfFeedbackParameters.UseCounter;
			this.generatedBytes = 0;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x000B41C4 File Offset: 0x000B41C4
		public IDigest Digest
		{
			get
			{
				if (!(this.prf is HMac))
				{
					return null;
				}
				return ((HMac)this.prf).GetUnderlyingDigest();
			}
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000B41E8 File Offset: 0x000B41E8
		public int GenerateBytes(byte[] output, int outOff, int length)
		{
			int num = this.generatedBytes + length;
			if (num < 0 || num >= this.maxSizeExcl)
			{
				throw new DataLengthException("Current KDFCTR may only be used for " + this.maxSizeExcl + " bytes");
			}
			if (this.generatedBytes % this.h == 0)
			{
				this.generateNext();
			}
			int sourceIndex = this.generatedBytes % this.h;
			int val = this.h - this.generatedBytes % this.h;
			int num2 = Math.Min(val, length);
			Array.Copy(this.k, sourceIndex, output, outOff, num2);
			this.generatedBytes += num2;
			int i = length - num2;
			outOff += num2;
			while (i > 0)
			{
				this.generateNext();
				num2 = Math.Min(this.h, i);
				Array.Copy(this.k, 0, output, outOff, num2);
				this.generatedBytes += num2;
				i -= num2;
				outOff += num2;
			}
			return length;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000B42F0 File Offset: 0x000B42F0
		private void generateNext()
		{
			if (this.generatedBytes == 0)
			{
				this.prf.BlockUpdate(this.iv, 0, this.iv.Length);
			}
			else
			{
				this.prf.BlockUpdate(this.k, 0, this.k.Length);
			}
			if (this.useCounter)
			{
				int num = this.generatedBytes / this.h + 1;
				switch (this.ios.Length)
				{
				case 1:
					goto IL_BD;
				case 2:
					goto IL_A8;
				case 3:
					break;
				case 4:
					this.ios[0] = (byte)(num >> 24);
					break;
				default:
					throw new InvalidOperationException("Unsupported size of counter i");
				}
				this.ios[this.ios.Length - 3] = (byte)(num >> 16);
				IL_A8:
				this.ios[this.ios.Length - 2] = (byte)(num >> 8);
				IL_BD:
				this.ios[this.ios.Length - 1] = (byte)num;
				this.prf.BlockUpdate(this.ios, 0, this.ios.Length);
			}
			this.prf.BlockUpdate(this.fixedInputData, 0, this.fixedInputData.Length);
			this.prf.DoFinal(this.k, 0);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x000B4428 File Offset: 0x000B4428
		public IMac GetMac()
		{
			return this.prf;
		}

		// Token: 0x04001453 RID: 5203
		private static readonly BigInteger IntegerMax = BigInteger.ValueOf(2147483647L);

		// Token: 0x04001454 RID: 5204
		private static readonly BigInteger Two = BigInteger.Two;

		// Token: 0x04001455 RID: 5205
		private readonly IMac prf;

		// Token: 0x04001456 RID: 5206
		private readonly int h;

		// Token: 0x04001457 RID: 5207
		private byte[] fixedInputData;

		// Token: 0x04001458 RID: 5208
		private int maxSizeExcl;

		// Token: 0x04001459 RID: 5209
		private byte[] ios;

		// Token: 0x0400145A RID: 5210
		private byte[] iv;

		// Token: 0x0400145B RID: 5211
		private bool useCounter;

		// Token: 0x0400145C RID: 5212
		private int generatedBytes;

		// Token: 0x0400145D RID: 5213
		private byte[] k;
	}
}
