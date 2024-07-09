using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D0D RID: 3341
	[ComVisible(true)]
	public class DeviceIndependentBitmap
	{
		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x060087B3 RID: 34739 RVA: 0x002901D8 File Offset: 0x002901D8
		// (set) Token: 0x060087B4 RID: 34740 RVA: 0x002901E0 File Offset: 0x002901E0
		public byte[] Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this._header));
				try
				{
					Marshal.Copy(this._data, 0, intPtr, Marshal.SizeOf(this._header));
					this._header = (Gdi32.BITMAPINFOHEADER)Marshal.PtrToStructure(intPtr, typeof(Gdi32.BITMAPINFOHEADER));
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x060087B5 RID: 34741 RVA: 0x00290260 File Offset: 0x00290260
		public Gdi32.BITMAPINFOHEADER Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x060087B6 RID: 34742 RVA: 0x00290268 File Offset: 0x00290268
		public int Size
		{
			get
			{
				return this._data.Length;
			}
		}

		// Token: 0x060087B7 RID: 34743 RVA: 0x00290274 File Offset: 0x00290274
		public DeviceIndependentBitmap()
		{
		}

		// Token: 0x060087B8 RID: 34744 RVA: 0x0029027C File Offset: 0x0029027C
		public DeviceIndependentBitmap(byte[] data)
		{
			this.Data = data;
		}

		// Token: 0x060087B9 RID: 34745 RVA: 0x0029028C File Offset: 0x0029028C
		public DeviceIndependentBitmap(DeviceIndependentBitmap image)
		{
			this._data = new byte[image._data.Length];
			Buffer.BlockCopy(image._data, 0, this._data, 0, image._data.Length);
			this._header = image._header;
		}

		// Token: 0x060087BA RID: 34746 RVA: 0x002902E0 File Offset: 0x002902E0
		internal void Read(IntPtr lpData, uint size)
		{
			this._header = (Gdi32.BITMAPINFOHEADER)Marshal.PtrToStructure(lpData, typeof(Gdi32.BITMAPINFOHEADER));
			this._data = new byte[size];
			Marshal.Copy(lpData, this._data, 0, this._data.Length);
		}

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x060087BB RID: 34747 RVA: 0x00290320 File Offset: 0x00290320
		private int MaskImageSize
		{
			get
			{
				return this._header.biHeight / 2 * this.GetDIBRowWidth(this._header.biWidth);
			}
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x060087BC RID: 34748 RVA: 0x00290344 File Offset: 0x00290344
		private int XorImageSize
		{
			get
			{
				return this._header.biHeight / 2 * this.GetDIBRowWidth(this._header.biWidth * (int)this._header.biBitCount * (int)this._header.biPlanes);
			}
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x060087BD RID: 34749 RVA: 0x00290380 File Offset: 0x00290380
		private int XorImageIndex
		{
			get
			{
				return (int)((long)Marshal.SizeOf(this._header) + (long)((ulong)this.ColorCount * (ulong)((long)Marshal.SizeOf(default(Gdi32.RGBQUAD)))));
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x060087BE RID: 34750 RVA: 0x002903C0 File Offset: 0x002903C0
		private uint ColorCount
		{
			get
			{
				if (this._header.biClrUsed != 0U)
				{
					return this._header.biClrUsed;
				}
				if (this._header.biBitCount <= 8)
				{
					return 1U << (int)this._header.biBitCount;
				}
				return 0U;
			}
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x060087BF RID: 34751 RVA: 0x00290410 File Offset: 0x00290410
		private int MaskImageIndex
		{
			get
			{
				return this.XorImageIndex + this.XorImageSize;
			}
		}

		// Token: 0x060087C0 RID: 34752 RVA: 0x00290420 File Offset: 0x00290420
		private int GetDIBRowWidth(int width)
		{
			return (width + 31) / 32 * 4;
		}

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x060087C1 RID: 34753 RVA: 0x0029042C File Offset: 0x0029042C
		public Bitmap Mask
		{
			get
			{
				if (this._mask == null)
				{
					IntPtr intPtr = IntPtr.Zero;
					IntPtr intPtr2 = IntPtr.Zero;
					IntPtr hgdiobj = IntPtr.Zero;
					IntPtr intPtr3 = IntPtr.Zero;
					IntPtr intPtr4 = IntPtr.Zero;
					try
					{
						intPtr = Gdi32.CreateCompatibleDC(IntPtr.Zero);
						intPtr2 = Gdi32.CreateCompatibleBitmap(intPtr, this._header.biWidth, this._header.biHeight / 2);
						hgdiobj = Gdi32.SelectObject(intPtr, intPtr2);
						intPtr3 = Marshal.AllocCoTaskMem((int)(this._header.biSize + (uint)(Marshal.SizeOf(default(Gdi32.RGBQUAD)) * 2)));
						Marshal.WriteInt32(intPtr3, Marshal.SizeOf(this._header));
						Marshal.WriteInt32(intPtr3, 4, this._header.biWidth);
						Marshal.WriteInt32(intPtr3, 8, this._header.biHeight / 2);
						Marshal.WriteInt16(intPtr3, 12, 1);
						Marshal.WriteInt16(intPtr3, 14, 1);
						Marshal.WriteInt32(intPtr3, 16, 0);
						Marshal.WriteInt32(intPtr3, 20, 0);
						Marshal.WriteInt32(intPtr3, 24, 0);
						Marshal.WriteInt32(intPtr3, 28, 0);
						Marshal.WriteInt32(intPtr3, 32, 0);
						Marshal.WriteInt32(intPtr3, 36, 0);
						Marshal.WriteInt32(intPtr3, 40, 0);
						Marshal.WriteByte(intPtr3, 44, byte.MaxValue);
						Marshal.WriteByte(intPtr3, 45, byte.MaxValue);
						Marshal.WriteByte(intPtr3, 46, byte.MaxValue);
						Marshal.WriteByte(intPtr3, 47, 0);
						intPtr4 = Marshal.AllocCoTaskMem(this.MaskImageSize);
						Marshal.Copy(this._data, this.MaskImageIndex, intPtr4, this.MaskImageSize);
						if (Gdi32.SetDIBitsToDevice(intPtr, 0, 0, (uint)this._header.biWidth, (uint)(this._header.biHeight / 2), 0, 0, 0U, (uint)(this._header.biHeight / 2), intPtr4, intPtr3, 0U) == 0)
						{
							throw new Win32Exception(Marshal.GetLastWin32Error());
						}
						this._mask = System.Drawing.Image.FromHbitmap(intPtr2);
					}
					finally
					{
						if (intPtr4 != IntPtr.Zero)
						{
							Marshal.FreeCoTaskMem(intPtr4);
						}
						if (intPtr3 != IntPtr.Zero)
						{
							Marshal.FreeCoTaskMem(intPtr3);
						}
						if (intPtr != IntPtr.Zero)
						{
							Gdi32.SelectObject(intPtr, hgdiobj);
						}
						if (intPtr != IntPtr.Zero)
						{
							Gdi32.DeleteObject(intPtr);
						}
					}
				}
				return this._mask;
			}
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x060087C2 RID: 34754 RVA: 0x00290678 File Offset: 0x00290678
		public Bitmap Color
		{
			get
			{
				if (this._color == null)
				{
					IntPtr intPtr = IntPtr.Zero;
					IntPtr intPtr2 = IntPtr.Zero;
					IntPtr intPtr3 = IntPtr.Zero;
					IntPtr hgdiobj = IntPtr.Zero;
					IntPtr intPtr4 = IntPtr.Zero;
					IntPtr intPtr5 = IntPtr.Zero;
					try
					{
						intPtr = User32.GetDC(IntPtr.Zero);
						intPtr2 = Gdi32.CreateCompatibleDC(intPtr);
						intPtr3 = Gdi32.CreateCompatibleBitmap(intPtr, this._header.biWidth, this._header.biHeight / 2);
						hgdiobj = Gdi32.SelectObject(intPtr2, intPtr3);
						intPtr4 = Marshal.AllocCoTaskMem(this.XorImageIndex);
						Marshal.Copy(this._data, 0, intPtr4, this.XorImageIndex);
						Marshal.WriteInt32(intPtr4, 8, this._header.biHeight / 2);
						intPtr5 = Marshal.AllocCoTaskMem(this.XorImageSize);
						Marshal.Copy(this._data, this.XorImageIndex, intPtr5, this.XorImageSize);
						if (Gdi32.SetDIBitsToDevice(intPtr2, 0, 0, (uint)this._header.biWidth, (uint)(this._header.biHeight / 2), 0, 0, 0U, (uint)(this._header.biHeight / 2), intPtr5, intPtr4, 0U) == 0)
						{
							throw new Win32Exception(Marshal.GetLastWin32Error());
						}
						this._color = System.Drawing.Image.FromHbitmap(intPtr3);
					}
					finally
					{
						if (intPtr != IntPtr.Zero)
						{
							Gdi32.DeleteDC(intPtr);
						}
						if (intPtr5 != IntPtr.Zero)
						{
							Marshal.FreeCoTaskMem(intPtr5);
						}
						if (intPtr4 != IntPtr.Zero)
						{
							Marshal.FreeCoTaskMem(intPtr4);
						}
						if (intPtr2 != IntPtr.Zero)
						{
							Gdi32.SelectObject(intPtr2, hgdiobj);
						}
						if (intPtr2 != IntPtr.Zero)
						{
							Gdi32.DeleteObject(intPtr2);
						}
					}
				}
				return this._color;
			}
		}

		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x060087C3 RID: 34755 RVA: 0x00290830 File Offset: 0x00290830
		public Bitmap Image
		{
			get
			{
				if (this._image == null)
				{
					IntPtr intPtr = IntPtr.Zero;
					IntPtr zero = IntPtr.Zero;
					IntPtr intPtr2 = IntPtr.Zero;
					IntPtr intPtr3 = IntPtr.Zero;
					try
					{
						intPtr = User32.GetDC(IntPtr.Zero);
						if (intPtr == IntPtr.Zero)
						{
							throw new Win32Exception(Marshal.GetLastWin32Error());
						}
						Gdi32.BITMAPINFO bitmapinfo = default(Gdi32.BITMAPINFO);
						bitmapinfo.bmiHeader = this._header;
						intPtr2 = Gdi32.CreateCompatibleDC(intPtr);
						intPtr3 = Gdi32.CreateDIBSection(intPtr2, ref bitmapinfo, 0U, out zero, IntPtr.Zero, 0U);
						Marshal.Copy(this._data, this.XorImageIndex, zero, this.XorImageSize);
						this._image = System.Drawing.Image.FromHbitmap(intPtr3);
					}
					finally
					{
						if (intPtr != IntPtr.Zero)
						{
							User32.ReleaseDC(IntPtr.Zero, intPtr);
						}
						if (intPtr3 != IntPtr.Zero)
						{
							Gdi32.DeleteObject(intPtr3);
						}
						if (intPtr2 != IntPtr.Zero)
						{
							Gdi32.DeleteDC(intPtr2);
						}
					}
				}
				return this._image;
			}
		}

		// Token: 0x04003E85 RID: 16005
		private Gdi32.BITMAPINFOHEADER _header;

		// Token: 0x04003E86 RID: 16006
		private byte[] _data;

		// Token: 0x04003E87 RID: 16007
		private Bitmap _mask;

		// Token: 0x04003E88 RID: 16008
		private Bitmap _color;

		// Token: 0x04003E89 RID: 16009
		private Bitmap _image;
	}
}
