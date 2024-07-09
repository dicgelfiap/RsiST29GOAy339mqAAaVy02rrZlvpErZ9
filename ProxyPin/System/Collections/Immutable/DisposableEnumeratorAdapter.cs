using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000C96 RID: 3222
	internal struct DisposableEnumeratorAdapter<T, TEnumerator> : IDisposable where TEnumerator : struct, IEnumerator<!0>
	{
		// Token: 0x060080CA RID: 32970 RVA: 0x0026175C File Offset: 0x0026175C
		internal DisposableEnumeratorAdapter(TEnumerator enumerator)
		{
			this._enumeratorStruct = enumerator;
			this._enumeratorObject = null;
		}

		// Token: 0x060080CB RID: 32971 RVA: 0x0026176C File Offset: 0x0026176C
		internal DisposableEnumeratorAdapter(IEnumerator<T> enumerator)
		{
			this._enumeratorStruct = default(TEnumerator);
			this._enumeratorObject = enumerator;
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x060080CC RID: 32972 RVA: 0x00261784 File Offset: 0x00261784
		public T Current
		{
			get
			{
				if (this._enumeratorObject == null)
				{
					return this._enumeratorStruct.Current;
				}
				return this._enumeratorObject.Current;
			}
		}

		// Token: 0x060080CD RID: 32973 RVA: 0x002617B0 File Offset: 0x002617B0
		public bool MoveNext()
		{
			if (this._enumeratorObject == null)
			{
				return this._enumeratorStruct.MoveNext();
			}
			return this._enumeratorObject.MoveNext();
		}

		// Token: 0x060080CE RID: 32974 RVA: 0x002617DC File Offset: 0x002617DC
		public void Dispose()
		{
			if (this._enumeratorObject != null)
			{
				this._enumeratorObject.Dispose();
				return;
			}
			this._enumeratorStruct.Dispose();
		}

		// Token: 0x060080CF RID: 32975 RVA: 0x00261808 File Offset: 0x00261808
		public DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04003D22 RID: 15650
		private readonly IEnumerator<T> _enumeratorObject;

		// Token: 0x04003D23 RID: 15651
		private TEnumerator _enumeratorStruct;
	}
}
