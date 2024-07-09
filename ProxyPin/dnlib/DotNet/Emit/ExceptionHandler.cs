using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet.Emit
{
	// Token: 0x020009DC RID: 2524
	[ComVisible(true)]
	public sealed class ExceptionHandler
	{
		// Token: 0x060060BB RID: 24763 RVA: 0x001CD8F0 File Offset: 0x001CD8F0
		public ExceptionHandler()
		{
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x001CD8F8 File Offset: 0x001CD8F8
		public ExceptionHandler(ExceptionHandlerType handlerType)
		{
			this.HandlerType = handlerType;
		}

		// Token: 0x04003085 RID: 12421
		public Instruction TryStart;

		// Token: 0x04003086 RID: 12422
		public Instruction TryEnd;

		// Token: 0x04003087 RID: 12423
		public Instruction FilterStart;

		// Token: 0x04003088 RID: 12424
		public Instruction HandlerStart;

		// Token: 0x04003089 RID: 12425
		public Instruction HandlerEnd;

		// Token: 0x0400308A RID: 12426
		public ITypeDefOrRef CatchType;

		// Token: 0x0400308B RID: 12427
		public ExceptionHandlerType HandlerType;
	}
}
