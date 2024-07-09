using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D34 RID: 3380
	[ComVisible(true)]
	public class ResourceTableHeader
	{
		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x06008954 RID: 35156 RVA: 0x00294C60 File Offset: 0x00294C60
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x06008955 RID: 35157 RVA: 0x00294C68 File Offset: 0x00294C68
		// (set) Token: 0x06008956 RID: 35158 RVA: 0x00294C70 File Offset: 0x00294C70
		public Kernel32.RESOURCE_HEADER Header
		{
			get
			{
				return this._header;
			}
			set
			{
				this._header = value;
			}
		}

		// Token: 0x06008957 RID: 35159 RVA: 0x00294C7C File Offset: 0x00294C7C
		public ResourceTableHeader()
		{
		}

		// Token: 0x06008958 RID: 35160 RVA: 0x00294C84 File Offset: 0x00294C84
		public ResourceTableHeader(string key)
		{
			this._key = key;
		}

		// Token: 0x06008959 RID: 35161 RVA: 0x00294C94 File Offset: 0x00294C94
		internal ResourceTableHeader(IntPtr lpRes)
		{
			this.Read(lpRes);
		}

		// Token: 0x0600895A RID: 35162 RVA: 0x00294CA4 File Offset: 0x00294CA4
		internal virtual IntPtr Read(IntPtr lpRes)
		{
			this._header = (Kernel32.RESOURCE_HEADER)Marshal.PtrToStructure(lpRes, typeof(Kernel32.RESOURCE_HEADER));
			IntPtr ptr = new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header));
			this._key = Marshal.PtrToStringUni(ptr);
			return ResourceUtil.Align(ptr.ToInt64() + (long)((this._key.Length + 1) * Marshal.SystemDefaultCharSize));
		}

		// Token: 0x0600895B RID: 35163 RVA: 0x00294D20 File Offset: 0x00294D20
		internal virtual void Write(BinaryWriter w)
		{
			w.Write(this._header.wLength);
			w.Write(this._header.wValueLength);
			w.Write(this._header.wType);
			w.Write(Encoding.Unicode.GetBytes(this._key));
			w.Write(0);
			ResourceUtil.PadToDWORD(w);
		}

		// Token: 0x0600895C RID: 35164 RVA: 0x00294D88 File Offset: 0x00294D88
		public override string ToString()
		{
			return this.ToString(0);
		}

		// Token: 0x0600895D RID: 35165 RVA: 0x00294D94 File Offset: 0x00294D94
		public virtual string ToString(int indent)
		{
			return base.ToString();
		}

		// Token: 0x04003ED2 RID: 16082
		protected Kernel32.RESOURCE_HEADER _header;

		// Token: 0x04003ED3 RID: 16083
		protected string _key;
	}
}
