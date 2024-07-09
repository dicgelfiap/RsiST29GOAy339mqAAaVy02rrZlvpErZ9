using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B5D RID: 2909
	[NullableContext(2)]
	[Nullable(0)]
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x06007528 RID: 29992 RVA: 0x00232498 File Offset: 0x00232498
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06007529 RID: 29993 RVA: 0x002324A8 File Offset: 0x002324A8
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x0600752A RID: 29994 RVA: 0x002324B0 File Offset: 0x002324B0
		public virtual XmlNodeType NodeType
		{
			get
			{
				XObject xmlObject = this._xmlObject;
				if (xmlObject == null)
				{
					return XmlNodeType.None;
				}
				return xmlObject.NodeType;
			}
		}

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x0600752B RID: 29995 RVA: 0x002324C8 File Offset: 0x002324C8
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x0600752C RID: 29996 RVA: 0x002324CC File Offset: 0x002324CC
		[Nullable(1)]
		public virtual List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x0600752D RID: 29997 RVA: 0x002324D4 File Offset: 0x002324D4
		[Nullable(1)]
		public virtual List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x0600752E RID: 29998 RVA: 0x002324DC File Offset: 0x002324DC
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x0600752F RID: 29999 RVA: 0x002324E0 File Offset: 0x002324E0
		// (set) Token: 0x06007530 RID: 30000 RVA: 0x002324E4 File Offset: 0x002324E4
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06007531 RID: 30001 RVA: 0x002324EC File Offset: 0x002324EC
		[NullableContext(1)]
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x06007532 RID: 30002 RVA: 0x002324F4 File Offset: 0x002324F4
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040038EF RID: 14575
		private readonly XObject _xmlObject;
	}
}
