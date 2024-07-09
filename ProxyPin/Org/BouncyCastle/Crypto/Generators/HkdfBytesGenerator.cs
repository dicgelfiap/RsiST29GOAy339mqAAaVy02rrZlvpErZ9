using System;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020003C5 RID: 965
	public class HkdfBytesGenerator : IDerivationFunction
	{
		// Token: 0x06001E95 RID: 7829 RVA: 0x000B36CC File Offset: 0x000B36CC
		public HkdfBytesGenerator(IDigest hash)
		{
			this.hMacHash = new HMac(hash);
			this.hashLen = hash.GetDigestSize();
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000B36EC File Offset: 0x000B36EC
		public virtual void Init(IDerivationParameters parameters)
		{
			if (!(parameters is HkdfParameters))
			{
				throw new ArgumentException("HKDF parameters required for HkdfBytesGenerator", "parameters");
			}
			HkdfParameters hkdfParameters = (HkdfParameters)parameters;
			if (hkdfParameters.SkipExtract)
			{
				this.hMacHash.Init(new KeyParameter(hkdfParameters.GetIkm()));
			}
			else
			{
				this.hMacHash.Init(this.Extract(hkdfParameters.GetSalt(), hkdfParameters.GetIkm()));
			}
			this.info = hkdfParameters.GetInfo();
			this.generatedBytes = 0;
			this.currentT = new byte[this.hashLen];
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000B3788 File Offset: 0x000B3788
		private KeyParameter Extract(byte[] salt, byte[] ikm)
		{
			if (salt == null)
			{
				this.hMacHash.Init(new KeyParameter(new byte[this.hashLen]));
			}
			else
			{
				this.hMacHash.Init(new KeyParameter(salt));
			}
			this.hMacHash.BlockUpdate(ikm, 0, ikm.Length);
			byte[] array = new byte[this.hashLen];
			this.hMacHash.DoFinal(array, 0);
			return new KeyParameter(array);
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000B3800 File Offset: 0x000B3800
		private void ExpandNext()
		{
			int num = this.generatedBytes / this.hashLen + 1;
			if (num >= 256)
			{
				throw new DataLengthException("HKDF cannot generate more than 255 blocks of HashLen size");
			}
			if (this.generatedBytes != 0)
			{
				this.hMacHash.BlockUpdate(this.currentT, 0, this.hashLen);
			}
			this.hMacHash.BlockUpdate(this.info, 0, this.info.Length);
			this.hMacHash.Update((byte)num);
			this.hMacHash.DoFinal(this.currentT, 0);
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x000B3894 File Offset: 0x000B3894
		public virtual IDigest Digest
		{
			get
			{
				return this.hMacHash.GetUnderlyingDigest();
			}
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000B38A4 File Offset: 0x000B38A4
		public virtual int GenerateBytes(byte[] output, int outOff, int len)
		{
			if (this.generatedBytes + len > 255 * this.hashLen)
			{
				throw new DataLengthException("HKDF may only be used for 255 * HashLen bytes of output");
			}
			if (this.generatedBytes % this.hashLen == 0)
			{
				this.ExpandNext();
			}
			int sourceIndex = this.generatedBytes % this.hashLen;
			int val = this.hashLen - this.generatedBytes % this.hashLen;
			int num = Math.Min(val, len);
			Array.Copy(this.currentT, sourceIndex, output, outOff, num);
			this.generatedBytes += num;
			int i = len - num;
			outOff += num;
			while (i > 0)
			{
				this.ExpandNext();
				num = Math.Min(this.hashLen, i);
				Array.Copy(this.currentT, 0, output, outOff, num);
				this.generatedBytes += num;
				i -= num;
				outOff += num;
			}
			return len;
		}

		// Token: 0x04001439 RID: 5177
		private HMac hMacHash;

		// Token: 0x0400143A RID: 5178
		private int hashLen;

		// Token: 0x0400143B RID: 5179
		private byte[] info;

		// Token: 0x0400143C RID: 5180
		private byte[] currentT;

		// Token: 0x0400143D RID: 5181
		private int generatedBytes;
	}
}
