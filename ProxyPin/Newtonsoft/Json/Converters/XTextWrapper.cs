using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B59 RID: 2905
	[NullableContext(2)]
	[Nullable(0)]
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x06007512 RID: 29970 RVA: 0x002321C0 File Offset: 0x002321C0
		[Nullable(1)]
		private XText Text
		{
			[NullableContext(1)]
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x06007513 RID: 29971 RVA: 0x002321D0 File Offset: 0x002321D0
		[NullableContext(1)]
		public XTextWrapper(XText text) : base(text)
		{
		}

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x06007514 RID: 29972 RVA: 0x002321DC File Offset: 0x002321DC
		// (set) Token: 0x06007515 RID: 29973 RVA: 0x002321EC File Offset: 0x002321EC
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value;
			}
		}

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x06007516 RID: 29974 RVA: 0x002321FC File Offset: 0x002321FC
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Text.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Text.Parent);
			}
		}
	}
}
