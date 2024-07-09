using System;
using System.Threading;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000490 RID: 1168
	public class ThreadedSeedGenerator
	{
		// Token: 0x06002406 RID: 9222 RVA: 0x000C9188 File Offset: 0x000C9188
		public byte[] GenerateSeed(int numBytes, bool fast)
		{
			return new ThreadedSeedGenerator.SeedGenerator().GenerateSeed(numBytes, fast);
		}

		// Token: 0x02000E14 RID: 3604
		private class SeedGenerator
		{
			// Token: 0x06008C2E RID: 35886 RVA: 0x002A1CBC File Offset: 0x002A1CBC
			private void Run(object ignored)
			{
				while (!this.stop)
				{
					this.counter++;
				}
			}

			// Token: 0x06008C2F RID: 35887 RVA: 0x002A1CE0 File Offset: 0x002A1CE0
			public byte[] GenerateSeed(int numBytes, bool fast)
			{
				ThreadPriority priority = Thread.CurrentThread.Priority;
				byte[] result;
				try
				{
					Thread.CurrentThread.Priority = ThreadPriority.Normal;
					result = this.DoGenerateSeed(numBytes, fast);
				}
				finally
				{
					Thread.CurrentThread.Priority = priority;
				}
				return result;
			}

			// Token: 0x06008C30 RID: 35888 RVA: 0x002A1D30 File Offset: 0x002A1D30
			private byte[] DoGenerateSeed(int numBytes, bool fast)
			{
				this.counter = 0;
				this.stop = false;
				byte[] array = new byte[numBytes];
				int num = 0;
				int num2 = fast ? numBytes : (numBytes * 8);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.Run));
				for (int i = 0; i < num2; i++)
				{
					while (this.counter == num)
					{
						try
						{
							Thread.Sleep(1);
						}
						catch (Exception)
						{
						}
					}
					num = this.counter;
					if (fast)
					{
						array[i] = (byte)num;
					}
					else
					{
						int num3 = i / 8;
						array[num3] = (byte)((int)array[num3] << 1 | (num & 1));
					}
				}
				this.stop = true;
				return array;
			}

			// Token: 0x04004159 RID: 16729
			private volatile int counter = 0;

			// Token: 0x0400415A RID: 16730
			private volatile bool stop = false;
		}
	}
}
