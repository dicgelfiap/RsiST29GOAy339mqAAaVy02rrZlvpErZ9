using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet
{
	// Token: 0x0200078A RID: 1930
	[ComVisible(true)]
	public abstract class ClassLayout : IMDTokenProvider
	{
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x0016C8FC File Offset: 0x0016C8FC
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.ClassLayout, this.rid);
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004504 RID: 17668 RVA: 0x0016C90C File Offset: 0x0016C90C
		// (set) Token: 0x06004505 RID: 17669 RVA: 0x0016C914 File Offset: 0x0016C914
		public uint Rid
		{
			get
			{
				return this.rid;
			}
			set
			{
				this.rid = value;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06004506 RID: 17670 RVA: 0x0016C920 File Offset: 0x0016C920
		// (set) Token: 0x06004507 RID: 17671 RVA: 0x0016C928 File Offset: 0x0016C928
		public ushort PackingSize
		{
			get
			{
				return this.packingSize;
			}
			set
			{
				this.packingSize = value;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004508 RID: 17672 RVA: 0x0016C934 File Offset: 0x0016C934
		// (set) Token: 0x06004509 RID: 17673 RVA: 0x0016C93C File Offset: 0x0016C93C
		public uint ClassSize
		{
			get
			{
				return this.classSize;
			}
			set
			{
				this.classSize = value;
			}
		}

		// Token: 0x04002420 RID: 9248
		protected uint rid;

		// Token: 0x04002421 RID: 9249
		protected ushort packingSize;

		// Token: 0x04002422 RID: 9250
		protected uint classSize;
	}
}
