using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000B39 RID: 2873
	[NullableContext(1)]
	[Nullable(0)]
	internal class RootFilter : PathFilter
	{
		// Token: 0x06007437 RID: 29751 RVA: 0x0022F368 File Offset: 0x0022F368
		private RootFilter()
		{
		}

		// Token: 0x06007438 RID: 29752 RVA: 0x0022F370 File Offset: 0x0022F370
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			return new JToken[]
			{
				root
			};
		}

		// Token: 0x040038CA RID: 14538
		public static readonly RootFilter Instance = new RootFilter();
	}
}
