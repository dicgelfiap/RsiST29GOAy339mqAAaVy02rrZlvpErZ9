using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B58 RID: 2904
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDocumentWrapper : XContainerWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06007500 RID: 29952 RVA: 0x00231FE8 File Offset: 0x00231FE8
		private XDocument Document
		{
			get
			{
				return (XDocument)base.WrappedNode;
			}
		}

		// Token: 0x06007501 RID: 29953 RVA: 0x00231FF8 File Offset: 0x00231FF8
		public XDocumentWrapper(XDocument document) : base(document)
		{
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06007502 RID: 29954 RVA: 0x00232004 File Offset: 0x00232004
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				List<IXmlNode> childNodes = base.ChildNodes;
				if (this.Document.Declaration != null && (childNodes.Count == 0 || childNodes[0].NodeType != XmlNodeType.XmlDeclaration))
				{
					childNodes.Insert(0, new XDeclarationWrapper(this.Document.Declaration));
				}
				return childNodes;
			}
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06007503 RID: 29955 RVA: 0x00232064 File Offset: 0x00232064
		protected override bool HasChildNodes
		{
			get
			{
				return base.HasChildNodes || this.Document.Declaration != null;
			}
		}

		// Token: 0x06007504 RID: 29956 RVA: 0x00232084 File Offset: 0x00232084
		public IXmlNode CreateComment([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XComment(text));
		}

		// Token: 0x06007505 RID: 29957 RVA: 0x00232094 File Offset: 0x00232094
		public IXmlNode CreateTextNode([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06007506 RID: 29958 RVA: 0x002320A4 File Offset: 0x002320A4
		public IXmlNode CreateCDataSection([Nullable(2)] string data)
		{
			return new XObjectWrapper(new XCData(data));
		}

		// Token: 0x06007507 RID: 29959 RVA: 0x002320B4 File Offset: 0x002320B4
		public IXmlNode CreateWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06007508 RID: 29960 RVA: 0x002320C4 File Offset: 0x002320C4
		public IXmlNode CreateSignificantWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06007509 RID: 29961 RVA: 0x002320D4 File Offset: 0x002320D4
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XDeclarationWrapper(new XDeclaration(version, encoding, standalone));
		}

		// Token: 0x0600750A RID: 29962 RVA: 0x002320E4 File Offset: 0x002320E4
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XDocumentTypeWrapper(new XDocumentType(name, publicId, systemId, internalSubset));
		}

		// Token: 0x0600750B RID: 29963 RVA: 0x002320F8 File Offset: 0x002320F8
		public IXmlNode CreateProcessingInstruction(string target, [Nullable(2)] string data)
		{
			return new XProcessingInstructionWrapper(new XProcessingInstruction(target, data));
		}

		// Token: 0x0600750C RID: 29964 RVA: 0x00232108 File Offset: 0x00232108
		public IXmlElement CreateElement(string elementName)
		{
			return new XElementWrapper(new XElement(elementName));
		}

		// Token: 0x0600750D RID: 29965 RVA: 0x0023211C File Offset: 0x0023211C
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XElementWrapper(new XElement(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri)));
		}

		// Token: 0x0600750E RID: 29966 RVA: 0x00232134 File Offset: 0x00232134
		public IXmlNode CreateAttribute(string name, [Nullable(2)] string value)
		{
			return new XAttributeWrapper(new XAttribute(name, value));
		}

		// Token: 0x0600750F RID: 29967 RVA: 0x00232148 File Offset: 0x00232148
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, [Nullable(2)] string value)
		{
			return new XAttributeWrapper(new XAttribute(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri), value));
		}

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06007510 RID: 29968 RVA: 0x00232164 File Offset: 0x00232164
		[Nullable(2)]
		public IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get
			{
				if (this.Document.Root == null)
				{
					return null;
				}
				return new XElementWrapper(this.Document.Root);
			}
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x00232188 File Offset: 0x00232188
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			XDeclarationWrapper xdeclarationWrapper = newChild as XDeclarationWrapper;
			if (xdeclarationWrapper != null)
			{
				this.Document.Declaration = xdeclarationWrapper.Declaration;
				return xdeclarationWrapper;
			}
			return base.AppendChild(newChild);
		}
	}
}
