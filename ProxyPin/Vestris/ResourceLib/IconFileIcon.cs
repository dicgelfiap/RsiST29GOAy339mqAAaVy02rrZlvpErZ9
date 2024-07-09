using System;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D1F RID: 3359
	[ComVisible(true)]
	public class IconFileIcon
	{
		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x0600889A RID: 34970 RVA: 0x00292A5C File Offset: 0x00292A5C
		public Kernel32.FILEGRPICONDIRENTRY Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x0600889B RID: 34971 RVA: 0x00292A64 File Offset: 0x00292A64
		// (set) Token: 0x0600889C RID: 34972 RVA: 0x00292A6C File Offset: 0x00292A6C
		public DeviceIndependentBitmap Image
		{
			get
			{
				return this._image;
			}
			set
			{
				this._image = value;
			}
		}

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x0600889E RID: 34974 RVA: 0x00292A8C File Offset: 0x00292A8C
		public byte Width
		{
			get
			{
				return this._header.bWidth;
			}
		}

		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x0600889F RID: 34975 RVA: 0x00292A9C File Offset: 0x00292A9C
		public byte Height
		{
			get
			{
				return this._header.bHeight;
			}
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x060088A0 RID: 34976 RVA: 0x00292AAC File Offset: 0x00292AAC
		public uint ImageSize
		{
			get
			{
				return this._header.dwImageSize;
			}
		}

		// Token: 0x060088A1 RID: 34977 RVA: 0x00292ABC File Offset: 0x00292ABC
		internal IntPtr Read(IntPtr lpData, IntPtr lpAllData)
		{
			this._header = (Kernel32.FILEGRPICONDIRENTRY)Marshal.PtrToStructure(lpData, typeof(Kernel32.FILEGRPICONDIRENTRY));
			IntPtr lpData2 = new IntPtr(lpAllData.ToInt64() + (long)((ulong)this._header.dwFileOffset));
			this._image.Read(lpData2, this._header.dwImageSize);
			return new IntPtr(lpData.ToInt64() + (long)Marshal.SizeOf(this._header));
		}

		// Token: 0x060088A2 RID: 34978 RVA: 0x00292B38 File Offset: 0x00292B38
		public override string ToString()
		{
			return string.Format("{0}x{1}", this.Width, this.Height);
		}

		// Token: 0x04003EA7 RID: 16039
		private Kernel32.FILEGRPICONDIRENTRY _header;

		// Token: 0x04003EA8 RID: 16040
		private DeviceIndependentBitmap _image = new DeviceIndependentBitmap();
	}
}
