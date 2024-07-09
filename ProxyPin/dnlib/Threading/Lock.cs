using System;
using System.Threading;

namespace dnlib.Threading
{
	// Token: 0x02000746 RID: 1862
	internal class Lock
	{
		// Token: 0x06004120 RID: 16672 RVA: 0x00162B20 File Offset: 0x00162B20
		public static Lock Create()
		{
			return new Lock();
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x00162B28 File Offset: 0x00162B28
		private Lock()
		{
			this.lockObj = new object();
			this.recurseCount = 0;
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x00162B44 File Offset: 0x00162B44
		public void EnterReadLock()
		{
			Monitor.Enter(this.lockObj);
			if (this.recurseCount != 0)
			{
				Monitor.Exit(this.lockObj);
				throw new LockException("Recursive locks aren't supported");
			}
			this.recurseCount++;
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x00162B80 File Offset: 0x00162B80
		public void ExitReadLock()
		{
			if (this.recurseCount <= 0)
			{
				throw new LockException("Too many exit lock method calls");
			}
			this.recurseCount--;
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x00162BB4 File Offset: 0x00162BB4
		public void EnterWriteLock()
		{
			Monitor.Enter(this.lockObj);
			if (this.recurseCount != 0)
			{
				Monitor.Exit(this.lockObj);
				throw new LockException("Recursive locks aren't supported");
			}
			this.recurseCount--;
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x00162BF0 File Offset: 0x00162BF0
		public void ExitWriteLock()
		{
			if (this.recurseCount >= 0)
			{
				throw new LockException("Too many exit lock method calls");
			}
			this.recurseCount++;
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x0400229A RID: 8858
		private readonly object lockObj;

		// Token: 0x0400229B RID: 8859
		private int recurseCount;
	}
}
