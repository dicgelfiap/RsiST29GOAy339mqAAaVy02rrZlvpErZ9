using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D19 RID: 3353
	[ComVisible(true)]
	public class FontDirectoryResource : Resource
	{
		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x06008875 RID: 34933 RVA: 0x00292650 File Offset: 0x00292650
		// (set) Token: 0x06008876 RID: 34934 RVA: 0x00292658 File Offset: 0x00292658
		public List<FontDirectoryEntry> Fonts
		{
			get
			{
				return this._fonts;
			}
			set
			{
				this._fonts = value;
			}
		}

		// Token: 0x06008877 RID: 34935 RVA: 0x00292664 File Offset: 0x00292664
		public FontDirectoryResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_FONTDIR), null, ResourceUtil.NEUTRALLANGID, 0)
		{
		}

		// Token: 0x06008878 RID: 34936 RVA: 0x00292690 File Offset: 0x00292690
		public FontDirectoryResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x06008879 RID: 34937 RVA: 0x002926AC File Offset: 0x002926AC
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			IntPtr intPtr = lpRes;
			ushort num = (ushort)Marshal.ReadInt16(lpRes);
			lpRes = new IntPtr(lpRes.ToInt64() + 2L);
			for (int i = 0; i < (int)num; i++)
			{
				FontDirectoryEntry fontDirectoryEntry = new FontDirectoryEntry();
				lpRes = fontDirectoryEntry.Read(lpRes);
				this._fonts.Add(fontDirectoryEntry);
			}
			int num2 = this._size - (int)(lpRes.ToInt64() - intPtr.ToInt64());
			this._reserved = new byte[num2];
			Marshal.Copy(lpRes, this._reserved, 0, num2);
			return lpRes;
		}

		// Token: 0x0600887A RID: 34938 RVA: 0x00292738 File Offset: 0x00292738
		internal override void Write(BinaryWriter w)
		{
			w.Write((ushort)this._fonts.Count);
			foreach (FontDirectoryEntry fontDirectoryEntry in this._fonts)
			{
				fontDirectoryEntry.Write(w);
			}
			w.Write(this._reserved);
		}

		// Token: 0x04003EA2 RID: 16034
		private List<FontDirectoryEntry> _fonts = new List<FontDirectoryEntry>();

		// Token: 0x04003EA3 RID: 16035
		private byte[] _reserved;
	}
}
