using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B55 RID: 2901
	[NullableContext(2)]
	internal interface IXmlNode
	{
		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x060074E8 RID: 29928
		XmlNodeType NodeType { get; }

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x060074E9 RID: 29929
		string LocalName { get; }

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x060074EA RID: 29930
		[Nullable(1)]
		List<IXmlNode> ChildNodes { [NullableContext(1)] get; }

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x060074EB RID: 29931
		[Nullable(1)]
		List<IXmlNode> Attributes { [NullableContext(1)] get; }

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x060074EC RID: 29932
		IXmlNode ParentNode { get; }

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x060074ED RID: 29933
		// (set) Token: 0x060074EE RID: 29934
		string Value { get; set; }

		// Token: 0x060074EF RID: 29935
		[NullableContext(1)]
		IXmlNode AppendChild(IXmlNode newChild);

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x060074F0 RID: 29936
		string NamespaceUri { get; }

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x060074F1 RID: 29937
		object WrappedNode { get; }
	}
}
