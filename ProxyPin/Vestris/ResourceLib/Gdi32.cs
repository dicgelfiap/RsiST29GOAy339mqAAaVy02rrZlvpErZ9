using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D1B RID: 3355
	[ComVisible(true)]
	public abstract class Gdi32
	{
		// Token: 0x0600887F RID: 34943
		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SetDIBitsToDevice(IntPtr hdc, int XDest, int YDest, uint dwWidth, uint dwHeight, int XSrc, int YSrc, uint uStartScan, uint cScanLines, byte[] lpvBits, [In] ref Gdi32.BITMAPINFO lpbmi, uint fuColorUse);

		// Token: 0x06008880 RID: 34944
		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int SetDIBitsToDevice(IntPtr hdc, int XDest, int YDest, uint dwWidth, uint dwHeight, int XSrc, int YSrc, uint uStartScan, uint cScanLines, IntPtr lpvBits, IntPtr lpbmi, uint fuColorUse);

		// Token: 0x06008881 RID: 34945
		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int GetDIBits(IntPtr hdc, IntPtr hbmp, uint uStartScan, uint cScanLines, [Out] byte[] lpvBits, [In] ref Gdi32.BITMAPINFO lpbmi, uint uUsage);

		// Token: 0x06008882 RID: 34946
		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref Gdi32.BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

		// Token: 0x06008883 RID: 34947
		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		// Token: 0x06008884 RID: 34948
		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr CreateDC(string lpDriverName, string lpDeviceName, string lpOutput, IntPtr lpInitData);

		// Token: 0x06008885 RID: 34949
		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		// Token: 0x06008886 RID: 34950
		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		// Token: 0x06008887 RID: 34951
		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int DeleteObject(IntPtr hObject);

		// Token: 0x06008888 RID: 34952
		[DllImport("gdi32.dll", SetLastError = true)]
		internal static extern int DeleteDC(IntPtr hdc);

		// Token: 0x020011D9 RID: 4569
		public enum BitmapCompression
		{
			// Token: 0x04004C9C RID: 19612
			BI_RGB,
			// Token: 0x04004C9D RID: 19613
			BI_RLE8,
			// Token: 0x04004C9E RID: 19614
			BI_RLE4,
			// Token: 0x04004C9F RID: 19615
			BI_BITFIELDS,
			// Token: 0x04004CA0 RID: 19616
			BI_JPEG,
			// Token: 0x04004CA1 RID: 19617
			BI_PNG
		}

		// Token: 0x020011DA RID: 4570
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct BITMAPINFOHEADER
		{
			// Token: 0x17001F4C RID: 8012
			// (get) Token: 0x0600967C RID: 38524 RVA: 0x002CC414 File Offset: 0x002CC414
			public Gdi32.BitmapCompression BitmapCompression
			{
				get
				{
					return (Gdi32.BitmapCompression)this.biCompression;
				}
			}

			// Token: 0x17001F4D RID: 8013
			// (get) Token: 0x0600967D RID: 38525 RVA: 0x002CC41C File Offset: 0x002CC41C
			public PixelFormat PixelFormat
			{
				get
				{
					ushort num = this.biBitCount;
					if (num <= 8)
					{
						if (num == 1)
						{
							return PixelFormat.Format1bppIndexed;
						}
						if (num == 4)
						{
							return PixelFormat.Format4bppIndexed;
						}
						if (num == 8)
						{
							return PixelFormat.Format8bppIndexed;
						}
					}
					else
					{
						if (num == 16)
						{
							return PixelFormat.Format16bppRgb565;
						}
						if (num == 24)
						{
							return PixelFormat.Format24bppRgb;
						}
						if (num == 32)
						{
							return PixelFormat.Format32bppArgb;
						}
					}
					return PixelFormat.Undefined;
				}
			}

			// Token: 0x17001F4E RID: 8014
			// (get) Token: 0x0600967E RID: 38526 RVA: 0x002CC498 File Offset: 0x002CC498
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

			// Token: 0x04004CA2 RID: 19618
			public uint biSize;

			// Token: 0x04004CA3 RID: 19619
			public int biWidth;

			// Token: 0x04004CA4 RID: 19620
			public int biHeight;

			// Token: 0x04004CA5 RID: 19621
			public ushort biPlanes;

			// Token: 0x04004CA6 RID: 19622
			public ushort biBitCount;

			// Token: 0x04004CA7 RID: 19623
			public uint biCompression;

			// Token: 0x04004CA8 RID: 19624
			public uint biSizeImage;

			// Token: 0x04004CA9 RID: 19625
			public int biXPelsPerMeter;

			// Token: 0x04004CAA RID: 19626
			public int biYPelsPerMeter;

			// Token: 0x04004CAB RID: 19627
			public uint biClrUsed;

			// Token: 0x04004CAC RID: 19628
			public uint biClrImportant;
		}

		// Token: 0x020011DB RID: 4571
		public struct BITMAPINFO
		{
			// Token: 0x04004CAD RID: 19629
			public Gdi32.BITMAPINFOHEADER bmiHeader;

			// Token: 0x04004CAE RID: 19630
			public Gdi32.RGBQUAD bmiColors;
		}

		// Token: 0x020011DC RID: 4572
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct RGBQUAD
		{
			// Token: 0x04004CAF RID: 19631
			public byte rgbBlue;

			// Token: 0x04004CB0 RID: 19632
			public byte rgbGreen;

			// Token: 0x04004CB1 RID: 19633
			public byte rgbRed;

			// Token: 0x04004CB2 RID: 19634
			public byte rgbReserved;
		}

		// Token: 0x020011DD RID: 4573
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct BITMAPFILEHEADER
		{
			// Token: 0x04004CB3 RID: 19635
			public ushort bfType;

			// Token: 0x04004CB4 RID: 19636
			public uint bfSize;

			// Token: 0x04004CB5 RID: 19637
			public ushort bfReserved1;

			// Token: 0x04004CB6 RID: 19638
			public ushort bfReserved2;

			// Token: 0x04004CB7 RID: 19639
			public uint bfOffBits;
		}

		// Token: 0x020011DE RID: 4574
		internal enum DIBColors
		{
			// Token: 0x04004CB9 RID: 19641
			DIB_RGB_COLORS,
			// Token: 0x04004CBA RID: 19642
			DIB_PAL_COLORS,
			// Token: 0x04004CBB RID: 19643
			DIB_PAL_INDICES,
			// Token: 0x04004CBC RID: 19644
			DIB_PAL_LOGINDICES = 4
		}
	}
}
