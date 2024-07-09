using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B5B RID: 2907
	[NullableContext(2)]
	[Nullable(0)]
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x0600751C RID: 29980 RVA: 0x00232280 File Offset: 0x00232280
		[Nullable(1)]
		private XProcessingInstruction ProcessingInstruction
		{
			[NullableContext(1)]
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x0600751D RID: 29981 RVA: 0x00232290 File Offset: 0x00232290
		[NullableContext(1)]
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction) : base(processingInstruction)
		{
		}

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x0600751E RID: 29982 RVA: 0x0023229C File Offset: 0x0023229C
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x0600751F RID: 29983 RVA: 0x002322AC File Offset: 0x002322AC
		// (set) Token: 0x06007520 RID: 29984 RVA: 0x002322BC File Offset: 0x002322BC
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value;
			}
		}
	}
}
