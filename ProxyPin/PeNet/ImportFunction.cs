using System;
using System.Runtime.InteropServices;

namespace PeNet
{
	// Token: 0x02000B90 RID: 2960
	[ComVisible(true)]
	public class ImportFunction
	{
		// Token: 0x06007715 RID: 30485 RVA: 0x002392FC File Offset: 0x002392FC
		public ImportFunction(string name, string dll, ushort hint)
		{
			this.Name = name;
			this.DLL = dll;
			this.Hint = hint;
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06007716 RID: 30486 RVA: 0x0023931C File Offset: 0x0023931C
		public string Name { get; }

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06007717 RID: 30487 RVA: 0x00239324 File Offset: 0x00239324
		public string DLL { get; }

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06007718 RID: 30488 RVA: 0x0023932C File Offset: 0x0023932C
		public ushort Hint { get; }
	}
}
