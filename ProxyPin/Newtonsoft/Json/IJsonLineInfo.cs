using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000A74 RID: 2676
	public interface IJsonLineInfo
	{
		// Token: 0x0600683B RID: 26683
		bool HasLineInfo();

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x0600683C RID: 26684
		int LineNumber { get; }

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x0600683D RID: 26685
		int LinePosition { get; }
	}
}
