using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace dnlib.Utils
{
	// Token: 0x02000739 RID: 1849
	internal class CollectionDebugView<TValue>
	{
		// Token: 0x060040E1 RID: 16609 RVA: 0x00162014 File Offset: 0x00162014
		public CollectionDebugView(ICollection<TValue> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			this.list = list;
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x00162038 File Offset: 0x00162038
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this.list.Count];
				this.list.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04002288 RID: 8840
		private readonly ICollection<TValue> list;
	}
}
