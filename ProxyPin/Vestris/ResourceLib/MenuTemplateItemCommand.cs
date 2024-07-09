using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D2F RID: 3375
	[ComVisible(true)]
	public class MenuTemplateItemCommand : MenuTemplateItem
	{
		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x06008910 RID: 35088 RVA: 0x00293DF8 File Offset: 0x00293DF8
		// (set) Token: 0x06008911 RID: 35089 RVA: 0x00293E00 File Offset: 0x00293E00
		public ushort MenuId
		{
			get
			{
				return this._menuId;
			}
			set
			{
				this._menuId = value;
			}
		}

		// Token: 0x06008913 RID: 35091 RVA: 0x00293E14 File Offset: 0x00293E14
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._header = (User32.MENUITEMTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.MENUITEMTEMPLATE));
			lpRes = new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header));
			this._menuId = (ushort)Marshal.ReadInt16(lpRes);
			lpRes = new IntPtr(lpRes.ToInt64() + 2L);
			lpRes = base.Read(lpRes);
			return lpRes;
		}

		// Token: 0x06008914 RID: 35092 RVA: 0x00293E88 File Offset: 0x00293E88
		internal override void Write(BinaryWriter w)
		{
			w.Write(this._header.mtOption);
			w.Write(this._menuId);
			base.Write(w);
		}

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x06008915 RID: 35093 RVA: 0x00293EB0 File Offset: 0x00293EB0
		public bool IsSeparator
		{
			get
			{
				return (this._header.mtOption & 2048) > 0 || (this._header.mtOption == 0 && this._menuString == null && this._menuId == 0);
			}
		}

		// Token: 0x06008916 RID: 35094 RVA: 0x00293F00 File Offset: 0x00293F00
		public override string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsSeparator)
			{
				stringBuilder.AppendLine(string.Format("{0}MENUITEM SEPARATOR", new string(' ', indent)));
			}
			else
			{
				stringBuilder.AppendLine(string.Format("{0}MENUITEM \"{1}\", {2}", new string(' ', indent), (this._menuString == null) ? string.Empty : this._menuString.Replace("\t", "\\t"), this._menuId));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003EC5 RID: 16069
		private ushort _menuId;
	}
}
