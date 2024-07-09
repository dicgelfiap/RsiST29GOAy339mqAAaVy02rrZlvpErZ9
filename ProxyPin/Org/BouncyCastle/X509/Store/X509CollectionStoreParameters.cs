using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x0200070D RID: 1805
	public class X509CollectionStoreParameters : IX509StoreParameters
	{
		// Token: 0x06003F18 RID: 16152 RVA: 0x0015A948 File Offset: 0x0015A948
		public X509CollectionStoreParameters(ICollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.collection = Platform.CreateArrayList(collection);
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x0015A970 File Offset: 0x0015A970
		public ICollection GetCollection()
		{
			return Platform.CreateArrayList(this.collection);
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x0015A980 File Offset: 0x0015A980
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("X509CollectionStoreParameters: [\n");
			stringBuilder.Append("  collection: " + this.collection + "\n");
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04002089 RID: 8329
		private readonly IList collection;
	}
}
