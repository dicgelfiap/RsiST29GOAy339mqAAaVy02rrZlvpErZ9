using System;
using System.Runtime.InteropServices;

namespace PeNet.Structures.MetaDataTables
{
	// Token: 0x02000BD1 RID: 3025
	[ComVisible(true)]
	public class DeclSecurity : AbstractTable
	{
		// Token: 0x060079D9 RID: 31193 RVA: 0x00240290 File Offset: 0x00240290
		public DeclSecurity(byte[] buff, uint offset, HeapSizes heapSizes, IndexSize indexSizes) : base(buff, offset, heapSizes, indexSizes)
		{
			this.Action = (ushort)base.ReadSize(2U);
			this.Parent = base.ReadSize(base.IndexSizes[Index.HasDeclSecurity]);
			this.PermissionSet = base.ReadSize(base.HeapSizes.Blob);
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x060079DA RID: 31194 RVA: 0x002402EC File Offset: 0x002402EC
		public ushort Action { get; }

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x060079DB RID: 31195 RVA: 0x002402F4 File Offset: 0x002402F4
		public uint Parent { get; }

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x060079DC RID: 31196 RVA: 0x002402FC File Offset: 0x002402FC
		public uint PermissionSet { get; }
	}
}
