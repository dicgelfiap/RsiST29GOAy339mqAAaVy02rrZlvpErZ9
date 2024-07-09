using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D38 RID: 3384
	[ComVisible(true)]
	public class StringTable : ResourceTableHeader
	{
		// Token: 0x17001D47 RID: 7495
		// (get) Token: 0x06008985 RID: 35205 RVA: 0x002954EC File Offset: 0x002954EC
		public Dictionary<string, StringTableEntry> Strings
		{
			get
			{
				return this._strings;
			}
		}

		// Token: 0x06008986 RID: 35206 RVA: 0x002954F4 File Offset: 0x002954F4
		public StringTable()
		{
		}

		// Token: 0x06008987 RID: 35207 RVA: 0x00295508 File Offset: 0x00295508
		public StringTable(string key) : base(key)
		{
			this._header.wType = 1;
		}

		// Token: 0x06008988 RID: 35208 RVA: 0x00295528 File Offset: 0x00295528
		internal StringTable(IntPtr lpRes)
		{
			this.Read(lpRes);
		}

		// Token: 0x06008989 RID: 35209 RVA: 0x00295544 File Offset: 0x00295544
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._strings.Clear();
			IntPtr lpRes2 = base.Read(lpRes);
			while (lpRes2.ToInt64() < lpRes.ToInt64() + (long)((ulong)this._header.wLength))
			{
				StringTableEntry stringTableEntry = new StringTableEntry(lpRes2);
				this._strings.Add(stringTableEntry.Key, stringTableEntry);
				lpRes2 = ResourceUtil.Align(lpRes2.ToInt64() + (long)((ulong)stringTableEntry.Header.wLength));
			}
			return new IntPtr(lpRes.ToInt64() + (long)((ulong)this._header.wLength));
		}

		// Token: 0x0600898A RID: 35210 RVA: 0x002955D8 File Offset: 0x002955D8
		internal override void Write(BinaryWriter w)
		{
			long position = w.BaseStream.Position;
			base.Write(w);
			int num = this._strings.Count;
			foreach (KeyValuePair<string, StringTableEntry> keyValuePair in this._strings)
			{
				keyValuePair.Value.Write(w);
				ResourceUtil.WriteAt(w, w.BaseStream.Position - position, position);
				if (--num != 0)
				{
					ResourceUtil.PadToDWORD(w);
				}
			}
		}

		// Token: 0x17001D48 RID: 7496
		// (get) Token: 0x0600898B RID: 35211 RVA: 0x0029565C File Offset: 0x0029565C
		// (set) Token: 0x0600898C RID: 35212 RVA: 0x00295684 File Offset: 0x00295684
		public ushort LanguageID
		{
			get
			{
				if (string.IsNullOrEmpty(this._key))
				{
					return 0;
				}
				return Convert.ToUInt16(this._key.Substring(0, 4), 16);
			}
			set
			{
				this._key = string.Format("{0:x4}{1:x4}", value, this.CodePage);
			}
		}

		// Token: 0x17001D49 RID: 7497
		// (get) Token: 0x0600898D RID: 35213 RVA: 0x002956A8 File Offset: 0x002956A8
		// (set) Token: 0x0600898E RID: 35214 RVA: 0x002956D0 File Offset: 0x002956D0
		public ushort CodePage
		{
			get
			{
				if (string.IsNullOrEmpty(this._key))
				{
					return 0;
				}
				return Convert.ToUInt16(this._key.Substring(4, 4), 16);
			}
			set
			{
				this._key = string.Format("{0:x4}{1:x4}", this.LanguageID, value);
			}
		}

		// Token: 0x17001D4A RID: 7498
		public string this[string key]
		{
			get
			{
				return this._strings[key].Value;
			}
			set
			{
				StringTableEntry stringTableEntry = null;
				if (!this._strings.TryGetValue(key, out stringTableEntry))
				{
					stringTableEntry = new StringTableEntry(key);
					this._strings.Add(key, stringTableEntry);
				}
				stringTableEntry.Value = value;
			}
		}

		// Token: 0x06008991 RID: 35217 RVA: 0x0029574C File Offset: 0x0029574C
		public override string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0}BEGIN", new string(' ', indent)));
			stringBuilder.AppendLine(string.Format("{0}BLOCK \"{1}\"", new string(' ', indent + 1), this._key));
			stringBuilder.AppendLine(string.Format("{0}BEGIN", new string(' ', indent + 1)));
			foreach (StringTableEntry stringTableEntry in this._strings.Values)
			{
				stringBuilder.AppendLine(string.Format("{0}VALUE \"{1}\", \"{2}\"", new string(' ', indent + 2), stringTableEntry.Key, stringTableEntry.StringValue));
			}
			stringBuilder.AppendLine(string.Format("{0}END", new string(' ', indent + 1)));
			stringBuilder.AppendLine(string.Format("{0}END", new string(' ', indent)));
			return stringBuilder.ToString();
		}

		// Token: 0x04003ED6 RID: 16086
		private Dictionary<string, StringTableEntry> _strings = new Dictionary<string, StringTableEntry>();
	}
}
