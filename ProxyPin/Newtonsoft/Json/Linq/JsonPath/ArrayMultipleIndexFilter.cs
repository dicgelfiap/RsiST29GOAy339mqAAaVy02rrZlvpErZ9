using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000B2D RID: 2861
	[NullableContext(1)]
	[Nullable(0)]
	internal class ArrayMultipleIndexFilter : PathFilter
	{
		// Token: 0x060073FC RID: 29692 RVA: 0x0022D748 File Offset: 0x0022D748
		public ArrayMultipleIndexFilter(List<int> indexes)
		{
			this.Indexes = indexes;
		}

		// Token: 0x060073FD RID: 29693 RVA: 0x0022D758 File Offset: 0x0022D758
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken t in current)
			{
				foreach (int index in this.Indexes)
				{
					JToken tokenIndex = PathFilter.GetTokenIndex(t, errorWhenNoMatch, index);
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				List<int>.Enumerator enumerator2 = default(List<int>.Enumerator);
				t = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040038AC RID: 14508
		internal List<int> Indexes;
	}
}
