using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B5F RID: 2911
	[NullableContext(1)]
	[Nullable(0)]
	internal class XElementWrapper : XContainerWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x0600753A RID: 30010 RVA: 0x00232580 File Offset: 0x00232580
		private XElement Element
		{
			get
			{
				return (XElement)base.WrappedNode;
			}
		}

		// Token: 0x0600753B RID: 30011 RVA: 0x00232590 File Offset: 0x00232590
		public XElementWrapper(XElement element) : base(element)
		{
		}

		// Token: 0x0600753C RID: 30012 RVA: 0x0023259C File Offset: 0x0023259C
		public void SetAttributeNode(IXmlNode attribute)
		{
			XObjectWrapper xobjectWrapper = (XObjectWrapper)attribute;
			this.Element.Add(xobjectWrapper.WrappedNode);
			this._attributes = null;
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x0600753D RID: 30013 RVA: 0x002325CC File Offset: 0x002325CC
		public override List<IXmlNode> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (!this.Element.HasAttributes && !this.HasImplicitNamespaceAttribute(this.NamespaceUri))
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>();
						foreach (XAttribute attribute in this.Element.Attributes())
						{
							this._attributes.Add(new XAttributeWrapper(attribute));
						}
						string namespaceUri = this.NamespaceUri;
						if (this.HasImplicitNamespaceAttribute(namespaceUri))
						{
							this._attributes.Insert(0, new XAttributeWrapper(new XAttribute("xmlns", namespaceUri)));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x0600753E RID: 30014 RVA: 0x002326B4 File Offset: 0x002326B4
		private bool HasImplicitNamespaceAttribute(string namespaceUri)
		{
			if (!StringUtils.IsNullOrEmpty(namespaceUri))
			{
				IXmlNode parentNode = this.ParentNode;
				if (namespaceUri != ((parentNode != null) ? parentNode.NamespaceUri : null) && StringUtils.IsNullOrEmpty(this.GetPrefixOfNamespace(namespaceUri)))
				{
					bool flag = false;
					if (this.Element.HasAttributes)
					{
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							if (xattribute.Name.LocalName == "xmlns" && StringUtils.IsNullOrEmpty(xattribute.Name.NamespaceName) && xattribute.Value == namespaceUri)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600753F RID: 30015 RVA: 0x002327A4 File Offset: 0x002327A4
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			IXmlNode result = base.AppendChild(newChild);
			this._attributes = null;
			return result;
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x06007540 RID: 30016 RVA: 0x002327B4 File Offset: 0x002327B4
		// (set) Token: 0x06007541 RID: 30017 RVA: 0x002327C4 File Offset: 0x002327C4
		[Nullable(2)]
		public override string Value
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Value;
			}
			[NullableContext(2)]
			set
			{
				this.Element.Value = value;
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x06007542 RID: 30018 RVA: 0x002327D4 File Offset: 0x002327D4
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.LocalName;
			}
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x06007543 RID: 30019 RVA: 0x002327E8 File Offset: 0x002327E8
		[Nullable(2)]
		public override string NamespaceUri
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.NamespaceName;
			}
		}

		// Token: 0x06007544 RID: 30020 RVA: 0x002327FC File Offset: 0x002327FC
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this.Element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x06007545 RID: 30021 RVA: 0x00232810 File Offset: 0x00232810
		public bool IsEmpty
		{
			get
			{
				return this.Element.IsEmpty;
			}
		}

		// Token: 0x040038F0 RID: 14576
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<IXmlNode> _attributes;
	}
}
