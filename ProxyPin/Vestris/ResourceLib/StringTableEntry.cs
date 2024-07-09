using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D39 RID: 3385
	[ComVisible(true)]
	public class StringTableEntry
	{
		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x06008992 RID: 35218 RVA: 0x00295860 File Offset: 0x00295860
		public Kernel32.RESOURCE_HEADER Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x06008993 RID: 35219 RVA: 0x00295868 File Offset: 0x00295868
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x06008994 RID: 35220 RVA: 0x00295870 File Offset: 0x00295870
		public string StringValue
		{
			get
			{
				if (this._value == null)
				{
					return this._value;
				}
				return this._value.Substring(0, this._value.Length - 1);
			}
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x06008995 RID: 35221 RVA: 0x002958A0 File Offset: 0x002958A0
		// (set) Token: 0x06008996 RID: 35222 RVA: 0x002958A8 File Offset: 0x002958A8
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value == null)
				{
					this._value = null;
					this._header.wValueLength = 0;
					return;
				}
				if (value.Length == 0 || value[value.Length - 1] != '\0')
				{
					this._value = value + "\0";
				}
				else
				{
					this._value = value;
				}
				this._header.wValueLength = (ushort)this._value.Length;
			}
		}

		// Token: 0x06008997 RID: 35223 RVA: 0x00295928 File Offset: 0x00295928
		public StringTableEntry(string key)
		{
			this._key = key;
			this._header.wType = 1;
			this._header.wLength = 0;
			this._header.wValueLength = 0;
		}

		// Token: 0x06008998 RID: 35224 RVA: 0x0029595C File Offset: 0x0029595C
		internal StringTableEntry(IntPtr lpRes)
		{
			this.Read(lpRes);
		}

		// Token: 0x06008999 RID: 35225 RVA: 0x0029596C File Offset: 0x0029596C
		internal void Read(IntPtr lpRes)
		{
			this._header = (Kernel32.RESOURCE_HEADER)Marshal.PtrToStructure(lpRes, typeof(Kernel32.RESOURCE_HEADER));
			IntPtr ptr = new IntPtr(lpRes.ToInt64() + (long)Marshal.SizeOf(this._header));
			this._key = Marshal.PtrToStringUni(ptr);
			IntPtr ptr2 = ResourceUtil.Align(ptr.ToInt64() + (long)((this._key.Length + 1) * Marshal.SystemDefaultCharSize));
			if (this._header.wValueLength > 0)
			{
				this._value = Marshal.PtrToStringUni(ptr2, (int)this._header.wValueLength);
				if (this._value.Length == 0 || this._value[this._value.Length - 1] != '\0')
				{
					this._value += "\0";
				}
			}
		}

		// Token: 0x0600899A RID: 35226 RVA: 0x00295A54 File Offset: 0x00295A54
		internal void Write(BinaryWriter w)
		{
			long position = w.BaseStream.Position;
			w.Write(this._header.wLength);
			w.Write(this._header.wValueLength);
			w.Write(this._header.wType);
			w.Write(Encoding.Unicode.GetBytes(this._key));
			w.Write(0);
			ResourceUtil.PadToDWORD(w);
			long position2 = w.BaseStream.Position;
			if (this._value != null)
			{
				w.Write(Encoding.Unicode.GetBytes(this._value));
			}
			ResourceUtil.WriteAt(w, (w.BaseStream.Position - position2) / (long)Marshal.SystemDefaultCharSize, position + 2L);
			if (StringTableEntry.ConsiderPaddingForLength)
			{
				ResourceUtil.PadToDWORD(w);
			}
			ResourceUtil.WriteAt(w, w.BaseStream.Position - position, position);
		}

		// Token: 0x04003ED7 RID: 16087
		private Kernel32.RESOURCE_HEADER _header;

		// Token: 0x04003ED8 RID: 16088
		private string _key;

		// Token: 0x04003ED9 RID: 16089
		private string _value;

		// Token: 0x04003EDA RID: 16090
		public static bool ConsiderPaddingForLength;
	}
}
