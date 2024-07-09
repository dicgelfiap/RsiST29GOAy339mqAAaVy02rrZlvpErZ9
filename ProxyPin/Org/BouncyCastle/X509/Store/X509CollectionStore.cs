using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x0200070C RID: 1804
	internal class X509CollectionStore : IX509Store
	{
		// Token: 0x06003F16 RID: 16150 RVA: 0x0015A8B0 File Offset: 0x0015A8B0
		internal X509CollectionStore(ICollection collection)
		{
			this._local = Platform.CreateArrayList(collection);
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x0015A8C4 File Offset: 0x0015A8C4
		public ICollection GetMatches(IX509Selector selector)
		{
			if (selector == null)
			{
				return Platform.CreateArrayList(this._local);
			}
			IList list = Platform.CreateArrayList();
			foreach (object obj in this._local)
			{
				if (selector.Match(obj))
				{
					list.Add(obj);
				}
			}
			return list;
		}

		// Token: 0x04002088 RID: 8328
		private ICollection _local;
	}
}
