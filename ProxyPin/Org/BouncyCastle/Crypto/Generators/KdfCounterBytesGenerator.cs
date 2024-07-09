using System;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003C9 RID: 969
	public class KdfCounterBytesGenerator : IMacDerivationFunction, IDerivationFunction
	{
		// Token: 0x06001E9E RID: 7838 RVA: 0x000B39A0 File Offset: 0x000B39A0
		public KdfCounterBytesGenerator(IMac prf)
		{
			this.prf = prf;
			this.h = prf.GetMacSize();
			this.k = new byte[this.h];
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000B39CC File Offset: 0x000B39CC
		public void Init(IDerivationParameters param)
		{
			KdfCounterParameters kdfCounterParameters = param as KdfCounterParameters;
			if (kdfCounterParameters == null)
			{
				throw new ArgumentException("Wrong type of arguments given");
			}
			this.prf.Init(new KeyParameter(kdfCounterParameters.Ki));
			this.fixedInputDataCtrPrefix = kdfCounterParameters.FixedInputDataCounterPrefix;
			this.fixedInputData_afterCtr = kdfCounterParameters.FixedInputDataCounterSuffix;
			int r = kdfCounterParameters.R;
			this.ios = new byte[r / 8];
			BigInteger bigInteger = KdfCounterBytesGenerator.Two.Pow(r).Multiply(BigInteger.ValueOf((long)this.h));
			this.maxSizeExcl = ((bigInteger.CompareTo(KdfCounterBytesGenerator.IntegerMax) == 1) ? int.MaxValue : bigInteger.IntValue);
			this.generatedBytes = 0;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000B3A84 File Offset: 0x000B3A84
		public IMac GetMac()
		{
			return this.prf;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000B3A8C File Offset: 0x000B3A8C
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

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000B3AB0 File Offset: 0x000B3AB0
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

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000B3BB8 File Offset: 0x000B3BB8
		private void generateNext()
		{
			int num = this.generatedBytes / this.h + 1;
			switch (this.ios.Length)
			{
			case 1:
				goto IL_6E;
			case 2:
				goto IL_59;
			case 3:
				break;
			case 4:
				this.ios[0] = (byte)(num >> 24);
				break;
			default:
				throw new InvalidOperationException("Unsupported size of counter i");
			}
			this.ios[this.ios.Length - 3] = (byte)(num >> 16);
			IL_59:
			this.ios[this.ios.Length - 2] = (byte)(num >> 8);
			IL_6E:
			this.ios[this.ios.Length - 1] = (byte)num;
			this.prf.BlockUpdate(this.fixedInputDataCtrPrefix, 0, this.fixedInputDataCtrPrefix.Length);
			this.prf.BlockUpdate(this.ios, 0, this.ios.Length);
			this.prf.BlockUpdate(this.fixedInputData_afterCtr, 0, this.fixedInputData_afterCtr.Length);
			this.prf.DoFinal(this.k, 0);
		}

		// Token: 0x0400143E RID: 5182
		private static readonly BigInteger IntegerMax = BigInteger.ValueOf(2147483647L);

		// Token: 0x0400143F RID: 5183
		private static readonly BigInteger Two = BigInteger.Two;

		// Token: 0x04001440 RID: 5184
		private readonly IMac prf;

		// Token: 0x04001441 RID: 5185
		private readonly int h;

		// Token: 0x04001442 RID: 5186
		private byte[] fixedInputDataCtrPrefix;

		// Token: 0x04001443 RID: 5187
		private byte[] fixedInputData_afterCtr;

		// Token: 0x04001444 RID: 5188
		private int maxSizeExcl;

		// Token: 0x04001445 RID: 5189
		private byte[] ios;

		// Token: 0x04001446 RID: 5190
		private int generatedBytes;

		// Token: 0x04001447 RID: 5191
		private byte[] k;
	}
}
