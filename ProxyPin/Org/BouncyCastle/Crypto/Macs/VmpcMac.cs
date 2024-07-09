using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020003EF RID: 1007
	public class VmpcMac : IMac
	{
		// Token: 0x06001FFC RID: 8188 RVA: 0x000BA274 File Offset: 0x000BA274
		public virtual int DoFinal(byte[] output, int outOff)
		{
			for (int i = 1; i < 25; i++)
			{
				this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
				this.x4 = this.P[(int)(this.x4 + this.x3) + i & 255];
				this.x3 = this.P[(int)(this.x3 + this.x2) + i & 255];
				this.x2 = this.P[(int)(this.x2 + this.x1) + i & 255];
				this.x1 = this.P[(int)(this.x1 + this.s) + i & 255];
				this.T[(int)(this.g & 31)] = (this.T[(int)(this.g & 31)] ^ this.x1);
				this.T[(int)(this.g + 1 & 31)] = (this.T[(int)(this.g + 1 & 31)] ^ this.x2);
				this.T[(int)(this.g + 2 & 31)] = (this.T[(int)(this.g + 2 & 31)] ^ this.x3);
				this.T[(int)(this.g + 3 & 31)] = (this.T[(int)(this.g + 3 & 31)] ^ this.x4);
				this.g = (this.g + 4 & 31);
				byte b = this.P[(int)(this.n & byte.MaxValue)];
				this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b;
				this.n = (this.n + 1 & byte.MaxValue);
			}
			for (int j = 0; j < 768; j++)
			{
				this.s = this.P[(int)(this.s + this.P[j & 255] + this.T[j & 31] & byte.MaxValue)];
				byte b2 = this.P[j & 255];
				this.P[j & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
			}
			byte[] array = new byte[20];
			for (int k = 0; k < 20; k++)
			{
				this.s = this.P[(int)(this.s + this.P[k & 255] & byte.MaxValue)];
				array[k] = this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
				byte b3 = this.P[k & 255];
				this.P[k & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b3;
			}
			Array.Copy(array, 0, output, outOff, array.Length);
			this.Reset();
			return array.Length;
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x000BA5DC File Offset: 0x000BA5DC
		public virtual string AlgorithmName
		{
			get
			{
				return "VMPC-MAC";
			}
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x000BA5E4 File Offset: 0x000BA5E4
		public virtual int GetMacSize()
		{
			return 20;
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000BA5E8 File Offset: 0x000BA5E8
		public virtual void Init(ICipherParameters parameters)
		{
			if (!(parameters is ParametersWithIV))
			{
				throw new ArgumentException("VMPC-MAC Init parameters must include an IV", "parameters");
			}
			ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
			KeyParameter keyParameter = (KeyParameter)parametersWithIV.Parameters;
			if (!(parametersWithIV.Parameters is KeyParameter))
			{
				throw new ArgumentException("VMPC-MAC Init parameters must include a key", "parameters");
			}
			this.workingIV = parametersWithIV.GetIV();
			if (this.workingIV == null || this.workingIV.Length < 1 || this.workingIV.Length > 768)
			{
				throw new ArgumentException("VMPC-MAC requires 1 to 768 bytes of IV", "parameters");
			}
			this.workingKey = keyParameter.GetKey();
			this.Reset();
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000BA6A0 File Offset: 0x000BA6A0
		private void initKey(byte[] keyBytes, byte[] ivBytes)
		{
			this.s = 0;
			this.P = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				this.P[i] = (byte)i;
			}
			for (int j = 0; j < 768; j++)
			{
				this.s = this.P[(int)(this.s + this.P[j & 255] + keyBytes[j % keyBytes.Length] & byte.MaxValue)];
				byte b = this.P[j & 255];
				this.P[j & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b;
			}
			for (int k = 0; k < 768; k++)
			{
				this.s = this.P[(int)(this.s + this.P[k & 255] + ivBytes[k % ivBytes.Length] & byte.MaxValue)];
				byte b2 = this.P[k & 255];
				this.P[k & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
			}
			this.n = 0;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000BA800 File Offset: 0x000BA800
		public virtual void Reset()
		{
			this.initKey(this.workingKey, this.workingIV);
			this.g = (this.x1 = (this.x2 = (this.x3 = (this.x4 = (this.n = 0)))));
			this.T = new byte[32];
			for (int i = 0; i < 32; i++)
			{
				this.T[i] = 0;
			}
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000BA880 File Offset: 0x000BA880
		public virtual void Update(byte input)
		{
			this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
			byte b = input ^ this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
			this.x4 = this.P[(int)(this.x4 + this.x3 & byte.MaxValue)];
			this.x3 = this.P[(int)(this.x3 + this.x2 & byte.MaxValue)];
			this.x2 = this.P[(int)(this.x2 + this.x1 & byte.MaxValue)];
			this.x1 = this.P[(int)(this.x1 + this.s + b & byte.MaxValue)];
			this.T[(int)(this.g & 31)] = (this.T[(int)(this.g & 31)] ^ this.x1);
			this.T[(int)(this.g + 1 & 31)] = (this.T[(int)(this.g + 1 & 31)] ^ this.x2);
			this.T[(int)(this.g + 2 & 31)] = (this.T[(int)(this.g + 2 & 31)] ^ this.x3);
			this.T[(int)(this.g + 3 & 31)] = (this.T[(int)(this.g + 3 & 31)] ^ this.x4);
			this.g = (this.g + 4 & 31);
			byte b2 = this.P[(int)(this.n & byte.MaxValue)];
			this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
			this.P[(int)(this.s & byte.MaxValue)] = b2;
			this.n = (this.n + 1 & byte.MaxValue);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000BAA94 File Offset: 0x000BAA94
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (inOff + len > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			for (int i = 0; i < len; i++)
			{
				this.Update(input[inOff + i]);
			}
		}

		// Token: 0x040014FD RID: 5373
		private byte g;

		// Token: 0x040014FE RID: 5374
		private byte n = 0;

		// Token: 0x040014FF RID: 5375
		private byte[] P = null;

		// Token: 0x04001500 RID: 5376
		private byte s = 0;

		// Token: 0x04001501 RID: 5377
		private byte[] T;

		// Token: 0x04001502 RID: 5378
		private byte[] workingIV;

		// Token: 0x04001503 RID: 5379
		private byte[] workingKey;

		// Token: 0x04001504 RID: 5380
		private byte x1;

		// Token: 0x04001505 RID: 5381
		private byte x2;

		// Token: 0x04001506 RID: 5382
		private byte x3;

		// Token: 0x04001507 RID: 5383
		private byte x4;
	}
}
