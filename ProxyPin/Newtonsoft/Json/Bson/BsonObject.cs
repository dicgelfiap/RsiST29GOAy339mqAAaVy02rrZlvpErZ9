using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B66 RID: 2918
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x060075A5 RID: 30117 RVA: 0x00235C60 File Offset: 0x00235C60
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x060075A6 RID: 30118 RVA: 0x00235C9C File Offset: 0x00235C9C
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x060075A7 RID: 30119 RVA: 0x00235CA0 File Offset: 0x00235CA0
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x060075A8 RID: 30120 RVA: 0x00235CB4 File Offset: 0x00235CB4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400391B RID: 14619
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
