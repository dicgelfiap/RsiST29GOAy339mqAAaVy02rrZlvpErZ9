using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B51 RID: 2897
	[NullableContext(1)]
	internal interface IXmlDocument : IXmlNode
	{
		// Token: 0x060074CF RID: 29903
		IXmlNode CreateComment([Nullable(2)] string text);

		// Token: 0x060074D0 RID: 29904
		IXmlNode CreateTextNode([Nullable(2)] string text);

		// Token: 0x060074D1 RID: 29905
		IXmlNode CreateCDataSection([Nullable(2)] string data);

		// Token: 0x060074D2 RID: 29906
		IXmlNode CreateWhitespace([Nullable(2)] string text);

		// Token: 0x060074D3 RID: 29907
		IXmlNode CreateSignificantWhitespace([Nullable(2)] string text);

		// Token: 0x060074D4 RID: 29908
		[NullableContext(2)]
		[return: Nullable(1)]
		IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone);

		// Token: 0x060074D5 RID: 29909
		[NullableContext(2)]
		[return: Nullable(1)]
		IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset);

		// Token: 0x060074D6 RID: 29910
		IXmlNode CreateProcessingInstruction(string target, [Nullable(2)] string data);

		// Token: 0x060074D7 RID: 29911
		IXmlElement CreateElement(string elementName);

		// Token: 0x060074D8 RID: 29912
		IXmlElement CreateElement(string qualifiedName, string namespaceUri);

		// Token: 0x060074D9 RID: 29913
		IXmlNode CreateAttribute(string name, [Nullable(2)] string value);

		// Token: 0x060074DA RID: 29914
		IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, [Nullable(2)] string value);

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x060074DB RID: 29915
		[Nullable(2)]
		IXmlElement DocumentElement { [NullableContext(2)] get; }
	}
}
