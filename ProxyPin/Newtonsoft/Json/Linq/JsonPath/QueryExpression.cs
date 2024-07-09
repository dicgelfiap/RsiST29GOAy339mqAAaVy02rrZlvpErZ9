using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000B34 RID: 2868
	internal abstract class QueryExpression
	{
		// Token: 0x06007426 RID: 29734 RVA: 0x0022ED38 File Offset: 0x0022ED38
		public QueryExpression(QueryOperator @operator)
		{
			this.Operator = @operator;
		}

		// Token: 0x06007427 RID: 29735
		[NullableContext(1)]
		public abstract bool IsMatch(JToken root, JToken t);

		// Token: 0x040038C4 RID: 14532
		internal QueryOperator Operator;
	}
}
