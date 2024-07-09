using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AD9 RID: 2777
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorContext
	{
		// Token: 0x06006E8E RID: 28302 RVA: 0x00217E2C File Offset: 0x00217E2C
		internal ErrorContext([Nullable(2)] object originalObject, [Nullable(2)] object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x06006E8F RID: 28303 RVA: 0x00217E54 File Offset: 0x00217E54
		// (set) Token: 0x06006E90 RID: 28304 RVA: 0x00217E5C File Offset: 0x00217E5C
		internal bool Traced { get; set; }

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x06006E91 RID: 28305 RVA: 0x00217E68 File Offset: 0x00217E68
		public Exception Error { get; }

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06006E92 RID: 28306 RVA: 0x00217E70 File Offset: 0x00217E70
		[Nullable(2)]
		public object OriginalObject { [NullableContext(2)] get; }

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x06006E93 RID: 28307 RVA: 0x00217E78 File Offset: 0x00217E78
		[Nullable(2)]
		public object Member { [NullableContext(2)] get; }

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06006E94 RID: 28308 RVA: 0x00217E80 File Offset: 0x00217E80
		public string Path { get; }

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06006E95 RID: 28309 RVA: 0x00217E88 File Offset: 0x00217E88
		// (set) Token: 0x06006E96 RID: 28310 RVA: 0x00217E90 File Offset: 0x00217E90
		public bool Handled { get; set; }
	}
}
