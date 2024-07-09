using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D3B RID: 3387
	[ComVisible(true)]
	public class VarFileInfo : ResourceTableHeader
	{
		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x0600899F RID: 35231 RVA: 0x00295B44 File Offset: 0x00295B44
		public Dictionary<string, VarTable> Vars
		{
			get
			{
				return this._vars;
			}
		}

		// Token: 0x060089A0 RID: 35232 RVA: 0x00295B4C File Offset: 0x00295B4C
		public VarFileInfo() : base("VarFileInfo")
		{
			this._header.wType = 1;
		}

		// Token: 0x060089A1 RID: 35233 RVA: 0x00295B70 File Offset: 0x00295B70
		internal VarFileInfo(IntPtr lpRes)
		{
			this.Read(lpRes);
		}

		// Token: 0x060089A2 RID: 35234 RVA: 0x00295B8C File Offset: 0x00295B8C
		internal override IntPtr Read(IntPtr lpRes)
		{
			this._vars.Clear();
			IntPtr lpRes2 = base.Read(lpRes);
			while (lpRes2.ToInt64() < lpRes.ToInt64() + (long)((ulong)this._header.wLength))
			{
				VarTable varTable = new VarTable(lpRes2);
				this._vars.Add(varTable.Key, varTable);
				lpRes2 = ResourceUtil.Align(lpRes2.ToInt64() + (long)((ulong)varTable.Header.wLength));
			}
			return new IntPtr(lpRes.ToInt64() + (long)((ulong)this._header.wLength));
		}

		// Token: 0x060089A3 RID: 35235 RVA: 0x00295C20 File Offset: 0x00295C20
		internal override void Write(BinaryWriter w)
		{
			long position = w.BaseStream.Position;
			base.Write(w);
			foreach (KeyValuePair<string, VarTable> keyValuePair in this._vars)
			{
				keyValuePair.Value.Write(w);
			}
			ResourceUtil.WriteAt(w, w.BaseStream.Position - position, position);
		}

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x060089A4 RID: 35236 RVA: 0x00295C88 File Offset: 0x00295C88
		public VarTable Default
		{
			get
			{
				Dictionary<string, VarTable>.Enumerator enumerator = this._vars.GetEnumerator();
				if (enumerator.MoveNext())
				{
					KeyValuePair<string, VarTable> keyValuePair = enumerator.Current;
					return keyValuePair.Value;
				}
				return null;
			}
		}

		// Token: 0x17001D51 RID: 7505
		public ushort this[ushort language]
		{
			get
			{
				return this.Default[language];
			}
			set
			{
				this.Default[language] = value;
			}
		}

		// Token: 0x060089A7 RID: 35239 RVA: 0x00295CE4 File Offset: 0x00295CE4
		public override string ToString(int indent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0}BEGIN", new string(' ', indent)));
			foreach (VarTable varTable in this._vars.Values)
			{
				stringBuilder.Append(varTable.ToString(indent + 1));
			}
			stringBuilder.AppendLine(string.Format("{0}END", new string(' ', indent)));
			return stringBuilder.ToString();
		}

		// Token: 0x04003EDB RID: 16091
		private Dictionary<string, VarTable> _vars = new Dictionary<string, VarTable>();
	}
}
