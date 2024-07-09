using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D27 RID: 3367
	[ComVisible(true)]
	public class MenuExTemplateItemCollection : List<MenuExTemplateItem>
	{
		// Token: 0x060088E7 RID: 35047 RVA: 0x002935F8 File Offset: 0x002935F8
		internal IntPtr Read(IntPtr lpRes)
		{
			User32.MENUEXITEMTEMPLATE menuexitemtemplate;
			do
			{
				lpRes = ResourceUtil.Align(lpRes.ToInt64());
				menuexitemtemplate = (User32.MENUEXITEMTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.MENUEXITEMTEMPLATE));
				MenuExTemplateItem menuExTemplateItem;
				if ((menuexitemtemplate.bResInfo & 1) > 0)
				{
					menuExTemplateItem = new MenuExTemplateItemPopup();
				}
				else
				{
					menuExTemplateItem = new MenuExTemplateItemCommand();
				}
				lpRes = menuExTemplateItem.Read(lpRes);
				base.Add(menuExTemplateItem);
			}
			while ((menuexitemtemplate.bResInfo & 128) <= 0);
			return lpRes;
		}

		// Token: 0x060088E8 RID: 35048 RVA: 0x0029366C File Offset: 0x0029366C
		internal void Write(BinaryWriter w)
		{
			foreach (MenuExTemplateItem menuExTemplateItem in this)
			{
				ResourceUtil.PadToDWORD(w);
				menuExTemplateItem.Write(w);
			}
		}

		// Token: 0x060088E9 RID: 35049 RVA: 0x002936C8 File Offset: 0x002936C8
		public override string ToString()
		{
			return this.ToString(0);
		}

		// Token: 0x060088EA RID: 35050 RVA: 0x002936D4 File Offset: 0x002936D4
		public string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.Count > 0)
			{
				stringBuilder.AppendLine(string.Format("{0}BEGIN", new string(' ', indent)));
				foreach (MenuExTemplateItem menuExTemplateItem in this)
				{
					stringBuilder.Append(menuExTemplateItem.ToString(indent + 1));
				}
				stringBuilder.AppendLine(string.Format("{0}END", new string(' ', indent)));
			}
			return stringBuilder.ToString();
		}
	}
}
