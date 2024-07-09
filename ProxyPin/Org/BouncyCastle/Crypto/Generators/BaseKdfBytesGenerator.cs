using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003B1 RID: 945
	public class BaseKdfBytesGenerator : IDerivationFunction
	{
		// Token: 0x06001E31 RID: 7729 RVA: 0x000B0B5C File Offset: 0x000B0B5C
		public BaseKdfBytesGenerator(int counterStart, IDigest digest)
		{
			this.counterStart = counterStart;
			this.digest = digest;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000B0B74 File Offset: 0x000B0B74
		public virtual void Init(IDerivationParameters parameters)
		{
			if (parameters is KdfParameters)
			{
				KdfParameters kdfParameters = (KdfParameters)parameters;
				this.shared = kdfParameters.GetSharedSecret();
				this.iv = kdfParameters.GetIV();
				return;
			}
			if (parameters is Iso18033KdfParameters)
			{
				Iso18033KdfParameters iso18033KdfParameters = (Iso18033KdfParameters)parameters;
				this.shared = iso18033KdfParameters.GetSeed();
				this.iv = null;
				return;
			}
			throw new ArgumentException("KDF parameters required for KDF Generator");
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x000B0BE0 File Offset: 0x000B0BE0
		public virtual IDigest Digest
		{
			get
			{
				return this.digest;
			}
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000B0BE8 File Offset: 0x000B0BE8
		public virtual int GenerateBytes(byte[] output, int outOff, int length)
		{
			if (output.Length - length < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			long num = (long)length;
			int digestSize = this.digest.GetDigestSize();
			if (num > 8589934591L)
			{
				throw new ArgumentException("Output length too large");
			}
			int num2 = (int)((num + (long)digestSize - 1L) / (long)digestSize);
			byte[] array = new byte[this.digest.GetDigestSize()];
			byte[] array2 = new byte[4];
			Pack.UInt32_To_BE((uint)this.counterStart, array2, 0);
			uint num3 = (uint)(this.counterStart & -256);
			for (int i = 0; i < num2; i++)
			{
				this.digest.BlockUpdate(this.shared, 0, this.shared.Length);
				this.digest.BlockUpdate(array2, 0, 4);
				if (this.iv != null)
				{
					this.digest.BlockUpdate(this.iv, 0, this.iv.Length);
				}
				this.digest.DoFinal(array, 0);
				if (length > digestSize)
				{
					Array.Copy(array, 0, output, outOff, digestSize);
					outOff += digestSize;
					length -= digestSize;
				}
				else
				{
					Array.Copy(array, 0, output, outOff, length);
				}
				byte[] array3;
				if (((array3 = array2)[3] = array3[3] + 1) == 0)
				{
					num3 += 256U;
					Pack.UInt32_To_BE(num3, array2, 0);
				}
			}
			this.digest.Reset();
			return (int)num;
		}

		// Token: 0x040013FE RID: 5118
		private int counterStart;

		// Token: 0x040013FF RID: 5119
		private IDigest digest;

		// Token: 0x04001400 RID: 5120
		private byte[] shared;

		// Token: 0x04001401 RID: 5121
		private byte[] iv;
	}
}
