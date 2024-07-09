using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020003A1 RID: 929
	public class RsaEngine : IAsymmetricBlockCipher
	{
		// Token: 0x06001D7C RID: 7548 RVA: 0x000A82CC File Offset: 0x000A82CC
		public RsaEngine() : this(new RsaCoreEngine())
		{
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000A82DC File Offset: 0x000A82DC
		public RsaEngine(IRsa rsa)
		{
			this.core = rsa;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x000A82EC File Offset: 0x000A82EC
		public virtual string AlgorithmName
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000A82F4 File Offset: 0x000A82F4
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.core.Init(forEncryption, parameters);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x000A8304 File Offset: 0x000A8304
		public virtual int GetInputBlockSize()
		{
			return this.core.GetInputBlockSize();
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000A8314 File Offset: 0x000A8314
		public virtual int GetOutputBlockSize()
		{
			return this.core.GetOutputBlockSize();
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x000A8324 File Offset: 0x000A8324
		public virtual byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen)
		{
			BigInteger input = this.core.ConvertInput(inBuf, inOff, inLen);
			BigInteger result = this.core.ProcessBlock(input);
			return this.core.ConvertOutput(result);
		}

		// Token: 0x04001381 RID: 4993
		private readonly IRsa core;
	}
}
