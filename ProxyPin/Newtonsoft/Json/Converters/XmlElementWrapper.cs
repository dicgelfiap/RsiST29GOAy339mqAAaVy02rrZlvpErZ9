using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B4D RID: 2893
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x060074B1 RID: 29873 RVA: 0x00231AF0 File Offset: 0x00231AF0
		public XmlElementWrapper(XmlElement element) : base(element)
		{
			this._element = element;
		}

		// Token: 0x060074B2 RID: 29874 RVA: 0x00231B00 File Offset: 0x00231B00
		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		// Token: 0x060074B3 RID: 29875 RVA: 0x00231B30 File Offset: 0x00231B30
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this._element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x060074B4 RID: 29876 RVA: 0x00231B40 File Offset: 0x00231B40
		public bool IsEmpty
		{
			get
			{
				return this._element.IsEmpty;
			}
		}

		// Token: 0x040038E6 RID: 14566
		private readonly XmlElement _element;
	}
}
