using System;

namespace System.Collections.Immutable
{
	// Token: 0x02000CC2 RID: 3266
	internal class SecureObjectPool<T, TCaller> where TCaller : ISecurePooledObjectUser
	{
		// Token: 0x060083D9 RID: 33753 RVA: 0x002685E4 File Offset: 0x002685E4
		public void TryAdd(TCaller caller, SecurePooledObject<T> item)
		{
			if (caller.PoolUserId == item.Owner)
			{
				item.Owner = -1;
				AllocFreeConcurrentStack<SecurePooledObject<T>>.TryAdd(item);
			}
		}

		// Token: 0x060083DA RID: 33754 RVA: 0x0026860C File Offset: 0x0026860C
		public bool TryTake(TCaller caller, out SecurePooledObject<T> item)
		{
			if (caller.PoolUserId != -1 && AllocFreeConcurrentStack<SecurePooledObject<T>>.TryTake(out item))
			{
				item.Owner = caller.PoolUserId;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x060083DB RID: 33755 RVA: 0x00268648 File Offset: 0x00268648
		public SecurePooledObject<T> PrepNew(TCaller caller, T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			return new SecurePooledObject<T>(newValue)
			{
				Owner = caller.PoolUserId
			};
		}
	}
}
