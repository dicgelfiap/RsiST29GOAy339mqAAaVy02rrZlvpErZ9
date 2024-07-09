using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000489 RID: 1161
	public class DigestRandomGenerator : IRandomGenerator
	{
		// Token: 0x060023CD RID: 9165 RVA: 0x000C86F8 File Offset: 0x000C86F8
		public DigestRandomGenerator(IDigest digest)
		{
			this.digest = digest;
			this.seed = new byte[digest.GetDigestSize()];
			this.seedCounter = 1L;
			this.state = new byte[digest.GetDigestSize()];
			this.stateCounter = 1L;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000C8748 File Offset: 0x000C8748
		public void AddSeedMaterial(byte[] inSeed)
		{
			lock (this)
			{
				this.DigestUpdate(inSeed);
				this.DigestUpdate(this.seed);
				this.DigestDoFinal(this.seed);
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000C8798 File Offset: 0x000C8798
		public void AddSeedMaterial(long rSeed)
		{
			lock (this)
			{
				this.DigestAddCounter(rSeed);
				this.DigestUpdate(this.seed);
				this.DigestDoFinal(this.seed);
			}
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000C87E8 File Offset: 0x000C87E8
		public void NextBytes(byte[] bytes)
		{
			this.NextBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000C87F8 File Offset: 0x000C87F8
		public void NextBytes(byte[] bytes, int start, int len)
		{
			lock (this)
			{
				int num = 0;
				this.GenerateState();
				int num2 = start + len;
				for (int i = start; i < num2; i++)
				{
					if (num == this.state.Length)
					{
						this.GenerateState();
						num = 0;
					}
					bytes[i] = this.state[num++];
				}
			}
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000C886C File Offset: 0x000C886C
		private void CycleSeed()
		{
			this.DigestUpdate(this.seed);
			long seedVal;
			this.seedCounter = (seedVal = this.seedCounter) + 1L;
			this.DigestAddCounter(seedVal);
			this.DigestDoFinal(this.seed);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000C88B0 File Offset: 0x000C88B0
		private void GenerateState()
		{
			long seedVal;
			this.stateCounter = (seedVal = this.stateCounter) + 1L;
			this.DigestAddCounter(seedVal);
			this.DigestUpdate(this.state);
			this.DigestUpdate(this.seed);
			this.DigestDoFinal(this.state);
			if (this.stateCounter % 10L == 0L)
			{
				this.CycleSeed();
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000C8914 File Offset: 0x000C8914
		private void DigestAddCounter(long seedVal)
		{
			byte[] array = new byte[8];
			Pack.UInt64_To_LE((ulong)seedVal, array);
			this.digest.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000C8944 File Offset: 0x000C8944
		private void DigestUpdate(byte[] inSeed)
		{
			this.digest.BlockUpdate(inSeed, 0, inSeed.Length);
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000C8958 File Offset: 0x000C8958
		private void DigestDoFinal(byte[] result)
		{
			this.digest.DoFinal(result, 0);
		}

		// Token: 0x040016B8 RID: 5816
		private const long CYCLE_COUNT = 10L;

		// Token: 0x040016B9 RID: 5817
		private long stateCounter;

		// Token: 0x040016BA RID: 5818
		private long seedCounter;

		// Token: 0x040016BB RID: 5819
		private IDigest digest;

		// Token: 0x040016BC RID: 5820
		private byte[] state;

		// Token: 0x040016BD RID: 5821
		private byte[] seed;
	}
}
