using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000A82 RID: 2690
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x060068C6 RID: 26822 RVA: 0x001FC2C0 File Offset: 0x001FC2C0
		// (set) Token: 0x060068C7 RID: 26823 RVA: 0x001FC2C8 File Offset: 0x001FC2C8
		public MemberSerialization MemberSerialization
		{
			get
			{
				return this._memberSerialization;
			}
			set
			{
				this._memberSerialization = value;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x060068C8 RID: 26824 RVA: 0x001FC2D4 File Offset: 0x001FC2D4
		// (set) Token: 0x060068C9 RID: 26825 RVA: 0x001FC2E4 File Offset: 0x001FC2E4
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x060068CA RID: 26826 RVA: 0x001FC2F4 File Offset: 0x001FC2F4
		// (set) Token: 0x060068CB RID: 26827 RVA: 0x001FC304 File Offset: 0x001FC304
		public NullValueHandling ItemNullValueHandling
		{
			get
			{
				return this._itemNullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._itemNullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x060068CC RID: 26828 RVA: 0x001FC314 File Offset: 0x001FC314
		// (set) Token: 0x060068CD RID: 26829 RVA: 0x001FC324 File Offset: 0x001FC324
		public Required ItemRequired
		{
			get
			{
				return this._itemRequired.GetValueOrDefault();
			}
			set
			{
				this._itemRequired = new Required?(value);
			}
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x001FC334 File Offset: 0x001FC334
		public JsonObjectAttribute()
		{
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x001FC33C File Offset: 0x001FC33C
		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x001FC34C File Offset: 0x001FC34C
		[NullableContext(1)]
		public JsonObjectAttribute(string id) : base(id)
		{
		}

		// Token: 0x04003540 RID: 13632
		private MemberSerialization _memberSerialization;

		// Token: 0x04003541 RID: 13633
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x04003542 RID: 13634
		internal Required? _itemRequired;

		// Token: 0x04003543 RID: 13635
		internal NullValueHandling? _itemNullValueHandling;
	}
}
