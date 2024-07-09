using System;
using System.Runtime.InteropServices;
using PeNet.Utilities;

namespace PeNet.Structures
{
	// Token: 0x02000BAA RID: 2986
	[ComVisible(true)]
	public class IMAGE_IMPORT_BY_NAME : AbstractStructure
	{
		// Token: 0x0600784B RID: 30795 RVA: 0x0023C758 File Offset: 0x0023C758
		public IMAGE_IMPORT_BY_NAME(byte[] buff, uint offset) : base(buff, offset)
		{
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x0600784C RID: 30796 RVA: 0x0023C764 File Offset: 0x0023C764
		// (set) Token: 0x0600784D RID: 30797 RVA: 0x0023C778 File Offset: 0x0023C778
		public ushort Hint
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

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x0600784E RID: 30798 RVA: 0x0023C790 File Offset: 0x0023C790
		public string Name
		{
			get
			{
				return this.Buff.GetCString((ulong)(this.Offset + 2U));
			}
		}
	}
}
