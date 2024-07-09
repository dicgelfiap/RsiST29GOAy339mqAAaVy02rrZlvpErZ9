using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000ADA RID: 2778
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06006E97 RID: 28311 RVA: 0x00217E9C File Offset: 0x00217E9C
		[Nullable(2)]
		public object CurrentObject { [NullableContext(2)] get; }

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06006E98 RID: 28312 RVA: 0x00217EA4 File Offset: 0x00217EA4
		public ErrorContext ErrorContext { get; }

		// Token: 0x06006E99 RID: 28313 RVA: 0x00217EAC File Offset: 0x00217EAC
		public ErrorEventArgs([Nullable(2)] object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}
	}
}
