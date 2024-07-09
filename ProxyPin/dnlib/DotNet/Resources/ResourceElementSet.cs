using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Resources
{
	// Token: 0x020008E7 RID: 2279
	[ComVisible(true)]
	public sealed class ResourceElementSet
	{
		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x060058D5 RID: 22741 RVA: 0x001B480C File Offset: 0x001B480C
		public int Count
		{
			get
			{
				return this.dict.Count;
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x060058D6 RID: 22742 RVA: 0x001B481C File Offset: 0x001B481C
		public IEnumerable<ResourceElement> ResourceElements
		{
			get
			{
				return this.dict.Values;
			}
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x001B482C File Offset: 0x001B482C
		public void Add(ResourceElement elem)
		{
			this.dict[elem.Name] = elem;
		}

		// Token: 0x04002AEB RID: 10987
		private readonly Dictionary<string, ResourceElement> dict = new Dictionary<string, ResourceElement>(StringComparer.Ordinal);
	}
}
