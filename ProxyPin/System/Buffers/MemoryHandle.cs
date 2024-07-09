using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000CF0 RID: 3312
	[ComVisible(true)]
	public struct MemoryHandle : IDisposable
	{
		// Token: 0x060085E6 RID: 34278 RVA: 0x0027351C File Offset: 0x0027351C
		[CLSCompliant(false)]
		public unsafe MemoryHandle(void* pointer, GCHandle handle = default(GCHandle), IPinnable pinnable = null)
		{
			this._pointer = pointer;
			this._handle = handle;
			this._pinnable = pinnable;
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x060085E7 RID: 34279 RVA: 0x00273534 File Offset: 0x00273534
		[CLSCompliant(false)]
		public unsafe void* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x060085E8 RID: 34280 RVA: 0x0027353C File Offset: 0x0027353C
		public void Dispose()
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
			if (this._pinnable != null)
			{
				this._pinnable.Unpin();
				this._pinnable = null;
			}
			this._pointer = null;
		}

		// Token: 0x04003DE5 RID: 15845
		private unsafe void* _pointer;

		// Token: 0x04003DE6 RID: 15846
		private GCHandle _handle;

		// Token: 0x04003DE7 RID: 15847
		private IPinnable _pinnable;
	}
}
