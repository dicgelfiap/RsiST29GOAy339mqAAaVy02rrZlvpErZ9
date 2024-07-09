using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D2E RID: 3374
	[ComVisible(true)]
	public class MenuTemplateItemCollection : List<MenuTemplateItem>
	{
		// Token: 0x0600890C RID: 35084 RVA: 0x00293C88 File Offset: 0x00293C88
		internal IntPtr Read(IntPtr lpRes)
		{
			User32.MENUITEMTEMPLATE menuitemtemplate;
			do
			{
				menuitemtemplate = (User32.MENUITEMTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.MENUITEMTEMPLATE));
				MenuTemplateItem menuTemplateItem;
				if ((menuitemtemplate.mtOption & 16) > 0)
				{
					menuTemplateItem = new MenuTemplateItemPopup();
				}
				else
				{
					menuTemplateItem = new MenuTemplateItemCommand();
				}
				lpRes = menuTemplateItem.Read(lpRes);
				base.Add(menuTemplateItem);
			}
			while ((menuitemtemplate.mtOption & 128) == 0);
			return lpRes;
		}

		// Token: 0x0600890D RID: 35085 RVA: 0x00293CF0 File Offset: 0x00293CF0
		internal void Write(BinaryWriter w)
		{
			foreach (MenuTemplateItem menuTemplateItem in this)
			{
				menuTemplateItem.Write(w);
			}
		}

		// Token: 0x0600890E RID: 35086 RVA: 0x00293D44 File Offset: 0x00293D44
		public override string ToString()
		{
			return this.ToString(0);
		}

		// Token: 0x0600890F RID: 35087 RVA: 0x00293D50 File Offset: 0x00293D50
		public string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.Count > 0)
			{
				stringBuilder.AppendLine(string.Format("{0}BEGIN", new string(' ', indent)));
				foreach (MenuTemplateItem menuTemplateItem in this)
				{
					stringBuilder.Append(menuTemplateItem.ToString(indent + 1));
				}
				stringBuilder.AppendLine(string.Format("{0}END", new string(' ', indent)));
			}
			return stringBuilder.ToString();
		}
	}
}
