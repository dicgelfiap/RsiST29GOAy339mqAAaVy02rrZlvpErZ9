using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkUI.Collections
{
	// Token: 0x020000B9 RID: 185
	public class ObservableList<T> : List<T>, IDisposable
	{
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060007A3 RID: 1955 RVA: 0x0003FB20 File Offset: 0x0003FB20
		// (remove) Token: 0x060007A4 RID: 1956 RVA: 0x0003FB5C File Offset: 0x0003FB5C
		public event EventHandler<ObservableListModified<T>> ItemsAdded;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060007A5 RID: 1957 RVA: 0x0003FB98 File Offset: 0x0003FB98
		// (remove) Token: 0x060007A6 RID: 1958 RVA: 0x0003FBD4 File Offset: 0x0003FBD4
		public event EventHandler<ObservableListModified<T>> ItemsRemoved;

		// Token: 0x060007A7 RID: 1959 RVA: 0x0003FC10 File Offset: 0x0003FC10
		~ObservableList()
		{
			this.Dispose(false);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0003FC40 File Offset: 0x0003FC40
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0003FC50 File Offset: 0x0003FC50
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (this.ItemsAdded != null)
				{
					this.ItemsAdded = null;
				}
				if (this.ItemsRemoved != null)
				{
					this.ItemsRemoved = null;
				}
				this._disposed = true;
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0003FC88 File Offset: 0x0003FC88
		public new void Add(T item)
		{
			base.Add(item);
			if (this.ItemsAdded != null)
			{
				this.ItemsAdded(this, new ObservableListModified<T>(new List<T>
				{
					item
				}));
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0003FCBC File Offset: 0x0003FCBC
		public new void AddRange(IEnumerable<T> collection)
		{
			List<T> list = collection.ToList<T>();
			base.AddRange(list);
			if (this.ItemsAdded != null)
			{
				this.ItemsAdded(this, new ObservableListModified<T>(list));
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0003FCF8 File Offset: 0x0003FCF8
		public new void Remove(T item)
		{
			base.Remove(item);
			if (this.ItemsRemoved != null)
			{
				this.ItemsRemoved(this, new ObservableListModified<T>(new List<T>
				{
					item
				}));
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0003FD2C File Offset: 0x0003FD2C
		public new void Clear()
		{
			ObservableListModified<T> observableListModified = new ObservableListModified<T>(this.ToList<T>());
			base.Clear();
			if (observableListModified.Items.Count<T>() > 0 && this.ItemsRemoved != null)
			{
				this.ItemsRemoved(this, observableListModified);
			}
		}

		// Token: 0x04000559 RID: 1369
		private bool _disposed;
	}
}
