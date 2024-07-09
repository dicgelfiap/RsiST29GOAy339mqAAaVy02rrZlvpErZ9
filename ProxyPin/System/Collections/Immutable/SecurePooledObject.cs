using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000CC4 RID: 3268
	internal class SecurePooledObject<T>
	{
		// Token: 0x060083DE RID: 33758 RVA: 0x00268688 File Offset: 0x00268688
		internal SecurePooledObject(T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			this._value = newValue;
		}

		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x060083DF RID: 33759 RVA: 0x002686A4 File Offset: 0x002686A4
		// (set) Token: 0x060083E0 RID: 33760 RVA: 0x002686AC File Offset: 0x002686AC
		internal int Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x060083E1 RID: 33761 RVA: 0x002686B8 File Offset: 0x002686B8
		internal T Use<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			if (!this.IsOwned<TCaller>(ref caller))
			{
				Requires.FailObjectDisposed<TCaller>(caller);
			}
			return this._value;
		}

		// Token: 0x060083E2 RID: 33762 RVA: 0x002686D8 File Offset: 0x002686D8
		internal bool TryUse<TCaller>(ref TCaller caller, out T value) where TCaller : struct, ISecurePooledObjectUser
		{
			if (this.IsOwned<TCaller>(ref caller))
			{
				value = this._value;
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x060083E3 RID: 33763 RVA: 0x002686FC File Offset: 0x002686FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool IsOwned<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			return caller.PoolUserId == this._owner;
		}

		// Token: 0x04003D53 RID: 15699
		private readonly T _value;

		// Token: 0x04003D54 RID: 15700
		private int _owner;
	}
}
