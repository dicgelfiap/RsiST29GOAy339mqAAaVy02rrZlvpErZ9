using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000B3B RID: 2875
	[NullableContext(1)]
	[Nullable(0)]
	internal class ScanMultipleFilter : PathFilter
	{
		// Token: 0x0600743C RID: 29756 RVA: 0x0022F3B0 File Offset: 0x0022F3B0
		public ScanMultipleFilter(List<string> names)
		{
			this._names = names;
		}

		// Token: 0x0600743D RID: 29757 RVA: 0x0022F3C0 File Offset: 0x0022F3C0
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken c in current)
			{
				JToken value = c;
				for (;;)
				{
					JContainer container = value as JContainer;
					value = PathFilter.GetNextScanValue(c, container, value);
					if (value == null)
					{
						break;
					}
					JProperty property = value as JProperty;
					if (property != null)
					{
						foreach (string b in this._names)
						{
							if (property.Name == b)
							{
								yield return property.Value;
							}
						}
						List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
					}
					property = null;
				}
				value = null;
				c = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040038CC RID: 14540
		private List<string> _names;
	}
}
