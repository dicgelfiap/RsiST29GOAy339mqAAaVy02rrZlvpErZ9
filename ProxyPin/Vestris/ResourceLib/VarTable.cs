using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D3C RID: 3388
	[ComVisible(true)]
	public class VarTable : ResourceTableHeader
	{
		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x060089A8 RID: 35240 RVA: 0x00295D8C File Offset: 0x00295D8C
		public Dictionary<ushort, ushort> Languages
		{
			get
			{
				return this._languages;
			}
		}

		// Token: 0x060089A9 RID: 35241 RVA: 0x00295D94 File Offset: 0x00295D94
		public VarTable()
		{
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x00295DA8 File Offset: 0x00295DA8
		public VarTable(string key) : base(key)
		{
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x00295DBC File Offset: 0x00295DBC
		internal VarTable(IntPtr lpRes)
		{
			this.Read(lpRes);
		}

		// Token: 0x060089AC RID: 35244 RVA: 0x00295DD8 File Offset: 0x00295DD8
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._languages.Clear();
			IntPtr ptr = base.Read(lpRes);
			while (ptr.ToInt64() < lpRes.ToInt64() + (long)((ulong)this._header.wLength))
			{
				Kernel32.VAR_HEADER var_HEADER = (Kernel32.VAR_HEADER)Marshal.PtrToStructure(ptr, typeof(Kernel32.VAR_HEADER));
				this._languages.Add(var_HEADER.wLanguageIDMS, var_HEADER.wCodePageIBM);
				ptr = new IntPtr(ptr.ToInt64() + (long)Marshal.SizeOf(var_HEADER));
			}
			return new IntPtr(lpRes.ToInt64() + (long)((ulong)this._header.wLength));
		}

		// Token: 0x060089AD RID: 35245 RVA: 0x00295E80 File Offset: 0x00295E80
		internal override void Write(BinaryWriter w)
		{
			long position = w.BaseStream.Position;
			base.Write(w);
			Dictionary<ushort, ushort>.Enumerator enumerator = this._languages.GetEnumerator();
			long position2 = w.BaseStream.Position;
			while (enumerator.MoveNext())
			{
				KeyValuePair<ushort, ushort> keyValuePair = enumerator.Current;
				w.Write(keyValuePair.Key);
				keyValuePair = enumerator.Current;
				w.Write(keyValuePair.Value);
			}
			ResourceUtil.WriteAt(w, w.BaseStream.Position - position2, position + 2L);
			ResourceUtil.PadToDWORD(w);
			ResourceUtil.WriteAt(w, w.BaseStream.Position - position, position);
		}

		// Token: 0x17001D53 RID: 7507
		public ushort this[ushort key]
		{
			get
			{
				return this._languages[key];
			}
			set
			{
				this._languages[key] = value;
			}
		}

		// Token: 0x060089B0 RID: 35248 RVA: 0x00295F48 File Offset: 0x00295F48
		public override string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0}BEGIN", new string(' ', indent)));
			Dictionary<ushort, ushort>.Enumerator enumerator = this._languages.GetEnumerator();
			while (enumerator.MoveNext())
			{
				StringBuilder stringBuilder2 = stringBuilder;
				string format = "{0}VALUE \"Translation\", 0x{1:x}, 0x{2:x}";
				object arg = new string(' ', indent + 1);
				KeyValuePair<ushort, ushort> keyValuePair = enumerator.Current;
				object arg2 = keyValuePair.Key;
				keyValuePair = enumerator.Current;
				stringBuilder2.AppendLine(string.Format(format, arg, arg2, keyValuePair.Value));
			}
			stringBuilder.AppendLine(string.Format("{0}END", new string(' ', indent)));
			return stringBuilder.ToString();
		}

		// Token: 0x04003EDC RID: 16092
		private Dictionary<ushort, ushort> _languages = new Dictionary<ushort, ushort>();
	}
}
