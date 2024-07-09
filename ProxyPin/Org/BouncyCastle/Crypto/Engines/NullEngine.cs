using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000394 RID: 916
	public class NullEngine : IBlockCipher
	{
		// Token: 0x06001CEF RID: 7407 RVA: 0x000A494C File Offset: 0x000A494C
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.initialised = true;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x000A4958 File Offset: 0x000A4958
		public virtual string AlgorithmName
		{
			get
			{
				return "Null";
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x000A4960 File Offset: 0x000A4960
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000A4964 File Offset: 0x000A4964
		public virtual int GetBlockSize()
		{
			return 1;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000A4968 File Offset: 0x000A4968
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.initialised)
			{
				throw new InvalidOperationException("Null engine not initialised");
			}
			Check.DataLength(input, inOff, 1, "input buffer too short");
			Check.OutputLength(output, outOff, 1, "output buffer too short");
			for (int i = 0; i < 1; i++)
			{
				output[outOff + i] = input[inOff + i];
			}
			return 1;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000A49C8 File Offset: 0x000A49C8
		public virtual void Reset()
		{
		}

		// Token: 0x04001336 RID: 4918
		private const int BlockSize = 1;

		// Token: 0x04001337 RID: 4919
		private bool initialised;
	}
}
