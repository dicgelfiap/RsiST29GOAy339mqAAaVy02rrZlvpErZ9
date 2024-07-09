using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000A75 RID: 2677
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonArrayAttribute : JsonContainerAttribute
	{
		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x0600683E RID: 26686 RVA: 0x001FB478 File Offset: 0x001FB478
		// (set) Token: 0x0600683F RID: 26687 RVA: 0x001FB480 File Offset: 0x001FB480
		public bool AllowNullItems
		{
			get
			{
				return this._allowNullItems;
			}
			set
			{
				this._allowNullItems = value;
			}
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x001FB48C File Offset: 0x001FB48C
		public JsonArrayAttribute()
		{
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x001FB494 File Offset: 0x001FB494
		public JsonArrayAttribute(bool allowNullItems)
		{
			this._allowNullItems = allowNullItems;
		}

		// Token: 0x06006842 RID: 26690 RVA: 0x001FB4A4 File Offset: 0x001FB4A4
		[NullableContext(1)]
		public JsonArrayAttribute(string id) : base(id)
		{
		}

		// Token: 0x04003527 RID: 13607
		private bool _allowNullItems;
	}
}
