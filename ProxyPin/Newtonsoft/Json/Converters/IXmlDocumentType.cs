using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B53 RID: 2899
	[NullableContext(1)]
	internal interface IXmlDocumentType : IXmlNode
	{
		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x060074E1 RID: 29921
		string Name { get; }

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x060074E2 RID: 29922
		string System { get; }

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x060074E3 RID: 29923
		string Public { get; }

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x060074E4 RID: 29924
		string InternalSubset { get; }
	}
}
