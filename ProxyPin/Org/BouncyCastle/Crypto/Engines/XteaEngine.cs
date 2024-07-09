using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003B0 RID: 944
	public class XteaEngine : IBlockCipher
	{
		// Token: 0x06001E27 RID: 7719 RVA: 0x000B08E8 File Offset: 0x000B08E8
		public XteaEngine()
		{
			this._initialised = false;
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x000B0920 File Offset: 0x000B0920
		public virtual string AlgorithmName
		{
			get
			{
				return "XTEA";
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x000B0928 File Offset: 0x000B0928
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000B092C File Offset: 0x000B092C
		public virtual int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000B0930 File Offset: 0x000B0930
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

		// Token: 0x06001E2C RID: 7724 RVA: 0x000B0984 File Offset: 0x000B0984
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

		// Token: 0x06001E2D RID: 7725 RVA: 0x000B09F4 File Offset: 0x000B09F4
		public virtual void Reset()
		{
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000B09F8 File Offset: 0x000B09F8
		private void setKey(byte[] key)
		{
			int i;
			int num = i = 0;
			while (i < 4)
			{
				this._S[i] = Pack.BE_To_UInt32(key, num);
				i++;
				num += 4;
			}
			num = (i = 0);
			while (i < 32)
			{
				this._sum0[i] = (uint)(num + (int)this._S[num & 3]);
				num += -1640531527;
				this._sum1[i] = (uint)(num + (int)this._S[num >> 11 & 3]);
				i++;
			}
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000B0A74 File Offset: 0x000B0A74
		private int encryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			for (int i = 0; i < 32; i++)
			{
				num += ((num2 << 4 ^ num2 >> 5) + num2 ^ this._sum0[i]);
				num2 += ((num << 4 ^ num >> 5) + num ^ this._sum1[i]);
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000B0AE8 File Offset: 0x000B0AE8
		private int decryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			for (int i = 31; i >= 0; i--)
			{
				num2 -= ((num << 4 ^ num >> 5) + num ^ this._sum1[i]);
				num -= ((num2 << 4 ^ num2 >> 5) + num2 ^ this._sum0[i]);
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x040013F6 RID: 5110
		private const int rounds = 32;

		// Token: 0x040013F7 RID: 5111
		private const int block_size = 8;

		// Token: 0x040013F8 RID: 5112
		private const int delta = -1640531527;

		// Token: 0x040013F9 RID: 5113
		private uint[] _S = new uint[4];

		// Token: 0x040013FA RID: 5114
		private uint[] _sum0 = new uint[32];

		// Token: 0x040013FB RID: 5115
		private uint[] _sum1 = new uint[32];

		// Token: 0x040013FC RID: 5116
		private bool _initialised;

		// Token: 0x040013FD RID: 5117
		private bool _forEncryption;
	}
}
