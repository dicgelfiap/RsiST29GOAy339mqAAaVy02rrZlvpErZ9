using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dnlib.Utils
{
	// Token: 0x02000740 RID: 1856
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebugView<, >))]
	[ComVisible(true)]
	public class LazyList<TValue, TContext> : LazyList<TValue>, ILazyList<TValue>, IList<TValue>, ICollection<TValue>, IEnumerable<!0>, IEnumerable where TValue : class
	{
		// Token: 0x0600410A RID: 16650 RVA: 0x0016271C File Offset: 0x0016271C
		public LazyList() : this(null)
		{
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x00162728 File Offset: 0x00162728
		public LazyList(IListListener<TValue> listener) : base(listener)
		{
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x00162734 File Offset: 0x00162734
		public LazyList(int length, TContext context, Func<TContext, int, TValue> readOriginalValue) : this(length, null, context, readOriginalValue)
		{
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x00162740 File Offset: 0x00162740
		public LazyList(int length, IListListener<TValue> listener, TContext context, Func<TContext, int, TValue> readOriginalValue) : base(length, listener)
		{
			this.context = context;
			this.readOriginalValue = readOriginalValue;
			for (int i = 0; i < length; i++)
			{
				this.list.Add(new LazyList<TValue, TContext>.LazyElement(i, this));
			}
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x0016278C File Offset: 0x0016278C
		private TValue ReadOriginalValue_NoLock(LazyList<TValue, TContext>.LazyElement elem)
		{
			return this.ReadOriginalValue_NoLock(this.list.IndexOf(elem), elem.origIndex);
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x001627A8 File Offset: 0x001627A8
		private TValue ReadOriginalValue_NoLock(int index, int origIndex)
		{
			TValue result = this.readOriginalValue(this.context, origIndex);
			IListListener<TValue> listener = this.listener;
			if (listener != null)
			{
				listener.OnLazyAdd(index, ref result);
			}
			return result;
		}

		// Token: 0x0400228D RID: 8845
		private TContext context;

		// Token: 0x0400228E RID: 8846
		private readonly Func<TContext, int, TValue> readOriginalValue;

		// Token: 0x02000FC1 RID: 4033
		private sealed class LazyElement : LazyList<TValue>.Element
		{
			// Token: 0x17001DB5 RID: 7605
			// (get) Token: 0x06008D93 RID: 36243 RVA: 0x002A6E50 File Offset: 0x002A6E50
			public override bool IsInitialized_NoLock
			{
				get
				{
					return this.lazyList == null;
				}
			}

			// Token: 0x06008D94 RID: 36244 RVA: 0x002A6E5C File Offset: 0x002A6E5C
			public override TValue GetValue_NoLock(int index)
			{
				if (this.lazyList != null)
				{
					this.value = this.lazyList.ReadOriginalValue_NoLock(index, this.origIndex);
					this.lazyList = null;
				}
				return this.value;
			}

			// Token: 0x06008D95 RID: 36245 RVA: 0x002A6E90 File Offset: 0x002A6E90
			public override void SetValue_NoLock(int index, TValue value)
			{
				this.value = value;
				this.lazyList = null;
			}

			// Token: 0x06008D96 RID: 36246 RVA: 0x002A6EA0 File Offset: 0x002A6EA0
			public LazyElement(int origIndex, LazyList<TValue, TContext> lazyList)
			{
				this.origIndex = origIndex;
				this.lazyList = lazyList;
			}

			// Token: 0x06008D97 RID: 36247 RVA: 0x002A6EB8 File Offset: 0x002A6EB8
			public override string ToString()
			{
				if (this.lazyList != null)
				{
					this.value = this.lazyList.ReadOriginalValue_NoLock(this);
					this.lazyList = null;
				}
				if (this.value != null)
				{
					return this.value.ToString();
				}
				return string.Empty;
			}

			// Token: 0x040042FC RID: 17148
			internal readonly int origIndex;

			// Token: 0x040042FD RID: 17149
			private LazyList<TValue, TContext> lazyList;
		}
	}
}
