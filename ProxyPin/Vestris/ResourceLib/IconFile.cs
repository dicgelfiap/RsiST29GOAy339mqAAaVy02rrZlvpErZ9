using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D1E RID: 3358
	[ComVisible(true)]
	public class IconFile
	{
		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x06008893 RID: 34963 RVA: 0x00292934 File Offset: 0x00292934
		// (set) Token: 0x06008894 RID: 34964 RVA: 0x00292944 File Offset: 0x00292944
		public IconFile.GroupType Type
		{
			get
			{
				return (IconFile.GroupType)this._header.wType;
			}
			set
			{
				this._header.wType = (ushort)((byte)value);
			}
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x06008895 RID: 34965 RVA: 0x00292954 File Offset: 0x00292954
		// (set) Token: 0x06008896 RID: 34966 RVA: 0x0029295C File Offset: 0x0029295C
		public List<IconFileIcon> Icons
		{
			get
			{
				return this._icons;
			}
			set
			{
				this._icons = value;
			}
		}

		// Token: 0x06008897 RID: 34967 RVA: 0x00292968 File Offset: 0x00292968
		public IconFile(string filename)
		{
			this.LoadFrom(filename);
		}

		// Token: 0x06008898 RID: 34968 RVA: 0x00292984 File Offset: 0x00292984
		public void LoadFrom(string filename)
		{
			byte[] array = File.ReadAllBytes(filename);
			IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			try
			{
				Marshal.Copy(array, 0, intPtr, array.Length);
				this.Read(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06008899 RID: 34969 RVA: 0x002929D0 File Offset: 0x002929D0
		internal IntPtr Read(IntPtr lpData)
		{
			this._icons.Clear();
			this._header = (Kernel32.FILEGRPICONDIR)Marshal.PtrToStructure(lpData, typeof(Kernel32.FILEGRPICONDIR));
			IntPtr intPtr = new IntPtr(lpData.ToInt64() + (long)Marshal.SizeOf(this._header));
			for (int i = 0; i < (int)this._header.wCount; i++)
			{
				IconFileIcon iconFileIcon = new IconFileIcon();
				intPtr = iconFileIcon.Read(intPtr, lpData);
				this._icons.Add(iconFileIcon);
			}
			return intPtr;
		}

		// Token: 0x04003EA5 RID: 16037
		private Kernel32.FILEGRPICONDIR _header;

		// Token: 0x04003EA6 RID: 16038
		private List<IconFileIcon> _icons = new List<IconFileIcon>();

		// Token: 0x020011DF RID: 4575
		public enum GroupType
		{
			// Token: 0x04004CBE RID: 19646
			Icon = 1,
			// Token: 0x04004CBF RID: 19647
			Cursor
		}
	}
}
