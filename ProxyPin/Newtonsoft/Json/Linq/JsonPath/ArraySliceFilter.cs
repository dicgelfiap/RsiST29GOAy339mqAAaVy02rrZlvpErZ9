using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000B2E RID: 2862
	internal class ArraySliceFilter : PathFilter
	{
		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x060073FE RID: 29694 RVA: 0x0022D778 File Offset: 0x0022D778
		// (set) Token: 0x060073FF RID: 29695 RVA: 0x0022D780 File Offset: 0x0022D780
		public int? Start { get; set; }

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06007400 RID: 29696 RVA: 0x0022D78C File Offset: 0x0022D78C
		// (set) Token: 0x06007401 RID: 29697 RVA: 0x0022D794 File Offset: 0x0022D794
		public int? End { get; set; }

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06007402 RID: 29698 RVA: 0x0022D7A0 File Offset: 0x0022D7A0
		// (set) Token: 0x06007403 RID: 29699 RVA: 0x0022D7A8 File Offset: 0x0022D7A8
		public int? Step { get; set; }

		// Token: 0x06007404 RID: 29700 RVA: 0x0022D7B4 File Offset: 0x0022D7B4
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			int? num = this.Step;
			int num2 = 0;
			if (num.GetValueOrDefault() == num2 & num != null)
			{
				throw new JsonException("Step cannot be zero.");
			}
			foreach (JToken jtoken in current)
			{
				JArray a = jtoken as JArray;
				if (a != null)
				{
					int stepCount = this.Step ?? 1;
					int num3 = this.Start ?? ((stepCount > 0) ? 0 : (a.Count - 1));
					int stopIndex = this.End ?? ((stepCount > 0) ? a.Count : -1);
					num = this.Start;
					num2 = 0;
					if (num.GetValueOrDefault() < num2 & num != null)
					{
						num3 = a.Count + num3;
					}
					num = this.End;
					num2 = 0;
					if (num.GetValueOrDefault() < num2 & num != null)
					{
						stopIndex = a.Count + stopIndex;
					}
					num3 = Math.Max(num3, (stepCount > 0) ? 0 : int.MinValue);
					num3 = Math.Min(num3, (stepCount > 0) ? a.Count : (a.Count - 1));
					stopIndex = Math.Max(stopIndex, -1);
					stopIndex = Math.Min(stopIndex, a.Count);
					bool positiveStep = stepCount > 0;
					if (this.IsValid(num3, stopIndex, positiveStep))
					{
						int i = num3;
						while (this.IsValid(i, stopIndex, positiveStep))
						{
							yield return a[i];
							i += stepCount;
						}
					}
					else if (errorWhenNoMatch)
					{
						throw new JsonException("Array slice of {0} to {1} returned no results.".FormatWith(CultureInfo.InvariantCulture, (this.Start != null) ? this.Start.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*", (this.End != null) ? this.End.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*"));
					}
				}
				else if (errorWhenNoMatch)
				{
					throw new JsonException("Array slice is not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, jtoken.GetType().Name));
				}
				a = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x0022D7D4 File Offset: 0x0022D7D4
		private bool IsValid(int index, int stopIndex, bool positiveStep)
		{
			if (positiveStep)
			{
				return index < stopIndex;
			}
			return index > stopIndex;
		}
	}
}
