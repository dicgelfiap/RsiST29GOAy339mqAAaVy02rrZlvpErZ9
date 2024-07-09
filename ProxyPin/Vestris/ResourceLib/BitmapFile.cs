using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D09 RID: 3337
	[ComVisible(true)]
	public class BitmapFile
	{
		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x0600879F RID: 34719 RVA: 0x0028FE58 File Offset: 0x0028FE58
		public DeviceIndependentBitmap Bitmap
		{
			get
			{
				return this._bitmap;
			}
		}

		// Token: 0x060087A0 RID: 34720 RVA: 0x0028FE60 File Offset: 0x0028FE60
		public BitmapFile(string filename)
		{
			byte[] array = File.ReadAllBytes(filename);
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(this._header));
			try
			{
				Marshal.Copy(array, 0, intPtr, Marshal.SizeOf(this._header));
				this._header = (Gdi32.BITMAPFILEHEADER)Marshal.PtrToStructure(intPtr, typeof(Gdi32.BITMAPFILEHEADER));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			int num = array.Length - Marshal.SizeOf(this._header);
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(array, Marshal.SizeOf(this._header), array2, 0, num);
			this._bitmap = new DeviceIndependentBitmap(array2);
		}

		// Token: 0x04003E80 RID: 16000
		private Gdi32.BITMAPFILEHEADER _header;

		// Token: 0x04003E81 RID: 16001
		private DeviceIndependentBitmap _bitmap;
	}
}
