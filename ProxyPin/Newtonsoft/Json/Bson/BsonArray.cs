using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B67 RID: 2919
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x060075AA RID: 30122 RVA: 0x00235CD0 File Offset: 0x00235CD0
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x060075AB RID: 30123 RVA: 0x00235CE8 File Offset: 0x00235CE8
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x060075AC RID: 30124 RVA: 0x00235CEC File Offset: 0x00235CEC
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x060075AD RID: 30125 RVA: 0x00235D00 File Offset: 0x00235D00
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400391C RID: 14620
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
