using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000391 RID: 913
	public class IsaacEngine : IStreamCipher
	{
		// Token: 0x06001CC9 RID: 7369 RVA: 0x000A3760 File Offset: 0x000A3760
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to ISAAC Init - " + Platform.GetTypeName(parameters), "parameters");
			}
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.setKey(keyParameter.GetKey());
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x000A37AC File Offset: 0x000A37AC
		public virtual byte ReturnByte(byte input)
		{
			if (this.index == 0)
			{
				this.isaac();
				this.keyStream = Pack.UInt32_To_BE(this.results);
			}
			byte result = this.keyStream[this.index] ^ input;
			this.index = (this.index + 1 & 1023);
			return result;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x000A3808 File Offset: 0x000A3808
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
				if (this.index == 0)
				{
					this.isaac();
					this.keyStream = Pack.UInt32_To_BE(this.results);
				}
				output[i + outOff] = (this.keyStream[this.index] ^ input[i + inOff]);
				this.index = (this.index + 1 & 1023);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x000A38B8 File Offset: 0x000A38B8
		public virtual string AlgorithmName
		{
			get
			{
				return "ISAAC";
			}
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000A38C0 File Offset: 0x000A38C0
		public virtual void Reset()
		{
			this.setKey(this.workingKey);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x000A38D0 File Offset: 0x000A38D0
		private void setKey(byte[] keyBytes)
		{
			this.workingKey = keyBytes;
			if (this.engineState == null)
			{
				this.engineState = new uint[IsaacEngine.stateArraySize];
			}
			if (this.results == null)
			{
				this.results = new uint[IsaacEngine.stateArraySize];
			}
			for (int i = 0; i < IsaacEngine.stateArraySize; i++)
			{
				this.engineState[i] = (this.results[i] = 0U);
			}
			this.a = (this.b = (this.c = 0U));
			this.index = 0;
			byte[] array = new byte[keyBytes.Length + (keyBytes.Length & 3)];
			Array.Copy(keyBytes, 0, array, 0, keyBytes.Length);
			for (int i = 0; i < array.Length; i += 4)
			{
				this.results[i >> 2] = Pack.LE_To_UInt32(array, i);
			}
			uint[] array2 = new uint[IsaacEngine.sizeL];
			for (int i = 0; i < IsaacEngine.sizeL; i++)
			{
				array2[i] = 2654435769U;
			}
			for (int i = 0; i < 4; i++)
			{
				this.mix(array2);
			}
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < IsaacEngine.stateArraySize; j += IsaacEngine.sizeL)
				{
					for (int k = 0; k < IsaacEngine.sizeL; k++)
					{
						uint[] array3;
						IntPtr intPtr;
						(array3 = array2)[(int)(intPtr = (IntPtr)k)] = array3[(int)intPtr] + ((i < 1) ? this.results[j + k] : this.engineState[j + k]);
					}
					this.mix(array2);
					for (int k = 0; k < IsaacEngine.sizeL; k++)
					{
						this.engineState[j + k] = array2[k];
					}
				}
			}
			this.isaac();
			this.initialised = true;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000A3A94 File Offset: 0x000A3A94
		private void isaac()
		{
			this.b += (this.c += 1U);
			for (int i = 0; i < IsaacEngine.stateArraySize; i++)
			{
				uint num = this.engineState[i];
				switch (i & 3)
				{
				case 0:
					this.a ^= this.a << 13;
					break;
				case 1:
					this.a ^= this.a >> 6;
					break;
				case 2:
					this.a ^= this.a << 2;
					break;
				case 3:
					this.a ^= this.a >> 16;
					break;
				}
				this.a += this.engineState[i + 128 & 255];
				uint num2 = this.engineState[i] = this.engineState[(int)(num >> 2 & 255U)] + this.a + this.b;
				this.results[i] = (this.b = this.engineState[(int)(num2 >> 10 & 255U)] + num);
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000A3BD8 File Offset: 0x000A3BD8
		private void mix(uint[] x)
		{
			x[0] = (x[0] ^ x[1] << 11);
			x[3] = x[3] + x[0];
			x[1] = x[1] + x[2];
			x[1] = (x[1] ^ x[2] >> 2);
			x[4] = x[4] + x[1];
			x[2] = x[2] + x[3];
			x[2] = (x[2] ^ x[3] << 8);
			x[5] = x[5] + x[2];
			x[3] = x[3] + x[4];
			x[3] = (x[3] ^ x[4] >> 16);
			x[6] = x[6] + x[3];
			x[4] = x[4] + x[5];
			x[4] = (x[4] ^ x[5] << 10);
			x[7] = x[7] + x[4];
			x[5] = x[5] + x[6];
			x[5] = (x[5] ^ x[6] >> 4);
			x[0] = x[0] + x[5];
			x[6] = x[6] + x[7];
			x[6] = (x[6] ^ x[7] << 8);
			x[1] = x[1] + x[6];
			x[7] = x[7] + x[0];
			x[7] = (x[7] ^ x[0] >> 9);
			x[2] = x[2] + x[7];
			x[0] = x[0] + x[1];
		}

		// Token: 0x04001320 RID: 4896
		private static readonly int sizeL = 8;

		// Token: 0x04001321 RID: 4897
		private static readonly int stateArraySize = IsaacEngine.sizeL << 5;

		// Token: 0x04001322 RID: 4898
		private uint[] engineState = null;

		// Token: 0x04001323 RID: 4899
		private uint[] results = null;

		// Token: 0x04001324 RID: 4900
		private uint a = 0U;

		// Token: 0x04001325 RID: 4901
		private uint b = 0U;

		// Token: 0x04001326 RID: 4902
		private uint c = 0U;

		// Token: 0x04001327 RID: 4903
		private int index = 0;

		// Token: 0x04001328 RID: 4904
		private byte[] keyStream = new byte[IsaacEngine.stateArraySize << 2];

		// Token: 0x04001329 RID: 4905
		private byte[] workingKey = null;

		// Token: 0x0400132A RID: 4906
		private bool initialised = false;
	}
}
