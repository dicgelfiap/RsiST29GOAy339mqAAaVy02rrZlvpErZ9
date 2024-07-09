using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D13 RID: 3347
	[ComVisible(true)]
	public class DialogTemplateControl : DialogTemplateControlBase
	{
		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x06008828 RID: 34856 RVA: 0x00291784 File Offset: 0x00291784
		// (set) Token: 0x06008829 RID: 34857 RVA: 0x00291794 File Offset: 0x00291794
		public override short x
		{
			get
			{
				return this._header.x;
			}
			set
			{
				this._header.x = value;
			}
		}

		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x0600882A RID: 34858 RVA: 0x002917A4 File Offset: 0x002917A4
		// (set) Token: 0x0600882B RID: 34859 RVA: 0x002917B4 File Offset: 0x002917B4
		public override short y
		{
			get
			{
				return this._header.y;
			}
			set
			{
				this._header.y = value;
			}
		}

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x0600882C RID: 34860 RVA: 0x002917C4 File Offset: 0x002917C4
		// (set) Token: 0x0600882D RID: 34861 RVA: 0x002917D4 File Offset: 0x002917D4
		public override short cx
		{
			get
			{
				return this._header.cx;
			}
			set
			{
				this._header.cx = value;
			}
		}

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x0600882E RID: 34862 RVA: 0x002917E4 File Offset: 0x002917E4
		// (set) Token: 0x0600882F RID: 34863 RVA: 0x002917F4 File Offset: 0x002917F4
		public override short cy
		{
			get
			{
				return this._header.cy;
			}
			set
			{
				this._header.cy = value;
			}
		}

		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x06008830 RID: 34864 RVA: 0x00291804 File Offset: 0x00291804
		// (set) Token: 0x06008831 RID: 34865 RVA: 0x00291814 File Offset: 0x00291814
		public override uint Style
		{
			get
			{
				return this._header.style;
			}
			set
			{
				this._header.style = value;
			}
		}

		// Token: 0x17001CFA RID: 7418
		// (get) Token: 0x06008832 RID: 34866 RVA: 0x00291824 File Offset: 0x00291824
		// (set) Token: 0x06008833 RID: 34867 RVA: 0x00291834 File Offset: 0x00291834
		public override uint ExtendedStyle
		{
			get
			{
				return this._header.dwExtendedStyle;
			}
			set
			{
				this._header.dwExtendedStyle = value;
			}
		}

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x06008834 RID: 34868 RVA: 0x00291844 File Offset: 0x00291844
		// (set) Token: 0x06008835 RID: 34869 RVA: 0x00291854 File Offset: 0x00291854
		public short Id
		{
			get
			{
				return this._header.id;
			}
			set
			{
				this._header.id = value;
			}
		}

		// Token: 0x06008837 RID: 34871 RVA: 0x0029186C File Offset: 0x0029186C
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._header = (User32.DIALOGITEMTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.DIALOGITEMTEMPLATE));
			lpRes = new IntPtr(lpRes.ToInt64() + 18L);
			lpRes = base.Read(lpRes);
			return lpRes;
		}

		// Token: 0x06008838 RID: 34872 RVA: 0x002918A8 File Offset: 0x002918A8
		public override void Write(BinaryWriter w)
		{
			w.Write(this._header.style);
			w.Write(this._header.dwExtendedStyle);
			w.Write(this._header.x);
			w.Write(this._header.y);
			w.Write(this._header.cx);
			w.Write(this._header.cy);
			w.Write(this._header.id);
			base.Write(w);
		}

		// Token: 0x06008839 RID: 34873 RVA: 0x00291938 File Offset: 0x00291938
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} \"{1}\" {2}, {3}, {4}, {5}, {6}, {7}", new object[]
			{
				base.ControlClass,
				base.CaptionId,
				this.Id,
				this.x,
				this.y,
				this.cx,
				this.cy,
				DialogTemplateUtil.StyleToString<User32.WindowStyles, User32.DialogStyles>(this.Style)
			});
			User32.DialogItemClass controlClass = base.ControlClass;
			if (controlClass != User32.DialogItemClass.Button)
			{
				if (controlClass == User32.DialogItemClass.Edit)
				{
					stringBuilder.AppendFormat("| {0}", DialogTemplateUtil.StyleToString<User32.EditControlStyles>(this.Style & 65535U));
				}
			}
			else
			{
				stringBuilder.AppendFormat("| {0}", (User32.ButtonControlStyles)(this.Style & 65535U));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003E97 RID: 16023
		private User32.DIALOGITEMTEMPLATE _header;
	}
}
