using System;
using System.Runtime.InteropServices;
using System.Text;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BB2 RID: 2994
	[ComVisible(true)]
	public class IMAGE_RESOURCE_DIR_STRING_U : AbstractStructure
	{
		// Token: 0x060078F6 RID: 30966 RVA: 0x0023DEB4 File Offset: 0x0023DEB4
		public IMAGE_RESOURCE_DIR_STRING_U(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x060078F7 RID: 30967 RVA: 0x0023DEC0 File Offset: 0x0023DEC0
		// (set) Token: 0x060078F8 RID: 30968 RVA: 0x0023DED4 File Offset: 0x0023DED4
		public ushort Length
		{
			get
			{
				return this.Buff.BytesToUInt16((ulong)this.Offset);
			}
			set
			{
				this.Buff.SetUInt16((ulong)this.Offset, value);
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x060078F9 RID: 30969 RVA: 0x0023DEEC File Offset: 0x0023DEEC
		public string NameString
		{
			get
			{
				byte[] array = new byte[(int)(this.Length * 2)];
				Array.Copy(this.Buff, (long)((ulong)(this.Offset + 2U)), array, 0L, (long)(this.Length * 2));
				return Encoding.Unicode.GetString(array);
			}
		}
	}
}
