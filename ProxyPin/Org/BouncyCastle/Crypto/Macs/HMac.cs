using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003EA RID: 1002
	public class HMac : IMac
	{
		// Token: 0x06001FC5 RID: 8133 RVA: 0x000B8AC0 File Offset: 0x000B8AC0
		public HMac(IDigest digest)
		{
			this.digest = digest;
			this.digestSize = digest.GetDigestSize();
			this.blockLength = digest.GetByteLength();
			this.inputPad = new byte[this.blockLength];
			this.outputBuf = new byte[this.blockLength + this.digestSize];
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x000B8B20 File Offset: 0x000B8B20
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "/HMAC";
			}
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000B8B38 File Offset: 0x000B8B38
		public virtual IDigest GetUnderlyingDigest()
		{
			return this.digest;
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000B8B40 File Offset: 0x000B8B40
		public virtual void Init(ICipherParameters parameters)
		{
			this.digest.Reset();
			byte[] key = ((KeyParameter)parameters).GetKey();
			int num = key.Length;
			if (num > this.blockLength)
			{
				this.digest.BlockUpdate(key, 0, num);
				this.digest.DoFinal(this.inputPad, 0);
				num = this.digestSize;
			}
			else
			{
				Array.Copy(key, 0, this.inputPad, 0, num);
			}
			Array.Clear(this.inputPad, num, this.blockLength - num);
			Array.Copy(this.inputPad, 0, this.outputBuf, 0, this.blockLength);
			HMac.XorPad(this.inputPad, this.blockLength, 54);
			HMac.XorPad(this.outputBuf, this.blockLength, 92);
			if (this.digest is IMemoable)
			{
				this.opadState = ((IMemoable)this.digest).Copy();
				((IDigest)this.opadState).BlockUpdate(this.outputBuf, 0, this.blockLength);
			}
			this.digest.BlockUpdate(this.inputPad, 0, this.inputPad.Length);
			if (this.digest is IMemoable)
			{
				this.ipadState = ((IMemoable)this.digest).Copy();
			}
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000B8C8C File Offset: 0x000B8C8C
		public virtual int GetMacSize()
		{
			return this.digestSize;
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000B8C94 File Offset: 0x000B8C94
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000B8CA4 File Offset: 0x000B8CA4
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.digest.BlockUpdate(input, inOff, len);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000B8CB4 File Offset: 0x000B8CB4
		public virtual int DoFinal(byte[] output, int outOff)
		{
			this.digest.DoFinal(this.outputBuf, this.blockLength);
			if (this.opadState != null)
			{
				((IMemoable)this.digest).Reset(this.opadState);
				this.digest.BlockUpdate(this.outputBuf, this.blockLength, this.digest.GetDigestSize());
			}
			else
			{
				this.digest.BlockUpdate(this.outputBuf, 0, this.outputBuf.Length);
			}
			int result = this.digest.DoFinal(output, outOff);
			Array.Clear(this.outputBuf, this.blockLength, this.digestSize);
			if (this.ipadState != null)
			{
				((IMemoable)this.digest).Reset(this.ipadState);
			}
			else
			{
				this.digest.BlockUpdate(this.inputPad, 0, this.inputPad.Length);
			}
			return result;
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000B8DA8 File Offset: 0x000B8DA8
		public virtual void Reset()
		{
			this.digest.Reset();
			this.digest.BlockUpdate(this.inputPad, 0, this.inputPad.Length);
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000B8DD0 File Offset: 0x000B8DD0
		private static void XorPad(byte[] pad, int len, byte n)
		{
			for (int i = 0; i < len; i++)
			{
				IntPtr intPtr;
				pad[(int)(intPtr = (IntPtr)i)] = (pad[(int)intPtr] ^ n);
			}
		}

		// Token: 0x040014C6 RID: 5318
		private const byte IPAD = 54;

		// Token: 0x040014C7 RID: 5319
		private const byte OPAD = 92;

		// Token: 0x040014C8 RID: 5320
		private readonly IDigest digest;

		// Token: 0x040014C9 RID: 5321
		private readonly int digestSize;

		// Token: 0x040014CA RID: 5322
		private readonly int blockLength;

		// Token: 0x040014CB RID: 5323
		private IMemoable ipadState;

		// Token: 0x040014CC RID: 5324
		private IMemoable opadState;

		// Token: 0x040014CD RID: 5325
		private readonly byte[] inputPad;

		// Token: 0x040014CE RID: 5326
		private readonly byte[] outputBuf;
	}
}
