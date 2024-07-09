using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003CC RID: 972
	public class Mgf1BytesGenerator : IDerivationFunction
	{
		// Token: 0x06001EB3 RID: 7859 RVA: 0x000B444C File Offset: 0x000B444C
		public Mgf1BytesGenerator(IDigest digest)
		{
			this.digest = digest;
			this.hLen = digest.GetDigestSize();
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000B4468 File Offset: 0x000B4468
		public void Init(IDerivationParameters parameters)
		{
			if (!typeof(MgfParameters).IsInstanceOfType(parameters))
			{
				throw new ArgumentException("MGF parameters required for MGF1Generator");
			}
			MgfParameters mgfParameters = (MgfParameters)parameters;
			this.seed = mgfParameters.GetSeed();
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x000B44AC File Offset: 0x000B44AC
		public IDigest Digest
		{
			get
			{
				return this.digest;
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000B44B4 File Offset: 0x000B44B4
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000B44D4 File Offset: 0x000B44D4
		public int GenerateBytes(byte[] output, int outOff, int length)
		{
			if (output.Length - length < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			byte[] array = new byte[this.hLen];
			byte[] array2 = new byte[4];
			int num = 0;
			this.digest.Reset();
			if (length > this.hLen)
			{
				do
				{
					this.ItoOSP(num, array2);
					this.digest.BlockUpdate(this.seed, 0, this.seed.Length);
					this.digest.BlockUpdate(array2, 0, array2.Length);
					this.digest.DoFinal(array, 0);
					Array.Copy(array, 0, output, outOff + num * this.hLen, this.hLen);
				}
				while (++num < length / this.hLen);
			}
			if (num * this.hLen < length)
			{
				this.ItoOSP(num, array2);
				this.digest.BlockUpdate(this.seed, 0, this.seed.Length);
				this.digest.BlockUpdate(array2, 0, array2.Length);
				this.digest.DoFinal(array, 0);
				Array.Copy(array, 0, output, outOff + num * this.hLen, length - num * this.hLen);
			}
			return length;
		}

		// Token: 0x0400145E RID: 5214
		private IDigest digest;

		// Token: 0x0400145F RID: 5215
		private byte[] seed;

		// Token: 0x04001460 RID: 5216
		private int hLen;
	}
}
