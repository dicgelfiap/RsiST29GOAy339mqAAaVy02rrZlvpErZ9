using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D14 RID: 3348
	[ComVisible(true)]
	public abstract class DialogTemplateControlBase
	{
		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x0600883A RID: 34874
		// (set) Token: 0x0600883B RID: 34875
		public abstract short x { get; set; }

		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x0600883C RID: 34876
		// (set) Token: 0x0600883D RID: 34877
		public abstract short y { get; set; }

		// Token: 0x17001CFE RID: 7422
		// (get) Token: 0x0600883E RID: 34878
		// (set) Token: 0x0600883F RID: 34879
		public abstract short cx { get; set; }

		// Token: 0x17001CFF RID: 7423
		// (get) Token: 0x06008840 RID: 34880
		// (set) Token: 0x06008841 RID: 34881
		public abstract short cy { get; set; }

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x06008842 RID: 34882
		// (set) Token: 0x06008843 RID: 34883
		public abstract uint Style { get; set; }

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x06008844 RID: 34884
		// (set) Token: 0x06008845 RID: 34885
		public abstract uint ExtendedStyle { get; set; }

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x06008846 RID: 34886 RVA: 0x00291A34 File Offset: 0x00291A34
		// (set) Token: 0x06008847 RID: 34887 RVA: 0x00291A3C File Offset: 0x00291A3C
		public ResourceId CaptionId
		{
			get
			{
				return this._captionId;
			}
			set
			{
				this._captionId = value;
			}
		}

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x06008848 RID: 34888 RVA: 0x00291A48 File Offset: 0x00291A48
		// (set) Token: 0x06008849 RID: 34889 RVA: 0x00291A50 File Offset: 0x00291A50
		public ResourceId ControlClassId
		{
			get
			{
				return this._controlClassId;
			}
			set
			{
				this._controlClassId = value;
			}
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x0600884A RID: 34890 RVA: 0x00291A5C File Offset: 0x00291A5C
		public User32.DialogItemClass ControlClass
		{
			get
			{
				return (User32.DialogItemClass)((int)this.ControlClassId.Id);
			}
		}

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x0600884B RID: 34891 RVA: 0x00291A70 File Offset: 0x00291A70
		// (set) Token: 0x0600884C RID: 34892 RVA: 0x00291A78 File Offset: 0x00291A78
		public byte[] CreationData
		{
			get
			{
				return this._creationData;
			}
			set
			{
				this._creationData = value;
			}
		}

		// Token: 0x0600884D RID: 34893 RVA: 0x00291A84 File Offset: 0x00291A84
		internal virtual IntPtr Read(IntPtr lpRes)
		{
			lpRes = DialogTemplateUtil.ReadResourceId(lpRes, out this._controlClassId);
			lpRes = DialogTemplateUtil.ReadResourceId(lpRes, out this._captionId);
			if ((ushort)Marshal.ReadInt16(lpRes) == 0)
			{
				lpRes = new IntPtr(lpRes.ToInt64() + 2L);
			}
			else
			{
				ushort num = (ushort)Marshal.ReadInt16(lpRes);
				this._creationData = new byte[(int)num];
				Marshal.Copy(lpRes, this._creationData, 0, this._creationData.Length);
				lpRes = new IntPtr(lpRes.ToInt64() + (long)((ulong)num));
			}
			return lpRes;
		}

		// Token: 0x0600884E RID: 34894 RVA: 0x00291B10 File Offset: 0x00291B10
		public virtual void Write(BinaryWriter w)
		{
			DialogTemplateUtil.WriteResourceId(w, this._controlClassId);
			DialogTemplateUtil.WriteResourceId(w, this._captionId);
			if (this._creationData == null)
			{
				w.Write(0);
				return;
			}
			ResourceUtil.PadToWORD(w);
			w.Write((ushort)this._creationData.Length);
			w.Write(this._creationData);
		}

		// Token: 0x04003E98 RID: 16024
		private ResourceId _captionId;

		// Token: 0x04003E99 RID: 16025
		private ResourceId _controlClassId;

		// Token: 0x04003E9A RID: 16026
		private byte[] _creationData;
	}
}
