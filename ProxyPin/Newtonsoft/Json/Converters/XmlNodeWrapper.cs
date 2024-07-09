using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B50 RID: 2896
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlNodeWrapper : IXmlNode
	{
		// Token: 0x060074C1 RID: 29889 RVA: 0x00231C08 File Offset: 0x00231C08
		[NullableContext(1)]
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x060074C2 RID: 29890 RVA: 0x00231C18 File Offset: 0x00231C18
		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x060074C3 RID: 29891 RVA: 0x00231C20 File Offset: 0x00231C20
		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x060074C4 RID: 29892 RVA: 0x00231C30 File Offset: 0x00231C30
		public virtual string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x060074C5 RID: 29893 RVA: 0x00231C40 File Offset: 0x00231C40
		[Nullable(1)]
		public List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				if (this._childNodes == null)
				{
					if (!this._node.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>(this._node.ChildNodes.Count);
						foreach (object obj in this._node.ChildNodes)
						{
							XmlNode node = (XmlNode)obj;
							this._childNodes.Add(XmlNodeWrapper.WrapNode(node));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x060074C6 RID: 29894 RVA: 0x00231D00 File Offset: 0x00231D00
		protected virtual bool HasChildNodes
		{
			get
			{
				return this._node.HasChildNodes;
			}
		}

		// Token: 0x060074C7 RID: 29895 RVA: 0x00231D10 File Offset: 0x00231D10
		[NullableContext(1)]
		internal static IXmlNode WrapNode(XmlNode node)
		{
			XmlNodeType nodeType = node.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return new XmlElementWrapper((XmlElement)node);
			}
			if (nodeType == XmlNodeType.DocumentType)
			{
				return new XmlDocumentTypeWrapper((XmlDocumentType)node);
			}
			if (nodeType != XmlNodeType.XmlDeclaration)
			{
				return new XmlNodeWrapper(node);
			}
			return new XmlDeclarationWrapper((XmlDeclaration)node);
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x060074C8 RID: 29896 RVA: 0x00231D70 File Offset: 0x00231D70
		[Nullable(1)]
		public List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				if (this._attributes == null)
				{
					if (!this.HasAttributes)
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>(this._node.Attributes.Count);
						foreach (object obj in this._node.Attributes)
						{
							XmlAttribute node = (XmlAttribute)obj;
							this._attributes.Add(XmlNodeWrapper.WrapNode(node));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x060074C9 RID: 29897 RVA: 0x00231E28 File Offset: 0x00231E28
		private bool HasAttributes
		{
			get
			{
				XmlElement xmlElement = this._node as XmlElement;
				if (xmlElement != null)
				{
					return xmlElement.HasAttributes;
				}
				XmlAttributeCollection attributes = this._node.Attributes;
				return attributes != null && attributes.Count > 0;
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x060074CA RID: 29898 RVA: 0x00231E70 File Offset: 0x00231E70
		public IXmlNode ParentNode
		{
			get
			{
				XmlAttribute xmlAttribute = this._node as XmlAttribute;
				XmlNode xmlNode = (xmlAttribute != null) ? xmlAttribute.OwnerElement : this._node.ParentNode;
				if (xmlNode == null)
				{
					return null;
				}
				return XmlNodeWrapper.WrapNode(xmlNode);
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x060074CB RID: 29899 RVA: 0x00231EB8 File Offset: 0x00231EB8
		// (set) Token: 0x060074CC RID: 29900 RVA: 0x00231EC8 File Offset: 0x00231EC8
		public string Value
		{
			get
			{
				return this._node.Value;
			}
			set
			{
				this._node.Value = value;
			}
		}

		// Token: 0x060074CD RID: 29901 RVA: 0x00231ED8 File Offset: 0x00231ED8
		[NullableContext(1)]
		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			this._childNodes = null;
			this._attributes = null;
			return newChild;
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x060074CE RID: 29902 RVA: 0x00231F14 File Offset: 0x00231F14
		public string NamespaceUri
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		// Token: 0x040038E9 RID: 14569
		[Nullable(1)]
		private readonly XmlNode _node;

		// Token: 0x040038EA RID: 14570
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<IXmlNode> _childNodes;

		// Token: 0x040038EB RID: 14571
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<IXmlNode> _attributes;
	}
}
