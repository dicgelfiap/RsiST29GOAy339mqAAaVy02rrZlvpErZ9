using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D25 RID: 3365
	[ComVisible(true)]
	public class MenuExTemplate : MenuTemplateBase
	{
		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x060088D9 RID: 35033 RVA: 0x00293370 File Offset: 0x00293370
		// (set) Token: 0x060088DA RID: 35034 RVA: 0x00293378 File Offset: 0x00293378
		public MenuExTemplateItemCollection MenuItems
		{
			get
			{
				return this._menuItems;
			}
			set
			{
				this._menuItems = value;
			}
		}

		// Token: 0x060088DB RID: 35035 RVA: 0x00293384 File Offset: 0x00293384
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._header = (User32.MENUEXTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.MENUEXTEMPLATE));
			IntPtr lpRes2 = ResourceUtil.Align(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header) + (long)((ulong)this._header.wOffset));
			return this._menuItems.Read(lpRes2);
		}

		// Token: 0x060088DC RID: 35036 RVA: 0x002933E8 File Offset: 0x002933E8
		internal override void Write(BinaryWriter w)
		{
			long position = w.BaseStream.Position;
			w.Write(this._header.wVersion);
			w.Write(this._header.wOffset);
			ResourceUtil.Pad(w, this._header.wOffset - 4);
			w.BaseStream.Seek(position + (long)((ulong)this._header.wOffset) + 4L, SeekOrigin.Begin);
			this._menuItems.Write(w);
		}

		// Token: 0x060088DD RID: 35037 RVA: 0x00293468 File Offset: 0x00293468
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("MENUEX");
			stringBuilder.Append(this._menuItems.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x04003EBA RID: 16058
		private User32.MENUEXTEMPLATE _header;

		// Token: 0x04003EBB RID: 16059
		private MenuExTemplateItemCollection _menuItems = new MenuExTemplateItemCollection();
	}
}
