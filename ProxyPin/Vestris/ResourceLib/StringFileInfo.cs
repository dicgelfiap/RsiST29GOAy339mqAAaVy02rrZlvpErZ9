using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D36 RID: 3382
	[ComVisible(true)]
	public class StringFileInfo : ResourceTableHeader
	{
		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x0600896E RID: 35182 RVA: 0x00295004 File Offset: 0x00295004
		public Dictionary<string, StringTable> Strings
		{
			get
			{
				return this._strings;
			}
		}

		// Token: 0x0600896F RID: 35183 RVA: 0x0029500C File Offset: 0x0029500C
		public StringFileInfo() : base("StringFileInfo")
		{
			this._header.wType = 1;
		}

		// Token: 0x06008970 RID: 35184 RVA: 0x00295030 File Offset: 0x00295030
		internal StringFileInfo(IntPtr lpRes)
		{
			this.Read(lpRes);
		}

		// Token: 0x06008971 RID: 35185 RVA: 0x0029504C File Offset: 0x0029504C
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._strings.Clear();
			IntPtr lpRes2 = base.Read(lpRes);
			while (lpRes2.ToInt64() < lpRes.ToInt64() + (long)((ulong)this._header.wLength))
			{
				StringTable stringTable = new StringTable(lpRes2);
				this._strings.Add(stringTable.Key, stringTable);
				lpRes2 = ResourceUtil.Align(lpRes2.ToInt64() + (long)((ulong)stringTable.Header.wLength));
			}
			return new IntPtr(lpRes.ToInt64() + (long)((ulong)this._header.wLength));
		}

		// Token: 0x06008972 RID: 35186 RVA: 0x002950E0 File Offset: 0x002950E0
		internal override void Write(BinaryWriter w)
		{
			long position = w.BaseStream.Position;
			base.Write(w);
			foreach (KeyValuePair<string, StringTable> keyValuePair in this._strings)
			{
				keyValuePair.Value.Write(w);
			}
			ResourceUtil.WriteAt(w, w.BaseStream.Position - position, position);
			ResourceUtil.PadToDWORD(w);
		}

		// Token: 0x17001D42 RID: 7490
		// (get) Token: 0x06008973 RID: 35187 RVA: 0x00295150 File Offset: 0x00295150
		public StringTable Default
		{
			get
			{
				Dictionary<string, StringTable>.Enumerator enumerator = this._strings.GetEnumerator();
				if (enumerator.MoveNext())
				{
					KeyValuePair<string, StringTable> keyValuePair = enumerator.Current;
					return keyValuePair.Value;
				}
				return null;
			}
		}

		// Token: 0x17001D43 RID: 7491
		public string this[string key]
		{
			get
			{
				return this.Default[key];
			}
			set
			{
				this.Default[key] = value;
			}
		}

		// Token: 0x06008976 RID: 35190 RVA: 0x002951AC File Offset: 0x002951AC
		public override string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0}BEGIN", new string(' ', indent)));
			stringBuilder.AppendLine(string.Format("{0}BLOCK \"{1}\"", new string(' ', indent + 1), this._key));
			foreach (StringTable stringTable in this._strings.Values)
			{
				stringBuilder.Append(stringTable.ToString(indent + 1));
			}
			stringBuilder.AppendLine(string.Format("{0}END", new string(' ', indent)));
			return stringBuilder.ToString();
		}

		// Token: 0x04003ED4 RID: 16084
		private Dictionary<string, StringTable> _strings = new Dictionary<string, StringTable>();
	}
}
