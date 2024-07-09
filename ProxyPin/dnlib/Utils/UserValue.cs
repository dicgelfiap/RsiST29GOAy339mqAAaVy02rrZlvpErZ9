using System;
using System.Diagnostics;
using dnlib.Threading;

namespace dnlib.Utils
{
	// Token: 0x02000743 RID: 1859
	[DebuggerDisplay("{value}")]
	internal struct UserValue<TValue>
	{
		// Token: 0x17000AFE RID: 2814
		// (set) Token: 0x06004116 RID: 16662 RVA: 0x0016293C File Offset: 0x0016293C
		public Lock Lock
		{
			set
			{
				this.theLock = value;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (set) Token: 0x06004117 RID: 16663 RVA: 0x00162948 File Offset: 0x00162948
		public Func<TValue> ReadOriginalValue
		{
			set
			{
				this.readOriginalValue = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x00162954 File Offset: 0x00162954
		// (set) Token: 0x06004119 RID: 16665 RVA: 0x001629D8 File Offset: 0x001629D8
		public TValue Value
		{
			get
			{
				Lock @lock = this.theLock;
				if (@lock != null)
				{
					@lock.EnterWriteLock();
				}
				TValue result;
				try
				{
					if (!this.isValueInitialized)
					{
						this.value = this.readOriginalValue();
						this.readOriginalValue = null;
						this.isValueInitialized = true;
					}
					result = this.value;
				}
				finally
				{
					Lock lock2 = this.theLock;
					if (lock2 != null)
					{
						lock2.ExitWriteLock();
					}
				}
				return result;
			}
			set
			{
				Lock @lock = this.theLock;
				if (@lock != null)
				{
					@lock.EnterWriteLock();
				}
				try
				{
					this.value = value;
					this.readOriginalValue = null;
					this.isUserValue = true;
					this.isValueInitialized = true;
				}
				finally
				{
					Lock lock2 = this.theLock;
					if (lock2 != null)
					{
						lock2.ExitWriteLock();
					}
				}
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x00162A48 File Offset: 0x00162A48
		public bool IsValueInitialized
		{
			get
			{
				Lock @lock = this.theLock;
				if (@lock != null)
				{
					@lock.EnterReadLock();
				}
				bool result;
				try
				{
					result = this.isValueInitialized;
				}
				finally
				{
					Lock lock2 = this.theLock;
					if (lock2 != null)
					{
						lock2.ExitReadLock();
					}
				}
				return result;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x00162AA4 File Offset: 0x00162AA4
		public bool IsUserValue
		{
			get
			{
				Lock @lock = this.theLock;
				if (@lock != null)
				{
					@lock.EnterReadLock();
				}
				bool result;
				try
				{
					result = this.isUserValue;
				}
				finally
				{
					Lock lock2 = this.theLock;
					if (lock2 != null)
					{
						lock2.ExitReadLock();
					}
				}
				return result;
			}
		}

		// Token: 0x04002295 RID: 8853
		private Lock theLock;

		// Token: 0x04002296 RID: 8854
		private Func<TValue> readOriginalValue;

		// Token: 0x04002297 RID: 8855
		private TValue value;

		// Token: 0x04002298 RID: 8856
		private bool isUserValue;

		// Token: 0x04002299 RID: 8857
		private bool isValueInitialized;
	}
}
