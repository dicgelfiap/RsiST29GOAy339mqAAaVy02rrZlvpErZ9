using System;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D20 RID: 3360
	[ComVisible(true)]
	public class IconImageResource : Resource
	{
		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x060088A3 RID: 34979 RVA: 0x00292B5C File Offset: 0x00292B5C
		// (set) Token: 0x060088A4 RID: 34980 RVA: 0x00292B64 File Offset: 0x00292B64
		public Kernel32.GRPICONDIRENTRY Header
		{
			get
			{
				return this._header;
			}
			set
			{
				this._header = value;
			}
		}

		// Token: 0x17001D1B RID: 7451
		// (get) Token: 0x060088A5 RID: 34981 RVA: 0x00292B70 File Offset: 0x00292B70
		// (set) Token: 0x060088A6 RID: 34982 RVA: 0x00292B80 File Offset: 0x00292B80
		public ushort Id
		{
			get
			{
				return this._header.nID;
			}
			set
			{
				this._header.nID = value;
			}
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x060088A7 RID: 34983 RVA: 0x00292B90 File Offset: 0x00292B90
		// (set) Token: 0x060088A8 RID: 34984 RVA: 0x00292B98 File Offset: 0x00292B98
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

		// Token: 0x060088A9 RID: 34985 RVA: 0x00292BA4 File Offset: 0x00292BA4
		internal IconImageResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x060088AA RID: 34986 RVA: 0x00292BC0 File Offset: 0x00292BC0
		public IconImageResource(ResourceId type) : base(IntPtr.Zero, IntPtr.Zero, type, new ResourceId(IntPtr.Zero), ResourceUtil.NEUTRALLANGID, Marshal.SizeOf(typeof(Kernel32.GRPICONDIRENTRY)))
		{
		}

		// Token: 0x060088AB RID: 34987 RVA: 0x00292BFC File Offset: 0x00292BFC
		public IconImageResource(IconFileIcon icon, ResourceId type, ResourceId name, ushort language)
		{
			this._name = name;
			this._type = type;
			this._language = language;
			this._header.bColors = icon.Header.bColors;
			this._header.bHeight = icon.Header.bHeight;
			this._header.bReserved = icon.Header.bReserved;
			this._header.bWidth = icon.Header.bWidth;
			this._header.dwImageSize = icon.Header.dwImageSize;
			this._header.wBitsPerPixel = icon.Header.wBitsPerPixel;
			this._header.wPlanes = icon.Header.wPlanes;
			this._header.nID = (ushort)((int)name.Id);
			this._image = new DeviceIndependentBitmap(icon.Image);
		}

		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x060088AC RID: 34988 RVA: 0x00292CF8 File Offset: 0x00292CF8
		// (set) Token: 0x060088AD RID: 34989 RVA: 0x00292D08 File Offset: 0x00292D08
		public byte Width
		{
			get
			{
				return this._header.bWidth;
			}
			set
			{
				this._header.bWidth = value;
			}
		}

		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x060088AE RID: 34990 RVA: 0x00292D18 File Offset: 0x00292D18
		// (set) Token: 0x060088AF RID: 34991 RVA: 0x00292D28 File Offset: 0x00292D28
		public byte Height
		{
			get
			{
				return this._header.bHeight;
			}
			set
			{
				this._header.bHeight = value;
			}
		}

		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x060088B0 RID: 34992 RVA: 0x00292D38 File Offset: 0x00292D38
		// (set) Token: 0x060088B1 RID: 34993 RVA: 0x00292D48 File Offset: 0x00292D48
		public uint ImageSize
		{
			get
			{
				return this._header.dwImageSize;
			}
			set
			{
				this._header.dwImageSize = value;
			}
		}

		// Token: 0x060088B2 RID: 34994 RVA: 0x00292D58 File Offset: 0x00292D58
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			this._header = (Kernel32.GRPICONDIRENTRY)Marshal.PtrToStructure(lpRes, typeof(Kernel32.GRPICONDIRENTRY));
			IntPtr intPtr = Kernel32.FindResourceEx(hModule, this._type.Id, (IntPtr)((int)this._header.nID), this._language);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			IntPtr intPtr2 = Kernel32.LoadResource(hModule, intPtr);
			if (intPtr2 == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			IntPtr intPtr3 = Kernel32.LockResource(intPtr2);
			if (intPtr3 == IntPtr.Zero)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			this.ReadImage(intPtr3, (uint)Kernel32.SizeofResource(hModule, intPtr));
			return new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header));
		}

		// Token: 0x060088B3 RID: 34995 RVA: 0x00292E38 File Offset: 0x00292E38
		internal virtual void ReadImage(IntPtr dibBits, uint size)
		{
			this._image.Read(dibBits, size);
		}

		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x060088B4 RID: 34996 RVA: 0x00292E48 File Offset: 0x00292E48
		public PixelFormat PixelFormat
		{
			get
			{
				ushort wBitsPerPixel = this._header.wBitsPerPixel;
				if (wBitsPerPixel <= 8)
				{
					if (wBitsPerPixel == 1)
					{
						return PixelFormat.Format1bppIndexed;
					}
					if (wBitsPerPixel == 4)
					{
						return PixelFormat.Format4bppIndexed;
					}
					if (wBitsPerPixel == 8)
					{
						return PixelFormat.Format8bppIndexed;
					}
				}
				else
				{
					if (wBitsPerPixel == 16)
					{
						return PixelFormat.Format16bppRgb565;
					}
					if (wBitsPerPixel == 24)
					{
						return PixelFormat.Format24bppRgb;
					}
					if (wBitsPerPixel == 32)
					{
						return PixelFormat.Format32bppArgb;
					}
				}
				return PixelFormat.Undefined;
			}
		}

		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x060088B5 RID: 34997 RVA: 0x00292EC8 File Offset: 0x00292EC8
		public string PixelFormatString
		{
			get
			{
				PixelFormat pixelFormat = this.PixelFormat;
				if (pixelFormat <= PixelFormat.Format1bppIndexed)
				{
					if (pixelFormat == PixelFormat.Format24bppRgb)
					{
						return "24-bit True Colors";
					}
					if (pixelFormat != PixelFormat.Format32bppRgb)
					{
						if (pixelFormat != PixelFormat.Format1bppIndexed)
						{
							goto IL_7C;
						}
						return "1-bit B/W";
					}
				}
				else
				{
					if (pixelFormat == PixelFormat.Format4bppIndexed)
					{
						return "4-bit 16 Colors";
					}
					if (pixelFormat == PixelFormat.Format8bppIndexed)
					{
						return "8-bit 256 Colors";
					}
					if (pixelFormat != PixelFormat.Format32bppArgb)
					{
						goto IL_7C;
					}
				}
				return "32-bit Alpha Channel";
				IL_7C:
				return "Unknown";
			}
		}

		// Token: 0x060088B6 RID: 34998 RVA: 0x00292F5C File Offset: 0x00292F5C
		public override string ToString()
		{
			return string.Format("{0}x{1} {2}", this.Width, this.Height, this.PixelFormatString);
		}

		// Token: 0x060088B7 RID: 34999 RVA: 0x00292F94 File Offset: 0x00292F94
		internal override void Write(BinaryWriter w)
		{
			w.Write(this._header.bWidth);
			w.Write(this._header.bHeight);
			w.Write(this._header.bColors);
			w.Write(this._header.bReserved);
			w.Write(this._header.wPlanes);
			w.Write(this._header.wBitsPerPixel);
			w.Write(this._header.dwImageSize);
			w.Write(this._header.nID);
			ResourceUtil.PadToWORD(w);
		}

		// Token: 0x060088B8 RID: 35000 RVA: 0x00293034 File Offset: 0x00293034
		public virtual void SaveIconTo(string filename)
		{
			Resource.SaveTo(filename, this._type, new ResourceId((uint)this._header.nID), this._language, this._image.Data);
		}

		// Token: 0x04003EA9 RID: 16041
		protected Kernel32.GRPICONDIRENTRY _header;

		// Token: 0x04003EAA RID: 16042
		protected DeviceIndependentBitmap _image = new DeviceIndependentBitmap();
	}
}
