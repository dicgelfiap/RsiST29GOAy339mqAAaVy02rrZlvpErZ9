using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B5A RID: 2906
	[NullableContext(2)]
	[Nullable(0)]
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x06007517 RID: 29975 RVA: 0x00232220 File Offset: 0x00232220
		[Nullable(1)]
		private XComment Text
		{
			[NullableContext(1)]
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x00232230 File Offset: 0x00232230
		[NullableContext(1)]
		public XCommentWrapper(XComment text) : base(text)
		{
		}

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06007519 RID: 29977 RVA: 0x0023223C File Offset: 0x0023223C
		// (set) Token: 0x0600751A RID: 29978 RVA: 0x0023224C File Offset: 0x0023224C
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

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x0600751B RID: 29979 RVA: 0x0023225C File Offset: 0x0023225C
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
