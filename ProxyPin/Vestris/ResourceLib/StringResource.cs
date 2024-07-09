using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vestris.ResourceLib
{
	// Token: 0x02000D37 RID: 3383
	[ComVisible(true)]
	public class StringResource : Resource
	{
		// Token: 0x17001D44 RID: 7492
		// (get) Token: 0x06008977 RID: 35191 RVA: 0x00295274 File Offset: 0x00295274
		// (set) Token: 0x06008978 RID: 35192 RVA: 0x0029527C File Offset: 0x0029527C
		public Dictionary<ushort, string> Strings
		{
			get
			{
				return this._strings;
			}
			set
			{
				this._strings = value;
			}
		}

		// Token: 0x17001D45 RID: 7493
		public string this[ushort id]
		{
			get
			{
				return this._strings[id];
			}
			set
			{
				this._strings[id] = value;
			}
		}

		// Token: 0x0600897B RID: 35195 RVA: 0x002952A8 File Offset: 0x002952A8
		public StringResource() : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_STRING), null, ResourceUtil.NEUTRALLANGID, 0)
		{
		}

		// Token: 0x0600897C RID: 35196 RVA: 0x002952D4 File Offset: 0x002952D4
		public StringResource(ResourceId blockId) : base(IntPtr.Zero, IntPtr.Zero, new ResourceId(Kernel32.ResourceTypes.RT_STRING), blockId, ResourceUtil.NEUTRALLANGID, 0)
		{
		}

		// Token: 0x0600897D RID: 35197 RVA: 0x00295300 File Offset: 0x00295300
		public StringResource(ushort blockId) : this(new ResourceId((uint)blockId))
		{
		}

		// Token: 0x0600897E RID: 35198 RVA: 0x00295310 File Offset: 0x00295310
		public StringResource(IntPtr hModule, IntPtr hResource, ResourceId type, ResourceId name, ushort language, int size) : base(hModule, hResource, type, name, language, size)
		{
		}

		// Token: 0x0600897F RID: 35199 RVA: 0x0029532C File Offset: 0x0029532C
		public static ushort GetBlockId(int stringId)
		{
			return (ushort)(stringId / 16 + 1);
		}

		// Token: 0x17001D46 RID: 7494
		// (get) Token: 0x06008980 RID: 35200 RVA: 0x00295338 File Offset: 0x00295338
		// (set) Token: 0x06008981 RID: 35201 RVA: 0x00295360 File Offset: 0x00295360
		public ushort BlockId
		{
			get
			{
				return (ushort)base.Name.Id.ToInt64();
			}
			set
			{
				base.Name = new ResourceId((uint)value);
			}
		}

		// Token: 0x06008982 RID: 35202 RVA: 0x00295370 File Offset: 0x00295370
		internal override IntPtr Read(IntPtr hModule, IntPtr lpRes)
		{
			for (int i = 0; i < 16; i++)
			{
				ushort num = (ushort)Marshal.ReadInt16(lpRes);
				if (num != 0)
				{
					ushort key = (ushort)((int)((this.BlockId - 1) * 16) + i);
					string value = Marshal.PtrToStringUni(new IntPtr(lpRes.ToInt64() + 2L), (int)num);
					this._strings.Add(key, value);
				}
				lpRes = new IntPtr(lpRes.ToInt64() + 2L + (long)((int)num * Marshal.SystemDefaultCharSize));
			}
			return lpRes;
		}

		// Token: 0x06008983 RID: 35203 RVA: 0x002953EC File Offset: 0x002953EC
		internal override void Write(BinaryWriter w)
		{
			for (int i = 0; i < 16; i++)
			{
				ushort key = (ushort)((int)((this.BlockId - 1) * 16) + i);
				string text = null;
				if (this._strings.TryGetValue(key, out text))
				{
					w.Write((ushort)text.Length);
					w.Write(Encoding.Unicode.GetBytes(text));
				}
				else
				{
					w.Write(0);
				}
			}
		}

		// Token: 0x06008984 RID: 35204 RVA: 0x0029545C File Offset: 0x0029545C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("STRINGTABLE");
			stringBuilder.AppendLine("BEGIN");
			Dictionary<ushort, string>.Enumerator enumerator = this._strings.GetEnumerator();
			while (enumerator.MoveNext())
			{
				StringBuilder stringBuilder2 = stringBuilder;
				string format = " {0} {1}";
				KeyValuePair<ushort, string> keyValuePair = enumerator.Current;
				object arg = keyValuePair.Key;
				keyValuePair = enumerator.Current;
				stringBuilder2.AppendLine(string.Format(format, arg, keyValuePair.Value));
			}
			stringBuilder.AppendLine("END");
			return stringBuilder.ToString();
		}

		// Token: 0x04003ED5 RID: 16085
		private Dictionary<ushort, string> _strings = new Dictionary<ushort, string>();
	}
}
