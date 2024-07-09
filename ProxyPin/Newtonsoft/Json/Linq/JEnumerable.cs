using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B1B RID: 2843
	[NullableContext(1)]
	[Nullable(0)]
	[Newtonsoft.Json.IsReadOnly]
	public struct JEnumerable<[Nullable(0)] T> : IJEnumerable<T>, IEnumerable<!0>, IEnumerable, IEquatable<JEnumerable<T>> where T : JToken
	{
		// Token: 0x06007244 RID: 29252 RVA: 0x002272B4 File Offset: 0x002272B4
		public JEnumerable(IEnumerable<T> enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x06007245 RID: 29253 RVA: 0x002272C8 File Offset: 0x002272C8
		public IEnumerator<T> GetEnumerator()
		{
			return (this._enumerable ?? JEnumerable<T>.Empty).GetEnumerator();
		}

		// Token: 0x06007246 RID: 29254 RVA: 0x002272E8 File Offset: 0x002272E8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170017D1 RID: 6097
		public IJEnumerable<JToken> this[object key]
		{
			get
			{
				if (this._enumerable == null)
				{
					return JEnumerable<JToken>.Empty;
				}
				return new JEnumerable<JToken>(this._enumerable.Values(key));
			}
		}

		// Token: 0x06007248 RID: 29256 RVA: 0x00227320 File Offset: 0x00227320
		public bool Equals([Nullable(new byte[]
		{
			0,
			1
		})] JEnumerable<T> other)
		{
			return object.Equals(this._enumerable, other._enumerable);
		}

		// Token: 0x06007249 RID: 29257 RVA: 0x00227334 File Offset: 0x00227334
		public override bool Equals(object obj)
		{
			if (obj is JEnumerable<T>)
			{
				JEnumerable<T> other = (JEnumerable<T>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600724A RID: 29258 RVA: 0x00227360 File Offset: 0x00227360
		public override int GetHashCode()
		{
			if (this._enumerable == null)
			{
				return 0;
			}
			return this._enumerable.GetHashCode();
		}

		// Token: 0x04003865 RID: 14437
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public static readonly JEnumerable<T> Empty = new JEnumerable<T>(Enumerable.Empty<T>());

		// Token: 0x04003866 RID: 14438
		private readonly IEnumerable<T> _enumerable;
	}
}
