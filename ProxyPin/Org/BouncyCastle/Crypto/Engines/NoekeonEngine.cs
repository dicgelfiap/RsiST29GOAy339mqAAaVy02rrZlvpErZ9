using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000393 RID: 915
	public class NoekeonEngine : IBlockCipher
	{
		// Token: 0x06001CDE RID: 7390 RVA: 0x000A43BC File Offset: 0x000A43BC
		public NoekeonEngine()
		{
			this._initialised = false;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x000A43F0 File Offset: 0x000A43F0
		public virtual string AlgorithmName
		{
			get
			{
				return "Noekeon";
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x000A43F8 File Offset: 0x000A43F8
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000A43FC File Offset: 0x000A43FC
		public virtual int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000A4400 File Offset: 0x000A4400
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("Invalid parameters passed to Noekeon init - " + Platform.GetTypeName(parameters), "parameters");
			}
			this._forEncryption = forEncryption;
			this._initialised = true;
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.setKey(keyParameter.GetKey());
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000A4458 File Offset: 0x000A4458
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this._initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(input, inOff, 16, "input buffer too short");
			Check.OutputLength(output, outOff, 16, "output buffer too short");
			if (!this._forEncryption)
			{
				return this.decryptBlock(input, inOff, output, outOff);
			}
			return this.encryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000A44CC File Offset: 0x000A44CC
		public virtual void Reset()
		{
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000A44D0 File Offset: 0x000A44D0
		private void setKey(byte[] key)
		{
			this.subKeys[0] = Pack.BE_To_UInt32(key, 0);
			this.subKeys[1] = Pack.BE_To_UInt32(key, 4);
			this.subKeys[2] = Pack.BE_To_UInt32(key, 8);
			this.subKeys[3] = Pack.BE_To_UInt32(key, 12);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x000A4510 File Offset: 0x000A4510
		private int encryptBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.state[0] = Pack.BE_To_UInt32(input, inOff);
			this.state[1] = Pack.BE_To_UInt32(input, inOff + 4);
			this.state[2] = Pack.BE_To_UInt32(input, inOff + 8);
			this.state[3] = Pack.BE_To_UInt32(input, inOff + 12);
			int i;
			uint[] array;
			for (i = 0; i < 16; i++)
			{
				(array = this.state)[0] = (array[0] ^ NoekeonEngine.roundConstants[i]);
				this.theta(this.state, this.subKeys);
				this.pi1(this.state);
				this.gamma(this.state);
				this.pi2(this.state);
			}
			(array = this.state)[0] = (array[0] ^ NoekeonEngine.roundConstants[i]);
			this.theta(this.state, this.subKeys);
			Pack.UInt32_To_BE(this.state[0], output, outOff);
			Pack.UInt32_To_BE(this.state[1], output, outOff + 4);
			Pack.UInt32_To_BE(this.state[2], output, outOff + 8);
			Pack.UInt32_To_BE(this.state[3], output, outOff + 12);
			return 16;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000A4630 File Offset: 0x000A4630
		private int decryptBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.state[0] = Pack.BE_To_UInt32(input, inOff);
			this.state[1] = Pack.BE_To_UInt32(input, inOff + 4);
			this.state[2] = Pack.BE_To_UInt32(input, inOff + 8);
			this.state[3] = Pack.BE_To_UInt32(input, inOff + 12);
			Array.Copy(this.subKeys, 0, this.decryptKeys, 0, this.subKeys.Length);
			this.theta(this.decryptKeys, NoekeonEngine.nullVector);
			int i;
			uint[] array;
			for (i = 16; i > 0; i--)
			{
				this.theta(this.state, this.decryptKeys);
				(array = this.state)[0] = (array[0] ^ NoekeonEngine.roundConstants[i]);
				this.pi1(this.state);
				this.gamma(this.state);
				this.pi2(this.state);
			}
			this.theta(this.state, this.decryptKeys);
			(array = this.state)[0] = (array[0] ^ NoekeonEngine.roundConstants[i]);
			Pack.UInt32_To_BE(this.state[0], output, outOff);
			Pack.UInt32_To_BE(this.state[1], output, outOff + 4);
			Pack.UInt32_To_BE(this.state[2], output, outOff + 8);
			Pack.UInt32_To_BE(this.state[3], output, outOff + 12);
			return 16;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000A477C File Offset: 0x000A477C
		private void gamma(uint[] a)
		{
			a[1] = (a[1] ^ (~a[3] & ~a[2]));
			a[0] = (a[0] ^ (a[2] & a[1]));
			uint num = a[3];
			a[3] = a[0];
			a[0] = num;
			a[2] = (a[2] ^ (a[0] ^ a[1] ^ a[3]));
			a[1] = (a[1] ^ (~a[3] & ~a[2]));
			a[0] = (a[0] ^ (a[2] & a[1]));
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x000A47F4 File Offset: 0x000A47F4
		private void theta(uint[] a, uint[] k)
		{
			uint num = a[0] ^ a[2];
			num ^= (this.rotl(num, 8) ^ this.rotl(num, 24));
			a[1] = (a[1] ^ num);
			a[3] = (a[3] ^ num);
			for (int i = 0; i < 4; i++)
			{
				IntPtr intPtr;
				a[(int)(intPtr = (IntPtr)i)] = (a[(int)intPtr] ^ k[i]);
			}
			num = (a[1] ^ a[3]);
			num ^= (this.rotl(num, 8) ^ this.rotl(num, 24));
			a[0] = (a[0] ^ num);
			a[2] = (a[2] ^ num);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x000A4884 File Offset: 0x000A4884
		private void pi1(uint[] a)
		{
			a[1] = this.rotl(a[1], 1);
			a[2] = this.rotl(a[2], 5);
			a[3] = this.rotl(a[3], 2);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000A48BC File Offset: 0x000A48BC
		private void pi2(uint[] a)
		{
			a[1] = this.rotl(a[1], 31);
			a[2] = this.rotl(a[2], 27);
			a[3] = this.rotl(a[3], 30);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x000A48F8 File Offset: 0x000A48F8
		private uint rotl(uint x, int y)
		{
			return x << y | x >> 32 - y;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x000A490C File Offset: 0x000A490C
		// Note: this type is marked as 'beforefieldinit'.
		static NoekeonEngine()
		{
			uint[] array = new uint[4];
			NoekeonEngine.nullVector = array;
			NoekeonEngine.roundConstants = new uint[]
			{
				128U,
				27U,
				54U,
				108U,
				216U,
				171U,
				77U,
				154U,
				47U,
				94U,
				188U,
				99U,
				198U,
				151U,
				53U,
				106U,
				212U
			};
		}

		// Token: 0x0400132E RID: 4910
		private const int GenericSize = 16;

		// Token: 0x0400132F RID: 4911
		private static readonly uint[] nullVector;

		// Token: 0x04001330 RID: 4912
		private static readonly uint[] roundConstants;

		// Token: 0x04001331 RID: 4913
		private uint[] state = new uint[4];

		// Token: 0x04001332 RID: 4914
		private uint[] subKeys = new uint[4];

		// Token: 0x04001333 RID: 4915
		private uint[] decryptKeys = new uint[4];

		// Token: 0x04001334 RID: 4916
		private bool _initialised;

		// Token: 0x04001335 RID: 4917
		private bool _forEncryption;
	}
}
