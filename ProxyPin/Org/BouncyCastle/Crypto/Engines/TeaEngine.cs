using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003A9 RID: 937
	public class TeaEngine : IBlockCipher
	{
		// Token: 0x06001DE0 RID: 7648 RVA: 0x000AC484 File Offset: 0x000AC484
		public TeaEngine()
		{
			this._initialised = false;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000AC494 File Offset: 0x000AC494
		public virtual string AlgorithmName
		{
			get
			{
				return "TEA";
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x000AC49C File Offset: 0x000AC49C
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000AC4A0 File Offset: 0x000AC4A0
		public virtual int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000AC4A4 File Offset: 0x000AC4A4
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to TEA init - " + Platform.GetTypeName(parameters));
			}
			this._forEncryption = forEncryption;
			this._initialised = true;
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.setKey(keyParameter.GetKey());
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000AC4F8 File Offset: 0x000AC4F8
		public virtual int ProcessBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			if (!this._initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(inBytes, inOff, 8, "input buffer too short");
			Check.OutputLength(outBytes, outOff, 8, "output buffer too short");
			if (!this._forEncryption)
			{
				return this.decryptBlock(inBytes, inOff, outBytes, outOff);
			}
			return this.encryptBlock(inBytes, inOff, outBytes, outOff);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000AC568 File Offset: 0x000AC568
		public virtual void Reset()
		{
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x000AC56C File Offset: 0x000AC56C
		private void setKey(byte[] key)
		{
			this._a = Pack.BE_To_UInt32(key, 0);
			this._b = Pack.BE_To_UInt32(key, 4);
			this._c = Pack.BE_To_UInt32(key, 8);
			this._d = Pack.BE_To_UInt32(key, 12);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000AC5A4 File Offset: 0x000AC5A4
		private int encryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			uint num3 = 0U;
			for (int num4 = 0; num4 != 32; num4++)
			{
				num3 += 2654435769U;
				num += ((num2 << 4) + this._a ^ num2 + num3 ^ (num2 >> 5) + this._b);
				num2 += ((num << 4) + this._c ^ num + num3 ^ (num >> 5) + this._d);
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000AC630 File Offset: 0x000AC630
		private int decryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			uint num3 = 3337565984U;
			for (int num4 = 0; num4 != 32; num4++)
			{
				num2 -= ((num << 4) + this._c ^ num + num3 ^ (num >> 5) + this._d);
				num -= ((num2 << 4) + this._a ^ num2 + num3 ^ (num2 >> 5) + this._b);
				num3 -= 2654435769U;
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x040013A5 RID: 5029
		private const int rounds = 32;

		// Token: 0x040013A6 RID: 5030
		private const int block_size = 8;

		// Token: 0x040013A7 RID: 5031
		private const uint delta = 2654435769U;

		// Token: 0x040013A8 RID: 5032
		private const uint d_sum = 3337565984U;

		// Token: 0x040013A9 RID: 5033
		private uint _a;

		// Token: 0x040013AA RID: 5034
		private uint _b;

		// Token: 0x040013AB RID: 5035
		private uint _c;

		// Token: 0x040013AC RID: 5036
		private uint _d;

		// Token: 0x040013AD RID: 5037
		private bool _initialised;

		// Token: 0x040013AE RID: 5038
		private bool _forEncryption;
	}
}
