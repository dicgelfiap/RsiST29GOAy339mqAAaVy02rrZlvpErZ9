using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B54 RID: 2900
	[NullableContext(1)]
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x060074E5 RID: 29925
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x060074E6 RID: 29926
		string GetPrefixOfNamespace(string namespaceUri);

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x060074E7 RID: 29927
		bool IsEmpty { get; }
	}
}
