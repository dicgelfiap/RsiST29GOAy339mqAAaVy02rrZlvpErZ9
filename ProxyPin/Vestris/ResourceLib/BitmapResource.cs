using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D0A RID: 3338
	[ComVisible(true)]
	public class BitmapResource : Resource
	{
		// Token: 0x060087A1 RID: 34721 RVA: 0x0028FF20 File Offset: 0x0028FF20
		public BitmapResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x060087A2 RID: 34722 RVA: 0x0028FF34 File Offset: 0x0028FF34
		public BitmapResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_BITMAP), new ResourceId(1U), 0, 0)
		{
		}

		// Token: 0x060087A3 RID: 34723 RVA: 0x0028FF54 File Offset: 0x0028FF54
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			byte[] array = new byte[this._size];
			Marshal.Copy(lpRes, array, 0, array.Length);
			this._bitmap = new DeviceIndependentBitmap(array);
			return new IntPtr(lpRes.ToInt64() + (long)this._size);
		}

		// Token: 0x060087A4 RID: 34724 RVA: 0x0028FF9C File Offset: 0x0028FF9C
		internal override void Write(BinaryWriter w)
		{
			w.Write(this._bitmap.Data);
		}

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x060087A5 RID: 34725 RVA: 0x0028FFB0 File Offset: 0x0028FFB0
		// (set) Token: 0x060087A6 RID: 34726 RVA: 0x0028FFB8 File Offset: 0x0028FFB8
		public DeviceIndependentBitmap Bitmap
		{
			get
			{
				return this._bitmap;
			}
			set
			{
				this._bitmap = value;
			}
		}

		// Token: 0x04003E82 RID: 16002
		private DeviceIndependentBitmap _bitmap;
	}
}
