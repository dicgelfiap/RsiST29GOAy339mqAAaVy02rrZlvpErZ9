using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200038E RID: 910
	public class HC256Engine : IStreamCipher
	{
		// Token: 0x06001CA7 RID: 7335 RVA: 0x000A26F4 File Offset: 0x000A26F4
		private uint Step()
		{
			uint num = this.cnt & 1023U;
			uint result;
			if (this.cnt < 1024U)
			{
				uint num2 = this.p[(int)((UIntPtr)(num - 3U & 1023U))];
				uint num3 = this.p[(int)((UIntPtr)(num - 1023U & 1023U))];
				uint[] array;
				IntPtr intPtr;
				(array = this.p)[(int)(intPtr = (IntPtr)((UIntPtr)num))] = array[(int)intPtr] + (this.p[(int)((UIntPtr)(num - 10U & 1023U))] + (HC256Engine.RotateRight(num2, 10) ^ HC256Engine.RotateRight(num3, 23)) + this.q[(int)((UIntPtr)((num2 ^ num3) & 1023U))]);
				num2 = this.p[(int)((UIntPtr)(num - 12U & 1023U))];
				result = (this.q[(int)((UIntPtr)(num2 & 255U))] + this.q[(int)((UIntPtr)((num2 >> 8 & 255U) + 256U))] + this.q[(int)((UIntPtr)((num2 >> 16 & 255U) + 512U))] + this.q[(int)((UIntPtr)((num2 >> 24 & 255U) + 768U))] ^ this.p[(int)((UIntPtr)num)]);
			}
			else
			{
				uint num4 = this.q[(int)((UIntPtr)(num - 3U & 1023U))];
				uint num5 = this.q[(int)((UIntPtr)(num - 1023U & 1023U))];
				uint[] array;
				IntPtr intPtr;
				(array = this.q)[(int)(intPtr = (IntPtr)((UIntPtr)num))] = array[(int)intPtr] + (this.q[(int)((UIntPtr)(num - 10U & 1023U))] + (HC256Engine.RotateRight(num4, 10) ^ HC256Engine.RotateRight(num5, 23)) + this.p[(int)((UIntPtr)((num4 ^ num5) & 1023U))]);
				num4 = this.q[(int)((UIntPtr)(num - 12U & 1023U))];
				result = (this.p[(int)((UIntPtr)(num4 & 255U))] + this.p[(int)((UIntPtr)((num4 >> 8 & 255U) + 256U))] + this.p[(int)((UIntPtr)((num4 >> 16 & 255U) + 512U))] + this.p[(int)((UIntPtr)((num4 >> 24 & 255U) + 768U))] ^ this.q[(int)((UIntPtr)num)]);
			}
			this.cnt = (this.cnt + 1U & 2047U);
			return result;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000A291C File Offset: 0x000A291C
		private void Init()
		{
			if (this.key.Length != 32 && this.key.Length != 16)
			{
				throw new ArgumentException("The key must be 128/256 bits long");
			}
			if (this.iv.Length < 16)
			{
				throw new ArgumentException("The IV must be at least 128 bits long");
			}
			if (this.key.Length != 32)
			{
				byte[] destinationArray = new byte[32];
				Array.Copy(this.key, 0, destinationArray, 0, this.key.Length);
				Array.Copy(this.key, 0, destinationArray, 16, this.key.Length);
				this.key = destinationArray;
			}
			if (this.iv.Length < 32)
			{
				byte[] array = new byte[32];
				Array.Copy(this.iv, 0, array, 0, this.iv.Length);
				Array.Copy(this.iv, 0, array, this.iv.Length, array.Length - this.iv.Length);
				this.iv = array;
			}
			this.idx = 0;
			this.cnt = 0U;
			uint[] array2 = new uint[2560];
			for (int i = 0; i < 32; i++)
			{
				uint[] array3;
				IntPtr intPtr;
				(array3 = array2)[(int)(intPtr = (IntPtr)(i >> 2))] = (array3[(int)intPtr] | (uint)((uint)this.key[i] << 8 * (i & 3)));
			}
			for (int j = 0; j < 32; j++)
			{
				uint[] array3;
				IntPtr intPtr;
				(array3 = array2)[(int)(intPtr = (IntPtr)((j >> 2) + 8))] = (array3[(int)intPtr] | (uint)((uint)this.iv[j] << 8 * (j & 3)));
			}
			for (uint num = 16U; num < 2560U; num += 1U)
			{
				uint num2 = array2[(int)((UIntPtr)(num - 2U))];
				uint num3 = array2[(int)((UIntPtr)(num - 15U))];
				array2[(int)((UIntPtr)num)] = (HC256Engine.RotateRight(num2, 17) ^ HC256Engine.RotateRight(num2, 19) ^ num2 >> 10) + array2[(int)((UIntPtr)(num - 7U))] + (HC256Engine.RotateRight(num3, 7) ^ HC256Engine.RotateRight(num3, 18) ^ num3 >> 3) + array2[(int)((UIntPtr)(num - 16U))] + num;
			}
			Array.Copy(array2, 512, this.p, 0, 1024);
			Array.Copy(array2, 1536, this.q, 0, 1024);
			for (int k = 0; k < 4096; k++)
			{
				this.Step();
			}
			this.cnt = 0U;
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x000A2B60 File Offset: 0x000A2B60
		public virtual string AlgorithmName
		{
			get
			{
				return "HC-256";
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x000A2B68 File Offset: 0x000A2B68
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			ICipherParameters cipherParameters = parameters;
			if (parameters is ParametersWithIV)
			{
				this.iv = ((ParametersWithIV)parameters).GetIV();
				cipherParameters = ((ParametersWithIV)parameters).Parameters;
			}
			else
			{
				this.iv = new byte[0];
			}
			if (cipherParameters is KeyParameter)
			{
				this.key = ((KeyParameter)cipherParameters).GetKey();
				this.Init();
				this.initialised = true;
				return;
			}
			throw new ArgumentException("Invalid parameter passed to HC256 init - " + Platform.GetTypeName(parameters), "parameters");
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x000A2C00 File Offset: 0x000A2C00
		private byte GetByte()
		{
			if (this.idx == 0)
			{
				Pack.UInt32_To_LE(this.Step(), this.buf);
			}
			byte result = this.buf[this.idx];
			this.idx = (this.idx + 1 & 3);
			return result;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x000A2C4C File Offset: 0x000A2C4C
		public virtual void ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			if (!this.initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(input, inOff, len, "input buffer too short");
			Check.OutputLength(output, outOff, len, "output buffer too short");
			for (int i = 0; i < len; i++)
			{
				output[outOff + i] = (input[inOff + i] ^ this.GetByte());
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x000A2CC0 File Offset: 0x000A2CC0
		public virtual void Reset()
		{
			this.Init();
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x000A2CC8 File Offset: 0x000A2CC8
		public virtual byte ReturnByte(byte input)
		{
			return input ^ this.GetByte();
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000A2CD4 File Offset: 0x000A2CD4
		private static uint RotateRight(uint x, int bits)
		{
			return x >> bits | x << -bits;
		}

		// Token: 0x0400130B RID: 4875
		private uint[] p = new uint[1024];

		// Token: 0x0400130C RID: 4876
		private uint[] q = new uint[1024];

		// Token: 0x0400130D RID: 4877
		private uint cnt = 0U;

		// Token: 0x0400130E RID: 4878
		private byte[] key;

		// Token: 0x0400130F RID: 4879
		private byte[] iv;

		// Token: 0x04001310 RID: 4880
		private bool initialised;

		// Token: 0x04001311 RID: 4881
		private byte[] buf = new byte[4];

		// Token: 0x04001312 RID: 4882
		private int idx = 0;
	}
}
