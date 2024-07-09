using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B5E RID: 2910
	[NullableContext(2)]
	[Nullable(0)]
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x06007533 RID: 30003 RVA: 0x002324F8 File Offset: 0x002324F8
		[Nullable(1)]
		private XAttribute Attribute
		{
			[NullableContext(1)]
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06007534 RID: 30004 RVA: 0x00232508 File Offset: 0x00232508
		[NullableContext(1)]
		public XAttributeWrapper(XAttribute attribute) : base(attribute)
		{
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06007535 RID: 30005 RVA: 0x00232514 File Offset: 0x00232514
		// (set) Token: 0x06007536 RID: 30006 RVA: 0x00232524 File Offset: 0x00232524
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value;
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x06007537 RID: 30007 RVA: 0x00232534 File Offset: 0x00232534
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x06007538 RID: 30008 RVA: 0x00232548 File Offset: 0x00232548
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06007539 RID: 30009 RVA: 0x0023255C File Offset: 0x0023255C
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Attribute.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Attribute.Parent);
			}
		}
	}
}
