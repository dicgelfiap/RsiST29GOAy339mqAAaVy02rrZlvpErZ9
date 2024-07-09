using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D12 RID: 3346
	[ComVisible(true)]
	public abstract class DialogTemplateBase
	{
		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x06008807 RID: 34823
		// (set) Token: 0x06008808 RID: 34824
		public abstract short x { get; set; }

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x06008809 RID: 34825
		// (set) Token: 0x0600880A RID: 34826
		public abstract short y { get; set; }

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x0600880B RID: 34827
		// (set) Token: 0x0600880C RID: 34828
		public abstract short cx { get; set; }

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x0600880D RID: 34829
		// (set) Token: 0x0600880E RID: 34830
		public abstract short cy { get; set; }

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x0600880F RID: 34831
		// (set) Token: 0x06008810 RID: 34832
		public abstract uint Style { get; set; }

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x06008811 RID: 34833
		// (set) Token: 0x06008812 RID: 34834
		public abstract uint ExtendedStyle { get; set; }

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x06008813 RID: 34835
		public abstract ushort ControlCount { get; }

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x06008814 RID: 34836 RVA: 0x00291350 File Offset: 0x00291350
		// (set) Token: 0x06008815 RID: 34837 RVA: 0x00291358 File Offset: 0x00291358
		public string TypeFace
		{
			get
			{
				return this._typeface;
			}
			set
			{
				this._typeface = value;
			}
		}

		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x06008816 RID: 34838 RVA: 0x00291364 File Offset: 0x00291364
		// (set) Token: 0x06008817 RID: 34839 RVA: 0x0029136C File Offset: 0x0029136C
		public ushort PointSize
		{
			get
			{
				return this._pointSize;
			}
			set
			{
				this._pointSize = value;
			}
		}

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x06008818 RID: 34840 RVA: 0x00291378 File Offset: 0x00291378
		// (set) Token: 0x06008819 RID: 34841 RVA: 0x00291380 File Offset: 0x00291380
		public string Caption
		{
			get
			{
				return this._caption;
			}
			set
			{
				this._caption = value;
			}
		}

		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x0600881A RID: 34842 RVA: 0x0029138C File Offset: 0x0029138C
		// (set) Token: 0x0600881B RID: 34843 RVA: 0x00291394 File Offset: 0x00291394
		public ResourceId MenuId
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

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x0600881C RID: 34844 RVA: 0x002913A0 File Offset: 0x002913A0
		// (set) Token: 0x0600881D RID: 34845 RVA: 0x002913A8 File Offset: 0x002913A8
		public ResourceId WindowClassId
		{
			get
			{
				return this._windowClassId;
			}
			set
			{
				this._windowClassId = value;
			}
		}

		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x0600881E RID: 34846 RVA: 0x002913B4 File Offset: 0x002913B4
		// (set) Token: 0x0600881F RID: 34847 RVA: 0x002913BC File Offset: 0x002913BC
		public List<DialogTemplateControlBase> Controls
		{
			get
			{
				return this._controls;
			}
			set
			{
				this._controls = value;
			}
		}

		// Token: 0x06008820 RID: 34848 RVA: 0x002913C8 File Offset: 0x002913C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0}, {1}, {2}, {3}", new object[]
			{
				this.x,
				this.y,
				(int)(this.x + this.cx),
				(int)(this.y + this.cy)
			}));
			string text = DialogTemplateUtil.StyleToString<User32.WindowStyles, User32.DialogStyles>(this.Style);
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendLine("STYLE " + text);
			}
			string text2 = DialogTemplateUtil.StyleToString<User32.WindowStyles, User32.ExtendedDialogStyles>(this.ExtendedStyle);
			if (!string.IsNullOrEmpty(text2))
			{
				stringBuilder.AppendLine("EXSTYLE " + text2);
			}
			stringBuilder.AppendLine(string.Format("CAPTION \"{0}\"", this._caption));
			stringBuilder.AppendLine(string.Format("FONT {0}, \"{1}\"", this._pointSize, this._typeface));
			if (this._controls.Count > 0)
			{
				stringBuilder.AppendLine("{");
				foreach (DialogTemplateControlBase dialogTemplateControlBase in this._controls)
				{
					stringBuilder.AppendLine(" " + dialogTemplateControlBase.ToString());
				}
				stringBuilder.AppendLine("}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008821 RID: 34849 RVA: 0x00291550 File Offset: 0x00291550
		public virtual string ToControlString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} \"{1}\" {2}, {3}, {4}, {5}", new object[]
			{
				this.WindowClassId,
				this.Caption,
				this.x,
				this.y,
				this.cx,
				this.cy
			});
			return stringBuilder.ToString();
		}

		// Token: 0x06008822 RID: 34850 RVA: 0x002915CC File Offset: 0x002915CC
		internal virtual IntPtr Read(IntPtr lpRes)
		{
			lpRes = DialogTemplateUtil.ReadResourceId(lpRes, out this._menuId);
			lpRes = DialogTemplateUtil.ReadResourceId(lpRes, out this._windowClassId);
			this.Caption = Marshal.PtrToStringUni(lpRes);
			lpRes = new IntPtr(lpRes.ToInt64() + (long)((this.Caption.Length + 1) * Marshal.SystemDefaultCharSize));
			if ((this.Style & 64U) > 0U || (this.Style & 72U) > 0U)
			{
				this.PointSize = (ushort)Marshal.ReadInt16(lpRes);
				lpRes = new IntPtr(lpRes.ToInt64() + 2L);
			}
			return lpRes;
		}

		// Token: 0x06008823 RID: 34851
		internal abstract IntPtr AddControl(IntPtr lpRes);

		// Token: 0x06008824 RID: 34852 RVA: 0x00291668 File Offset: 0x00291668
		internal IntPtr ReadControls(IntPtr lpRes)
		{
			for (int i = 0; i < (int)this.ControlCount; i++)
			{
				lpRes = ResourceUtil.Align(lpRes);
				lpRes = this.AddControl(lpRes);
			}
			return lpRes;
		}

		// Token: 0x06008825 RID: 34853 RVA: 0x002916A0 File Offset: 0x002916A0
		internal void WriteControls(BinaryWriter w)
		{
			foreach (DialogTemplateControlBase dialogTemplateControlBase in this.Controls)
			{
				ResourceUtil.PadToDWORD(w);
				dialogTemplateControlBase.Write(w);
			}
		}

		// Token: 0x06008826 RID: 34854 RVA: 0x00291700 File Offset: 0x00291700
		public virtual void Write(BinaryWriter w)
		{
			DialogTemplateUtil.WriteResourceId(w, this._menuId);
			DialogTemplateUtil.WriteResourceId(w, this._windowClassId);
			w.Write(Encoding.Unicode.GetBytes(this.Caption));
			w.Write(0);
			if ((this.Style & 64U) > 0U || (this.Style & 72U) > 0U)
			{
				w.Write(this.PointSize);
			}
		}

		// Token: 0x04003E91 RID: 16017
		private string _caption;

		// Token: 0x04003E92 RID: 16018
		private ResourceId _menuId;

		// Token: 0x04003E93 RID: 16019
		private ResourceId _windowClassId;

		// Token: 0x04003E94 RID: 16020
		private ushort _pointSize;

		// Token: 0x04003E95 RID: 16021
		private string _typeface;

		// Token: 0x04003E96 RID: 16022
		private List<DialogTemplateControlBase> _controls = new List<DialogTemplateControlBase>();
	}
}
