using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200038D RID: 909
	public class HC128Engine : IStreamCipher
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x000A2170 File Offset: 0x000A2170
		private static uint F1(uint x)
		{
			return HC128Engine.RotateRight(x, 7) ^ HC128Engine.RotateRight(x, 18) ^ x >> 3;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x000A2188 File Offset: 0x000A2188
		private static uint F2(uint x)
		{
			return HC128Engine.RotateRight(x, 17) ^ HC128Engine.RotateRight(x, 19) ^ x >> 10;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000A21A0 File Offset: 0x000A21A0
		private uint G1(uint x, uint y, uint z)
		{
			return (HC128Engine.RotateRight(x, 10) ^ HC128Engine.RotateRight(z, 23)) + HC128Engine.RotateRight(y, 8);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000A21BC File Offset: 0x000A21BC
		private uint G2(uint x, uint y, uint z)
		{
			return (HC128Engine.RotateLeft(x, 10) ^ HC128Engine.RotateLeft(z, 23)) + HC128Engine.RotateLeft(y, 8);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x000A21D8 File Offset: 0x000A21D8
		private static uint RotateLeft(uint x, int bits)
		{
			return x << bits | x >> -bits;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000A21E8 File Offset: 0x000A21E8
		private static uint RotateRight(uint x, int bits)
		{
			return x >> bits | x << -bits;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000A21F8 File Offset: 0x000A21F8
		private uint H1(uint x)
		{
			return this.q[(int)((UIntPtr)(x & 255U))] + this.q[(int)((UIntPtr)((x >> 16 & 255U) + 256U))];
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000A2224 File Offset: 0x000A2224
		private uint H2(uint x)
		{
			return this.p[(int)((UIntPtr)(x & 255U))] + this.p[(int)((UIntPtr)((x >> 16 & 255U) + 256U))];
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000A2250 File Offset: 0x000A2250
		private static uint Mod1024(uint x)
		{
			return x & 1023U;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000A225C File Offset: 0x000A225C
		private static uint Mod512(uint x)
		{
			return x & 511U;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000A2268 File Offset: 0x000A2268
		private static uint Dim(uint x, uint y)
		{
			return HC128Engine.Mod512(x - y);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000A2274 File Offset: 0x000A2274
		private uint Step()
		{
			uint num = HC128Engine.Mod512(this.cnt);
			uint result;
			if (this.cnt < 512U)
			{
				uint[] array;
				IntPtr intPtr;
				(array = this.p)[(int)(intPtr = (IntPtr)((UIntPtr)num))] = array[(int)intPtr] + this.G1(this.p[(int)((UIntPtr)HC128Engine.Dim(num, 3U))], this.p[(int)((UIntPtr)HC128Engine.Dim(num, 10U))], this.p[(int)((UIntPtr)HC128Engine.Dim(num, 511U))]);
				result = (this.H1(this.p[(int)((UIntPtr)HC128Engine.Dim(num, 12U))]) ^ this.p[(int)((UIntPtr)num)]);
			}
			else
			{
				uint[] array;
				IntPtr intPtr;
				(array = this.q)[(int)(intPtr = (IntPtr)((UIntPtr)num))] = array[(int)intPtr] + this.G2(this.q[(int)((UIntPtr)HC128Engine.Dim(num, 3U))], this.q[(int)((UIntPtr)HC128Engine.Dim(num, 10U))], this.q[(int)((UIntPtr)HC128Engine.Dim(num, 511U))]);
				result = (this.H2(this.q[(int)((UIntPtr)HC128Engine.Dim(num, 12U))]) ^ this.q[(int)((UIntPtr)num)]);
			}
			this.cnt = HC128Engine.Mod1024(this.cnt + 1U);
			return result;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000A2390 File Offset: 0x000A2390
		private void Init()
		{
			if (this.key.Length != 16)
			{
				throw new ArgumentException("The key must be 128 bits long");
			}
			this.idx = 0;
			this.cnt = 0U;
			uint[] array = new uint[1280];
			for (int i = 0; i < 16; i++)
			{
				uint[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)(i >> 2))] = (array2[(int)intPtr] | (uint)((uint)this.key[i] << 8 * (i & 3)));
			}
			Array.Copy(array, 0, array, 4, 4);
			int num = 0;
			while (num < this.iv.Length && num < 16)
			{
				uint[] array2;
				IntPtr intPtr;
				(array2 = array)[(int)(intPtr = (IntPtr)((num >> 2) + 8))] = (array2[(int)intPtr] | (uint)((uint)this.iv[num] << 8 * (num & 3)));
				num++;
			}
			Array.Copy(array, 8, array, 12, 4);
			for (uint num2 = 16U; num2 < 1280U; num2 += 1U)
			{
				array[(int)((UIntPtr)num2)] = HC128Engine.F2(array[(int)((UIntPtr)(num2 - 2U))]) + array[(int)((UIntPtr)(num2 - 7U))] + HC128Engine.F1(array[(int)((UIntPtr)(num2 - 15U))]) + array[(int)((UIntPtr)(num2 - 16U))] + num2;
			}
			Array.Copy(array, 256, this.p, 0, 512);
			Array.Copy(array, 768, this.q, 0, 512);
			for (int j = 0; j < 512; j++)
			{
				this.p[j] = this.Step();
			}
			for (int k = 0; k < 512; k++)
			{
				this.q[k] = this.Step();
			}
			this.cnt = 0U;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x000A252C File Offset: 0x000A252C
		public virtual string AlgorithmName
		{
			get
			{
				return "HC-128";
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x000A2534 File Offset: 0x000A2534
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
			throw new ArgumentException("Invalid parameter passed to HC128 init - " + Platform.GetTypeName(parameters), "parameters");
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x000A25CC File Offset: 0x000A25CC
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

		// Token: 0x06001CA3 RID: 7331 RVA: 0x000A2618 File Offset: 0x000A2618
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

		// Token: 0x06001CA4 RID: 7332 RVA: 0x000A268C File Offset: 0x000A268C
		public virtual void Reset()
		{
			this.Init();
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000A2694 File Offset: 0x000A2694
		public virtual byte ReturnByte(byte input)
		{
			return input ^ this.GetByte();
		}

		// Token: 0x04001303 RID: 4867
		private uint[] p = new uint[512];

		// Token: 0x04001304 RID: 4868
		private uint[] q = new uint[512];

		// Token: 0x04001305 RID: 4869
		private uint cnt = 0U;

		// Token: 0x04001306 RID: 4870
		private byte[] key;

		// Token: 0x04001307 RID: 4871
		private byte[] iv;

		// Token: 0x04001308 RID: 4872
		private bool initialised;

		// Token: 0x04001309 RID: 4873
		private byte[] buf = new byte[4];

		// Token: 0x0400130A RID: 4874
		private int idx = 0;
	}
}
