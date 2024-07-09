using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D08 RID: 3336
	internal sealed class Aligned : IDisposable
	{
		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x0600879B RID: 34715 RVA: 0x0028FD7C File Offset: 0x0028FD7C
		private bool Disposed
		{
			get
			{
				return this._ptr == IntPtr.Zero;
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x0600879C RID: 34716 RVA: 0x0028FD90 File Offset: 0x0028FD90
		public IntPtr Ptr
		{
			get
			{
				if (this.Disposed)
				{
					throw new ObjectDisposedException("Aligned");
				}
				return this._ptr;
			}
		}

		// Token: 0x0600879D RID: 34717 RVA: 0x0028FDB0 File Offset: 0x0028FDB0
		public Aligned(IntPtr lp, int size)
		{
			if (lp == IntPtr.Zero)
			{
				throw new ArgumentException("Cannot align a null pointer.", "lp");
			}
			if (lp.ToInt64() % 8L == 0L)
			{
				this._ptr = lp;
				this.allocated = false;
				return;
			}
			this._ptr = Marshal.AllocHGlobal(size);
			this.allocated = true;
			Kernel32.MoveMemory(this._ptr, lp, (uint)size);
		}

		// Token: 0x0600879E RID: 34718 RVA: 0x0028FE28 File Offset: 0x0028FE28
		public void Dispose()
		{
			if (!this.allocated || this.Disposed)
			{
				return;
			}
			Marshal.FreeHGlobal(this._ptr);
			this._ptr = IntPtr.Zero;
		}

		// Token: 0x04003E7E RID: 15998
		private IntPtr _ptr;

		// Token: 0x04003E7F RID: 15999
		private bool allocated;
	}
}
