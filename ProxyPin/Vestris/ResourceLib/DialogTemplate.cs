using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D11 RID: 3345
	[ComVisible(true)]
	public class DialogTemplate : DialogTemplateBase
	{
		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x060087F5 RID: 34805 RVA: 0x002910C8 File Offset: 0x002910C8
		// (set) Token: 0x060087F6 RID: 34806 RVA: 0x002910D8 File Offset: 0x002910D8
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

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x060087F7 RID: 34807 RVA: 0x002910E8 File Offset: 0x002910E8
		// (set) Token: 0x060087F8 RID: 34808 RVA: 0x002910F8 File Offset: 0x002910F8
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

		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x060087F9 RID: 34809 RVA: 0x00291108 File Offset: 0x00291108
		// (set) Token: 0x060087FA RID: 34810 RVA: 0x00291118 File Offset: 0x00291118
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

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x060087FB RID: 34811 RVA: 0x00291128 File Offset: 0x00291128
		// (set) Token: 0x060087FC RID: 34812 RVA: 0x00291138 File Offset: 0x00291138
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

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x060087FD RID: 34813 RVA: 0x00291148 File Offset: 0x00291148
		// (set) Token: 0x060087FE RID: 34814 RVA: 0x00291158 File Offset: 0x00291158
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

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x060087FF RID: 34815 RVA: 0x00291168 File Offset: 0x00291168
		// (set) Token: 0x06008800 RID: 34816 RVA: 0x00291178 File Offset: 0x00291178
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

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x06008801 RID: 34817 RVA: 0x00291188 File Offset: 0x00291188
		public override ushort ControlCount
		{
			get
			{
				return this._header.cdit;
			}
		}

		// Token: 0x06008803 RID: 34819 RVA: 0x002911A0 File Offset: 0x002911A0
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._header = (User32.DIALOGTEMPLATE)Marshal.PtrToStructure(lpRes, typeof(User32.DIALOGTEMPLATE));
			lpRes = new IntPtr(lpRes.ToInt64() + 18L);
			lpRes = base.Read(lpRes);
			if ((this.Style & 64U) > 0U || (this.Style & 72U) > 0U)
			{
				base.TypeFace = Marshal.PtrToStringUni(lpRes);
				lpRes = new IntPtr(lpRes.ToInt64() + (long)((base.TypeFace.Length + 1) * Marshal.SystemDefaultCharSize));
			}
			return base.ReadControls(lpRes);
		}

		// Token: 0x06008804 RID: 34820 RVA: 0x0029123C File Offset: 0x0029123C
		internal override IntPtr AddControl(IntPtr lpRes)
		{
			DialogTemplateControl dialogTemplateControl = new DialogTemplateControl();
			base.Controls.Add(dialogTemplateControl);
			return dialogTemplateControl.Read(lpRes);
		}

		// Token: 0x06008805 RID: 34821 RVA: 0x00291268 File Offset: 0x00291268
		public override void Write(BinaryWriter w)
		{
			w.Write(this._header.style);
			w.Write(this._header.dwExtendedStyle);
			w.Write((ushort)base.Controls.Count);
			w.Write(this._header.x);
			w.Write(this._header.y);
			w.Write(this._header.cx);
			w.Write(this._header.cy);
			base.Write(w);
			if ((this.Style & 64U) > 0U || (this.Style & 72U) > 0U)
			{
				w.Write(Encoding.Unicode.GetBytes(base.TypeFace));
				w.Write(0);
			}
			base.WriteControls(w);
		}

		// Token: 0x06008806 RID: 34822 RVA: 0x0029133C File Offset: 0x0029133C
		public override string ToString()
		{
			return string.Format("DIALOG {0}", base.ToString());
		}

		// Token: 0x04003E90 RID: 16016
		private User32.DIALOGTEMPLATE _header;
	}
}
