using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003AD RID: 941
	public class VmpcEngine : IStreamCipher
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x000B0158 File Offset: 0x000B0158
		public virtual string AlgorithmName
		{
			get
			{
				return "VMPC";
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000B0160 File Offset: 0x000B0160
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is ParametersWithIV))
			{
				throw new ArgumentException("VMPC Init parameters must include an IV");
			}
			ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
			if (!(parametersWithIV.Parameters is KeyParameter))
			{
				throw new ArgumentException("VMPC Init parameters must include a key");
			}
			KeyParameter keyParameter = (KeyParameter)parametersWithIV.Parameters;
			this.workingIV = parametersWithIV.GetIV();
			if (this.workingIV == null || this.workingIV.Length < 1 || this.workingIV.Length > 768)
			{
				throw new ArgumentException("VMPC requires 1 to 768 bytes of IV");
			}
			this.workingKey = keyParameter.GetKey();
			this.InitKey(this.workingKey, this.workingIV);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x000B0218 File Offset: 0x000B0218
		protected virtual void InitKey(byte[] keyBytes, byte[] ivBytes)
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

		// Token: 0x06001E1C RID: 7708 RVA: 0x000B0378 File Offset: 0x000B0378
		public virtual void ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			Check.DataLength(input, inOff, len, "input buffer too short");
			Check.OutputLength(output, outOff, len, "output buffer too short");
			for (int i = 0; i < len; i++)
			{
				this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
				byte b = this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
				byte b2 = this.P[(int)(this.n & byte.MaxValue)];
				this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
				this.n = (this.n + 1 & byte.MaxValue);
				output[i + outOff] = (input[i + inOff] ^ b);
			}
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000B0488 File Offset: 0x000B0488
		public virtual void Reset()
		{
			this.InitKey(this.workingKey, this.workingIV);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000B049C File Offset: 0x000B049C
		public virtual byte ReturnByte(byte input)
		{
			this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
			byte b = this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
			byte b2 = this.P[(int)(this.n & byte.MaxValue)];
			this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
			this.P[(int)(this.s & byte.MaxValue)] = b2;
			this.n = (this.n + 1 & byte.MaxValue);
			return input ^ b;
		}

		// Token: 0x040013F1 RID: 5105
		protected byte n = 0;

		// Token: 0x040013F2 RID: 5106
		protected byte[] P = null;

		// Token: 0x040013F3 RID: 5107
		protected byte s = 0;

		// Token: 0x040013F4 RID: 5108
		protected byte[] workingIV;

		// Token: 0x040013F5 RID: 5109
		protected byte[] workingKey;
	}
}
