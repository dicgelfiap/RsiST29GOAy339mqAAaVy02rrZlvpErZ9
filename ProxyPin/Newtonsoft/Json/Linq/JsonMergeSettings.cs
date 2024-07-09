using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B22 RID: 2850
	public class JsonMergeSettings
	{
		// Token: 0x060072DA RID: 29402 RVA: 0x00228B64 File Offset: 0x00228B64
		public JsonMergeSettings()
		{
			this._propertyNameComparison = StringComparison.Ordinal;
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x060072DB RID: 29403 RVA: 0x00228B74 File Offset: 0x00228B74
		// (set) Token: 0x060072DC RID: 29404 RVA: 0x00228B7C File Offset: 0x00228B7C
		public MergeArrayHandling MergeArrayHandling
		{
			get
			{
				return this._mergeArrayHandling;
			}
			set
			{
				if (value < MergeArrayHandling.Concat || value > MergeArrayHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeArrayHandling = value;
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x060072DD RID: 29405 RVA: 0x00228BA0 File Offset: 0x00228BA0
		// (set) Token: 0x060072DE RID: 29406 RVA: 0x00228BA8 File Offset: 0x00228BA8
		public MergeNullValueHandling MergeNullValueHandling
		{
			get
			{
				return this._mergeNullValueHandling;
			}
			set
			{
				if (value < MergeNullValueHandling.Ignore || value > MergeNullValueHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeNullValueHandling = value;
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x060072DF RID: 29407 RVA: 0x00228BCC File Offset: 0x00228BCC
		// (set) Token: 0x060072E0 RID: 29408 RVA: 0x00228BD4 File Offset: 0x00228BD4
		public StringComparison PropertyNameComparison
		{
			get
			{
				return this._propertyNameComparison;
			}
			set
			{
				if (value < StringComparison.CurrentCulture || value > StringComparison.OrdinalIgnoreCase)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._propertyNameComparison = value;
			}
		}

		// Token: 0x04003871 RID: 14449
		private MergeArrayHandling _mergeArrayHandling;

		// Token: 0x04003872 RID: 14450
		private MergeNullValueHandling _mergeNullValueHandling;

		// Token: 0x04003873 RID: 14451
		private StringComparison _propertyNameComparison;
	}
}
