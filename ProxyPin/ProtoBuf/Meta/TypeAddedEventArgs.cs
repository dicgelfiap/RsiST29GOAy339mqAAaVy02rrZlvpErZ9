using System;
using System.Runtime.InteropServices;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C7A RID: 3194
	[ComVisible(true)]
	public sealed class TypeAddedEventArgs : EventArgs
	{
		// Token: 0x06007F75 RID: 32629 RVA: 0x0025AC9C File Offset: 0x0025AC9C
		internal TypeAddedEventArgs(MetaType metaType)
		{
			this.MetaType = metaType;
			this.ApplyDefaultBehaviour = true;
		}

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x06007F76 RID: 32630 RVA: 0x0025ACB4 File Offset: 0x0025ACB4
		// (set) Token: 0x06007F77 RID: 32631 RVA: 0x0025ACBC File Offset: 0x0025ACBC
		public bool ApplyDefaultBehaviour { get; set; }

		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x06007F78 RID: 32632 RVA: 0x0025ACC8 File Offset: 0x0025ACC8
		public MetaType MetaType { get; }

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x06007F79 RID: 32633 RVA: 0x0025ACD0 File Offset: 0x0025ACD0
		public Type Type
		{
			get
			{
				return this.MetaType.Type;
			}
		}

		// Token: 0x17001BB4 RID: 7092
		// (get) Token: 0x06007F7A RID: 32634 RVA: 0x0025ACE0 File Offset: 0x0025ACE0
		public RuntimeTypeModel Model
		{
			get
			{
				return this.MetaType.Model as RuntimeTypeModel;
			}
		}
	}
}
