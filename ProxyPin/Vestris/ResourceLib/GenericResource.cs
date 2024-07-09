using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D1C RID: 3356
	[ComVisible(true)]
	public class GenericResource : Resource
	{
		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x0600888A RID: 34954 RVA: 0x00292804 File Offset: 0x00292804
		// (set) Token: 0x0600888B RID: 34955 RVA: 0x0029280C File Offset: 0x0029280C
		public byte[] Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x0600888C RID: 34956 RVA: 0x00292818 File Offset: 0x00292818
		public GenericResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x0600888D RID: 34957 RVA: 0x0029282C File Offset: 0x0029282C
		public GenericResource(ResourceId type, ResourceId name, ushort language)
		{
			this._type = type;
			this._name = name;
			this._language = language;
		}

		// Token: 0x0600888E RID: 34958 RVA: 0x0029284C File Offset: 0x0029284C
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			if (this._size > 0)
			{
				this._data = new byte[this._size];
				Marshal.Copy(lpRes, this._data, 0, this._data.Length);
			}
			return new IntPtr(lpRes.ToInt64() + (long)this._size);
		}

		// Token: 0x0600888F RID: 34959 RVA: 0x002928A4 File Offset: 0x002928A4
		internal override void Write(BinaryWriter w)
		{
			w.Write(this._data);
		}

		// Token: 0x04003EA4 RID: 16036
		protected byte[] _data;
	}
}
