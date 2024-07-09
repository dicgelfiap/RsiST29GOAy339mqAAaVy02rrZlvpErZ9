using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B52 RID: 2898
	[NullableContext(1)]
	internal interface IXmlDeclaration : IXmlNode
	{
		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x060074DC RID: 29916
		string Version { get; }

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x060074DD RID: 29917
		// (set) Token: 0x060074DE RID: 29918
		string Encoding { get; set; }

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x060074DF RID: 29919
		// (set) Token: 0x060074E0 RID: 29920
		string Standalone { get; set; }
	}
}
