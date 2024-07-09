using System;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003CA RID: 970
	public class KdfDoublePipelineIterationBytesGenerator : IMacDerivationFunction, IDerivationFunction
	{
		// Token: 0x06001EA5 RID: 7845 RVA: 0x000B3CD8 File Offset: 0x000B3CD8
		public KdfDoublePipelineIterationBytesGenerator(IMac prf)
		{
			this.prf = prf;
			this.h = prf.GetMacSize();
			this.a = new byte[this.h];
			this.k = new byte[this.h];
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000B3D18 File Offset: 0x000B3D18
		public void Init(IDerivationParameters parameters)
		{
			KdfDoublePipelineIterationParameters kdfDoublePipelineIterationParameters = parameters as KdfDoublePipelineIterationParameters;
			if (kdfDoublePipelineIterationParameters == null)
			{
				throw new ArgumentException("Wrong type of arguments given");
			}
			this.prf.Init(new KeyParameter(kdfDoublePipelineIterationParameters.Ki));
			this.fixedInputData = kdfDoublePipelineIterationParameters.FixedInputData;
			int r = kdfDoublePipelineIterationParameters.R;
			this.ios = new byte[r / 8];
			if (kdfDoublePipelineIterationParameters.UseCounter)
			{
				BigInteger bigInteger = KdfDoublePipelineIterationBytesGenerator.Two.Pow(r).Multiply(BigInteger.ValueOf((long)this.h));
				this.maxSizeExcl = ((bigInteger.CompareTo(KdfDoublePipelineIterationBytesGenerator.IntegerMax) == 1) ? int.MaxValue : bigInteger.IntValue);
			}
			else
			{
				this.maxSizeExcl = KdfDoublePipelineIterationBytesGenerator.IntegerMax.IntValue;
			}
			this.useCounter = kdfDoublePipelineIterationParameters.UseCounter;
			this.generatedBytes = 0;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000B3DF0 File Offset: 0x000B3DF0
		private void generateNext()
		{
			if (this.generatedBytes == 0)
			{
				this.prf.BlockUpdate(this.fixedInputData, 0, this.fixedInputData.Length);
				this.prf.DoFinal(this.a, 0);
			}
			else
			{
				this.prf.BlockUpdate(this.a, 0, this.a.Length);
				this.prf.DoFinal(this.a, 0);
			}
			this.prf.BlockUpdate(this.a, 0, this.a.Length);
			if (this.useCounter)
			{
				int num = this.generatedBytes / this.h + 1;
				switch (this.ios.Length)
				{
				case 1:
					goto IL_FD;
				case 2:
					goto IL_E8;
				case 3:
					break;
				case 4:
					this.ios[0] = (byte)(num >> 24);
					break;
				default:
					throw new InvalidOperationException("Unsupported size of counter i");
				}
				this.ios[this.ios.Length - 3] = (byte)(num >> 16);
				IL_E8:
				this.ios[this.ios.Length - 2] = (byte)(num >> 8);
				IL_FD:
				this.ios[this.ios.Length - 1] = (byte)num;
				this.prf.BlockUpdate(this.ios, 0, this.ios.Length);
			}
			this.prf.BlockUpdate(this.fixedInputData, 0, this.fixedInputData.Length);
			this.prf.DoFinal(this.k, 0);
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x000B3F68 File Offset: 0x000B3F68
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

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000B3F8C File Offset: 0x000B3F8C
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

		// Token: 0x06001EAA RID: 7850 RVA: 0x000B4094 File Offset: 0x000B4094
		public IMac GetMac()
		{
			return this.prf;
		}

		// Token: 0x04001448 RID: 5192
		private static readonly BigInteger IntegerMax = BigInteger.ValueOf(2147483647L);

		// Token: 0x04001449 RID: 5193
		private static readonly BigInteger Two = BigInteger.Two;

		// Token: 0x0400144A RID: 5194
		private readonly IMac prf;

		// Token: 0x0400144B RID: 5195
		private readonly int h;

		// Token: 0x0400144C RID: 5196
		private byte[] fixedInputData;

		// Token: 0x0400144D RID: 5197
		private int maxSizeExcl;

		// Token: 0x0400144E RID: 5198
		private byte[] ios;

		// Token: 0x0400144F RID: 5199
		private bool useCounter;

		// Token: 0x04001450 RID: 5200
		private int generatedBytes;

		// Token: 0x04001451 RID: 5201
		private byte[] a;

		// Token: 0x04001452 RID: 5202
		private byte[] k;
	}
}
