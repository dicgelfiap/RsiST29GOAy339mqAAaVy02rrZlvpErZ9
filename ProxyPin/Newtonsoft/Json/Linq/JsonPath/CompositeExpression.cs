using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000B35 RID: 2869
	[NullableContext(1)]
	[Nullable(0)]
	internal class CompositeExpression : QueryExpression
	{
		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06007428 RID: 29736 RVA: 0x0022ED48 File Offset: 0x0022ED48
		// (set) Token: 0x06007429 RID: 29737 RVA: 0x0022ED50 File Offset: 0x0022ED50
		public List<QueryExpression> Expressions { get; set; }

		// Token: 0x0600742A RID: 29738 RVA: 0x0022ED5C File Offset: 0x0022ED5C
		public CompositeExpression(QueryOperator @operator) : base(@operator)
		{
			this.Expressions = new List<QueryExpression>();
		}

		// Token: 0x0600742B RID: 29739 RVA: 0x0022ED70 File Offset: 0x0022ED70
		public override bool IsMatch(JToken root, JToken t)
		{
			QueryOperator @operator = this.Operator;
			if (@operator == QueryOperator.And)
			{
				using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsMatch(root, t))
						{
							return false;
						}
					}
				}
				return true;
			}
			if (@operator != QueryOperator.Or)
			{
				throw new ArgumentOutOfRangeException();
			}
			using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMatch(root, t))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
